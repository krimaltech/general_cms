using Backend.Data;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
  public class AddressService : IAddressService
  {
    private readonly ProjectDbContext _context;
    private readonly ILogger<AddressService> _logger;

    public AddressService(ProjectDbContext context, ILogger<AddressService> logger)
    {
      _logger = logger;
      _context = context;
    }

    public async Task<ActionResult<Address>> GetAddressAsync()
    {
      Address? address = await _context.Addresses.FirstOrDefaultAsync();
      if (address == null)
      {
        _logger.LogWarning("Address is not found");
        return new BadRequestObjectResult("Address is not found");
      }
      return new OkObjectResult(address);
    }

    public async Task<ActionResult> AddAddressAsync(AddressRequest request)
    {
      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        Address address = new Address
        {
          ID = 1,
          Country = request.Country,
          Region = request.Region,
          SubRegion = request.SubRegion,
          CityOrTown = request.CityOrTown,
          Street = request.Street,
          PostalCode = request.PostalCode,
          Latitude = request.Latitude,
          Longitude = request.Longitude
        };

        await _context.Addresses.AddAsync(address);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Address added successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to add address");
        return new StatusCodeResult(500);
      }
    }

    public async Task<ActionResult> UpdateAddressAsync(AddressUpdateRequest request)
    {
      Address? address = await _context.Addresses.FirstOrDefaultAsync();
      if (address == null)
      {
        _logger.LogWarning("Address not found");
        return new NotFoundObjectResult("Address not found");
      }
      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        if (request.Country != null) address.Country = request.Country;
        if (request.Region != null) address.Region = request.Region;
        if (request.SubRegion != null) address.SubRegion = request.Country;
        if (request.CityOrTown != null) address.CityOrTown = request.CityOrTown;
        if (request.Street != null) address.Street = request.Street;
        if (request.PostalCode != null) address.PostalCode = request.PostalCode;
        if (request.Latitude != null) address.Latitude = request.Latitude;
        if (request.Longitude != null) address.Longitude = request.Longitude;

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Address updated successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to update address");
        return new StatusCodeResult(500);
      }
    }

    public async Task<ActionResult> DeleteAddressAsync()
    {
      Address? address = await _context.Addresses.FirstOrDefaultAsync();
      if (address == null)
      {
        _logger.LogWarning("Address not found");
        return new NotFoundObjectResult("Address not found");
      }
      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Address deleted successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to delete address");
        return new StatusCodeResult(500);
      }
    }

  }
}