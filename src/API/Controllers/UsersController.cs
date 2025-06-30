using Application.Common.DTOs.Users;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public UsersController(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Users.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id, cancellationToken);
        if (user == null) return NotFound();
        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpGet("mechanics")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAvailableMechanics(CancellationToken cancellationToken)
    {
        var mechanics = await _unitOfWork.UsersExtended.GetAvailableMechanicsAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<UserDto>>(mechanics));
    }

    [HttpGet("by-role/{roleId}")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByRole(int roleId, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UsersExtended.GetByRoleAsync(roleId, cancellationToken);
        return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
    }

    [HttpGet("by-specialization/{specializationId}")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersBySpecialization(int specializationId, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UsersExtended.GetBySpecializationAsync(specializationId, cancellationToken);
        return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createUserDto, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Username = createUserDto.Username,
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            Email = createUserDto.Email,
            Password = _passwordHasher.HashPassword(createUserDto.Password),
            Phone = createUserDto.Phone
        };

        await _unitOfWork.Users.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Add roles
        foreach (var roleId in createUserDto.RoleIds)
        {
            var userRole = new UserRole { UserId = user.Id, RoleId = roleId };
            await _unitOfWork.Repository<UserRole>().AddAsync(userRole, cancellationToken);
        }

        // Add specializations
        foreach (var specializationId in createUserDto.SpecializationIds)
        {
            var userSpecialization = new UserSpecialization { UserId = user.Id, SpecializationId = specializationId };
            await _unitOfWork.Repository<UserSpecialization>().AddAsync(userSpecialization, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var createdUser = await _unitOfWork.Users.GetByIdAsync(user.Id, cancellationToken);
        return Ok(_mapper.Map<UserDto>(createdUser));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id, cancellationToken);
        if (user == null) return NotFound();

        user.Username = updateUserDto.Username ?? user.Username;
        user.FirstName = updateUserDto.FirstName ?? user.FirstName;
        user.LastName = updateUserDto.LastName ?? user.LastName;
        user.Email = updateUserDto.Email ?? user.Email;
        if (!string.IsNullOrEmpty(updateUserDto.Password))
        {
            user.Password = _passwordHasher.HashPassword(updateUserDto.Password);
        }
        user.Phone = updateUserDto.Phone ?? user.Phone;

        // Update roles
        var existingRoles = await _unitOfWork.Repository<UserRole>()
            .FindAsync(ur => ur.UserId == id, cancellationToken);
        
        foreach (var role in existingRoles)
        {
            await _unitOfWork.Repository<UserRole>().DeleteAsync(role, cancellationToken);
        }

        if (updateUserDto.RoleIds?.Any() == true)
        {
            foreach (var roleId in updateUserDto.RoleIds)
            {
                var userRole = new UserRole { UserId = id, RoleId = roleId };
                await _unitOfWork.Repository<UserRole>().AddAsync(userRole, cancellationToken);
            }
        }

        // Update specializations
        var existingSpecializations = await _unitOfWork.Repository<UserSpecialization>()
            .FindAsync(us => us.UserId == id, cancellationToken);
        
        foreach (var specialization in existingSpecializations)
        {
            await _unitOfWork.Repository<UserSpecialization>().DeleteAsync(specialization, cancellationToken);
        }

        if (updateUserDto.SpecializationIds?.Any() == true)
        {
            foreach (var specializationId in updateUserDto.SpecializationIds)
            {
                var userSpecialization = new UserSpecialization { UserId = id, SpecializationId = specializationId };
                await _unitOfWork.Repository<UserSpecialization>().AddAsync(userSpecialization, cancellationToken);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var updatedUser = await _unitOfWork.Users.GetByIdAsync(id, cancellationToken);
        return Ok(_mapper.Map<UserDto>(updatedUser));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id, cancellationToken);
        if (user == null) return NotFound();

        await _unitOfWork.Users.DeleteAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
} 