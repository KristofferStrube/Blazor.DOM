using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// Options for when an <see cref="EventListener{TEvent}"/> subscribes to an <see cref="Event"/> with a callback.
/// </summary>
/// <remarks><see href="https://dom.spec.whatwg.org/#dictdef-addeventlisteneroptions">See the API definition here</see></remarks>
public class AddEventListenerOptions : EventListenerOptions
{
    /// <summary>
    /// When set to <see langword="true"/> it indicates that the callback will not cancel the event by invoking <see cref="Event.PreventDefaultAsync"/>. This is used to enable performance optimizations.
    /// </summary>
    [JsonPropertyName("passive")]
    public bool Passive { get; set; }

    /// <summary>
    /// When set to <see langword="true"/> it indicates that the callback will only be invoked once after which the event listener will be removed.
    /// </summary>
    [JsonPropertyName("once")]
    public bool Once { get; set; }
}
