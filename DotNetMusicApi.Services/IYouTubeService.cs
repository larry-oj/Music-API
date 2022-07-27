using DotNetMusicApi.Services.Models.YouTube;

namespace DotNetMusicApi.Services;

public interface IYouTubeService
{
    Task<List<Item>> SearchVideosAsync(string query, int limit);
}