using DotNetMusicApi.Services;
using DotNetMusicApi.Services.Models.Converter;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMusicApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ConverterController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly IConversionService _conversionService;
    private readonly IConfiguration _configuration;

    public ConverterController(ILogger<SearchController> logger, 
        IConversionService conversionService, 
        IConfiguration configuration)
    {
        _logger = logger;
        _conversionService = conversionService;
        _configuration = configuration;
    }
    
    [HttpPost]
    [Route("videos")]
    public async Task<IActionResult> Enqueue([FromBody] ConverterEnqueueRequest data)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            return Ok(await _conversionService.EnqueueAsync(data));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Returning error");
            return ex is ArgumentNullException ? BadRequest(ex.Message) : StatusCode(500, "Sorry! Something went wrong");
        }
    }

    [HttpGet]
    [Route("videos/{id}/status")]
    public async Task<IActionResult> GetStatus(string id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            return Ok(await _conversionService.GetStatusAsync(id));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Returning error");
            return ex is ArgumentNullException ? BadRequest(ex.Message) : StatusCode(500, "Sorry! Something went wrong");
        }
    }
    
    [HttpGet]
    [Route("videos/{id}")]
    public async Task<IActionResult> GetVideo(string id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            var file = await _conversionService.GetFileAsync(id);
            return File(file.FileStream, file.ContentType, file.FileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Returning error");
            if (ex is ArgumentNullException or ApiException)
            {
                return BadRequest(ex.Message);
            }
            return StatusCode(500, "Sorry! Something went wrong");
        }
    }
}