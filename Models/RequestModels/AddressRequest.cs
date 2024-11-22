
namespace Backend.Models.RequestModels
{
  public class AddressRequest
  {
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
