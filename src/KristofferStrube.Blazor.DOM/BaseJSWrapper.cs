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
    /// <inheritdoc/>
    public bool DisposesJSReference { get; }

    /// <inheritdoc cref="IJSCreatable{T}.CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    internal BaseJSWrapper(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        helperTask = new(jSRuntime.GetHelperAsync);
        JSReference = jSReference;
        JSRuntime = jSRuntime;
        DisposesJSReference = options.DisposesJSReference;
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (helperTask.IsValueCreated)
        {
            IJSObjectReference module = await helperTask.Value;
            await module.DisposeAsync();
        }
        await IJSWrapper.DisposeJSReference(this);
        GC.SuppressFinalize(this);
    }
}