using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetByRoleAsync(Guid roleId, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetBySpecializationAsync(Guid specializationId, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetAvailableMechanicsAsync(CancellationToken cancellationToken = default);
} 