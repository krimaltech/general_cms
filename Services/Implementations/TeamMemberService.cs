using Backend.Data;
using Backend.Models.MainModels;
using Backend.Models.RequestModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Backend.Services.Implementations
{
  public class TeamMemberService : ITeamMemberService
  {

    private readonly ProjectDbContext _context;
    private readonly ILogger<TeamMemberService> _logger;

    public TeamMemberService(ProjectDbContext context, ILogger<TeamMemberService> logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task<List<TeamMember>> GetTeamMembersAsync()
    {
      return await _context.TeamMembers.ToListAsync();
    }

    public async Task<ActionResult<TeamMember>> GetSingleTeamMemberAsync(int id)
    {
      TeamMember? member = await _context.TeamMembers.FirstOrDefaultAsync(x => x.ID == id);
      if (member == null)
      {
        _logger.LogWarning("Member not found.");
        return new NotFoundObjectResult("Member not found");
      }
      return new OkObjectResult(member);
    }
    public async Task<ActionResult> AddTeamMemberAsync(TeamMemberRequest request)
    {
      using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        TeamMember member = new TeamMember
        {
          Name = request.Name,
          Position = request.Position,
          SuperiorName = request.SuperiorName,
          SuperiorPosition = request.SuperiorPosition,
          Department = request.Department,
          ImageUrl = request.ImageUrl
        };

        await _context.TeamMembers.AddAsync(member);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return new OkObjectResult("Team Member is added successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while trying to add team member");
        return new StatusCodeResult(500);
      }
    }

    public async Task<ActionResult> UpdateTeamMemberAsync(int id, TeamMemberUpdateRequest request)
    {
      TeamMember? member = await _context.TeamMembers.FirstOrDefaultAsync(x => x.ID == id);
      if (member == null)
      {
        _logger.LogWarning("Member not found");
        return new NotFoundObjectResult("Member not found");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        if (request.Name != null) member.Name = request.Name;
        if (request.Position != null) member.Position = request.Position;
        if (request.SuperiorName != null) member.SuperiorName = request.SuperiorName;
        if (request.SuperiorPosition != null) member.SuperiorPosition = request.SuperiorPosition;
        if (request.Department != null) member.Department = request.Department;
        if (request.ImageUrl != null) member.ImageUrl = request.ImageUrl;

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Team member updated successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogWarning(ex, "An error occured while updating the team member");
        return new StatusCodeResult(500);
      }
    }
    public async Task<ActionResult> DeleteTeamMemberAsync(int id)
    {

      TeamMember? member = await _context.TeamMembers.FirstOrDefaultAsync(x => x.ID == id);

      if (member == null)
      {
        _logger.LogWarning("Team Member not found");
        return new NotFoundObjectResult("Team member not found");
      }

      using var transaction = await _context.Database.BeginTransactionAsync();

      try
      {
        _context.TeamMembers.Remove(member);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new OkObjectResult("Team member removed successfully");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        _logger.LogError(ex, "An Error occured while trying to remove team member");
        return new StatusCodeResult(500);
      }
    }

  }
}