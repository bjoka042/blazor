# Plugin based .NET Core Blazor Demo
This project shows the possibility to load external razor libs as plugins to a Blazor App.

# Goals
The aim was to be able to load external assemblies into existing app and be able to use razor pages and components defined in those libs. I also took the time to investigate patterns for moving the layout files into a common lib (Layout), That way if I was to create another host (Razor web app) i could reuse the layout and have a seamless look and feel between the apps.

## Solution projects
- Host1 (Website)
  - McMaster.NETCore.Plugins (External, https://github.com/natemcmaster/DotNetCorePlugins)
  - McMaster.NETCore.Plugins.Mvc (External, https://github.com/natemcmaster/DotNetCorePlugins)
  - Layout
  - HostBase (Library)
  - PluginBase (Library)
  - Plugins (Folder)
    - Plugin1 (Razor library)
    - plugin2 (Razor library)
    
# How to run
Build solution 
- Plugin1 and Plugin2 have AfterBuild targets to CopyOutputToDestination "Host1\Plugins"

Set Startup Projects to "Host1"
Run application using, Run->Host1.
