namespace MME.Hercules.Forms.User
{
    partial class Email
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.skip = new System.Windows.Forms.Button();
            this.finished = new System.Windows.Forms.Button();
            this.alertbox = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.skipArea = new System.Windows.Forms.PictureBox();
            this.pictureBoxBack = new System.Windows.Forms.PictureBox();
            this.keyboard = new MME.Hercules.Keyboards.Keyboard2();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.alertbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skipArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBack)).BeginInit();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb.Location = new System.Drawing.Point(0, 0);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(1024, 768);
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;
            this.pb.Click += new System.EventHandler(this.pb_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(195, 236);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(634, 47);
            this.textBox1.TabIndex = 1;
            this.textBox1.TabStop = false;
            // 
            // skip
            // 
            this.skip.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.skip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.skip.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skip.ForeColor = System.Drawing.Color.White;
            this.skip.Location = new System.Drawing.Point(545, 645);
            this.skip.Name = "skip";
            this.skip.Size = new System.Drawing.Size(145, 66);
            this.skip.TabIndex = 3;
            this.skip.TabStop = false;
            this.skip.Text = "Skip";
            this.skip.UseVisualStyleBackColor = false;
            this.skip.Click += new System.EventHandler(this.skip_Click);
            // 
            // finished
            // 
            this.finished.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.finished.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.finished.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finished.ForeColor = System.Drawing.Color.White;
            this.finished.Location = new System.Drawing.Point(696, 645);
            this.finished.Name = "finished";
            this.finished.Size = new System.Drawing.Size(222, 66);
            this.finished.TabIndex = 4;
            this.finished.TabStop = false;
            this.finished.Text = "Finished";
            this.finished.UseVisualStyleBackColor = false;
            this.finished.Click += new System.EventHandler(this.finished_Click);
            // 
            // alertbox
            // 
            this.alertbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.alertbox.Controls.Add(this.button1);
            this.alertbox.Controls.Add(this.label2);
            this.alertbox.Controls.Add(this.label1);
            this.alertbox.Location = new System.Drawing.Point(171, 236);
            this.alertbox.Name = "alertbox";
            this.alertbox.Size = new System.Drawing.Size(713, 198);
            this.alertbox.TabIndex = 5;
            this.alertbox.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(242, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(222, 53);
            this.button1.TabIndex = 6;
            this.button1.TabStop = false;
            this.button1.Text = "Continue";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(501, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please try again or contact an attendant.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(651, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "In order to proceed, a valid email address is required.";
            // 
            // skipArea
            // 
            this.skipArea.BackColor = System.Drawing.Color.Black;
            this.skipArea.Location = new System.Drawing.Point(882, 30);
            this.skipArea.Name = "skipArea";
            this.skipArea.Size = new System.Drawing.Size(91, 69);
            this.skipArea.TabIndex = 7;
            this.skipArea.TabStop = false;
            this.skipArea.Click += new System.EventHandler(this.skip_Click);
            // 
            // pictureBoxBack
            // 
            this.pictureBoxBack.Location = new System.Drawing.Point(4, 708);
            this.pictureBoxBack.Name = "pictureBoxBack";
            this.pictureBoxBack.Size = new System.Drawing.Size(119, 57);
            this.pictureBoxBack.TabIndex = 9;
            this.pictureBoxBack.TabStop = false;
            this.pictureBoxBack.Visible = false;
            this.pictureBoxBack.Click += new System.EventHandler(this.pictureBoxBack_Click);
            // 
            // keyboard
            // 
            this.keyboard.BackColor = System.Drawing.Color.Transparent;
            this.keyboard.CurrentTextBox = null;
            this.keyboard.Location = new System.Drawing.Point(117, 387);
            this.keyboard.Name = "keyboard";
            this.keyboard.Size = new System.Drawing.Size(811, 252);
            this.keyboard.TabIndex = 8;
            // 
            // Email
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBoxBack);
            this.Controls.Add(this.keyboard);
            this.Controls.Add(this.skipArea);
            this.Controls.Add(this.alertbox);
            this.Controls.Add(this.finished);
            this.Controls.Add(this.skip);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Email";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Email_FormClosed);
            this.Load += new System.EventHandler(this.Email_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Email_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.alertbox.ResumeLayout(false);
            this.alertbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skipArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button skip;
        private System.Windows.Forms.Button finished;
        private System.Windows.Forms.Panel alertbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox skipArea;
        private Keyboards.Keyboard2 keyboard;
        private System.Windows.Forms.PictureBox pictureBoxBack;
    }
}