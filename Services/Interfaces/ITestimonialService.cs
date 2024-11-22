using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.Interfaces
{
  public interface ITestimonialService
  {
    public Task<List<Testimonial>> GetTestimonialsAsync();
    public Task<ActionResult<Testimonial>> GetSingleTestimonial(int id);
    public Task<ActionResult> AddTestimonialAsync(TestimonialRequest request);
    public Task<ActionResult> UpdateTestimonialAsync(int id, TestimonialUpdateRequest request);
    public Task<ActionResult> DeleteTestimonialAsync(int id);
  }
}