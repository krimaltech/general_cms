using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.Interfaces
{
  public interface IBlogService
  {
    Task<ActionResult> GetBlogsAsync();
    Task<ActionResult> GetSingleBlogAsync(int id);
    Task<ActionResult> AddBlogAsync(BlogRequest request);
    Task<ActionResult> UpdateBlogAsync(int id, BlogUpdateRequest request);
    Task<ActionResult> DeleteBlogAsync(int id);
    Task<ActionResult> DeleteBlogSectionAsync(int id);
  }
}