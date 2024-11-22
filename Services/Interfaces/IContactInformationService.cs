using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.Interfaces
{
  public interface IContactInformationService
  {
    public Task<List<ContactInformation>> GetContactInformationAsync();
    public Task<ActionResult> AddContactInformationAsync(ContactInformationRequest request);
    public Task<ActionResult> UpdateContactInformationAsync(ContactInformationUpdateRequest request);
    public Task<ActionResult> DeleteContactInformationAsync();
  }
}