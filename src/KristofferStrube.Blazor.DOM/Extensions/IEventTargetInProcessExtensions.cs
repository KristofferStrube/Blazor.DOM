using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM.Extensions;

/// <summary>
/// Extension methods that hold the implementations for <see cref="EventTargetInProcess"/> so that they can be used even though a consumer of the libary is not able to extend that class.
/// </summary>
public static class IEventTargetInProcessExtensions
{
    /// <inheritdoc cref="IEventTargetInProcess.AddEventListener{TInProcessEvent, TEvent}(string, EventListenerInProcess{TInProcessEvent, TEvent}?, AddEventListenerOptions?)"/>
    public static void AddEventListener<TInProcessEvent, TEvent>(this IEventTargetInProcess eventTarget, IJSInProcessObjectReference inProcessHelper, string type, EventListenerInProcess<TInProcessEvent, TEvent>? callback, AddEventListenerOptions? options = null)
        where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
    {
        inProcessHelper.InvokeVoid("addEventListener", eventTarget.JSReference, type, callback?.JSReference, options);
    }

    /// <inheritdoc cref="IEventTargetInProcess.AddEventListener{TInProcessEvent, TEvent}(EventListenerInProcess{TInProcessEvent, TEvent}?, AddEventListenerOptions?)"/>
    public static void AddEventListener<TInProcessEvent, TEvent>(this IEventTargetInProcess eventTarget, IJSInProcessObjectReference inProcessHelper, EventListenerInProcess<TInProcessEvent, TEvent>? callback, AddEventListenerOptions? options = null)
        where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
    {
        inProcessHelper.InvokeVoid("addEventListener", eventTarget.JSReference, typeof(TEvent).Name, callback?.JSReference, options);
    }

    /// <inheritdoc cref="IEventTargetInProcess.RemoveEventListener{TInProcessEvent, TEvent}(string, EventListenerInProcess{TInProcessEvent, TEvent}?, EventListenerOptions?)"/>
    public static void RemoveEventListener<TInProcessEvent, TEvent>(this IEventTargetInProcess eventTarget, IJSInProcessObjectReference inProcessHelper, string type, EventListenerInProcess<TInProcessEvent, TEvent>? callback, EventListenerOptions? options = null)
        where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
    {
        inProcessHelper.InvokeVoid("removeEventListener", eventTarget.JSReference, type, callback?.JSReference, options);
    }

    /// <inheritdoc cref="IEventTargetInProcess.RemoveEventListener{TInProcessEvent, TEvent}(EventListenerInProcess{TInProcessEvent, TEvent}?, EventListenerOptions?)"/>
    public static void RemoveEventListener<TInProcessEvent, TEvent>(this IEventTargetInProcess eventTarget, IJSInProcessObjectReference inProcessHelper, EventListenerInProcess<TInProcessEvent, TEvent>? callback, EventListenerOptions? options = null)
        where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
    {
        inProcessHelper.InvokeVoid("removeEventListener", eventTarget.JSReference, typeof(TEvent).Name, callback?.JSReference, options);
    }

    /// <inheritdoc cref="IEventTargetInProcess.DispatchEvent(Event)"/>
    public static bool DispatchEvent(this IEventTargetInProcess eventTarget, Event eventInstance)
    {
        return eventTarget.JSReference.Invoke<bool>("dispatchEvent", eventInstance.JSReference);
    }
}
