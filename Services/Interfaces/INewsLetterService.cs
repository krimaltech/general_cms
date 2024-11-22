using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.Interfaces
{
  public interface INewsLetterService
  {
    public Task<ActionResult<IEnumerable<NewsLetter>>> GetNewsLettersAsync();
    public Task<ActionResult<NewsLetter>> GetSingleNewsLetterAsync(int id);
    public Task<ActionResult> AddNewsLetterAsync(NewsLetterRequest request);
    public Task<ActionResult> DeleteNewsLetterAsync(int id);
  }
}