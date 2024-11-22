using Backend.Models.MainModels;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ProjectDbContext : DbContext
    {
        public DbSet<MainInformation> MainInformation { get; set; } = null!;
        public DbSet<About> About { get; set; } = null!;
        public DbSet<ContactInformation> ContactInformation { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<Video> Videos { get; set; } = null!;
        public DbSet<Menu> Menus { get; set; } = null!;
        public DbSet<TeamMember> TeamMembers { get; set; } = null!;
        public DbSet<Blog> Blogs { get; set; } = null!;
        public DbSet<BlogSection> BlogSections { get; set; } = null!;
        public DbSet<Testimonial> Testimonials { get; set; } = null!;
        public DbSet<NewsLetter> NewsLetters { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<Methodology> Methodologies { get; set; } = null!;
        public DbSet<Technology> Technologies { get; set; } = null!;
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options) { }
    }
}
