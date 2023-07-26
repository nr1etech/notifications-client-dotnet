using ManageClient.Models.Common;

namespace ManageClient.Models.Message;

public record CreateSmsMessage
{
    public string TemplateSlug { get; set; }
    public string TemplateLocale { get; set; }
    public SmsRecipient Recipient { get; set; }
    public object? MergeValues { get; set; }
    public Dictionary<string, string>? Metadata { get; set; }
    public string? SenderID { get; set; }
}
