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
            chkShuffle = new CheckBox();
            chkLoop = new CheckBox();
            rbYouTube = new RadioButton();
            rbSpotify = new RadioButton();
            lblPlaybackSpeed = new Label();
            nudPlaybackSpeed = new NumericUpDown();
            rbLightTheme = new RadioButton();
            rbDarkTheme = new RadioButton();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)nudPlaybackSpeed).BeginInit();
            SuspendLayout();
            // 
            // chkShuffle
            // 
            chkShuffle.AutoSize = true;
            chkShuffle.Location = new Point(20, 20);
            chkShuffle.Name = "chkShuffle";
            chkShuffle.Size = new Size(128, 25);
            chkShuffle.TabIndex = 0;
            chkShuffle.Text = "Enable Shuffle";
            // 
            // chkLoop
            // 
            chkLoop.AutoSize = true;
            chkLoop.Location = new Point(20, 50);
            chkLoop.Name = "chkLoop";
            chkLoop.Size = new Size(114, 25);
            chkLoop.TabIndex = 1;
            chkLoop.Text = "Enable Loop";
            // 
            // rbYouTube
            // 
            rbYouTube.AutoSize = true;
            rbYouTube.Checked = true;
            rbYouTube.Location = new Point(20, 80);
            rbYouTube.Name = "rbYouTube";
            rbYouTube.Size = new Size(88, 25);
            rbYouTube.TabIndex = 2;
            rbYouTube.TabStop = true;
            rbYouTube.Text = "YouTube";
            // 
            // rbSpotify
            // 
            rbSpotify.AutoSize = true;
            rbSpotify.Location = new Point(20, 110);
            rbSpotify.Name = "rbSpotify";
            rbSpotify.Size = new Size(77, 25);
            rbSpotify.TabIndex = 3;
            rbSpotify.Text = "Spotify";
            // 
            // lblPlaybackSpeed
            // 
            lblPlaybackSpeed.AutoSize = true;
            lblPlaybackSpeed.Location = new Point(20, 140);
            lblPlaybackSpeed.Name = "lblPlaybackSpeed";
            lblPlaybackSpeed.Size = new Size(121, 21);
            lblPlaybackSpeed.TabIndex = 4;
            lblPlaybackSpeed.Text = "Playback Speed:";
            // 
            // nudPlaybackSpeed
            // 
            nudPlaybackSpeed.DecimalPlaces = 2;
            nudPlaybackSpeed.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudPlaybackSpeed.Location = new Point(147, 138);
            nudPlaybackSpeed.Maximum = new decimal(new int[] { 20, 0, 0, 65536 });
            nudPlaybackSpeed.Minimum = new decimal(new int[] { 5, 0, 0, 65536 });
            nudPlaybackSpeed.Name = "nudPlaybackSpeed";
            nudPlaybackSpeed.Size = new Size(120, 29);
            nudPlaybackSpeed.TabIndex = 5;
            nudPlaybackSpeed.Value = new decimal(new int[] { 10, 0, 0, 65536 });
            // 
            // rbLightTheme
            // 
            rbLightTheme.AutoSize = true;
            rbLightTheme.Checked = true;
            rbLightTheme.Location = new Point(20, 170);
            rbLightTheme.Name = "rbLightTheme";
            rbLightTheme.Size = new Size(114, 25);
            rbLightTheme.TabIndex = 6;
            rbLightTheme.TabStop = true;
            rbLightTheme.Text = "Light Theme";
            // 
            // rbDarkTheme
            // 
            rbDarkTheme.AutoSize = true;
            rbDarkTheme.Location = new Point(20, 200);
            rbDarkTheme.Name = "rbDarkTheme";
            rbDarkTheme.Size = new Size(112, 25);
            rbDarkTheme.TabIndex = 7;
            rbDarkTheme.Text = "Dark Theme";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(150, 230);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 33);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // SettingsForm
            // 
            ClientSize = new Size(384, 272);
            Controls.Add(chkShuffle);
            Controls.Add(chkLoop);
            Controls.Add(rbYouTube);
            Controls.Add(rbSpotify);
            Controls.Add(lblPlaybackSpeed);
            Controls.Add(nudPlaybackSpeed);
            Controls.Add(rbLightTheme);
            Controls.Add(rbDarkTheme);
            Controls.Add(btnSave);
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)nudPlaybackSpeed).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}