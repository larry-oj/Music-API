using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DotNetMusicApi.Services.Models.Audd;

public class RecongnizeRequest
{
    [JsonPropertyName("url")]
    [Required]
    public string Url { get; set; }
}