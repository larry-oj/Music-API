using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DotNetMusicApi.Models;

public class SearchQueryRequest
{
    [Required]
    [JsonPropertyName("query")]
    public string Query { get; set; } = default!;
        
    [JsonPropertyName("limit")]
    public int Limit { get; set; } = 5;
}