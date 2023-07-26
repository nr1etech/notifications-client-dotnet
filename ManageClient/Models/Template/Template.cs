using ManageClient.Models.Common;

namespace ManageClient.Models.Template;

public record TemplateList : ModelList<Template> { }

public record Template
{
    public string TemplateID { get; set; }
    public string Slug { get; set; }
    public string Locale { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public List<TemplateContent> Content { get; set; }
}
