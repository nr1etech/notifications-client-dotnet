namespace ManageClient.Models.Account;

public record UpdateAccountData
{
    public string? UserName { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public string? Identity { get; set; }
    public string? Role { get; set; }
}
