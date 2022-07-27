using System.Text.Json.Serialization;

namespace DotNetMusicApi.Services.Models.Converter;

public class ConverterStatusResponse
{
    [JsonPropertyName("id")] 
    public string Id { get; set; }
    
    [JsonPropertyName("is_failed")] 
    public bool IsFailed { get; set; }
    
    [JsonPropertyName("is_finished")] 
    public bool IsFinished { get; set; }
}