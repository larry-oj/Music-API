using System.Text.Json;
using DotNetMusicApi.Services.Models.Spotify;
using DotNetMusicApi.Services.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetMusicApi.Services;

public class TimedTokenService : IHostedService, IDisposable
{
    private readonly ILogger<TimedTokenService> _logger;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;
    private readonly SpotifyOptions _spotifyOptions;
    private Timer _timer;
    
    public TimedTokenService(ILogger<TimedTokenService> logger,
        IHttpClientFactory clientFactory, 
        IConfiguration configuration, 
        SpotifyOptions spotifyOptions)
    {
        _logger = logger;
        _clientFactory = clientFactory;
        _configuration = configuration;
        _spotifyOptions = spotifyOptions;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed token service is running");

        Generate(null);
        _timer = new Timer(Generate, null, TimeSpan.Zero, TimeSpan.FromMinutes(45));

        return Task.CompletedTask;
    }

    private void Generate(object? state)
    {
        var tokenUrl = _configuration.GetSection("Spotify:TokenUrl").Value;
        var clientId = _configuration.GetSection("Spotify:ClientId").Value;
        var clientSecret = _configuration.GetSection("Spotify:ClientSecret").Value;
        
        var bytes = System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}");
        var secret = System.Convert.ToBase64String(bytes);

        string content;

        using (var client = _clientFactory.CreateClient())
        {
            var nvc = new List<KeyValuePair<string, string>>
            {
                new("grant_type", "client_credentials")
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(tokenUrl),
                Headers =
                {
                    { "Authorization", $@"Basic {secret}" }
                },
                Content = new FormUrlEncodedContent(nvc)
            };

            var response = client.Send(request);
            content = response.Content.ReadAsStringAsync().Result;
        }

        var spotifyTokenResponse = JsonSerializer.Deserialize<SpotifyTokenResponse>(content);
        _spotifyOptions.Token = spotifyTokenResponse!.Token;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed token service is stopping");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}