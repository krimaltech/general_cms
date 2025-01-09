using Backend.Data;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
  public class MenuService : IMenuService
  {

    private readonly ProjectDbContext _context;
    private readonly ILogger<MenuService> _logger;

    public MenuService(ProjectDbContext context, ILogger<MenuService> logger)
    {
      _context = context;
      _logger = logger;
    }
    public async Task<List<Menu>> GetMenusAsync()
    {
      return await _context.Menus.ToListAsync();
    }
    public async Task<ActionResult<Menu>> GetSingleMenuAsync(int id)
    {
      Menu? menu = await _context.Menus.FirstOrDefaultAsync(x => x.ID == id);
      if (menu == null)
      {
        _logger.LogWarning("Menu not found");
        return new NotFoundObjectResult("Menu not found");
      }
      return new OkObjectResult(menu);
    }
    public async Task<ActionResult> AddMenuAsync(MenuRequest request)
    {

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        Menu menu = new Menu
        {
          Name = request.Name,
          Slug = request.Slug
        };
        await _context.Menus.AddAsync(menu);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return new OkObjectResult("Menu added successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to add menu");
        return new StatusCodeResult(500);
      }

    }

    public async Task<ActionResult> UpdateMenuAsync(int id, MenuUpdateRequest request)
    {
      Menu? menu = await _context.Menus.FirstOrDefaultAsync(x => x.ID == id);
      if (menu == null)
      {
        _logger.LogWarning("Menu not found");
        return new NotFoundObjectResult("Menu not found");
      }
      using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        if (request.Name != null) menu.Name = request.Name;
        if (request.Slug != null) menu.Slug = request.Slug;

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Menu successfully updated");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "Error occured while updating the menu");
        return new StatusCodeResult(500);
      }
    }
    public async Task<ActionResult> DeleteMenuAsync(int id)
    {
      Menu? menu = await _context.Menus.FirstOrDefaultAsync(x => x.ID == id);
      if (menu == null)
      {
        _logger.LogWarning("Menu not found");
        return new NotFoundObjectResult("Menu not found");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        _context.Menus.Remove(menu);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Menu deleted successfully");

      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to delete menu");
        return new StatusCodeResult(500);
      }

    }
  }
}