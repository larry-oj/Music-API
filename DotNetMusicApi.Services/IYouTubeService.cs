using DotNetMusicApi.Services.Models.YouTube;

namespace DotNetMusicApi.Services;

public interface IYouTubeService
{
    Task<List<Item>> SearchVideos(string query, int limit);
}