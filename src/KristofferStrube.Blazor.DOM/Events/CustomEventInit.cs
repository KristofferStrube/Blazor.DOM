using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// Allows to set whether an <see cref="CustomEvent"/> bubbles and/or is cancelable and to set extra detail on the <see cref="CustomEvent"/>.
/// </summary>
/// <remarks><see href="https://dom.spec.whatwg.org/#dictdef-customeventinit">See the API definition here</see></remarks>
public class CustomEventInit : EventInit
{
    /// <summary>
    /// Any custom data that a <see cref="CustomEvent"/> should contain. Typically used for synthetic events.
    /// </summary>
    [JsonPropertyName("detail")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IJSObjectReference? Detail { get; set; }
}
