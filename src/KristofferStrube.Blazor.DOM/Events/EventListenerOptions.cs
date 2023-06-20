using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// Options for when an <see cref="EventListener{TEvent}"/> is removed from an <see cref="EventTarget"/>s listener list.
/// </summary>
/// <remarks><see href="https://dom.spec.whatwg.org/#dictdef-eventlisteneroptions">See the API definition here</see></remarks>
public class EventListenerOptions
{
    /// <summary>
    /// When set to <see langword="true"/> prevents callback from being invoked when the <see cref="Event.GetEventPhaseAsync"/> method returns <c>None</c>. When false, callback will not be invoked when the <see cref="Event.GetEventPhaseAsync"/> method returns <c>CapturingPhase</c>. Either way, callback will be invoked if the <see cref="Event.GetEventPhaseAsync"/> method returns <c>AtTarget</c>.
    /// </summary>
    [JsonPropertyName("capture")]
    public bool Capture { get; set; }
}
