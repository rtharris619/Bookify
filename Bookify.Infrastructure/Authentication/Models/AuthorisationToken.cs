using System.Text.Json.Serialization;

namespace Bookify.Infrastructure.Authentication.Models;

public sealed class AuthorisationToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; } = string.Empty;
}
