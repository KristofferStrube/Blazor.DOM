﻿using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// Though promises do not have a built-in aborting mechanism, many APIs using them require abort semantics.
/// <see cref="AbortController"/> is meant to support these requirements by providing an <see cref="AbortAsync(IJSObjectReference?)"/> method that toggles the state of a corresponding <see cref="AbortSignal"/> object.
/// The API which wishes to support aborting can accept an <see cref="AbortSignal"/> object, and use its state to determine how to proceed.
/// </summary>
/// <remarks><see href="https://dom.spec.whatwg.org/#abortcontroller">See the API definition here</see></remarks>
[IJSWrapperConverter]
public class AbortController : BaseJSWrapper, IJSCreatable<AbortController>
{
    /// <inheritdoc/>
    public static async Task<AbortController> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<AbortController> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new AbortController(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A wrapper instance for a <see cref="AbortController"/>.</returns>
    public static async Task<AbortController> CreateAsync(IJSRuntime jSRuntime)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructAbortController");
        AbortController abortController = new(jSRuntime, jSInstance, new() { DisposesJSReference = true });
        return abortController;
    }

    /// <inheritdoc/>
    protected AbortController(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }

    /// <summary>
    /// Returns the <see cref="AbortSignal"/> object associated with this object.
    /// </summary>
    /// <returns></returns>
    public async Task<AbortSignal> GetSignalAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "signal");
        return await AbortSignal.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Invoking this method will store reason in this object’s <see cref="AbortSignal.GetReasonAsync"/>, and signal to any observers that the associated activity is to be aborted.
    /// If reason is undefined, then an <see cref="AbortErrorException"/> will be stored.
    /// </summary>
    /// <param name="reason">The reason for why the activity is aborted.</param>
    public async Task AbortAsync(string reason)
    {
        await JSReference.InvokeVoidAsync("abort", reason);
    }

    /// <summary>
    /// Invoking this method will store reason in this object’s <see cref="AbortSignal.GetReasonAsync"/>, and signal to any observers that the associated activity is to be aborted.
    /// If reason is undefined, then an <see cref="AbortErrorException"/> will be stored.
    /// </summary>
    /// <param name="reason">The reason for why the activity is aborted.</param>
    public async Task AbortAsync(IJSObjectReference reason)
    {
        await JSReference.InvokeVoidAsync("abort", reason);
    }

    /// <summary>
    /// Invoking this method will store reason in this object’s <see cref="AbortSignal.GetReasonAsync"/>, and signal to any observers that the associated activity is to be aborted.
    /// If reason is undefined, then an <see cref="AbortErrorException"/> will be stored.
    /// </summary>
    /// <param name="reason">The reason for why the activity is aborted.</param>
    public async Task AbortAsync(IJSWrapper reason)
    {
        await JSReference.InvokeVoidAsync("abort", reason?.JSReference);
    }

    /// <summary>
    /// Invoking this method will store reason in this object’s <see cref="AbortSignal.GetReasonAsync"/>, and signal to any observers that the associated activity is to be aborted.
    /// </summary>
    public async Task AbortAsync()
    {
        await JSReference.InvokeVoidAsync("abort");
    }
}
