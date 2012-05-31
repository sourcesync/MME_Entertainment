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
using MME.HerculesConfig;
using System.Runtime.InteropServices;
using System.Reflection;

namespace HerculesWPFMaster
{

    public class NativeMethods
    {
        // PInvoke declaration for EnumDisplaySettings Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern int EnumDisplaySettings(
            string lpszDeviceName,
            int iModeNum,
            ref DEVMODE lpDevMode);

        // PInvoke declaration for ChangeDisplaySettings Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern int ChangeDisplaySettings(
            ref DEVMODE lpDevMode,
            int dwFlags);

        // add more functions as needed …

        // constants
        public const int ENUM_CURRENT_SETTINGS = -1;
        public const int DMDO_DEFAULT = 0;
        public const int DMDO_90 = 1;
        public const int DMDO_180 = 2;
        public const int DMDO_270 = 3;
        // add more constants as needed …

        //gw
        public const int DISP_CHANGE_SUCCESSFUL = 0;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct DEVMODE
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmDeviceName;

        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public int dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmFormName;

        public short dmLogPixels;
        public short dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    };


    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private System.Collections.ArrayList ctls = new System.Collections.ArrayList();
        private object current;
        public int orientation = 0;
        
        public delegate void UserControlMasterDelegate(String option);
        public UserControlMasterDelegate evt = null;
        private bool silenced = false;

        //public WebBrowser webBrowser1 = null;

        public UserControl1()
        {
            InitializeComponent();

            //  user control delegates...
            this.ctlmain.evt += new HerculesWPFMain.UserControlMain.UserControlMainDelegate(this.main_selected);
            this.ctlblank.evt += new HerculesWPFBlank.UserControlBlank.UserControlBlankDelegate(this.blank_selected);
            this.ctlmenu.evt += new HerculesWPFMenu.UserControlMenu.UserControlMenuDelegate(this.menu_selected);
            this.ctlchoose.evt += new HerculesWPFChoose.UserControlChoose.UserControlChooseDelegate(this.choose_selected);
            this.ctlgamemenu.tevt += new HerculesWPFGameMenu.UserControl1.UserControlGameMainToggle(this.game_selected);

            //  user control array..
            ctls.Add(this.ctlchoose);
            ctls.Add(this.ctlmain);
            ctls.Add(this.ctlmenu);
            ctls.Add(this.ctlblank);
            ctls.Add(this.ctlevents);
            ctls.Add(this.ctlmemorygame);
            ctls.Add(this.ctldraw);
            ctls.Add(this.ctlgamemenu);
            ctls.Add(this.ctlttt);

            this.webBrowser1.Loaded +=new RoutedEventHandler(webBrowser1_Loaded);

            /*
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "TEXT_COLOR")))
            {

            }
             * */

            try
            {
                BitmapSource src = WindowUtility.GetScreenBitmapWPF("calendar.jpg");
                this.ctlevents.Source = src;
            }
            catch (System.Exception E)
            {
                System.Windows.MessageBox.Show(E.ToString());
            }
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
            if (!silenced)
            {
                this.HideScriptErrors(this.webBrowser1, true);
                silenced = true;
            }
            //this.webBrowser1.Navigate(new Uri("http://www.whitecastle.com", UriKind.RelativeOrAbsolute));
        }


        public void main_selected(String option)
        {
            if (this.webBrowser1 != null)
            {
                this.webBrowser1.Dispose();
                this.webBrowser1 = null;
                this.silenced = false;
            }

            if ((option == "menu")||(option=="drinks")) // menu
            {
                this.ShowMenu();
            }
            else if (option == "hidden")
            {
                this.ShowHidden();
            }
            else if (option == "social")
            {
                this.ShowSocial();
            }
            else if (option == "photobooth") // photobooth
            {
                //this.ShowPhotobooth();
            }
            else if (option == "camera") // photobooth
            {
                //this.ShowPhotobooth();
            }
            else if (option == "web") //web...
            {
                this.ShowWebMain();
            }
            else if (option == "about") //web...
            {
                this.ShowAbout();
            }
            else if (option == "daybreak site") //web...
            {
                this.ShowDBS();
            }
            else if (option == "jackboxer site") //web...
            {
                this.ShowJBS();
            }
            else if (option == "events") // events...
            {
                this.ShowEvents();
            }
            else if (option == "calendar") // calendar...
            {
                this.ShowCalendar();
            }
            else if (option == "promo") //promo...
            {
                //this.ShowPromo();
            }
            else if (option == "concierge") //promo...
            {
                this.ShowConcierge();
            }
            else if (option == "check-in") // checkin
            {
                //this.ShowCheckin();
            }
            else if (option == "games") // games...
            {
                this.ShowGameMenu();
            }

            if (this.evt != null) this.evt(option);
        }


        public void game_selected(int option)
        {
            if (option == 0)
            {
                this.ShowMemoryGame();
            }
            else if (option == 1)
            {
                this.ShowDraw();
            }
            else if (option == 2)
            {
                this.ShowTTT();
            }
        }

        public void choose_selected(int option)
        {
            if (this.ctlchoose.mode == 1) // go to main from choose
            {
                this.ShowMain();
            }
            else // go back up from choose...
            {
                this.ShowMenu();
            }
        }

        public void blank_selected(int option)
        {
            this.ShowMain();
        }


        public void menu_selected(String option)
        {
            if (option == null)
            {
                this.ShowMain();
            }
            else if (option=="server")
            {
                this.ShowServer();
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
            this.ctlmemorygame.Visibility = System.Windows.Visibility.Hidden;
            this.ctldraw.Visibility = System.Windows.Visibility.Hidden;
            this.ctlgamemenu.Visibility = System.Windows.Visibility.Hidden;
            this.ctlttt.Visibility = System.Windows.Visibility.Hidden;

            if (this.webBrowser1 != null)
            {
                FrameworkElement el = this.webBrowser1 as FrameworkElement;
                el.SetValue(Canvas.LeftProperty, 2000.0);
            }

            this.imagepromo.Visibility = System.Windows.Visibility.Hidden;

            this.imagecono.Visibility = System.Windows.Visibility.Hidden;

            this.imagesocial.Visibility = System.Windows.Visibility.Hidden;


            this.imageserver.Visibility = System.Windows.Visibility.Hidden;

            this.addressbar.Visibility = System.Windows.Visibility.Hidden;

            //this.ctlphotobooth.Stop();
        }

        private void HideRotators()
        {
            FrameworkElement el = this.image1 as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 2000.0);
            el = this.image2 as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 2000.0);
        }

        private void ShowRotators()
        {
            FrameworkElement el = this.image1 as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 964.0);
            el = this.image2 as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 60.0);
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

            if (this.evt != null) this.evt("main");
        }

        public void ShowMenu()
        {
            this.HideAll();
            this.ctlmenu.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlmenu;
            this.ShowRotators();
            this.ShowBack();
        }

        public void ShowCalendar()
        {
            this.HideAll();
            this.ctlevents.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlevents;
            this.ShowRotators();
            this.ShowBack();       
        }

        public void ShowGameMenu()
        {
            this.HideAll();
            this.ctlgamemenu.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlgamemenu;
            this.ShowRotators();
            this.ShowBack();
        }

        public void ShowMemoryGame()
        {
            this.HideAll();
            this.ctlmemorygame.Visibility = System.Windows.Visibility.Visible;
            this.ctlmemorygame.Restart();
            this.current = this.ctlmemorygame;
            this.ShowRotators();
            this.ShowBack();
        }

        public void ShowDraw()
        {
            this.HideAll();
            this.ctldraw.Visibility = System.Windows.Visibility.Visible;
            this.ctldraw.Restart();
            this.current = this.ctldraw;
            this.ShowRotators();
            this.ShowBack();
        }

        public void ShowTTT()
        {
            this.HideAll();
            this.ctlttt.Visibility = System.Windows.Visibility.Visible;
            this.ctlttt.Restart();
            this.current = this.ctlttt;
            this.ShowRotators();
            this.ShowBack();
        }

        public void ShowPromo()
        {
            this.HideAll();
            this.imagepromo.Visibility = System.Windows.Visibility.Visible;

            this.current = this.imagepromo;
            this.ShowRotators();
            this.ShowBack();
        }

        public void ShowServer()
        {
            this.HideAll();
            this.imageserver.Visibility = System.Windows.Visibility.Visible;

            this.current = this.imageserver;
            this.ShowRotators();
            this.ShowBack();
        }

        public void ShowSocial()
        {
            this.HideAll();
            this.imagesocial.Visibility = System.Windows.Visibility.Visible;

            this.current = this.imagesocial;
            this.ShowRotators();
            this.ShowBack();
        }

        public void ShowConcierge()
        {
            this.HideAll();
            this.imagecono.Visibility = System.Windows.Visibility.Visible;

            this.current = this.imagecono;
            this.ShowRotators();
            this.ShowBack();
        }

        public void ShowHidden()
        {
            String weburl = "http://dev4.northkingdom.com/daybreak/main-site-4/";

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "HiddenURL")))
            {
                weburl = ConfigUtility.GetConfig(ConfigUtility.Config, "HiddenURL");
            }

            this.ShowWeb(weburl, false);
        }

        public void ShowAbout()
        {
            String weburl = "http://www.whitecastle.com/company";

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "AboutURL")))
            {
                weburl = ConfigUtility.GetConfig(ConfigUtility.Config, "AboutURL");
            }

            this.ShowWeb( weburl, false);
        }

        public void ShowJBS()
        {
            String weburl = "http://findthetruth.jackboxers.com";

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "JBSURL")))
            {
                weburl = ConfigUtility.GetConfig(ConfigUtility.Config, "JBSURL");
            }
           
            this.ShowWeb(weburl, false);
        }

        public void ShowDBS()
        {
            String weburl = "http://dev4.northkingdom.com/daybreak/main-site-4/";

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "DBSURL")))
            {
                weburl = ConfigUtility.GetConfig(ConfigUtility.Config, "DBSURL");
            }

            this.ShowWeb(weburl, false);
        }

        public void ShowEvents()
        {
            String weburl = "http://www.whitecastle.com/company";

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "EventsURL")))
            {
                weburl = ConfigUtility.GetConfig(ConfigUtility.Config, "EventsURL");
            }

            this.ShowWeb(weburl, false);
        }

        public void ShowWebMain()
        {
            String weburl = "http://www.whitecastle.com/company";

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "WebURL")))
            {
                weburl = ConfigUtility.GetConfig(ConfigUtility.Config, "WebURL");
            }

            this.ShowWeb(weburl, true);
        }

        public void ShowWeb(String weburl, bool ab)
        {
            this.HideAll();

            /*
            if (this.webBrowser1 != null)
            {
                this.webBrowser1.Dispose();
                this.webBrowser1 = null;
            }
             * */
            if (this.webBrowser1 == null)
            {
                this.webBrowser1 = new WebBrowser();
                this.webBrowser1.Navigated += new NavigatedEventHandler(webBrowser1_Navigated);
                //this.webBrowser1.Loaded +=new RoutedEventHandler(webBrowser1_Loaded);
            }

            if (ab)
            {
                this.addressbar.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.addressbar.Visibility = System.Windows.Visibility.Hidden;

            }
            this.webBrowser1.Height = 715;
            this.webBrowser1.Width = 1024;
            this.canvas_master.Children.Add(this.webBrowser1);
            FrameworkElement el = this.webBrowser1 as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 0.0);
            if (ab)
            {
                el.SetValue(Canvas.TopProperty, 35.0);
            }
            else
            {
                el.SetValue(Canvas.TopProperty, 0.0);
            }
            this.webBrowser1.BringIntoView();
            this.current = this.webBrowser1;

            //this.HideRotators();

            /*
            String val = ConfigUtility.GetConfig(ConfigUtility.Config, "WebURL");

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "WebURL")))
            {
                String weburl = ConfigUtility.GetConfig(ConfigUtility.Config, "WebURL");
                this.webBrowser1.Navigate(new Uri(weburl, UriKind.RelativeOrAbsolute));
            }
            else
            {
                this.webBrowser1.Navigate(new Uri("http://www.whitecastle.com/company", UriKind.RelativeOrAbsolute));
            }
             
             */
            this.webBrowser1.Navigate(new Uri(weburl, UriKind.RelativeOrAbsolute));

            this.ShowBack();
        }

        public void HideScriptErrors(WebBrowser wb, bool Hide)
        {
            try
            {
                FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fiComWebBrowser == null) return;
                object objComWebBrowser = fiComWebBrowser.GetValue(wb);
                if (objComWebBrowser == null) return;
                objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { Hide });
            }
            catch (System.Exception ex)
            {
            }
        }

        void webBrowser1_Navigated(object sender, NavigationEventArgs e)
        {
            if (!silenced)
            {
                this.HideScriptErrors(this.webBrowser1, true);
                silenced = true;
            }

            this.addressbar.Text = e.Uri.ToString();
        }

        void KeyDown(object sender, KeyEventArgs args)
        {
            //if (Keyboard.IsKeyDown(Key.Return))
            if ( args.Key == Key.Return )
            {
                if (this.webBrowser1 != null)
                {
                    this.webBrowser1.Navigate(this.addressbar.Text);
                }
            }
        }

        public void ShowChoose(String option)
        {
            this.HideAll();
            if (this.ctlchoose.mode == 1) // purchased
                this.ctlchoose.Restart();
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


            if (this.evt != null) this.evt("blank");
        }

        private void rotate_via_graphics()
        {
            try
            {

                // initialize the DEVMODE structure

                DEVMODE dm = new DEVMODE();
                dm.dmDeviceName = new string(new char[32]);
                dm.dmFormName = new string(new char[32]);
                dm.dmSize = (short)Marshal.SizeOf(dm);

                if (0 != NativeMethods.EnumDisplaySettings(
                    null,
                    NativeMethods.ENUM_CURRENT_SETTINGS,
                    ref dm))
                {
                    /*
                    System.Windows.Forms.MessageBox.Show(
                        String.Format("res->{0},{1}",
                        dm.dmPelsWidth, dm.dmPelsHeight));
                    */

                    // swap width and height
                    //int temp = dm.dmPelsHeight;
                    //dm.dmPelsHeight = dm.dmPelsWidth;
                    //dm.dmPelsWidth = temp;

                    int new_orientation = 0;


                    // determine new orientation
                    switch (dm.dmDisplayOrientation)
                    {
                        case NativeMethods.DMDO_DEFAULT:
                            dm.dmDisplayOrientation = NativeMethods.DMDO_180;
                            new_orientation = 1;
                            break;
                        case NativeMethods.DMDO_270:
                            dm.dmDisplayOrientation = NativeMethods.DMDO_180;
                            break;
                        case NativeMethods.DMDO_180:
                            dm.dmDisplayOrientation = NativeMethods.DMDO_DEFAULT;
                            new_orientation = 0;
                            break;
                        case NativeMethods.DMDO_90:
                            dm.dmDisplayOrientation = NativeMethods.DMDO_DEFAULT;
                            break;
                        default:
                            // unknown orientation value
                            // add exception handling here
                            break;
                    }

                    bool worked = true;
                    int iRet = NativeMethods.ChangeDisplaySettings(ref dm, 0);
                    if (NativeMethods.DISP_CHANGE_SUCCESSFUL != iRet)
                    {
                        // add exception handling here
                        System.Windows.Forms.MessageBox.Show("cannot change display");
                        worked = false;
                    }

                    if (worked)
                    {
                        this.orientation = new_orientation;
                    }

                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("cannot enum display");
                }

            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }


        private void image1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            rotate_via_graphics();

            return;

            if ((sender == this.image1)||(sender == this.image2 ) )
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
                /*
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
                 * */
        }

        private void imageback_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.current == this.ctldraw )
            {
                this.ShowGameMenu();
            }
            else if ( this.current == this.ctlmemorygame )
            {
                this.ShowGameMenu();
            }
            else if (this.current != this.ctlmain)
            {
                this.ShowMain();
            }
            else
            {
                //this.ShowBlank();
            }
        }
    }
}
