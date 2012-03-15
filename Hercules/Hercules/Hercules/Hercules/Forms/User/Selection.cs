using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Threading;

namespace MME.Hercules.Forms.User
{
    public partial class Selection : Form
    {
        private Session currentSession;
        private XmlNode pageNode;

        public Selection(XmlNode pagenode, Session currentSession)
        {
            Application.DoEvents();

            this.pageNode = pagenode;
            this.currentSession = currentSession;

            InitializeComponent();
        }

        private void Selection_Load(object sender, EventArgs e)
        {
            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;

            WindowUtility.SetScreen(pb, pageNode.Attributes["screen"].Value);

            if (this.currentSession.Responses == null)
                this.currentSession.Responses = new List<string>();

            foreach (XmlNode node in pageNode.ChildNodes)
            {
                if (!string.IsNullOrEmpty(node.Attributes["color"].Value))
                {
                    Panel panel = new Panel();
                    panel.Location = new Point(Convert.ToInt32(node.Attributes["x"].Value), Convert.ToInt32(node.Attributes["y"].Value));
                    panel.Width = Convert.ToInt32(node.Attributes["width"].Value);
                    panel.Height = Convert.ToInt32(node.Attributes["height"].Value);
                    panel.BackColor = ColorTranslator.FromHtml(node.Attributes["color"].Value);                    
                    pb.Controls.Add(panel);
                    panel.BringToFront();
                }
            }

            if (!string.IsNullOrEmpty(pageNode.Attributes["sound"].Value))
            {
                SoundUtility.Play(pageNode.Attributes["sound"].Value);
            }
        }

       
        private void pb_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (XmlNode node in pageNode.ChildNodes)
            {
                if (
                    e.X >= Convert.ToInt32(node.Attributes["x"].Value) &&
                    e.X <= Convert.ToInt32(node.Attributes["x"].Value) + Convert.ToInt32(node.Attributes["width"].Value) &&
                    e.Y >= Convert.ToInt32(node.Attributes["y"].Value) &&
                    e.Y <= Convert.ToInt32(node.Attributes["y"].Value) + Convert.ToInt32(node.Attributes["height"].Value)
                   )
                {
                    // flicker box

                    // play sound
                    SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);

                    // set the response for use later
                    this.currentSession.Responses.Add(node.Attributes["value"].Value);                   
                                         
                    Thread.Sleep(700);

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }
    }
}
