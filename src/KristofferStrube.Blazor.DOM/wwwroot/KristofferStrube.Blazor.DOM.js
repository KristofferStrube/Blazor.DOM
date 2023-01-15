export function getAttribute(object, attribute) { return object[attribute]; }

export function constructEvent(type, eventInitDict = null) {
    return new Event(type, eventInitDict);
}