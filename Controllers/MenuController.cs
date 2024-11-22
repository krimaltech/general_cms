using System.ComponentModel.DataAnnotations;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class MenuController : ControllerBase
  {
    private readonly ILogger<MenuController> _logger;
    private readonly IMenuService _menuService;

    public MenuController(ILogger<MenuController> logger, IMenuService menuService)
    {
      _logger = logger;
      _menuService = menuService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Menu>>> GetImages()
    {
      return await _menuService.GetMenusAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Menu>> GetSingleMenu(int id)
    {
      return await _menuService.GetSingleMenuAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult> AddMenu([FromBody] MenuRequest request)
    {
      if (request == null)
      {
        _logger.LogWarning("Received null request");
        return BadRequest("Invalid request");
      }
      var validationErrors = ValiateMenuRequest(request);

      if (validationErrors.Any())
      {
        foreach (var error in validationErrors)
        {
          _logger.LogWarning(error.ErrorMessage);
        }
        return BadRequest(validationErrors.Select(e => e.ErrorMessage).ToList());
      }

      return await _menuService.AddMenuAsync(request);

    }

    private List<ValidationResult> ValiateMenuRequest(MenuRequest request)
    {
      var errors = new List<ValidationResult>();

      if (String.IsNullOrEmpty(request.Name))
      {
        errors.Add(new ValidationResult("Name cannot be null or empty", ["Name"]));
      }

      if (String.IsNullOrEmpty(request.Slug))
      {
        errors.Add(new ValidationResult("Slug cannot be null or empty", ["Slug"]));
      }

      return errors;
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateMenu(int id, [FromBody] MenuUpdateRequest request)
    {

      if (request == null)
      {
        _logger.LogWarning("Received null request");
        return BadRequest("Invalid request");
      }

      var validationErrors = ValidateMenuUpdateRequest(request);
      if (validationErrors.Any())
      {
        foreach (var error in validationErrors)
        {
          _logger.LogWarning(error.ErrorMessage);
        }
        return BadRequest(validationErrors.Select(e => e.ErrorMessage).ToList());
      }

      return await _menuService.UpdateMenuAsync(id, request);
    }

    private List<ValidationResult> ValidateMenuUpdateRequest(MenuUpdateRequest request)
    {

      var errors = new List<ValidationResult>();
      if (request.Name != null && request.Name == String.Empty) errors.Add(new ValidationResult("Menu name should not be empty", ["Name"]));
      if (request.Slug != null && request.Slug == String.Empty) errors.Add(new ValidationResult("Slug should not be empty", ["Slug"]));

      return errors;
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMenu(int id)
    {
      return await _menuService.DeleteMenuAsync(id);
    }
  }
}