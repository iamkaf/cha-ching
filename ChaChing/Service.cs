using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace ChaChing
{
    internal class Service
    {
        public static Plugin plugin = null!;
        public static IDalamudPluginInterface pluginInterface = null!;
        public static Configuration pluginConfig = null!;

        [PluginService]
        public static ICommandManager CommandManager { get; private set; } = null!;

        [PluginService]
        public static IFlyTextGui FlyTextGui { get; private set; } = null!;

        [PluginService]
        public static IClientState ClientState { get; private set; } = null!;

        [PluginService]
        public static IChatGui ChatGui { get; private set; } = null!;

        [PluginService]
        public static IFramework Framework { get; private set; } = null!;

        [PluginService]
        public static IPluginLog PluginLog { get; private set; } = null!;
    }
}