using DotNetMusicApi.Services.Models.Spotify;

namespace DotNetMusicApi.Services;

public interface ISpotifyService : IDisposable
{
    Task<List<Track>> SearchTracksAsync(string query, int limit);
    Task<string> GetTrackName(string url);
}