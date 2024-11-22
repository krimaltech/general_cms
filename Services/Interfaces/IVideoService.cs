using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.Interfaces
{
  public interface IVideoService
  {
    public Task<List<Video>> GetVideosAsync();
    public Task<ActionResult<Video>> GetSingleVideoAsync(int id);
    public Task<ActionResult> AddVideoAsync(VideoRequest request);
    public Task<ActionResult> DeleteVideoAsync(int id);
  }
}