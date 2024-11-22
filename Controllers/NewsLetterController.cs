using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class NewsLetterController : ControllerBase
  {

    private readonly ILogger<NewsLetterController> _logger;
    private readonly INewsLetterService _newsLetterService;

    public NewsLetterController(ILogger<NewsLetterController> logger, INewsLetterService newsLetterService)
    {
      _logger = logger;
      _newsLetterService = newsLetterService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NewsLetter>>> GetNewsLetters()
    {
      return await _newsLetterService.GetNewsLettersAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NewsLetter>> GetSingleNewsLetter(int id)
    {
      return await _newsLetterService.GetSingleNewsLetterAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult> AddNewsLetter([FromBody] NewsLetterRequest request)
    {
      if (request == null)
      {
        _logger.LogWarning("Received null request");
        return new BadRequestObjectResult("Invalid request");
      }

      if (String.IsNullOrEmpty(request.Email))
      {
        _logger.LogWarning("Received null or empty email");
        return new BadRequestObjectResult("Invalid email");
      }

      return await _newsLetterService.AddNewsLetterAsync(request);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteNewsLetter(int id)
    {
      return await _newsLetterService.DeleteNewsLetterAsync(id);
    }
  }
}