using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public RolesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> GetRoles(CancellationToken cancellationToken)
    {
        var roles = await _unitOfWork.Repository<Role>().GetAllAsync(cancellationToken);
        return Ok(roles);
    }

    [HttpPost]
    public async Task<ActionResult<Role>> CreateRole(CreateRoleDto createRoleDto, CancellationToken cancellationToken)
    {
        var role = new Role
        {
            Name = createRoleDto.Name,
            Description = createRoleDto.Description,
            CreatedBy = "System",
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Repository<Role>().AddAsync(role, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(role);
    }
}

public class CreateRoleDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
} 