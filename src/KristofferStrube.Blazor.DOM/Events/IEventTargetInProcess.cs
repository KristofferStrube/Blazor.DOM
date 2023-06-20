using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// An interface that defines the method and members accessible in <see cref="EventTargetInProcess"/> or classes alike it.
/// </summary>
public interface IEventTargetInProcess
{
    /// <inheritdoc cref="IJSWrapper.JSReference" />
    IJSInProcessObjectReference JSReference { get; }

    /// <inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)"/>
    void AddEventListener<TInProcessEvent, TEvent>(string type, EventListenerInProcess<TInProcessEvent, TEvent>? callback, AddEventListenerOptions? options = null)
        where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>;

    /// <inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(EventListener{TEvent}?, AddEventListenerOptions?)"/>
    void AddEventListener<TInProcessEvent, TEvent>(EventListenerInProcess<TInProcessEvent, TEvent>? callback, AddEventListenerOptions? options = null)
        where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>;

    /// <inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)"/>
    void RemoveEventListener<TInProcessEvent, TEvent>(string type, EventListenerInProcess<TInProcessEvent, TEvent>? callback, EventListenerOptions? options = null)
        where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>;

    /// <inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(EventListener{TEvent}?, EventListenerOptions?)"/>
    void RemoveEventListener<TInProcessEvent, TEvent>(EventListenerInProcess<TInProcessEvent, TEvent>? callback, EventListenerOptions? options = null)
        where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>;

    /// <inheritdoc cref="EventTarget.DispatchEventAsync(Event)"/>
    bool DispatchEvent(Event eventInstance);
}