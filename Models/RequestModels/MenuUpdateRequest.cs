using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.RequestModels
{
  public class MenuUpdateRequest
  {

    public string? Name { get; set; }

    public string? Slug { get; set; }
  }
}