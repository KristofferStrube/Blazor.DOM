using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// <see href="https://dom.spec.whatwg.org/#dictdef-addeventlisteneroptions">AddEventListenerOptions browser specs</see>
/// </summary>
public class AddEventListenerOptions : EventListenerOptions
{
    [JsonPropertyName("passive")]
    public bool Passive { get; set; }

    [JsonPropertyName("once")]
    public bool Once { get; set; }
}
