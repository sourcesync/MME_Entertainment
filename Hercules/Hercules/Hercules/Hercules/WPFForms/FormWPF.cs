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
        object current = null;

        int orientation=0;

        HerculesWPFBlank.UserControlBlank ctlblank = null;
        ElementHost blankhost = null;
        HerculesWPFMain.UserControlMain ctlmain = null;
        ElementHost mainhost = null;
        HerculesWPFMenu.UserControlMenu ctlmenu = null;
        ElementHost menuhost = null;
        HerculesWPFChoose.UserControlChoose ctlchoose = null;
        ElementHost choosehost = null;

        System.Collections.ArrayList ctls = new System.Collections.ArrayList();

        public FormWPF()
        {
            InitializeComponent();
            
            
            this.ctlblank = new HerculesWPFBlank.UserControlBlank();
            this.ctlblank.evt = new HerculesWPFBlank.UserControlBlank.UserControlBlankDelegate(this.blank_selected);
            this.ctlblank.Visibility = System.Windows.Visibility.Visible;
            this.blankhost = new ElementHost();
            this.blankhost.Size = new Size(1024, 768);
            this.blankhost.Location = new Point(0, 0);
            this.blankhost.Child = this.ctlblank;
            this.Controls.Add(this.blankhost);

            this.ctlmain = new HerculesWPFMain.UserControlMain();
            this.ctlmain.evt = new HerculesWPFMain.UserControlMain.UserControlMainDelegate(this.main_selected);
            this.ctlmain.Visibility = System.Windows.Visibility.Visible;
            this.mainhost = new ElementHost();
            this.mainhost.Size = new Size(1024, 768);
            this.mainhost.Location = new Point(0, 0);
            this.mainhost.Child = this.ctlmain;
            this.Controls.Add(this.mainhost);

            this.ctlmenu = new HerculesWPFMenu.UserControlMenu();
            this.ctlmenu.evt = new HerculesWPFMenu.UserControlMenu.UserControlMenuDelegate(this.menu_selected);
            this.ctlmenu.Visibility = System.Windows.Visibility.Visible;
            this.menuhost = new ElementHost();
            this.menuhost.Size = new Size(1024, 768);
            this.menuhost.Location = new Point(0, 0);
            this.menuhost.Child = this.ctlmenu;
            this.Controls.Add(this.menuhost);

            this.ctlchoose = new HerculesWPFChoose.UserControlChoose();
            this.ctlchoose.evt = new HerculesWPFChoose.UserControlChoose.UserControlChooseDelegate(this.choose_selected);
            this.ctlchoose.Visibility = System.Windows.Visibility.Visible;
            this.choosehost = new ElementHost();
            this.choosehost.Size = new Size(1024, 768);
            this.choosehost.Location = new Point(0, 0);
            this.choosehost.Child = this.ctlchoose;
            this.Controls.Add(this.choosehost);
        }


        private void HideAll()
        {
            if (this.ctlblank != null)
            {
                //this.ctlblank.Visibility = System.Windows.Visibility.Hidden;
                this.blankhost.Visible = false;
            }
            if (this.ctlmain != null)
            {
                //this.ctlmain.Visibility = System.Windows.Visibility.Hidden;
                this.mainhost.Visible = false;
            }
            if (this.ctlmenu != null)
            {
                //this.ctlmain.Visibility = System.Windows.Visibility.Hidden;
                this.menuhost.Visible = false;
            }
            if (this.ctlchoose != null)
            {
                //this.ctlmain.Visibility = System.Windows.Visibility.Hidden;
                this.choosehost.Visible = false;
            }
            //this.ctlphotobooth.Stop();
        }

        private void HideRotators()
        {
            //this.image1.Visibility = System.Windows.Visibility.Hidden;
            //this.image2.Visibility = System.Windows.Visibility.Hidden;
            this.pictureBox1.Visible = false;
            this.pictureBox2.Visible = false;
        }

        private void ShowRotators()
        {
            //this.image1.Visibility = System.Windows.Visibility.Visible;
            //this.image2.Visibility = System.Windows.Visibility.Visible;
            this.pictureBox2.Visible = true;
            this.pictureBox2.BringToFront();
            this.pictureBox1.Visible = true;
            this.pictureBox1.BringToFront();
        }

        public void ShowMain()
        {
            this.HideAll();
            if (this.ctlmain != null)
            {
                //this.ctlmain.Visibility = System.Windows.Visibility.Visible;
                this.mainhost.Visible = true;
                this.mainhost.BringToFront();
            }
            
            this.current = this.ctlmain;
            this.ShowRotators();
        }

        public void ShowMenu()
        {
            this.HideAll();
            if (this.menuhost != null)
            {
                //this.ctlmenu.Visibility = System.Windows.Visibility.Visible;
                this.menuhost.Visible = true;
                this.menuhost.BringToFront();
                
                this.current = this.ctlmenu;
                this.ShowRotators();
            }
        }

        public void ShowChoose(int option)
        {
            this.HideAll();
            if (this.ctlchoose != null)
            {
                this.ctlchoose.SetOption(option);
                //this.ctlchoose.Visibility = System.Windows.Visibility.Visible;
                this.choosehost.Visible = true;
                this.choosehost.BringToFront();
               
                this.current = this.ctlchoose;
                this.ShowRotators();
            }
        }

        public void ShowPhotobooth()
        {
            //this.HideAll();
            //this.ctlphotobooth.Visibility = System.Windows.Visibility.Visible;
            //this.current = this.ctlphotobooth;
            //this.ShowRotators();
            //this.ctlphotobooth.Start();
            this.DialogResult = DialogResult.OK;
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
            this.current = this.ctlblank;
            
            this.HideRotators();
            if (this.ctlblank != null)
            {
                this.blankhost.Visible = true;
                this.blankhost.BringToFront();
            }

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

        private void Rotate(int i)
        {
            this.ctlmain.SetRotation(i);
            this.ctlmenu.SetRotation(i);
            this.ctlchoose.SetRotation(i);
        }

        
        private void toggle(object sender)
        {
            if (sender == this.pictureBox2 )
            {
                if (this.orientation == 0)
                {
                    if ((false) && (this.current == this.ctlchoose))
                    {
                        this.ShowMenu();
                    }
                    else if (this.current != this.ctlmain)
                    {
                        this.ShowMain();
                    }
                    else
                    {
                        this.ShowBlank();
                    }
                }
                else
                {
                    this.orientation = 0;
                    /*RotateTransform tr = new RotateTransform();
                    tr.CenterX = this.canvas_master.Width / 2.0;
                    tr.CenterY = this.canvas_master.Height / 2.0;
                    tr.Angle = 0;
                    this.canvas_master.RenderTransform = tr;
                     * */
                    this.Rotate(this.orientation);

                    if (this.current == this.ctlblank)
                    {
                        this.ShowMain();
                    }
                }
            }
            else if (sender == this.pictureBox1)
            {
                if (this.orientation == 0)
                {
                    this.orientation = 1;
                   
                    this.Rotate(this.orientation);
                    /*RotateTransform tr = new RotateTransform();
                    tr.CenterX = this.canvas_master.Width / 2.0;
                    tr.CenterY = this.canvas_master.Height / 2.0;
                    tr.Angle = 180;
                    this.canvas_master.RenderTransform = tr;
                    */

                    if (this.current == this.ctlblank)
                    {
                        this.ShowMain();
                    }
                }
                else
                {
                    if ((false) && (this.current == this.ctlchoose))
                    {
                        this.ShowMenu();
                    }
                    else if (this.current != this.ctlmain)
                    {
                        this.ShowMain();
                    }
                    else
                    {
                        this.ShowBlank();
                    }
                }
            }
        }
         

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.toggle(sender);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.toggle(sender);
        }


    }
}
