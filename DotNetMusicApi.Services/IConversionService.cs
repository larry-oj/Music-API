using DotNetMusicApi.Services.Models.Converter;

namespace DotNetMusicApi.Services;

public interface IConversionService : IDisposable
{
    Task<string> EnqueueWebHookedAsync(string url, string callbackUrl);
    Task<string> EnqueueAsync(string url);
    Task<ConverterStatusResponse> GetStatusAsync(string id);
    Task<ConverterFile> GetFileAsync(string id);
    Task<ConverterFile> GetFileBlockingAsync(string url);
}