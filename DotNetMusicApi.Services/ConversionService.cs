using System.Net.Http.Json;
using System.Text.Json;
using DotNetMusicApi.Services.Models.Converter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DotNetMusicApi.Services;

public class ConversionService : IConversionService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ConversionService> _logger;
    private readonly HttpClient _httpClient;

    public ConversionService(IConfiguration configuration, 
        ILogger<ConversionService> logger, 
        IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<string> EnqueueWebHookedAsync(string url, string callbackUrl)
    {
        if (string.IsNullOrEmpty(url))
        {
            _logger.LogError("Url is null");
            throw new ArgumentNullException("Url is null");
        }
        if (string.IsNullOrEmpty(callbackUrl))
        {
            _logger.LogError("СallbackUrl is null");
            throw new ArgumentNullException("СallbackUrl is null");
        }

        var uri = new Uri(_configuration.GetSection("Converter:EnqueueUrl").Value);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = uri,
            Content = JsonContent.Create(new ConverterEnqueueRequest(url, callbackUrl))
        };
        
        var response = await _httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) 
            throw new Exception(content);

        var data = JsonSerializer.Deserialize<ConverterEnqueueResponse>(content) ?? throw new InvalidOperationException();
        return data.Id;
    }

    public async Task<string> EnqueueAsync(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            _logger.LogError("Url is null");
            throw new ArgumentNullException("Url is null");
        }

        var uri = new Uri(_configuration.GetSection("Converter:EnqueueUrl").Value);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = uri,
            Content = JsonContent.Create(new ConverterEnqueueRequest(url))
        };
        
        var response = await _httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) 
            throw new Exception(content);

        var data = JsonSerializer.Deserialize<ConverterEnqueueResponse>(content) ?? throw new InvalidOperationException();
        return data.Id;
    }

    public async Task<ConverterEnqueueResponse> EnqueueAsync(ConverterEnqueueRequest data)
    {
        if (data is null) 
        {
            _logger.LogError("Data is null");
            throw new ArgumentNullException("Data error");
        }
        if (data.WithCallback && string.IsNullOrEmpty(data.CallbackUrl))
        {
            _logger.LogError("СallbackUrl is null");
            throw new ArgumentNullException("СallbackUrl is null");
        }
        
        var uri = new Uri(_configuration.GetSection("Converter:EnqueueUrl").Value);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = uri,
            Content = JsonContent.Create(data)
        };
        
        var response = await _httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) 
            throw new Exception(content);
        
        return JsonSerializer.Deserialize<ConverterEnqueueResponse>(content) ?? throw new InvalidOperationException();
    }

    public async Task<ConverterStatusResponse> GetStatusAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogError("Id is null");
            throw new ArgumentNullException("Id is null");
        }

        var uri = new Uri(_configuration.GetSection("Converter:StatusUrl").Value.Replace(@"{id}", id));
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = uri
        };
        
        var response = await _httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) 
            throw new Exception(content);
        
        return JsonSerializer.Deserialize<ConverterStatusResponse>(content) ?? throw new InvalidOperationException();
    }

    public async Task<ConverterFile> GetFileAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogError("Id is null");
            throw new ArgumentNullException("Id is null");
        }

        var uri = new Uri(_configuration.GetSection("Converter:FileUrl").Value.Replace(@"{id}", id));
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = uri
        };
        
        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) 
            throw new ApiException(await response.Content.ReadAsStringAsync());
        
        var fileStream = await response.Content.ReadAsStreamAsync();
        var contentType = response.Content.Headers.ContentType?.MediaType;
        var fileName = response.Content.Headers.ContentDisposition?.FileName;

        if (contentType is null || fileName is null)
        {
            _logger.LogError("Error retrieving file");
            throw new ApiException("Error retrieving file");
        }

        return new ConverterFile(fileStream, contentType, fileName);
    }

    public async Task<ConverterFile> GetFileBlockingAsync(string url)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}

public class ApiException : Exception
{
    public ApiException(string message)
        : base(message)
    {
    }
}