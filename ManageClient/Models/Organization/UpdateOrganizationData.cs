namespace ManageClient.Models.Organization;

public record UpdateOrganizationData
{
    public string? OrganizationName { get; set; }
    public string? Status { get; set; }
}
