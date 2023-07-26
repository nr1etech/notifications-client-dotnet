using NotificationsClientTest;

Console.WriteLine("Starting tests...");

var clientID = Environment.GetEnvironmentVariable("NR1ET_NOTI_CLIENT_ID");
var clientSecret = Environment.GetEnvironmentVariable("NR1ET_NOTI_CLIENT_SECRET");
var tokenUrl = Environment.GetEnvironmentVariable("NR1ET_NOTI_AUTH_TOKEN_URL");
var baseApiUrl = Environment.GetEnvironmentVariable("NR1ET_NOTI_API_URL");

var scope = "api";

if (string.IsNullOrEmpty(clientID))
{
    throw new ApplicationException("Client ID not found in NR1ET_NOTI_CLIENT_ID environment variable");
}
if (string.IsNullOrEmpty(clientSecret))
{
    throw new ApplicationException("Client secret not found in NR1ET_NOTI_CLIENT_SECRET environment variable");
}
if (string.IsNullOrEmpty(tokenUrl))
{
    throw new ApplicationException("Auth Token URL not found in NR1ET_NOTI_AUTH_TOKEN_URL environment variable");
}
if (string.IsNullOrEmpty(baseApiUrl))
{
    throw new ApplicationException("API Base URL not found in NR1ET_NOTI_API_URL environment variable");
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
