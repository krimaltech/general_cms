using Backend.Data;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
  public class MainInformationService : IMainInformationService
  {
    private readonly ProjectDbContext _context;
    private readonly ILogger<MainInformationService> _logger;

    public MainInformationService(ProjectDbContext context, ILogger<MainInformationService> logger)
    {
      _context = context;
      _logger = logger;
    }
    public async Task<List<MainInformation>> GetMainInformationAsync()
    {
      return await _context.MainInformation.ToListAsync();
    }
    public async Task<ActionResult> AddMainInformtionAsync(MainInformationRequest request)
    {
      using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {

        MainInformation mainInformation = new MainInformation
        {
          ID = 1,
          ProjectName = request.ProjectName,
          Tagline = request.Tagline,
          IntroText = request.IntroText,
          LogoUrl = request.LogoUrl,
          FaviconUrl = request.FaviconUrl,
          MetaTitle = request.MetaTitle,
          MetaDescription = request.MetaDescription
        };
        await _context.AddAsync(mainInformation);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        _logger.LogInformation("Main Information successfully added: {ProjectName}", request.ProjectName);
        return new OkObjectResult("Main Infromation Successfully Added");
      }
      catch (Exception ex)
      {
        transaction.Rollback();
        _logger.LogError(ex, "An error occured while adding Main Information");
        return new StatusCodeResult(500);
      }
    }

    public async Task<ActionResult> UpdateMainInformationAsync(MainInformationUpdateRequest request)
    {

      MainInformation? mainInformation = await _context.MainInformation.FirstOrDefaultAsync();

      if (mainInformation == null)
      {
        _logger.LogWarning("Menu not found");
        return new NotFoundObjectResult("Main information not found.");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        if (request.ProjectName != null) mainInformation.ProjectName = request.ProjectName;
        if (request.Tagline != null) mainInformation.Tagline = request.Tagline;
        if (request.IntroText != null) mainInformation.IntroText = request.IntroText;
        if (request.LogoUrl != null) mainInformation.LogoUrl = request.LogoUrl;
        if (request.FaviconUrl != null) mainInformation.FaviconUrl = request.FaviconUrl;
        if (request.MetaTitle != null) mainInformation.MetaTitle = request.MetaTitle;
        if (request.MetaDescription != null) mainInformation.MetaDescription = request.MetaDescription;

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Main information updated successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "Error occured while updating the main information");
        return new StatusCodeResult(500);
      }
    }

    public async Task<ActionResult> DeleteMainInformationAsync()
    {
      MainInformation? mainInformation = await _context.MainInformation.FirstOrDefaultAsync();

      if (mainInformation == null)
      {
        _logger.LogWarning("Main information not found");
        return new NotFoundObjectResult("Main information is not found");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        _context.MainInformation.Remove(mainInformation);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Main information successfully deleted");
      }

      catch (Exception ex)
      {
        transaction.Rollback();
        _logger.LogWarning(ex, "An error occured while deleting the main informaiton");
        return new StatusCodeResult(500);
      }
    }
  }


}
