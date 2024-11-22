using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.MainModels
{
    public class ContactInformation
    {
        public int ID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        public string? PrimaryTelephoneNumber {get; set;}

        public string? SecondaryTelephoneNumber {get; set;}

        [Required]
        public string PrimaryMobileNumber { get; set; } = String.Empty;

        public string? SecondaryMobileNumber { get; set; }

        public string? FacebookUrl { get; set; }

        public string? InstagramUrl { get; set; }

        public string? TwitterUrl { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? TiktokUrl { get; set; }

        public string? YoutubeUrl { get; set; }

        public string? TheadsUrl { get; set; }

        public string? GooglePlusUrl { get; set; }
    }
}
