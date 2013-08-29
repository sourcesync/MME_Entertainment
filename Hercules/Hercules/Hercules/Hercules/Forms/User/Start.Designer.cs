namespace MME.Hercules.Forms.User
{
    partial class Start
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
            this.startArea = new System.Windows.Forms.PictureBox();
            this.sb2 = new System.Windows.Forms.PictureBox();
            this.sb1 = new System.Windows.Forms.PictureBox();
            this.pb2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.Dock = System.Windows.Forms.DockStyle.Top;
            this.pb.Location = new System.Drawing.Point(0, 0);
            this.pb.Margin = new System.Windows.Forms.Padding(0);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(1024, 768);
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;
            this.pb.Click += new System.EventHandler(this.pb_Click);
            // 
            // startArea
            // 
            this.startArea.BackColor = System.Drawing.Color.Transparent;
            this.startArea.Location = new System.Drawing.Point(768, 581);
            this.startArea.Name = "startArea";
            this.startArea.Size = new System.Drawing.Size(117, 89);
            this.startArea.TabIndex = 2;
            this.startArea.TabStop = false;
            this.startArea.Click += new System.EventHandler(this.startArea_Click);
            // 
            // sb2
            // 
            this.sb2.BackColor = System.Drawing.Color.Transparent;
            this.sb2.Location = new System.Drawing.Point(415, 581);
            this.sb2.Name = "sb2";
            this.sb2.Size = new System.Drawing.Size(117, 89);
            this.sb2.TabIndex = 3;
            this.sb2.TabStop = false;
            this.sb2.Visible = false;
            // 
            // sb1
            // 
            this.sb1.BackColor = System.Drawing.Color.Transparent;
            this.sb1.Location = new System.Drawing.Point(101, 581);
            this.sb1.Name = "sb1";
            this.sb1.Size = new System.Drawing.Size(117, 89);
            this.sb1.TabIndex = 4;
            this.sb1.TabStop = false;
            this.sb1.Visible = false;
            // 
            // pb2
            // 
            this.pb2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pb2.Location = new System.Drawing.Point(0, 768);
            this.pb2.Margin = new System.Windows.Forms.Padding(0);
            this.pb2.Name = "pb2";
            this.pb2.Size = new System.Drawing.Size(1024, 768);
            this.pb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb2.TabIndex = 5;
            this.pb2.TabStop = false;
            this.pb2.Visible = false;
            this.pb2.Click += new System.EventHandler(this.pb2_Click_1);
            this.pb2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb2_MouseDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Red;
            this.pictureBox1.Location = new System.Drawing.Point(996, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 25);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pb2);
            this.Controls.Add(this.sb1);
            this.Controls.Add(this.sb2);
            this.Controls.Add(this.startArea);
            this.Controls.Add(this.pb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Start";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Hercules";
            this.Text = "Hercules";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Start_FormClosed);
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Start_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.StartForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.PictureBox startArea;
        private System.Windows.Forms.PictureBox sb2;
        private System.Windows.Forms.PictureBox sb1;
        private System.Windows.Forms.PictureBox pb2;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}