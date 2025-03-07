using System.Drawing;
using System.Windows.Forms;

namespace MusicPlayerWinForms
{
    partial class SettingsForm
    {
        private CheckBox chkShuffle;
        private CheckBox chkLoop;
        private RadioButton rbYouTube;
        private RadioButton rbSpotify;
        private Label lblPlaybackSpeed;
        private NumericUpDown nudPlaybackSpeed;
        private RadioButton rbLightTheme;
        private RadioButton rbDarkTheme;
        private Button btnSave;

        private void InitializeComponent()
        {
            this.chkShuffle = new CheckBox();
            this.chkLoop = new CheckBox();
            this.rbYouTube = new RadioButton();
            this.rbSpotify = new RadioButton();
            this.lblPlaybackSpeed = new Label();
            this.nudPlaybackSpeed = new NumericUpDown();
            this.rbLightTheme = new RadioButton();
            this.rbDarkTheme = new RadioButton();
            this.btnSave = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.nudPlaybackSpeed)).BeginInit();
            this.SuspendLayout();

            // SettingsForm properties
            this.Text = "Settings";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterParent;

            // chkShuffle
            this.chkShuffle.Location = new Point(20, 20);
            this.chkShuffle.Text = "Enable Shuffle";
            this.chkShuffle.AutoSize = true;

            // chkLoop
            this.chkLoop.Location = new Point(20, 50);
            this.chkLoop.Text = "Enable Loop";
            this.chkLoop.AutoSize = true;

            // rbYouTube
            this.rbYouTube.Location = new Point(20, 80);
            this.rbYouTube.Text = "YouTube";
            this.rbYouTube.AutoSize = true;
            this.rbYouTube.Checked = true;

            // rbSpotify
            this.rbSpotify.Location = new Point(20, 110);
            this.rbSpotify.Text = "Spotify";
            this.rbSpotify.AutoSize = true;

            // lblPlaybackSpeed
            this.lblPlaybackSpeed.Location = new Point(20, 140);
            this.lblPlaybackSpeed.Text = "Playback Speed:";
            this.lblPlaybackSpeed.AutoSize = true;

            // nudPlaybackSpeed
            this.nudPlaybackSpeed.Location = new Point(140, 138);
            this.nudPlaybackSpeed.DecimalPlaces = 2;
            this.nudPlaybackSpeed.Increment = 0.1M;
            this.nudPlaybackSpeed.Minimum = 0.5M;
            this.nudPlaybackSpeed.Maximum = 2.0M;
            this.nudPlaybackSpeed.Value = 1.0M;

            // rbLightTheme
            this.rbLightTheme.Location = new Point(20, 170);
            this.rbLightTheme.Text = "Light Theme";
            this.rbLightTheme.AutoSize = true;
            this.rbLightTheme.Checked = true;

            // rbDarkTheme
            this.rbDarkTheme.Location = new Point(20, 200);
            this.rbDarkTheme.Text = "Dark Theme";
            this.rbDarkTheme.AutoSize = true;

            // btnSave
            this.btnSave.Location = new Point(150, 230);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // Add controls to the form.
            this.Controls.Add(this.chkShuffle);
            this.Controls.Add(this.chkLoop);
            this.Controls.Add(this.rbYouTube);
            this.Controls.Add(this.rbSpotify);
            this.Controls.Add(this.lblPlaybackSpeed);
            this.Controls.Add(this.nudPlaybackSpeed);
            this.Controls.Add(this.rbLightTheme);
            this.Controls.Add(this.rbDarkTheme);
            this.Controls.Add(this.btnSave);

            ((System.ComponentModel.ISupportInitialize)(this.nudPlaybackSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}