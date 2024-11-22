using System.ComponentModel.DataAnnotations;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class TeamMemberController : ControllerBase
  {

    private readonly ILogger<NewsLetterController> _logger;
    private readonly ITeamMemberService _teamMemberService;

    public TeamMemberController(ILogger<NewsLetterController> logger, ITeamMemberService teamMemberService)
    {
      _logger = logger;
      _teamMemberService = teamMemberService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamMember>>> GetTeamMembers()
    {
      return await _teamMemberService.GetTeamMembersAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeamMember>> GetSingleTeamMember(int id)
    {
      return await _teamMemberService.GetSingleTeamMemberAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult> AddTeaMMember([FromBody] TeamMemberRequest request)
    {
      if (request == null)
      {
        _logger.LogWarning("Received null request");
        return new BadRequestObjectResult("Invalid request");
      }
      if (String.IsNullOrEmpty(request.Name))
      {
        _logger.LogWarning("Received empty or null name");
        return new BadRequestObjectResult("Invalid name");
      }

      return await _teamMemberService.AddTeamMemberAsync(request);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateTeamMember(int id, [FromBody] TeamMemberUpdateRequest request)
    {
      if (request == null)
      {
        _logger.LogWarning("Received null request");
        return new BadRequestObjectResult("Invalid request");
      }
      if (request.Name != null && request.Name == String.Empty)
      {
        _logger.LogWarning("Received empty name");
        return new BadRequestObjectResult("Invalid Name");
      }
      return await _teamMemberService.UpdateTeamMemberAsync(id, request);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteNewsLetter(int id)
    {
      return await _teamMemberService.DeleteTeamMemberAsync(id);
    }
  }
}