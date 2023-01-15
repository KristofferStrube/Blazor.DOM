using KristofferStrube.Blazor.DOM.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

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
        IJSObjectReference jSInstance = eventInitDict is null
            ? await helper.InvokeAsync<IJSObjectReference>("constructEvent", type)
            : await helper.InvokeAsync<IJSObjectReference>("constructEvent", type, eventInitDict);
        Event eventInstance = new(jSRuntime, jSInstance);
        return eventInstance;
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="Event"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="Event"/>.</param>
    internal Event(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<string> GetTypeAsync()
    {
        var helper = await helperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "type");
    }
}
