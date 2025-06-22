using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .Include(u => u.UserSpecializations)
                .ThenInclude(us => us.Specialization)
            .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetByRoleAsync(int roleId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .Where(u => u.UserRoles.Any(ur => ur.RoleId == roleId) && !u.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> GetBySpecializationAsync(int specializationId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(u => u.UserSpecializations)
                .ThenInclude(us => us.Specialization)
            .Where(u => u.UserSpecializations.Any(us => us.SpecializationId == specializationId) && !u.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAvailableMechanicsAsync(CancellationToken cancellationToken = default)
    {
        var mechanicRoleId = await _context.Set<Role>()
            .Where(r => r.Name == "Mechanic")
            .Select(r => r.Id)
            .FirstOrDefaultAsync(cancellationToken);

        return await _dbSet
            .Include(u => u.UserRoles)
            .Include(u => u.UserSpecializations)
                .ThenInclude(us => us.Specialization)
            .Where(u => u.UserRoles.Any(ur => ur.RoleId == mechanicRoleId) && !u.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public override async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .Include(u => u.UserSpecializations)
                .ThenInclude(us => us.Specialization)
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted, cancellationToken);
    }
} 