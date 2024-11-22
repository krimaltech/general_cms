using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.Interfaces
{
  public interface IAddressService
  {

    Task<ActionResult<Address>> GetAddressAsync();
    Task<ActionResult> AddAddressAsync(AddressRequest request);
    Task<ActionResult> UpdateAddressAsync(AddressUpdateRequest request);
    Task<ActionResult> DeleteAddressAsync();
  }
}

