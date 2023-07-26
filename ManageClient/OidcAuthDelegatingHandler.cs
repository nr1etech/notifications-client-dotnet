using System.Net.Http.Headers;
using IdentityModel.Client;

namespace ManageClient;

public record OidcClientCredentialsConfiguration
{
    public string ClientID { get; set; }
    public string ClientSecret { get; set; }
    public string Scope { get; set; }
    public string TokenUrl { get; set; }
}

// Custom delegating handler for adding Auth headers to outbound requests
public class OidcClientCredentialAuthHeaderHandler : DelegatingHandler
{
    private readonly HttpClient httpClient;
    private readonly OidcClientCredentialsConfiguration configuration;
    private TokenResponse? token;

    public OidcClientCredentialAuthHeaderHandler(OidcClientCredentialsConfiguration config, HttpClient authHttpClient)
    {
        this.httpClient = authHttpClient;

        this.token = null;
        this.configuration = config;
        InnerHandler = new HttpClientHandler();
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await this.GetToken(cancellationToken).ConfigureAwait(false);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }

    private async Task<string> GetToken(CancellationToken cancellationToken)
    {
        if (this.token != null && DateTimeOffset.FromUnixTimeSeconds(this.token.ExpiresIn) <= DateTime.UtcNow)
        {
            return this.token.AccessToken;
        }

        var response = await this.httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
        {
            Address = this.configuration.TokenUrl,
            ClientId = this.configuration.ClientID,
            ClientSecret = this.configuration.ClientSecret,
            Scope = this.configuration.Scope,
        });

        if (response == null || response.IsError)
        {
            throw new Exception("Token Error");
        }

        this.token = response;


        return this.token.AccessToken;
    }
}
