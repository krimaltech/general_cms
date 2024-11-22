using Backend.Data;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
  public class ImageService : IImageService
  {

    private readonly ProjectDbContext _context;
    private readonly ILogger<ImageService> _logger;

    public ImageService(ProjectDbContext context, ILogger<ImageService> logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task<List<Image>> GetImagesAsync()
    {
      return await _context.Images.ToListAsync();
    }

    public async Task<ActionResult<Image>> GetSingleImageAsync(int id)
    {
      Image? image = await _context.Images.FirstOrDefaultAsync(x => x.ID == id);
      if (image == null)
      {
        _logger.LogWarning("Image not found.");
        return new NotFoundObjectResult("Image not found");
      }
      return new OkObjectResult(image);
    }
    public async Task<ActionResult> AddImageAsync(ImageRequest request)
    {
      using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        Image image = new Image
        {
          ImageUrl = request.ImageUrl,
          Alt = request.Alt
        };

        await _context.Images.AddAsync(image);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return new OkObjectResult("Contact information is added successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to add contact information");
        return new StatusCodeResult(500);
      }
    }
    public async Task<ActionResult> DeleteImageAsync(int id)
    {

      Image? image = await _context.Images.FirstOrDefaultAsync(x => x.ID == id);

      if (image == null)
      {
        _logger.LogWarning("Contact information not found");
        return new NotFoundObjectResult("Contact information not found");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        _context.Images.Remove(image);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Image deleted successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogError(ex, "An Error occured while trying to delete the image");
        return new StatusCodeResult(500);
      }
    }

  }
}