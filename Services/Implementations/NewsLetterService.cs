using Backend.Data;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Backend.Services.Implementations
{
  public class NewsLetterService : INewsLetterService
  {

    private readonly ProjectDbContext _context;
    private readonly ILogger<NewsLetterService> _logger;

    public NewsLetterService(ProjectDbContext context, ILogger<NewsLetterService> logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task<ActionResult<IEnumerable<NewsLetter>>> GetNewsLettersAsync()
    {
      var newsLetters = await _context.NewsLetters.ToListAsync();
      if (newsLetters == null)
      {
        _logger.LogWarning("News letters not found");
        return new NotFoundObjectResult("News letters not found");
      }
      return new OkObjectResult(newsLetters);
    }


    public async Task<ActionResult<NewsLetter>> GetSingleNewsLetterAsync(int id)
    {
      var newsLetter = await _context.NewsLetters.FirstOrDefaultAsync(x => x.ID == id);
      if (newsLetter == null)
      {
        _logger.LogWarning("News letter not found");
        return new NotFoundObjectResult("News letter not found");
      }
      return new OkObjectResult(newsLetter);
    }

    public async Task<ActionResult> AddNewsLetterAsync(NewsLetterRequest request)
    {

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        NewsLetter newsLetter = new NewsLetter
        {
          Email = request.Email
        };
        await _context.NewsLetters.AddAsync(newsLetter);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("News letter successfully added");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to add news letter ");
        return new StatusCodeResult(500);
      }

    }

    public async Task<ActionResult> DeleteNewsLetterAsync(int id)
    {
      var newsLetter = await _context.NewsLetters.FirstOrDefaultAsync(x => x.ID == id);

      if (newsLetter == null)
      {
        _logger.LogWarning("Newsletter not found");
        return new NotFoundObjectResult("Newsletter not found");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        _context.NewsLetters.Remove(newsLetter);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Newsletter successfully deleted");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to delete news letter ");
        return new StatusCodeResult(500);
      }
    }

  }
}