using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace MusicPlayerWinForms
{
    public partial class SettingsForm : Form
    {
        // Exposed properties that can be read after the form is accepted.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        // Exposed properties that can be read after the form is accepted.
        public bool ShuffleEnabled { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool LoopEnabled { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseSpotify { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal PlaybackSpeed { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Theme { get; private set; }

        public SettingsForm()
        {
            InitializeComponent();
        }

        // Event handler for the Save button.
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Capture settings from UI controls.
            ShuffleEnabled = chkShuffle.Checked;
            LoopEnabled = chkLoop.Checked;
            UseSpotify = rbSpotify.Checked;
            PlaybackSpeed = nudPlaybackSpeed.Value;
            Theme = rbDarkTheme.Checked ? "Dark" : "Light";

            // Optionally, you could call Settings.Default to persist these values.

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}