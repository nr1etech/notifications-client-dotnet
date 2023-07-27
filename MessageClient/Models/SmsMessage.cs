namespace MessageClient.Models;

public record SmsMessage
{
    public string TemplateSlug { get; set; }
    public string TemplateLocale { get; set; }
    public SmsRecipient Recipient { get; set; }
    public object? MergeValues { get; set; }
    public Dictionary<string, string>? Metadata { get; set; }
    public string? SenderID { get; set; }
    public bool? SendDraft { get; set; }
}

public record SmsRecipient
{
    public string Phone { get; set; }
}
