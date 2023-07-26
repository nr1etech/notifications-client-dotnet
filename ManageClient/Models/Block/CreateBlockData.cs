namespace ManageClient.Models.Block;

public record CreateBlockData
{
    public string Recipient { get; set; }
    public string Reason { get; set; }
    public string Description { get; set; }
}
