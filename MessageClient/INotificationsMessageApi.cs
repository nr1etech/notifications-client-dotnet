using MessageClient.Models;
using Refit;

namespace MessageClient;

[Headers("Content-Type: application/json")]
interface INotificationsMessageApi
{
    [Headers("Accept: application/vnd.notification.create-email.v1+json")]
    [Post("/message/{organizationID}/email")]
    public Task<SendResponse> SendEmail([Body] EmailMessage message, string organizationID);

    [Headers("Accept: application/vnd.notification.create-sms.v1+json")]
    [Post("/message/{organizationID}/sms")]
    public Task<SendResponse> SendSms([Body] SmsMessage message, string organizationID);
}
