namespace Backend.Models.RequestModels
{
  public class TeamMemberRequest
  {
    public string Name { get; set; } = String.Empty;
    public string? Position { get; set; }
    public string? SuperiorName { get; set; }
    public string? SuperiorPosition { get; set; }
    public string? Department { get; set; }

    public string? ImageUrl { get; set; }
  }
}
