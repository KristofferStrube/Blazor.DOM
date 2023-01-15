using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.DOM;

public class AddEventListenerOptions : EventListenerOptions
{
    [JsonPropertyName("passive")]
    public bool Passive { get; set; }

    [JsonPropertyName("once")]
    public bool Once { get; set; }
}
