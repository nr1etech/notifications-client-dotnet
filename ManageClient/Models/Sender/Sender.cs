using ManageClient.Models.Common;

namespace ManageClient.Models.Sender;

public record SenderList : ModelList<Sender> { }

public record Sender
{
    public string SenderID { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public int Priority { get; set; }
    public string ServiceProvider { get; set; }
    public SenderConfigurationBase SenderConfiguration { get; set; }
    public string WebhookKey { get; set; }
}
