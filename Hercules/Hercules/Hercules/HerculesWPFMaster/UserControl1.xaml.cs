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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace HerculesWPFMaster
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private System.Collections.ArrayList ctls = new System.Collections.ArrayList();
        private object current;
        private int orientation = 0;
        
        public delegate void UserControlMasterDelegate(int option);
        public UserControlMasterDelegate evt = null;

        public UserControl1()
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
            ctls.Add(this.ctlevents);
            //ctls.Add(this.webBrowser1);

            this.webBrowser1.Loaded +=new RoutedEventHandler(webBrowser1_Loaded);
        }

        
        public RenderTargetBitmap RenderToFile()
        {
            /*
            int Height = (int)this.copycanvas.ActualHeight;
            int Width = (int)this.copycanvas.ActualWidth;
 
            RenderTargetBitmap bmp = new RenderTargetBitmap(Width, Height, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(this.copycanvas);
 
            string file = "c:\\tmp\\pic.png";
 
            string Extension = System.IO.Path.GetExtension(file).ToLower();
 
            BitmapEncoder encoder;            
            if (Extension == ".gif") 
                encoder = new GifBitmapEncoder();            
            else if (Extension == ".png")
                encoder = new PngBitmapEncoder();            
            else if (Extension == ".jpg")
                encoder = new JpegBitmapEncoder();            
            else
                return null;
 
            encoder.Frames.Add(BitmapFrame.Create(bmp));
 
            using (Stream stm = File.Create(file))
            {
                encoder.Save(stm);
            }
            return bmp;
             * */
            return null;
        }

        void webBrowser1_Loaded(object sender, RoutedEventArgs e)
        {
            //this.webBrowser1.Navigate(new Uri("http://www.whitecastle.com", UriKind.RelativeOrAbsolute));
        }


        public void main_selected(int option)
        {
            if (option == 0) // menu
            {
                this.ShowMenu();
            }
            else if (option == 1) // photobooth
            {
                //this.ShowPhotobooth();
            }
            else if (option == 2) //web...
            {
                this.ShowWeb();
            }
            else if (option == 3) // events...
            {
                this.ShowEvents();
            }
            else if (option == 4) //promo...
            {
                //this.ShowPromo();
            }
            else if (option == 5) // checkin
            {
                //this.ShowCheckin();
            }

            if (this.evt != null) this.evt(option);
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

        private void HideAll()
        {
            this.ctlmenu.Visibility = System.Windows.Visibility.Hidden;
            this.ctlmain.Visibility = System.Windows.Visibility.Hidden;
            this.ctlblank.Visibility = System.Windows.Visibility.Hidden;
            this.ctlchoose.Visibility = System.Windows.Visibility.Hidden;
            this.ctlevents.Visibility = System.Windows.Visibility.Hidden;

            FrameworkElement el = this.webBrowser1 as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 2000.0);


            //this.ctlphotobooth.Stop();
        }

        private void HideRotators()
        {
            FrameworkElement el = this.image1 as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 2000.0);
            el = this.image2 as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 2000.0);
            //this.image1.Visibility = System.Windows.Visibility.Hidden;
            //this.image2.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ShowRotators()
        {
            FrameworkElement el = this.image1 as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 994.0);
            el = this.image2 as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 30.0);
            //this.image1.Visibility = System.Windows.Visibility.Visible;
            
            //this.image2.Visibility = System.Windows.Visibility.Visible;
        }

        private void ShowBack()
        {
            FrameworkElement el = this.imageback as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 4.0);
        }

        private void HideBack()
        {
            FrameworkElement el = this.imageback as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 2000.0);
        }

        public void ShowMain()
        {
            this.HideAll();
            this.ctlmain.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlmain;
            this.ShowRotators();
            this.ShowBack();

            if (this.evt != null) this.evt(-1);
        }

        public void ShowMenu()
        {
            this.HideAll();
            this.ctlmenu.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlmenu;
            this.ShowRotators();
            this.ShowBack();
        }

        public void ShowEvents()
        {
            this.HideAll();
            this.ctlevents.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlevents;
            this.ShowRotators();
            this.ShowBack();
        }

        public void ShowWeb()
        {
            this.HideAll();
            
            /*
            FrameworkElement el = this.webBrowser1 as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 0.0);
            el.SetValue(Canvas.TopProperty, 0.0);
            el.Width = 1024;
            el.Height = 768;
            this.current = this.webBrowser1;
            this.ShowRotators();
            
            this.webBrowser1.Navigate(new Uri("http://www.whitecastle.com", UriKind.RelativeOrAbsolute));
            */

            
            if (this.webBrowser1 != null)
            {
                this.webBrowser1.Dispose();
                this.webBrowser1 = null;
            }
            this.webBrowser1 = new WebBrowser();
            this.webBrowser1.Height = 768;
            this.webBrowser1.Width = 1024;
            this.canvas_master.Children.Add(this.webBrowser1);
            FrameworkElement el = this.webBrowser1 as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 0.0);
            el.SetValue(Canvas.TopProperty, 0.0);
            this.webBrowser1.BringIntoView();
            this.current = this.webBrowser1;
            this.webBrowser1.Navigate(new Uri("http://www.whitecastle.com", UriKind.RelativeOrAbsolute));

            this.ShowBack();
        }

        public void ShowChoose(int option)
        {
            this.HideAll();
            this.ctlchoose.SetOption(option);
            this.ctlchoose.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlchoose;
            this.ShowRotators();
            this.HideBack();
        }

        public void ShowBlank()
        {
            this.HideAll();
            this.ctlblank.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlblank;
            this.HideRotators();
            this.HideBack();


            if (this.evt != null) this.evt(-2);
        }

        private void image1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender == this.image1)
            {
                if (this.orientation == 0)
                {
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
                }
            }
        }

        private void imageback_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.current != this.ctlmain)
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
