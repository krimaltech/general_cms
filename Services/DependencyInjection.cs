using Backend.Models.MainModels;
using Backend.Services.Implementations;
using Backend.Services.Interfaces;

public static class DependencyInjection
{
  public static void AddApplicationServices(this IServiceCollection services)
  {
    services.AddScoped<IBlogService, BlogService>();
    services.AddScoped<IMainInformationService, MainInformationService>();
    services.AddScoped<IContactInformationService, ContactInformationService>();
    services.AddScoped<IImageService, ImageService>();
    services.AddScoped<IVideoService, VideoService>();
    services.AddScoped<IMenuService, MenuService>();
    services.AddScoped<INewsLetterService, NewsLetterService>();
    services.AddScoped<ITeamMemberService, TeamMemberService>();
    services.AddScoped<ITestimonialService, TestimonialService>();
    services.AddScoped<IAboutService, AboutServices>();
    services.AddScoped<IAddressService, AddressService>();
  }
}