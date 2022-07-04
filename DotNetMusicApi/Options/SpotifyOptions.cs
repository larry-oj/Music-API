using System.Text.Json;
using DotNetMusicApi.Models.Spotify;

namespace DotNetMusicApi.Options;

public class SpotifyOptions
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _clientFactory;

    public SpotifyOptions(IConfiguration configuration,
        IHttpClientFactory clientFactory)
    {
        _configuration = configuration;
        _clientFactory = clientFactory;
        Token = null;
    }
    
    public string? Token { get; private set; }

    public async Task RefreshToken()
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
                new KeyValuePair<string, string>("grant_type", "client_credentials")
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

            var response = await client.SendAsync(request);
            content = await response.Content.ReadAsStringAsync();
        }

        var spotifyTokenResponse = JsonSerializer.Deserialize<SpotifyTokenResponse>(content);
        this.Token = spotifyTokenResponse!.Token;
    }
}