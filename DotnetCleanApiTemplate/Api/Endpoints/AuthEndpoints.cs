using DotnetCleanApiTemplate.Api.Auth;
using Microsoft.Extensions.Options;

namespace DotnetCleanApiTemplate.Api.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth");

        group.MapPost("/token", (
            TokenRequest request,
            ITokenService tokenService,
            IOptions<JwtOptions> jwtOptions) =>
        {
            var options = jwtOptions.Value;

            if (!string.Equals(request.Username, options.DemoUsername, StringComparison.Ordinal) ||
                !string.Equals(request.Password, options.DemoPassword, StringComparison.Ordinal))
            {
                return Results.Unauthorized();
            }

            var token = tokenService.CreateAccessToken(request.Username);
            return Results.Ok(token);
        })
        .WithName("GetAccessToken");

        return app;
    }
}

public sealed record TokenRequest(string Username, string Password);
