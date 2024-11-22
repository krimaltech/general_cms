using System.ComponentModel.DataAnnotations;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class ServiceController : ControllerBase
  {
    private readonly ILogger<ServiceController> _logger;
    private readonly IServiceService _serviceService;

    public ServiceController(IServiceService serviceService, ILogger<ServiceController> logger)
    {

      _logger = logger;
      _serviceService = serviceService;
    }

    [HttpGet]
    public async Task<ActionResult> GetServices()
    {
      return await _serviceService.GetServicesAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingleBlog(int id)
    {
      return await _serviceService.GetSingleServiceAsync(id);
    }


    [HttpPost]
    public async Task<ActionResult> AddService([FromBody] ServiceRequest request)
    {

      if (request == null)
      {
        _logger.LogWarning("received null request");
        return BadRequest("Invalid Request");
      }

      var validationErrors = VadidateServiceRequest(request);
      if (validationErrors.Any())
      {
        foreach (var error in validationErrors)
        {
          _logger.LogWarning(error.ErrorMessage);
        }
        return BadRequest(validationErrors.Select(e => e.ErrorMessage).ToList());
      }

      var result = await _serviceService.AddServiceAsync(request);
      return result;
    }

    private List<ValidationResult> VadidateServiceRequest(ServiceRequest request)
    {
      var errors = new List<ValidationResult>();

      if (string.IsNullOrEmpty(request.Title))
      {
        errors.Add(new ValidationResult("Title cannot be null or empty.", ["Title"]));
      }

      if (request.Methodologies != null && request.Methodologies.Any())
      {
        foreach (var methodology in request.Methodologies)
        {
          if (string.IsNullOrEmpty(methodology.Title))
          {
            errors.Add(new ValidationResult("Methodology Title cannot be null or empty", ["Title"]));
          }
        }
      }
      if (request.Technologies != null && request.Technologies.Any())
      {
        foreach (var technology in request.Technologies)
        {
          if (string.IsNullOrEmpty(technology.Title))
          {
            errors.Add(new ValidationResult("Technology Title cannot be null or empty", ["Title"]));
          }
        }
      }
      return errors;
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateService(int id, [FromBody] ServiceUpdateRequest request)
    {
      if (request == null)
      {
        _logger.LogWarning("No changes found in the request.");
        return BadRequest("Nothing to change");
      }

      var validationErrors = ValidateServiceUpdateRequest(request);
      if (validationErrors.Any())
      {
        foreach (var error in validationErrors)
        {
          _logger.LogWarning(error.ErrorMessage);
        }
        return BadRequest(validationErrors.Select(e => e.ErrorMessage).ToList());
      }

      var result = await _serviceService.UpdateServiceAsync(id, request);

      return result;
    }

    private List<ValidationResult> ValidateServiceUpdateRequest(ServiceUpdateRequest request)
    {
      var errors = new List<ValidationResult>();

      if (request.Title != null && request.Title == String.Empty)
      {
        errors.Add(new ValidationResult("Blog title cannot be empty", ["Title"]));
      }

      if (request.Methodologies != null && request.Methodologies.Any())
      {
        foreach (var methodology in request.Methodologies)
        {
          if (methodology.Title != null && methodology.Title == String.Empty)
          {
            errors.Add(new ValidationResult("Methodology Title cannot be empty", ["Title"]));
          }
        }
      }
      if (request.Technologies != null && request.Technologies.Any())
      {
        foreach (var technology in request.Technologies)
        {
          if (technology.Title != null && technology.Title == String.Empty)
          {
            errors.Add(new ValidationResult("Technology Title cannot be empty", ["Title"]));
          }
        }
      }
      return errors;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
      return await _serviceService.DeleteServiceAsync(id);
    }

    [HttpDelete("methodology/{id}")]
    public async Task<ActionResult> DeleteMethodology(int id)
    {
      return await _serviceService.DeleteMethodologyAsync(id);
    }
    [HttpDelete("technology/{id}")]
    public async Task<ActionResult> DeleteTechnology(int id)
    {
      return await _serviceService.DeleteTechnologyAsync(id);
    }

  }
}