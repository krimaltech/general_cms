using System.ComponentModel.DataAnnotations;
using Backend.Data;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class MainInformationController : ControllerBase
  {

    private readonly ILogger<MainInformationController> _logger;
    private readonly IMainInformationService _mainInformationService;

    public MainInformationController(ILogger<MainInformationController> logger, IMainInformationService mainInformationService)
    {
      _logger = logger;
      _mainInformationService = mainInformationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MainInformation>>> GetMainInformation()
    {
      return await _mainInformationService.GetMainInformationAsync();
    }

    [HttpPost]
    public async Task<ActionResult> AddMainInformation([FromBody] MainInformationRequest request)
    {

      if (request == null)
      {
        _logger.LogWarning("Received null request");
        return BadRequest("Invalid request");
      }
      var validationErrors = ValidateMainInformationRequest(request);

      if (validationErrors.Any())
      {
        foreach (var error in validationErrors)
        {
          _logger.LogWarning(error.ErrorMessage);
        }
        return BadRequest(validationErrors.Select(e => e.ErrorMessage).ToList());
      }

      return await _mainInformationService.AddMainInformtionAsync(request);

    }

    private List<ValidationResult> ValidateMainInformationRequest(MainInformationRequest request)
    {
      var errors = new List<ValidationResult>();

      if (String.IsNullOrEmpty(request.ProjectName))
      {
        errors.Add(new ValidationResult("Project Name should not be null or empty", ["ProjectName"]));
      }

      if (String.IsNullOrEmpty(request.LogoUrl))
      {
        errors.Add(new ValidationResult("LogoUrl should not be null or empty", ["LogoUrl"]));
      }

      if (String.IsNullOrEmpty(request.FaviconUrl))
      {
        errors.Add(new ValidationResult("FaviconUrl should not be null or empty", ["FaviconUrl"]));
      }

      return errors;
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateMainInformation([FromBody] MainInformationUpdateRequest request)
    {
      if (request == null)
      {
        _logger.LogWarning("Invalid Request");
        return BadRequest("Invalid Request");
      }
      var validationErrors = ValidateMainInformationUpdateRequest(request);
      if (validationErrors.Any())
      {
        foreach (var error in validationErrors)
        {
          _logger.LogWarning(error.ErrorMessage);
        }
        return BadRequest(validationErrors.Select(x => x.ErrorMessage).ToList());
      }

      var result = await _mainInformationService.UpdateMainInformationAsync(request);

      return result;
    }

    private List<ValidationResult> ValidateMainInformationUpdateRequest(MainInformationUpdateRequest request)
    {
      var errors = new List<ValidationResult>();

      if (request.ProjectName != null && request.ProjectName == String.Empty)
      {
        errors.Add(new ValidationResult("Project Name should not be empty."));
      }

      if (request.LogoUrl != null && request.LogoUrl == String.Empty)
      {
        errors.Add(new ValidationResult("LogoUrl should not be empty."));
      }

      if (request.FaviconUrl != null && request.FaviconUrl == String.Empty)
      {
        errors.Add(new ValidationResult("FaviconUrl should not be empty."));
      }
      return errors;
    }
    public async Task<ActionResult> DeleteMainInformation()
    {
      return await _mainInformationService.DeleteMainInformationAsync();
    }
  }
}