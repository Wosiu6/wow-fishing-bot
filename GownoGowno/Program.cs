using System.Diagnostics;
using System.Runtime.InteropServices;

[DllImport("User32.dll")]
static extern int SetForegroundWindow(IntPtr point);


