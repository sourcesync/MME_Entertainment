namespace MME.Hercules.Forms.Operator
{
    partial class Chromakey
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.composite = new System.Windows.Forms.PictureBox();
            this.photo = new System.Windows.Forms.PictureBox();
            this.testbg = new System.Windows.Forms.PictureBox();
            this.changephoto = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.hueupdown = new System.Windows.Forms.NumericUpDown();
            this.maxupdown = new System.Windows.Forms.NumericUpDown();
            this.minupdown = new System.Windows.Forms.NumericUpDown();
            this.satupdown = new System.Windows.Forms.NumericUpDown();
            this.toleranceupdown = new System.Windows.Forms.NumericUpDown();
            this.button6 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.composite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.photo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.testbg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hueupdown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxupdown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minupdown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.satupdown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toleranceupdown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(205, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(603, 42);
            this.label1.TabIndex = 1;
            this.label1.Text = "CHROMAKEY CONFIGURATION";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label3.Location = new System.Drawing.Point(207, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 26);
            this.label3.TabIndex = 25;
            this.label3.Text = "Hue";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(152, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 26);
            this.label2.TabIndex = 26;
            this.label2.Text = "Tolerance";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(148, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 26);
            this.label4.TabIndex = 27;
            this.label4.Text = "Saturation";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label5.Location = new System.Drawing.Point(150, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 26);
            this.label5.TabIndex = 28;
            this.label5.Text = "Min Value";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label6.Location = new System.Drawing.Point(144, 258);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 26);
            this.label6.TabIndex = 29;
            this.label6.Text = "Max Value";
            // 
            // composite
            // 
            this.composite.BackColor = System.Drawing.Color.Gray;
            this.composite.Location = new System.Drawing.Point(502, 103);
            this.composite.Name = "composite";
            this.composite.Size = new System.Drawing.Size(426, 583);
            this.composite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.composite.TabIndex = 35;
            this.composite.TabStop = false;
            this.composite.Click += new System.EventHandler(this.composite_Click);
            // 
            // photo
            // 
            this.photo.BackColor = System.Drawing.Color.Gray;
            this.photo.Location = new System.Drawing.Point(139, 323);
            this.photo.Name = "photo";
            this.photo.Size = new System.Drawing.Size(120, 164);
            this.photo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.photo.TabIndex = 36;
            this.photo.TabStop = false;
            // 
            // testbg
            // 
            this.testbg.BackColor = System.Drawing.Color.Gray;
            this.testbg.Location = new System.Drawing.Point(275, 323);
            this.testbg.Name = "testbg";
            this.testbg.Size = new System.Drawing.Size(122, 164);
            this.testbg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.testbg.TabIndex = 37;
            this.testbg.TabStop = false;
            // 
            // changephoto
            // 
            this.changephoto.ForeColor = System.Drawing.Color.Black;
            this.changephoto.Location = new System.Drawing.Point(139, 493);
            this.changephoto.Name = "changephoto";
            this.changephoto.Size = new System.Drawing.Size(120, 41);
            this.changephoto.TabIndex = 38;
            this.changephoto.Text = "Change Test Photo";
            this.changephoto.UseVisualStyleBackColor = true;
            this.changephoto.Click += new System.EventHandler(this.changephoto_Click);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(275, 493);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 41);
            this.button1.TabIndex = 39;
            this.button1.Text = "Change Test Background";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(263, 581);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(169, 41);
            this.button2.TabIndex = 40;
            this.button2.Text = "SAVE SETTINGS";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(78, 628);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(169, 41);
            this.button3.TabIndex = 41;
            this.button3.Text = "RESET";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(78, 581);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(169, 41);
            this.button4.TabIndex = 42;
            this.button4.Text = "SAVE COMPOSITE";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Location = new System.Drawing.Point(263, 628);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(169, 41);
            this.button5.TabIndex = 43;
            this.button5.Text = "EXIT CONFIG";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // hueupdown
            // 
            this.hueupdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hueupdown.Location = new System.Drawing.Point(274, 103);
            this.hueupdown.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.hueupdown.Name = "hueupdown";
            this.hueupdown.Size = new System.Drawing.Size(92, 29);
            this.hueupdown.TabIndex = 44;
            this.hueupdown.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.hueupdown.ValueChanged += new System.EventHandler(this.hue_Scroll);
            // 
            // maxupdown
            // 
            this.maxupdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxupdown.Location = new System.Drawing.Point(274, 265);
            this.maxupdown.Name = "maxupdown";
            this.maxupdown.Size = new System.Drawing.Size(92, 29);
            this.maxupdown.TabIndex = 48;
            this.maxupdown.Value = new decimal(new int[] {
            95,
            0,
            0,
            0});
            this.maxupdown.ValueChanged += new System.EventHandler(this.maximum_Scroll);
            // 
            // minupdown
            // 
            this.minupdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minupdown.Location = new System.Drawing.Point(274, 224);
            this.minupdown.Name = "minupdown";
            this.minupdown.Size = new System.Drawing.Size(92, 29);
            this.minupdown.TabIndex = 47;
            this.minupdown.Value = new decimal(new int[] {
            35,
            0,
            0,
            0});
            this.minupdown.ValueChanged += new System.EventHandler(this.min_Scroll);
            // 
            // satupdown
            // 
            this.satupdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.satupdown.Location = new System.Drawing.Point(274, 183);
            this.satupdown.Name = "satupdown";
            this.satupdown.Size = new System.Drawing.Size(92, 29);
            this.satupdown.TabIndex = 46;
            this.satupdown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.satupdown.ValueChanged += new System.EventHandler(this.saturation_Scroll);
            // 
            // toleranceupdown
            // 
            this.toleranceupdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toleranceupdown.Location = new System.Drawing.Point(274, 144);
            this.toleranceupdown.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.toleranceupdown.Name = "toleranceupdown";
            this.toleranceupdown.Size = new System.Drawing.Size(92, 29);
            this.toleranceupdown.TabIndex = 45;
            this.toleranceupdown.Value = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.toleranceupdown.ValueChanged += new System.EventHandler(this.tolerance_Scroll);
            // 
            // button6
            // 
            this.button6.ForeColor = System.Drawing.Color.Black;
            this.button6.Location = new System.Drawing.Point(139, 540);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(120, 23);
            this.button6.TabIndex = 49;
            this.button6.Text = "Rotate";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Chromakey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.ControlBox = false;
            this.Controls.Add(this.button6);
            this.Controls.Add(this.maxupdown);
            this.Controls.Add(this.minupdown);
            this.Controls.Add(this.satupdown);
            this.Controls.Add(this.toleranceupdown);
            this.Controls.Add(this.hueupdown);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.changephoto);
            this.Controls.Add(this.testbg);
            this.Controls.Add(this.photo);
            this.Controls.Add(this.composite);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Chromakey";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chromakey";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Chromakey_Load);
            ((System.ComponentModel.ISupportInitialize)(this.composite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.photo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.testbg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hueupdown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxupdown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minupdown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.satupdown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toleranceupdown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox composite;
        private System.Windows.Forms.PictureBox photo;
        private System.Windows.Forms.PictureBox testbg;
        private System.Windows.Forms.Button changephoto;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.NumericUpDown hueupdown;
        private System.Windows.Forms.NumericUpDown maxupdown;
        private System.Windows.Forms.NumericUpDown minupdown;
        private System.Windows.Forms.NumericUpDown satupdown;
        private System.Windows.Forms.NumericUpDown toleranceupdown;
        private System.Windows.Forms.Button button6;
    }
}