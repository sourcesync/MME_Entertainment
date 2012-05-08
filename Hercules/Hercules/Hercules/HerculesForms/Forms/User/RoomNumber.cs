using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MME.Hercules.Forms.User
{
    public partial class RoomNumber : Form
    {
        Session currentSession;

        public RoomNumber(Session currentSession)
        {
            this.currentSession = currentSession;

            InitializeComponent();
        }

        private void RoomNumber_Load(object sender, EventArgs e)
        {
            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;
            
            WindowUtility.SetScreen(pb, Hercules.Properties.Resources.ROOM_NUMBER_SCREEN);

            this.Refresh();


            SoundUtility.Play(Hercules.Properties.SoundResources.ROOM_NUMBER);

            keyboard.Parent = pb;
            keyboard.CurrentTextBox = textBox1;

            label1.Parent = pb;
            label2.Parent = pb;

            label1.ForeColor = Color.Black;
            label2.ForeColor = Color.Black;



        }

        private void finished_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                alertbox.Visible = true;
                return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            alertbox.Visible = false;

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            keyboard.CurrentTextBox = textBox2;

            if (keyboard.CurrentLayout != KeyboardLayout.ABC)
                keyboard.SetKeyboard(KeyboardLayout.ABC);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            keyboard.CurrentTextBox = textBox1;

         
            if (keyboard.CurrentLayout != KeyboardLayout.Numeric)
                keyboard.SetKeyboard(KeyboardLayout.Numeric);
        }
    }
}
