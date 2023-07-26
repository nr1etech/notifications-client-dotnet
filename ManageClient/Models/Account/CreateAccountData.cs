namespace ManageClient.Models.Account;

public record CreateAccountData
{
    public string Description { get; set; }
    public string? UserName { get; set; }
    public string? Identity { get; set; }
    public string Role { get; set; }
    public string AccountType { get; set; }
}
