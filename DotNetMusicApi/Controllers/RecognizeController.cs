using System.Text.Json;
using DotNetMusicApi.Services;
using DotNetMusicApi.Services.Models.Audd;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMusicApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RecognizeController : ControllerBase
{
    private readonly ILogger<RecognizeController> _logger;
    private readonly IRecognitionService _recognitionService;

    public RecognizeController(ILogger<RecognizeController> logger, IRecognitionService recognitionService)
    {
        _logger = logger;
        _recognitionService = recognitionService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Recognize([FromBody] RecongnizeRequest data)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _recognitionService.RecognizeAsync(data.Url);
            
            if (result.Status == "success") return Ok(result.Result);
            
            var err = JsonSerializer.Serialize<Error>(result.Error) ?? "null";
            throw new Exception(err);
        }
        catch (Exception e)
        {
            _logger.LogError("Error while recognizing: " + e.Message);
            return StatusCode(500, "Sorry, something went wrong!");
        }
    }
}