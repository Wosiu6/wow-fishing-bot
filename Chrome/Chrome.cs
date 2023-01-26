using Chrome.Configuration;
using Chrome.Utils;
using NAudio.CoreAudioApi;

namespace Chrome
{
    public partial class Chrome : Form
    {
        private static bool s_isRunning = false;
        private static int s_castTimeRemaining = 22;

        public Chrome()
        {
            InitializeComponent();

            Text = Guid.NewGuid().ToString();

            MMDeviceEnumerator deviceEnumerator = new();
            MMDeviceCollection? devices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
            cbOutput.Items.AddRange(devices.ToArray());
            cbOutput.Text = $"Please select {Config.ProcessName}'s output device.";
            lblVolumeTreshold.Text = $"Volume treshold : {Config.VolumeTreshold}";

            btnStop.Enabled = s_isRunning;
        }

        private void StartExecution()
        {
            bool isOutputSelected = cbOutput.SelectedIndex != -1;

            if (!isOutputSelected) MessageBox.Show("Choose output device");
            else if (!tTick.Enabled)
            {
                SendButtonPress();
                Thread.Sleep(Config.DelayInMs);

                tTick.Start();
                s_isRunning = true;
                UpdateButtonsAndLabel();
            }
        }

        private void PauseExecution()
        {
            if (tTick.Enabled) tTick.Stop();
            if (tCast.Enabled) tCast.Stop();

            s_isRunning = false;

            UpdateButtonsAndLabel();
        }

        private void ExecuteTick()
        {
            UpdateButtonsAndLabel();

            object? selectedOutput = cbOutput.SelectedItem;
            bool isOutputSelected = cbOutput.SelectedIndex != -1;
            bool isFakeCast = new Random().Next(0, 100) > Config.CastFailChance;
            int delayForTick = new Random().Next(Config.DelayInMs - 231, Config.DelayInMs + 219);

            if (isOutputSelected)
            {
                int currentVolume = (int)(((MMDevice)selectedOutput).AudioMeterInformation.MasterPeakValue * 100);

                pbVolume.Value = currentVolume;
                lblVolume.Text = $"Current volume: {currentVolume}";

                if (currentVolume > Config.VolumeTreshold)
                {
                    if (isFakeCast) Thread.Sleep(delayForTick + 5000);

                    SendButtonPress(delayForTick + 37);
                    SendButtonPress(delayForTick - 79);
                }
            }
            else
            {
                PauseExecution();
                MessageBox.Show("Audio device needs to be set");
            }
        }

        private void UpdateButtonsAndLabel()
        {
            btnStop.Enabled = s_isRunning;
            btnStart.Enabled = !s_isRunning;
            lblState.Text = s_isRunning ? "Running" : "Stopped";
        }

        private void SendButtonPress(int delayAfterButtonPress = 0)
        {
            IOUtils.SendDefaultButtonPress();

            s_castTimeRemaining = 22;
            tCast.Start();

            Thread.Sleep(delayAfterButtonPress);
        }

#pragma warning disable IDE1006 // Naming Styles

        #region Button Clicks
        private void btnStart_Click(object sender, EventArgs e)
        {
            StartExecution();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            PauseExecution();
        }
        #endregion

        #region Timer Ticks
        private void tTick_Tick(object sender, EventArgs e)
        {
            ExecuteTick();
        }

        private void tCast_tick(object sender, EventArgs e)
        {
            s_castTimeRemaining--;
            if (s_castTimeRemaining < 1) SendButtonPress();
        }
        #endregion

        #region Menu Item Clicks
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
        #endregion

#pragma warning restore IDE1006 // Naming Styles
    }
}