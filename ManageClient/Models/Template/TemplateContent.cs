namespace ManageClient.Models.Template;

public record TemplateContent
{
    public string Stage { get; set; }
    public string? Subject { get; set; }
    public string Body { get; set; }
    public DateTime DateLastModified { get; set; }
}
