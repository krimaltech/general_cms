
namespace Backend.Models.RequestModels
{
  public class ServiceUpdateRequest
  {

    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public List<MethodologyUpdateRequest>? Methodologies { get; set; }
    public List<TechnologyUpdateRequest>? Technologies { get; set; }
  }

  public class MethodologyUpdateRequest
  {

    public int ID { get; set; }
    public string? Title { get; set; }

    public int? StepNumber { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int? ServiceID { get; set; }
  }

  public class TechnologyUpdateRequest
  {
    public int? ID { get; set; }
    public string? Title { get; set; } = String.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int? ServiceID { get; set; }

  }

}