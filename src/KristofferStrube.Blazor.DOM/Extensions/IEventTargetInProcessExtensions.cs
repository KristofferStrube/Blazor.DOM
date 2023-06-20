using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM.Extensions;

/// <summary>
/// Extension methods that hold the implementations for <see cref="EventTargetInProcess"/> so that they can be used even though a consumer of the libary is not able to extend that class.
/// </summary>
public static class IEventTargetInProcessExtensions
{
    /// <inheritdoc cref="IEventTargetInProcess.AddEventListener{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)"/>
    public static void AddEventListener<TEvent>(this IEventTargetInProcess eventTarget, IJSInProcessObjectReference inProcessHelper, string type, EventListener<TEvent>? callback, AddEventListenerOptions? options = null)
        where TEvent : Event, IJSCreatable<TEvent>
    {
        inProcessHelper.InvokeVoid("addEventListener", eventTarget.JSReference, type, callback?.JSReference, options);
    }

    /// <inheritdoc cref="IEventTargetInProcess.AddEventListener{TEvent}(EventListener{TEvent}?, AddEventListenerOptions?)"/>
    public static void AddEventListener<TEvent>(this IEventTargetInProcess eventTarget, IJSInProcessObjectReference inProcessHelper, EventListener<TEvent>? callback, AddEventListenerOptions? options = null)
        where TEvent : Event, IJSCreatable<TEvent>
    {
        inProcessHelper.InvokeVoid("addEventListener", eventTarget.JSReference, typeof(TEvent).Name, callback?.JSReference, options);
    }

    /// <inheritdoc cref="IEventTargetInProcess.RemoveEventListener{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)"/>
    public static void RemoveEventListener<TEvent>(this IEventTargetInProcess eventTarget, IJSInProcessObjectReference inProcessHelper, string type, EventListener<TEvent>? callback, EventListenerOptions? options = null) where TEvent : Event, IJSCreatable<TEvent>
    {
        inProcessHelper.InvokeVoid("removeEventListener", eventTarget.JSReference, type, callback?.JSReference, options);
    }

    /// <inheritdoc cref="IEventTargetInProcess.RemoveEventListener{TEvent}(EventListener{TEvent}?, EventListenerOptions?)"/>
    public static void RemoveEventListener<TEvent>(this IEventTargetInProcess eventTarget, IJSInProcessObjectReference inProcessHelper, EventListener<TEvent>? callback, EventListenerOptions? options = null) where TEvent : Event, IJSCreatable<TEvent>
    {
        inProcessHelper.InvokeVoid("removeEventListener", eventTarget.JSReference, typeof(TEvent).Name, callback?.JSReference, options);
    }

    /// <inheritdoc cref="IEventTargetInProcess.DispatchEvent(Event)"/>
    public static bool DispatchEvent(this IEventTargetInProcess eventTarget, IJSInProcessObjectReference inProcessHelper, Event eventInstance)
    {
        return eventTarget.JSReference.Invoke<bool>("dispatchEvent", eventInstance.JSReference);
    }
}
