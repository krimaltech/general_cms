using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.Interfaces
{
  public interface IAboutService
  {
    Task<ActionResult<IEnumerable<About>>> GetAboutsAsync();
    Task<ActionResult> AddAboutAsync(AboutRequest request);
    Task<ActionResult> UpdateAboutAsync(AboutUpdateRequest request);
    Task<ActionResult> DeleteAboutAsync();
  }
}