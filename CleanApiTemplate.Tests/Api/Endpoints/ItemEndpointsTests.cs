using System.Net;
using System.Net.Http.Json;
using CleanApiTemplate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CleanApiTemplate.Tests.Api.Endpoints;

public sealed class ItemEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly Guid KnownItemId = Guid.Parse("e57e4bc8-8a7b-4a0e-bf8e-f56d7193b94b");

    private readonly HttpClient _client;

    public ItemEndpointsTests(WebApplicationFactory<Program> factory)
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
}