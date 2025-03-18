using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using YoutubeExplode;
using YoutubeExplode.Common;
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

        // Theme related variables
        private bool isDarkMode = false;
        private Color lightModeBackColor = SystemColors.Control;
        private Color lightModeForeColor = SystemColors.ControlText;
        private Color darkModeBackColor = Color.FromArgb(45, 45, 48); // Dark gray
        private Color darkModeForeColor = Color.WhiteSmoke; // Light gray

        // Constructor accepts an API key.
        public MainForm(string apiKey)
        {
            InitializeComponent();
            this.apiKey = apiKey;
            youtubeClient = new YoutubeClient();
            InitializePlaybackTimer();
            ApplyTheme(); // Apply initial theme on startup
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
                    trackBarPlayback.Maximum = totalSeconds;
                    trackBarPlayback.Value = Math.Min(currentSeconds, totalSeconds);
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

        private async void SearchButton_Click(object sender, EventArgs e, string s = null)
        {
            string query = searchTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(query))
            {
                MessageBox.Show("Enter a search query.");
                return;
            }

            listBoxResults.Items.Clear();

            try
            {
                var videos = await youtubeClient.Search.GetVideosAsync(query);
                foreach (var video in videos)
                {
                    listBoxResults.Items.Add(new VideoListItem(video.Title, video.Id));
                    if (listBoxResults.Items.Count >= 10)
                        break;
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

                // Download directly — no need for Task.Run() since this is async
                await youtubeClient.Videos.Streams.DownloadAsync(streamInfo, currentAudioFile);

                // Improved Buffer Wait Logic with Timeout (Prevents Infinite Loops)
                const int minBufferSize = 1 * 1024 * 1024; // 1 MB
                const int maxWaitTime = 10000; // 10 seconds max wait
                FileInfo fileInfo = new FileInfo(currentAudioFile);

                int elapsedTime = 0;
                while ((!fileInfo.Exists || fileInfo.Length < minBufferSize) && elapsedTime < maxWaitTime)
                {
                    await Task.Delay(200);
                    fileInfo.Refresh();
                    elapsedTime += 200;
                }

                if (!fileInfo.Exists || fileInfo.Length < minBufferSize)
                {
                    MessageBox.Show("Failed to buffer audio file.", "Buffer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                StopPlayback();

                // Ensured Proper Resource Disposal
                mediaReader?.Dispose();
                waveOut?.Dispose();

                // Use MediaFoundationReader directly (FileStream no longer needed)
                mediaReader = new MediaFoundationReader(currentAudioFile);
                waveOut = new WaveOutEvent();
                waveOut.Init(mediaReader);
                waveOut.Play();

                // Start playback progress timer
                playbackTimer.Start();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Network error: " + ex.Message, "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show("File I/O error: " + ex.Message, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // Volume control adjustment
        private void VolumeTrackBar_Scroll(object sender, EventArgs e)
        {
            if (waveOut != null)
            {
                // Convert the TrackBar value from 0-100 to 0.0-1.0
                waveOut.Volume = volumeTrackBar.Value / 100f;
            }
        }

        // Handle user seeking by clicking on the playback track bar.
        private void TrackBarPlayback_Scroll(object sender, EventArgs e)
        {
            if (mediaReader != null)
            {
                // Calculate new time based on track bar value.
                var newTime = TimeSpan.FromSeconds(trackBarPlayback.Value);
                mediaReader.CurrentTime = newTime;
            }
        }

        // Theme toggle functionality.
        private void ThemeToggleButton_Click(object sender, EventArgs e)
        {
            isDarkMode = !isDarkMode;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            if (isDarkMode)
            {
                this.BackColor = darkModeBackColor;
                this.ForeColor = darkModeForeColor;
                searchTextBox.BackColor = darkModeBackColor;
                searchTextBox.ForeColor = darkModeForeColor;
                searchButton.BackColor = Color.Black;
                searchButton.ForeColor = darkModeForeColor;
                listBoxResults.BackColor = darkModeBackColor;
                listBoxResults.ForeColor = darkModeForeColor;
                playButton.BackColor = darkModeBackColor;
                playButton.ForeColor = darkModeForeColor;
                pauseButton.BackColor = darkModeBackColor;
                pauseButton.ForeColor = darkModeForeColor;
                stopButton.BackColor = darkModeBackColor;
                stopButton.ForeColor = darkModeForeColor;
                nextButton.BackColor = darkModeBackColor;
                nextButton.ForeColor = darkModeForeColor;
                previousButton.BackColor = darkModeBackColor;
                previousButton.ForeColor = darkModeForeColor;
                themeToggleButton.BackColor = darkModeBackColor;
                themeToggleButton.ForeColor = darkModeForeColor;
                downloadButton.BackColor = darkModeBackColor;
                downloadButton.ForeColor = darkModeForeColor;
                volumeTrackBar.BackColor = darkModeBackColor; // Trackbar background itself doesn't change color easily, but handle if needed for more advanced theming
                volumeTrackBar.ForeColor = darkModeForeColor;
                trackBarPlayback.BackColor = darkModeBackColor; // Changed from ProgressBar to TrackBar
                trackBarPlayback.ForeColor = darkModeForeColor; // Changed from ProgressBar to TrackBar

            }
            else
            {
                this.BackColor = lightModeBackColor;
                this.ForeColor = lightModeForeColor;
                searchTextBox.BackColor = lightModeBackColor;
                searchTextBox.ForeColor = lightModeForeColor;
                searchButton.BackColor = lightModeBackColor;
                searchButton.ForeColor = lightModeForeColor;
                listBoxResults.BackColor = Color.White;
                playButton.BackColor = lightModeBackColor;
                playButton.ForeColor = lightModeForeColor;
                pauseButton.BackColor = lightModeBackColor;
                pauseButton.ForeColor = lightModeForeColor;
                stopButton.BackColor = lightModeBackColor;
                stopButton.ForeColor = lightModeForeColor;
                nextButton.BackColor = lightModeBackColor;
                nextButton.ForeColor = lightModeForeColor;
                previousButton.BackColor = lightModeBackColor;
                previousButton.ForeColor = lightModeForeColor;
                themeToggleButton.BackColor = lightModeBackColor;
                themeToggleButton.ForeColor = lightModeForeColor;
                downloadButton.BackColor = lightModeBackColor;
                downloadButton.ForeColor = lightModeForeColor;
                volumeTrackBar.BackColor = lightModeBackColor;
                volumeTrackBar.ForeColor = lightModeForeColor;
                trackBarPlayback.BackColor = lightModeBackColor; // Changed from ProgressBar to TrackBar
                trackBarPlayback.ForeColor = lightModeForeColor; // Changed from ProgressBar to TrackBar
            }
        }

        // Downloads the audio or video stream to a user-specified folder and format.
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
                    IStreamInfo streamInfo;

                    if (radioButtonAudioOnly.Checked)
                    {
                        streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                    }
                    else
                    {
                        streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
                    }

                    if (streamInfo == null)
                    {
                        this.Invoke((Action)(() =>
                        {
                            MessageBox.Show("No suitable stream available for download.", "Stream Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }));
                        return;
                    }

                    // Open a SaveFileDialog for the user to choose the filename and format.
                    this.Invoke((Action)(() =>
                    {
                        using (SaveFileDialog saveDialog = new SaveFileDialog())
                        {
                            saveDialog.FileName = item.Title;
                            saveDialog.Filter = radioButtonAudioOnly.Checked
                                ? "MP3 files|*.mp3|WAV files|*.wav|Original (webm)|*.webm"
                                : "MP4 files|*.mp4|Original (webm)|*.webm";
                            saveDialog.Title = "Save file";
                            if (saveDialog.ShowDialog() == DialogResult.OK)
                            {
                                string selectedPath = saveDialog.FileName;
                                Task.Run(async () =>
                                {
                                    await youtubeClient.Videos.Streams.DownloadAsync(streamInfo, selectedPath);
                                    this.Invoke((Action)(() =>
                                    {
                                        MessageBox.Show($"Downloaded to {selectedPath}", "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show($"Download failed: {ex.Message}", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
            });
        }

        private void radioButtonAudioOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAudioOnly.Checked)
            {
                downloadButton.Text = "Download Audio";
            }
        }

        private void radioButtonVideoWithAudio_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonVideoWithAudio.Checked)
            {
                downloadButton.Text = "Download Video";
            }
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