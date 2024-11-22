using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.Interfaces
{
  public interface IMenuService
  {
    public Task<List<Menu>> GetMenusAsync();
    public Task<ActionResult<Menu>> GetSingleMenuAsync(int id);
    public Task<ActionResult> AddMenuAsync(MenuRequest request);

    public Task<ActionResult> UpdateMenuAsync(int id, MenuUpdateRequest request);
    public Task<ActionResult> DeleteMenuAsync(int id);
  }
}