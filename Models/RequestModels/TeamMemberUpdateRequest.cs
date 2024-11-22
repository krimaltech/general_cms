namespace Backend.Models.RequestModels
{
  public class TeamMemberUpdateRequest
  {
    public string? Name { get; set; } 
    public string? Position { get; set; }
    public string? SuperiorName { get; set; }
    public string? SuperiorPosition { get; set; }
    public string? Department { get; set; }
    public string? ImageUrl { get; set; }
  }
}
