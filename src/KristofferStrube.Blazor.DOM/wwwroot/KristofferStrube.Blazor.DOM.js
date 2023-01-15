export function getAttribute(object, attribute) { return object[attribute]; }

export function addEventListener(target, type, eventCallback, options = null)
{
    if (options instanceof Boolean)
    {
    }
    else
    {
        target.addEventListener(type, eventCallback.callback, options)
    }
}

export function constructEventCallback(callbackObjRef) {
    return {
        callback: (e) => callbackObjRef.invokeMethodAsync("InvokeCallback", DotNet.createJSObjectReference(e))
    };
}

export function constructEvent(type, eventInitDict = null) {
    return new Event(type, eventInitDict);
}

export function constructEventTarget() { return new EventTarget(); }