namespace RockyHockeyGUI.VirtualTable
{
    partial class VirtualTableView
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
            this.x0TextBox = new System.Windows.Forms.TextBox();
            this.panel = new System.Windows.Forms.Panel();
            this.y0TextBox = new System.Windows.Forms.TextBox();
            this.x1TextBox = new System.Windows.Forms.TextBox();
            this.y1TextBox = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.goButton = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.stopButton = new System.Windows.Forms.Button();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // x0TextBox
            // 
            this.x0TextBox.Location = new System.Drawing.Point(828, 12);
            this.x0TextBox.Name = "x0TextBox";
            this.x0TextBox.Size = new System.Drawing.Size(100, 20);
            this.x0TextBox.TabIndex = 0;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.White;
            this.panel.Location = new System.Drawing.Point(12, 12);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(785, 437);
            this.panel.TabIndex = 1;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelPaint);
            this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelMouseDown);
            this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelMouseMove);
            this.panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelMouseUp);
            // 
            // y0TextBox
            // 
            this.y0TextBox.Location = new System.Drawing.Point(828, 38);
            this.y0TextBox.Name = "y0TextBox";
            this.y0TextBox.Size = new System.Drawing.Size(100, 20);
            this.y0TextBox.TabIndex = 0;
            // 
            // x1TextBox
            // 
            this.x1TextBox.Location = new System.Drawing.Point(957, 12);
            this.x1TextBox.Name = "x1TextBox";
            this.x1TextBox.Size = new System.Drawing.Size(100, 20);
            this.x1TextBox.TabIndex = 0;
            // 
            // y1TextBox
            // 
            this.y1TextBox.Location = new System.Drawing.Point(957, 38);
            this.y1TextBox.Name = "y1TextBox";
            this.y1TextBox.Size = new System.Drawing.Size(100, 20);
            this.y1TextBox.TabIndex = 0;
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(803, 427);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 0;
            this.textBox5.Text = "250";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(803, 411);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Speed (pixel/sec)";
            // 
            // goButton
            // 
            this.goButton.Enabled = false;
            this.goButton.Location = new System.Drawing.Point(982, 401);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 3;
            this.goButton.Text = "Go!";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.GoButtonClick);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 20;
            this.timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(982, 425);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 3;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButtonClick);
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(806, 329);
            this.trackBar.Maximum = 20;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(254, 45);
            this.trackBar.TabIndex = 4;
            this.trackBar.Value = 3;
            this.trackBar.Scroll += new System.EventHandler(this.TrackBarScroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(809, 360);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "0.0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1033, 360);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "0.02";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(803, 313);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Friction";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(806, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "X0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(933, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "X1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(806, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Y0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(933, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Y1";
            // 
            // VirtualTableView
            // 
            this.AcceptButton = this.goButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1069, 460);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.y0TextBox);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.y1TextBox);
            this.Controls.Add(this.x1TextBox);
            this.Controls.Add(this.x0TextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "VirtualTableView";
            this.Text = "Virtual Table";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox x0TextBox;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.TextBox y0TextBox;
        private System.Windows.Forms.TextBox x1TextBox;
        private System.Windows.Forms.TextBox y1TextBox;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}