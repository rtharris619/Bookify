using Bookify.Domain.Users;

namespace Bookify.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterUserAsync(
        User user,
        string password,
        CancellationToken cancellationToken = default);
}
