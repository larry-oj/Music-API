using System.Data;
using DotNetMusicApi.Models;
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
    
    [HttpGet]
    [Route("spotify")]
    public async Task<IActionResult> SearchSpotify ([FromQuery] SearchQueryRequest parameters)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (string.IsNullOrEmpty(parameters.Query))
        {
            _logger.LogError("Query is null");
            return BadRequest("Query is null");
        }

        if (parameters.Limit is <= 0 or > 15)
        {
            _logger.LogError("Invalid limit value. Must be higher than 0 and lower then 16");
            return BadRequest("Invalid limit value. Must be higher than 0 and lower then 16");
        }

        try
        {
            var searchResult = await _spotifyService.SearchTracksAsync(parameters.Query, parameters.Limit);
            return Ok(new SpotifySearchResponse(searchResult));
        }
        catch (Exception ex)
        {
            var errMsg = ex is DataException ? "Sorry! Spotify service is currently unavailable" : "Sorry! Something went wrong";
            _logger.LogError(ex, errMsg);
            return StatusCode(500, errMsg);
        }
    }

    [HttpGet]
    [Route("youtube")]
    public async Task<IActionResult> SearchYoutube([FromQuery] SearchQueryRequest parameters)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (string.IsNullOrEmpty(parameters.Query))
        {
            _logger.LogError("Query is null");
            return BadRequest("Query is null");
        }

        if (parameters.Limit is <= 0 or > 15)
        {
            _logger.LogError("Invalid limit value. Must be higher than 0 and lower then 16");
            return BadRequest("Invalid limit value. Must be higher than 0 and lower then 16");
        }
        
        try
        {
            var searchResult = await _youTubeService.SearchVideosAsync(parameters.Query, parameters.Limit);
            return Ok(new YouTubeSearchResponse(searchResult));
        }
        catch (Exception ex)
        {
            var errMsg = ex is DataException ? "Sorry! YouTube service is currently unavailable" : "Sorry! Something went wrong";
            _logger.LogError(ex, errMsg);
            return StatusCode(500, errMsg);
        }
    }
}