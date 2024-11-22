using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.RequestModels
{
  public class AboutRequest
  {
    public string CompanyDescription { get; set; } = String.Empty;

    public string? Objectives { get; set; }

    public string? Vision { get; set; }

    public string? Mission { get; set; }

    public string? Features { get; set; }

    public string? ImageUrl { get; set; }
  }
}
