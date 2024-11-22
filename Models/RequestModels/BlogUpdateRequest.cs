namespace Backend.Models.RequestModels
{
    public class BlogUpdateRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public string? Slug { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public List<SectionUpdate>? BlogSections { get; set; }
    }
    public class SectionUpdate
    {
        public int? ID {get; set;}
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Slug { get; set; }
    }
}
