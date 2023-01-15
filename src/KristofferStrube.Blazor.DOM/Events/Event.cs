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
    internal Event(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

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
    public async Task<EventTarget> GetTargetAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "target");
        return new EventTarget(jSRuntime, jSInstance);
    }
}
