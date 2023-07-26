using System.Text.Json;
using System.Text.Json.Serialization;
using MessageClient.Models;
using Refit;

namespace MessageClient;

public class NotificationsMessageClient
{
    public string OrganizationID { get; set; }
    private readonly INotificationsMessageApi notificationsMessageApi;

    public NotificationsMessageClient(HttpClient httpClient, string? organizationId = null)
    {
        this.OrganizationID = organizationId ?? "";

        this.notificationsMessageApi = RestService.For<INotificationsMessageApi>(httpClient, new RefitSettings()
        {
            ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                }
            ),

        });
    }

    public async Task<SendResponse> SendEmailAsync(EmailMessage message, string? organizationID = null)
    {
        return await this.notificationsMessageApi.SendEmail(message, organizationID ?? this.OrganizationID);
    }

    public async Task<SendResponse> SendSmsAsync(SmsMessage message, string? organizationID = null)
    {
        return await this.notificationsMessageApi.SendSms(message, organizationID ?? this.OrganizationID);
    }
}
