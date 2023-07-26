using System.Text.Json.Serialization;
using System.Runtime.CompilerServices;

namespace ManageClient.Models.Info;

public record AccountInfo {
    public string OrganizationID { get; set; }
    public string OrganizationName { get; set; }
    public string OrganizationSlug { get; set; }
    public string AccountID { get; set; }
    public string AccountDescription { get; set; }
    public string AccountRole { get; set; }
}


public record AuthenticatedAccountInfo
{
    public List<AccountInfo> Accounts { get; set; }
}

