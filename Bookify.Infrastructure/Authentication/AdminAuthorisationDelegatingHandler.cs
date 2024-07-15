using Bookify.Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Bookify.Infrastructure.Authentication;

internal sealed class AdminAuthorisationDelegatingHandler : DelegatingHandler
{
    private readonly KeycloakOptions _keycloakOptions;

    public AdminAuthorisationDelegatingHandler(IOptions<KeycloakOptions> keycloakOptions)
    {
        _keycloakOptions = keycloakOptions.Value;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authorisationToken = await GetAuthorisationToken(cancellationToken);

        request.Headers.Authorization = new AuthenticationHeaderValue(
            JwtBearerDefaults.AuthenticationScheme, 
            authorisationToken.AccessToken);

        var httpResponseMessage = await base.SendAsync(request, cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();

        return httpResponseMessage;
    }

    private async Task<AuthorisationToken> GetAuthorisationToken(CancellationToken cancellationToken)
    {
        var authRequestParams = new KeyValuePair<string, string>[]
        {
            new("client_id", _keycloakOptions.AdminClientId),
            new("client_secret", _keycloakOptions.AdminClientSecret),
            new("scope", "openid email"),
            new("grant_type", "client_credentials")
        };

        var authRequestContent = new FormUrlEncodedContent(authRequestParams);

        using var authRequest = new HttpRequestMessage(HttpMethod.Post, new Uri(_keycloakOptions.TokenUrl))
        {
            Content = authRequestContent
        };

        try
        {
            var authResponse = await base.SendAsync(authRequest, cancellationToken);

            authResponse.EnsureSuccessStatusCode();

            return await authResponse.Content.ReadFromJsonAsync<AuthorisationToken>(cancellationToken) ?? throw new ApplicationException();
        }
        catch (Exception ex)
        {
            throw new ApplicationException();
        }
    }
}
