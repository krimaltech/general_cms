using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.Interfaces
{
  public interface IMainInformationService
  {
    public Task<List<MainInformation>> GetMainInformationAsync();
    public Task<ActionResult> AddMainInformtionAsync(MainInformationRequest request);
    public Task<ActionResult> UpdateMainInformationAsync(MainInformationUpdateRequest request);
    public Task<ActionResult> DeleteMainInformationAsync();
  }
}