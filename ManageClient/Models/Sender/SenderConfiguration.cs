using System.Text.Json.Serialization;

namespace ManageClient.Models.Sender;

[JsonDerivedType(typeof(TestSenderConfiguration))]
[JsonDerivedType(typeof(TwilioSenderConfiguration))]
[JsonDerivedType(typeof(SendgridSenderConfiguration))]
[JsonDerivedType(typeof(AmazonSESSenderConfiguration))]
public record SenderConfigurationBase { }

public record TestSenderConfiguration : SenderConfigurationBase { }

public record TwilioSenderConfiguration : SenderConfigurationBase
{
    public string ApiKeySID { get; set; }
    public string Secret { get; set; }
    public string AccountSID { get; set; }
    public string MessagingServiceSID { get; set; }
}

public record SenderConfigurationEmailFrom
{
    public string Name { get; set; }
    public string Address { get; set; }
}

public record SendgridSenderConfiguration : SenderConfigurationBase
{
    public string ApiKeyName { get; set; }
    public string ApiKeyID { get; set; }
    public string ApiKey { get; set; }
    public SenderConfigurationEmailFrom From { get; set; }
}

public record AmazonSESSenderConfiguration : SenderConfigurationBase
{
    public string Region { get; set; }
    public string AccessKeyID { get; set; }
    public string SecretAccessKey { get; set; }
    public string? ConfigurationSetName { get; set; }
    public SenderConfigurationEmailFrom From { get; set; }
}
