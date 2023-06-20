using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// A base class for all wrapper classes from the Blazor.DOM library.
/// </summary>
public abstract class BaseJSWrapper : IJSWrapper, IAsyncDisposable
{
    /// <summary>
    /// A lazily loaded task that provide access to JS helper functions.
    /// </summary>
    protected readonly Lazy<Task<IJSObjectReference>> helperTask;

    /// <inheritdoc/>
    public IJSObjectReference JSReference { get; }

    /// <inheritdoc/>
    public IJSRuntime JSRuntime { get; }

    /// <summary>
    /// Constructs a wrapper instance for an equivalent JS instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing JS instance that should be wrapped.</param>
    internal BaseJSWrapper(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        helperTask = new(jSRuntime.GetHelperAsync);
        JSReference = jSReference;
        JSRuntime = jSRuntime;
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (helperTask.IsValueCreated)
        {
            IJSObjectReference module = await helperTask.Value;
            await module.DisposeAsync();
        }
        GC.SuppressFinalize(this);
    }
}