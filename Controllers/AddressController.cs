using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
  public class AddressController : ControllerBase
  {
    private readonly ILogger<AddressController> _logger;
    private readonly IAddressService _addressService;

    public AddressController(ILogger<AddressController> logger, IAddressService addressService)
    {
      _logger = logger;
      _addressService = addressService;
    }

    [HttpGet]
    public async Task<ActionResult<Address>> GetAddress()
    {
      return await _addressService.GetAddressAsync();
    }

    [HttpPost]
    public async Task<ActionResult> AddAddress([FromBody] AddressRequest request)
    {
      if (Request == null)
      {
        _logger.LogWarning("Received null request");
        return BadRequest("Invalid request");
      }

      if (String.IsNullOrEmpty(request.Country))
      {
        _logger.LogWarning("Received null or empty Country name");
        return BadRequest("Country cannot be null or empty");
      }

      return await _addressService.AddAddressAsync(request);

    }

    [HttpPatch]
    public async Task<ActionResult> UpdateAddress([FromBody] AddressUpdateRequest request)
    {
      if (request == null)
      {
        _logger.LogWarning("Received null request");
        return BadRequest("Invalid request");
      }
      if (request.Country != null && request.Country == String.Empty)
      {
        _logger.LogWarning("Received empty country name");
        return BadRequest("Country name cannot be empty");
      }
      return await _addressService.UpdateAddressAsync(request);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAddress()
    {
      return await _addressService.DeleteAddressAsync();
    }
  }
}