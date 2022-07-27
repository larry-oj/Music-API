using DotNetMusicApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMusicApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly ISpotifyService _spotifyService;
    private readonly IYouTubeService _youTubeService;
    private readonly IConfiguration _configuration;

    public SearchController(ILogger<SearchController> logger, 
        ISpotifyService spotifyService, 
        IYouTubeService youTubeService, 
        IConfiguration configuration)
    {
        _logger = logger;
        _spotifyService = spotifyService;
        _youTubeService = youTubeService;
        _configuration = configuration;
    }
    
    // ...
}