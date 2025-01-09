using Backend.Data;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
  public class AboutServices : IAboutService
  {
    private readonly ProjectDbContext _context;
    private readonly ILogger<AboutServices> _logger;

    public AboutServices(ProjectDbContext context, ILogger<AboutServices> logger)
    {
      _logger = logger;
      _context = context;
    }

    public async Task<ActionResult<IEnumerable<About>>> GetAboutsAsync()
    {
      var about = await _context.About.ToListAsync();

      if (about == null)
      {
        _logger.LogWarning("About not found");
        return new BadRequestObjectResult("About not found");
      }
      return new OkObjectResult(about);
    }

    public async Task<ActionResult> AddAboutAsync(AboutRequest request)
    {
      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        About? about = new About
        {
          ID = 1,
          CompanyDescription = request.CompanyDescription,
          Objectives = request.Objectives,
          Vision = request.Vision,
          Mission = request.Mission,
          Features = request.Features,
          ImageUrl = request.ImageUrl
        };

        await _context.About.AddAsync(about);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("About successfully added");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to add About");
        return new StatusCodeResult(500);
      }
    }
    public async Task<ActionResult> UpdateAboutAsync(AboutUpdateRequest request)
    {

      About? about = await _context.About.FirstOrDefaultAsync();

      if (about == null)
      {
        _logger.LogWarning("About not found");
        return new BadRequestObjectResult("About not found");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        if (request.CompanyDescription != null) about.CompanyDescription = request.CompanyDescription;
        if (request.Objectives != null) about.Objectives = request.Objectives;
        if (request.Vision != null) about.Vision = request.Vision;
        if (request.Mission != null) about.Mission = request.Mission;
        if (request.Features != null) about.Features = request.Features;
        if (request.ImageUrl != null) about.ImageUrl = request.ImageUrl;

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("About updated successfully");

      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to add About");
        return new StatusCodeResult(500);
      }
    }

    public async Task<ActionResult> DeleteAboutAsync(int id)
    {
      About? about = await _context.About.FirstOrDefaultAsync();

      if (about == null)
      {
        _logger.LogWarning("About not found");
        return new BadRequestObjectResult("About not found");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        _context.About.Remove(about);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("About Successfully deleted");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to delete About");
        return new StatusCodeResult(500);
      }
    }

  }
}