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
  public class AboutController : ControllerBase
  {

    private readonly ILogger<AboutController> _logger;
    private readonly IAboutService _aboutService;

    public AboutController(ILogger<AboutController> logger, IAboutService aboutService)
    {
      _logger = logger;
      _aboutService = aboutService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<About>>> GetAbout()
    {
      return await _aboutService.GetAboutsAsync();
    }

    [HttpPost]
    public async Task<ActionResult> AddAbout([FromBody] AboutRequest request)
    {

      if (request == null)
      {
        _logger.LogWarning("Received null request");
        return BadRequest("Invalid request");
      }

      if (String.IsNullOrEmpty(request.CompanyDescription))
      {
        _logger.LogWarning("Null or empty Company description received");
        return BadRequest("company description cannot be null");
      }

      var result = await _aboutService.AddAboutAsync(request);
      return result;
    }


    [HttpPatch]
    public async Task<ActionResult> UpdateAbout([FromBody] AboutUpdateRequest request)
    {
      if (request == null)
      {
        _logger.LogWarning("Received null Request");
        return BadRequest("Invalid Request");
      }

      if (request.CompanyDescription != null && request.CompanyDescription == String.Empty)
      {
        _logger.LogWarning("Received empty company description");
        return BadRequest("Company description cannot be empty");
      }

      var result = await _aboutService.UpdateAboutAsync(request);

      return result;
    }


    public async Task<ActionResult> DeleteAbout()
    {
      return await _aboutService.DeleteAboutAsync();
    }
  }
}