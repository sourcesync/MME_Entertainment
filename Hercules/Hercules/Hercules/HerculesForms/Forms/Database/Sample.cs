using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace MME.Hercules.Forms.Database
{
    public partial class Sample : Form
    {
        private Session currentSession;

        public Sample(Session currentSession)
        {
            this.currentSession = currentSession;

            InitializeComponent();  
        }

        private void Sample_Load(object sender, EventArgs e)
        {

            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;


            WindowUtility.SetScreen(pb, "resort.jpg");
            choice1.Parent = pb;
            choice2.Parent = pb;


            this.Refresh();

            //SoundUtility.Play("welcomedisney2.wav");

            SoundUtility.Play("resort.wav");

            choice1.Name = "resort_yes";
            choice2.Name = "resort_no";





        }

        private void choice1_Click(object sender, EventArgs e)
        {

            PictureBox pic = (PictureBox)sender;


            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);

            Thread.Sleep(800);

            if (choice1.Name == "resort_yes")
            {
                WindowUtility.SetScreen(pb, "gender.jpg");
                SoundUtility.Play("gender.wav");

                choice1.Name = "gender_male";
                choice2.Name = "gender_female";
            }
            else
            {
                this.Refresh();

                DialogResult = System.Windows.Forms.DialogResult.OK;

            }


        }
    }
}
