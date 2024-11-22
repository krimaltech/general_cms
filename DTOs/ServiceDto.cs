
namespace Backend.Dtos
{
  public class ServiceDto
  {
    public int ID { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string? ImageUrl { get; set; } = String.Empty;

    public List<MethodologyDto>? Methodologies { get; set; }
    public List<TechnologyDto>? Technologies { get; set; }
  }

  public class MethodologyDto
  {
    public int ID { get; set; }
    public string Title { get; set; } = String.Empty;
    public int StepNumber { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int ServiceID;
  }

  public class TechnologyDto
  {
    public int ID { get; set; }
    public string Title { get; set; } = String.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int ServiceID;
  }

}