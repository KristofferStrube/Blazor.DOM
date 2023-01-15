using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// <see href="https://dom.spec.whatwg.org/#dictdef-eventinit">EventInit browser specs</see>
/// </summary>
public class EventInit
{
    [JsonPropertyName("bubbles")]
    public bool Bubbles { get; set; }

    [JsonPropertyName("cancelable")]
    public bool Cancelable { get; set; }

    [JsonPropertyName("composed")]
    public bool Composed { get; set; }
}
