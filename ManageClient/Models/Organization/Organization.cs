using ManageClient.Models.Common;

namespace ManageClient.Models.Organization;

public record OrganizationList : ModelList<Organization> { }

public record Organization
{
    public string OrganizationID { get; set; }
    public string OrganizationName { get; set; }
    public string Slug { get; set; }
    public string Status { get; set; }
}
