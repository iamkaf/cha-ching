using Dalamud.Configuration;
using System;

namespace ChaChing
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        private static int VersionLatest = 0; // Start with version 0 for our plugin

        public int Version { get; set; } = VersionLatest;

        public bool EnableNegativePopups { get; set; } = false; // Off by default

        public void Initialize()
        {
            // No migration needed for version 0
            if (Version != VersionLatest)
            {
                Version = VersionLatest;
                Save();
            }
        }

        public void Save()
        {
            Service.pluginInterface.SavePluginConfig(this);
        }
    }
}