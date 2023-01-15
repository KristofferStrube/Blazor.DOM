namespace KristofferStrube.Blazor.DOM.Events;

public enum EventPhase : ushort
{
    None = 0,
    CapturingPhase = 1,
    AtTarget = 2,
    BubblingPhase = 3
}
