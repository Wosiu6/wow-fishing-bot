using NAudio.CoreAudioApi;
using System.Diagnostics;

namespace Chrome
{
    public partial class Chrome : Form
    {
        public Chrome()
        {
            InitializeComponent();
            MMDeviceEnumerator deviceEnumerator = new();
            MMDeviceCollection? devices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
            cbOutput.Items.AddRange(devices.ToArray());
            Text = Guid.NewGuid().ToString();
            cbOutput.Text = "Please select WoW's output device.";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Process? process = Process.GetProcessesByName(Config.ProcessName).FirstOrDefault();
            bool isOutputSelected = cbOutput.SelectedIndex != -1;

            if (!tTick.Enabled && isOutputSelected && process is not null)
            {
                clickButton();
                Thread.Sleep(1500);

                tTick.Start();
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
            object? selectedOutput = cbOutput.SelectedItem;
            bool isOutputSelected = cbOutput.SelectedIndex != -1;

            switch (tTick.Enabled)
            {
                case true: lblState.Text = "Running";
                    break;
                case false: lblState.Text = "Not Running";
                    break;
            }

            if (isOutputSelected)
            {
                int currentVolume = (int)(((MMDevice)selectedOutput).AudioMeterInformation.MasterPeakValue * 100);
                pbVolume.Value = currentVolume;
                lblVolume.Text = currentVolume.ToString();

                if (currentVolume > Config.VolumeTreshold)
                {
                    clickButton();

                    Thread.Sleep(Config.DelayInMs);
                    clickButton();
                    Thread.Sleep(Config.DelayInMs);
                }
            }
            else
            {
                MessageBox.Show("Audio device needs to be set");
                StopTimer();
            }
        }

        private void StopTimer()
        {
            if (tTick.Enabled)
            {
                tTick.Stop();
                lblState.Text = "Not Running";
            }
        }

        private static void clickButton()
        {
            Process? process = Process.GetProcessesByName(Config.ProcessName).FirstOrDefault();

            if (process != null)
            {
                IntPtr mainHandle = process.MainWindowHandle;
                SetForegroundWindow(mainHandle);
                SendKeys.SendWait(Config.InteractKey);
            }
            else
            {
                MessageBox.Show("World of Warcraft not running");
            }
        }

        private void miDelay_Click(object sender, EventArgs e)
        {
            using (Prompt prompt = new Prompt("Input new Delay In Ms", "Delay In Ms"))
            {
                string? res = prompt.Result;

                if (int.TryParse(res, out int newValue))
                {
                    Config.DelayInMs = newValue;
                }
            }
        }

        private void miVolume_Click(object sender, EventArgs e)
        {
            using (Prompt prompt = new Prompt("Input new volume treshold", "Volume Treshold"))
            {
                string? res = prompt.Result;

                if (int.TryParse(res, out int newValue))
                {
                    Config.VolumeTreshold = newValue;
                }
            }
        }

        private void miProcess_Click(object sender, EventArgs e)
        {
            using (Prompt prompt = new Prompt("Input new process name", "Process Name"))
            {
                Config.ProcessName = prompt.Result;
            }
        }

        private void miInteractKey_Click(object sender, EventArgs e)
        {
            using (Prompt prompt = new Prompt("Input new interact key", "Interact Key"))
            {
                Config.InteractKey = prompt.Result;
            }
        }
    }
    public class Prompt : IDisposable
    {
        private Form prompt { get; set; }
        public string Result { get; }

        public Prompt(string text, string caption)
        {
            Result = ShowDialog(text, caption);
        }

        private string ShowDialog(string description, string title)
        {
            prompt = new Form()
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
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        public void Dispose()
        {
            if (prompt != null)
            {
                prompt.Dispose();
            }
        }
    }
}