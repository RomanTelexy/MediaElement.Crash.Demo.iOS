# MediaElement.Crash.Demo (iOS)

Minimal .NET MAUI (.NET 9) reproduction showing an iOS-only crash when using a `MediaElement` inside a `CarouselView` `DataTemplate` without Shell navigation.

## Overview

This app intentionally removes Shell to keep the navigation stack minimal and surfaces an iOS crash related to `MediaElement` usage in a templated control.  
Android (and likely Windows/MacCatalyst) run without crashing; iOS reproduces the fault.

## Error:
```
{System.InvalidOperationException: Cannot find current page   
at CommunityToolkit.Maui.Core.Views.MauiMediaElement..ctor(AVPlayerViewController playerViewController, MediaElement virtualView) 
in /_/src/CommunityToolkit.Maui.MediaElement/Views/MauiMediaElement.macios.cs:line 57   
at CommunityToolkit.Maui.Core.Handlers.MediaElementHandler.CreatePlatformView()
```

## Key Points

- `App` overrides `CreateWindow` to return a `NavigationPage` instead of using Shell.
- `MainPage` contains a `CarouselView` whose `ItemTemplate` is a very simple `MediaElement` bound to a list of three remote MP4 URLs.
- Crash occurs only on iOS. The same code path does not crash other platforms (based on current observations).
- Wrapping the `MainPage` in a `NavigationPage` permits exception capture/visibility; using plain `new Window(new MainPage())` more directly exposes the failure scenario.
- The scenario highlights a potential issue with `MediaElement` lifecycle inside a recycled templated control (`CarouselView`).

## Relevant Code

### App.xaml.cs (navigation override)
