using System.Data;
using System.Text.Json;
using DotNetMusicApi.Services.Extensions;
using DotNetMusicApi.Services.Models.YouTube;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DotNetMusicApi.Services;

public class YouTubeService : IYouTubeService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<YouTubeService> _logger;
    private readonly HttpClient _httpClient;

    public YouTubeService(IConfiguration configuration, 
        ILogger<YouTubeService> logger, 
        IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<List<Item>> SearchVideosAsync(string query, int limit)
    {
        var uri = new Uri(_configuration.GetSection("YouTube:SearchUrl").Value)
            .AppendParameter("key", _configuration.GetSection("YouTube:Token").Value)
            .AppendParameter("part", "snippet")
            .AppendParameter("maxResults", limit.ToString())
            .AppendParameter("q", query);

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = uri
        };

        var response = await _httpClient.SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();

            var youtubeSearchResponse = JsonSerializer.Deserialize<YouTubeSearchResponse>(content) ?? throw new InvalidOperationException();
        return youtubeSearchResponse.Items.
            Where(i => i.Id.Kind == "youtube#video")
            .ToList();
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}