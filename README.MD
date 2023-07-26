# NetRadius Notifications .Net Client

A .Net client for NetRadius Notifications API. Provides a simple typed API and OIDC authentication logic.

## Usage Example

### Messaging API Client

```csharp
using MessageClient;

// Setup the authorization configuration
var authConfig = new OidcClientCredentialsConfiguration()
{
    ClientID = "my client id",
    ClientSecret = "my client secret",
    Scope = "api",
    TokenUrl = "https://myorg.authsure.io/connect/token"
};

// Create the request handler for the authorization. Also provide an HTTP Client to be used by the authorization request.
var authHandler = new OidcClientCredentialAuthHeaderHandler(authConfig, new HttpClient());

// Create an HTTP client for the API calls. Provide the authorization handler and API url.
var httpClient = new HttpClient(authHandler)
{
    BaseAddress = new Uri("https://notifications.netradius.com")
};

// Instantiate the client
var client = new NotificationsMessageClient(httpClient, "my-organization-id");

// Send an Email
var email = new EmailMessage() {
    TemplateSlug = "my-template-slug",
    TemplateLocale = "en-US",
    Recipient = new EmailRecipient() {
        Name = "Joe Example",
        Email = "joe@example.com"
    },
    MergeValues = new MergeValues() {
        Thing1 = "Thing One",
        Thing2 = "Thing Two",
    },
    Metadata = new Dictionary<string, string>() { { "sentBy", "Example" } },
    SenderID = null,
};

var messageResult = await client.SendEmailAsync(email);

Console.WriteLine($"Message ID: {messageResult.MessageID}");
```


### Management API Client

```csharp
using ManageClient;

// Setup the authorization configuration
var authConfig = new OidcClientCredentialsConfiguration()
{
    ClientID = "my client id",
    ClientSecret = "my client secret",
    Scope = "api",
    TokenUrl = "https://myorg.authsure.io/connect/token"
};

// Create the request handler for the authorization. Also provide an HTTP Client to be used by the authorization request.
var authHandler = new OidcClientCredentialAuthHeaderHandler(manageAuthConfig, new HttpClient());

// Create an HTTP client for the API calls. Provide the authorization handler and API url.
var httpClient = new HttpClient(authHandler)
{
    BaseAddress = new Uri("https://notifications.netradius.com")
};

// Instantiate the client
var client = new NotificationsManageClient(httpClient);
client.OrganizationID = "my-organization-id";

// Make API calls
var info = await client.GetInfoAsync();
```
