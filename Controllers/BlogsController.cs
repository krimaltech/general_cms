using System.ComponentModel.DataAnnotations;
using Backend.Data;
using Backend.Dtos;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{


  [ApiController]
  [Route("/api/[controller]")]
  public class BlogsController : ControllerBase
  {
    ProjectDbContext _context;
    ILogger<BlogsController> _logger;

    IBlogService _blogService;

    public BlogsController(ProjectDbContext context, ILogger<BlogsController> logger, IBlogService blogService)
    {
      _context = context;
      _logger = logger;
      _blogService = blogService;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<BlogDto>>> GetBlogs()
    {
      return await _blogService.GetBlogsAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BlogDto>> GetSingleBlog(int id) {
      return await _blogService.GetSingleBlogAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult> AddBlogs([FromBody] BlogRequest request)
    {
      if (request == null)
      {
        _logger.LogWarning("received null request");
        return BadRequest("Invalid Request");
      }

      var validationErrors = ValidateBlogRequest(request);
      if (validationErrors.Any())
      {
        foreach (var error in validationErrors)
        {
          _logger.LogWarning(error.ErrorMessage);
        }
        return BadRequest(validationErrors.Select(e => e.ErrorMessage).ToList());
      }

      var result = await _blogService.AddBlogAsync(request);
      return result;
    }

    /* This is a helper method for Blog request validation*/
    private List<ValidationResult> ValidateBlogRequest(BlogRequest request)
    {
      var errors = new List<ValidationResult>();

      if (string.IsNullOrEmpty(request.Title))
      {
        errors.Add(new ValidationResult("Title cannot be null or empty.", ["Title"]));
      }
      if (string.IsNullOrEmpty(request.Description))
      {
        errors.Add(new ValidationResult("Description cannot be null or empty.", ["Description"]));
      }
      if (string.IsNullOrEmpty(request.Slug))
      {
        errors.Add(new ValidationResult("Slug cannot be null.", ["Slug"]));
      }
      if (request.BlogSections != null && request.BlogSections.Any())
      {
        foreach (var section in request.BlogSections)
        {
          if (string.IsNullOrEmpty(section.Title))
          {
            errors.Add(new ValidationResult("Section Title cannot be null or empty", ["Title"]));
          }
          if (string.IsNullOrEmpty(section.Description))
          {
            errors.Add(new ValidationResult("Section Description cannot be null or empty", ["Description"]));
          }
          if (string.IsNullOrEmpty(section.Slug))
          {
            errors.Add(new ValidationResult("Section Slug cannot be null or empty", ["Slug"]));
          }
        }
      }
      return errors;
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateBlog(int id, [FromBody] BlogUpdateRequest request)
    {
      if (request == null)
      {
        _logger.LogWarning("No changes found in the request.");
        return BadRequest("Nothing to change");
      }

      var validationErrors = ValidateBlogUpdateRequest(request);
      if (validationErrors.Any())
      {
        foreach (var error in validationErrors)
        {
          _logger.LogWarning(error.ErrorMessage);
        }
        return BadRequest(validationErrors.Select(e => e.ErrorMessage).ToList());
      }

      var result = await _blogService.UpdateBlogAsync(id, request);

      return result;
    }

    private List<ValidationResult> ValidateBlogUpdateRequest(BlogUpdateRequest request)
    {
      var errors = new List<ValidationResult>();

      if (request.Title != null && request.Title == String.Empty)
      {
        errors.Add(new ValidationResult("Blog title cannot be empty", ["Title"]));
      }
      if (request.Description != null && request.Description == String.Empty)
      {
        errors.Add(new ValidationResult("Blog Description cannot be empty", ["Description"]));
      }
      if (request.Slug != null && request.Slug == String.Empty)
      {
        errors.Add(new ValidationResult("Slug cannot be empty", ["Slug"]));
      }
      if (request.BlogSections != null && request.BlogSections.Any())
      {
        foreach (var section in request.BlogSections)
        {
          if (section.Title != null && section.Title == String.Empty)
          {
            errors.Add(new ValidationResult("Section Title cannot be empty", ["Title"]));
          }
          if (section.Description != null && section.Description == String.Empty)
          {
            errors.Add(new ValidationResult("Section Description cannot be empty", ["Description"]));
          }
          if (section.Slug != null && section.Slug == String.Empty)
          {
            errors.Add(new ValidationResult("Slug cannot be empty", ["Slug"]));
          }
        }
      }
      return errors;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBLog(int id)
    {
      return await _blogService.DeleteBlogAsync(id);
    }

    [HttpDelete("blogsection/{id}")]
    public async Task<ActionResult> DeleteBlogSection(int id) {
      return await _blogService.DeleteBlogSectionAsync(id);
    }

  }
}