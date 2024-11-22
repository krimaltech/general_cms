using System.ComponentModel.DataAnnotations;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class VideoController : ControllerBase
  {
    private readonly ILogger<VideoController> _logger;
    private readonly IVideoService _videoService;

    public VideoController(ILogger<VideoController> logger, IVideoService videoService)
    {
      _logger = logger; 
      _videoService = videoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Video>>> GetVideos()
    {
      return await _videoService.GetVideosAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Video>> GetSingleVideo(int id)
    {
      return await _videoService.GetSingleVideoAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult> AddVideo([FromBody] VideoRequest request)
    {
      if (request == null)
      {
        _logger.LogWarning("Received null request");
        return BadRequest("Invalid request");
      }
      var validationErrors = ValidateVideoRequest(request);

      if (validationErrors.Any())
      {
        foreach (var error in validationErrors)
        {
          _logger.LogWarning(error.ErrorMessage);
        }
        return BadRequest(validationErrors.Select(e => e.ErrorMessage).ToList());
      }
      return await _videoService.AddVideoAsync(request);
    }

    private List<ValidationResult> ValidateVideoRequest(VideoRequest request)
    {
      var errors = new List<ValidationResult>();

      if (String.IsNullOrEmpty(request.VideoUrl))
      {
        errors.Add(new ValidationResult("VideoUrl cannot be null or empty", ["VideoUrl"]));
      }

      if (String.IsNullOrEmpty(request.Alt))
      {
        errors.Add(new ValidationResult("Alt cannot be null or empty", ["Alt"]));
      }

      return errors;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteVideo(int id)
    {
      return await _videoService.DeleteVideoAsync(id);
    }
  }
}