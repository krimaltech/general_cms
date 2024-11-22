using Backend.Data;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
  public class ContactInformationService : IContactInformationService
  {

    private readonly ProjectDbContext _context;
    private readonly ILogger<ContactInformationService> _logger;

    public ContactInformationService(ProjectDbContext context, ILogger<ContactInformationService> logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task<List<ContactInformation>> GetContactInformationAsync()
    {
      return await _context.ContactInformation.ToListAsync();
    }

    public async Task<ActionResult> AddContactInformationAsync(ContactInformationRequest request)
    {
      using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        ContactInformation contactInformation = new ContactInformation
        {
          ID = 1,
          Email = request.Email,
          PrimaryMobileNumber = request.PrimaryMobileNumber,
          SecondaryMobileNumber = request.SecondaryMobileNumber,
          PrimaryTelephoneNumber = request.PrimaryTelephoneNumber,
          SecondaryTelephoneNumber = request.SecondaryMobileNumber,
          FacebookUrl = request.FacebookUrl,
          InstagramUrl = request.InstagramUrl,
          TwitterUrl = request.TwitterUrl,
          LinkedInUrl = request.LinkedInUrl,
          TiktokUrl = request.TiktokUrl,
          YoutubeUrl = request.YoutubeUrl,
          TheadsUrl = request.TheadsUrl,
          GooglePlusUrl = request.GooglePlusUrl
        };

        await _context.ContactInformation.AddAsync(contactInformation);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return new OkObjectResult("Contact information is added successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to add contact information");
        return new StatusCodeResult(500);
      }
    }

    public async Task<ActionResult> UpdateContactInformationAsync(ContactInformationUpdateRequest request)
    {
      ContactInformation? contactInformation = await _context.ContactInformation.FirstOrDefaultAsync();
      if (contactInformation == null)
      {
        _logger.LogWarning("Contact information not found");
        return new NotFoundObjectResult("Contact information not found");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        if (request.Email != null) contactInformation.Email = request.Email;
        if (request.PrimaryMobileNumber != null) contactInformation.PrimaryMobileNumber = request.PrimaryMobileNumber;
        if (request.SecondaryMobileNumber != null) contactInformation.SecondaryMobileNumber = request.SecondaryMobileNumber;
        if (request.PrimaryTelephoneNumber != null) contactInformation.PrimaryTelephoneNumber = request.PrimaryTelephoneNumber;
        if (request.SecondaryTelephoneNumber != null) contactInformation.SecondaryTelephoneNumber = request.SecondaryTelephoneNumber;
        if (request.FacebookUrl != null) contactInformation.FacebookUrl = request.FacebookUrl;
        if (request.InstagramUrl != null) contactInformation.InstagramUrl = request.InstagramUrl;
        if (request.TwitterUrl != null) contactInformation.TwitterUrl = request.TwitterUrl;
        if (request.LinkedInUrl != null) contactInformation.LinkedInUrl = request.LinkedInUrl;
        if (request.TiktokUrl != null) contactInformation.TiktokUrl = request.TiktokUrl;
        if (request.YoutubeUrl != null) contactInformation.YoutubeUrl = request.YoutubeUrl;
        if (request.TheadsUrl != null) contactInformation.TheadsUrl = request.TheadsUrl;
        if (request.GooglePlusUrl != null) contactInformation.GooglePlusUrl = request.GooglePlusUrl;

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Contact Information updated successfully.");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "Error occured while trying to update the contact information");
        return new StatusCodeResult(500);
      }

    }

    public async Task<ActionResult> DeleteContactInformationAsync()
    {

      ContactInformation? contactInformation = await _context.ContactInformation.FirstOrDefaultAsync();

      if (contactInformation == null)
      {
        _logger.LogWarning("Contact information not found");
        return new NotFoundObjectResult("Contact information not found");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        _context.ContactInformation.Remove(contactInformation);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Contact information deleted successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogError(ex, "An Error occured while trying to delete the contact information");
        return new StatusCodeResult(500);
      }
    }

  }
}