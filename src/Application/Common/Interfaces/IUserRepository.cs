using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetByRoleAsync(int roleId, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetBySpecializationAsync(int specializationId, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetAvailableMechanicsAsync(CancellationToken cancellationToken = default);
} 