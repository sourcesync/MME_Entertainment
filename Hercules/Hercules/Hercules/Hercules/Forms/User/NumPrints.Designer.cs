namespace MME.Hercules.Forms.User
{
    partial class NumPrints
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
            this.b1 = new System.Windows.Forms.PictureBox();
            this.pb = new System.Windows.Forms.PictureBox();
            this.timeoutlabel = new System.Windows.Forms.Label();
            this.b2 = new System.Windows.Forms.PictureBox();
            this.b3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.b1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.b2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.b3)).BeginInit();
            this.SuspendLayout();
            // 
            // b1
            // 
            this.b1.BackColor = System.Drawing.Color.Transparent;
            this.b1.Image = global::MME.Hercules.Properties.Resources.chooseprint1;
            this.b1.Location = new System.Drawing.Point(186, 271);
            this.b1.Name = "b1";
            this.b1.Padding = new System.Windows.Forms.Padding(10);
            this.b1.Size = new System.Drawing.Size(163, 165);
            this.b1.TabIndex = 3;
            this.b1.TabStop = false;
            this.b1.Click += new System.EventHandler(this.b1_Click);
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(0, 0);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(1018, 740);
            this.pb.TabIndex = 4;
            this.pb.TabStop = false;
            // 
            // timeoutlabel
            // 
            this.timeoutlabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.timeoutlabel.AutoSize = true;
            this.timeoutlabel.BackColor = System.Drawing.Color.Transparent;
            this.timeoutlabel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeoutlabel.ForeColor = System.Drawing.Color.White;
            this.timeoutlabel.Location = new System.Drawing.Point(314, 689);
            this.timeoutlabel.Name = "timeoutlabel";
            this.timeoutlabel.Size = new System.Drawing.Size(391, 24);
            this.timeoutlabel.TabIndex = 5;
            this.timeoutlabel.Text = "Please make your choice in 30 seconds";
            // 
            // b2
            // 
            this.b2.BackColor = System.Drawing.Color.Transparent;
            this.b2.Image = global::MME.Hercules.Properties.Resources.chooseprint2;
            this.b2.Location = new System.Drawing.Point(400, 271);
            this.b2.Name = "b2";
            this.b2.Padding = new System.Windows.Forms.Padding(10);
            this.b2.Size = new System.Drawing.Size(163, 165);
            this.b2.TabIndex = 6;
            this.b2.TabStop = false;
            this.b2.Click += new System.EventHandler(this.b2_Click);
            // 
            // b3
            // 
            this.b3.BackColor = System.Drawing.Color.Transparent;
            this.b3.Image = global::MME.Hercules.Properties.Resources.chooseprint3;
            this.b3.Location = new System.Drawing.Point(612, 271);
            this.b3.Name = "b3";
            this.b3.Padding = new System.Windows.Forms.Padding(10);
            this.b3.Size = new System.Drawing.Size(163, 165);
            this.b3.TabIndex = 7;
            this.b3.TabStop = false;
            this.b3.Click += new System.EventHandler(this.b3_Click);
            // 
            // NumPrints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1018, 740);
            this.ControlBox = false;
            this.Controls.Add(this.b3);
            this.Controls.Add(this.b2);
            this.Controls.Add(this.timeoutlabel);
            this.Controls.Add(this.b1);
            this.Controls.Add(this.pb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NumPrints";
            this.ShowIcon = false;
            this.Text = "NumPrints";
            this.Load += new System.EventHandler(this.NumPrints_Load);
            ((System.ComponentModel.ISupportInitialize)(this.b1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.b2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.b3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox b1;
        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.Label timeoutlabel;
        private System.Windows.Forms.PictureBox b2;
        private System.Windows.Forms.PictureBox b3;
    }
}