﻿using System;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using System.Drawing;

namespace MME.Hercules.Forms.User
{
    public partial class Email : Form
    {
        private Session currentSession;
        private Thread thread;

        public Email(Session currentSession)
        {
            Application.DoEvents();

            this.currentSession = currentSession;
            InitializeComponent();            
        }

        private void Email_Load(object sender, EventArgs e)
        {
            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;

            WindowUtility.SetScreen(pb, Hercules.Properties.Resources.EMAIL_SCREEN);

            keyboard.Parent = pb;
            keyboard.CurrentTextBox = textBox1;

            skipArea.Parent = pb;
            skip.Visible = (!ConfigUtility.GetConfig(ConfigUtility.Config, "ForceEmail").Equals("1"));

            // set skip button
            skipArea.Location = new Point(Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config,"SkipEmailButtonX")),
                              Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config,"SkipEmailButtonY")));

            skipArea.Width = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "SkipEmailButtonWidth"));
            skipArea.Height = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "SkipEmailButtonHeight"));

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "SkipEmailButtonColor")))
                skipArea.BackColor = ColorTranslator.FromHtml(ConfigUtility.GetConfig(ConfigUtility.Config, "SkipEmailButtonColor"));
            else
                skipArea.BackColor = Color.Transparent;


            thread = new Thread(PlayIntroSounds);
            thread.Start();
        }

        private void PlayIntroSounds()
        {
            Thread.Sleep(800);
            SoundUtility.Play(Hercules.Properties.SoundResources.ENTER_EMAIL);
        }

        private void skip_Click(object sender, EventArgs e)
        {
            SoundUtility.StopSpeaking();
            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);

            Thread.Sleep(1000);
            
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        private void finished_Click(object sender, EventArgs e)
        {
            if (!isValidEmail(this.textBox1.Text))
            {
                alertbox.Visible = true;
                return;
            }

            SoundUtility.StopSpeaking();

            if (thread.ThreadState == ThreadState.Running)
                thread.Abort();

            thread = null;
            
            if (pb.Image != null)
                pb.Image.Dispose();


            this.currentSession.EmailAddress = this.textBox1.Text;
           
            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);
            Thread.Sleep(1000);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

       

        private void Email_FormClosed(object sender, FormClosedEventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            alertbox.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

 

        private void skipArea_Click(object sender, EventArgs e)
        {
            if (thread.ThreadState == ThreadState.Running)
                thread.Abort();

            thread = null;

            if (pb.Image != null)
                pb.Image.Dispose();

            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);
            Thread.Sleep(1000);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

    }
}
