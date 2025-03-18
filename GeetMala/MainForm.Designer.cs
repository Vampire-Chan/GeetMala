namespace MusicPlayerWinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ListBox listBoxResults;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.TrackBar volumeTrackBar;
        private System.Windows.Forms.TrackBar trackBarPlayback; // Changed from ProgressBar to TrackBar
        private System.Windows.Forms.Button themeToggleButton;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.RadioButton radioButtonAudioOnly;
        private System.Windows.Forms.RadioButton radioButtonVideoWithAudio;
       
        /// <summary>
        /// Clean up any resources being used.
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
        /// Method required for Designer support.
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            searchTextBox = new TextBox();
            searchButton = new Button();
            listBoxResults = new ListBox();
            playButton = new Button();
            pauseButton = new Button();
            stopButton = new Button();
            nextButton = new Button();
            previousButton = new Button();
            volumeTrackBar = new TrackBar();
            trackBarPlayback = new TrackBar();
            themeToggleButton = new Button();
            downloadButton = new Button();
            radioButtonAudioOnly = new RadioButton();
            radioButtonVideoWithAudio = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)volumeTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarPlayback).BeginInit();
            SuspendLayout();
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(12, 12);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(400, 29);
            searchTextBox.TabIndex = 0;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(420, 12);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(80, 29);
            searchButton.TabIndex = 1;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += SearchButton_Click;
            // 
            // listBoxResults
            // 
            listBoxResults.FormattingEnabled = true;
            listBoxResults.Location = new Point(12, 50);
            listBoxResults.Name = "listBoxResults";
            listBoxResults.Size = new Size(488, 109);
            listBoxResults.TabIndex = 2;
            // 
            // playButton
            // 
            playButton.Location = new Point(12, 190);
            playButton.Name = "playButton";
            playButton.Size = new Size(80, 30);
            playButton.TabIndex = 3;
            playButton.Text = "Play";
            playButton.UseVisualStyleBackColor = true;
            playButton.Click += PlayButton_Click;
            // 
            // pauseButton
            // 
            pauseButton.Location = new Point(120, 190);
            pauseButton.Name = "pauseButton";
            pauseButton.Size = new Size(80, 30);
            pauseButton.TabIndex = 4;
            pauseButton.Text = "Pause";
            pauseButton.UseVisualStyleBackColor = true;
            pauseButton.Click += PauseButton_Click;
            // 
            // stopButton
            // 
            stopButton.Location = new Point(220, 190);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(80, 30);
            stopButton.TabIndex = 5;
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += StopButton_Click;
            // 
            // nextButton
            // 
            nextButton.Location = new Point(420, 190);
            nextButton.Name = "nextButton";
            nextButton.Size = new Size(80, 30);
            nextButton.TabIndex = 7;
            nextButton.Text = "Next";
            nextButton.UseVisualStyleBackColor = true;
            nextButton.Click += NextButton_Click;
            // 
            // previousButton
            // 
            previousButton.Location = new Point(321, 190);
            previousButton.Name = "previousButton";
            previousButton.Size = new Size(80, 30);
            previousButton.TabIndex = 6;
            previousButton.Text = "Previous";
            previousButton.UseVisualStyleBackColor = true;
            previousButton.Click += PreviousButton_Click;
            // 
            // volumeTrackBar
            // 
            volumeTrackBar.Location = new Point(12, 240);
            volumeTrackBar.Maximum = 100;
            volumeTrackBar.Value = 50;
            volumeTrackBar.Minimum = 0;
            volumeTrackBar.Name = "volumeTrackBar";
            volumeTrackBar.Size = new Size(200, 45);
            volumeTrackBar.TabIndex = 8;
            volumeTrackBar.TickFrequency = 10;
            volumeTrackBar.Scroll += VolumeTrackBar_Scroll;
            // 
            // trackBarPlayback
            // 
            trackBarPlayback.Location = new Point(12, 330);
            trackBarPlayback.Maximum = 100;
            trackBarPlayback.Name = "trackBarPlayback";
            trackBarPlayback.Size = new Size(488, 45);
            trackBarPlayback.TabIndex = 9;
            trackBarPlayback.TickFrequency = 10;
            trackBarPlayback.Scroll += TrackBarPlayback_Scroll;
            // 
            // themeToggleButton
            // 
            themeToggleButton.Location = new Point(220, 240);
            themeToggleButton.Name = "themeToggleButton";
            themeToggleButton.Size = new Size(100, 30);
            themeToggleButton.TabIndex = 10;
            themeToggleButton.Text = "Toggle Theme";
            themeToggleButton.UseVisualStyleBackColor = true;
            themeToggleButton.Click += ThemeToggleButton_Click;
            // 
            // downloadButton
            // 
            downloadButton.Location = new Point(340, 240);
            downloadButton.Name = "downloadButton";
            downloadButton.Size = new Size(160, 30);
            downloadButton.TabIndex = 11;
            downloadButton.Text = "Download Audio";
            downloadButton.UseVisualStyleBackColor = true;
            downloadButton.Click += DownloadButton_Click;
            // 
            // radioButtonAudioOnly
            // 
            radioButtonAudioOnly.AutoSize = true;
            radioButtonAudioOnly.Checked = true;
            radioButtonAudioOnly.Location = new Point(12, 292);
            radioButtonAudioOnly.Name = "radioButtonAudioOnly";
            radioButtonAudioOnly.Size = new Size(106, 25);
            radioButtonAudioOnly.TabIndex = 12;
            radioButtonAudioOnly.TabStop = true;
            radioButtonAudioOnly.Text = "Audio Only";
            radioButtonAudioOnly.UseVisualStyleBackColor = true;
            radioButtonAudioOnly.CheckedChanged += radioButtonAudioOnly_CheckedChanged;
            // 
            // radioButtonVideoWithAudio
            // 
            radioButtonVideoWithAudio.AutoSize = true;
            radioButtonVideoWithAudio.Location = new Point(120, 292);
            radioButtonVideoWithAudio.Name = "radioButtonVideoWithAudio";
            radioButtonVideoWithAudio.Size = new Size(147, 25);
            radioButtonVideoWithAudio.TabIndex = 13;
            radioButtonVideoWithAudio.Text = "Video with Audio";
            radioButtonVideoWithAudio.UseVisualStyleBackColor = true;
            radioButtonVideoWithAudio.CheckedChanged += radioButtonVideoWithAudio_CheckedChanged;
            // 
            // MainForm
            // 
            ClientSize = new Size(512, 366);
            Controls.Add(radioButtonVideoWithAudio);
            Controls.Add(radioButtonAudioOnly);
            Controls.Add(downloadButton);
            Controls.Add(themeToggleButton);
            Controls.Add(trackBarPlayback);
            Controls.Add(volumeTrackBar);
            Controls.Add(nextButton);
            Controls.Add(previousButton);
            Controls.Add(stopButton);
            Controls.Add(pauseButton);
            Controls.Add(playButton);
            Controls.Add(listBoxResults);
            Controls.Add(searchButton);
            Controls.Add(searchTextBox);
            Name = "MainForm";
            Text = "YouTube Music Player";
            ((System.ComponentModel.ISupportInitialize)volumeTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarPlayback).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}