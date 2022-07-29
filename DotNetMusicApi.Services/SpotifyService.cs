using System.Data;
using System.Text.Json;
using System.Text.RegularExpressions;
using DotNetMusicApi.Services.Extensions;
using DotNetMusicApi.Services.Models.Spotify;
using DotNetMusicApi.Services.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DotNetMusicApi.Services;

public class SpotifyService : ISpotifyService
{
    private readonly SpotifyOptions _spotifyOptions;
    private readonly IConfiguration _configuration;
    private readonly ILogger<SpotifyService> _logger;
    private readonly HttpClient _httpClient;

    public SpotifyService(SpotifyOptions spotifyOptions, 
        ILogger<SpotifyService> logger, 
        IHttpClientFactory clientFactory, 
        IConfiguration configuration)
    {
        _spotifyOptions = spotifyOptions;
        _logger = logger;
        _httpClient = clientFactory.CreateClient();
        _configuration = configuration;
    }

    public async Task<List<Track>> SearchTracksAsync(string query, int limit)
    {
        if (string.IsNullOrEmpty(_spotifyOptions.Token))
        {
            _logger.LogError("Spotify token is null");
            throw new DataException("Spotify token is null");
        }

        var uri = new Uri(_configuration.GetSection("Spotify:SearchUrl").Value)
            .AppendParameter("q", query)
            .AppendParameter("type", "track")
            .AppendParameter("limit", limit.ToString());

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = uri,
            Headers =
            {
                { "Authorization", $@"Bearer {_spotifyOptions.Token}" }
            }
        };
        
        var response = await _httpClient.SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        

        var spotifySearchResponse = JsonSerializer.Deserialize<SpotifySearchResponse>(content);
        return spotifySearchResponse!.Items.Tracks;
    }

    public async Task<string> GetTrackName(string url)
    {
        if (string.IsNullOrEmpty(_spotifyOptions.Token))
        {
            _logger.LogError("Spotify token is null");
            throw new Exception("Spotify token is null");
        }
        
        var trackId = Regex.Match(url, @"track\/(\w+)\?*").Groups[1].Value;

        if (string.IsNullOrEmpty(trackId))
        {
            _logger.LogError("Link is invalid (invalid id)");
            throw new ArgumentNullException("Link is invalid (invalid id)");
        }
        
        var uri = new Uri(_configuration.GetSection("Spotify:TrackUrl").Value.Replace(@"{id}", trackId));

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = uri,
            Headers =
            {
                { "Authorization", $@"Bearer {_spotifyOptions.Token}" }
            }
        };
        
        var response = await _httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new DataException("Link is invalid");
        
        var spotifyTrack = JsonSerializer.Deserialize<Track>(content);
        return spotifyTrack!.Name + " - " + spotifyTrack!.Artists[0].Name;
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}