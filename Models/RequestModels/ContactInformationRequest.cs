namespace Backend.Models.RequestModels
{
  public class ContactInformationRequest
  {
    public string Email { get; set; } = String.Empty;

    public string? PrimaryTelephoneNumber { get; set; }

    public string? SecondaryTelephoneNumber { get; set; }

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
