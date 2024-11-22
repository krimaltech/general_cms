namespace Backend.Dtos
{

  public class BlogDto
  {
    public int ID { get; set; }
    public string Title { get; set; } = String.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string Slug { get; set; } = String.Empty;
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public ICollection<BlogSectionDto>? BlogSections { get; set; }
  }

  public class BlogSectionDto
  {
    public int ID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string Slug { get; set; } = string.Empty;
    public int BlogID { get; set; }
  }
}
