# Fireworks ScreenSaver: User's Guide



## Downloads & Runtimes

Download the latest release from the [Releases page](https://github.com/RemusRigo/FireworksSS-VB.NET/releases).

Each release contains one folder per framework and architecture:

|Folder|Target Framework|Architecture|Required Runtime|
|-|-|-|-|
|`net48 x64`|.NET Framework 4.8|64-bit|Built into Windows 10/11 ([.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48) for older systems)|
|`net48 x86`|.NET Framework 4.8|32-bit|Built into Windows 10/11 ([.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48) for older systems)|
|`net8.0 x64`|.NET 8.0|64-bit|[.NET 8.0 Desktop Runtime (x64)](https://dotnet.microsoft.com/download/dotnet/8.0)|
|`net8.0 x86`|.NET 8.0|32-bit|[.NET 8.0 Desktop Runtime (x86)](https://dotnet.microsoft.com/download/dotnet/8.0)|
|`net10.0 x64`|.NET 10.0|64-bit|[.NET 10.0 Desktop Runtime (x64)](https://dotnet.microsoft.com/download/dotnet/10.0)|
|`net10.0 x86`|.NET 10.0|32-bit|[.NET 10.0 Desktop Runtime (x86)](https://dotnet.microsoft.com/download/dotnet/10.0)|

> **Which one do I need?** If you are not sure, use **`net48 x64`**: it runs on any 64-bit
> Windows 10/11 without installing anything.
> For the .NET 8.0 / 10.0 builds you need the **Desktop Runtime** of that version;
> the plain ".NET Runtime" is not enough.

To check which .NET runtimes are installed, open a command prompt and run:

```
dotnet --list-runtimes
```

Look for a `Microsoft.WindowsDesktop.App 8.x` or `10.x` line.



## Install

1. Download and extract the folder that matches your system (see the table above), e.g. `net48 x64`.
2. Copy the **whole folder** to a permanent location, for example:
   `D:\My Apps\Fireworks ScreenSaver\`
   The folder must contain all of its files; the screensaver won't start if any of them is missing:
   * `net48` folders: `FireworksSS.scr`, `FireworksSS.scr.config`
   * `net8.0` / `net10.0` folders: `FireworksSS.scr`, `FireworksSS.dll`, `FireworksSS.dll.config`,
     `FireworksSS.deps.json`, `FireworksSS.runtimeconfig.json`
3. Right-click `FireworksSS.scr` and choose **Install**.
   The Windows **Screen Saver Settings** dialog opens with *FireworksSS* selected.
4. Set the **Wait** time and click **OK**.

> If Windows blocks the file ("Windows protected your PC"), right-click `FireworksSS.scr` →
> **Properties** → check **Unblock** → **OK**, then try again.



## Test it

* In **Screen Saver Settings**, click **Preview**, or
* double-click `FireworksSS.scr`.

Rockets launch from the bottom of the screen, climb, and burst into colorful sparks
(sometimes two-tone) that drift down and fade out.
Press any key, click, or move the mouse to exit.

The small monitor in **Screen Saver Settings** shows a live, scaled-down preview.



## Settings

Open **Screen Saver Settings** (Start → type *screen saver*), select *FireworksSS*, and click **Settings...**

|Setting|Range|Default|Description|
|-|-|-|-|
|**Launch rate**|1 - 10 rockets / second|2|How many rockets are launched per second, on average|
|**Burst size**|20 - 300 particles|110|How many sparks each explosion makes, on average (each burst varies a little)|
|**Particle life**|300 - 5000 milliseconds|1100|How long the sparks last before fading out, on average|

Settings are saved when you close the dialog.
They are stored per user in the registry under `HKEY_CURRENT_USER\Software\Remus RIGO\FireworksSS.VB-NET`.
Right-click the title bar → **About...** for version information.

> High values for all three settings mean many particles on screen at once,
> which uses more CPU on slower computers.



## Multi-monitor

The fireworks cover all monitors at once, including ones with different resolutions or scaling.



## Uninstall

1. In **Screen Saver Settings**, choose another screensaver (or *None*) and click **OK**.
2. Delete the folder you copied in step 2 of *Install*.
3. Optional: delete the registry key `HKEY_CURRENT_USER\Software\Remus RIGO\FireworksSS.VB-NET`.
