using Backend.Data;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Backend.Services.Implementations
{
  public class TestimonialService : ITestimonialService
  {

    private readonly ProjectDbContext _context;
    private readonly ILogger<TestimonialService> _logger;

    public TestimonialService(ProjectDbContext context, ILogger<TestimonialService> logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task<List<Testimonial>> GetTestimonialsAsync()
    {
      return await _context.Testimonials.ToListAsync();
    }

    public async Task<ActionResult<Testimonial>> GetSingleTestimonial(int id)
    {
      Testimonial? testimonial = await _context.Testimonials.FirstOrDefaultAsync(x => x.ID == id);
      if (testimonial == null)
      {
        _logger.LogWarning("Testimonial not found.");
        return new NotFoundObjectResult("Testimonial not found");
      }
      return new OkObjectResult(testimonial);
    }
    public async Task<ActionResult> AddTestimonialAsync(TestimonialRequest request)
    {
      using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        Testimonial testimonial = new Testimonial
        {
          Name = request.Name,
          Organization = request.Organization,
          Email = request.Email,
          Message = request.Message,
          ImageUrl = request.ImageUrl
        };

        await _context.Testimonials.AddAsync(testimonial);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return new OkObjectResult("Testimonial is added successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to add Testimonial");
        return new StatusCodeResult(500);
      }
    }

    public async Task<ActionResult> UpdateTestimonialAsync(int id, TestimonialUpdateRequest request)
    {
      Testimonial? testimonial = await _context.Testimonials.FirstOrDefaultAsync(x => x.ID == id);
      if (testimonial == null)
      {
        _logger.LogWarning("Testimonial not found");
        return new NotFoundObjectResult("Testimonial not found");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        if (request.Name != null) testimonial.Name = request.Name;
        if (request.Organization != null) testimonial.Organization = request.Organization;
        if (request.Email != null) testimonial.Email = request.Email;
        if (request.Message != null) testimonial.Message = request.Message;
        if (request.ImageUrl != null) testimonial.ImageUrl = request.ImageUrl;

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Testimonial updated successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while updating the Testimonial");
        return new StatusCodeResult(500);
      }
    }
    public async Task<ActionResult> DeleteTestimonialAsync(int id)
    {

      Testimonial? testimonial = await _context.Testimonials.FirstOrDefaultAsync(x => x.ID == id);

      if (testimonial == null)
      {
        _logger.LogWarning("Testimonial not found");
        return new NotFoundObjectResult("Testimonial not found");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        _context.Testimonials.Remove(testimonial);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Testimonial removed successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogError(ex, "An Error occured while trying to remove testimonial");
        return new StatusCodeResult(500);
      }
    }

  }
}