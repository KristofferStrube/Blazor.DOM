using KristofferStrube.Blazor.DOM.Events;
using KristofferStrube.Blazor.DOM.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// <see href="https://dom.spec.whatwg.org/#event">Event browser specs</see>
/// </summary>
public class Event : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="Event"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="Event"/>.</param>
    /// <returns>A wrapper instance for a <see cref="Event"/>.</returns>
    public static Event Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        Event eventInstance = new(jSRuntime, jSReference);
        return eventInstance;
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A wrapper instance for a <see cref="Event"/>.</returns>
    public static async Task<Event> CreateAsync(IJSRuntime jSRuntime, string type, EventInit? eventInitDict = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructEvent", type, eventInitDict);
        Event eventInstance = new(jSRuntime, jSInstance);
        return eventInstance;
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="Event"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="Event"/>.</param>
    protected Event(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Gets the type of this <see cref="Event"/>
    /// </summary>
    /// <returns>A string representing the type of event, e.g. "click", "hashchange", or "submit".</returns>
    public async Task<string> GetTypeAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "type");
    }

    /// <summary>
    /// Gets the target of this <see cref="Event"/>.
    /// </summary>
    /// <returns>The object to which this event is dispatched (its target).</returns>
    public async Task<EventTarget?> GetTargetAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference? jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "target");
        return jSInstance is null ? null : EventTarget.Create(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Gets the target of this <see cref="Event"/>.
    /// </summary>
    /// <returns>The object to which this event is dispatched (its target).</returns>
    public async Task<EventTarget?> GetSrcElementAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference? jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "srcElement");
        return jSInstance is null ? null : EventTarget.Create(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Gets the current target of this <see cref="Event"/>.
    /// </summary>
    /// <returns>The object whose event listener’s callback is currently being invoked.</returns>
    public async Task<EventTarget?> GetCurrentTargetAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference? jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "currentTarget");
        return jSInstance is null ? null : EventTarget.Create(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Returns the invocation target objects of event’s path (objects on which listeners will be invoked), except for any nodes in shadow trees of which the shadow root’s mode is "closed" that are not reachable from event’s currentTarget.
    /// </summary>
    /// <returns>An array of <see cref="EventTarget"/>s</returns>
    public async Task<EventTarget[]> ComposedPathAsync()
    {
        IJSObjectReference jSArray = await JSReference.InvokeAsync<IJSObjectReference>("composedPath");
        IJSObjectReference helper = await helperTask.Value;
        var length = await helper.InvokeAsync<int>("getAttribute", jSArray, "length");
        return (await Task.WhenAll(Enumerable
            .Range(0, length)
            .Select(async i => EventTarget.Create(jSRuntime, await helper.InvokeAsync<IJSObjectReference>("getAttribute", jSArray, i)))))
            .ToArray();
    }

    /// <summary>
    /// Gets this <see cref="Event"/>'s phase.
    /// </summary>
    /// <returns>The event’s phase, which is one of <see cref="EventPhase.None"/>, <see cref="EventPhase.CapturingPhase"/>, <see cref="EventPhase.AtTarget"/>, and <see cref="EventPhase.BubblingPhase"/>.</returns>
    public async Task<EventPhase> GetEventPhaseAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<EventPhase>("getAttribute", JSReference, "eventPhase");
    }

    /// <summary>
    /// When dispatched in a tree, invoking this method prevents event from reaching any objects other than the current object.
    /// </summary>
    /// <returns></returns>
    public async Task StopPropagationAsync()
    {
        await JSReference.InvokeVoidAsync("stopPropagation");
    }

    /// <summary>
    /// Its steps are to return <see langword="true"/> if this’s stop propagation flag is set; otherwise <see langword="false"/>.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> GetCancelBubbleAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "cancelBubble");
    }

    /// <summary>
    /// Its steps are to set this’s stop propagation flag if the given value is <see langword="true"/>; otherwise do nothing.
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task SetCancelBubbleAsync(bool value)
    {
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeAsync<bool>("setAttribute", JSReference, "cancelBubble", value);
    }

    /// <summary>
    /// Invoking this method prevents event from reaching any registered event listeners after the current one finishes running and, when dispatched in a tree, also prevents event from reaching any other objects.
    /// </summary>
    /// <returns></returns>
    public async Task StopImmediatePropagationAsync()
    {
        await JSReference.InvokeVoidAsync("stopImmediatePropagation");
    }

    /// <summary>
    /// Returns <see langword="true"/> or <see langword="false"/> depending on how the event was initialized.
    /// </summary>
    /// <returns>True if event goes through its target’s ancestors in reverse tree order; otherwise false.</returns>
    public async Task<bool> GetBubblesAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "bubbles");
    }

    /// <summary>
    /// Returns <see langword="true"/> or <see langword="false"/> depending on how event was initialized.
    /// </summary>
    /// <returns>Its return value does not always carry meaning, but <see langword="true"/> can indicate that part of the operation during which event was dispatched,can be canceled by invoking the <see cref="PreventDefaultAsync"/> method.</returns>
    public async Task<bool> GetCancelableAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "cancelable");
    }

    /// <summary>
    /// If invoked when the cancelable attribute value is <see langword="true"/>, and while executing a listener for the event with passive set to <see langword="false"/>, signals to the operation that caused event to be dispatched that it needs to be canceled.
    /// </summary>
    /// <returns></returns>
    public async Task PreventDefaultAsync()
    {
        await JSReference.InvokeVoidAsync("preventDefault");
    }

    /// <summary>
    /// Returns <see langword="true"/> or <see langword="false"/> depending on how the event was initialized.
    /// </summary>
    /// <returns><see langword="true"/> if <see cref="PreventDefaultAsync"/> was invoked successfully to indicate cancelation; otherwise <see langword="false"/>.</returns>
    public async Task<bool> GetDefaultPreventedAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "defaultPrevented");
    }

    /// <summary>
    /// Returns <see langword="true"/> or <see langword="false"/> depending on how the event was initialized.
    /// </summary>
    /// <returns><see langword="true"/> if event invokes listeners past a ShadowRoot node that is the root of its target; otherwise <see langword="false"/>.</returns>
    public async Task<bool> GetComposedAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "composed");
    }

    /// <summary>
    /// Gets the the flag <c>isTrusted</c> of this <see cref="Event"/>.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if event was dispatched by the user agent, and <see langword="false"/> otherwise.</returns>
    public async Task<bool> GetIsTrustedAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "isTrusted");
    }

    /// <summary>
    /// Gets the the flag <c>isTrusted</c> of this <see cref="Event"/>.
    /// </summary>
    /// <returns>The event’s timestamp as the number of milliseconds measured relative to the <see href="https://w3c.github.io/hr-time/#dfn-get-time-origin-timestamp">time origin</see>.</returns>
    public async Task<double> GetTimeStampAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "timeStamp");
    }
}
