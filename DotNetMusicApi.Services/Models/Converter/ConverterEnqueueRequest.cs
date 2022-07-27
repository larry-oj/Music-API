using System.Text.Json.Serialization;

namespace DotNetMusicApi.Services.Models.Converter;

public class ConverterEnqueueRequest
{
    [JsonPropertyName("url")]
    public string Url { get; set; }
    
    [JsonPropertyName("with_callback")] 
    public bool WithCallback { get; set; } = false;
    
    [JsonPropertyName("callback_url")] 
    public string? CallbackUrl { get; set; }

    public ConverterEnqueueRequest(string url)
    {
        Url = url;
    }

    public ConverterEnqueueRequest(string url, string callbackUrl)
    {
        Url = url;
        WithCallback = true;
        CallbackUrl = callbackUrl;
    }
}