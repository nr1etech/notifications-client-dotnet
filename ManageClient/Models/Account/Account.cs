using ManageClient.Models.Common;

namespace ManageClient.Models.Account;

public record AccountList : ModelList<Account> { }
public record Account
{
    public string AccountID { get; set; }
    public string Description { get; set; }
    public string OrganizationName { get; set; }
    public string OrganizationSlug { get; set; }
    public string Status { get; set; }
    public string? UserName { get; set; }
    public string? Identity { get; set; }
    public string Role { get; set; }
    public string AccountType { get; set; }
}

public record AccountSecret : Account
{
    public string Secret { get; set; }

}
