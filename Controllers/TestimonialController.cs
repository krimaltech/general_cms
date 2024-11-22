using System.ComponentModel.DataAnnotations;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class TestimonialController : ControllerBase
  {

    private readonly ILogger<TestimonialController> _logger;
    private readonly ITestimonialService _testimonialService;

    public TestimonialController(ILogger<TestimonialController> logger, ITestimonialService testimonialService)
    {
      _logger = logger;
      _testimonialService = testimonialService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Testimonial>>> GetTestimonials()
    {
      return await _testimonialService.GetTestimonialsAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Testimonial>> GetSingleTestimonial(int id)
    {
      return await _testimonialService.GetSingleTestimonial(id);
    }

    [HttpPost]
    public async Task<ActionResult> AddTestimonial([FromBody] TestimonialRequest request)
    {
      if (request == null)
      {
        _logger.LogWarning("Received null request");
        return new BadRequestObjectResult("Invalid request");
      }

      var validationErrors = ValidateTestimonialRequest(request);
      if (validationErrors.Any())
      {
        foreach (var error in validationErrors)
        {
          _logger.LogWarning(error.ErrorMessage);
        }
        return BadRequest(validationErrors.Select(x => x.ErrorMessage).ToList());
      }

      return await _testimonialService.AddTestimonialAsync(request);
    }

    private List<ValidationResult> ValidateTestimonialRequest(TestimonialRequest request)
    {
      var errors = new List<ValidationResult>();

      if (String.IsNullOrEmpty(request.Name))
      {
        errors.Add(new ValidationResult("Name cannot be null or empty", ["Name"]));
      }

      if (String.IsNullOrEmpty(request.Email))
      {
        errors.Add(new ValidationResult("Email cannot be null or empty", ["Email"]));
      }

      if (String.IsNullOrEmpty(request.Message))
      {
        errors.Add(new ValidationResult("Message cannot be null or empty", ["Message"]));
      }

      if (String.IsNullOrEmpty(request.ImageUrl))
      {
        errors.Add(new ValidationResult("ImageUrl cannot be null or empty", ["ImageUrl"]));
      }
      return errors;
    }


    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateTestimonial(int id, [FromBody] TestimonialUpdateRequest request)
    {
      if (request == null)
      {
        _logger.LogWarning("Received null request");
        return new BadRequestObjectResult("Invalid request");
      }
      var validationErrors = ValidateTestimonialUpdateRequest(request);
      if (validationErrors.Any())
      {
        foreach (var error in validationErrors)
        {
          _logger.LogWarning(error.ErrorMessage);
        }
        return BadRequest(validationErrors.Select(x => x.ErrorMessage).ToList());
      }

      return await _testimonialService.UpdateTestimonialAsync(id, request);
    }

    private List<ValidationResult> ValidateTestimonialUpdateRequest(TestimonialUpdateRequest request)
    {
      var errors = new List<ValidationResult>();

      if (request.Name != null && request.Name == String.Empty)
      {
        errors.Add(new ValidationResult("Name cannot be empty", ["Name"]));
      }
      if (request.Email != null && request.Email == String.Empty)
      {
        errors.Add(new ValidationResult("Email cannot be empty", ["Email"]));
      }
      if (request.Message != null && request.Message == String.Empty)
      {
        errors.Add(new ValidationResult("Message cannot be empty", ["Message"]));
      }
      if (request.ImageUrl != null && request.ImageUrl == String.Empty)
      {
        errors.Add(new ValidationResult("ImageUrl cannot be empty", ["ImageUrl"]));
      }


      return errors;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTestimonial(int id)
    {
      return await _testimonialService.DeleteTestimonialAsync(id);
    }
  }
}