using System.ComponentModel.DataAnnotations;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class ImageController : ControllerBase
  {
    private readonly ILogger<ImageController> _logger;
    private readonly IImageService _imageService;

    public ImageController(ILogger<ImageController> logger, IImageService imageService)
    {
      _logger = logger;
      _imageService = imageService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Image>>> GetImages()
    {
      return await _imageService.GetImagesAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Image>> GetSingleImage(int id)
    {
      return await _imageService.GetSingleImageAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult> AddContactInformation([FromBody] ImageRequest request)
    {
      if (request == null)
      {
        _logger.LogWarning("Received null request");
        return BadRequest("Invalid request");
      }
      var validationErrors = ValidateImageRequest(request);

      if (validationErrors.Any())
      {
        foreach (var error in validationErrors)
        {
          _logger.LogWarning(error.ErrorMessage);
        }
        return BadRequest(validationErrors.Select(e => e.ErrorMessage).ToList());
      }

      return await _imageService.AddImageAsync(request);

    }

    private List<ValidationResult> ValidateImageRequest(ImageRequest request)
    {
      var errors = new List<ValidationResult>();

      if (String.IsNullOrEmpty(request.ImageUrl))
      {
        errors.Add(new ValidationResult("ImageUrl cannot be null or empty", ["ImageUrl"]));
      }

      if (String.IsNullOrEmpty(request.Alt))
      {
        errors.Add(new ValidationResult("Alt cannot be null or empty", ["Alt"]));
      }

      return errors;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteContactInformation(int id)
    {
      return await _imageService.DeleteImageAsync(id);
    }
  }
}