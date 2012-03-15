namespace MME.Hercules.Forms.Operator
{
    partial class Camera
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
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.imageQualities = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.imageSizes = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.meteringModes = new System.Windows.Forms.ComboBox();
            this.AFDistance = new System.Windows.Forms.ComboBox();
            this.exposureModes = new System.Windows.Forms.ComboBox();
            this.photoEffects = new System.Windows.Forms.ComboBox();
            this.brightness = new System.Windows.Forms.TrackBar();
            this.zoom = new System.Windows.Forms.TrackBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.brightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(233, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(519, 42);
            this.label1.TabIndex = 2;
            this.label1.Text = "CAMERA CONFIGURATION";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Teal;
            this.label9.Location = new System.Drawing.Point(506, 324);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(124, 24);
            this.label9.TabIndex = 50;
            this.label9.Text = "Image Quality";
            // 
            // imageQualities
            // 
            this.imageQualities.FormattingEnabled = true;
            this.imageQualities.Location = new System.Drawing.Point(693, 329);
            this.imageQualities.Name = "imageQualities";
            this.imageQualities.Size = new System.Drawing.Size(121, 21);
            this.imageQualities.TabIndex = 49;
            this.imageQualities.SelectedIndexChanged += new System.EventHandler(this.imageQualities_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Teal;
            this.label8.Location = new System.Drawing.Point(507, 286);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 24);
            this.label8.TabIndex = 48;
            this.label8.Text = "Image Size";
            // 
            // imageSizes
            // 
            this.imageSizes.FormattingEnabled = true;
            this.imageSizes.Location = new System.Drawing.Point(693, 289);
            this.imageSizes.Name = "imageSizes";
            this.imageSizes.Size = new System.Drawing.Size(121, 21);
            this.imageSizes.TabIndex = 47;
            this.imageSizes.SelectedIndexChanged += new System.EventHandler(this.imageSizes_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Teal;
            this.label6.Location = new System.Drawing.Point(124, 406);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 24);
            this.label6.TabIndex = 41;
            this.label6.Text = "Brightness";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Teal;
            this.label5.Location = new System.Drawing.Point(514, 406);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 24);
            this.label5.TabIndex = 40;
            this.label5.Text = "Zoom";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Teal;
            this.label4.Location = new System.Drawing.Point(507, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 24);
            this.label4.TabIndex = 39;
            this.label4.Text = "Metering Mode";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Teal;
            this.label3.Location = new System.Drawing.Point(506, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 24);
            this.label3.TabIndex = 38;
            this.label3.Text = "AF Distance";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Teal;
            this.label2.Location = new System.Drawing.Point(506, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 24);
            this.label2.TabIndex = 37;
            this.label2.Text = "Exposure Modes";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Teal;
            this.label10.Location = new System.Drawing.Point(507, 131);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 24);
            this.label10.TabIndex = 36;
            this.label10.Text = "Photo Effects";
            // 
            // meteringModes
            // 
            this.meteringModes.FormattingEnabled = true;
            this.meteringModes.Location = new System.Drawing.Point(693, 249);
            this.meteringModes.Name = "meteringModes";
            this.meteringModes.Size = new System.Drawing.Size(121, 21);
            this.meteringModes.TabIndex = 35;
            this.meteringModes.SelectedIndexChanged += new System.EventHandler(this.meteringModes_SelectedIndexChanged);
            // 
            // AFDistance
            // 
            this.AFDistance.FormattingEnabled = true;
            this.AFDistance.Location = new System.Drawing.Point(693, 209);
            this.AFDistance.Name = "AFDistance";
            this.AFDistance.Size = new System.Drawing.Size(121, 21);
            this.AFDistance.TabIndex = 34;
            this.AFDistance.SelectedIndexChanged += new System.EventHandler(this.AFDistance_SelectedIndexChanged);
            // 
            // exposureModes
            // 
            this.exposureModes.FormattingEnabled = true;
            this.exposureModes.Location = new System.Drawing.Point(693, 170);
            this.exposureModes.Name = "exposureModes";
            this.exposureModes.Size = new System.Drawing.Size(121, 21);
            this.exposureModes.TabIndex = 33;
            this.exposureModes.SelectedIndexChanged += new System.EventHandler(this.exposureModes_SelectedIndexChanged);
            // 
            // photoEffects
            // 
            this.photoEffects.FormattingEnabled = true;
            this.photoEffects.Location = new System.Drawing.Point(693, 131);
            this.photoEffects.Name = "photoEffects";
            this.photoEffects.Size = new System.Drawing.Size(121, 21);
            this.photoEffects.TabIndex = 32;
            this.photoEffects.SelectedIndexChanged += new System.EventHandler(this.photoEffects_SelectedIndexChanged);
            // 
            // brightness
            // 
            this.brightness.LargeChange = 1;
            this.brightness.Location = new System.Drawing.Point(216, 406);
            this.brightness.Name = "brightness";
            this.brightness.Size = new System.Drawing.Size(267, 45);
            this.brightness.TabIndex = 31;
            this.brightness.Scroll += new System.EventHandler(this.brightness_Scroll);
            // 
            // zoom
            // 
            this.zoom.LargeChange = 1;
            this.zoom.Location = new System.Drawing.Point(580, 406);
            this.zoom.Name = "zoom";
            this.zoom.Size = new System.Drawing.Size(267, 45);
            this.zoom.TabIndex = 30;
            this.zoom.Scroll += new System.EventHandler(this.OnChangeZoom);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(152, 131);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(286, 217);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(134, 599);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(174, 43);
            this.button1.TabIndex = 51;
            this.button1.Text = "EXIT CONFIG";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(134, 541);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(174, 46);
            this.button2.TabIndex = 52;
            this.button2.Text = "TAKE TEST PICTURE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(134, 480);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(174, 46);
            this.button3.TabIndex = 53;
            this.button3.Text = "SAVE SETTINGS";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(511, 480);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(174, 46);
            this.button4.TabIndex = 54;
            this.button4.Text = "Debug";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Camera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.imageQualities);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.imageSizes);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.meteringModes);
            this.Controls.Add(this.AFDistance);
            this.Controls.Add(this.exposureModes);
            this.Controls.Add(this.photoEffects);
            this.Controls.Add(this.brightness);
            this.Controls.Add(this.zoom);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Camera";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Camera";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.Load += new System.EventHandler(this.Camera_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Camera_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.brightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox imageQualities;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox imageSizes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox meteringModes;
        private System.Windows.Forms.ComboBox AFDistance;
        private System.Windows.Forms.ComboBox exposureModes;
        private System.Windows.Forms.ComboBox photoEffects;
        private System.Windows.Forms.TrackBar brightness;
        private System.Windows.Forms.TrackBar zoom;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}