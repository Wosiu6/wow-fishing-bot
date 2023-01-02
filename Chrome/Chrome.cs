using NAudio.CoreAudioApi;
using System.Diagnostics;

namespace Chrome
{
    public partial class Chrome : Form
    {
        private static bool s_isRunning = false;
        private static int s_castTimeRemaining = 22;

        public Chrome()
        {
            InitializeComponent();
            MMDeviceEnumerator deviceEnumerator = new();
            MMDeviceCollection? devices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
            cbOutput.Items.AddRange(devices.ToArray());
            Text = Guid.NewGuid().ToString();
            cbOutput.Text = "Please select WoW's output device.";
            btnStop.Enabled = s_isRunning;
        }

        private void ExecuteTick()
        {
            UpdateButtonsAndLabel();

            object? selectedOutput = cbOutput.SelectedItem;
            bool isOutputSelected = cbOutput.SelectedIndex != -1;
            int delayForTick = new Random().Next(Config.DelayInMs - 231, Config.DelayInMs + 219);
            bool isFakeCast = new Random().Next(0, 100) > 90;

            if (isOutputSelected)
            {
                int currentVolume = (int)(((MMDevice)selectedOutput).AudioMeterInformation.MasterPeakValue * 100);

                pbVolume.Value = currentVolume;
                lblVolume.Text = currentVolume.ToString();

                if (currentVolume > Config.VolumeTreshold)
                {
                    ExecuteBobberClickAndRecast();
                }
            }
            else
            {
                MessageBox.Show("Audio device needs to be set");
                Stop();
            }

            void ExecuteBobberClickAndRecast()
            {
                if (isFakeCast) Thread.Sleep(delayForTick + 3000);

                SendButtonPress();

                Thread.Sleep(delayForTick + 37);
                SendButtonPress();
                Thread.Sleep(delayForTick - 79);
            }
        }

        private void SendButtonPress()
        {
            Process? process = Process.GetProcessesByName(Config.ProcessName).FirstOrDefault();

            if (process != null)
            {
                IntPtr mainHandle = process.MainWindowHandle;
                _ = SetForegroundWindow(mainHandle);
                SendKeys.SendWait(Config.InteractKey);

                s_castTimeRemaining = 22;
                tCast.Start();
            }
            else
            {
                MessageBox.Show("World of Warcraft not running");
            }
        }

        private void Stop()
        {
            if (tTick.Enabled)
            {
                tTick.Stop();
            }

            if (tCast.Enabled)
            {
                tCast.Stop();
            }

            s_isRunning = false;
            UpdateButtonsAndLabel();
        }

        private void UpdateButtonsAndLabel()
        {
            btnStop.Enabled = s_isRunning;
            btnStart.Enabled = !s_isRunning;
            lblState.Text = s_isRunning ? "Running" : "Stopped";
        }

        #region events
#pragma warning disable IDE1006 // Naming Styles
        private void btnStart_Click(object sender, EventArgs e)
        {
            Process? process = Process.GetProcessesByName(Config.ProcessName).FirstOrDefault();
            bool isOutputSelected = cbOutput.SelectedIndex != -1;

            if (!tTick.Enabled && isOutputSelected && process is not null)
            {
                SendButtonPress();
                Thread.Sleep(Config.DelayInMs);

                tTick.Start();
                s_isRunning = true;
                UpdateButtonsAndLabel();
            }
            else if (!isOutputSelected)
            {
                MessageBox.Show("Choose output device");
                Stop();
            }
            else
            {
                MessageBox.Show("WoW not running");
                Stop();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void tTick_Tick(object sender, EventArgs e)
        {
            ExecuteTick();
        }
        private void miDelay_Click(object sender, EventArgs e)
        {
            using Prompt prompt = new("Input new Delay In Ms", "Delay In Ms");
            string? res = prompt.Result;

            if (int.TryParse(res, out int newValue))
            {
                Config.DelayInMs = newValue;
            }
        }

        private void miVolume_Click(object sender, EventArgs e)
        {
            using Prompt prompt = new("Input new volume treshold", "Volume Treshold");
            string? res = prompt.Result;

            if (int.TryParse(res, out int newValue))
            {
                Config.VolumeTreshold = newValue;
            }
        }

        private void miProcess_Click(object sender, EventArgs e)
        {
            using Prompt prompt = new("Input new process name", "Process Name");
            Config.ProcessName = prompt.Result;
        }

        private void miInteractKey_Click(object sender, EventArgs e)
        {
            using Prompt prompt = new("Input new interact key", "Interact Key");
            Config.InteractKey = prompt.Result;
        }

        private void tCast_tick(object sender, EventArgs e)
        {
            s_castTimeRemaining--;
            if (s_castTimeRemaining < 1) SendButtonPress();
        }

#pragma warning restore IDE1006 // Naming Styles
        #endregion
    }
    public class Prompt : IDisposable
    {
        private Form Window { get; set; }
        public string Result { get; }

        public Prompt(string text, string caption) => Result = ShowDialog(text, caption);

        private string ShowDialog(string description, string title)
        {
            Window = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterScreen,
                TopMost = true
            };
            Label textLabel = new() { Left = 50, Top = 20, Text = description };
            TextBox textBox = new() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new() { Text = "OK", Left = 375, Top = 80, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { Window.Close(); };
            Window.Controls.Add(textBox);
            Window.Controls.Add(confirmation);
            Window.Controls.Add(textLabel);
            Window.AcceptButton = confirmation;

            return Window.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}