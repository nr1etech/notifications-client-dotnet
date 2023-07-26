namespace ManageClient.Models;

public static class MessageType
{
    public const string Email = "email";
    public const string Sms = "sms";
}

public static class MessageStatus
{
    public const string Created = "created";
    public const string Pending = "pending";
    public const string Sending = "sending";
    public const string Sent = "sent";
    public const string Success = "success";
    public const string Failure = "failure";
}

public static class OrganizationStatus
{
    public const string Active = "active";
    public const string Inactive = "inactive";
}

public static class AccountRole
{
    public const string GlobalAdmin = "global-admin";
    public const string Management = "management";
    public const string Messaging = "messaging";
    public const string MessagingTest = "messaging-test";
}

public static class AccountType
{
    public const string User = "user";
    public const string Client = "client";
}

public static class AccountStatus
{
    public const string Active = "active";
    public const string Inactive = "inactive";
}

public static class TemplateStatus
{
    public const string Active = "active";
    public const string Inactive = "inactive";
}

public static class TemplateStage
{
    public const string Published = "published";
    public const string Draft = "draft";
}

public static class SenderStatus
{
    public const string Active = "active";
    public const string Inactive = "inactive";
}

public static class ServiceProvider
{
    public const string Twilio = "twilio";
    public const string SendGrid = "sendgrid";
    public const string AwsSes = "aws-ses";
    public const string EmailIntegrationTest = "email-integration-test";
    public const string SmsIntegrationTest = "sms-integration-test";
}

public static class BlockReasonType
{
    public const string Spam = "spam";
    public const string Bounce = "bounce";
    public const string Blocked = "blocked";
    public const string OptOut = "optout";
    public const string Other = "other";
    public const string Manual = "manual";
}
