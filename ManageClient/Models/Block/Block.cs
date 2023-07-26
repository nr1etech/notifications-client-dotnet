using ManageClient.Models.Common;

namespace ManageClient.Models.Block;

public record BlockList : ModelList<Block> { }

public record Block
{
    public string BlockID { get; set; }
    public string Recipient { get; set; }
    public string Reason { get; set; }
    public string? Description { get; set; }
    public DateTime DateAdded { get; set; }
}
