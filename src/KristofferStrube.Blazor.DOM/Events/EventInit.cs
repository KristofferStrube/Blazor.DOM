using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// <see href="https://dom.spec.whatwg.org/#dictdef-eventinit">EventInit browser specs</see>
/// </summary>
public class EventInit
{
    [JsonPropertyName("bubbles")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Bubbles { get; set; }

    [JsonPropertyName("cancelable")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Cancelable { get; set; }

    [JsonPropertyName("composed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Composed { get; set; }
}
