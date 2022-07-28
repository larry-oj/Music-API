using System.Text.Json.Serialization;

namespace DotNetMusicApi.Models;

public class SpotifySearchResponse
{
    [JsonPropertyName("tracks")]
    public List<Track> Tracks { get; set; }

    public SpotifySearchResponse(List<Services.Models.Spotify.Track> obj)
    {
        Tracks = new List<Track>();

        foreach (var track in obj)
        {
            Tracks.Add(new Track
            {
                Name = track.Name,
                Album = new Album
                {
                    Name = track.Album?.Name!,
                    Covers = track.Album?.Images?
                        .Select(image => new Image
                        {
                            Url = image.Url, 
                            Height = image.Height, 
                            Width = image.Width
                        }).ToList() ?? default!,
                    ReleaseDate = track.Album?.ReleaseDate!,
                    ExternalUrls = new ExternalUrls
                    {
                        Spotify = track.Album?.ExternalUrls.Spotify!
                    }
                },
                Artists = track.Artists?
                    .Select(artist => new Artist
                    {
                        Name = artist.Name,
                        Images = artist.Images?
                            .Select(image => new Image
                            {
                                Url = image.Url,
                                Height = image.Height,
                                Width = image.Width
                            }).ToList(),
                        ExternalUrls = new ExternalUrls
                        {
                            Spotify = artist.ExternalUrls.Spotify!
                        }
                    }).ToList(),
                DurationMs = track.DurationMs,
                Explicit = track.Explicit,
                ExternalUrls = new ExternalUrls
                {
                    Spotify = track.ExternalUrls.Spotify!
                },
                PreviewUrl = track.PreviewUrl
            });
        }
    }
}

public class Track
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("album")]
    public Album Album { get; set; }
    
    [JsonPropertyName("artists")]
    public List<Artist>? Artists { get; set; }
    
    [JsonPropertyName("duration_ms")]
    public int DurationMs { get; set; }
    
    [JsonPropertyName("explicit")]
    public bool Explicit { get; set; }
    
    [JsonPropertyName("external_urls")]
    public ExternalUrls ExternalUrls { get; set; }
    
    [JsonPropertyName("preview_url")]
    public string? PreviewUrl { get; set; }
}

public class Artist
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("images")]
    public List<Image>? Images { get; set; }
    
    [JsonPropertyName("external_urls")]
    public ExternalUrls ExternalUrls { get; set; }
}

public class Album
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("covers")]
    public List<Image> Covers { get; set; }
    
    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; }
    
    [JsonPropertyName("external_urls")]
    public ExternalUrls ExternalUrls { get; set; }
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