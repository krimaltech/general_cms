using Backend.Data;
using Backend.Dtos;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
  public class ServiceService : IServiceService
  {
    private readonly ProjectDbContext _context;
    private readonly ILogger<ServiceService> _logger;

    public ServiceService(ILogger<ServiceService> logger, ProjectDbContext context)
    {
      _logger = logger;
      _context = context;
    }

    public async Task<ActionResult> GetServicesAsync()
    {
      var services = await _context.Services.Include(s => s.Methodologies).Include(s => s.Technologies).ToListAsync();
      var serviceDtos = services.Select(s => new ServiceDto
      {
        Title = s.Title,
        Description = s.Description,
        ImageUrl = s.ImageUrl,
        Methodologies = s.Methodologies?.Select(m => new MethodologyDto
        {
          Title = m.Title,
          StepNumber = m.StepNumber,
          Description = m.Description,
          ImageUrl = m.ImageUrl,
          ServiceID = m.ServiceID
        }).ToList(),
        Technologies = s.Technologies?.Select(t => new TechnologyDto
        {
          Title = t.Title,
          Description = t.Description,
          ImageUrl = t.ImageUrl,
          ServiceID = t.ServiceID
        }).ToList()
      }).ToList();

      if (serviceDtos == null)
      {
        _logger.LogWarning("No services found");
        return new NotFoundObjectResult("No services found");
      }
      return new OkObjectResult(serviceDtos);
    }

    public async Task<ActionResult> GetSingleServiceAsync(int id)
    {
      var service = await _context.Services.Include(s => s.Methodologies).Include(s => s.Technologies).FirstOrDefaultAsync(x => x.ID == id);

      if (service == null)
      {
        _logger.LogWarning("No service found");
        return new NotFoundObjectResult("No service found");
      }

      var serviceDto = new ServiceDto
      {
        ID = service.ID,
        Title = service.Title,
        Description = service.Description,
        ImageUrl = service.ImageUrl,
        Methodologies = service.Methodologies?.Select(m => new MethodologyDto
        {
          ID = m.ID,
          Title = m.Title,
          StepNumber = m.StepNumber,
          Description = m.Description,
          ImageUrl = m.ImageUrl,
          ServiceID = m.ServiceID
        }).ToList(),
        Technologies = service.Technologies?.Select(t => new TechnologyDto
        {
          ID = t.ID,
          Title = t.Title,
          Description = t.Description,
          ImageUrl = t.ImageUrl,
          ServiceID = t.ServiceID
        }).ToList(),
      };

      return new OkObjectResult(serviceDto);
    }
    public async Task<ActionResult> AddServiceAsync(ServiceRequest request)
    {
      var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        Service service = new Service
        {
          Title = request.Title,
          Description = request.Description,
          ImageUrl = request.ImageUrl,
        };

        await _context.Services.AddAsync(service);
        await _context.SaveChangesAsync();

        Service? currentService = await _context.Services.FirstOrDefaultAsync(x => x.Title == request.Title);

        if (currentService == null)
        {
          _logger.LogWarning("Failed to retrieve current service");
          return new NotFoundObjectResult("Failed to retrieve the newly added service");
        }

        if (request.Methodologies != null && request.Methodologies.Any())
        {

          var methodologies = request.Methodologies.Select(m => new Methodology
          {
            Title = m.Title,
            StepNumber = m.StepNumber,
            Description = m.Description,
            ImageUrl = m.ImageUrl,
          }).ToList();

          await _context.Methodologies.AddRangeAsync(methodologies);
          await _context.SaveChangesAsync();
        }
        if (request.Technologies != null && request.Technologies.Any())
        {

          var technologies = request.Technologies.Select(t => new Technology
          {
            Title = t.Title,
            Description = t.Description,
            ImageUrl = t.ImageUrl,
          }).ToList();
          await _context.Technologies.AddRangeAsync(technologies);
          await _context.SaveChangesAsync();
        }

        await transaction.CommitAsync();
        return new OkObjectResult("Service added successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogError(ex, "Error occurred while adding the service");
        return new StatusCodeResult(500);
      }
    }

    public async Task<ActionResult> UpdateServiceAsync(int id, ServiceUpdateRequest request)
    {

      Service? currentService = await _context.Services.FirstOrDefaultAsync(x => x.ID == id);

      if (currentService == null)
      {
        _logger.LogWarning("Service not found");
        return new NotFoundObjectResult("Service not found.");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        if (request.Title != null)
          currentService.Title = request.Title;

        if (request.Description != null)
          currentService.Description = request.Description;

        if (request.ImageUrl != null)
          currentService.ImageUrl = request.ImageUrl;


        if (request.Methodologies != null && request.Methodologies.Any())
        {
          foreach (var m in request.Methodologies)
          {
            Methodology? currentMethodology = _context.Methodologies.FirstOrDefault(x => x.ID == m.ID);
            if (currentMethodology != null)
            {
              if (m.Title != null)
                currentMethodology.Title = m.Title;

              if (m.StepNumber != null) currentMethodology.StepNumber = m.StepNumber.Value;

              if (m.ImageUrl != null)
                currentMethodology.ImageUrl = m.ImageUrl;

              if (m.Description != null)
                currentMethodology.Description = m.Description;
            }
            else
            {
              if (string.IsNullOrEmpty(m.Title) || m.StepNumber == null)
              {
                _logger.LogWarning("Empty or null values found at required fields");
                return new BadRequestObjectResult("Invalid details in Methodology");
              }

              await _context.Methodologies.AddAsync(new Methodology
              {
                Title = m.Title!,
                ImageUrl = m.ImageUrl,
                Description = m.Description,
                StepNumber = m.StepNumber.Value,
                ServiceID = currentService.ID
              });
            }
          }

        }
        if (request.Technologies != null && request.Technologies.Any())
        {
          foreach (var t in request.Technologies)
          {
            Technology? currentTechnology = _context.Technologies.FirstOrDefault(x => x.ID == t.ID);
            if (currentTechnology != null)
            {
              if (t.Title != null)
                currentTechnology.Title = t.Title;

              if (t.ImageUrl != null)
                currentTechnology.ImageUrl = t.ImageUrl;

              if (t.Description != null)
                currentTechnology.Description = t.Description;
            }
            else
            {
              if (string.IsNullOrEmpty(t.Title))
              {
                _logger.LogWarning("Empty or null values found at required fields");
                return new BadRequestObjectResult("Invalid details in Technology");
              }

              await _context.Technologies.AddAsync(new Technology
              {
                Title = t.Title!,
                ImageUrl = t.ImageUrl,
                Description = t.Description,
                ServiceID = currentService.ID
              });
            }
          }
        }
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Service updated successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogError(ex, "An error occured while trying to update the service");
        return new StatusCodeResult(500);
      }

    }

    public async Task<ActionResult> DeleteServiceAsync(int id)
    {

      Service? currentService = await _context.Services.FirstOrDefaultAsync(x => x.ID == id);
      if (currentService == null)
      {
        _logger.LogWarning("No services found");
        return new BadRequestObjectResult("No services found");
      }
      using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        if (_context.Methodologies.Any(x => x.ServiceID == currentService.ID))
        {
          List<Methodology> methodologies = [];
          foreach (var methodology in _context.Methodologies.Where(x => x.ServiceID == currentService.ID))
          {
            _context.Methodologies.Remove(methodology);
          }
        }
        if (_context.Technologies.Any(x => x.ServiceID == currentService.ID))
        {
          List<Technology> technologies = [];
          foreach (var technology in _context.Technologies.Where(x => x.ServiceID == currentService.ID))
          {
            _context.Technologies.Remove(technology);
          }
        }

        _context.Services.Remove(currentService);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Blog deleted successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to delete service");
        return new StatusCodeResult(500);
      }
    }

    public async Task<ActionResult> DeleteMethodologyAsync(int id)
    {
      var methodology = await _context.Methodologies.FirstOrDefaultAsync(s => s.ID == id);

      if (methodology == null)
      {
        _logger.LogWarning("Methodology not found");
        return new NotFoundObjectResult("Methodology not found.");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        _context.Remove(methodology);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Methodology deleted successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to delete Methodology");
        return new StatusCodeResult(500);
      }
    }
    public async Task<ActionResult> DeleteTechnologyAsync(int id)
    {
      var technology = await _context.Technologies.FirstOrDefaultAsync(s => s.ID == id);

      if (technology == null)
      {
        _logger.LogWarning("Technology not found");
        return new NotFoundObjectResult("Technology not found.");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        _context.Remove(technology);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Technology deleted successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to delete Technology");
        return new StatusCodeResult(500);
      }
    }
  }
}