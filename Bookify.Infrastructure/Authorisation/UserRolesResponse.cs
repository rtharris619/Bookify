using Bookify.Domain.Users;

namespace Bookify.Infrastructure.Authorisation;

public sealed class UserRolesResponse
{
    public Guid Id { get; init; }
    public List<Role> Roles { get; init; } = [];
}
