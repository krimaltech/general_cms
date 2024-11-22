using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.MainModels
{
  public class Methodology
  {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Required]
    public string Title { get; set; } = String.Empty;

    [Required]
    public int StepNumber { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }


    [Required]
    public int ServiceID { get; set; }

    [ForeignKey("ServiceID")]

    public Service? Service { get; set; }
  }
}