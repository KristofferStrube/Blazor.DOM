using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// <see cref="Event"/>s using the <see cref="CustomEvent"/> interface can be used to carry custom data which is accessible using the <see cref="GetDetailAsync"/> method.
/// </summary>
/// <remarks><see href="https://dom.spec.whatwg.org/#customevent">See the API definition here</see></remarks>
[IJSWrapperConverter]
public class CustomEvent : Event, IJSCreatable<CustomEvent>
{
    /// <inheritdoc/>
    public static new async Task<CustomEvent> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<CustomEvent> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new CustomEvent(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="type">The type of the new <see cref="Event"/>.</param>
    /// <param name="eventInitDict">Extra options for setting whether the event bubbles and is cancelable.</param>
    /// <returns>A wrapper instance for a <see cref="CustomEvent"/>.</returns>
    public static async Task<CustomEvent> CreateAsync(IJSRuntime jSRuntime, string type, CustomEventInit? eventInitDict = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructCustomEvent", type, eventInitDict);
        return new CustomEvent(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc/>
    protected CustomEvent(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }

    /// <summary>
    /// The details of the <see cref="CustomEvent"/>.
    /// </summary>
    /// <returns>Any custom data the event was created with. Typically used for synthetic events.</returns>
    public async Task<IJSObjectReference?> GetDetailAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "detail");
    }
}
