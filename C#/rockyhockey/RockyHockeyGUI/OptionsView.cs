using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;

namespace RockyHockeyGUI
{
    /// <summary>
    /// Displays the configurable options
    /// </summary>
    public partial class OptionsView : Form
    {
        /// <summary>
        /// Initializes the OptionsView
        /// </summary>
        public OptionsView()
        {
            InitializeComponent();
            InitializeDifficultyComboBox();
            InitializeSelectedCameraCombobox();
            LoadConfigData();
        }

        /// <summary>
        /// Logger for displaying a MessageBox
        /// </summary>
        public MessageBoxLogger MsgBoxLogger { get; } = new MessageBoxLogger();

        public FilterInfoCollection filterInfoCollection;

        private void InitializeDifficultyComboBox()
        {
            foreach (string difficulty in Enum.GetNames(typeof(Difficulties)))
            {
                DifficultyComboBox.Items.Add(difficulty);
            }
        }

        private void InitializeSelectedCameraCombobox()
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                SelectedCameraCBO.Items.Add(filterInfo.Name);
            SelectedCameraCBO.SelectedIndex = 0;

        }

        private void LoadConfigData()
        {
            HeightTextBox.Text = Config.Instance.GameFieldSizeMM.Height.ToString();
            WidthTextBox.Text = Config.Instance.GameFieldSizeMM.Width.ToString();
            FrameRateTextBox.Text = Config.Instance.FrameRate.ToString();
            DifficultyComboBox.SelectedItem = Config.Instance.GameDifficulty.ToString();
            PunchAxisPositionTextBox.Text = Config.Instance.ImaginaryAxePosition.ToString();
            ToleranceTextBox.Text = Config.Instance.Tolerance.ToString();
            if(!String.IsNullOrEmpty(Config.Instance.Camera1.name))
                SelectedCameraCBO.SelectedItem = Config.Instance.Camera1.name;
            MaximumBatVelocityTextBox.Text = Config.Instance.MaxBatVelocity.ToString();
            RestPositionDivisorTextBox.Text = Config.Instance.RestPositionDivisor.ToString();
            PuckRadiusTextBox.Text = Config.Instance.PuckRadiusMM.ToString();
            BatRadiusTextBox.Text = Config.Instance.BatRadiusMM.ToString();
        }

        private void AbortButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                var gameFieldSizeMM = new Size(Convert.ToInt32(WidthTextBox.Text), Convert.ToInt32(HeightTextBox.Text));
                var gameFieldSize = new Size((int)Math.Ceiling(gameFieldSizeMM.Width / Config.Instance.SizeRatio), (int)Math.Ceiling(gameFieldSizeMM.Height / Config.Instance.SizeRatio));
                Config.Instance.GameFieldSizeMM = gameFieldSizeMM;
                Config.Instance.GameFieldSize = gameFieldSize;
                Config.Instance.FrameRate = Convert.ToInt32(FrameRateTextBox.Text);
                Config.Instance.GameDifficulty = (Difficulties)Enum.Parse(typeof(Difficulties), DifficultyComboBox.SelectedItem.ToString());
                Config.Instance.ImaginaryAxePosition = Convert.ToInt32(PunchAxisPositionTextBox.Text);
                Config.Instance.Tolerance = Convert.ToInt32(ToleranceTextBox.Text);
                Console.WriteLine(filterInfoCollection[SelectedCameraCBO.SelectedIndex].MonikerString);
                Config.Instance.Camera1.name = filterInfoCollection[SelectedCameraCBO.SelectedIndex].MonikerString;
                Config.Instance.MaxBatVelocity = Convert.ToDouble(MaximumBatVelocityTextBox.Text);
                Config.Instance.RestPositionDivisor = Convert.ToDouble(RestPositionDivisorTextBox.Text);
                Config.Instance.PuckRadiusMM = Convert.ToDouble(PuckRadiusTextBox.Text);
                Config.Instance.PuckRadius = Math.Ceiling(Config.Instance.PuckRadiusMM / Config.Instance.SizeRatio);
                Config.Instance.BatRadiusMM = Convert.ToDouble(BatRadiusTextBox.Text);
                Config.Instance.BatRadius = Math.Ceiling(Config.Instance.BatRadiusMM / Config.Instance.SizeRatio);

                Config.Instance.save();
                Close();
            }
            catch
            {
                MsgBoxLogger?.Log("Please enter valid values");
                MsgBoxLogger?.Show();
            }
        }
    }
}
