namespace Backend.Models.RequestModels
{
  public class BlogRequest
  {
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string? ImageURL { get; set; }
    public string Slug { get; set; } = String.Empty;
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public List<Section>? BlogSections { get; set; }
  };

  public class Section
  {
    public int ID { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string? ImageUrl { get; set; }
    public string Slug { get; set; } = String.Empty;
  }
}