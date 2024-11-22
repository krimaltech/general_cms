using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.MainModels
{
  public class Technology
  {
    public int ID { get; set; }

    [Required]
    public string Title { get; set; } = String.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }


    [Required]
    public int ServiceID { get; set; }

    [ForeignKey("ServiceID")]

    public Service? Service { get; set; }
  }
}