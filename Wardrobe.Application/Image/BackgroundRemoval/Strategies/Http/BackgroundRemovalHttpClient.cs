using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Wardrobe.Application.Image.BackgroundRemoval.Strategies.Http;

public class BackgroundRemovalHttpClient : IBackgroundRemovalHttpClient
{
    private readonly HttpClient _client;
    private readonly ILogger<BackgroundRemovalHttpClient> _logger;
    
    private record BackgroundRemovalBody(string Url);

    public BackgroundRemovalHttpClient(HttpClient client, ILogger<BackgroundRemovalHttpClient> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<string> PostImage(string imageBase64)
    {
        var body = new BackgroundRemovalBody(Url: imageBase64);
        var json = JsonSerializer.Serialize(body);
        
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("BackgroundRemoverLambda", content);
        _logger.LogDebug("Response status code: {ResponseStatusCode}", response.StatusCode);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadAsStringAsync();
    }
    
}

public interface IBackgroundRemovalHttpClient
{
    Task<string> PostImage(string imageBase64);
}