using System.Text.Json.Serialization;

namespace DotNetMusicApi.Services.Models.Spotify;

public class SpotifySearchResponse
{
    [JsonPropertyName("tracks")]
    public List<Track> Tracks { get; } = new List<Track>();
}

public class Album
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("images")]
    public List<Image> Images { get; } = new List<Image>();

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("uri")]
    public string Uri { get; set; }
}

public class Artist
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("uri")]
    public string Uri { get; set; }

    [JsonPropertyName("images")]
    public List<Image> Images { get; } = new List<Image>();
}

public class ExternalUrls
{
    [JsonPropertyName("spotify")]
    public string Spotify { get; set; }
}

public class Image
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }
}

public class Track
{
    [JsonPropertyName("album")]
    public Album Album { get; set; }

    [JsonPropertyName("artists")]
    public List<Artist> Artists { get; } = new List<Artist>();

    [JsonPropertyName("duration_ms")]
    public int DurationMs { get; set; }

    [JsonPropertyName("explicit")]
    public bool Explicit { get; set; }

    [JsonPropertyName("external_urls")]
    public ExternalUrls ExternalUrls { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("preview_url")]
    public string? PreviewUrl { get; set; }

    [JsonPropertyName("uri")]
    public string Uri { get; set; }
}

