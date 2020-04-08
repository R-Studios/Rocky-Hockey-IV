namespace Calibration
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cameraIndex = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.positionUpperLeftX = new System.Windows.Forms.TextBox();
            this.positionUpperLeftY = new System.Windows.Forms.TextBox();
            this.positionUpperRightY = new System.Windows.Forms.TextBox();
            this.positionUpperRightX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.positionLowerLeftY = new System.Windows.Forms.TextBox();
            this.positionLowerLeftX = new System.Windows.Forms.TextBox();
            this.positionLowerRightY = new System.Windows.Forms.TextBox();
            this.positionLowerRightX = new System.Windows.Forms.TextBox();
            this.gameFieldWidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.gameFieldHeight = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.offsetLeftSide = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.offsetTopSide = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.previewButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.picturePreview = new System.Windows.Forms.PictureBox();
            this.connectCameraButton = new System.Windows.Forms.Button();
            this.chooseConfigFileButton = new System.Windows.Forms.Button();
            this.fileLocationLabel = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.configFileFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.camera1Button = new System.Windows.Forms.RadioButton();
            this.camera2Button = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picturePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // cameraIndex
            // 
            this.cameraIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cameraIndex.FormattingEnabled = true;
            this.cameraIndex.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cameraIndex.Location = new System.Drawing.Point(107, 180);
            this.cameraIndex.Name = "cameraIndex";
            this.cameraIndex.Size = new System.Drawing.Size(121, 28);
            this.cameraIndex.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Kamera:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Position oben links (X|Y):";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(848, 73);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // positionUpperLeftX
            // 
            this.positionUpperLeftX.Location = new System.Drawing.Point(236, 225);
            this.positionUpperLeftX.Name = "positionUpperLeftX";
            this.positionUpperLeftX.Size = new System.Drawing.Size(57, 26);
            this.positionUpperLeftX.TabIndex = 9;
            this.positionUpperLeftX.Text = "0";
            // 
            // positionUpperLeftY
            // 
            this.positionUpperLeftY.Location = new System.Drawing.Point(300, 225);
            this.positionUpperLeftY.Name = "positionUpperLeftY";
            this.positionUpperLeftY.Size = new System.Drawing.Size(57, 26);
            this.positionUpperLeftY.TabIndex = 10;
            this.positionUpperLeftY.Text = "0";
            // 
            // positionUpperRightY
            // 
            this.positionUpperRightY.Location = new System.Drawing.Point(300, 257);
            this.positionUpperRightY.Name = "positionUpperRightY";
            this.positionUpperRightY.Size = new System.Drawing.Size(57, 26);
            this.positionUpperRightY.TabIndex = 13;
            this.positionUpperRightY.Text = "0";
            // 
            // positionUpperRightX
            // 
            this.positionUpperRightX.Location = new System.Drawing.Point(236, 257);
            this.positionUpperRightX.Name = "positionUpperRightX";
            this.positionUpperRightX.Size = new System.Drawing.Size(57, 26);
            this.positionUpperRightX.TabIndex = 12;
            this.positionUpperRightX.Text = "320";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 260);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(198, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Position oben rechts (X|Y):";
            // 
            // positionLowerLeftY
            // 
            this.positionLowerLeftY.Location = new System.Drawing.Point(300, 290);
            this.positionLowerLeftY.Name = "positionLowerLeftY";
            this.positionLowerLeftY.Size = new System.Drawing.Size(57, 26);
            this.positionLowerLeftY.TabIndex = 16;
            this.positionLowerLeftY.Text = "240";
            // 
            // positionLowerLeftX
            // 
            this.positionLowerLeftX.Location = new System.Drawing.Point(236, 290);
            this.positionLowerLeftX.Name = "positionLowerLeftX";
            this.positionLowerLeftX.Size = new System.Drawing.Size(57, 26);
            this.positionLowerLeftX.TabIndex = 15;
            this.positionLowerLeftX.Text = "0";
            // 
            // positionLowerRightY
            // 
            this.positionLowerRightY.Location = new System.Drawing.Point(300, 323);
            this.positionLowerRightY.Name = "positionLowerRightY";
            this.positionLowerRightY.Size = new System.Drawing.Size(57, 26);
            this.positionLowerRightY.TabIndex = 19;
            this.positionLowerRightY.Text = "240";
            // 
            // positionLowerRightX
            // 
            this.positionLowerRightX.Location = new System.Drawing.Point(236, 323);
            this.positionLowerRightX.Name = "positionLowerRightX";
            this.positionLowerRightX.Size = new System.Drawing.Size(57, 26);
            this.positionLowerRightX.TabIndex = 18;
            this.positionLowerRightX.Text = "320";
            // 
            // gameFieldWidth
            // 
            this.gameFieldWidth.Location = new System.Drawing.Point(236, 372);
            this.gameFieldWidth.Name = "gameFieldWidth";
            this.gameFieldWidth.Size = new System.Drawing.Size(121, 26);
            this.gameFieldWidth.TabIndex = 21;
            this.gameFieldWidth.Text = "320";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 375);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 20);
            this.label3.TabIndex = 20;
            this.label3.Text = "Spielfeldbreite in Px:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 293);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(190, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "Position unten links (X|Y):";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 326);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(203, 20);
            this.label9.TabIndex = 17;
            this.label9.Text = "Position unten rechts (X|Y):";
            // 
            // gameFieldHeight
            // 
            this.gameFieldHeight.Location = new System.Drawing.Point(236, 405);
            this.gameFieldHeight.Name = "gameFieldHeight";
            this.gameFieldHeight.Size = new System.Drawing.Size(121, 26);
            this.gameFieldHeight.TabIndex = 23;
            this.gameFieldHeight.Text = "240";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 408);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 20);
            this.label6.TabIndex = 22;
            this.label6.Text = "Spielfeldhoehe in Px:";
            // 
            // offsetLeftSide
            // 
            this.offsetLeftSide.Location = new System.Drawing.Point(236, 453);
            this.offsetLeftSide.Name = "offsetLeftSide";
            this.offsetLeftSide.Size = new System.Drawing.Size(121, 26);
            this.offsetLeftSide.TabIndex = 25;
            this.offsetLeftSide.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 456);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(158, 20);
            this.label7.TabIndex = 24;
            this.label7.Text = "Offset von links in Px:";
            // 
            // offsetTopSide
            // 
            this.offsetTopSide.Location = new System.Drawing.Point(236, 485);
            this.offsetTopSide.Name = "offsetTopSide";
            this.offsetTopSide.Size = new System.Drawing.Size(121, 26);
            this.offsetTopSide.TabIndex = 27;
            this.offsetTopSide.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 488);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(163, 20);
            this.label10.TabIndex = 26;
            this.label10.Text = "Offset von oben in Px:";
            // 
            // previewButton
            // 
            this.previewButton.Enabled = false;
            this.previewButton.Location = new System.Drawing.Point(159, 570);
            this.previewButton.Name = "previewButton";
            this.previewButton.Size = new System.Drawing.Size(198, 35);
            this.previewButton.TabIndex = 29;
            this.previewButton.Text = "Vorschau";
            this.previewButton.UseVisualStyleBackColor = true;
            this.previewButton.Click += new System.EventHandler(this.PreviewButtonClick);
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(662, 570);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(198, 35);
            this.saveButton.TabIndex = 30;
            this.saveButton.Text = "Abspeichern";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // picturePreview
            // 
            this.picturePreview.BackColor = System.Drawing.SystemColors.ControlLight;
            this.picturePreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picturePreview.Location = new System.Drawing.Point(373, 176);
            this.picturePreview.Name = "picturePreview";
            this.picturePreview.Size = new System.Drawing.Size(487, 373);
            this.picturePreview.TabIndex = 31;
            this.picturePreview.TabStop = false;
            // 
            // connectCameraButton
            // 
            this.connectCameraButton.Location = new System.Drawing.Point(236, 176);
            this.connectCameraButton.Name = "connectCameraButton";
            this.connectCameraButton.Size = new System.Drawing.Size(121, 35);
            this.connectCameraButton.TabIndex = 32;
            this.connectCameraButton.Text = "Verbinden";
            this.connectCameraButton.UseMnemonic = false;
            this.connectCameraButton.UseVisualStyleBackColor = true;
            this.connectCameraButton.Click += new System.EventHandler(this.ConnectButtonClick);
            // 
            // chooseConfigFileButton
            // 
            this.chooseConfigFileButton.Location = new System.Drawing.Point(714, 98);
            this.chooseConfigFileButton.Name = "chooseConfigFileButton";
            this.chooseConfigFileButton.Size = new System.Drawing.Size(146, 40);
            this.chooseConfigFileButton.TabIndex = 33;
            this.chooseConfigFileButton.Text = "Durchsuchen";
            this.chooseConfigFileButton.UseVisualStyleBackColor = true;
            this.chooseConfigFileButton.UseWaitCursor = true;
            this.chooseConfigFileButton.Click += new System.EventHandler(this.chooseConfigFileButton_Click);
            // 
            // fileLocationLabel
            // 
            this.fileLocationLabel.AutoSize = true;
            this.fileLocationLabel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.fileLocationLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fileLocationLabel.Location = new System.Drawing.Point(168, 98);
            this.fileLocationLabel.MaximumSize = new System.Drawing.Size(540, 40);
            this.fileLocationLabel.MinimumSize = new System.Drawing.Size(540, 40);
            this.fileLocationLabel.Name = "fileLocationLabel";
            this.fileLocationLabel.Padding = new System.Windows.Forms.Padding(5);
            this.fileLocationLabel.Size = new System.Drawing.Size(540, 40);
            this.fileLocationLabel.TabIndex = 35;
            this.fileLocationLabel.Text = "Für das Abspeichern bitte die Konfigurationsdatei auswählen...";
            this.fileLocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox2
            // 
            this.textBox2.AcceptsReturn = true;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(12, 98);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(150, 40);
            this.textBox2.TabIndex = 36;
            this.textBox2.Text = "Speicherort der Konfigurationsdatei: ";
            // 
            // configFileFileDialog
            // 
            this.configFileFileDialog.DefaultExt = "xml";
            this.configFileFileDialog.FileName = "RockyHockyConfig";
            this.configFileFileDialog.Filter = "XML Files|*.xml";
            this.configFileFileDialog.InitialDirectory = "C:\\";
            // 
            // camera1Button
            // 
            this.camera1Button.AutoSize = true;
            this.camera1Button.Location = new System.Drawing.Point(551, 575);
            this.camera1Button.Name = "camera1Button";
            this.camera1Button.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.camera1Button.Size = new System.Drawing.Size(47, 24);
            this.camera1Button.TabIndex = 37;
            this.camera1Button.TabStop = true;
            this.camera1Button.Text = ":1";
            this.camera1Button.UseVisualStyleBackColor = true;
            this.camera1Button.CheckedChanged += new System.EventHandler(this.camera1Button_CheckedChanged);
            // 
            // camera2Button
            // 
            this.camera2Button.AutoSize = true;
            this.camera2Button.Location = new System.Drawing.Point(604, 575);
            this.camera2Button.Name = "camera2Button";
            this.camera2Button.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.camera2Button.Size = new System.Drawing.Size(47, 24);
            this.camera2Button.TabIndex = 38;
            this.camera2Button.TabStop = true;
            this.camera2Button.Text = ":2";
            this.camera2Button.UseVisualStyleBackColor = true;
            this.camera2Button.CheckedChanged += new System.EventHandler(this.camera2Button_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(369, 577);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 20);
            this.label1.TabIndex = 39;
            this.label1.Text = "Speichern fuer Kamera:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 617);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.camera2Button);
            this.Controls.Add(this.camera1Button);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.fileLocationLabel);
            this.Controls.Add(this.chooseConfigFileButton);
            this.Controls.Add(this.connectCameraButton);
            this.Controls.Add(this.picturePreview);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.previewButton);
            this.Controls.Add(this.offsetTopSide);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.offsetLeftSide);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.gameFieldHeight);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gameFieldWidth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.positionLowerRightY);
            this.Controls.Add(this.positionLowerRightX);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.positionLowerLeftY);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.positionLowerLeftX);
            this.Controls.Add(this.positionUpperRightY);
            this.Controls.Add(this.positionUpperRightX);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.positionUpperLeftY);
            this.Controls.Add(this.positionUpperLeftX);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cameraIndex);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picturePreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox positionUpperLeftX;
        private System.Windows.Forms.TextBox positionUpperLeftY;
        private System.Windows.Forms.TextBox positionUpperRightY;
        private System.Windows.Forms.TextBox positionUpperRightX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox positionLowerLeftY;
        private System.Windows.Forms.TextBox positionLowerLeftX;
        private System.Windows.Forms.TextBox positionLowerRightY;
        private System.Windows.Forms.TextBox positionLowerRightX;
        private System.Windows.Forms.TextBox gameFieldWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox gameFieldHeight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox offsetLeftSide;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox offsetTopSide;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button previewButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.PictureBox picturePreview;
        private System.Windows.Forms.ComboBox cameraIndex;
        private System.Windows.Forms.Button connectCameraButton;
        private System.Windows.Forms.Button chooseConfigFileButton;
        private System.Windows.Forms.Label fileLocationLabel;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.OpenFileDialog configFileFileDialog;
        private System.Windows.Forms.RadioButton camera1Button;
        private System.Windows.Forms.RadioButton camera2Button;
        private System.Windows.Forms.Label label1;
    }
}

