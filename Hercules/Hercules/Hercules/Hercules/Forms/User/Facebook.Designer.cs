namespace MME.Hercules.Forms.User
{
    partial class Facebook
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
            this.finished = new System.Windows.Forms.Button();
            this.skip = new System.Windows.Forms.Button();
            this.email = new System.Windows.Forms.TextBox();
            this.alertbox = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pass = new System.Windows.Forms.TextBox();
            this.notice = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.usepanel = new System.Windows.Forms.Panel();
            this.labelQuestion = new System.Windows.Forms.Label();
            this.fbno = new System.Windows.Forms.PictureBox();
            this.fbyes = new System.Windows.Forms.PictureBox();
            this.pb2 = new System.Windows.Forms.PictureBox();
            this.keyboard = new MME.Hercules.Keyboards.Keyboard2();
            this.pictureBoxBack = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.alertbox.SuspendLayout();
            this.usepanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fbno)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fbyes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBack)).BeginInit();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb.Location = new System.Drawing.Point(0, 0);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(1024, 768);
            this.pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;
            // 
            // finished
            // 
            this.finished.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.finished.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.finished.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finished.ForeColor = System.Drawing.Color.White;
            this.finished.Location = new System.Drawing.Point(541, 657);
            this.finished.Name = "finished";
            this.finished.Size = new System.Drawing.Size(216, 75);
            this.finished.TabIndex = 8;
            this.finished.TabStop = false;
            this.finished.Text = "Finished";
            this.finished.UseVisualStyleBackColor = false;
            this.finished.Click += new System.EventHandler(this.finished_Click);
            // 
            // skip
            // 
            this.skip.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.skip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.skip.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skip.ForeColor = System.Drawing.Color.White;
            this.skip.Location = new System.Drawing.Point(771, 657);
            this.skip.Name = "skip";
            this.skip.Size = new System.Drawing.Size(139, 75);
            this.skip.TabIndex = 7;
            this.skip.TabStop = false;
            this.skip.Text = "Skip";
            this.skip.UseVisualStyleBackColor = false;
            this.skip.Click += new System.EventHandler(this.skip_Click);
            // 
            // email
            // 
            this.email.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email.Location = new System.Drawing.Point(26, 671);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(46, 47);
            this.email.TabIndex = 5;
            this.email.TabStop = false;
            this.email.Visible = false;
            this.email.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // alertbox
            // 
            this.alertbox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.alertbox.Controls.Add(this.button1);
            this.alertbox.Controls.Add(this.label3);
            this.alertbox.Controls.Add(this.label1);
            this.alertbox.Location = new System.Drawing.Point(114, 139);
            this.alertbox.Name = "alertbox";
            this.alertbox.Size = new System.Drawing.Size(799, 251);
            this.alertbox.TabIndex = 10;
            this.alertbox.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(288, 165);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(222, 53);
            this.button1.TabIndex = 6;
            this.button1.TabStop = false;
            this.button1.Text = "Continue";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(19, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(706, 32);
            this.label3.TabIndex = 1;
            this.label3.Text = "Invalid email or password.   Please try again or Skip.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(19, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(672, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Facebook reported a problem trying to log you in.";
            // 
            // pass
            // 
            this.pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pass.Location = new System.Drawing.Point(78, 671);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(46, 47);
            this.pass.TabIndex = 11;
            this.pass.TabStop = false;
            this.pass.Visible = false;
            this.pass.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // notice
            // 
            this.notice.AutoSize = true;
            this.notice.BackColor = System.Drawing.Color.Transparent;
            this.notice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notice.ForeColor = System.Drawing.Color.White;
            this.notice.Location = new System.Drawing.Point(28, 680);
            this.notice.Name = "notice";
            this.notice.Size = new System.Drawing.Size(448, 15);
            this.notice.TabIndex = 12;
            this.notice.Text = "Facebook passwords are sent directly to Facebook and are never saved or stored.";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(114, 139);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(796, 250);
            this.webBrowser1.TabIndex = 14;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser1_Navigating);
            // 
            // usepanel
            // 
            this.usepanel.Controls.Add(this.pictureBoxBack);
            this.usepanel.Controls.Add(this.labelQuestion);
            this.usepanel.Controls.Add(this.fbno);
            this.usepanel.Controls.Add(this.fbyes);
            this.usepanel.Controls.Add(this.pb2);
            this.usepanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usepanel.Location = new System.Drawing.Point(0, 0);
            this.usepanel.Name = "usepanel";
            this.usepanel.Size = new System.Drawing.Size(1024, 768);
            this.usepanel.TabIndex = 15;
            // 
            // labelQuestion
            // 
            this.labelQuestion.AutoSize = true;
            this.labelQuestion.BackColor = System.Drawing.Color.Transparent;
            this.labelQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuestion.Location = new System.Drawing.Point(108, 583);
            this.labelQuestion.Name = "labelQuestion";
            this.labelQuestion.Size = new System.Drawing.Size(796, 39);
            this.labelQuestion.TabIndex = 3;
            this.labelQuestion.Text = "Would You Like To Post Your Image To FaceBook?";
            this.labelQuestion.Visible = false;
            // 
            // fbno
            // 
            this.fbno.Location = new System.Drawing.Point(529, 269);
            this.fbno.Name = "fbno";
            this.fbno.Padding = new System.Windows.Forms.Padding(10);
            this.fbno.Size = new System.Drawing.Size(220, 270);
            this.fbno.TabIndex = 2;
            this.fbno.TabStop = false;
            this.fbno.Click += new System.EventHandler(this.fbno_Click);
            // 
            // fbyes
            // 
            this.fbyes.Location = new System.Drawing.Point(258, 269);
            this.fbyes.Name = "fbyes";
            this.fbyes.Padding = new System.Windows.Forms.Padding(10);
            this.fbyes.Size = new System.Drawing.Size(220, 270);
            this.fbyes.TabIndex = 1;
            this.fbyes.TabStop = false;
            this.fbyes.Click += new System.EventHandler(this.fbyes_Click);
            // 
            // pb2
            // 
            this.pb2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb2.Location = new System.Drawing.Point(0, 0);
            this.pb2.Name = "pb2";
            this.pb2.Size = new System.Drawing.Size(1024, 768);
            this.pb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb2.TabIndex = 0;
            this.pb2.TabStop = false;
            // 
            // keyboard
            // 
            this.keyboard.BackColor = System.Drawing.Color.Transparent;
            this.keyboard.CurrentTextBox = null;
            this.keyboard.Location = new System.Drawing.Point(111, 401);
            this.keyboard.Name = "keyboard";
            this.keyboard.Size = new System.Drawing.Size(808, 252);
            this.keyboard.TabIndex = 13;
            // 
            // pictureBoxBack
            // 
            this.pictureBoxBack.Location = new System.Drawing.Point(4, 719);
            this.pictureBoxBack.Name = "pictureBoxBack";
            this.pictureBoxBack.Size = new System.Drawing.Size(51, 44);
            this.pictureBoxBack.TabIndex = 4;
            this.pictureBoxBack.TabStop = false;
            this.pictureBoxBack.Visible = false;
            this.pictureBoxBack.Click += new System.EventHandler(this.pictureBoxBack_Click);
            // 
            // Facebook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.usepanel);
            this.Controls.Add(this.alertbox);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.keyboard);
            this.Controls.Add(this.notice);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.finished);
            this.Controls.Add(this.skip);
            this.Controls.Add(this.email);
            this.Controls.Add(this.pb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Facebook";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Facebook";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Facebook_FormClosing);
            this.Load += new System.EventHandler(this.Facebook_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.alertbox.ResumeLayout(false);
            this.alertbox.PerformLayout();
            this.usepanel.ResumeLayout(false);
            this.usepanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fbno)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fbyes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.Button finished;
        private System.Windows.Forms.Button skip;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.Panel alertbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Label notice;
        private Keyboards.Keyboard2 keyboard;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Panel usepanel;
        private System.Windows.Forms.PictureBox pb2;
        private System.Windows.Forms.PictureBox fbno;
        private System.Windows.Forms.PictureBox fbyes;
        private System.Windows.Forms.Label labelQuestion;
        private System.Windows.Forms.PictureBox pictureBoxBack;
    }
}