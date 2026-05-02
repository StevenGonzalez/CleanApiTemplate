namespace DotnetCleanApiTemplate.Api.Auth;

public interface ITokenService
{
    TokenResponse CreateAccessToken(string username);
}

public sealed record TokenResponse(string AccessToken, DateTime ExpiresAtUtc);
