# Cha-Ching

A Dalamud plugin for Final Fantasy XIV that provides visual notifications (FlyText) for gil gains and losses, and displays your current gil in a dedicated UI window.

## Features

*   **Gil Change Notifications:** Get instant FlyText notifications directly on your character for gil gains and losses.
    *   **Gains:** Displayed in yellow with a `+` prefix.
    *   **Losses:** Displayed in red with a `-` prefix.
*   **Configurable Popups:** Easily enable or disable notifications for gil losses via the plugin's configuration UI.
*   **Current Gil Display:** A dedicated main UI window to continuously display your current gil amount.

## Installation

Cha-Ching is a Dalamud plugin and requires [XIVLauncher](https://github.com/goatcorp/FFXIVQuickLauncher/releases) to be installed.

### Local Installation

1.  Launch the game using **XIVLauncher**.
2.  Log in to a character.
3.  In the chat box, type the command `/xlplugins` to open the Dalamud plugin installer.
4.  Go to the **"Settings"** tab.
5.  Scroll down to the **"Experimental"** section.
6.  Click the **"+"** button next to "Dev Plugins".
7.  In the text box that appears, enter the path to your built plugin's debug dll.
8.  Click the **"Save and Close"** button.

## Usage

### Configuration UI

To access the plugin's settings:
1.  Open the Dalamud plugin installer (`/xlplugins`).
2.  Find "Cha-Ching" in your installed plugins.
3.  Click the **"Config"** button.

Here you will find the **"Enable Negative Popups"** checkbox. Toggle this to enable or disable FlyText notifications for gil losses.

### Main UI (Current Gil Display)

To open the window displaying your current gil:
1.  Open the Dalamud plugin installer (`/xlplugins`).
2.  Find "Cha-Ching" in your installed plugins.
3.  Click the **"Main"** button.

Your current gil amount will be displayed in this window and will update in real-time.

## Building from Source (Optional)

If you wish to build Cha-Ching from its source code, you will need:
*   **Final Fantasy XIV** installed.
*   **XIVLauncher** installed.
*   **.NET 8 SDK (x64)** from the [official Microsoft .NET website](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).
*   An IDE like **Visual Studio 2022 Community** (with ".NET desktop development" workload) or **JetBrains Rider**.

1.  Clone the repository:
    `git clone https://github.com/iamkaf/ChaChing.git` (Replace with actual repo link)
2.  Navigate to the plugin directory:
    `cd ChaChing/ChaChing`
3.  Build the project:
    `dotnet build`

The compiled `.dll` will be in `ChaChing/ChaChing/bin/Debug/` (or `Release/`).

## Credits and Acknowledgements

*   Developed using the **Dalamud** plugin framework.
*   Utilizes **FFXIVClientStructs** for direct game memory access.
*   Inspired by the **PatMe** plugin for its configuration and UI patterns.

---

**Note:** This plugin is a third-party tool and is not officially supported by Square Enix. Use at your own discretion.