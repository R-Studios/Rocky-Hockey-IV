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
            InitializeComboBox();
            LoadConfigData();
        }

        /// <summary>
        /// Logger for displaying a MessageBox
        /// </summary>
        public MessageBoxLogger MsgBoxLogger { get; } = new MessageBoxLogger();

        private void InitializeComboBox()
        {
            foreach (string difficulty in Enum.GetNames(typeof(Difficulties)))
            {
                DifficultyComboBox.Items.Add(difficulty);
            }
        }

        private void LoadConfigData()
        {
            HeightTextBox.Text = Config.Instance.GameFieldSize.Height.ToString();
            WidthTextBox.Text = Config.Instance.GameFieldSize.Width.ToString();
            FrameRateTextBox.Text = Config.Instance.FrameRate.ToString();
            DifficultyComboBox.SelectedItem = Config.Instance.GameDifficulty.ToString();
            PunchAxisPositionTextBox.Text = Config.Instance.ImaginaryAxePosition.ToString();
            ToleranceTextBox.Text = Config.Instance.Tolerance.ToString();
            Camera1IndexTextBox.Text = Config.Instance.Camera1.index.ToString();
            Camera2IndexTextBox.Text = Config.Instance.Camera2.index.ToString();
            MaximumBatVelocityTextBox.Text = Config.Instance.MaxBatVelocity.ToString();
            RestPositionDivisorTextBox.Text = Config.Instance.RestPositionDivisor.ToString();
        }

        private void AbortButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                var gameFieldSize = new Size(Convert.ToInt32(WidthTextBox.Text), Convert.ToInt32(HeightTextBox.Text));
                Config.Instance.GameFieldSize = gameFieldSize;
                Config.Instance.FrameRate = Convert.ToInt32(FrameRateTextBox.Text);
                Config.Instance.GameDifficulty = (Difficulties)Enum.Parse(typeof(Difficulties), DifficultyComboBox.SelectedItem.ToString());
                Config.Instance.ImaginaryAxePosition = Convert.ToInt32(PunchAxisPositionTextBox.Text);
                Config.Instance.Tolerance = Convert.ToInt32(ToleranceTextBox.Text);
                Config.Instance.Camera1.index = Convert.ToInt32(Camera1IndexTextBox.Text);
                Config.Instance.Camera2.index = Convert.ToInt32(Camera2IndexTextBox.Text);
                Config.Instance.MaxBatVelocity = Convert.ToDouble(MaximumBatVelocityTextBox.Text);
                Config.Instance.RestPositionDivisor = Convert.ToDouble(RestPositionDivisorTextBox.Text);

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
