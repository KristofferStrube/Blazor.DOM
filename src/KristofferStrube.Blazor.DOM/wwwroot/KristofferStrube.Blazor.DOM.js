export function getAttribute(object, attribute) { return object[attribute]; }

export function getJSReference(element) { return element.valueOf(); }

export function addEventListener(target, type, eventListener, options = null)
{
    if (options instanceof Boolean)
    {
    }
    else
    {
        target.addEventListener(type, eventListener, options)
    }
}

export function constructEventListener() {
    return { };
}

export function registerEventHandlerAsync(objRef, jSInstance) {
    jSInstance.handleEvent = (e) => objRef.invokeMethodAsync("HandleEventAsync", DotNet.createJSObjectReference(e))
}

export function constructEvent(type, eventInitDict = null) {
    return new Event(type, eventInitDict);
}

export function constructEventTarget() { return new EventTarget(); }