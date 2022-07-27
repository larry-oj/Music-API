﻿using System.Data;
using System.Text.Json;
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
    private readonly IHttpClientFactory _httpClientFactory;

    public SpotifyService(SpotifyOptions spotifyOptions, 
        ILogger<SpotifyService> logger, 
        IHttpClientFactory clientFactory, 
        IConfiguration configuration)
    {
        _spotifyOptions = spotifyOptions;
        _logger = logger;
        _httpClientFactory = clientFactory;
        _configuration = configuration;
    }

    public async Task<List<Track>> SearchTracks(string query, int limit)
    {
        if (string.IsNullOrEmpty(_spotifyOptions.Token))
        {
            _logger.LogError("Spotify token is null");
            throw new DataException("Spotify token is null");
        }
        
        string content;
        using (var client = _httpClientFactory.CreateClient())
        {
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
            
            var response = await client.SendAsync(request);
            content = await response.Content.ReadAsStringAsync();
        }

        var spotifySearchResponse = JsonSerializer.Deserialize<SpotifySearchResponse>(content);
        return spotifySearchResponse!.Tracks;
    }
}