using NotificationsClientTest;

Console.WriteLine("Starting tests...");

var clientID = "netradius-sanity-test";
var clientSecret = Environment.GetEnvironmentVariable("NR1ET_NOTI_CLIENT_SECRET");
var scope = "api";
var tokenUrl = "https://notifications-dev.authsure.io/connect/token";
var baseApiUrl = "https://api.jroberts.dev.notifications.netradius.com";

if (string.IsNullOrEmpty(clientSecret))
{
    throw new ApplicationException("Client secrent not found in NR1ET_NOTI_CLIENT_SECRET environment variable");
}

await ManageTest.Test(
    clientID,
    clientSecret,
    scope,
    tokenUrl,
    baseApiUrl,
    enableRequestLogging: false
);

Console.WriteLine("Test complete");
