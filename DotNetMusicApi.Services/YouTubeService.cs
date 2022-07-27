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
    private readonly IHttpClientFactory _httpClientFactory;

    public YouTubeService(IConfiguration configuration, 
        ILogger<YouTubeService> logger, 
        IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<Item>> SearchVideos(string query, int limit)
    {
        string content;
        using (var client = _httpClientFactory.CreateClient())
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

            var response = await client.SendAsync(request);
            content = await response.Content.ReadAsStringAsync();
        }

        var youtubeSearchResponse = JsonSerializer.Deserialize<YouTubeSearchResponse>(content) ?? throw new InvalidOperationException();
        return youtubeSearchResponse.Items.
            Where(i => i.Id.Kind == "youtube#video")
            .ToList();
    }
}