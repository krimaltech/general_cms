using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.RequestModels
{
  public class MenuRequest
  {

    public string Name { get; set; } = String.Empty;

    public string Slug { get; set; } = String.Empty;
  }
}
