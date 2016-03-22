using System.Diagnostics;
using System.Windows;

namespace WorkWithDB.UI.Debug
{
    public static class GuiDebug
    {
        private static bool? inDesignerMode;

        public static bool InDesignerMode
        {
            get
            {
                if (inDesignerMode == null)
                {
                    //Debugger.Launch();
                    inDesignerMode =
                        Process.GetCurrentProcess().MainModule.FileName.Contains("devenv.exe")  ||
                        Process.GetCurrentProcess().MainModule.FileName.Contains("XDesProc.exe")||
                        Process.GetCurrentProcess().MainModule.FileName.Contains("Blend.exe");
                }
                return inDesignerMode.Value;
            }
        }
    }
}