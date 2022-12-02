using NAudio.CoreAudioApi;
using System.Diagnostics;

namespace Chrome
{
    public partial class Chrome : Form
    {
        public static bool IsRunning = false;

        public Chrome()
        {
            InitializeComponent();
            MMDeviceEnumerator deviceEnumerator = new();
            MMDeviceCollection? devices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
            cbOutput.Items.AddRange(devices.ToArray());
            Text = Guid.NewGuid().ToString();
            cbOutput.Text = "Please select WoW's output device.";
            btnStop.Enabled = IsRunning;
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
                StopTimer();
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

        private static void SendButtonPress()
        {
            Process? process = Process.GetProcessesByName(Config.ProcessName).FirstOrDefault();

            if (process != null)
            {
                IntPtr mainHandle = process.MainWindowHandle;
                _ = SetForegroundWindow(mainHandle);
                SendKeys.SendWait(Config.InteractKey);
            }
            else
            {
                MessageBox.Show("World of Warcraft not running");
            }
        }
        private void StopTimer()
        {
            if (tTick.Enabled)
            {
                tTick.Stop();
                IsRunning = false;
                UpdateButtonsAndLabel();
            }
        }

        private void UpdateButtonsAndLabel()
        {
            btnStop.Enabled = IsRunning;
            btnStart.Enabled = !IsRunning;
            lblState.Text = IsRunning ? "Running" : "Stopped";
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
                IsRunning = true;
                UpdateButtonsAndLabel();
            }
            else if (!isOutputSelected)
            {
                MessageBox.Show("Choose output device");
                StopTimer();
            }
            else
            {
                MessageBox.Show("WoW not running");
                StopTimer();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopTimer();
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
            Label textLabel = new Label() { Left = 50, Top = 20, Text = description };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "OK", Left = 375, Top = 80, DialogResult = DialogResult.OK };
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