using Chrome.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Chrome.Utils
{
    public static class IOUtils
    {
        [DllImport("User32.dll")]
        private static extern int SetForegroundWindow(IntPtr point);

        public static void SendDefaultButtonPress()
        {
            if (SetForgroundWindow()) SendKeys.SendWait(Config.InteractKey);
        }

        public static bool SetForgroundWindow()
        {
            bool isProcessRunning = false;
            Process? process = Process.GetProcessesByName(Config.ProcessName).FirstOrDefault();

            if (process != null)
            {
                IntPtr mainHandle = process.MainWindowHandle;
                _ = SetForegroundWindow(mainHandle);

                isProcessRunning = true;
            }
            else
            {
                MessageBox.Show($"{Config.ProcessName} not running");
            }

            return isProcessRunning;
        }
    }
}
