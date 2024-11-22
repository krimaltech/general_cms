using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.Interfaces
{
  public interface ITeamMemberService
  {
    public Task<List<TeamMember>> GetTeamMembersAsync();
    public Task<ActionResult<TeamMember>> GetSingleTeamMemberAsync(int id);
    public Task<ActionResult> AddTeamMemberAsync(TeamMemberRequest request);

    public Task<ActionResult> UpdateTeamMemberAsync(int id, TeamMemberUpdateRequest request);
    public Task<ActionResult> DeleteTeamMemberAsync(int id);
  }
}