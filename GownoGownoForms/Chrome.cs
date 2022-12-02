using NAudio.CoreAudioApi;
using System.Diagnostics;

namespace GownoGownoForms
{
    public partial class Chrome : Form
    {
        private static string keyToCLick = ".";

        public Chrome()
        {
            InitializeComponent();
            MMDeviceEnumerator deviceEnumerator = new();
            MMDeviceCollection? devices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
            cbOutput.Items.AddRange(devices.ToArray());
            Text = Guid.NewGuid().ToString();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Process? process = Process.GetProcessById(27704);

            if (!tTick.Enabled && cbOutput.SelectedItem is not null)
            {
                clickButton();
                Thread.Sleep(1500);

                tTick.Start();
            }
            else if (process is not null)
            {
                MessageBox.Show("Choose output device");
            }
            else
            {
                MessageBox.Show("WoW not running");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (tTick.Enabled) tTick.Stop();
        }

        private void tTick_Tick(object sender, EventArgs e)
        {
            object? selectedOutput = cbOutput.SelectedItem;

            if (selectedOutput is not null)
            {
                int currentVolume = (int)(((MMDevice)selectedOutput).AudioMeterInformation.MasterPeakValue * 100);
                pbVolume.Value = currentVolume;
                lblVolume.Text = currentVolume.ToString();

                if (currentVolume > 30)
                {
                    clickButton();

                    Thread.Sleep(1500);
                    clickButton();
                    Thread.Sleep(1500);
                }
            }
        }

        private static void clickButton()
        {
            Process? process = Process.GetProcessById(27704);

            if (process != null)
            {
                IntPtr mainHandle = process.MainWindowHandle;
                SetForegroundWindow(mainHandle);
                SendKeys.SendWait(keyToCLick);
            }
            else
            {
                MessageBox.Show("World of Warcraft not running");
            }
        }
    }
}