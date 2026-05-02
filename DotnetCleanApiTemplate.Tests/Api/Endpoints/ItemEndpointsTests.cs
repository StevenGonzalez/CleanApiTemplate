using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using DotnetCleanApiTemplate.Domain.Entities;
using DotnetCleanApiTemplate.Tests.Api;

namespace DotnetCleanApiTemplate.Tests.Api.Endpoints;

public sealed class ItemEndpointsTests : IClassFixture<TestWebApplicationFactory>
{
    private static readonly Guid KnownItemId = Guid.Parse("e57e4bc8-8a7b-4a0e-bf8e-f56d7193b94b");

    private readonly HttpClient _client;

    public ItemEndpointsTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Health_ReturnsOk()
    {
        var response = await _client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetItems_ReturnsSeededItems()
    {
        var response = await _client.GetAsync("/items");
        var items = await response.Content.ReadFromJsonAsync<List<Item>>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(items);
        Assert.NotEmpty(items);
    }

    [Fact]
    public async Task GetItemById_WhenItemExists_ReturnsOk()
    {
        var response = await _client.GetAsync($"/items/{KnownItemId}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetItemById_WhenItemDoesNotExist_ReturnsNotFound()
    {
        var response = await _client.GetAsync($"/items/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateItem_WhenUnauthenticated_ReturnsUnauthorized()
    {
        var response = await _client.PostAsJsonAsync("/items", new { name = "New item" });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateItem_WhenAuthenticated_ReturnsCreated()
    {
        var authResponse = await _client.PostAsJsonAsync("/auth/token", new { username = "demo", password = "demo123" });
        var token = await authResponse.Content.ReadFromJsonAsync<TokenResponse>();

        Assert.Equal(HttpStatusCode.OK, authResponse.StatusCode);
        Assert.NotNull(token);

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token!.AccessToken);

        var response = await _client.PostAsJsonAsync("/items", new { name = "New item" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}

public sealed record TokenResponse(string AccessToken, DateTime ExpiresAtUtc);