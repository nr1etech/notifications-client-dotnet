
namespace NotificationsClientTest;

public class LoggingManageOidcClientCredentialAuthHeaderHandler : ManageClient.OidcClientCredentialAuthHeaderHandler
{
    public LoggingManageOidcClientCredentialAuthHeaderHandler(ManageClient.OidcClientCredentialsConfiguration config, HttpClient httpClient)
        : base(config, httpClient) { }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        Console.WriteLine("Request:");
        Console.WriteLine(request.ToString());
        if (request.Content != null)
        {
            Console.WriteLine(await request.Content.ReadAsStringAsync(cancellationToken));
        }

        Console.WriteLine();

        Console.WriteLine("Response:");
        Console.WriteLine(response.ToString());
        if (response.Content != null)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync(cancellationToken));
        }
        Console.WriteLine();

        return response;
    }

}

public class LoggingMessageOidcClientCredentialAuthHeaderHandler : MessageClient.OidcClientCredentialAuthHeaderHandler
{
    public LoggingMessageOidcClientCredentialAuthHeaderHandler(MessageClient.OidcClientCredentialsConfiguration config, HttpClient httpClient)
        : base(config, httpClient) { }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        Console.WriteLine("Request:");
        Console.WriteLine(request.ToString());
        if (request.Content != null)
        {
            Console.WriteLine(await request.Content.ReadAsStringAsync(cancellationToken));
        }

        Console.WriteLine();

        Console.WriteLine("Response:");
        Console.WriteLine(response.ToString());
        if (response.Content != null)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync(cancellationToken));
        }
        Console.WriteLine();

        return response;
    }

}
