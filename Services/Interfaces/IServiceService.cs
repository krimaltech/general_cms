using Backend.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.Interfaces
{
  public interface IServiceService
  {
    Task<ActionResult> GetServicesAsync();
    Task<ActionResult> GetSingleServiceAsync(int id);
    Task<ActionResult> AddServiceAsync(ServiceRequest request);
    Task<ActionResult> UpdateServiceAsync(int id, ServiceUpdateRequest request);
    Task<ActionResult> DeleteServiceAsync(int id);
    Task<ActionResult> DeleteMethodologyAsync(int id);
    Task<ActionResult> DeleteTechnologyAsync(int id);
  }
}