using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.MainModels
{
    public class Address
    {
        public int ID { get; set; }

        [Required]
        public string Country { get; set; } = string.Empty;

        //Province, States
        public string? Region { get; set; }

        //Districts, Counties
        public string? SubRegion { get; set; }

        //Municipalities, Cities
        public string? CityOrTown { get; set; }

        public string? Street { get; set; }

        public string? PostalCode { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}
