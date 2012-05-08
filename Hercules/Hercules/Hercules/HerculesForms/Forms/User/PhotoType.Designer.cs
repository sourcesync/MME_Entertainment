namespace MME.Hercules.Forms.User
{
    partial class PhotoType
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
            this.pb = new System.Windows.Forms.PictureBox();
            this.timeoutlabel = new System.Windows.Forms.Label();
            this.bwpb = new System.Windows.Forms.PictureBox();
            this.colorpb = new System.Windows.Forms.PictureBox();
            this.sb = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bwpb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorpb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb)).BeginInit();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb.Location = new System.Drawing.Point(0, 0);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(1018, 740);
            this.pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb.TabIndex = 0;
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
            this.timeoutlabel.TabIndex = 1;
            this.timeoutlabel.Text = "Please make your choice in 30 seconds";
            // 
            // bwpb
            // 
            this.bwpb.Location = new System.Drawing.Point(256, 271);
            this.bwpb.Name = "bwpb";
            this.bwpb.Padding = new System.Windows.Forms.Padding(10);
            this.bwpb.Size = new System.Drawing.Size(220, 270);
            this.bwpb.TabIndex = 2;
            this.bwpb.TabStop = false;
            this.bwpb.Click += new System.EventHandler(this.bwpb_Click);
            this.bwpb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.colortypepb_MouseDown);
            // 
            // colorpb
            // 
            this.colorpb.Location = new System.Drawing.Point(530, 271);
            this.colorpb.Name = "colorpb";
            this.colorpb.Padding = new System.Windows.Forms.Padding(10);
            this.colorpb.Size = new System.Drawing.Size(220, 270);
            this.colorpb.TabIndex = 3;
            this.colorpb.TabStop = false;
            this.colorpb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.colortypepb_MouseDown);
            // 
            // sb
            // 
            this.sb.Location = new System.Drawing.Point(801, 271);
            this.sb.Name = "sb";
            this.sb.Padding = new System.Windows.Forms.Padding(10);
            this.sb.Size = new System.Drawing.Size(220, 270);
            this.sb.TabIndex = 4;
            this.sb.TabStop = false;
            this.sb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.colortypepb_MouseDown);
            // 
            // PhotoType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1018, 740);
            this.ControlBox = false;
            this.Controls.Add(this.sb);
            this.Controls.Add(this.colorpb);
            this.Controls.Add(this.bwpb);
            this.Controls.Add(this.timeoutlabel);
            this.Controls.Add(this.pb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "PhotoType";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Photo Type Selection";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PhotoType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bwpb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorpb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.Label timeoutlabel;
        private System.Windows.Forms.PictureBox bwpb;
        private System.Windows.Forms.PictureBox colorpb;
        private System.Windows.Forms.PictureBox sb;

        //gw
        public ColorType color_choice = ColorType.BW_Color;
        //gw
    }
}