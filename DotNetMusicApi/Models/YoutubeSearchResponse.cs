using System.Text.Json.Serialization;

namespace DotNetMusicApi.Models;

public class YouTubeSearchResponse
{
    [JsonPropertyName("items")]
    public List<Item> Items { get; set; }

    public YouTubeSearchResponse(List<Services.Models.YouTube.Item> obj)
    {
        Items = new List<Item>();
        foreach (var item in obj)
        {
            Items.Add(new Item
            {
                VideoId = item.Id.VideoId,
                Snippet = new Snippet
                {
                    PublishedAt = item.Snippet.PublishedAt,
                    ChannelId = item.Snippet.ChannelId,
                    Title = item.Snippet.Title,
                    Thumbnails = new Thumbnails
                    {
                        Default = new Size
                        {
                            Url = item.Snippet.Thumbnails.Default.Url,
                            Width = item.Snippet.Thumbnails.Default.Width,
                            Height = item.Snippet.Thumbnails.Default.Height
                        },
                        Medium = new Size
                        {
                            Url = item.Snippet.Thumbnails.Medium.Url,
                            Width = item.Snippet.Thumbnails.Medium.Width,
                            Height = item.Snippet.Thumbnails.Medium.Height
                        },
                        High = new Size
                        {
                            Url = item.Snippet.Thumbnails.High.Url,
                            Width = item.Snippet.Thumbnails.High.Width,
                            Height = item.Snippet.Thumbnails.High.Height
                        }
                    },
                    ChannelTitle = item.Snippet.ChannelTitle,
                    PublishTime = item.Snippet.PublishTime
                }
            });
        }
    }
}

public class Item
{
    [JsonPropertyName("videoId")]
    public string VideoId { get; set; }

    [JsonPropertyName("snippet")]
    public Snippet Snippet { get; set; }
}

public class Snippet
{
    [JsonPropertyName("publishedAt")]
    public DateTime PublishedAt { get; set; }

    [JsonPropertyName("channelId")]
    public string ChannelId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("thumbnails")]
    public Thumbnails Thumbnails { get; set; }

    [JsonPropertyName("channelTitle")]
    public string ChannelTitle { get; set; }

    [JsonPropertyName("publishTime")]
    public DateTime PublishTime { get; set; }
}

public class Thumbnails
{
    [JsonPropertyName("default")]
    public Size Default { get; set; }

    [JsonPropertyName("medium")]
    public Size Medium { get; set; }

    [JsonPropertyName("high")]
    public Size High { get; set; }
}

public class Size
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }
}