namespace MME.Hercules.Forms.User
{
    partial class Keyboard
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
            this.kb = new MME.Hercules.Keyboards.Keyboard2();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.finished = new System.Windows.Forms.Button();
            this.pb = new System.Windows.Forms.PictureBox();
            this.alertbox = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.alertbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // kb
            // 
            this.kb.BackColor = System.Drawing.Color.Transparent;
            this.kb.CurrentTextBox = null;
            this.kb.Location = new System.Drawing.Point(99, 371);
            this.kb.Name = "kb";
            this.kb.Size = new System.Drawing.Size(808, 252);
            this.kb.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(192, 287);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(582, 45);
            this.textBox1.TabIndex = 1;
            // 
            // finished
            // 
            this.finished.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.finished.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.finished.Font = new System.Drawing.Font("Script MT Bold", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finished.ForeColor = System.Drawing.Color.White;
            this.finished.Location = new System.Drawing.Point(673, 635);
            this.finished.Name = "finished";
            this.finished.Size = new System.Drawing.Size(222, 66);
            this.finished.TabIndex = 5;
            this.finished.TabStop = false;
            this.finished.Text = "Finished";
            this.finished.UseVisualStyleBackColor = false;
            this.finished.Click += new System.EventHandler(this.finished_Click);
            // 
            // pb
            // 
            this.pb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb.Location = new System.Drawing.Point(0, 0);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(1008, 730);
            this.pb.TabIndex = 6;
            this.pb.TabStop = false;
            // 
            // alertbox
            // 
            this.alertbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.alertbox.Controls.Add(this.button1);
            this.alertbox.Controls.Add(this.label2);
            this.alertbox.Controls.Add(this.label1);
            this.alertbox.Location = new System.Drawing.Point(152, 248);
            this.alertbox.Name = "alertbox";
            this.alertbox.Size = new System.Drawing.Size(713, 198);
            this.alertbox.TabIndex = 7;
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
            // Keyboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.ControlBox = false;
            this.Controls.Add(this.alertbox);
            this.Controls.Add(this.kb);
            this.Controls.Add(this.finished);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Keyboard";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Keyboard";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Keyboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.alertbox.ResumeLayout(false);
            this.alertbox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Keyboards.Keyboard2 kb;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button finished;
        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.Panel alertbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}