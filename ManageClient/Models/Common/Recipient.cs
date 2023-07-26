using System.Text.Json.Serialization;

namespace ManageClient.Models.Common;

[JsonDerivedType(typeof(SmsRecipient))]
[JsonDerivedType(typeof(EmailRecipient))]
public record RecipientBase { }

public record SmsRecipient : RecipientBase
{
    public string Phone { get; set; }
}

public record EmailRecipient : RecipientBase
{
    public string Name { get; set; }
    public string Email { get; set; }
}
