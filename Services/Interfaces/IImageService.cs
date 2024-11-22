using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.Interfaces
{
  public interface IImageService
  {
    public Task<List<Image>> GetImagesAsync();
    public Task<ActionResult<Image>> GetSingleImageAsync(int id);
    public Task<ActionResult> AddImageAsync(ImageRequest request);
    public Task<ActionResult> DeleteImageAsync(int id);
  }
}