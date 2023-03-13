using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

public class CustomEvent : Event, IJSCreatable<CustomEvent>
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="CustomEvent"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="CustomEvent"/>.</param>
    /// <returns>A wrapper instance for a <see cref="CustomEvent"/>.</returns>
    public static new Task<CustomEvent> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        CustomEvent eventInstance = new(jSRuntime, jSReference);
        return Task.FromResult(eventInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A wrapper instance for a <see cref="CustomEvent"/>.</returns>
    public static async Task<CustomEvent> CreateAsync(IJSRuntime jSRuntime, string type, CustomEventInit? eventInitDict = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructCustomEvent", type, eventInitDict);
        CustomEvent eventInstance = new(jSRuntime, jSInstance);
        return eventInstance;
    }

    protected CustomEvent(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    /// <summary>
    /// The details of the <see cref="CustomEvent"/>.
    /// </summary>
    /// <returns>Any custom data event was created with. Typically used for synthetic events.</returns>
    public async Task<IJSObjectReference?> GetDetailAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "detail");
    }
}
