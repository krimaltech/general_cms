using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.MainModels
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string ImageUrl { get; set; } = String.Empty;

        [Required]
        public string Alt { get; set; } = String.Empty;
    }
}
