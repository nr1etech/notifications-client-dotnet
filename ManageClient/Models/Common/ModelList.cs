namespace ManageClient.Models.Common;

public record ModelList<T>
{
    public List<T> Results { get; set; }
    public string nextPage { get; set; }
}
