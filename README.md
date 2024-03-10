[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](/LICENSE.md)
[![GitHub issues](https://img.shields.io/github/issues/KristofferStrube/Blazor.DOM)](https://github.com/KristofferStrube/Blazor.DOM/issues)
[![GitHub forks](https://img.shields.io/github/forks/KristofferStrube/Blazor.DOM)](https://github.com/KristofferStrube/Blazor.DOM/network/members)
[![GitHub stars](https://img.shields.io/github/stars/KristofferStrube/Blazor.DOM)](https://github.com/KristofferStrube/Blazor.DOM/stargazers)
[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/KristofferStrube.Blazor.DOM?label=NuGet%20Downloads)](https://www.nuget.org/packages/KristofferStrube.Blazor.DOM/)

# Introduction
A Blazor wrapper for the [DOM](https://dom.spec.whatwg.org/) browser API.

The API standardizes a lot of the most basic structures for working within the browser. This includes models for Events, Aborting mechanisms, and Nodes in the DOM. This project implements a wrapper around the API for Blazor so that we can easily and safely interact with it from Blazor.

*The project will not focus on the part of the API related to Nodes as this often leads to misuse of Blazor by circumventing the Blazor rendering engine.*

# Demo
The sample project can be demoed at https://kristofferstrube.github.io/Blazor.DOM/

On each page, you can find the corresponding code for the example in the top right corner.

On the [Status page](https://kristofferstrube.github.io/Blazor.DOM/Status) you can see how much of the WebIDL specs this wrapper has covered.

# Events
The package brings a wrapper for `EventTarget`s. This enables us to listen for specific `Event`s happening on objects and to dispatch these events ourselves.
```csharp
ElementReference element; // Some element that we have a reference to.

EventTarget eventTarget = await EventTarget.CreateAsync(JSRuntime, element);

EventListener<Event> callback = await EventListener<Event>.CreateAsync(JSRuntime, async (e) =>
{
    if (await e.GetTypeAsync() is "pointerdown")
    {
        await e.PreventDefaultAsync();
        Console.WriteLine("A pointer was pressed down and we prevented the default behaviour.");
    }
    else
    {
        Console.WriteLine("Some other pointer event happened.");
    }
});

await eventTarget.AddEventListenerAsync("pointerdown", callback);
await eventTarget.AddEventListenerAsync("pointermove", callback);
await eventTarget.AddEventListenerAsync("pointerup", callback);
await eventTarget.AddEventListenerAsync("pointerleave", callback);
```
The above example serves as an imperative alternative to the the native way to listen to events. But it also opens up for controlling some of the options available on events like preventing the default behavior programmatically. In the above example we use this on a `ElementReference`, but we can also create an `EventTarget` from an `IJSObjectReference` instead which means we can listen for events happening on any JS object that emits events.

# Aborting
In JS the counterpart to a `CancellationTokenSource` is called an `AbortController`. Like we can get a `CancellationToken` from an `CancellationTokenSource` in .NET we can get an `AbortSignal` from an `AbortController` in JS. Multiple standard APIs and libraries allow us to parse an `AbortSignal` to functions to be able to stop some long-running action.

Let's imagine that there is some JS function which is cancellable called `myLongRunningFunction(signal)` which accepts an `AbortSignal`. Then we can cancel it if we need to like this.

```csharp
AbortController abortController = await AbortController.CreateAsync(JSRuntime);
AbortSignal abortSignal = await abortController.GetSignalAsync();

await JSRuntime.InvokeVoidAsync("myLongRunningFunction", abortSignal);

// At a later point we can cancel this long running function from another method.
await abortController.AbortAsync();
```

# Issues
Feel free to open issues on the repository if you find any errors with the package or have wishes for features.

# Related repositories
This project uses the `IJSCreatable` interface from the *Blazor.WebIDL* package to define that `Event`s should be able to construct themselves given a JS reference.
- https://github.com/KristofferStrube/Blazor.WebIDL

Many classes from the *Blazor.CSSFontLoading*, *Blazor.MediaCaptureStreams*, and *Blazor.WebAudio* projects extend the `EventTarget` class from this library to be able to dispatch and listen to events.
- https://github.com/KristofferStrube/Blazor.MediaCaptureStreams
- https://github.com/KristofferStrube/Blazor.WebAudio
- https://github.com/KristofferStrube/Blazor.CSSFontLoading

These repositories are going to use the library in the future to support a variety of features.
- https://github.com/KristofferStrube/Blazor.Streams (uses AbortSignals and Events)
- https://github.com/KristofferStrube/Blazor.FileAPI (uses Events)
- https://github.com/KristofferStrube/Blazor.FileSystemAccess (uses Events)

This project is going to be used in the *Blazor.UIEvent* project but we have not started progress on wrapping the *UI Event API* yet.

# Related articles
This repository was built with inspiration and help from the following series of articles:

- [Wrapping JavaScript libraries in Blazor WebAssembly/WASM](https://blog.elmah.io/wrapping-javascript-libraries-in-blazor-webassembly-wasm/)
- [Call anonymous C# functions from JS in Blazor WASM](https://blog.elmah.io/call-anonymous-c-functions-from-js-in-blazor-wasm/)
- [Using JS Object References in Blazor WASM to wrap JS libraries](https://blog.elmah.io/using-js-object-references-in-blazor-wasm-to-wrap-js-libraries/)
- [Blazor WASM 404 error and fix for GitHub Pages](https://blog.elmah.io/blazor-wasm-404-error-and-fix-for-github-pages/)
- [How to fix Blazor WASM base path problems](https://blog.elmah.io/how-to-fix-blazor-wasm-base-path-problems/)
