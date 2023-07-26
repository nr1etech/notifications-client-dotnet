namespace ManageClient.Models.Template;

public record UpdateTemplateData
{
    public string? Slug { get; set; }
    public string? Locale { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public string? Stage { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
}
