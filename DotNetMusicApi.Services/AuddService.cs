using System.Text.Json;
using DotNetMusicApi.Services.Models.Audd;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DotNetMusicApi.Services;

public class AuddService : IRecognitionService
{
    private readonly ILogger<AuddService> _logger;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AuddService(ILogger<AuddService> logger, 
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
    }

    public async Task<AuddRequestResponse> RecognizeAsync(string url)
    {
        var apiUrl = _configuration.GetSection("Audd:Url").Value;
        var apiToken = _configuration.GetSection("Audd:Token").Value;
        
        using var request = new HttpRequestMessage(new HttpMethod("POST"), apiUrl);
        var multipartContent = new MultipartFormDataContent();
        multipartContent.Add(new StringContent(url), "url");
        multipartContent.Add(new StringContent(apiToken), "api_token");
        request.Content = multipartContent; 

        var response = await _httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<AuddRequestResponse>(content)!;
    }
}