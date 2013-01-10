﻿using System;
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
        public bool offline = false;

        public System.Collections.Hashtable webkey = new System.Collections.Hashtable();

        public System.Windows.Threading.DispatcherTimer timer = null;

        public UserControl1()
        {
            try
            {
                InitializeComponent();

                //  user control delegates...
                this.ctlmain.evt += new HerculesWPFMain.UserControlMain.UserControlMainDelegate(this.main_selected);
                this.ctlblank.evt += new HerculesWPFBlank.UserControlBlank.UserControlBlankDelegate(this.blank_selected);
                this.ctlmenu.evt += new HerculesWPFMenu.UserControlMenu.UserControlMenuDelegate(this.menu_selected);
               // this.ctlchoose.evt += new HerculesWPFChoose.UserControlChoose.UserControlChooseDelegate(this.choose_selected);
                this.ctlgamemenu.tevt += new HerculesWPFGameMenu.UserControl1.UserControlGameMainToggle(this.game_selected);
                this.ctldjrequestor.evt += new HerculesWPFDJRequestor.UserControl1.UserControlDJDelegate(this.dj_selected);

                //  user control array..
                //ctls.Add(this.ctlchoose);
                ctls.Add(this.ctlmain);
                ctls.Add(this.ctlmenu);
                ctls.Add(this.ctlblank);
                ctls.Add(this.ctlevents);
                //ctls.Add(this.ctlmemorygame);
                ctls.Add(this.ctldraw);
                ctls.Add(this.ctlgamemenu);
                ctls.Add(this.ctlttt);
                ctls.Add(this.ctlgallery);
                ctls.Add(this.ctlcheckers);

                ctls.Add(this.ctldice);
                ctls.Add(this.ctltrivia);
                ctls.Add(this.ctlchess);
                ctls.Add(this.ctlspinthebottle);
                ctls.Add(this.ctlsendadrink);

                //ctls.Add(this.ctlangrybirds);
                ctls.Add(this.ctldjrequestor);

                this.webBrowser1.Loaded += new RoutedEventHandler(webBrowser1_Loaded);

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

                this.GetWebURLKey();

            }
            catch (System.Exception E)
            {
                System.Windows.MessageBox.Show(E.ToString());
            }

            //this.imagequit.Visibility = System.Windows.Visibility.Hidden;
        }

        public void GetWebURLKey()
        {
            //  override y offset based on config value...
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "WebURLKey")))
            {
                String keystr = ConfigUtility.GetConfig(ConfigUtility.Config, "WebURLKey");

                String[] keyvals = keystr.Split(new char[] { ',' });

                foreach (String kv in keyvals)
                {
                    String[] pair = kv.Split(new char[] { ';' });
                    this.webkey[pair[0]] = pair[1];
                }
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


        public void main_selected(String poption)
        {
            if (this.webBrowser1 != null)
            {
                this.webBrowser1.Navigate(new Uri("about:blank"));
                this.webBrowser1.Dispose();
                this.webBrowser1 = null;
                this.silenced = false;
            }


            String option = poption.ToLower();

            if ((option == "menu")||(option=="drinks")) // menu
            {

               // this.ctlchoose.Restart();
                this.ShowMenu();
            }
            else if (option == "send-a-drink")
            {
                this.ShowSendADrink();
            }
            else if (option == "message-table")
            {
                this.ShowMessage();
            }
            else if (option == "hidden")
            {
                this.ShowHidden();
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
            else if (option == "gallery") // gallery...
            {
                this.ShowGallery();
            }
            else if (option == "request-a-song") // gallery...
            {
                this.ShowRequestASong();
            }
            else //try it as a web url via key dictionary...
            {

                if ( this.webkey[option] != null )
                {
                    String url = (String)this.webkey[option];
                    this.ShowWeb( url, false );
                }
            }

            if (this.evt != null) this.evt(option);
        }

        public void dj_selected(int option)
        {
            if (option == 0)
            {
                this.ctldjrequestor.Stop();
                this.ShowMain();
            }
        }

        public void game_selected(int option)
        {
            if (option == 0)
            {
                this.ShowTTT(); //this.ShowMemoryGame();
            }
            //else if (option == 1)
            //{
            //     this.ShowDraw();
            //}
            else if (option == 2)
            {
                this.ShowCheckers();
            }
           else if (option == 3)
            {
                this.ShowDice(); //this.ShowAngryBirds();
            }
            else if (option == 4)
            {
                this.ShowTrivia(); //this.ShowAngryBirds();
            }
            else if (option == 5)
            {
                this.ShowChess(); //this.ShowAngryBirds();
            }
            else if (option == 6)
            {
                this.ShowSpinTheBottle(); //this.ShowAngryBirds();
            } 
        }

        public void choose_selected(int option)
        {
            if (option == 0)
            {
                this.ShowMenu();
            }
            else if (option == 1)
            {
                this.ShowMain();
            }

                /*
            else if (this.ctlchoose.mode == 0) // go to main from choose...
            {
                this.ShowMain();
            }
            else if (this.ctlchoose.mode ==1 ) // go to menu from checkout...
            {
                this.ShowMenu();
            }
            else // goto menu from purchase...
            {
                this.ShowMenu();
            }
                 * */
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
                //this.ShowChoose(option);
                
            }
        }

        private void HideAll()
        {
            this.ctlmenu.Visibility = System.Windows.Visibility.Hidden;
            this.ctlmain.Visibility = System.Windows.Visibility.Hidden;
            this.ctlblank.Visibility = System.Windows.Visibility.Hidden;
            //this.ctlchoose.Visibility = System.Windows.Visibility.Hidden;
            this.ctlevents.Visibility = System.Windows.Visibility.Hidden;
            //this.ctlmemorygame.Visibility = System.Windows.Visibility.Hidden;
            this.ctldraw.Visibility = System.Windows.Visibility.Hidden;
            this.ctlgamemenu.Visibility = System.Windows.Visibility.Hidden;
            this.ctlttt.Visibility = System.Windows.Visibility.Hidden;
            this.ctlgallery.Visibility = System.Windows.Visibility.Hidden;
            this.ctlgallery.Stop();
            //this.ctlangrybirds.Visibility = System.Windows.Visibility.Hidden;
            //this.ctlangrybirds.Stop();
            this.ctldjrequestor.Visibility = System.Windows.Visibility.Hidden;
            this.ctldjrequestor.Stop();
            this.ctlcheckers.Visibility = System.Windows.Visibility.Hidden;
            this.ctldice.Visibility = System.Windows.Visibility.Hidden;
            this.ctltrivia.Visibility = System.Windows.Visibility.Hidden;
            this.ctlchess.Visibility = System.Windows.Visibility.Hidden;
            this.ctlspinthebottle.Visibility = System.Windows.Visibility.Hidden;
            this.ctlsendadrink.Visibility = System.Windows.Visibility.Hidden;
            this.ctlmessage.Visibility = System.Windows.Visibility.Hidden;

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
            if (this.webBrowser1 != null)
            {
                this.webBrowser1.Navigate(new Uri("about:blank"));
            }

            this.HideAll();
            this.ctlmain.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlmain;
            this.ShowRotators();
            this.HideBack();

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

        public void ShowGallery()
        {
            this.HideAll();
            this.ctlgallery.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctlgallery;
            this.ShowRotators();
            this.ShowBack();
            this.ctlgallery.Start();
        }

        public void ShowRequestASong()
        {
            this.HideAll();
            this.ctldjrequestor.Visibility = System.Windows.Visibility.Visible;
            this.current = this.ctldjrequestor;
            this.ShowRotators();
            this.ShowBack();
            this.ctldjrequestor.Start();

            //this.ctlgallery.Start();
        }

        /*
        public void ShowMemoryGame()
        {
            this.HideAll();
            this.ctlmemorygame.Visibility = System.Windows.Visibility.Visible;
            this.ctlmemorygame.Restart();
            this.current = this.ctlmemorygame;
            this.ShowRotators();
            this.ShowBack();
        }
         * */

        public void ShowCheckers()
        {
            this.HideAll();
            this.ctlcheckers.Visibility = System.Windows.Visibility.Visible;
            //this.ctlcheckers.Restart();
            this.current = this.ctlcheckers;
            this.ShowRotators();
            this.ShowBack();
        }

        public void ShowDice()
        {
            this.HideAll();
            this.ctldice.Visibility = System.Windows.Visibility.Visible;
            //this.ctlcheckers.Restart();
            this.current = this.ctldice;
            this.ShowRotators();
            this.ShowBack();
        }

        public void ShowSendADrink()
        {
            this.HideAll();
            this.ctlsendadrink.Visibility = System.Windows.Visibility.Visible;
            //this.ctlcheckers.Restart();
            this.current = this.ctlsendadrink;
            this.ShowRotators();
            this.ShowBack();
            this.ctlsendadrink.Start();
        }

        public void ShowMessage()
        {
            this.HideAll();
            this.ctlmessage.Visibility = System.Windows.Visibility.Visible;
            //this.ctlcheckers.Restart();
            this.current = this.ctlmessage;
            this.ShowRotators();
            this.ShowBack();
        }

        public void ShowTrivia()
        {
            this.HideAll();
            this.ctltrivia.Visibility = System.Windows.Visibility.Visible;
            //this.ctlcheckers.Restart();
            this.current = this.ctltrivia;
            this.ShowRotators();
            this.ShowBack();
        }

        public void ShowChess()
        {
            this.HideAll();
            this.ctlchess.Visibility = System.Windows.Visibility.Visible;
            //this.ctlcheckers.Restart();
            this.current = this.ctlchess;
            this.ShowRotators();
            this.ShowBack();
        }

        public void ShowSpinTheBottle()
        {
            this.HideAll();
            this.ctlspinthebottle.Visibility = System.Windows.Visibility.Visible;
            //this.ctlcheckers.Restart();
            this.current = this.ctlspinthebottle;
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

        /*
        public void ShowDice()
        {
            this.HideAll();
            this.ctlttt.Visibility = System.Windows.Visibility.Visible;
            this.ctlttt.Restart();
            this.current = this.ctlttt;
            this.ShowRotators();
            this.ShowBack();
        }
         * */

        public void ShowAngryBirds()
        {
            this.HideAll();
            //this.ctlangrybirds.Visibility = System.Windows.Visibility.Visible;
            //this.ctlangrybirds.Restart();
            //this.current = this.ctlangrybirds;
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
            String weburl = "http://www.whitecastle.com";

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "EventsURL")))
            {
                weburl = ConfigUtility.GetConfig(ConfigUtility.Config, "EventsURL");
            }

            this.ShowWeb(weburl, false);
        }

        public void ShowWebMain()
        {
            String weburl = "http://www.whitecastle.com";

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "WebURL")))
            {
                weburl = ConfigUtility.GetConfig(ConfigUtility.Config, "WebURL");
            }

            this.ShowWeb(weburl, true);
        }

        public void ShowWeb(String weburl, bool ab)
        {
            if (weburl.ToLower().StartsWith("http:") && this.offline)
                return;

            
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

            this.webBrowser1.Visibility = System.Windows.Visibility.Hidden;

            if (ab)
            {
                this.addressbar.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.addressbar.Visibility = System.Windows.Visibility.Hidden;

            }
            this.webBrowser1.Height = 646;
            if (!ab) this.webBrowser1.Height = 646+45;

            this.webBrowser1.Width = 1024;

            this.canvas_master.Children.Add(this.webBrowser1);
            FrameworkElement el = this.webBrowser1 as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 0.0);
            if (ab)
            {
                el.SetValue(Canvas.TopProperty, 60.0);
            }
            else
            {
                el.SetValue(Canvas.TopProperty, 0.0);
            }
            //el.SetValue(Canvas.TopProperty, 1000.0);
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
            //this.webBrowser1.LoadCompleted += new LoadCompletedEventHandler(webBrowser1_LoadCompleted);

            this.ShowBack();


            this.webBrowser1.Visibility = System.Windows.Visibility.Visible;
        }

        void webBrowser1_LoadCompleted(object sender, NavigationEventArgs e)
        {
            this.webBrowser1.BringIntoView();
            this.webBrowser1.Visibility = System.Windows.Visibility.Visible;
            FrameworkElement el = this.webBrowser1 as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 0.0);
            el.SetValue(Canvas.TopProperty, 0.0);
            //sthrow new NotImplementedException();

           // this.RestartTimer(3);
        }

        private void RestartTimer(int t)
        {
            if (this.timer != null) this.timer.Stop();
            this.timer = null;
            this.timer = new System.Windows.Threading.DispatcherTimer();
            this.timer.Tick += new EventHandler(this.__timeout);
            this.timer.Interval = new TimeSpan(0, 0, t);
            this.timer.Start();
        }

        public delegate void Callback( int o );
            
        public void _Call( int o )
        {
            this.webBrowser1.BringIntoView();
            this.webBrowser1.Visibility = System.Windows.Visibility.Visible;
            FrameworkElement el = this.webBrowser1 as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, 0.0);
            el.SetValue(Canvas.TopProperty, 0.0);
        }


        //  handler for timer in ui thread...
        void __timeout(object sender, EventArgs e)
        {
            try
            {
                if (this.timer != null) this.timer.Stop();
                this.timer = null;

                this.Dispatcher.Invoke(new Callback(this._Call), new object[] {1});
            }
            catch (System.Exception E)
            {
                System.Windows.MessageBox.Show(E.ToString());
            }
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

        /*
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
         * */

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

        private void imageq_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void imageback_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.current == this.ctldraw )
            {
                this.ShowGameMenu();
            }
           // else if ( this.current == this.ctlmemorygame )
           // {
            //    this.ShowGameMenu();
           // }
            else if (this.current == this.ctlttt)
            {
                this.ShowGameMenu();
            }
            else if (this.current == this.ctlcheckers )
            {
                this.ShowGameMenu();
            }
            else if (this.current == this.ctldice)
            {
                this.ShowGameMenu();
            }
            else if (this.current == this.ctltrivia)
            {
                this.ShowGameMenu();
            }
            else if (this.current == this.ctlchess)
            {
                this.ShowGameMenu();
            }
            else if (this.current == this.ctlspinthebottle)
            {
                this.ShowGameMenu();
            }
            //else if (this.current == this.ctlangrybirds)
            //{
            //    this.ShowGameMenu();
            //}
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
