using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace MME.Hercules.WPFForms
{
    public partial class FormWPF : Form
    {

        HerculesWPFBlank.UserControlBlank ctlblank = null;
        HerculesWPFChoose.UserControlChoose ctlchoose = null;
        HerculesWPFMain.UserControlMain ctlmain = null;
        HerculesWPFMenu.UserControlMenu ctlmenu = null;

        System.Collections.ArrayList ctls = new System.Collections.ArrayList();

        public FormWPF()
        {
            InitializeComponent();
            
            
            this.ctlblank = new HerculesWPFBlank.UserControlBlank();
            this.ctlblank.evt = new HerculesWPFBlank.UserControlBlank.UserControlBlankDelegate(this.blank_selected);
            this.ctlblank.Visibility = System.Windows.Visibility.Visible;
            ElementHost elhost = new ElementHost();
            elhost.Size = new Size(1024, 768);
            elhost.Location = new Point(0, 0);
            elhost.Child = this.ctlblank;
            this.Controls.Add(elhost);
            

            this.ctlmain = new HerculesWPFMain.UserControlMain();
            this.ctlmain.evt = new HerculesWPFMain.UserControlMain.UserControlMainDelegate(this.main_selected);
            this.ctlmain.Visibility = System.Windows.Visibility.Visible;
            ElementHost elhost2 = new ElementHost();
            elhost2.Size = new Size(1024, 768);
            elhost2.Location = new Point(0, 0);
            elhost2.Child = this.ctlmain;
            this.Controls.Add(elhost2);
        }


        private void HideAll()
        {
            if (this.ctlblank!=null) this.ctlblank.Visibility = System.Windows.Visibility.Hidden;
            if (this.ctlmain!=null) this.ctlmain.Visibility = System.Windows.Visibility.Hidden;
            //this.ctlphotobooth.Stop();
        }

        private void HideRotators()
        {
            //this.image1.Visibility = System.Windows.Visibility.Hidden;
            //this.image2.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ShowRotators()
        {
            //this.image1.Visibility = System.Windows.Visibility.Visible;
            //this.image2.Visibility = System.Windows.Visibility.Visible;
        }

        public void ShowMain()
        {
            this.HideAll();
            if (this.ctlmain != null)
            {
                this.ctlmain.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.ctlmain = new HerculesWPFMain.UserControlMain();
                this.ctlmain.evt = new HerculesWPFMain.UserControlMain.UserControlMainDelegate(this.main_selected);
                this.ctlmain.Visibility = System.Windows.Visibility.Visible;
                ElementHost elhost2 = new ElementHost();
                elhost2.Size = new Size(1024, 768);
                elhost2.Location = new Point(0, 0);
                elhost2.Child = this.ctlmain;
                this.Controls.Add(elhost2);
            }
            
            //this.current = this.ctlmain;
            //this.ShowRotators();
        }

        public void ShowMenu()
        {
            //this.HideAll();
            //this.ctlmenu.Visibility = System.Windows.Visibility.Visible;
            //this.current = this.ctlmenu;
            //this.ShowRotators();
        }

        public void ShowChoose(int option)
        {
            //this.HideAll();
            //this.ctlchoose.SetOption(option);
            //this.ctlchoose.Visibility = System.Windows.Visibility.Visible;
            //this.current = this.ctlchoose;
            //this.ShowRotators();
        }

        public void ShowPhotobooth()
        {
            //this.HideAll();
            //this.ctlphotobooth.Visibility = System.Windows.Visibility.Visible;
            //this.current = this.ctlphotobooth;
            //this.ShowRotators();
            //this.ctlphotobooth.Start();
        }

        /*
        public void ShowSwipe()
        {
            this.HideAll();
            this.ctlswipe.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlswipe;
        }
         * */

        public void ShowBlank()
        {
            this.HideAll();
            if (this.ctlblank!=null) this.ctlblank.Visibility = System.Windows.Visibility.Visible;
            //this.current = this.ctlblank;
            //this.HideRotators();
        }

        public void main_selected(int option)
        {
            if (option == 0)
            {
                this.ShowMenu();
            }
            else if (option == 1)
            {
                this.ShowPhotobooth();
            }
        }


        public void choose_selected(int option)
        {
            this.ShowMenu();
        }

        public void blank_selected(int option)
        {
            this.ShowMain();
        }


        public void menu_selected(int option)
        {
            if (option < 0)
            {
                this.ShowMain();
            }
            else
            {
                this.ShowChoose(option);
            }
        }

        private void FormWPF_Load(object sender, EventArgs e)
        {
            this.ShowBlank();
        }


    }
}
