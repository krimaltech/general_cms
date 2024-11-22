namespace Backend.Models.RequestModels
{
  public class MainInformationUpdateRequest
  {
    public string? ProjectName { get; set; }

    public string? Tagline { get; set; }

    public string? IntroText { get; set; }

    public string? LogoUrl { get; set; }

    public string? FaviconUrl { get; set; }

    public string? MetaTitle { get; set; }

    public string? MetaDescription { get; set; }
  }
}