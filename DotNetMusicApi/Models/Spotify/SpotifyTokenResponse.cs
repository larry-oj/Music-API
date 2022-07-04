using System.Text.Json.Serialization;

namespace DotNetMusicApi.Models.Spotify;

public class SpotifyTokenResponse
{
    [JsonPropertyName("access_token")]
    public string Token { get; set; } = default!;
}