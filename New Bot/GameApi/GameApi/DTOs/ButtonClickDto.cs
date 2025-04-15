using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class ButtonClickDto
{
    [JsonPropertyName("id")]
    public string EventId { get; set; }

    [JsonPropertyName("buttonName")]
    public string ButtonName { get; set; }

    [JsonPropertyName("clickTime")]
    public string ClickTimeString { get; set; }

    [JsonIgnore]
    public DateTime ClickTime => DateTime.Parse(ClickTimeString);

    [JsonPropertyName("additionalData")]
    public Dictionary<string, string> AdditionalData { get; set; } = new Dictionary<string, string>();

    public ButtonClickRequest ToRequest()
    {
        return new ButtonClickRequest(
            eventId: EventId,
            buttonName: ButtonName,
            clickTime: ClickTime,
            metadata: AdditionalData
        );
    }
}