using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Chrome
{
    partial class Chrome
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chrome));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.pbVolume = new System.Windows.Forms.ProgressBar();
            this.cbOutput = new System.Windows.Forms.ComboBox();
            this.lblVolume = new System.Windows.Forms.Label();
            this.tTick = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miInteractKey = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelay = new System.Windows.Forms.ToolStripMenuItem();
            this.miVolume = new System.Windows.Forms.ToolStripMenuItem();
            this.miProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.lblState = new System.Windows.Forms.Label();
            this.tCast = new System.Windows.Forms.Timer(this.components);
            this.lblVolumeTreshold = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(298, 159);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(379, 159);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // pbVolume
            // 
            this.pbVolume.Location = new System.Drawing.Point(11, 56);
            this.pbVolume.Name = "pbVolume";
            this.pbVolume.Size = new System.Drawing.Size(442, 90);
            this.pbVolume.TabIndex = 2;
            // 
            // cbOutput
            // 
            this.cbOutput.FormattingEnabled = true;
            this.cbOutput.Location = new System.Drawing.Point(12, 27);
            this.cbOutput.Name = "cbOutput";
            this.cbOutput.Size = new System.Drawing.Size(442, 23);
            this.cbOutput.TabIndex = 3;
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(12, 149);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(0, 15);
            this.lblVolume.TabIndex = 4;
            // 
            // tTick
            // 
            this.tTick.Tick += new System.EventHandler(this.tTick_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(465, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miInteractKey,
            this.miDelay,
            this.miVolume,
            this.miProcess});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // miInteractKey
            // 
            this.miInteractKey.Name = "miInteractKey";
            this.miInteractKey.Size = new System.Drawing.Size(161, 22);
            this.miInteractKey.Text = "Interact Key";
            this.miInteractKey.Click += new System.EventHandler(this.miInteractKey_Click);
            // 
            // miDelay
            // 
            this.miDelay.Name = "miDelay";
            this.miDelay.Size = new System.Drawing.Size(161, 22);
            this.miDelay.Text = "Delay";
            this.miDelay.Click += new System.EventHandler(this.miDelay_Click);
            // 
            // miVolume
            // 
            this.miVolume.Name = "miVolume";
            this.miVolume.Size = new System.Drawing.Size(161, 22);
            this.miVolume.Text = "Volume Treshold";
            this.miVolume.Click += new System.EventHandler(this.miVolume_Click);
            // 
            // miProcess
            // 
            this.miProcess.Name = "miProcess";
            this.miProcess.Size = new System.Drawing.Size(161, 22);
            this.miProcess.Text = "Process Name";
            this.miProcess.Click += new System.EventHandler(this.miProcess_Click);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(217, 163);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(75, 15);
            this.lblState.TabIndex = 7;
            this.lblState.Text = "Not Running";
            // 
            // tCast
            // 
            this.tCast.Interval = 1000;
            this.tCast.Tick += new System.EventHandler(this.tCast_tick);
            // 
            // lblVolumeTreshold
            // 
            this.lblVolumeTreshold.AutoSize = true;
            this.lblVolumeTreshold.Location = new System.Drawing.Point(12, 167);
            this.lblVolumeTreshold.Name = "lblVolumeTreshold";
            this.lblVolumeTreshold.Size = new System.Drawing.Size(103, 15);
            this.lblVolumeTreshold.TabIndex = 8;
            this.lblVolumeTreshold.Text = "Volume treshhold:";
            // 
            // Chrome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 188);
            this.Controls.Add(this.lblVolumeTreshold);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lblVolume);
            this.Controls.Add(this.cbOutput);
            this.Controls.Add(this.pbVolume);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Chrome";
            this.Text = "Chrome";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnStart;
        private Button btnStop;
        private ProgressBar pbVolume;
        private ComboBox cbOutput;
        private Label lblVolume;
        private System.Windows.Forms.Timer tTick;
        private ContextMenuStrip contextMenuStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem miInteractKey;
        private ToolStripMenuItem miDelay;
        private ToolStripMenuItem miVolume;
        private ToolStripMenuItem miProcess;
        private Label lblState;
        private System.Windows.Forms.Timer tCast;
        private Label lblVolumeTreshold;
    }
}