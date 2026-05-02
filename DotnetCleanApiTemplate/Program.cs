using DotnetCleanApiTemplate.Api.Auth;
using DotnetCleanApiTemplate.Api.Endpoints;
using DotnetCleanApiTemplate.Api.Middleware;
using DotnetCleanApiTemplate.Application;
using DotnetCleanApiTemplate.Infrastructure;
using DotnetCleanApiTemplate.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables();

builder.Host.UseSerilog((context, loggerConfiguration) =>
{
	loggerConfiguration
		.ReadFrom.Configuration(context.Configuration)
		.Enrich.FromLogContext()
		.WriteTo.Console();
});

// Keep Program.cs as the composition root: register dependencies and middleware here.
builder.Services
	.AddApplication()
	.AddInfrastructure(builder.Configuration);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));
builder.Services.AddSingleton<ITokenService, JwtTokenService>();

var jwtSection = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()
	?? throw new InvalidOperationException("Jwt settings are not configured.");

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.SigningKey));

builder.Services
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateIssuerSigningKey = true,
			ValidateLifetime = true,
			ValidIssuer = jwtSection.Issuer,
			ValidAudience = jwtSection.Audience,
			IssuerSigningKey = signingKey,
			ClockSkew = TimeSpan.Zero
		};
	});

builder.Services.AddAuthorization();

builder.Services
	.AddHealthChecks()
	.AddDbContextCheck<AppDbContext>("database");

var serviceName = "DotnetCleanApiTemplate";

builder.Services
	.AddOpenTelemetry()
	.ConfigureResource(resource => resource.AddService(serviceName))
	.WithTracing(tracing =>
	{
		tracing
			.AddAspNetCoreInstrumentation()
			.AddHttpClientInstrumentation()
			.AddConsoleExporter();
	})
	.WithMetrics(metrics =>
	{
		metrics
			.AddAspNetCoreInstrumentation()
			.AddRuntimeInstrumentation()
			.AddConsoleExporter();
	});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	if (dbContext.Database.IsRelational())
	{
		dbContext.Database.Migrate();
	}
}

app.UseSerilogRequestLogging();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
	Predicate = _ => true
});

app.MapAuthEndpoints();
app.MapItemEndpoints();

app.Run();

public partial class Program;
