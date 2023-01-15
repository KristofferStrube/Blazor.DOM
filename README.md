[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](/LICENSE.md)
[![GitHub issues](https://img.shields.io/github/issues/KristofferStrube/Blazor.DOM)](https://github.com/KristofferStrube/Blazor.DOM/issues)
[![GitHub forks](https://img.shields.io/github/forks/KristofferStrube/Blazor.DOM)](https://github.com/KristofferStrube/Blazor.DOM/network/members)
[![GitHub stars](https://img.shields.io/github/stars/KristofferStrube/Blazor.DOM)](https://github.com/KristofferStrube/Blazor.DOM/stargazers)
<!--[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/KristofferStrube.Blazor.DOM?label=NuGet%20Downloads)](https://www.nuget.org/packages/KristofferStrube.Blazor.DOM/)-->

# Introduction
A Blazor wrapper for the [DOM](https://dom.spec.whatwg.org/) browser API.

The API standardizes a lot of the most basic structures for working within the browser. This includes models for Events, Aborting mechanisms, and Nodes in the DOM. This project implements a wrapper around the API for Blazor so that we can easily and safely interact with it from Blazor.

*The project will not focus on the part of the API related to Nodes as this often leads to misuse of Blazor by circumventing the Blazor rendering engine.*

**This project is still being developer so coverage is very limited.**

<!--# Demo
The sample project can be demoed at https://kristofferstrube.github.io/Blazor.DOM/

On each page you can find the corresponding code for the example in the top right corner.

On the [Status page](https://kristofferstrube.github.io/Blazor.DOM/Status) you can see how much of the WebIDL specs this wrapper has covered.-->

# Issues
Feel free to open issues on the repository if you find any errors with the package or have wishes for features.

# Related repositories
This project is going to be used in these projects:
- https://github.com/KristofferStrube/Blazor.Streams (uses AbortSignals and Events)
- https://github.com/KristofferStrube/Blazor.FileAPI (uses Events)
- https://github.com/KristofferStrube/Blazor.FileSystemAccess (uses Events)
- https://github.com/KristofferStrube/Blazor.CSSFontLoading (uses Events)

This project is going to be used in these projects that haven't been build yet because they are missing key features from this project:
- Blazor.UIEvent - API specifying the common UI events like MouseEvent, DragEvent, etc. (uses Events)

# Related articles
This repository was build with inspiration and help from the following series of articles:

- [Wrapping JavaScript libraries in Blazor WebAssembly/WASM](https://blog.elmah.io/wrapping-javascript-libraries-in-blazor-webassembly-wasm/)
- [Call anonymous C# functions from JS in Blazor WASM](https://blog.elmah.io/call-anonymous-c-functions-from-js-in-blazor-wasm/)
- [Using JS Object References in Blazor WASM to wrap JS libraries](https://blog.elmah.io/using-js-object-references-in-blazor-wasm-to-wrap-js-libraries/)
- [Blazor WASM 404 error and fix for GitHub Pages](https://blog.elmah.io/blazor-wasm-404-error-and-fix-for-github-pages/)
- [How to fix Blazor WASM base path problems](https://blog.elmah.io/how-to-fix-blazor-wasm-base-path-problems/)
