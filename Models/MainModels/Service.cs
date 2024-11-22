using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.MainModels
{
  public class Service
  {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Required]
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string? ImageUrl { get; set; } = String.Empty;

    public ICollection<Methodology>? Methodologies { get; set; }
    public ICollection<Technology>? Technologies { get; set; }
  }
}