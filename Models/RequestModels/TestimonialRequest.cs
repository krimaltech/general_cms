namespace Backend.Models.RequestModels
{
  public class TestimonialRequest
  {
    public string Name { get; set; } = String.Empty;

    public string? Organization { get; set; }

    public string Email { get; set; } = String.Empty;

    public string Message { get; set; } = String.Empty;

    public string ImageUrl { get; set; } = String.Empty;
  }
}