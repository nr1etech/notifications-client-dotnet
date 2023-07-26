namespace ManageClient.Models.Organization;

public record CreateOrganizationData
{
    public string OrganizationName { get; set; }
    public string Slug { get; set; }
}
