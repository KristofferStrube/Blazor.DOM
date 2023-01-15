using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// <see href="https://dom.spec.whatwg.org/#dictdef-eventlisteneroptions">EventListenerOptions browser specs</see>
/// </summary>
public class EventListenerOptions
{
    [JsonPropertyName("capture")]
    public bool Capture { get; set; }
}
