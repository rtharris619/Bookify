using Bookify.Application.Abstractions.Authentication;
using Bookify.Domain.Abstractions;
using Bookify.Infrastructure.Authentication.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Bookify.Infrastructure.Authentication;

internal sealed class JwtService : IJwtService
{
    private static readonly Error AuthenticationFailed = new(
        "Keycloak.AuthenticationFailed", 
        "Failed to acquire access token for authentication");

    private readonly HttpClient _httpClient;
    private readonly KeycloakOptions _keycloakOptions;

    public JwtService(HttpClient httpClient, IOptions<KeycloakOptions> keycloakOptions)
    {
        _httpClient = httpClient;
        _keycloakOptions = keycloakOptions.Value;
    }

    public async Task<Result<string>> GetAccessTokenAsync(
        string email, 
        string password, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var authRequestParams = new KeyValuePair<string, string>[]
            {
                new("client_id", _keycloakOptions.AuthClientId),
                new("client_secret", _keycloakOptions.AuthClientSecret),
                new("scope", "openid email"),
                new("grant_type", "password"),
                new("username", email),
                new("password", password)
            };

            var authRequestContent = new FormUrlEncodedContent(authRequestParams);

            var response = await _httpClient.PostAsync("", authRequestContent, cancellationToken);

            response.EnsureSuccessStatusCode();

            var authToken = await response.Content.ReadFromJsonAsync<AuthorisationToken>();

            if (authToken is null)
            {
                return Result.Failure<string>(AuthenticationFailed);
            }

            return authToken.AccessToken;
        }
        catch (HttpRequestException)
        {
            return Result.Failure<string>(AuthenticationFailed);
        }
    }
}
