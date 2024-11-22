using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.MainModels
{
    public class TeamMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;
        public string? Position { get; set; }
        public string? SuperiorName { get; set; }
        public string? SuperiorPosition { get; set; }
        public string? Department { get; set; }
        public string? ImageUrl { get; set; }
    }
}
