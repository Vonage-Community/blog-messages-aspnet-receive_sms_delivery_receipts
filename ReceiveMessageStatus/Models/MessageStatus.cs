using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReceiveMessageStatus.Models
{
    public class MessageStatus
    {
        [JsonPropertyName("message_uuid")]
        public string? MessageUuid { get; set; }

        [JsonPropertyName("to")]
        public string? To { get; set; }

        [JsonPropertyName("from")]
        public string? From { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("channel")]
        public string? Channel { get; set; }

        [JsonPropertyName("client_ref")]
        public string? ClientRef { get; set; }

        [JsonPropertyName("error")]
        public MessageStatusError? Error { get; set; }

        public override string ToString() =>
            JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
    }

    public class MessageStatusError
    {
        [JsonPropertyName("title")]
        public int Title { get; set; }

        [JsonPropertyName("detail")]
        public string? Detail { get; set; }
    }

}
