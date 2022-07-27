using System.Text.Json.Serialization;

namespace DotNetMusicApi.Services.Models.Converter;

public class ConverterEnqueueResponse
{
    [JsonPropertyName("id")] 
    public string Id { get; set; }
}