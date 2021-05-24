
namespace RockyHockeyGUI
{
    partial class GameFieldDetectionView
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
            this.RectanglePicBox = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DrawRectangleButton = new System.Windows.Forms.Button();
            this.StartGameButton = new System.Windows.Forms.Button();
            this.ResetCoordinatesButton = new System.Windows.Forms.Button();
            this.AbortButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RectanglePicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // RectanglePicBox
            // 
            this.RectanglePicBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RectanglePicBox.Location = new System.Drawing.Point(0, 0);
            this.RectanglePicBox.Name = "RectanglePicBox";
            this.RectanglePicBox.Size = new System.Drawing.Size(777, 341);
            this.RectanglePicBox.TabIndex = 0;
            this.RectanglePicBox.TabStop = false;
            this.RectanglePicBox.Click += new System.EventHandler(this.RectanglePicBox_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 360);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(777, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 344);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(488, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Gekennzeichnete Koordinaten (von links oben nach rechts oben nach recht unsten na" +
    "ch links unten):";
            // 
            // DrawRectangleButton
            // 
            this.DrawRectangleButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DrawRectangleButton.Location = new System.Drawing.Point(0, 386);
            this.DrawRectangleButton.Name = "DrawRectangleButton";
            this.DrawRectangleButton.Size = new System.Drawing.Size(777, 30);
            this.DrawRectangleButton.TabIndex = 3;
            this.DrawRectangleButton.Text = "Draw Rectangle";
            this.DrawRectangleButton.UseVisualStyleBackColor = true;
            this.DrawRectangleButton.Click += new System.EventHandler(this.DrawRectangleButton_Click);
            // 
            // StartGameButton
            // 
            this.StartGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StartGameButton.Location = new System.Drawing.Point(0, 422);
            this.StartGameButton.Name = "StartGameButton";
            this.StartGameButton.Size = new System.Drawing.Size(777, 30);
            this.StartGameButton.TabIndex = 4;
            this.StartGameButton.Text = "Start Game";
            this.StartGameButton.UseVisualStyleBackColor = true;
            this.StartGameButton.Click += new System.EventHandler(this.StartGameButton_Click);
            // 
            // ResetCoordinatesButton
            // 
            this.ResetCoordinatesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetCoordinatesButton.Location = new System.Drawing.Point(0, 458);
            this.ResetCoordinatesButton.Name = "ResetCoordinatesButton";
            this.ResetCoordinatesButton.Size = new System.Drawing.Size(777, 30);
            this.ResetCoordinatesButton.TabIndex = 5;
            this.ResetCoordinatesButton.Text = "Reset Coordinates";
            this.ResetCoordinatesButton.UseVisualStyleBackColor = true;
            this.ResetCoordinatesButton.Click += new System.EventHandler(this.ResetCoordinatesButton_Click);
            // 
            // AbortButton
            // 
            this.AbortButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AbortButton.Location = new System.Drawing.Point(0, 494);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(777, 30);
            this.AbortButton.TabIndex = 6;
            this.AbortButton.Text = "Abort";
            this.AbortButton.UseVisualStyleBackColor = true;
            // 
            // GameFieldDetectionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 602);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.ResetCoordinatesButton);
            this.Controls.Add(this.StartGameButton);
            this.Controls.Add(this.DrawRectangleButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.RectanglePicBox);
            this.Name = "GameFieldDetectionView";
            this.Text = "GameFieldDetection";
            ((System.ComponentModel.ISupportInitialize)(this.RectanglePicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox RectanglePicBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DrawRectangleButton;
        private System.Windows.Forms.Button StartGameButton;
        private System.Windows.Forms.Button ResetCoordinatesButton;
        private System.Windows.Forms.Button AbortButton;
    }
}