using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// <see cref="Event"/>s using the <see cref="CustomEvent"/> interface can be used to carry custom data which is accessible from <see cref="Detail"/>.
/// </summary>
/// <remarks><see href="https://dom.spec.whatwg.org/#customevent">See the API definition here</see></remarks>
public class CustomEventInProcess : CustomEvent, IJSInProcessCreatable<CustomEventInProcess, CustomEvent>
{
    /// <summary>
    /// An in-process helper.
    /// </summary>
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <inheritdoc/>
    public new IJSInProcessObjectReference JSReference { get; }

    /// <inheritdoc/>
    public static async Task<CustomEventInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        IJSInProcessObjectReference helper = await jSRuntime.GetInProcessHelperAsync();
        return new CustomEventInProcess(jSRuntime, helper, jSReference);
    }

    /// <inheritdoc/>
    protected CustomEventInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    /// <summary>
    /// The details of the <see cref="CustomEvent"/>.
    /// </summary>
    public IJSObjectReference Detail => inProcessHelper.Invoke<IJSObjectReference>("getAttribute", JSReference, "detail");
}
