using System.Text.Json.Serialization;

namespace DotNetMusicApi.Services.Models.Spotify;

public class SpotifyTokenResponse
{
    [JsonPropertyName("access_token")] public string? Token { get; set; }
}