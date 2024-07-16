using Microsoft.AspNetCore.Authorization;

namespace Bookify.Infrastructure.Authorisation;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission) : base(permission)
    {
    }
}
