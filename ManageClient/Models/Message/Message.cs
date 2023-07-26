using System.Text.Json;
using System.Text.Json.Serialization;
using ManageClient.Models.Common;

namespace ManageClient.Models.Message;

public record MessageList : ModelList<Message> { }

public record Message
{
    public string MessageID { get; set; }
    public string Type { get; set; }
    public string SentByAccountID { get; set; }
    public string SenderID { get; set; }
    public string MessageStatus { get; set; }
    public string ServiceProvider { get; set; }
    public string ServiceProviderMessageID { get; set; }
    public string ServiceProviderStatus { get; set; }
    public string ServiceProviderStatusMessage { get; set; }
    public string TemplateID { get; set; }
    public string TemplateSlug { get; set; }
    public string TemplateLocale { get; set; }
    public string FallbackOriginalLocale { get; set; }
    public RecipientBase Recipient { get; set; }
    public DateTime DateCreated { get; set; }
    [JsonConverter(typeof(NullableDateTimeJsonConverter))]
    public DateTime? DateCompleted { get; set; }
    public JsonElement? MergeValues { get; set; }
    public Dictionary<string, string>? Metadata { get; set; }
}
