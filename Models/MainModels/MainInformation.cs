using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.MainModels
{
    public class MainInformation
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [MaxLength(50)]
        [Required]
        public string ProjectName { get; set; } = String.Empty;

        public string? Tagline { get; set; }
        public string? IntroText { get; set; }

        [Required]
        public string LogoUrl { get; set; } = String.Empty;

        [Required]
        public string FaviconUrl { get; set; } = String.Empty;

        public string? MetaTitle { get; set; } = String.Empty;
        public string? MetaDescription { get; set; } = String.Empty;

    }
}
