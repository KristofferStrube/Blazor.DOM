# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.3.0] - 2024-10-23
### Fixed
- Fixed that `EventListener` didn't dispose the events that it created during callback after use.
### Changed
- Changed the version of `Blazor.WebIDL` to use the newest version which is 0.6.0.
### Added
- Added implementation of `Event.CreateAsync(jSRuntime, jSReference, CreationOptions)`.
- Added target for .NET 8.

## [0.2.2] - 2024-04-28
### Fixed
- Fixed that `AbortController` was placed in namespace `KristofferStrube.Blazor.DOM.Abort` instead of just `KristofferStrube.Blazor.DOM`.

## [0.2.1] - 2024-04-09
### Fixed
- Fixed that the static method `EventTarget.CreateAsync(IJSRuntime, JSReference)` was not public.

## [0.2.0] - 2024-03-10
### Changed
- Changed the version of `Blazor.WebIDL` to use the newest version which is 0.5.0.
