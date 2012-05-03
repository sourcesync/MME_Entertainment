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
        private int windowstate = 0;

        public WindowWhiteCastle()
        {
            InitializeComponent();

            //  user control delegates...
            this.ctlmain.evt += new HerculesWPFMain.UserControlMain.UserControlMainDelegate(this.main_selected);
            this.ctlblank.evt += new HerculesWPFBlank.UserControlBlank.UserControlBlankDelegate(this.blank_selected);
            this.ctlmenu.evt += new HerculesWPFMenu.UserControlMenu.UserControlMenuDelegate(this.menu_selected);
            this.ctlchoose.evt += new HerculesWPFChoose.UserControlChoose.UserControlChooseDelegate(this.choose_selected);

            //  user control array..
            ctls.Add(this.ctlchoose);
            ctls.Add(this.ctlmain);
            ctls.Add(this.ctlmenu);
            ctls.Add(this.ctlblank);
            //ctls.Add(this.ctlswipe);
            ctls.Add(this.ctlphotobooth);

            this.ShowBlank();
            //this.ShowSwipe();

            this.WindowStyle = WindowStyle.None;
            this.Topmost = true;
            this.WindowState = WindowState.Maximized;
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

            this.ctlphotobooth.Stop();
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

        public void ShowPhotobooth()
        {
            this.HideAll();
            this.ctlphotobooth.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlphotobooth;
            this.ShowRotators();
            this.ctlphotobooth.Start();
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
            this.ctlblank.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlblank;
            this.HideRotators();
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

        

        private void FullScreenButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void FullScreenButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.windowstate == 0)
            {
                this.Topmost = false;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowState = WindowState.Normal;
                this.windowstate = 1;
            }
            else
            {

                this.WindowStyle = WindowStyle.None;
                this.Topmost = true;
                this.WindowState = WindowState.Maximized;
                this.windowstate = 0;
            }
        }

        private void image3_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (this.windowstate == 0)
            {
                this.Topmost = false;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowState = WindowState.Normal;
                this.windowstate = 1;
            }
            else
            {

                this.WindowStyle = WindowStyle.None;
                this.Topmost = true;
                this.WindowState = WindowState.Maximized;
                this.windowstate = 0;
            }
        }


    }
}
