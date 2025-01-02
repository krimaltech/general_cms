using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.MainModels
{
    public class About
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string? tagline {get; set;}

        [Required]
        public string CompanyDescription { get; set; } = String.Empty;

        public string? Objectives { get; set; }

        public string? Vision { get; set; }

        public string? Mission { get; set; }

        public string? Features { get; set; }

        public string? ImageUrl { get; set; }
    }
}
