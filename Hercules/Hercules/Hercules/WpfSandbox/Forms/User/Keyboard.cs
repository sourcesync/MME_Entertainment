using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

namespace MME.Hercules.Forms.User
{
    public partial class Keyboard : Form
    {
        private Session currentSession;
        private XmlNode pageNode;

        public Keyboard(XmlNode pageNode, Session currentSession)
        {
            Application.DoEvents();

            this.pageNode = pageNode;
            this.currentSession = currentSession;

            InitializeComponent();
        }

        private void Keyboard_Load(object sender, EventArgs e)
        {
            //gw if (ConfigUtility.IsDeveloperMode)
                //this.WindowState = FormWindowState.Normal;

            //gw WindowUtility.SetScreen(pb, pageNode.Attributes["screen"].Value);

            if (this.currentSession.Responses == null)
                this.currentSession.Responses = new List<string>();

            kb.Parent = pb;
            kb.CurrentTextBox = textBox1;


            if (!string.IsNullOrEmpty(pageNode.Attributes["sound"].Value))
            {
                //gw SoundUtility.Play(pageNode.Attributes["sound"].Value);
            }


        }

        private void finished_Click(object sender, EventArgs e)
        {
            // if required, then force them
            if (textBox1.Text.Trim().Length == 0 && pageNode.Attributes["required"] != null && pageNode.Attributes["required"].Value.ToLower().Equals("true"))
            {
                alertbox.Visible = true;
                textBox1.Focus();

                return;
            }

            // play sound
            //gw SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);

            // set the response for use later
            this.currentSession.Responses.Add(textBox1.Text);

            Thread.Sleep(700);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            alertbox.Visible = false;
        }
    }
}
