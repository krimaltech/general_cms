using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.MainModels
{
    public class BlogSection
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

        [Required]
        public int BlogID { get; set; }

        [ForeignKey("BlogID")]
        public Blog? Blog { get; set; }
    }
}
