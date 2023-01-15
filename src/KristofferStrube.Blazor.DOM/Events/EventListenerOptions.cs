using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.DOM;

public class EventListenerOptions
{
    [JsonPropertyName("capture")]
    public bool Capture { get; set; }
}
