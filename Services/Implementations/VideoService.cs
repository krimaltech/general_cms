using Backend.Data;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
  public class VideoService : IVideoService
  {

    private readonly ProjectDbContext _context;
    private readonly ILogger<VideoService> _logger;

    public VideoService(ProjectDbContext context, ILogger<VideoService> logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task<List<Video>> GetVideosAsync()
    {
      return await _context.Videos.ToListAsync();
    }

    public async Task<ActionResult<Video>> GetSingleVideoAsync(int id)
    {
      Video? video = await _context.Videos.FirstOrDefaultAsync(x => x.ID == id);
      if (video == null)
      {
        _logger.LogWarning("Video not found.");
        return new NotFoundObjectResult("Video not found");
      }
      return new OkObjectResult(video);
    }
    public async Task<ActionResult> AddVideoAsync(VideoRequest request)
    {
      using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        Video video = new Video
        {
          VideoUrl = request.VideoUrl,
          Alt = request.Alt
        };

        await _context.Videos.AddAsync(video);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return new OkObjectResult("Video is added successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to add video");
        return new StatusCodeResult(500);
      }
    }
    public async Task<ActionResult> DeleteVideoAsync(int id)
    {

      Video? video = await _context.Videos.FirstOrDefaultAsync(x => x.ID == id);

      if (video == null)
      {
        _logger.LogWarning("Contact information not found");
        return new NotFoundObjectResult("Contact information not found");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        _context.Videos.Remove(video);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Video deleted successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogError(ex, "An Error occured while trying to delete the video");
        return new StatusCodeResult(500);
      }
    }

  }
}