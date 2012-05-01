using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfSandbox
{
    /// <summary>
    /// Interaction logic for WindowWhiteCastle.xaml
    /// </summary>
    public partial class WindowWhiteCastle : Window
    {
        private System.Collections.ArrayList ctls = new System.Collections.ArrayList();
        protected int orientation = 0;
        protected UserControl current = null;
        public System.Collections.ArrayList cart = new System.Collections.ArrayList();

        public WindowWhiteCastle()
        {
            InitializeComponent();

            ctls.Add(this.ctlchoose);
            ctls.Add(this.ctlmain);
            ctls.Add(this.ctlmenu);
            ctls.Add(this.ctlblank);

            this.ShowBlank();
        }

        public static WindowWhiteCastle getParent(UserControl ctl)
        {
            Canvas parent = (Canvas)ctl.Parent;
            Canvas w = (Canvas)parent.Parent;
            Viewbox p = (Viewbox)w.Parent;
            WindowWhiteCastle a = (WindowWhiteCastle)p.Parent;
            return a;
        }

        private void HideAll()
        {
            foreach (UserControl ctl in ctls)
            {
                ctl.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void HideRotators()
        {
            this.image1.Visibility = System.Windows.Visibility.Hidden;
            this.image2.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ShowRotators()
        {
            this.image1.Visibility = System.Windows.Visibility.Visible;
            this.image2.Visibility = System.Windows.Visibility.Visible;
        }

        public void ShowMain()
        {
            this.HideAll();
            this.ctlmain.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlmain;
            this.ShowRotators();
        }

        public void ShowMenu()
        {
            this.HideAll();
            this.ctlmenu.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlmenu;
            this.ShowRotators();
        }

        public void ShowChoose(int option)
        {
            this.HideAll();
            this.ctlchoose.SetOption(option);
            this.ctlchoose.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlchoose;
            this.ShowRotators();
        }

        public void ShowBlank()
        {
            this.HideAll();
            this.ctlblank.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlblank;
            this.ShowRotators();
        }

        private void image1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ( sender == this.image1 )
            {
                if (this.orientation == 0)
                {
                    if ((false)&&(this.current == this.ctlchoose))
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
                    RotateTransform tr = new RotateTransform();
                    tr.CenterX = this.canvas_master.Width / 2.0;
                    tr.CenterY = this.canvas_master.Height / 2.0;
                    tr.Angle = 0;
                    this.canvas_master.RenderTransform = tr;

                    if (this.current == this.ctlblank)
                    {
                        this.ShowMain();
                    }
                }
            }
            else if (sender == this.image2)
            {
                if (this.orientation == 0)
                {
                    this.orientation = 1;
                    RotateTransform tr = new RotateTransform();
                    tr.CenterX = this.canvas_master.Width / 2.0;
                    tr.CenterY = this.canvas_master.Height / 2.0;
                    tr.Angle = 180;
                    this.canvas_master.RenderTransform = tr;

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


    }
}
