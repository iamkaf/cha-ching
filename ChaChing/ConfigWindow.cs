using Dalamud.Interface.Windowing;
using Dalamud.Bindings.ImGui;
using System;
using System.Numerics;

namespace ChaChing
{
    public class ConfigWindow : Window, IDisposable
    {
        public ConfigWindow() : base("Cha-Ching Configuration")
        {
            IsOpen = false;

            SizeConstraints = new WindowSizeConstraints() { MinimumSize = new Vector2(300, 100), MaximumSize = new Vector2(float.MaxValue, float.MaxValue) };
        }

        public void Dispose()
        {
        }

        public override void Draw()
        {
            bool enableNegativePopups = Service.pluginConfig.EnableNegativePopups;
            if (ImGui.Checkbox("Enable Negative Popups", ref enableNegativePopups))
            {
                Service.pluginConfig.EnableNegativePopups = enableNegativePopups;
                Service.pluginConfig.Save();
            }
        }
    }
}