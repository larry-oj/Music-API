using DotNetMusicApi.Services.Models.YouTube;

namespace DotNetMusicApi.Services;

public interface IYouTubeService : IDisposable
{
    Task<List<Item>> SearchVideosAsync(string query, int limit);
}