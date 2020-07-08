namespace RockyHockeyGUI
{
    partial class CameraCalibration
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.GrabImage_Button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TopLeft_Radio = new System.Windows.Forms.RadioButton();
            this.TopRight_Radio = new System.Windows.Forms.RadioButton();
            this.BottomRight_Radio = new System.Windows.Forms.RadioButton();
            this.BottomLeft_Radio = new System.Windows.Forms.RadioButton();
            this.ImageSlicingCalibration_Panel = new System.Windows.Forms.Panel();
            this.PreciewClickLocation = new System.Windows.Forms.TextBox();
            this.DisplaysOrigin_CheckBox = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.Rotation_ComboBox = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.BottomLeft_Y = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.BottomLeft_X = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DrawLines_CheckBox = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.BottomRight_Y = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BottomRight_X = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TopRight_Y = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TopRight_X = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ShowResult_Button = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TopLeft_Y = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TopLeft_X = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConfigFieldsize_CheckBox = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Save_Button = new System.Windows.Forms.Button();
            this.ZoomFactor_ComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.LoadConfig_Button = new System.Windows.Forms.Button();
            this.ResolutionCalibration_Panel = new System.Windows.Forms.Panel();
            this.ChangeResolution_Button = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.ResolutionHeight_Box = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ResolutionWidth_Box = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.ImageSlicingCalibration_Panel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.ResolutionCalibration_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(-1, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(420, 280);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // GrabImage_Button
            // 
            this.GrabImage_Button.Location = new System.Drawing.Point(3, 4);
            this.GrabImage_Button.Name = "GrabImage_Button";
            this.GrabImage_Button.Size = new System.Drawing.Size(109, 23);
            this.GrabImage_Button.TabIndex = 1;
            this.GrabImage_Button.Text = "grab image";
            this.GrabImage_Button.UseVisualStyleBackColor = true;
            this.GrabImage_Button.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(3, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 646);
            this.panel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 279);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // TopLeft_Radio
            // 
            this.TopLeft_Radio.AutoSize = true;
            this.TopLeft_Radio.Location = new System.Drawing.Point(20, 80);
            this.TopLeft_Radio.Name = "TopLeft_Radio";
            this.TopLeft_Radio.Size = new System.Drawing.Size(107, 17);
            this.TopLeft_Radio.TabIndex = 5;
            this.TopLeft_Radio.TabStop = true;
            this.TopLeft_Radio.Text = "set top left corner";
            this.TopLeft_Radio.UseVisualStyleBackColor = true;
            this.TopLeft_Radio.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // TopRight_Radio
            // 
            this.TopRight_Radio.AutoSize = true;
            this.TopRight_Radio.Location = new System.Drawing.Point(20, 107);
            this.TopRight_Radio.Name = "TopRight_Radio";
            this.TopRight_Radio.Size = new System.Drawing.Size(113, 17);
            this.TopRight_Radio.TabIndex = 6;
            this.TopRight_Radio.TabStop = true;
            this.TopRight_Radio.Text = "set top right corner";
            this.TopRight_Radio.UseVisualStyleBackColor = true;
            this.TopRight_Radio.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // BottomRight_Radio
            // 
            this.BottomRight_Radio.AutoSize = true;
            this.BottomRight_Radio.Location = new System.Drawing.Point(20, 135);
            this.BottomRight_Radio.Name = "BottomRight_Radio";
            this.BottomRight_Radio.Size = new System.Drawing.Size(130, 17);
            this.BottomRight_Radio.TabIndex = 7;
            this.BottomRight_Radio.TabStop = true;
            this.BottomRight_Radio.Text = "set bottom right corner";
            this.BottomRight_Radio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BottomRight_Radio.UseVisualStyleBackColor = true;
            this.BottomRight_Radio.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // BottomLeft_Radio
            // 
            this.BottomLeft_Radio.AutoSize = true;
            this.BottomLeft_Radio.Location = new System.Drawing.Point(20, 163);
            this.BottomLeft_Radio.Name = "BottomLeft_Radio";
            this.BottomLeft_Radio.Size = new System.Drawing.Size(124, 17);
            this.BottomLeft_Radio.TabIndex = 8;
            this.BottomLeft_Radio.TabStop = true;
            this.BottomLeft_Radio.Text = "set bottom left corner";
            this.BottomLeft_Radio.UseVisualStyleBackColor = true;
            this.BottomLeft_Radio.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // ImageSlicingCalibration_Panel
            // 
            this.ImageSlicingCalibration_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageSlicingCalibration_Panel.Controls.Add(this.PreciewClickLocation);
            this.ImageSlicingCalibration_Panel.Controls.Add(this.DisplaysOrigin_CheckBox);
            this.ImageSlicingCalibration_Panel.Controls.Add(this.panel2);
            this.ImageSlicingCalibration_Panel.Controls.Add(this.label17);
            this.ImageSlicingCalibration_Panel.Controls.Add(this.Rotation_ComboBox);
            this.ImageSlicingCalibration_Panel.Controls.Add(this.panel6);
            this.ImageSlicingCalibration_Panel.Controls.Add(this.DrawLines_CheckBox);
            this.ImageSlicingCalibration_Panel.Controls.Add(this.panel5);
            this.ImageSlicingCalibration_Panel.Controls.Add(this.panel4);
            this.ImageSlicingCalibration_Panel.Controls.Add(this.ShowResult_Button);
            this.ImageSlicingCalibration_Panel.Controls.Add(this.panel3);
            this.ImageSlicingCalibration_Panel.Controls.Add(this.BottomLeft_Radio);
            this.ImageSlicingCalibration_Panel.Controls.Add(this.BottomRight_Radio);
            this.ImageSlicingCalibration_Panel.Controls.Add(this.TopLeft_Radio);
            this.ImageSlicingCalibration_Panel.Controls.Add(this.TopRight_Radio);
            this.ImageSlicingCalibration_Panel.Location = new System.Drawing.Point(568, 104);
            this.ImageSlicingCalibration_Panel.Name = "ImageSlicingCalibration_Panel";
            this.ImageSlicingCalibration_Panel.Size = new System.Drawing.Size(430, 575);
            this.ImageSlicingCalibration_Panel.TabIndex = 3;
            // 
            // PreciewClickLocation
            // 
            this.PreciewClickLocation.Enabled = false;
            this.PreciewClickLocation.Location = new System.Drawing.Point(126, 253);
            this.PreciewClickLocation.Name = "PreciewClickLocation";
            this.PreciewClickLocation.Size = new System.Drawing.Size(127, 20);
            this.PreciewClickLocation.TabIndex = 17;
            // 
            // DisplaysOrigin_CheckBox
            // 
            this.DisplaysOrigin_CheckBox.AutoSize = true;
            this.DisplaysOrigin_CheckBox.Location = new System.Drawing.Point(269, 209);
            this.DisplaysOrigin_CheckBox.Name = "DisplaysOrigin_CheckBox";
            this.DisplaysOrigin_CheckBox.Size = new System.Drawing.Size(91, 17);
            this.DisplaysOrigin_CheckBox.TabIndex = 15;
            this.DisplaysOrigin_CheckBox.Text = "displays origin";
            this.DisplaysOrigin_CheckBox.UseVisualStyleBackColor = true;
            this.DisplaysOrigin_CheckBox.CheckedChanged += new System.EventHandler(this.DisplaysOrigin_CheckBox_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(6, 280);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(421, 283);
            this.panel2.TabIndex = 16;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(22, 206);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 13);
            this.label17.TabIndex = 14;
            this.label17.Text = "rotate image";
            // 
            // Rotation_ComboBox
            // 
            this.Rotation_ComboBox.FormattingEnabled = true;
            this.Rotation_ComboBox.Location = new System.Drawing.Point(115, 205);
            this.Rotation_ComboBox.Name = "Rotation_ComboBox";
            this.Rotation_ComboBox.Size = new System.Drawing.Size(102, 21);
            this.Rotation_ComboBox.TabIndex = 13;
            this.Rotation_ComboBox.SelectedIndexChanged += new System.EventHandler(this.Rotation_ComboBox_SelectedIndexChanged);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.BottomLeft_Y);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.BottomLeft_X);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Location = new System.Drawing.Point(165, 161);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(252, 22);
            this.panel6.TabIndex = 12;
            // 
            // BottomLeft_Y
            // 
            this.BottomLeft_Y.Location = new System.Drawing.Point(151, 1);
            this.BottomLeft_Y.Name = "BottomLeft_Y";
            this.BottomLeft_Y.Size = new System.Drawing.Size(100, 20);
            this.BottomLeft_Y.TabIndex = 2;
            this.BottomLeft_Y.TextChanged += new System.EventHandler(this.BottomLeft_Y_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(131, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Y";
            // 
            // BottomLeft_X
            // 
            this.BottomLeft_X.Location = new System.Drawing.Point(21, 1);
            this.BottomLeft_X.Name = "BottomLeft_X";
            this.BottomLeft_X.Size = new System.Drawing.Size(100, 20);
            this.BottomLeft_X.TabIndex = 1;
            this.BottomLeft_X.TextChanged += new System.EventHandler(this.BottomLeft_X_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "X";
            // 
            // DrawLines_CheckBox
            // 
            this.DrawLines_CheckBox.AutoSize = true;
            this.DrawLines_CheckBox.Location = new System.Drawing.Point(32, 14);
            this.DrawLines_CheckBox.Name = "DrawLines_CheckBox";
            this.DrawLines_CheckBox.Size = new System.Drawing.Size(73, 17);
            this.DrawLines_CheckBox.TabIndex = 3;
            this.DrawLines_CheckBox.Text = "draw lines";
            this.DrawLines_CheckBox.UseVisualStyleBackColor = true;
            this.DrawLines_CheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.BottomRight_Y);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.BottomRight_X);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Location = new System.Drawing.Point(165, 133);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(252, 22);
            this.panel5.TabIndex = 11;
            // 
            // BottomRight_Y
            // 
            this.BottomRight_Y.Location = new System.Drawing.Point(151, 1);
            this.BottomRight_Y.Name = "BottomRight_Y";
            this.BottomRight_Y.Size = new System.Drawing.Size(100, 20);
            this.BottomRight_Y.TabIndex = 2;
            this.BottomRight_Y.TextChanged += new System.EventHandler(this.BottomRight_Y_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(131, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Y";
            // 
            // BottomRight_X
            // 
            this.BottomRight_X.Location = new System.Drawing.Point(21, 1);
            this.BottomRight_X.Name = "BottomRight_X";
            this.BottomRight_X.Size = new System.Drawing.Size(100, 20);
            this.BottomRight_X.TabIndex = 1;
            this.BottomRight_X.TextChanged += new System.EventHandler(this.BottomRight_X_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "X";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.TopRight_Y);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.TopRight_X);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(165, 105);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(252, 22);
            this.panel4.TabIndex = 10;
            // 
            // TopRight_Y
            // 
            this.TopRight_Y.Location = new System.Drawing.Point(151, 1);
            this.TopRight_Y.Name = "TopRight_Y";
            this.TopRight_Y.Size = new System.Drawing.Size(100, 20);
            this.TopRight_Y.TabIndex = 2;
            this.TopRight_Y.TextChanged += new System.EventHandler(this.TopRight_Y_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Y";
            // 
            // TopRight_X
            // 
            this.TopRight_X.Location = new System.Drawing.Point(21, 1);
            this.TopRight_X.Name = "TopRight_X";
            this.TopRight_X.Size = new System.Drawing.Size(100, 20);
            this.TopRight_X.TabIndex = 1;
            this.TopRight_X.TextChanged += new System.EventHandler(this.TopRight_X_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "X";
            // 
            // ShowResult_Button
            // 
            this.ShowResult_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowResult_Button.Location = new System.Drawing.Point(5, 254);
            this.ShowResult_Button.Name = "ShowResult_Button";
            this.ShowResult_Button.Size = new System.Drawing.Size(90, 20);
            this.ShowResult_Button.TabIndex = 4;
            this.ShowResult_Button.Text = "show result";
            this.ShowResult_Button.UseVisualStyleBackColor = true;
            this.ShowResult_Button.Click += new System.EventHandler(this.ShowResult_Button_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.TopLeft_Y);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.TopLeft_X);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(165, 78);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(252, 22);
            this.panel3.TabIndex = 9;
            // 
            // TopLeft_Y
            // 
            this.TopLeft_Y.Location = new System.Drawing.Point(151, 1);
            this.TopLeft_Y.Name = "TopLeft_Y";
            this.TopLeft_Y.Size = new System.Drawing.Size(100, 20);
            this.TopLeft_Y.TabIndex = 2;
            this.TopLeft_Y.TextChanged += new System.EventHandler(this.TopLeft_Y_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y";
            // 
            // TopLeft_X
            // 
            this.TopLeft_X.Location = new System.Drawing.Point(21, 1);
            this.TopLeft_X.Name = "TopLeft_X";
            this.TopLeft_X.Size = new System.Drawing.Size(100, 20);
            this.TopLeft_X.TabIndex = 1;
            this.TopLeft_X.TextChanged += new System.EventHandler(this.TopLeft_X_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // ConfigFieldsize_CheckBox
            // 
            this.ConfigFieldsize_CheckBox.AutoSize = true;
            this.ConfigFieldsize_CheckBox.Checked = true;
            this.ConfigFieldsize_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConfigFieldsize_CheckBox.Location = new System.Drawing.Point(133, 8);
            this.ConfigFieldsize_CheckBox.Name = "ConfigFieldsize_CheckBox";
            this.ConfigFieldsize_CheckBox.Size = new System.Drawing.Size(179, 17);
            this.ConfigFieldsize_CheckBox.TabIndex = 2;
            this.ConfigFieldsize_CheckBox.Text = "use width and height from config";
            this.ConfigFieldsize_CheckBox.UseVisualStyleBackColor = true;
            this.ConfigFieldsize_CheckBox.CheckedChanged += new System.EventHandler(this.ConfigFieldsize_CheckBox_CheckedChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Save_Button
            // 
            this.Save_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Save_Button.Location = new System.Drawing.Point(923, 685);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(75, 23);
            this.Save_Button.TabIndex = 12;
            this.Save_Button.Text = "save";
            this.Save_Button.UseVisualStyleBackColor = true;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // ZoomFactor_ComboBox
            // 
            this.ZoomFactor_ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ZoomFactor_ComboBox.FormattingEnabled = true;
            this.ZoomFactor_ComboBox.Location = new System.Drawing.Point(445, 687);
            this.ZoomFactor_ComboBox.Name = "ZoomFactor_ComboBox";
            this.ZoomFactor_ComboBox.Size = new System.Drawing.Size(97, 21);
            this.ZoomFactor_ComboBox.TabIndex = 13;
            this.ZoomFactor_ComboBox.SelectedIndexChanged += new System.EventHandler(this.ZoomFactor_ComboBox_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(407, 690);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "zoom";
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel_Button.Location = new System.Drawing.Point(803, 685);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_Button.TabIndex = 17;
            this.Cancel_Button.Text = "cancel";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // LoadConfig_Button
            // 
            this.LoadConfig_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadConfig_Button.Location = new System.Drawing.Point(923, 12);
            this.LoadConfig_Button.Name = "LoadConfig_Button";
            this.LoadConfig_Button.Size = new System.Drawing.Size(75, 23);
            this.LoadConfig_Button.TabIndex = 18;
            this.LoadConfig_Button.Text = "load config";
            this.LoadConfig_Button.UseVisualStyleBackColor = true;
            this.LoadConfig_Button.Click += new System.EventHandler(this.LoadConfig_Button_Click);
            // 
            // ResolutionCalibration_Panel
            // 
            this.ResolutionCalibration_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResolutionCalibration_Panel.Controls.Add(this.ChangeResolution_Button);
            this.ResolutionCalibration_Panel.Controls.Add(this.label12);
            this.ResolutionCalibration_Panel.Controls.Add(this.ResolutionHeight_Box);
            this.ResolutionCalibration_Panel.Controls.Add(this.label10);
            this.ResolutionCalibration_Panel.Controls.Add(this.ResolutionWidth_Box);
            this.ResolutionCalibration_Panel.Controls.Add(this.label11);
            this.ResolutionCalibration_Panel.Location = new System.Drawing.Point(569, 4);
            this.ResolutionCalibration_Panel.Name = "ResolutionCalibration_Panel";
            this.ResolutionCalibration_Panel.Size = new System.Drawing.Size(316, 94);
            this.ResolutionCalibration_Panel.TabIndex = 10;
            // 
            // ChangeResolution_Button
            // 
            this.ChangeResolution_Button.Location = new System.Drawing.Point(210, 68);
            this.ChangeResolution_Button.Name = "ChangeResolution_Button";
            this.ChangeResolution_Button.Size = new System.Drawing.Size(99, 23);
            this.ChangeResolution_Button.TabIndex = 4;
            this.ChangeResolution_Button.Text = "change resolution";
            this.ChangeResolution_Button.UseVisualStyleBackColor = true;
            this.ChangeResolution_Button.Click += new System.EventHandler(this.ChangeResolution_Button_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(2, 5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Resolution";
            // 
            // ResolutionHeight_Box
            // 
            this.ResolutionHeight_Box.Location = new System.Drawing.Point(88, 52);
            this.ResolutionHeight_Box.Name = "ResolutionHeight_Box";
            this.ResolutionHeight_Box.Size = new System.Drawing.Size(100, 20);
            this.ResolutionHeight_Box.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Height";
            // 
            // ResolutionWidth_Box
            // 
            this.ResolutionWidth_Box.Location = new System.Drawing.Point(88, 26);
            this.ResolutionWidth_Box.Name = "ResolutionWidth_Box";
            this.ResolutionWidth_Box.Size = new System.Drawing.Size(100, 20);
            this.ResolutionWidth_Box.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(27, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Width";
            // 
            // CameraCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 711);
            this.Controls.Add(this.ResolutionCalibration_Panel);
            this.Controls.Add(this.LoadConfig_Button);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ZoomFactor_ComboBox);
            this.Controls.Add(this.Save_Button);
            this.Controls.Add(this.ConfigFieldsize_CheckBox);
            this.Controls.Add(this.ImageSlicingCalibration_Panel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.GrabImage_Button);
            this.Name = "CameraCalibration";
            this.Text = "CameraCalibration";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ImageSlicingCalibration_Panel.ResumeLayout(false);
            this.ImageSlicingCalibration_Panel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResolutionCalibration_Panel.ResumeLayout(false);
            this.ResolutionCalibration_Panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button GrabImage_Button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton TopLeft_Radio;
        private System.Windows.Forms.RadioButton TopRight_Radio;
        private System.Windows.Forms.RadioButton BottomRight_Radio;
        private System.Windows.Forms.RadioButton BottomLeft_Radio;
        private System.Windows.Forms.Panel ImageSlicingCalibration_Panel;
        private System.Windows.Forms.CheckBox DrawLines_CheckBox;
        private System.Windows.Forms.CheckBox ConfigFieldsize_CheckBox;
        private System.Windows.Forms.Button ShowResult_Button;
        private System.Windows.Forms.TextBox TopLeft_X;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TopLeft_Y;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox TopRight_Y;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TopRight_X;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox BottomLeft_Y;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox BottomLeft_X;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox BottomRight_Y;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox BottomRight_X;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.ComboBox ZoomFactor_ComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.Button LoadConfig_Button;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox Rotation_ComboBox;
        private System.Windows.Forms.CheckBox DisplaysOrigin_CheckBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox PreciewClickLocation;
        private System.Windows.Forms.Panel ResolutionCalibration_Panel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox ResolutionHeight_Box;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ResolutionWidth_Box;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button ChangeResolution_Button;
    }
}