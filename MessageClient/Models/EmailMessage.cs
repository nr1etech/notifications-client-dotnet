namespace MessageClient.Models;

public record EmailMessage
{
    public string TemplateSlug { get; set; }
    public string TemplateLocale { get; set; }
    public EmailRecipient Recipient { get; set; }
    public object? MergeValues { get; set; }
    public Dictionary<string, string>? Metadata { get; set; }
    public string? SenderID { get; set; }
}

public record EmailRecipient
{
    public string Name { get; set; }
    public string Email { get; set; }
}
