using KristofferStrube.Blazor.DOM.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

public class EventTarget : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="EventTarget"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="EventTarget"/>.</param>
    /// <returns>A wrapper instance for a <see cref="EventTarget"/>.</returns>
    public static EventTarget Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        EventTarget eventTarget = new(jSRuntime, jSReference);
        return eventTarget;
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A wrapper instance for a <see cref="EventTarget"/>.</returns>
    public static async Task<EventTarget> CreateAsync(IJSRuntime jSRuntime)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructEventTarget");
        EventTarget eventTarget = new(jSRuntime, jSInstance);
        return eventTarget;
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="EventTarget"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="EventTarget"/>.</param>
    internal EventTarget(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Appends an event listener for events whose type attribute value is type. The event listener is appended to target’s event listener list and is not appended if it has the same type, callback, and capture.
    /// </summary>
    /// <param name="type">The type of events that the event listener will listen to.</param>
    /// <param name="callback">The callback argument sets the callback that will be invoked when the event is dispatched.</param>
    /// <param name="options">The options argument sets listener-specific options.</param>
    /// <returns></returns>
    public async Task AddEventListenerAsync(string type, EventCallback callback, AddEventListenerOptions? options = null)
    {
        var helper = await helperTask.Value;

        if (callback.eventCallbackJSReference is null)
        {
            callback.eventCallbackJSReference = await helper.InvokeAsync<IJSObjectReference>("constructEventCallback", callback.ObjRef);
        }

        await helper.InvokeVoidAsync("addEventListener", JSReference, type, callback.eventCallbackJSReference, options);
    }
}
