namespace RockyHockeyGUI
{
    partial class OptionsView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsView));
            this.GameFieldSizeLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.WidthTextBox = new System.Windows.Forms.TextBox();
            this.HeightTextBox = new System.Windows.Forms.TextBox();
            this.WidthLabel = new System.Windows.Forms.Label();
            this.HeightLabel = new System.Windows.Forms.Label();
            this.Camera1IndexTextBox = new System.Windows.Forms.TextBox();
            this.ToleranceTextBox = new System.Windows.Forms.TextBox();
            this.PunchAxisPositionTextBox = new System.Windows.Forms.TextBox();
            this.DifficultyComboBox = new System.Windows.Forms.ComboBox();
            this.FrameRateTextBox = new System.Windows.Forms.TextBox();
            this.Camera1IndexLabel = new System.Windows.Forms.Label();
            this.ToleranceLabel = new System.Windows.Forms.Label();
            this.PunchAxisLabel = new System.Windows.Forms.Label();
            this.DifficultyLabel = new System.Windows.Forms.Label();
            this.FrameRateLabel = new System.Windows.Forms.Label();
            this.MaxBatVelocityLabel = new System.Windows.Forms.Label();
            this.MaximumBatVelocityTextBox = new System.Windows.Forms.TextBox();
            this.RestPositionDivisorTextBox = new System.Windows.Forms.TextBox();
            this.PuckRadiusTextBox = new System.Windows.Forms.TextBox();
            this.BatRadiusTextBox = new System.Windows.Forms.TextBox();
            this.RestPositionDivisorLabel = new System.Windows.Forms.Label();
            this.PuckRadiusLabel = new System.Windows.Forms.Label();
            this.SizeRatioLabel = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.AbortButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GameFieldSizeLabel
            // 
            this.GameFieldSizeLabel.AutoSize = true;
            this.GameFieldSizeLabel.Location = new System.Drawing.Point(2, 0);
            this.GameFieldSizeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.GameFieldSizeLabel.Name = "GameFieldSizeLabel";
            this.GameFieldSizeLabel.Size = new System.Drawing.Size(103, 13);
            this.GameFieldSizeLabel.TabIndex = 0;
            this.GameFieldSizeLabel.Text = "Game field size (mm)";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.WidthTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.HeightTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.WidthLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.HeightLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.GameFieldSizeLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Camera1IndexTextBox, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.ToleranceTextBox, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.PunchAxisPositionTextBox, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.DifficultyComboBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.FrameRateTextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.Camera1IndexLabel, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.ToleranceLabel, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.PunchAxisLabel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.DifficultyLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.FrameRateLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.MaxBatVelocityLabel, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.MaximumBatVelocityTextBox, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.RestPositionDivisorTextBox, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.PuckRadiusTextBox, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.BatRadiusTextBox, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.RestPositionDivisorLabel, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.PuckRadiusLabel, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.SizeRatioLabel, 0, 11);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(329, 300);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // WidthTextBox
            // 
            this.WidthTextBox.Location = new System.Drawing.Point(166, 50);
            this.WidthTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.WidthTextBox.Name = "WidthTextBox";
            this.WidthTextBox.Size = new System.Drawing.Size(68, 20);
            this.WidthTextBox.TabIndex = 3;
            // 
            // HeightTextBox
            // 
            this.HeightTextBox.Location = new System.Drawing.Point(166, 26);
            this.HeightTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.HeightTextBox.Name = "HeightTextBox";
            this.HeightTextBox.Size = new System.Drawing.Size(68, 20);
            this.HeightTextBox.TabIndex = 2;
            // 
            // WidthLabel
            // 
            this.WidthLabel.AutoSize = true;
            this.WidthLabel.Location = new System.Drawing.Point(2, 48);
            this.WidthLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.WidthLabel.Name = "WidthLabel";
            this.WidthLabel.Size = new System.Drawing.Size(80, 13);
            this.WidthLabel.TabIndex = 1;
            this.WidthLabel.Text = "                width";
            // 
            // HeightLabel
            // 
            this.HeightLabel.AutoSize = true;
            this.HeightLabel.Location = new System.Drawing.Point(2, 24);
            this.HeightLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.HeightLabel.Name = "HeightLabel";
            this.HeightLabel.Size = new System.Drawing.Size(84, 13);
            this.HeightLabel.TabIndex = 0;
            this.HeightLabel.Text = "                height";
            // 
            // Camera1IndexTextBox
            // 
            this.Camera1IndexTextBox.Location = new System.Drawing.Point(166, 170);
            this.Camera1IndexTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.Camera1IndexTextBox.Name = "Camera1IndexTextBox";
            this.Camera1IndexTextBox.Size = new System.Drawing.Size(35, 20);
            this.Camera1IndexTextBox.TabIndex = 7;
            // 
            // ToleranceTextBox
            // 
            this.ToleranceTextBox.Location = new System.Drawing.Point(166, 146);
            this.ToleranceTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.ToleranceTextBox.Name = "ToleranceTextBox";
            this.ToleranceTextBox.Size = new System.Drawing.Size(88, 20);
            this.ToleranceTextBox.TabIndex = 5;
            // 
            // PunchAxisPositionTextBox
            // 
            this.PunchAxisPositionTextBox.Location = new System.Drawing.Point(166, 122);
            this.PunchAxisPositionTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.PunchAxisPositionTextBox.Name = "PunchAxisPositionTextBox";
            this.PunchAxisPositionTextBox.Size = new System.Drawing.Size(53, 20);
            this.PunchAxisPositionTextBox.TabIndex = 4;
            // 
            // DifficultyComboBox
            // 
            this.DifficultyComboBox.FormattingEnabled = true;
            this.DifficultyComboBox.Location = new System.Drawing.Point(166, 98);
            this.DifficultyComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.DifficultyComboBox.Name = "DifficultyComboBox";
            this.DifficultyComboBox.Size = new System.Drawing.Size(134, 21);
            this.DifficultyComboBox.TabIndex = 22;
            // 
            // FrameRateTextBox
            // 
            this.FrameRateTextBox.Location = new System.Drawing.Point(166, 74);
            this.FrameRateTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.FrameRateTextBox.Name = "FrameRateTextBox";
            this.FrameRateTextBox.Size = new System.Drawing.Size(53, 20);
            this.FrameRateTextBox.TabIndex = 3;
            // 
            // Camera1IndexLabel
            // 
            this.Camera1IndexLabel.AutoSize = true;
            this.Camera1IndexLabel.Location = new System.Drawing.Point(2, 168);
            this.Camera1IndexLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Camera1IndexLabel.Name = "Camera1IndexLabel";
            this.Camera1IndexLabel.Size = new System.Drawing.Size(72, 13);
            this.Camera1IndexLabel.TabIndex = 16;
            this.Camera1IndexLabel.Text = "Camera Index";
            // 
            // ToleranceLabel
            // 
            this.ToleranceLabel.AutoSize = true;
            this.ToleranceLabel.Location = new System.Drawing.Point(2, 144);
            this.ToleranceLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ToleranceLabel.Name = "ToleranceLabel";
            this.ToleranceLabel.Size = new System.Drawing.Size(55, 13);
            this.ToleranceLabel.TabIndex = 15;
            this.ToleranceLabel.Text = "Tolerance";
            // 
            // PunchAxisLabel
            // 
            this.PunchAxisLabel.AutoSize = true;
            this.PunchAxisLabel.Location = new System.Drawing.Point(2, 120);
            this.PunchAxisLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PunchAxisLabel.Name = "PunchAxisLabel";
            this.PunchAxisLabel.Size = new System.Drawing.Size(100, 13);
            this.PunchAxisLabel.TabIndex = 14;
            this.PunchAxisLabel.Text = "Punch Axis Position";
            // 
            // DifficultyLabel
            // 
            this.DifficultyLabel.AutoSize = true;
            this.DifficultyLabel.Location = new System.Drawing.Point(2, 96);
            this.DifficultyLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DifficultyLabel.Name = "DifficultyLabel";
            this.DifficultyLabel.Size = new System.Drawing.Size(47, 13);
            this.DifficultyLabel.TabIndex = 13;
            this.DifficultyLabel.Text = "Difficulty";
            // 
            // FrameRateLabel
            // 
            this.FrameRateLabel.AutoSize = true;
            this.FrameRateLabel.Location = new System.Drawing.Point(2, 72);
            this.FrameRateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FrameRateLabel.Name = "FrameRateLabel";
            this.FrameRateLabel.Size = new System.Drawing.Size(57, 13);
            this.FrameRateLabel.TabIndex = 12;
            this.FrameRateLabel.Text = "Frame rate";
            // 
            // MaxBatVelocityLabel
            // 
            this.MaxBatVelocityLabel.AutoSize = true;
            this.MaxBatVelocityLabel.Location = new System.Drawing.Point(2, 192);
            this.MaxBatVelocityLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MaxBatVelocityLabel.Name = "MaxBatVelocityLabel";
            this.MaxBatVelocityLabel.Size = new System.Drawing.Size(110, 13);
            this.MaxBatVelocityLabel.TabIndex = 18;
            this.MaxBatVelocityLabel.Text = "Maximum Bat Velocity";
            // 
            // MaximumBatVelocityTextBox
            // 
            this.MaximumBatVelocityTextBox.Location = new System.Drawing.Point(166, 194);
            this.MaximumBatVelocityTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumBatVelocityTextBox.Name = "MaximumBatVelocityTextBox";
            this.MaximumBatVelocityTextBox.Size = new System.Drawing.Size(74, 20);
            this.MaximumBatVelocityTextBox.TabIndex = 9;
            // 
            // RestPositionDivisorTextBox
            // 
            this.RestPositionDivisorTextBox.Location = new System.Drawing.Point(166, 218);
            this.RestPositionDivisorTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.RestPositionDivisorTextBox.Name = "RestPositionDivisorTextBox";
            this.RestPositionDivisorTextBox.Size = new System.Drawing.Size(53, 20);
            this.RestPositionDivisorTextBox.TabIndex = 10;
            // 
            // PuckRadiusTextBox
            // 
            this.PuckRadiusTextBox.Location = new System.Drawing.Point(166, 242);
            this.PuckRadiusTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.PuckRadiusTextBox.Name = "PuckRadiusTextBox";
            this.PuckRadiusTextBox.Size = new System.Drawing.Size(53, 20);
            this.PuckRadiusTextBox.TabIndex = 10;
            // 
            // BatRadiusTextBox
            // 
            this.BatRadiusTextBox.Location = new System.Drawing.Point(166, 266);
            this.BatRadiusTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.BatRadiusTextBox.Name = "BatRadiusTextBox";
            this.BatRadiusTextBox.Size = new System.Drawing.Size(53, 20);
            this.BatRadiusTextBox.TabIndex = 10;
            // 
            // RestPositionDivisorLabel
            // 
            this.RestPositionDivisorLabel.AutoSize = true;
            this.RestPositionDivisorLabel.Location = new System.Drawing.Point(2, 216);
            this.RestPositionDivisorLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RestPositionDivisorLabel.Name = "RestPositionDivisorLabel";
            this.RestPositionDivisorLabel.Size = new System.Drawing.Size(101, 13);
            this.RestPositionDivisorLabel.TabIndex = 19;
            this.RestPositionDivisorLabel.Text = "Rest position divisor";
            // 
            // PuckRadiusLabel
            // 
            this.PuckRadiusLabel.AutoSize = true;
            this.PuckRadiusLabel.Location = new System.Drawing.Point(2, 240);
            this.PuckRadiusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PuckRadiusLabel.Name = "PuckRadiusLabel";
            this.PuckRadiusLabel.Size = new System.Drawing.Size(93, 13);
            this.PuckRadiusLabel.TabIndex = 19;
            this.PuckRadiusLabel.Text = "Puck Radius (mm)";
            // 
            // SizeRatioLabel
            // 
            this.SizeRatioLabel.AutoSize = true;
            this.SizeRatioLabel.Location = new System.Drawing.Point(2, 264);
            this.SizeRatioLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SizeRatioLabel.Name = "SizeRatioLabel";
            this.SizeRatioLabel.Size = new System.Drawing.Size(84, 13);
            this.SizeRatioLabel.TabIndex = 19;
            this.SizeRatioLabel.Text = "Bat Radius (mm)";
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(175, 312);
            this.OKButton.Margin = new System.Windows.Forms.Padding(2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(73, 23);
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // AbortButton
            // 
            this.AbortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AbortButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AbortButton.Location = new System.Drawing.Point(251, 312);
            this.AbortButton.Margin = new System.Windows.Forms.Padding(2);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(86, 23);
            this.AbortButton.TabIndex = 3;
            this.AbortButton.Text = "Abort";
            this.AbortButton.UseVisualStyleBackColor = true;
            this.AbortButton.Click += new System.EventHandler(this.AbortButton_Click);
            // 
            // OptionsView
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.AbortButton;
            this.ClientSize = new System.Drawing.Size(347, 342);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "OptionsView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label GameFieldSizeLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox FrameRateTextBox;
        private System.Windows.Forms.TextBox PunchAxisPositionTextBox;
        private System.Windows.Forms.TextBox ToleranceTextBox;
        private System.Windows.Forms.TextBox Camera1IndexTextBox;
        private System.Windows.Forms.TextBox MaximumBatVelocityTextBox;
        private System.Windows.Forms.TextBox RestPositionDivisorTextBox;
        private System.Windows.Forms.Label FrameRateLabel;
        private System.Windows.Forms.Label DifficultyLabel;
        private System.Windows.Forms.Label PunchAxisLabel;
        private System.Windows.Forms.Label ToleranceLabel;
        private System.Windows.Forms.Label Camera1IndexLabel;
        private System.Windows.Forms.Label MaxBatVelocityLabel;
        private System.Windows.Forms.Label RestPositionDivisorLabel;
        private System.Windows.Forms.Label HeightLabel;
        private System.Windows.Forms.Label WidthLabel;
        private System.Windows.Forms.TextBox WidthTextBox;
        private System.Windows.Forms.TextBox HeightTextBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button AbortButton;
        private System.Windows.Forms.ComboBox DifficultyComboBox;
        private System.Windows.Forms.Label PuckRadiusLabel;
        private System.Windows.Forms.TextBox PuckRadiusTextBox;
        private System.Windows.Forms.Label SizeRatioLabel;
        private System.Windows.Forms.TextBox BatRadiusTextBox;
    }
}