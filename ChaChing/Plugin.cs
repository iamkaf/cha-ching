using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using System; // For Math.Abs
using Dalamud.Interface.Colors; // For ImGuiColors
using Dalamud.Game.Gui.FlyText;
using FFXIVClientStructs.FFXIV.Client.Game; // For InventoryManager and InventoryType
using Dalamud.Utility.Numerics;
using Dalamud.Interface.Windowing; // For WindowSystem

namespace ChaChing
{
    public sealed unsafe class Plugin : IDalamudPlugin
    {
        public string Name => "Cha-Ching";

        [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;

        private ConfigWindow ConfigWindow;
        private MainWindow MainWindow;
        private WindowSystem WindowSystem = new("ChaChing");

        private long _lastKnownGil = 0;

        public Plugin()
        {
            PluginInterface.Create<Service>(); // Initialize static services

            Service.plugin = this;
            Service.pluginInterface = PluginInterface;
            Service.pluginConfig = Service.pluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            Service.pluginConfig.Initialize();

            WindowSystem.AddWindow(ConfigWindow = new ConfigWindow());

            MainWindow = new MainWindow();
            WindowSystem.AddWindow(MainWindow);

            // Initialize last known gil
            _lastKnownGil = GetCurrentGil();

            // Register UI callbacks
            PluginInterface.UiBuilder.Draw += WindowSystem.Draw;
            PluginInterface.UiBuilder.OpenConfigUi += ConfigWindow.Toggle;
            PluginInterface.UiBuilder.OpenMainUi += MainWindow.Toggle;

            Service.ChatGui.ChatMessage += OnChatMessage; // Subscribe to chat messages
            Service.Framework.Update += OnFrameworkUpdate; // Subscribe to framework updates
        }

        public void Dispose()
        {
            Service.ChatGui.ChatMessage -= OnChatMessage; // Unsubscribe
            Service.Framework.Update -= OnFrameworkUpdate; // Unsubscribe from framework updates

            // Dispose UI
            WindowSystem.RemoveAllWindows();
            ConfigWindow.Dispose();
            MainWindow.Dispose();

            // Unsubscribe from UI callbacks
            PluginInterface.UiBuilder.Draw -= WindowSystem.Draw;
            PluginInterface.UiBuilder.OpenConfigUi -= ConfigWindow.Toggle;
            PluginInterface.UiBuilder.OpenMainUi -= MainWindow.Toggle;
        }

        private void OnChatMessage(XivChatType type, int timestamp, ref SeString sender, ref SeString message, ref bool isHandled)
        {
            // We only care about system messages. Gil gain messages are type 2110.
            if (type != (XivChatType)2110)
            {
                return;
            }

            // The gil change will be detected by OnFrameworkUpdate
        }

        private void OnOpenConfigUi()
        {
            // This plugin does not have a configuration UI.
            // This method is implemented to satisfy Dalamud's validation.
        }

        private void OnFrameworkUpdate(IFramework framework)
        {
            long currentGil = GetCurrentGil();

            if (currentGil != _lastKnownGil)
            {
                long gilChange = currentGil - _lastKnownGil;
                if (gilChange > 0)
                {
                    ShowGilFlyText((uint)gilChange, true);
                }
                else if (gilChange < 0 && Service.pluginConfig.EnableNegativePopups)
                {
                    ShowGilFlyText((uint)Math.Abs(gilChange), false);
                }
                _lastKnownGil = currentGil;
            }
        }

        private void ShowGilFlyText(uint gilAmount, bool isGain)
        {
            if (gilAmount < 1)
            {
                return;
            }

            string prefix = isGain ? "+" : "-";
            uint color = isGain ? ImGuiColors.ParsedGreen.ToByteColor().RGBA : ImGuiColors.DalamudRed.ToByteColor().RGBA;

            Service.FlyTextGui.AddFlyText(FlyTextKind.Buff, 0, 0, 0, new SeStringBuilder().AddText($"{prefix}{gilAmount:n0}{SeIconChar.Gil}").Build(), SeString.Empty, color, 0, 0);
        }

        public static long GetCurrentGil()
        {
            InventoryManager* inventoryManager = InventoryManager.Instance();
            if (inventoryManager == null)
            {
                            Service.PluginLog.Warning("InventoryManager is null.");
                return 0;
            }

            long gilAmount = inventoryManager->GetGil();
            return gilAmount;
        }
    }
}