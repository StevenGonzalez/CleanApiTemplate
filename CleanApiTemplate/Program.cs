using CleanApiTemplate.Api.Endpoints;
using CleanApiTemplate.Api.Middleware;
using CleanApiTemplate.Application;
using CleanApiTemplate.Infrastructure;
using System.Text.Json;
using Microsoft.Extensions.Logging.Console;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables();

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole(options =>
{
	options.IncludeScopes = true;
	options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
	options.JsonWriterOptions = new JsonWriterOptions
	{
		Indented = false
	};
});

// Keep Program.cs as the composition root: register dependencies and middleware here.
builder.Services
	.AddApplication()
	.AddInfrastructure();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapItemEndpoints();

app.Run();

public partial class Program;
