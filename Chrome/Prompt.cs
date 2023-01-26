namespace Chrome
{
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
