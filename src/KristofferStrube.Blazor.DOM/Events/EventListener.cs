using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// <see href="https://dom.spec.whatwg.org/#callbackdef-eventlistener">EventListener browser specs</see>
/// </summary>
public class EventListener<TEvent> : BaseJSWrapper where TEvent : Event, IJSWrapper<TEvent>
{
    private Action<TEvent>? callback;
    private Func<TEvent, Task>? asyncCallback;

    public static async Task<EventListener<TEvent>> CreateAsync(IJSRuntime jSRuntime, Action<TEvent> callback)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructEventListener");
        EventListener<TEvent> eventListener = new(jSRuntime, jSInstance)
        {
            callback = callback
        };
        await helper.InvokeVoidAsync("registerEventHandlerAsync", DotNetObjectReference.Create(eventListener), jSInstance);
        return eventListener;
    }

    public static async Task<EventListener<TEvent>> CreateAsync(IJSRuntime jSRuntime, Func<TEvent, Task> callback)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructEventListener");
        EventListener<TEvent> eventListener = new(jSRuntime, jSInstance)
        {
            asyncCallback = callback
        };
        await helper.InvokeVoidAsync("registerEventHandlerAsync", DotNetObjectReference.Create(eventListener), jSInstance);
        return eventListener;
    }

    protected EventListener(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    [JSInvokable]
    public async Task HandleEventAsync(IJSObjectReference jSObjectReference)
    {
        if (callback is not null)
        {
            callback.Invoke(await TEvent.CreateAsync(JSRuntime, jSObjectReference));
        }
        else if (asyncCallback is not null)
        {
            await asyncCallback.Invoke(await TEvent.CreateAsync(JSRuntime, jSObjectReference));
        }
    }
}
