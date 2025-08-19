using Dalamud.Interface.Windowing;
using Dalamud.Bindings.ImGui;
using System;
using System.Numerics;

namespace ChaChing
{
    public class MainWindow : Window, IDisposable
    {
        public MainWindow() : base("Cha-Ching Current Gil")
        {
            IsOpen = false;

            SizeConstraints = new WindowSizeConstraints
            {
                MinimumSize = new Vector2(200, 50),
                MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
            };
        }

        public void Dispose() { }

        public override void Draw()
        {
            long currentGil = Plugin.GetCurrentGil(); // Access static method from Plugin
            ImGui.Text($"Current Gil: {currentGil:N0}");
        }
    }
}