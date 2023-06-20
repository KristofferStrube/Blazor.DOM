using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <inheritdoc/>
public class EventInProcess : Event, IJSInProcessCreatable<EventInProcess, Event>
{
    /// <summary>
    /// An in-process helper.
    /// </summary>
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <inheritdoc/>
    public new IJSInProcessObjectReference JSReference { get; }

    /// <inheritdoc/>
    public static async Task<EventInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        IJSInProcessObjectReference helper = await jSRuntime.GetInProcessHelperAsync();
        return new(jSRuntime, helper, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="type">The type of the new <see cref="EventInProcess"/>.</param>
    /// <param name="eventInitDict">Extra options for setting whether the event bubbles and is cancelable.</param>
    /// <returns>A wrapper instance for a <see cref="EventInProcess"/>.</returns>
    public static new async Task<EventInProcess> CreateAsync(IJSRuntime jSRuntime, string type, EventInit? eventInitDict = null)
    {
        IJSInProcessObjectReference helper = await jSRuntime.GetInProcessHelperAsync();
        IJSInProcessObjectReference jSInstance = await helper.InvokeAsync<IJSInProcessObjectReference>("constructEvent", type, eventInitDict);
        return new(jSRuntime, helper, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="EventInProcess"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in-process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="EventInProcess"/>.</param>
    protected EventInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    /// <summary>
    /// Returns the type of this <see cref="Event"/>
    /// </summary>
    public string Type => inProcessHelper.Invoke<string>("getAttribute", JSReference, "type");

    /// <summary>
    /// Gets the target of this <see cref="Event"/>.
    /// </summary>
    public new async Task<EventTargetInProcess?> GetTargetAsync()
    {
        IJSInProcessObjectReference? jSInstance = await inProcessHelper.InvokeAsync<IJSInProcessObjectReference?>("getAttribute", JSReference, "target");
        IJSInProcessObjectReference helper = await JSRuntime.GetInProcessHelperAsync();
        return jSInstance is null ? null : new EventTargetInProcess(JSRuntime, helper, jSInstance);
    }

    /// <summary>
    /// Gets the current target of this <see cref="Event"/>.
    /// </summary>
    /// <returns>The object whose event listener’s callback is currently being invoked.</returns>
    public new async Task<EventTargetInProcess?> GetCurrentTargetAsync()
    {
        IJSInProcessObjectReference? jSInstance = await inProcessHelper.InvokeAsync<IJSInProcessObjectReference?>("getAttribute", JSReference, "currentTarget");
        IJSInProcessObjectReference helper = await JSRuntime.GetInProcessHelperAsync();
        return jSInstance is null ? null : new EventTargetInProcess(JSRuntime, helper, jSInstance);
    }

    /// <summary>
    /// Returns the invocation target objects of event’s path (objects on which listeners will be invoked), except for any nodes in shadow trees of which the shadow root’s mode is "closed" that are not reachable from event’s currentTarget.
    /// </summary>
    /// <returns>An array of <see cref="EventTargetInProcess"/>s</returns>
    public new async Task<EventTargetInProcess[]> ComposedPathAsync()
    {
        IJSObjectReference jSArray = await JSReference.InvokeAsync<IJSObjectReference>("composedPath");
        int length = await inProcessHelper.InvokeAsync<int>("getAttribute", jSArray, "length");
        return (await Task.WhenAll(Enumerable
            .Range(0, length)
            .Select(async i => await EventTargetInProcess.CreateAsync(JSRuntime, await inProcessHelper.InvokeAsync<IJSInProcessObjectReference>("getAttribute", jSArray, i)))))
            .ToArray();
    }

    /// <summary>
    /// Returns the <see cref="Event"/>'s phase.
    /// </summary>
    public EventPhase EventPhase => inProcessHelper.Invoke<EventPhase>("getAttribute", JSReference, "eventPhase");

    /// <summary>
    /// When dispatched in a tree, invoking this method prevents the event from reaching any objects other than the current object.
    /// </summary>
    /// <returns></returns>
    public void StopPropagation()
    {
        JSReference.InvokeVoid("stopPropagation");
    }

    /// <summary>
    /// Invoking this method prevents event from reaching any registered event listeners after the current one finishes running and, when dispatched in a tree, also prevents event from reaching any other objects.
    /// </summary>
    public void StopImmediatePropagation()
    {
        JSReference.InvokeVoid("stopImmediatePropagation");
    }

    /// <summary>
    /// Returns <see langword="true"/> if the event goes through its target’s ancestors in reverse tree order; otherwise <see langword="false"/>.
    /// </summary>
    public bool Bubbles => inProcessHelper.Invoke<bool>("getAttribute", JSReference, "bubbles");

    /// <summary>
    /// Its value does not always carry meaning, but <see langword="true"/> can indicate that part of the operation during which event was dispatched,can be canceled by invoking the <see cref="PreventDefault"/> method.
    /// </summary>
    public bool Cancelable => inProcessHelper.Invoke<bool>("getAttribute", JSReference, "cancelable");

    /// <summary>
    /// If invoked when the cancelable attribute value is <see langword="true"/>, and while executing a listener for the event with passive set to <see langword="false"/>, then it signals to the operation that caused event to be dispatched that it needs to be canceled.
    /// </summary>
    public void PreventDefault()
    {
        JSReference.InvokeVoid("preventDefault");
    }

    /// <summary>
    /// Returns <see langword="true"/> if <see cref="PreventDefault"/> was invoked successfully to indicate cancelation; otherwise <see langword="false"/>.
    /// </summary>
    public bool DefaultPrevented => inProcessHelper.Invoke<bool>("getAttribute", JSReference, "defaultPrevented");

    /// <summary>
    /// Returns <see langword="true"/> if the event invokes listeners past a ShadowRoot node that is the root of its target; otherwise <see langword="false"/>.
    /// </summary>
    public bool Composed => inProcessHelper.Invoke<bool>("getAttribute", JSReference, "composed");

    /// <summary>
    /// Returns <see langword="true"/> if the event was dispatched by the user agent, and <see langword="false"/> otherwise.
    /// </summary>
    public bool IsTrusted => inProcessHelper.Invoke<bool>("getAttribute", JSReference, "isTrusted");

    /// <summary>
    /// Returns the event’s timestamp as the number of milliseconds measured relative to the <see href="https://w3c.github.io/hr-time/#dfn-get-time-origin-timestamp">time origin</see>.
    /// </summary>
    public double TimeStamp => inProcessHelper.Invoke<double>("getAttribute", JSReference, "timeStamp");
}
