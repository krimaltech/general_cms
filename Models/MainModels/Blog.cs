using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.MainModels
{
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; } = String.Empty;

        [Required]
        public string Description { get; set; } = String.Empty;

        public string? ImageUrl { get; set; }

        [Required]
        public string Slug { get; set; } = String.Empty;

        public string? MetaTitle { get; set; } = String.Empty;

        public string? MetaDescription { get; set; } = String.Empty;

        public ICollection<BlogSection>? BlogSections { get; set; }

    }
}
