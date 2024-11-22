using Backend.Data;
using Backend.Dtos;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
  public class BlogService : IBlogService
  {

    private readonly ProjectDbContext _context;
    private readonly ILogger<BlogService> _logger;

    public BlogService(ProjectDbContext context, ILogger<BlogService> logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task<ActionResult> GetBlogsAsync()
    {
      var blogs = await _context.Blogs.Include(b => b.BlogSections).ToListAsync();

      var blogDtos = blogs.Select(b => new BlogDto
      {
        ID = b.ID,
        Title = b.Title,
        Description = b.Description,
        ImageUrl = b.ImageUrl,
        Slug = b.Slug,
        MetaTitle = b.MetaTitle,
        MetaDescription = b.MetaDescription,
        BlogSections = b.BlogSections?.Select(bs => new BlogSectionDto
        {
          ID = bs.ID,
          Title = bs.Title,
          Description = bs.Description,
          ImageUrl = bs.ImageUrl,
          Slug = bs.Slug,
          BlogID = bs.BlogID
        }).ToList()
      }).ToList();

      if (blogs == null)
      {
        _logger.LogWarning("No blogs found");
        return new NotFoundObjectResult("No blogs found");
      }

      return new OkObjectResult(blogDtos);

    }



    public async Task<ActionResult> GetSingleBlogAsync(int id)
    {
      var blog = await _context.Blogs.Include(b => b.BlogSections).FirstOrDefaultAsync(x => x.ID == id);

      if (blog == null)
      {
        _logger.LogWarning("No blog found");
        return new NotFoundObjectResult("No blog found");
      }

      var blogDto = new BlogDto
      {
        ID = blog.ID,
        Title = blog.Title,
        Description = blog.Description,
        ImageUrl = blog.ImageUrl,
        Slug = blog.Slug,
        MetaTitle = blog.MetaTitle,
        MetaDescription = blog.MetaDescription,
        BlogSections = blog.BlogSections?.Select(bs => new BlogSectionDto
        {
          ID = bs.ID,
          Title = bs.Title,
          Description = bs.Description,
          ImageUrl = bs.ImageUrl,
          Slug = bs.Slug,
          BlogID = bs.BlogID
        }).ToList()
      };
      return new OkObjectResult(blogDto);
    }

    public async Task<ActionResult> AddBlogAsync(BlogRequest request)
    {
      var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        Blog blog = new Blog
        {
          Title = request.Title,
          Description = request.Description,
          ImageUrl = request.ImageURL,
          Slug = request.Slug,
          MetaTitle = request.MetaTitle,
          MetaDescription = request.MetaDescription,
        };

        await _context.Blogs.AddAsync(blog);
        await _context.SaveChangesAsync();

        Blog? currentBlog = await _context.Blogs.FirstOrDefaultAsync(x => x.Title == request.Title);

        if (currentBlog == null)
        {
          _logger.LogWarning("Failed to retrieve current blog");
          return new NotFoundObjectResult("Failed to retrieve the newly added blog");
        }

        if (request.BlogSections != null && request.BlogSections.Any())
        {

          var blogSections = request.BlogSections.Select(section => new BlogSection
          {
            Title = section.Title,
            ImageUrl = section.ImageUrl,
            Description = section.Description,
            Slug = section.Slug,
            BlogID = currentBlog.ID
          }).ToList();

          await _context.BlogSections.AddRangeAsync(blogSections);
          await _context.SaveChangesAsync();
        }

        await transaction.CommitAsync();
        return new OkObjectResult("Blog added successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogError(ex, "Error occurred while adding the blog");
        return new StatusCodeResult(500);
      }
    }

    public async Task<ActionResult> UpdateBlogAsync(int id, BlogUpdateRequest request)
    {

      Blog? currentBlog = await _context.Blogs.FirstOrDefaultAsync(x => x.ID == id);

      if (currentBlog == null)
      {
        _logger.LogWarning("Blog not found");
        return new NotFoundObjectResult("Blog not found.");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        if (request.Title != null)
          currentBlog.Title = request.Title;

        if (request.Description != null)
          currentBlog.Description = request.Description;

        if (request.ImageURL != null)
          currentBlog.ImageUrl = request.ImageURL;

        if (request.Slug != null)
          currentBlog.Slug = request.Slug;

        if (request.MetaTitle != null)
          currentBlog.MetaTitle = request.MetaTitle;

        if (request.MetaDescription != null)
          currentBlog.MetaDescription = request.MetaDescription;

        if (request.BlogSections != null && request.BlogSections.Any())
        {
          foreach (var section in request.BlogSections)
          {
            BlogSection? currentSection = _context.BlogSections.FirstOrDefault(x => x.ID == section.ID);
            Console.WriteLine(section.Description);
            if (currentSection != null)
            {
              if (section.Title != null)
                currentSection.Title = section.Title;

              if (section.ImageUrl != null)
                currentSection.ImageUrl = section.ImageUrl;

              if (section.Description != null)
                currentSection.Description = section.Description;

              if (section.Slug != null)
                currentSection.Slug = section.Slug;
            }
            else
            {
              if (string.IsNullOrEmpty(section.Title) || string.IsNullOrEmpty(section.Description) || string.IsNullOrEmpty(section.Slug))
              {
                _logger.LogWarning("Empty or null values found at required fields");
                return new BadRequestObjectResult("Invalid details in Blog section");
              }

              await _context.BlogSections.AddAsync(new BlogSection
              {
                Title = section.Title!,
                ImageUrl = section.ImageUrl,
                Description = section.Description,
                Slug = section.Slug,
                BlogID = currentBlog.ID
              });
            }
          }
        }

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return new OkObjectResult("Blog updated successfully");

      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogError(ex, "An error occured while trying to update the blog");
        return new StatusCodeResult(500);
      }

    }

    public async Task<ActionResult> DeleteBlogAsync(int id)
    {

      Blog? currentBlog = await _context.Blogs.FirstOrDefaultAsync(x => x.ID == id);
      if (currentBlog == null)
      {
        _logger.LogWarning("No blogs found");
        return new BadRequestObjectResult("No blogs found");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        if (_context.BlogSections.Any(x => x.BlogID == currentBlog.ID))
        {
          List<BlogSection> blogSections = [];
          foreach (var section in _context.BlogSections.Where(x => x.BlogID == currentBlog.ID))
          {
            _context.BlogSections.Remove(section);
          }
        }
        _context.Blogs.Remove(currentBlog);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Blog deleted successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to delete blog");
        return new StatusCodeResult(500);
      }
    }

    public async Task<ActionResult> DeleteBlogSectionAsync(int id)
    {
      var blogSection = await _context.BlogSections.FirstOrDefaultAsync(s => s.ID == id);

      if (blogSection == null)
      {
        _logger.LogWarning("Section not found");
        return new NotFoundObjectResult("Section not found.");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        _context.Remove(blogSection);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Blog section deleted successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to delete blog section");
        return new StatusCodeResult(500);
      }
    }
  }

}

