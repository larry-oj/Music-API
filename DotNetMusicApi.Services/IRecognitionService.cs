using DotNetMusicApi.Services.Models.Audd;

namespace DotNetMusicApi.Services;

public interface IRecognitionService
{
    Task<AuddRequestResponse> RecognizeAsync(string url);
}