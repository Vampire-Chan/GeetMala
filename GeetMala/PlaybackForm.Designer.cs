using System.Drawing;
using System.Windows.Forms;

namespace MusicPlayerWinForms
{
    partial class PlaybackForm
    {
        private PictureBox pictureBoxAlbumArt;
        private Label lblSongTitle;
        private Label lblArtistName;
        private ProgressBar progressBarTrack;
        private Button btnPlay;
        private Button btnPause;
        private Button btnStop;

        private void InitializeComponent()
        {
            pictureBoxAlbumArt = new PictureBox();
            lblSongTitle = new Label();
            lblArtistName = new Label();
            progressBarTrack = new ProgressBar();
            btnPlay = new Button();
            btnPause = new Button();
            btnStop = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAlbumArt).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxAlbumArt
            // 
            pictureBoxAlbumArt.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxAlbumArt.Location = new Point(50, 20);
            pictureBoxAlbumArt.Name = "pictureBoxAlbumArt";
            pictureBoxAlbumArt.Size = new Size(200, 200);
            pictureBoxAlbumArt.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxAlbumArt.TabIndex = 0;
            pictureBoxAlbumArt.TabStop = false;
            // 
            // lblSongTitle
            // 
            lblSongTitle.Location = new Point(50, 230);
            lblSongTitle.Name = "lblSongTitle";
            lblSongTitle.Size = new Size(200, 30);
            lblSongTitle.TabIndex = 1;
            lblSongTitle.Text = "Song Title";
            lblSongTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblArtistName
            // 
            lblArtistName.Location = new Point(50, 260);
            lblArtistName.Name = "lblArtistName";
            lblArtistName.Size = new Size(200, 30);
            lblArtistName.TabIndex = 2;
            lblArtistName.Text = "Artist Name";
            lblArtistName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressBarTrack
            // 
            progressBarTrack.Location = new Point(50, 300);
            progressBarTrack.Name = "progressBarTrack";
            progressBarTrack.Size = new Size(200, 20);
            progressBarTrack.TabIndex = 3;
            // 
            // btnPlay
            // 
            btnPlay.Location = new Point(50, 330);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(50, 30);
            btnPlay.TabIndex = 4;
            btnPlay.Text = "Play";
            btnPlay.Click += btnPlay_Click;
            // 
            // btnPause
            // 
            btnPause.Location = new Point(120, 330);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(50, 30);
            btnPause.TabIndex = 5;
            btnPause.Text = "Pause";
            btnPause.Click += btnPause_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(190, 330);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(50, 30);
            btnStop.TabIndex = 6;
            btnStop.Text = "Stop";
            btnStop.Click += btnStop_Click;
            // 
            // PlaybackForm
            // 
            ClientSize = new Size(296, 391);
            Controls.Add(pictureBoxAlbumArt);
            Controls.Add(lblSongTitle);
            Controls.Add(lblArtistName);
            Controls.Add(progressBarTrack);
            Controls.Add(btnPlay);
            Controls.Add(btnPause);
            Controls.Add(btnStop);
            Name = "PlaybackForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mini Player";
            ((System.ComponentModel.ISupportInitialize)pictureBoxAlbumArt).EndInit();
            ResumeLayout(false);
        }
    }
}