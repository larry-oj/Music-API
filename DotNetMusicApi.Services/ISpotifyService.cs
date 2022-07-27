using DotNetMusicApi.Services.Models.Spotify;

namespace DotNetMusicApi.Services;

public interface ISpotifyService
{
    Task<List<Track>> SearchTracks(string query, int limit);
}