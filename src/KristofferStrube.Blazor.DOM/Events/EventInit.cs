using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// Allows to set whether an <see cref="Event"/> bubbles and/or is cancelable.
/// </summary>
/// <remarks><see href="https://dom.spec.whatwg.org/#dictdef-eventinit">See the API definition here</see></remarks>
public class EventInit
{
    /// <summary>
    /// <see langword="true"/> if the <see cref="Event"/> should go through its targets's ancestors in reverse tree order; otherwise <see langword="false"/>
    /// </summary>
    [JsonPropertyName("bubbles")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Bubbles { get; set; }

    /// <summary>
    /// <see langword="true"/> if the <see cref="Event"/> should be able to be canceled; otherwise <see langword="false"/>
    /// </summary>
    [JsonPropertyName("cancelable")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Cancelable { get; set; }

    /// <summary>
    /// <see langword="true"/> if the <see cref="Event"/> invokes listeners that are outside a <see href="https://dom.spec.whatwg.org/#shadowroot">ShadowRoot</see> node; otherwise <see langword="false"/>
    /// </summary>
    [JsonPropertyName("composed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Composed { get; set; }
}
