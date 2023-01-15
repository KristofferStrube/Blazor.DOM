export function getAttribute(object, attribute) { return object[attribute]; }

export function addEventListener(target, type, eventListener, options = null)
{
    if (options instanceof Boolean)
    {
    }
    else
    {
        target.addEventListener(type, eventListener.callback, options)
    }
}

export function constructEventListener(eventListenerObjRef) {
    return {
        callback: (e) => eventListenerObjRef.invokeMethodAsync("HandleEventAsync", DotNet.createJSObjectReference(e))
    };
}

export function constructEvent(type, eventInitDict = null) {
    return new Event(type, eventInitDict);
}

export function constructEventTarget() { return new EventTarget(); }