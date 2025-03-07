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
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.TrackBar volumeTrackBar;
        private System.Windows.Forms.ProgressBar progressBarPlayback;
        private System.Windows.Forms.Button themeToggleButton;
        private System.Windows.Forms.Button downloadButton;

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
            this.components = new System.ComponentModel.Container();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.listBoxResults = new System.Windows.Forms.ListBox();
            this.playButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.previousButton = new System.Windows.Forms.Button();
            this.volumeTrackBar = new System.Windows.Forms.TrackBar();
            this.progressBarPlayback = new System.Windows.Forms.ProgressBar();
            this.themeToggleButton = new System.Windows.Forms.Button();
            this.downloadButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(12, 12);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(400, 23);
            this.searchTextBox.TabIndex = 0;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(420, 12);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(80, 23);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // listBoxResults
            // 
            this.listBoxResults.FormattingEnabled = true;
            this.listBoxResults.ItemHeight = 15;
            this.listBoxResults.Location = new System.Drawing.Point(12, 50);
            this.listBoxResults.Name = "listBoxResults";
            this.listBoxResults.Size = new System.Drawing.Size(488, 124);
            this.listBoxResults.TabIndex = 2;
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(12, 190);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(80, 30);
            this.playButton.TabIndex = 3;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(98, 190);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(80, 30);
            this.pauseButton.TabIndex = 4;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(184, 190);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(80, 30);
            this.stopButton.TabIndex = 5;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // previousButton
            // 
            this.previousButton.Location = new System.Drawing.Point(270, 190);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(80, 30);
            this.previousButton.TabIndex = 6;
            this.previousButton.Text = "Previous";
            this.previousButton.UseVisualStyleBackColor = true;
            this.previousButton.Click += new System.EventHandler(this.PreviousButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(356, 190);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(80, 30);
            this.nextButton.TabIndex = 7;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // volumeTrackBar
            // 
            this.volumeTrackBar.Location = new System.Drawing.Point(12, 240);
            this.volumeTrackBar.Maximum = 100;
            this.volumeTrackBar.Name = "volumeTrackBar";
            this.volumeTrackBar.Size = new System.Drawing.Size(200, 45);
            this.volumeTrackBar.TabIndex = 8;
            this.volumeTrackBar.TickFrequency = 10;
            this.volumeTrackBar.Scroll += new System.EventHandler(this.VolumeTrackBar_Scroll);
            // 
            // progressBarPlayback
            // 
            this.progressBarPlayback.Location = new System.Drawing.Point(12, 300);
            this.progressBarPlayback.Name = "progressBarPlayback";
            this.progressBarPlayback.Size = new System.Drawing.Size(488, 23);
            this.progressBarPlayback.TabIndex = 9;
            this.progressBarPlayback.MouseDown += new System.Windows.Forms.MouseEventHandler(this.progressBarPlayback_MouseDown);
            // 
            // themeToggleButton
            // 
            this.themeToggleButton.Location = new System.Drawing.Point(220, 240);
            this.themeToggleButton.Name = "themeToggleButton";
            this.themeToggleButton.Size = new System.Drawing.Size(100, 30);
            this.themeToggleButton.TabIndex = 10;
            this.themeToggleButton.Text = "Toggle Theme";
            this.themeToggleButton.UseVisualStyleBackColor = true;
            this.themeToggleButton.Click += new System.EventHandler(this.ThemeToggleButton_Click);
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(340, 240);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(160, 30);
            this.downloadButton.TabIndex = 11;
            this.downloadButton.Text = "Download Audio";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(512, 340);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.themeToggleButton);
            this.Controls.Add(this.progressBarPlayback);
            this.Controls.Add(this.volumeTrackBar);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.previousButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.listBoxResults);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchTextBox);
            this.Name = "MainForm";
            this.Text = "YouTube Music Player";
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}