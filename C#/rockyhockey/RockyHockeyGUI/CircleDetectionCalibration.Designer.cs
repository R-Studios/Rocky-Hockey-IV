namespace RockyHockeyGUI
{
    partial class CircleDetectionCalibration
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
            this.MinRadiusTrackbar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.CurrentFrame = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.MaxRadiusTrackbar = new System.Windows.Forms.TrackBar();
            this.GrabImage_Button = new System.Windows.Forms.Button();
            this.FromCamera_Radio = new System.Windows.Forms.RadioButton();
            this.FromVideo_Radio = new System.Windows.Forms.RadioButton();
            this.FromImage_Radio = new System.Windows.Forms.RadioButton();
            this.ImageIndex_Box = new System.Windows.Forms.TextBox();
            this.CalibrateReader_Button = new System.Windows.Forms.Button();
            this.ImageDebugging_Button = new System.Windows.Forms.Button();
            this.FoundCircles_Box = new System.Windows.Forms.TextBox();
            this.MinRadiusdisplay_Box = new System.Windows.Forms.TextBox();
            this.MaxRadiusdisplay_Box = new System.Windows.Forms.TextBox();
            this.MinDistanceDisplay_Box = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MinDistanceTrackbar = new System.Windows.Forms.TrackBar();
            this.DPDisplay_Box = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DPTrackBar = new System.Windows.Forms.TrackBar();
            this.Param1Display_Box = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Param1trackBar = new System.Windows.Forms.TrackBar();
            this.Param2Display_Box = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Param2trackBar = new System.Windows.Forms.TrackBar();
            this.ImageStreamStart_Button = new System.Windows.Forms.Button();
            this.Save_Button = new System.Windows.Forms.Button();
            this.ImageIndex_Label = new System.Windows.Forms.Label();
            this.StopImageStream_Button = new System.Windows.Forms.Button();
            this.UseConfigParams_Button = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.MinRadiusTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentFrame)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxRadiusTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinDistanceTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DPTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Param1trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Param2trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // MinRadiusTrackbar
            // 
            this.MinRadiusTrackbar.Location = new System.Drawing.Point(72, 24);
            this.MinRadiusTrackbar.Maximum = 100;
            this.MinRadiusTrackbar.Minimum = 1;
            this.MinRadiusTrackbar.Name = "MinRadiusTrackbar";
            this.MinRadiusTrackbar.Size = new System.Drawing.Size(321, 45);
            this.MinRadiusTrackbar.TabIndex = 0;
            this.MinRadiusTrackbar.Value = 1;
            this.MinRadiusTrackbar.Scroll += new System.EventHandler(this.MinRadiusTrackbar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "min radius";
            // 
            // CurrentFrame
            // 
            this.CurrentFrame.Location = new System.Drawing.Point(1, 0);
            this.CurrentFrame.Name = "CurrentFrame";
            this.CurrentFrame.Size = new System.Drawing.Size(320, 634);
            this.CurrentFrame.TabIndex = 2;
            this.CurrentFrame.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.CurrentFrame);
            this.panel1.Location = new System.Drawing.Point(606, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 634);
            this.panel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "max radius";
            // 
            // MaxRadiusTrackbar
            // 
            this.MaxRadiusTrackbar.Location = new System.Drawing.Point(68, 75);
            this.MaxRadiusTrackbar.Maximum = 100;
            this.MaxRadiusTrackbar.Minimum = 1;
            this.MaxRadiusTrackbar.Name = "MaxRadiusTrackbar";
            this.MaxRadiusTrackbar.Size = new System.Drawing.Size(321, 45);
            this.MaxRadiusTrackbar.TabIndex = 4;
            this.MaxRadiusTrackbar.Value = 100;
            this.MaxRadiusTrackbar.Scroll += new System.EventHandler(this.MaxRadiusTrackbar_Scroll);
            // 
            // GrabImage_Button
            // 
            this.GrabImage_Button.Location = new System.Drawing.Point(473, 239);
            this.GrabImage_Button.Name = "GrabImage_Button";
            this.GrabImage_Button.Size = new System.Drawing.Size(113, 26);
            this.GrabImage_Button.TabIndex = 6;
            this.GrabImage_Button.Text = "grab image";
            this.GrabImage_Button.UseVisualStyleBackColor = true;
            this.GrabImage_Button.Click += new System.EventHandler(this.GrabImage_Button_Click);
            // 
            // FromCamera_Radio
            // 
            this.FromCamera_Radio.AutoSize = true;
            this.FromCamera_Radio.Location = new System.Drawing.Point(467, 12);
            this.FromCamera_Radio.Name = "FromCamera_Radio";
            this.FromCamera_Radio.Size = new System.Drawing.Size(83, 17);
            this.FromCamera_Radio.TabIndex = 7;
            this.FromCamera_Radio.TabStop = true;
            this.FromCamera_Radio.Text = "from camera";
            this.FromCamera_Radio.UseVisualStyleBackColor = true;
            this.FromCamera_Radio.CheckedChanged += new System.EventHandler(this.FromCamera_Radio_CheckedChanged);
            // 
            // FromVideo_Radio
            // 
            this.FromVideo_Radio.AutoSize = true;
            this.FromVideo_Radio.Location = new System.Drawing.Point(467, 35);
            this.FromVideo_Radio.Name = "FromVideo_Radio";
            this.FromVideo_Radio.Size = new System.Drawing.Size(74, 17);
            this.FromVideo_Radio.TabIndex = 8;
            this.FromVideo_Radio.TabStop = true;
            this.FromVideo_Radio.Text = "from video";
            this.FromVideo_Radio.UseVisualStyleBackColor = true;
            this.FromVideo_Radio.CheckedChanged += new System.EventHandler(this.FromVideo_Radio_CheckedChanged);
            // 
            // FromImage_Radio
            // 
            this.FromImage_Radio.AutoSize = true;
            this.FromImage_Radio.Location = new System.Drawing.Point(467, 58);
            this.FromImage_Radio.Name = "FromImage_Radio";
            this.FromImage_Radio.Size = new System.Drawing.Size(76, 17);
            this.FromImage_Radio.TabIndex = 9;
            this.FromImage_Radio.TabStop = true;
            this.FromImage_Radio.Text = "from image";
            this.FromImage_Radio.UseVisualStyleBackColor = true;
            this.FromImage_Radio.CheckedChanged += new System.EventHandler(this.FromImage_Radio_CheckedChanged);
            // 
            // ImageIndex_Box
            // 
            this.ImageIndex_Box.Location = new System.Drawing.Point(497, 201);
            this.ImageIndex_Box.Name = "ImageIndex_Box";
            this.ImageIndex_Box.Size = new System.Drawing.Size(100, 20);
            this.ImageIndex_Box.TabIndex = 10;
            this.ImageIndex_Box.Visible = false;
            // 
            // CalibrateReader_Button
            // 
            this.CalibrateReader_Button.Location = new System.Drawing.Point(421, 404);
            this.CalibrateReader_Button.Name = "CalibrateReader_Button";
            this.CalibrateReader_Button.Size = new System.Drawing.Size(122, 26);
            this.CalibrateReader_Button.TabIndex = 11;
            this.CalibrateReader_Button.Text = "calibrate reader";
            this.CalibrateReader_Button.UseVisualStyleBackColor = true;
            this.CalibrateReader_Button.Click += new System.EventHandler(this.CalibrateReader_Button_Click);
            // 
            // ImageDebugging_Button
            // 
            this.ImageDebugging_Button.Location = new System.Drawing.Point(421, 372);
            this.ImageDebugging_Button.Name = "ImageDebugging_Button";
            this.ImageDebugging_Button.Size = new System.Drawing.Size(122, 26);
            this.ImageDebugging_Button.TabIndex = 12;
            this.ImageDebugging_Button.Text = "debug image";
            this.ImageDebugging_Button.UseVisualStyleBackColor = true;
            this.ImageDebugging_Button.Click += new System.EventHandler(this.ImageDebugging_Button_Click);
            // 
            // FoundCircles_Box
            // 
            this.FoundCircles_Box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FoundCircles_Box.Location = new System.Drawing.Point(6, 340);
            this.FoundCircles_Box.Multiline = true;
            this.FoundCircles_Box.Name = "FoundCircles_Box";
            this.FoundCircles_Box.Size = new System.Drawing.Size(320, 306);
            this.FoundCircles_Box.TabIndex = 13;
            // 
            // MinRadiusdisplay_Box
            // 
            this.MinRadiusdisplay_Box.Enabled = false;
            this.MinRadiusdisplay_Box.Location = new System.Drawing.Point(395, 24);
            this.MinRadiusdisplay_Box.Name = "MinRadiusdisplay_Box";
            this.MinRadiusdisplay_Box.Size = new System.Drawing.Size(35, 20);
            this.MinRadiusdisplay_Box.TabIndex = 14;
            // 
            // MaxRadiusdisplay_Box
            // 
            this.MaxRadiusdisplay_Box.Enabled = false;
            this.MaxRadiusdisplay_Box.Location = new System.Drawing.Point(395, 75);
            this.MaxRadiusdisplay_Box.Name = "MaxRadiusdisplay_Box";
            this.MaxRadiusdisplay_Box.Size = new System.Drawing.Size(35, 20);
            this.MaxRadiusdisplay_Box.TabIndex = 15;
            // 
            // MinDistanceDisplay_Box
            // 
            this.MinDistanceDisplay_Box.Enabled = false;
            this.MinDistanceDisplay_Box.Location = new System.Drawing.Point(395, 126);
            this.MinDistanceDisplay_Box.Name = "MinDistanceDisplay_Box";
            this.MinDistanceDisplay_Box.Size = new System.Drawing.Size(35, 20);
            this.MinDistanceDisplay_Box.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "min distance";
            // 
            // MinDistanceTrackbar
            // 
            this.MinDistanceTrackbar.Location = new System.Drawing.Point(68, 126);
            this.MinDistanceTrackbar.Maximum = 100;
            this.MinDistanceTrackbar.Minimum = 1;
            this.MinDistanceTrackbar.Name = "MinDistanceTrackbar";
            this.MinDistanceTrackbar.Size = new System.Drawing.Size(321, 45);
            this.MinDistanceTrackbar.TabIndex = 16;
            this.MinDistanceTrackbar.Value = 1;
            this.MinDistanceTrackbar.Scroll += new System.EventHandler(this.MinDistanceTrackbar_Scroll);
            // 
            // DPDisplay_Box
            // 
            this.DPDisplay_Box.Enabled = false;
            this.DPDisplay_Box.Location = new System.Drawing.Point(395, 177);
            this.DPDisplay_Box.Name = "DPDisplay_Box";
            this.DPDisplay_Box.Size = new System.Drawing.Size(35, 20);
            this.DPDisplay_Box.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "dp";
            // 
            // DPTrackBar
            // 
            this.DPTrackBar.Location = new System.Drawing.Point(68, 177);
            this.DPTrackBar.Maximum = 100;
            this.DPTrackBar.Minimum = 1;
            this.DPTrackBar.Name = "DPTrackBar";
            this.DPTrackBar.Size = new System.Drawing.Size(321, 45);
            this.DPTrackBar.TabIndex = 19;
            this.DPTrackBar.Value = 1;
            this.DPTrackBar.Scroll += new System.EventHandler(this.DPTrackBar_Scroll);
            // 
            // Param1Display_Box
            // 
            this.Param1Display_Box.Enabled = false;
            this.Param1Display_Box.Location = new System.Drawing.Point(399, 228);
            this.Param1Display_Box.Name = "Param1Display_Box";
            this.Param1Display_Box.Size = new System.Drawing.Size(35, 20);
            this.Param1Display_Box.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "param1";
            // 
            // Param1trackBar
            // 
            this.Param1trackBar.Location = new System.Drawing.Point(72, 228);
            this.Param1trackBar.Maximum = 200;
            this.Param1trackBar.Minimum = 1;
            this.Param1trackBar.Name = "Param1trackBar";
            this.Param1trackBar.Size = new System.Drawing.Size(321, 45);
            this.Param1trackBar.TabIndex = 22;
            this.Param1trackBar.Value = 1;
            this.Param1trackBar.Scroll += new System.EventHandler(this.Param1trackBar_Scroll);
            // 
            // Param2Display_Box
            // 
            this.Param2Display_Box.Enabled = false;
            this.Param2Display_Box.Location = new System.Drawing.Point(399, 279);
            this.Param2Display_Box.Name = "Param2Display_Box";
            this.Param2Display_Box.Size = new System.Drawing.Size(35, 20);
            this.Param2Display_Box.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 282);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "param2";
            // 
            // Param2trackBar
            // 
            this.Param2trackBar.Location = new System.Drawing.Point(72, 279);
            this.Param2trackBar.Maximum = 200;
            this.Param2trackBar.Minimum = 1;
            this.Param2trackBar.Name = "Param2trackBar";
            this.Param2trackBar.Size = new System.Drawing.Size(321, 45);
            this.Param2trackBar.TabIndex = 25;
            this.Param2trackBar.Value = 1;
            this.Param2trackBar.Scroll += new System.EventHandler(this.Param2trackBar_Scroll);
            // 
            // ImageStreamStart_Button
            // 
            this.ImageStreamStart_Button.Location = new System.Drawing.Point(483, 280);
            this.ImageStreamStart_Button.Name = "ImageStreamStart_Button";
            this.ImageStreamStart_Button.Size = new System.Drawing.Size(113, 26);
            this.ImageStreamStart_Button.TabIndex = 28;
            this.ImageStreamStart_Button.Text = "stream images";
            this.ImageStreamStart_Button.UseVisualStyleBackColor = true;
            this.ImageStreamStart_Button.Click += new System.EventHandler(this.ImageStreamStart_Button_Click);
            // 
            // Save_Button
            // 
            this.Save_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Save_Button.Location = new System.Drawing.Point(464, 620);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(113, 26);
            this.Save_Button.TabIndex = 29;
            this.Save_Button.Text = "save";
            this.Save_Button.UseVisualStyleBackColor = true;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // ImageIndex_Label
            // 
            this.ImageIndex_Label.AutoSize = true;
            this.ImageIndex_Label.Location = new System.Drawing.Point(464, 182);
            this.ImageIndex_Label.Name = "ImageIndex_Label";
            this.ImageIndex_Label.Size = new System.Drawing.Size(63, 13);
            this.ImageIndex_Label.TabIndex = 30;
            this.ImageIndex_Label.Text = "image index";
            this.ImageIndex_Label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // StopImageStream_Button
            // 
            this.StopImageStream_Button.Location = new System.Drawing.Point(484, 312);
            this.StopImageStream_Button.Name = "StopImageStream_Button";
            this.StopImageStream_Button.Size = new System.Drawing.Size(113, 26);
            this.StopImageStream_Button.TabIndex = 31;
            this.StopImageStream_Button.Text = "stop stream";
            this.StopImageStream_Button.UseVisualStyleBackColor = true;
            this.StopImageStream_Button.Click += new System.EventHandler(this.StopImageStream_Button_Click);
            // 
            // UseConfigParams_Button
            // 
            this.UseConfigParams_Button.AutoSize = true;
            this.UseConfigParams_Button.Location = new System.Drawing.Point(470, 109);
            this.UseConfigParams_Button.Name = "UseConfigParams_Button";
            this.UseConfigParams_Button.Size = new System.Drawing.Size(123, 17);
            this.UseConfigParams_Button.TabIndex = 32;
            this.UseConfigParams_Button.Text = "use params in config";
            this.UseConfigParams_Button.UseVisualStyleBackColor = true;
            // 
            // CircleDetectionCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 658);
            this.Controls.Add(this.UseConfigParams_Button);
            this.Controls.Add(this.StopImageStream_Button);
            this.Controls.Add(this.ImageIndex_Label);
            this.Controls.Add(this.Save_Button);
            this.Controls.Add(this.ImageStreamStart_Button);
            this.Controls.Add(this.Param2Display_Box);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Param2trackBar);
            this.Controls.Add(this.Param1Display_Box);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Param1trackBar);
            this.Controls.Add(this.DPDisplay_Box);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DPTrackBar);
            this.Controls.Add(this.MinDistanceDisplay_Box);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MinDistanceTrackbar);
            this.Controls.Add(this.MaxRadiusdisplay_Box);
            this.Controls.Add(this.MinRadiusdisplay_Box);
            this.Controls.Add(this.FoundCircles_Box);
            this.Controls.Add(this.ImageDebugging_Button);
            this.Controls.Add(this.CalibrateReader_Button);
            this.Controls.Add(this.ImageIndex_Box);
            this.Controls.Add(this.FromImage_Radio);
            this.Controls.Add(this.FromVideo_Radio);
            this.Controls.Add(this.FromCamera_Radio);
            this.Controls.Add(this.GrabImage_Button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MaxRadiusTrackbar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MinRadiusTrackbar);
            this.Name = "CircleDetectionCalibration";
            this.Text = "CircleDetectionCalibration";
            ((System.ComponentModel.ISupportInitialize)(this.MinRadiusTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentFrame)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MaxRadiusTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinDistanceTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DPTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Param1trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Param2trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar MinRadiusTrackbar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox CurrentFrame;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar MaxRadiusTrackbar;
        private System.Windows.Forms.Button GrabImage_Button;
        private System.Windows.Forms.RadioButton FromCamera_Radio;
        private System.Windows.Forms.RadioButton FromVideo_Radio;
        private System.Windows.Forms.RadioButton FromImage_Radio;
        private System.Windows.Forms.TextBox ImageIndex_Box;
        private System.Windows.Forms.Button CalibrateReader_Button;
        private System.Windows.Forms.Button ImageDebugging_Button;
        private System.Windows.Forms.TextBox FoundCircles_Box;
        private System.Windows.Forms.TextBox MinRadiusdisplay_Box;
        private System.Windows.Forms.TextBox MaxRadiusdisplay_Box;
        private System.Windows.Forms.TextBox MinDistanceDisplay_Box;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar MinDistanceTrackbar;
        private System.Windows.Forms.TextBox DPDisplay_Box;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar DPTrackBar;
        private System.Windows.Forms.TextBox Param1Display_Box;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar Param1trackBar;
        private System.Windows.Forms.TextBox Param2Display_Box;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar Param2trackBar;
        private System.Windows.Forms.Button ImageStreamStart_Button;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.Label ImageIndex_Label;
        private System.Windows.Forms.Button StopImageStream_Button;
        private System.Windows.Forms.CheckBox UseConfigParams_Button;
    }
}