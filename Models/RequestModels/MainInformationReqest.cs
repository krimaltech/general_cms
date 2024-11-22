namespace Backend.Models.RequestModels
{
    public class MainInformationRequest
    {
        public string ProjectName { get; set; } = String.Empty;

        public string? Tagline { get; set; }

        public string? IntroText { get; set; }

        public string LogoUrl { get; set; } = String.Empty;

        public string FaviconUrl { get; set; } = String.Empty;

        public string? MetaTitle { get; set; } = String.Empty;

        public string? MetaDescription { get; set; } = String.Empty;
    }
}