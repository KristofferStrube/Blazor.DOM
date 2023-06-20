namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// The phase that the event is in.
/// </summary>
/// <remarks><see href="https://dom.spec.whatwg.org/#dom-event-eventphase">See the API definition here</see></remarks>
public enum EventPhase : ushort
{
    /// <summary>
    /// <see cref="Event"/>s not currently dispatched are in this phase.
    /// </summary>
    None = 0,

    /// <summary>
    /// When an <see cref="Event"/> is dispatched to an object that participates in a tree it will be in this phase before it reaches its target.
    /// </summary>
    CapturingPhase = 1,

    /// <summary>
    /// When an <see cref="Event"/> is dispatched it will be in this phase on its target.
    /// </summary>
    AtTarget = 2,

    /// <summary>
    /// When an <see cref="Event"/> is dispatched to an object that participates in a tree it will be in this phase after it reaches its target.
    /// </summary>
    BubblingPhase = 3
}
