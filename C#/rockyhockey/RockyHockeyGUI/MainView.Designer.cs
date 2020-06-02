namespace RockyHockeyGUI
{
    partial class MainView
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.PlotView = new OxyPlot.WindowsForms.PlotView();
            this.OptionsButton = new System.Windows.Forms.Button();
            this.GameTimeLabel = new System.Windows.Forms.Label();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.CalibrateButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(8, 8);
            this.StartButton.Margin = new System.Windows.Forms.Padding(2);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(114, 26);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start Rocky Hockey";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(126, 8);
            this.StopButton.Margin = new System.Windows.Forms.Padding(2);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(114, 26);
            this.StopButton.TabIndex = 1;
            this.StopButton.Text = "Stop Rocky Hockey";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // PlotView
            // 
            this.PlotView.Location = new System.Drawing.Point(8, 39);
            this.PlotView.Margin = new System.Windows.Forms.Padding(2);
            this.PlotView.Name = "PlotView";
            this.PlotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.PlotView.Size = new System.Drawing.Size(764, 391);
            this.PlotView.TabIndex = 0;
            this.PlotView.Text = "PlotView";
            this.PlotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.PlotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.PlotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // OptionsButton
            // 
            this.OptionsButton.Location = new System.Drawing.Point(244, 8);
            this.OptionsButton.Margin = new System.Windows.Forms.Padding(2);
            this.OptionsButton.Name = "OptionsButton";
            this.OptionsButton.Size = new System.Drawing.Size(114, 26);
            this.OptionsButton.TabIndex = 2;
            this.OptionsButton.Text = "Options";
            this.OptionsButton.UseVisualStyleBackColor = true;
            this.OptionsButton.Click += new System.EventHandler(this.OptionsButton_Click);
            // 
            // GameTimeLabel
            // 
            this.GameTimeLabel.AutoSize = true;
            this.GameTimeLabel.Location = new System.Drawing.Point(407, 14);
            this.GameTimeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.GameTimeLabel.Name = "GameTimeLabel";
            this.GameTimeLabel.Size = new System.Drawing.Size(60, 13);
            this.GameTimeLabel.TabIndex = 3;
            this.GameTimeLabel.Text = "Game time:";
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.Location = new System.Drawing.Point(546, 14);
            this.ScoreLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(54, 13);
            this.ScoreLabel.TabIndex = 4;
            this.ScoreLabel.Text = "score: 0:0";
            // 
            // CalibrateButton
            // 
            this.CalibrateButton.Location = new System.Drawing.Point(634, 8);
            this.CalibrateButton.Name = "CalibrateButton";
            this.CalibrateButton.Size = new System.Drawing.Size(138, 26);
            this.CalibrateButton.TabIndex = 5;
            this.CalibrateButton.Text = "calibrate bat";
            this.CalibrateButton.UseVisualStyleBackColor = true;
            this.CalibrateButton.Click += new System.EventHandler(this.CalibrateButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(778, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 240);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 442);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.CalibrateButton);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.GameTimeLabel);
            this.Controls.Add(this.OptionsButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.PlotView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MainView";
            this.Text = "RockyHockey";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private OxyPlot.WindowsForms.PlotView PlotView;
        private System.Windows.Forms.Button OptionsButton;
        private System.Windows.Forms.Label GameTimeLabel;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Button CalibrateButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

