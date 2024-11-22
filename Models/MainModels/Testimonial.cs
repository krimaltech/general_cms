using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.MainModels
{
    public class Testimonial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;

        public string? Organization { get; set; }

        [Required]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string Message { get; set; } = String.Empty;

        [Required]
        public string ImageUrl { get; set; } = String.Empty;
    }
}
