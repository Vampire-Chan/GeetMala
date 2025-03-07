using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;
using NAudio.Wave;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace MusicPlayerWinForms
{
    public partial class MainForm : Form
    {
        // YouTube Data API key for searches.
        private readonly string apiKey;
        // YoutubeExplode client for streaming and manifest retrieval.
        private YoutubeClient youtubeClient;
        private IWavePlayer waveOut;
        private MediaFoundationReader mediaReader;
        private string currentAudioFile;
        // List to store search results for next/previous playback.
        private readonly List<VideoListItem> searchResults = new List<VideoListItem>();
        // Timer to update playback progress.
        private System.Windows.Forms.Timer playbackTimer = new System.Windows.Forms.Timer();

        // Constructor accepts an API key.
        public MainForm(string apiKey)
        {
            InitializeComponent();
            this.apiKey = apiKey;
            youtubeClient = new YoutubeClient();
            InitializePlaybackTimer();
        }

        // Timer that updates the playback progress bar.
        private void InitializePlaybackTimer()
        {
            playbackTimer.Interval = 500; // update every 0.5 seconds
            playbackTimer.Tick += PlaybackTimer_Tick;
        }

        private void PlaybackTimer_Tick(object sender, EventArgs e)
        {
            if (mediaReader != null && mediaReader.TotalTime.TotalSeconds > 0)
            {
                try
                {
                    int currentSeconds = (int)mediaReader.CurrentTime.TotalSeconds;
                    int totalSeconds = (int)mediaReader.TotalTime.TotalSeconds;
                    progressBarPlayback.Maximum = totalSeconds;
                    progressBarPlayback.Value = Math.Min(currentSeconds, totalSeconds);
                }
                catch { }
            }
        }

        // Searches for YouTube videos using the YouTube Data API (v3)
        // based on the query entered in searchTextBox.
        private async void SearchButton_Click(object sender, EventArgs e)
        {
            string query = searchTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(query))
            {
                MessageBox.Show("Enter a search query.");
                return;
            }

            listBoxResults.Items.Clear();
            searchResults.Clear();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string requestUrl = $"https://www.googleapis.com/youtube/v3/search?part=snippet&type=video&maxResults=10&q={Uri.EscapeDataString(query)}&key={apiKey}";
                    HttpResponseMessage response = await client.GetAsync(requestUrl);
                    response.EnsureSuccessStatusCode();
                    string json = await response.Content.ReadAsStringAsync();
                    var document = JsonDocument.Parse(json);
                    JsonElement root = document.RootElement;
                    if (root.TryGetProperty("items", out JsonElement items))
                    {
                        foreach (JsonElement item in items.EnumerateArray())
                        {
                            string videoId = item.GetProperty("id").GetProperty("videoId").GetString();
                            string title = item.GetProperty("snippet").GetProperty("title").GetString();
                            VideoListItem videoItem = new VideoListItem(title, videoId);
                            listBoxResults.Items.Add(videoItem);
                            searchResults.Add(videoItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during search: " + ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Plays the selected YouTube video using chunked playback.
        // Also starts the progress timer.
        private async void PlayButton_Click(object sender, EventArgs e)
        {
            await PlaySelectedVideoAsync();
        }

        private async Task PlaySelectedVideoAsync()
        {
            if (listBoxResults.SelectedItem == null)
            {
                MessageBox.Show("Select a video to play.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            VideoListItem item = (VideoListItem)listBoxResults.SelectedItem;

            try
            {
                var streamManifest = await youtubeClient.Videos.Streams.GetManifestAsync(item.VideoId);
                var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                if (streamInfo == null)
                {
                    MessageBox.Show("No audio stream available for this video.", "Stream Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                currentAudioFile = Path.Combine(Path.GetTempPath(), item.VideoId + "." + streamInfo.Container.Name);

                Task downloadTask = Task.Run(async () =>
                {
                    // Download using the file path replacement.
                    await youtubeClient.Videos.Streams.DownloadAsync(streamInfo, currentAudioFile);
                });

                // Wait until a minimum buffer (1 MB) is available.
                const int minBufferSize = 1 * 1024 * 1024; // 1 MB
                FileInfo fileInfo = new FileInfo(currentAudioFile);
                while (!fileInfo.Exists || fileInfo.Length < minBufferSize)
                {
                    await Task.Delay(200);
                    fileInfo.Refresh();
                }

                StopPlayback();

                // Open the file with FileShare.ReadWrite so that it isn't locked while the download continues.
                FileStream fs = new FileStream(currentAudioFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                mediaReader = new MediaFoundationReader(fs.Name);
                waveOut = new WaveOutEvent();
                waveOut.Init(mediaReader);
                waveOut.Play();

                // Start the playback progress timer.
                playbackTimer.Start();

                // Optionally, await downloadTask if needed.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during playback: " + ex.Message, "Playback Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Toggles between pause and resume.
        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (waveOut != null)
            {
                if (waveOut.PlaybackState == PlaybackState.Playing)
                    waveOut.Pause();
                else if (waveOut.PlaybackState == PlaybackState.Paused)
                    waveOut.Play();
            }
        }

        // Stops playback and releases resources.
        private void StopButton_Click(object sender, EventArgs e)
        {
            StopPlayback();
        }

        // Helper method to stop playback.
        private void StopPlayback()
        {
            playbackTimer.Stop();
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
            if (mediaReader != null)
            {
                mediaReader.Dispose();
                mediaReader = null;
            }
        }

        // Next playback: selects the next video in search results and plays it.
        private async void NextButton_Click(object sender, EventArgs e)
        {
            if (listBoxResults.SelectedIndex < listBoxResults.Items.Count - 1)
            {
                listBoxResults.SelectedIndex += 1;
                await PlaySelectedVideoAsync();
            }
            else
            {
                MessageBox.Show("This is the last video in the current list.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Previous playback: selects the previous video in search results and plays it.
        private async void PreviousButton_Click(object sender, EventArgs e)
        {
            if (listBoxResults.SelectedIndex > 0)
            {
                listBoxResults.SelectedIndex -= 1;
                await PlaySelectedVideoAsync();
            }
            else
            {
                MessageBox.Show("This is the first video in the current list.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Volume control adjustment.
        private void VolumeTrackBar_Scroll(object sender, EventArgs e)
        {
            if (waveOut != null)
            {
                waveOut.Volume = volumeTrackBar.Value / 100f;
            }
        }

        // Handle user seeking by clicking on the progress bar.
        private void progressBarPlayback_MouseDown(object sender, MouseEventArgs e)
        {
            if (mediaReader != null)
            {
                // Calculate new time based on click position.
                double ratio = (double)e.X / progressBarPlayback.Width;
                var newTime = TimeSpan.FromSeconds(ratio * mediaReader.TotalTime.TotalSeconds);
                mediaReader.CurrentTime = newTime;
            }
        }

        // Theme toggle placeholder.
        private void ThemeToggleButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Theme toggle functionality is not yet implemented.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Downloads the audio stream to a user-specified folder and format (mp3 or wav).
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            if (listBoxResults.SelectedItem == null)
            {
                MessageBox.Show("Select a video to download.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            VideoListItem item = (VideoListItem)listBoxResults.SelectedItem;
            Task.Run(async () =>
            {
                try
                {
                    var streamManifest = await youtubeClient.Videos.Streams.GetManifestAsync(item.VideoId);
                    var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                    if (streamInfo == null)
                    {
                        this.Invoke((Action)(() =>
                        {
                            MessageBox.Show("No audio stream available for download.", "Stream Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }));
                        return;
                    }

                    // Open a SaveFileDialog for the user to choose the filename and format.
                    this.Invoke((Action)(() =>
                    {
                        using (SaveFileDialog saveDialog = new SaveFileDialog())
                        {
                            saveDialog.FileName = item.VideoId;
                            saveDialog.Filter = "MP3 files|*.mp3|WAV files|*.wav|Original (webm)|*.webm";
                            saveDialog.Title = "Save audio file";
                            if (saveDialog.ShowDialog() == DialogResult.OK)
                            {
                                string selectedPath = saveDialog.FileName;
                                Task.Run(async () =>
                                {
                                    await youtubeClient.Videos.Streams.DownloadAsync(streamInfo, selectedPath);
                                    this.Invoke((Action)(() =>
                                    {
                                        MessageBox.Show("Downloaded to " + selectedPath, "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }));
                                });
                            }
                        }
                    }));
                }
                catch (Exception ex)
                {
                    this.Invoke((Action)(() =>
                    {
                        MessageBox.Show("Download failed: " + ex.Message, "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
            });
        }

        // Helper class for representing video items in the list box.
        private class VideoListItem
        {
            public string Title { get; }
            public string VideoId { get; }

            public VideoListItem(string title, string videoId)
            {
                Title = title;
                VideoId = videoId;
            }

            public override string ToString() => Title;
        }
    }
}