namespace ManageClient.Models.Sender;

public record CreateSenderData
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public int Priority { get; set; }
    public string ServiceProvider { get; set; }
    public SenderConfigurationBase SenderConfiguration { get; set; }
}
