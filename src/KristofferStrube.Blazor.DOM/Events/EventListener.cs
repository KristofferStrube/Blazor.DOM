using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

public class EventListener
{
    private IJSRuntime jSRuntime;
    private Action<Event>? callback;
    private Func<Event, Task>? asyncCallback;

    public EventListener(IJSRuntime jSRuntime, Action<Event> callback)
    {
        this.jSRuntime = jSRuntime;
        this.callback = callback;
        ObjRef = DotNetObjectReference.Create(this);
    }

    public EventListener(IJSRuntime jSRuntime, Func<Event, Task> callback)
    {
        this.jSRuntime = jSRuntime;
        asyncCallback = callback;
        ObjRef = DotNetObjectReference.Create(this);
    }

    internal IJSObjectReference? JSReference { get; set; }
    internal DotNetObjectReference<EventListener> ObjRef { get; }

    [JSInvokable]
    public async Task HandleEventAsync(IJSObjectReference jSObjectReference)
    {
        if (callback is not null)
        {
            callback.Invoke(Event.Create(jSRuntime, jSObjectReference));
        }
        else if (asyncCallback is not null)
        {
            await asyncCallback.Invoke(Event.Create(jSRuntime, jSObjectReference));
        }
    }
}
