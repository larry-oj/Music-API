using System.Text.Json.Serialization;

namespace DotNetMusicApi.Services.Models.Spotify;

public class SpotifySearchResponse
{
    [JsonPropertyName("tracks")]
    public Item Items { get; set; }
}

public class Item
{
    [JsonPropertyName("items")]
    public List<Track> Tracks { get; set; }
}

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public class Album
{
    [JsonPropertyName("album_type")]
    public string AlbumType { get; set; }

    [JsonPropertyName("total_tracks")]
    public int TotalTracks { get; set; }

    [JsonPropertyName("available_markets")]
    public List<string> AvailableMarkets { get; set; }

    [JsonPropertyName("external_urls")]
    public ExternalUrls ExternalUrls { get; set; }

    [JsonPropertyName("href")]
    public string Href { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("images")]
    public List<Image> Images { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; }

    [JsonPropertyName("release_date_precision")]
    public string ReleaseDatePrecision { get; set; }

    [JsonPropertyName("restrictions")]
    public Restrictions Restrictions { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("uri")]
    public string Uri { get; set; }

    [JsonPropertyName("album_group")]
    public string AlbumGroup { get; set; }

    [JsonPropertyName("artists")]
    public List<Artist> Artists { get; set; }
}

public class Artist
{
    [JsonPropertyName("external_urls")]
    public ExternalUrls ExternalUrls { get; set; }

    [JsonPropertyName("href")]
    public string Href { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("uri")]
    public string Uri { get; set; }

    [JsonPropertyName("followers")]
    public Followers Followers { get; set; }

    [JsonPropertyName("genres")]
    public List<string> Genres { get; set; }

    [JsonPropertyName("images")]
    public List<Image> Images { get; set; }

    [JsonPropertyName("popularity")]
    public int Popularity { get; set; }
}

public class ExternalIds
{
    [JsonPropertyName("isrc")]
    public string Isrc { get; set; }

    [JsonPropertyName("ean")]
    public string Ean { get; set; }

    [JsonPropertyName("upc")]
    public string Upc { get; set; }
}

public class ExternalUrls
{
    [JsonPropertyName("spotify")]
    public string Spotify { get; set; }
}

public class Followers
{
    [JsonPropertyName("href")]
    public string Href { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }
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

public class LinkedFrom
{
    [JsonPropertyName("album")]
    public Album Album { get; set; }

    [JsonPropertyName("artists")]
    public List<Artist> Artists { get; set; }

    [JsonPropertyName("available_markets")]
    public List<string> AvailableMarkets { get; set; }

    [JsonPropertyName("disc_number")]
    public int DiscNumber { get; set; }

    [JsonPropertyName("duration_ms")]
    public int DurationMs { get; set; }

    [JsonPropertyName("explicit")]
    public bool Explicit { get; set; }

    [JsonPropertyName("external_ids")]
    public ExternalIds ExternalIds { get; set; }

    [JsonPropertyName("external_urls")]
    public ExternalUrls ExternalUrls { get; set; }

    [JsonPropertyName("href")]
    public string Href { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("is_playable")]
    public bool IsPlayable { get; set; }

    [JsonPropertyName("linked_from")]
    public LinkedFrom Linked_From { get; set; }

    [JsonPropertyName("restrictions")]
    public Restrictions Restrictions { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("popularity")]
    public int Popularity { get; set; }

    [JsonPropertyName("preview_url")]
    public string PreviewUrl { get; set; }

    [JsonPropertyName("track_number")]
    public int TrackNumber { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("uri")]
    public string Uri { get; set; }

    [JsonPropertyName("is_local")]
    public bool IsLocal { get; set; }
}

public class Restrictions
{
    [JsonPropertyName("reason")]
    public string Reason { get; set; }
}

public class Track
{
    [JsonPropertyName("album")]
    public Album Album { get; set; }

    [JsonPropertyName("artists")]
    public List<Artist> Artists { get; set; }

    [JsonPropertyName("available_markets")]
    public List<string> AvailableMarkets { get; set; }

    [JsonPropertyName("disc_number")]
    public int DiscNumber { get; set; }

    [JsonPropertyName("duration_ms")]
    public int DurationMs { get; set; }

    [JsonPropertyName("explicit")]
    public bool Explicit { get; set; }

    [JsonPropertyName("external_ids")]
    public ExternalIds ExternalIds { get; set; }

    [JsonPropertyName("external_urls")]
    public ExternalUrls ExternalUrls { get; set; }

    [JsonPropertyName("href")]
    public string Href { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("is_playable")]
    public bool IsPlayable { get; set; }

    [JsonPropertyName("linked_from")]
    public LinkedFrom LinkedFrom { get; set; }

    [JsonPropertyName("restrictions")]
    public Restrictions Restrictions { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("popularity")]
    public int Popularity { get; set; }

    [JsonPropertyName("preview_url")]
    public string PreviewUrl { get; set; }

    [JsonPropertyName("track_number")]
    public int TrackNumber { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("uri")]
    public string Uri { get; set; }

    [JsonPropertyName("is_local")]
    public bool IsLocal { get; set; }
}

