using System.ComponentModel.DataAnnotations;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class ContactInformationController : ControllerBase
  {
    private readonly ILogger<ContactInformationController> _logger;
    private readonly IContactInformationService _contactInformationService;

    public ContactInformationController(ILogger<ContactInformationController> logger, IContactInformationService contactInformationService)
    {
      _logger = logger;
      _contactInformationService = contactInformationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactInformation>>> GetContactInformation()
    {
      return await _contactInformationService.GetContactInformationAsync();
    }


    [HttpPost]
    public async Task<ActionResult> AddContactInformation([FromBody] ContactInformationRequest request)
    {
      if (request == null)
      {
        _logger.LogWarning("Received null request");
        return BadRequest("Invalid request");
      }
      var validationErrors = ValidateContactInformationRequest(request);

      if (validationErrors.Any())
      {
        foreach (var error in validationErrors)
        {
          _logger.LogWarning(error.ErrorMessage);
        }
        return BadRequest(validationErrors.Select(e => e.ErrorMessage).ToList());
      }

      return await _contactInformationService.AddContactInformationAsync(request);

    }

    private List<ValidationResult> ValidateContactInformationRequest(ContactInformationRequest request)
    {
      var errors = new List<ValidationResult>();

      if (String.IsNullOrEmpty(request.Email))
      {
        errors.Add(new ValidationResult("Email cannot be null or empty", ["Email"]));
      }

      if (String.IsNullOrEmpty(request.PrimaryMobileNumber))
      {
        errors.Add(new ValidationResult("Primary mobile number cannot be null or empty", ["PrimaryMobileNumber"]));
      }

      return errors;
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateContactInformation([FromBody] ContactInformationUpdateRequest request)
    {
      if (request == null)
      {
        _logger.LogWarning("Null request received");
        return BadRequest("Invalid request");
      }

      var validationErrors = ValidateContactInformationUpdateRequest(request);
      if (validationErrors.Any())
      {

        foreach (var error in validationErrors)
        {
          _logger.LogWarning(error.ErrorMessage);
        }
        return BadRequest(validationErrors.Select(e => e.ErrorMessage).ToList());
      }
      return await _contactInformationService.UpdateContactInformationAsync(request);
    }
    private List<ValidationResult> ValidateContactInformationUpdateRequest(ContactInformationUpdateRequest request)
    {
      var errors = new List<ValidationResult>();

      if (request.Email != null && request.Email == String.Empty)
      {
        errors.Add(new ValidationResult("Email cannot be empty", ["Email"]));
      }

      if (request.PrimaryMobileNumber != null && request.PrimaryMobileNumber == String.Empty)
      {
        errors.Add(new ValidationResult("Primary mobile number cannot be empty", ["PrimaryMobileNumber"]));
      }
      return errors;
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteContactInformation()
    {
      return await _contactInformationService.DeleteContactInformationAsync();
    }
  }
}