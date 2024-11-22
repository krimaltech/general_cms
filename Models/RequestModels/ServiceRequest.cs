
namespace Backend.Models.RequestModels
{
  public class ServiceRequest
  {

    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string? ImageUrl { get; set; } = String.Empty;
    public List<MethodologyRequest>? Methodologies { get; set; }
    public List<TechnologyRequest>? Technologies { get; set; }
  }

  public class MethodologyRequest
  {
    public string Title { get; set; } = String.Empty;

    public int StepNumber { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
  }

  public class TechnologyRequest
  {
    public string Title { get; set; } = String.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
  }

}