using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// An <see cref="Event"/> object is simply named an event. It allows for signaling that something has occurred, e.g., that an image has completed downloading.
/// </summary>
/// <remarks><see href="https://dom.spec.whatwg.org/#event">See the API definition here</see></remarks>
[IJSWrapperConverter]
public class Event : BaseJSWrapper, IJSCreatable<Event>
{
    /// <inheritdoc/>
    public static async Task<Event> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<Event> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new Event(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="type">The type of the new <see cref="Event"/>.</param>
    /// <param name="eventInitDict">Extra options for setting whether the event bubbles and is cancelable.</param>
    /// <returns>A wrapper instance for a <see cref="Event"/>.</returns>
    public static async Task<Event> CreateAsync(IJSRuntime jSRuntime, string type, EventInit? eventInitDict = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructEvent", type, eventInitDict);
        Event eventInstance = new(jSRuntime, jSInstance, new() { DisposesJSReference = true });
        return eventInstance;
    }

    /// <inheritdoc/>
    protected Event(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

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
        return jSInstance is null ? null : new EventTarget(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Gets the current target of this <see cref="Event"/>.
    /// </summary>
    /// <returns>The object whose event listener’s callback is currently being invoked.</returns>
    public async Task<EventTarget?> GetCurrentTargetAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference? jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "currentTarget");
        return jSInstance is null ? null : new EventTarget(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Returns the invocation target objects of event’s path (objects on which listeners will be invoked), except for any nodes in shadow trees of which the shadow root’s mode is "closed" that are not reachable from event’s currentTarget.
    /// </summary>
    /// <returns>An array of <see cref="EventTarget"/>s</returns>
    public async Task<EventTarget[]> ComposedPathAsync()
    {
        IJSObjectReference jSArray = await JSReference.InvokeAsync<IJSObjectReference>("composedPath");
        IJSObjectReference helper = await helperTask.Value;
        int length = await helper.InvokeAsync<int>("getAttribute", jSArray, "length");
        return (await Task.WhenAll(Enumerable
            .Range(0, length)
            .Select(async i => new EventTarget(JSRuntime, await helper.InvokeAsync<IJSObjectReference>("getAttribute", jSArray, i), new() { DisposesJSReference = true }))))
            .ToArray();
    }

    /// <summary>
    /// Gets this <see cref="Event"/>'s phase.
    /// </summary>
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
    /// Invoking this method prevents event from reaching any registered event listeners after the current one finishes running and, when dispatched in a tree, also prevents event from reaching any other objects.
    /// </summary>
    public async Task StopImmediatePropagationAsync()
    {
        await JSReference.InvokeVoidAsync("stopImmediatePropagation");
    }

    /// <summary>
    /// Returns <see langword="true"/> if the event goes through its target’s ancestors in reverse tree order; otherwise <see langword="false"/>.
    /// </summary>
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
    public async Task PreventDefaultAsync()
    {
        await JSReference.InvokeVoidAsync("preventDefault");
    }

    /// <summary>
    /// Returns <see langword="true"/> if <see cref="PreventDefaultAsync"/> was invoked successfully to indicate cancelation; otherwise <see langword="false"/>.
    /// </summary>
    public async Task<bool> GetDefaultPreventedAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "defaultPrevented");
    }

    /// <summary>
    /// Returns <see langword="true"/> if the event invokes listeners past a ShadowRoot node that is the root of its target; otherwise <see langword="false"/>.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> GetComposedAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "composed");
    }

    /// <summary>
    /// Returns <see langword="true"/> if the event was dispatched by the user agent, and <see langword="false"/> otherwise.
    /// </summary>
    public async Task<bool> GetIsTrustedAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "isTrusted");
    }

    /// <summary>
    /// Returns the event’s timestamp as the number of milliseconds measured relative to the <see href="https://w3c.github.io/hr-time/#dfn-get-time-origin-timestamp">time origin</see>.
    /// </summary>
    public async Task<double> GetTimeStampAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "timeStamp");
    }
}
