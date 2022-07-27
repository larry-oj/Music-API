using DotNetMusicApi.Services.Models.Spotify;

namespace DotNetMusicApi.Services;

public interface ISpotifyService
{
    Task<List<Track>> SearchTracksAsync(string query, int limit);
}