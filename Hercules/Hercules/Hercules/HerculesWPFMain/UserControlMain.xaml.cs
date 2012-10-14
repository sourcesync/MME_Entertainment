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

using System.Windows.Threading;
using System.Diagnostics;
using MME.HerculesConfig;

namespace HerculesWPFMain
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControlMain : UserControl
    {
        private Point scrollTarget;
        private Point scrollStartPoint;
        private Point scrollStartOffset;
        private Point previousPoint;
        private Vector velocity;
        private double friction = 0.95;
        private DispatcherTimer animationTimer = new DispatcherTimer();
        Point offset;
        Point lastoffset;
        bool firsttime = true;
        bool moving = false;
        bool wasmoving = false;
        public bool offline = false;

        private System.Collections.Hashtable main_hash = new System.Collections.Hashtable();

        private System.Collections.ArrayList images = new System.Collections.ArrayList();
        private System.Collections.ArrayList srcq = new System.Collections.ArrayList();
        private System.Collections.Hashtable src_hash = new System.Collections.Hashtable();


        private System.Collections.ArrayList captions = new System.Collections.ArrayList();
        private System.Collections.ArrayList cap_srcq = new System.Collections.ArrayList();
        private System.Collections.Hashtable cap_src_hash = new System.Collections.Hashtable();
        private System.Collections.Hashtable cap_tr = new System.Collections.Hashtable();


        private System.Collections.ArrayList labels = new System.Collections.ArrayList();
        private System.Collections.ArrayList lab_srcq = new System.Collections.ArrayList();
        private System.Collections.Hashtable lab_src_hash = new System.Collections.Hashtable();
        private System.Collections.Hashtable lab_tr = new System.Collections.Hashtable();

        public delegate void UserControlMainDelegate(String option);
        public UserControlMainDelegate evt = null;

        private System.Collections.Hashtable bmcache = new System.Collections.Hashtable();

        private int rotation = 0;

        private System.Collections.Hashtable menuItemOffset = new System.Collections.Hashtable();

        SolidColorBrush myRedBrush = new SolidColorBrush(Colors.Red);
        SolidColorBrush myYellowBrush = new SolidColorBrush(Colors.Yellow);
        SolidColorBrush myGreenBrush = new SolidColorBrush(Colors.Green);
        SolidColorBrush myTransparentBrush = new SolidColorBrush(Colors.Transparent);
        SolidColorBrush myBlackBrush = new SolidColorBrush(Colors.Black);
        SolidColorBrush myWhiteBrush = new SolidColorBrush(Colors.White);
        SolidColorBrush myForeBrush = null;

        public void SetRotation(int i)
        {
            if (i == 0)
            {

                this.RenderTransform = new RotateTransform(0, 1024 / 2.0, 768 / 2.0);
            }
            else
            {
                this.RenderTransform = new RotateTransform(180, 1024 / 2.0, 768 / 2.0);
            }
        }

        public String MakeCap(String inp)
        {
            if (inp.ToLower() == "dc") return "DC";
            if (inp.ToLower() == "nyc") return "NYC";

            String tot = "";
            String[] parts = inp.Split(new char[] { ' ' });

            foreach (String part in parts )
            {
                
                tot += ( part[0].ToString().ToUpper() + part.Substring(1) );
                tot += " ";
            }

            tot = tot.Trim();
            return tot;
        }

        public UserControlMain()
        {
            InitializeComponent();

            this.images.Add(this.image11);
            this.images.Add(this.image9);
            this.images.Add(this.image2);
            this.images.Add(this.image3);
            this.images.Add(this.image4);
            this.images.Add(this.image5);
            this.images.Add(this.image6);
            this.images.Add(this.image7);
            this.images.Add(this.image8);

            this.captions.Add(this.imageevents2);
            this.captions.Add(this.imageweb2);
            this.captions.Add(this.imagegames);
            this.captions.Add(this.imagepromos);
            this.captions.Add(this.imageph);
            this.captions.Add(this.imagemenu);
            this.captions.Add(this.imagecheckin);
            this.captions.Add(this.imageevents);
            this.captions.Add(this.imageweb);

            this.labels.Add(this.label1);
            this.labels.Add(this.label2);
            this.labels.Add(this.label3);
            this.labels.Add(this.label4);
            this.labels.Add(this.label5);
            this.labels.Add(this.label6);
            this.labels.Add(this.label7);
            this.labels.Add(this.label8);
            this.labels.Add(this.label9);

            /*
            this.srcq.Add("/HerculesWPFMain;component/Images/games-icon1.png");
            this.srcq.Add("/HerculesWPFMain;component/Images/promo-icon.png");
            this.srcq.Add("/HerculesWPFMain;component/Images/camera-icon.png");
            this.srcq.Add("/HerculesWPFMain;component/Images/menu-icon.png");
            this.srcq.Add("/HerculesWPFMain;component/Images/checkin-icon.png");
            this.srcq.Add("/HerculesWPFMain;component/Images/future-icon.png");
            this.srcq.Add("/HerculesWPFMain;component/Images/web-icon.png");
            */
            this.srcq  = WindowUtility.GetMainPaths("server");

            System.Collections.ArrayList main_items = WindowUtility.GetMain("server");
            if (main_items == null)
                return;
            else
            {
                for (int i = 0; i < this.srcq.Count; i++)
                {
                    String src_str = (String)this.srcq[i];
                    String main_item = (String)main_items[i];
                    this.main_hash[src_str] = main_item;
                }
            }

            
            /*
            this.cap_srcq.Add("/HerculesWPFMain;component/Images/games100.png");
            this.cap_srcq.Add("/HerculesWPFMain;component/Images/promos100.png");
            this.cap_srcq.Add("/HerculesWPFMain;component/Images/photobooth100.png");
            this.cap_srcq.Add("/HerculesWPFMain;component/Images/menu100.png");
            this.cap_srcq.Add("/HerculesWPFMain;component/Images/checkin100.png");
            this.cap_srcq.Add("/HerculesWPFMain;component/Images/events100.png");
            this.cap_srcq.Add("/HerculesWPFMain;component/Images/web100.png");
            */
            this.cap_srcq = WindowUtility.GetMainPaths("server");

            /*
            TransformGroup gr = new TransformGroup();
            gr.Children.Add(new TranslateTransform(-11, 0));
            gr.Children.Add(new ScaleTransform(3, 3));
             * */
            TransformGroup gr = new TransformGroup();
            //gr.Children.Add(new TranslateTransform(20, 0));
            gr.Children.Add(new ScaleTransform(2, 2));


            /*
            this.cap_tr["/HerculesWPFMain;component/Images/games100.png"] = gr;
            this.cap_tr["/HerculesWPFMain;component/Images/promos100.png"] = gr;
            this.cap_tr["/HerculesWPFMain;component/Images/photobooth100.png"] = gr;
            this.cap_tr["/HerculesWPFMain;component/Images/menu100.png"] = gr;
            this.cap_tr["/HerculesWPFMain;component/Images/checkin100.png"] = gr;
            this.cap_tr["/HerculesWPFMain;component/Images/events100.png"] = gr;
            this.cap_tr["/HerculesWPFMain;component/Images/web100.png"] = gr;
             * */
            foreach (String cap_path in this.cap_srcq)
            {
                this.cap_tr[cap_path] = gr;
            }

            /*
            this.src_hash[this.image11] = "/HerculesWPFMain;component/Images/future-icon.png";
            this.src_hash[this.image9] = "/HerculesWPFMain;component/Images/web-icon.png";
            this.src_hash[this.image2] = "/HerculesWPFMain;component/Images/games-icon1.png";
            this.src_hash[this.image3] = "/HerculesWPFMain;component/Images/promo-icon.png";
            this.src_hash[this.image4] = "/HerculesWPFMain;component/Images/camera-icon.png";
            this.src_hash[this.image5] = "/HerculesWPFMain;component/Images/menu-icon.png";
            this.src_hash[this.image6] = "/HerculesWPFMain;component/Images/checkin-icon.png";
            this.src_hash[this.image7] = "/HerculesWPFMain;component/Images/future-icon.png";
            this.src_hash[this.image8] = "/HerculesWPFMain;component/Images/web-icon.png";
            //this.src_hash[this.image10] = "/WpfSandbox;component/Images/games-icon1.png";
            */
            for (int i = 0; i < this.images.Count; i++)
            {
                int count = this.srcq.Count;
                int which = (i % count);
                String path = (String)this.srcq[which];
                this.src_hash[this.images[i]] = path;

                Image img = (Image)this.images[i];
                img.Source = this.GetBitMap(path);
            }

            /*
            this.cap_src_hash[this.imageevents2] = "/HerculesWPFMain;component/Images/events100.png";
            this.cap_src_hash[this.imageweb2] = "/HerculesWPFMain;component/Images/web100.png";
            this.cap_src_hash[this.imagegames] = "/HerculesWPFMain;component/Images/games100.png";
            this.cap_src_hash[this.imagepromos] = "/HerculesWPFMain;component/Images/promos100.png";
            this.cap_src_hash[this.imageph] = "/HerculesWPFMain;component/Images/photobooth100.png";
            this.cap_src_hash[this.imagemenu] = "/HerculesWPFMain;component/Images/menu100.png";
            this.cap_src_hash[this.imagecheckin] = "/HerculesWPFMain;component/Images/checkin100.png";
            this.cap_src_hash[this.imageevents] = "/HerculesWPFMain;component/Images/events100.png";
            this.cap_src_hash[this.imageweb] = "/HerculesWPFMain;component/Images/web100.png";
            */
            for (int i = 0; i < this.captions.Count; i++)
            {
                int which = (i % this.cap_srcq.Count);
                String path = (String)this.cap_srcq[which];
                this.cap_src_hash[this.captions[i]] = path;


                //Image img = (Image)this.images[i];
                //img.Source = this.GetBitMap(path);
            }


            this.lab_srcq = WindowUtility.GetMain("server");
            for (int i = 0; i < this.lab_srcq.Count; i++)
            {
                String path = (String)this.lab_srcq[i];
                TextBlock lbl = (TextBlock)this.labels[0];
                System.Windows.Media.Color col = new System.Windows.Media.Color();
                col.R = 0xff;
                col.G = 0xff;
                col.B = 0xff;
                lbl.Foreground = new SolidColorBrush(col);
                FrameworkElement el = lbl as FrameworkElement;
                
                //String ppath = (String)this.lab_srcq[i];
                //String item = ppath[0].ToString().ToUpper() + ppath.Substring(1);
                lbl.Text = MakeCap((String)this.lab_srcq[i]);

                double w = 1024 / 7;
                double off = (w - (double)el.GetValue(Canvas.ActualWidthProperty)) / 2.0 - 15;
                TransformGroup ggr = new TransformGroup();
                ggr.Children.Add(new ScaleTransform(2, 2));

                /* MANDARIN
                double fudge = 10.0f;
                if (path == "concierge")
                    fudge = 30;
                else if (path == "check-in")
                    fudge = 25;
                else if (path == "camera")
                    fudge = 20;
                else if (path == "calendar")
                    fudge = 25;
                else if (path == "promo")
                    fudge = 10;
                else if (path == "web")
                    fudge = 8;
                 * */
                
                this.GetMenuOffset();
                
                double fudge = 20.0f;


                String pppath = path.ToLower();
                if (this.menuItemOffset[pppath] != null)
                {
                    String val = (String)this.menuItemOffset[pppath];
                    fudge = double.Parse(val);

                }

                    /*
                if (path == "daybreak site")
                    fudge = 45;
                else if (path == "check-in")
                    fudge = 35;
                else if (path == "games")
                    fudge = 7;
                else if (path == "jackboxer site")
                    fudge = 45;
                else if (path == "photobooth")
                    fudge = 40;
                else if (path == "calendar")
                    fudge = 15;          
                else if (path == "gallery" )
                    fudge = 30;
                else if (path == "menu")
                    fudge = 0;
                else if (path == "promo")
                    fudge = 5;
                 */
                

                ggr.Children.Add(new TranslateTransform(off-fudge, 0));
                this.lab_tr[path] = ggr;
            }


            for (int i = 0; i < this.labels.Count; i++)
            {
                int which = (i % this.lab_srcq.Count);
                String path = (String)this.lab_srcq[which];
                this.lab_src_hash[this.labels[i]] = path;
                TextBlock lbl = (TextBlock)this.labels[i];

                //lbl.Text = path;
                //String item = path[0].ToString().ToUpper() + path.Substring(1);

                lbl.Text = MakeCap(path);
                
                
                lbl.Foreground = this.myWhiteBrush;
                

            }

            //this.ResizeImages(1.4);
            this.ResizeImages(1.0);

            double frac = 1024 / 7;

            for (int i = 0; i < this.images.Count; i++)
            {
                double x = (i - 1) * frac;
                double y = 320;
                FrameworkElement element = this.images[i] as FrameworkElement;
                element.SetValue(Canvas.LeftProperty, x);
                element.SetValue(Canvas.TopProperty, y);

                //  override y offset based on config value...
                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "MenuOffsetY")))
                {
                    double global_offset_y = 0.0;
                    global_offset_y = double.Parse(ConfigUtility.GetConfig(ConfigUtility.Config, "MenuOffsetY"));
                    element.SetValue(Canvas.TopProperty, global_offset_y);
                }

                y = 480;
                element = this.captions[i] as FrameworkElement;
                element.SetValue(Canvas.LeftProperty, x);
                element.SetValue(Canvas.TopProperty, y);

               

                Image img = (Image)this.captions[i];
                String path = (String)this.cap_src_hash[img];
                img.RenderTransform = (TransformGroup)this.cap_tr[path];
                img.Visibility = System.Windows.Visibility.Hidden;


                y = 480;
                element = this.labels[i] as FrameworkElement;
                element.SetValue(Canvas.LeftProperty, x);
                element.SetValue(Canvas.TopProperty, y);
                TextBlock lbl = (TextBlock)element;
                path = (String)this.lab_src_hash[lbl];
                lbl.RenderTransform = (TransformGroup)this.lab_tr[path];

                //  override y offset based on config value...
                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "MenuLabelOffsetY")))
                {
                    double global_offset_y = 0.0;
                    global_offset_y = double.Parse(ConfigUtility.GetConfig(ConfigUtility.Config, "MenuLabelOffsetY"));
                    element.SetValue(Canvas.TopProperty, global_offset_y);
                }

            }



            friction = 0.95;

            animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            animationTimer.Tick += new EventHandler(HandleWorldTimerTick);
            animationTimer.Start();


            String bg_file = "table_main_menu_bg.jpg";
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "BGFile")))
            {
                bg_file = ConfigUtility.GetConfig(ConfigUtility.Config, "BGFile");
            }

            BitmapSource src = WindowUtility.GetScreenBitmapWPF(bg_file);

            this.image1.Source = src;

        }

        private void GetMenuOffset()
        {
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "MenuItemOffset")))
            {
                String str = ConfigUtility.GetConfig(ConfigUtility.Config, "MenuItemOffset");

                String[] kvs = str.Split( new char[] {';'} );
                foreach ( String kv in kvs )
                {
                    String[] pair = kv.Split( new char[] {':'} );
                    this.menuItemOffset[pair[0]] = pair[1];
                }
            }
        }

        public String[] GetPrev(object o)
        {
            //  Get current item path...
            String path = (String)this.src_hash[o];
            int idx = this.srcq.IndexOf(path);
            if (idx == 0)
                return new String[] { (String)this.srcq[this.srcq.Count - 1], 
                    (String)this.cap_srcq[this.srcq.Count - 1],
                    (String)this.lab_srcq[this.srcq.Count - 1]};
            else
                return new String[] { (String)this.srcq[idx - 1], 
                    (String)this.cap_srcq[idx - 1],
                    (String)this.lab_srcq[idx - 1]};
        }

        public String[] GetPost(object o)
        {
            //  Get current item path...
            String path = (String)this.src_hash[o];
            int idx = this.srcq.IndexOf(path);
            if (idx == this.srcq.Count - 1)
                return new String[] { (String)this.srcq[0], (String)this.cap_srcq[0],
                 (String)this.lab_srcq[0]};
            else
                return new String[] { (String)this.srcq[idx + 1], (String)this.cap_srcq[idx + 1],
                (String)this.lab_srcq[idx + 1]};
        }

        public void HashPos(System.Collections.Hashtable hash, object el)
        {
            FrameworkElement element = el as FrameworkElement;
            FrameworkElement canvas = element.Parent as FrameworkElement;
            double x = (double)element.GetValue(Canvas.LeftProperty);
            hash[el] = x;
        }

        void ResizeImages(double factor)
        {
            for (int i = 0; i < this.images.Count; i++)
            {
                Image img = (Image)this.images[i];
                double w = img.Width;
                img.Width = factor * img.Width;
                double h = img.Height;
                img.Height = factor * img.Height;

                double offx = img.Width - w;
                double offy = img.Height - h;
                FrameworkElement element = img as FrameworkElement;
                double x = (double)element.GetValue(Canvas.LeftProperty);
                double y = (double)element.GetValue(Canvas.TopProperty);
                element.SetValue(Canvas.LeftProperty, x - offx / 2.0);
                element.SetValue(Canvas.TopProperty, y - offy / 2.0);

                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "MenuOffsetY")))
                {
                    double global_offset_y = 0.0;
                    global_offset_y = double.Parse(ConfigUtility.GetConfig(ConfigUtility.Config, "MenuOffsetY"));
                    element.SetValue(Canvas.TopProperty, global_offset_y);
                }
            }
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            //a.ShowMenu();
        }


        private void image5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //return;
            //this.image5_MouseDown(sender, e);
            this.item_clicked(sender);
        }

        private void image5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //this.item_clicked(sender);
            
        }

        private void item_clicked(object sender)
        {
            //if (this.moving) return;

            String src = (String)this.src_hash[sender];

            String main_item = (String)this.main_hash[src];

            if (evt != null) evt(main_item);
        }

        public BitmapImage GetBitMap(String path)
        {
            BitmapImage bi = (BitmapImage)this.bmcache[path];
            if (bi == null)
            {
                BitmapImage bm = WindowUtility.GetBitmapWPF(path);

                this.bmcache.Add(path, bm);
                return bm;
            }
            else
            {
                return bi;
            }
        }

        public void ReplaceLabel(TextBlock lbl, String txt)
        {
            //String item = txt[0].ToString().ToUpper() + txt.Substring(1);

            lbl.Text = MakeCap(txt);

            //String.lbl.Text = txt;
        }

        public void ReplaceImage(Image img, String path)
        {
            img.Source = null;
            BitmapImage bm = this.GetBitMap(path);
            img.Source = bm;
        }

        public void Update()
        {
            if (firsttime)
            {
                lastoffset = offset;
                firsttime = false;
                return;
            }

            /* increment each item in order */
            double diff = offset.X - lastoffset.X;
            for (int i = 0; i < this.images.Count; i++)
            {
                FrameworkElement el = this.images[i] as FrameworkElement;
                double coordx = (double)el.GetValue(Canvas.LeftProperty);
                coordx += diff;
                el.SetValue(Canvas.LeftProperty, coordx);

                el = this.captions[i] as FrameworkElement;
                coordx = (double)el.GetValue(Canvas.LeftProperty);
                coordx += diff;
                el.SetValue(Canvas.LeftProperty, coordx);

                el = this.labels[i] as FrameworkElement;
                coordx = (double)el.GetValue(Canvas.LeftProperty);
                coordx += diff;
                el.SetValue(Canvas.LeftProperty, coordx);
            }

            /*reque as needed*/
            {
                Image fimg = (Image)this.images[0];
                Image fcap = (Image)this.captions[0];
                fcap.Visibility = System.Windows.Visibility.Hidden;
                TextBlock flab = (TextBlock)this.labels[0];
                Image limg = (Image)this.images[this.images.Count - 1];
                Image lcap = (Image)this.captions[this.captions.Count - 1];
                lcap.Visibility = System.Windows.Visibility.Hidden;
                TextBlock llab = (TextBlock)this.labels[this.labels.Count - 1];

                FrameworkElement fel = fimg as FrameworkElement;
                FrameworkElement fcapel = fcap as FrameworkElement;
                FrameworkElement flabel = flab as FrameworkElement;
                double fcoordx = (double)fel.GetValue(Canvas.LeftProperty);
                FrameworkElement lel = limg as FrameworkElement;
                FrameworkElement lcapel = lcap as FrameworkElement;
                FrameworkElement llabel = llab as FrameworkElement;
                double lcoordx = (double)lel.GetValue(Canvas.LeftProperty);

                if (fcoordx + 1024 / 7 < 0)
                {
                    this.images.RemoveAt(0);
                    this.images.Add(fimg);
                    fel.SetValue(Canvas.LeftProperty, lcoordx + 1024 / 7);
                    String[] paths = this.GetPost(lel);
                    this.ReplaceImage(fimg, paths[0]);
                    this.src_hash[fimg] = paths[0];

                    this.captions.RemoveAt(0);
                    this.captions.Add(fcap);
                    fcapel.SetValue(Canvas.LeftProperty, lcoordx + 1024 / 7);
                    this.ReplaceImage(fcap, paths[1]);
                    fcap.RenderTransform = (TransformGroup)this.cap_tr[paths[1]];
                    this.cap_src_hash[fcap] = paths[1];

                    this.labels.RemoveAt(0);
                    this.labels.Add(flab);
                    flabel.SetValue(Canvas.LeftProperty, lcoordx + 1024 / 7);
                    this.ReplaceLabel(flab, paths[2]);
                    flab.RenderTransform = (TransformGroup)this.lab_tr[paths[2]];
                    this.lab_src_hash[flab] = paths[2];
                }
                else if (lcoordx > 1024)
                {
                    this.images.RemoveAt(this.images.Count - 1);
                    this.images.Insert(0, limg);
                    lel.SetValue(Canvas.LeftProperty, fcoordx - 1024 / 7);
                    String[] paths = this.GetPrev(fel);
                    this.ReplaceImage(limg, paths[0]);
                    this.src_hash[limg] = paths[0];

                    this.captions.RemoveAt(this.captions.Count - 1);
                    this.captions.Insert(0, lcap);
                    lcapel.SetValue(Canvas.LeftProperty, fcoordx - 1024 / 7);
                    this.ReplaceImage(lcap, paths[1]);
                    lcap.RenderTransform = (TransformGroup)this.cap_tr[paths[1]];
                    this.cap_src_hash[lcap] = paths[1];

                    this.labels.RemoveAt(this.labels.Count - 1);
                    this.labels.Insert(0, llab);
                    llabel.SetValue(Canvas.LeftProperty, fcoordx - 1024 / 7);
                    this.ReplaceLabel(llab, paths[2]);
                    llab.RenderTransform = (TransformGroup)this.lab_tr[paths[2]];
                    this.lab_src_hash[llab] = paths[2];
                }
            }

            lastoffset = offset;

        }

        #region Friction Stuff
        private void HandleWorldTimerTick(object sender, EventArgs e)
        {
            //System.Console.WriteLine(this.moving.ToString());
            if (IsMouseCaptured)
            {
                Point currentPoint = Mouse.GetPosition(this);
                velocity = previousPoint - currentPoint;
                previousPoint = currentPoint;
                if (velocity.Length < 0.25)
                {
                    this.moving = false;
                    this.wasmoving = true;
                }
                else
                {
                    this.moving = true;
                }
            }
            else
            {
                this.moving = true;

                if (velocity.Length > 0.5)
                {
                    if (velocity.X > 45) velocity.X = 45;
                    else if (velocity.X < -45) velocity.X = -45;

                    scrollTarget.X -= velocity.X;
                    velocity *= friction;

                    this.offset.X = scrollTarget.X;
                    this.offset.Y = scrollTarget.Y;

                    this.Update();
                }

            }

        }

        public double Friction
        {
            get { return 1.0 - friction; }
            set { friction = Math.Min(Math.Max(1.0 - value, 0), 1.0); }
        }
        #endregion



        #region Mouse Events
        
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {

            /*
            Point pt = e.GetPosition(this);
            double x = pt.X;
            double y = pt.Y;
            //339/81
            double lx = 339 - 50;
            double ty = 81 - 50;
            double rx = 339 + 50;
            double by = 81 + 50;
            if ((x > lx) && (x < rx) && (y > ty) && (y < by))
            {
                this.evt("hidden");
                return;
            }
            */

            if ( this.imagea.IsMouseOver ) 
            {
                this.imagea_MouseDown(this.imagea, e);
                return;
            }
            else if (this.imageb.IsMouseOver)
            {
                this.imageb_MouseDown(this.imageb, e);
                return;
            }
            //this.Cursor = (this.scrollViewer1.ExtentWidth > this.scrollViewer1.ViewportWidth) ||
            //        (this.scrollViewer1.ExtentHeight > this.scrollViewer1.ViewportHeight) ?
            //       Cursors.ScrollAll : Cursors.Arrow;

            //if (ScrollViewer.IsMouseOver)
            if (this.scrollViewer1.IsMouseOver)
            {
                // Save starting point, used later when determining how much to scroll.
                scrollStartPoint = e.GetPosition(this);
                scrollStartOffset.X = this.offset.X; // ScrollViewer.HorizontalOffset;
                scrollStartOffset.Y = this.offset.Y; // ScrollViewer.VerticalOffset;

                //if (this.offset.Y > 432)
                {
                    this.CaptureMouse();
                }
            }

        
            base.OnPreviewMouseDown(e);
            
        }
         


        /*
        // Update the cursor if can scroll or not.
        this.Cursor = (ScrollViewer.ExtentWidth > ScrollViewer.ViewportWidth) ||
            (ScrollViewer.ExtentHeight > ScrollViewer.ViewportHeight) ?
            Cursors.ScrollAll : Cursors.Arrow;
        */
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                Point currentPoint = e.GetPosition(this);

                // Determine the new amount to scroll.
                Point delta = new Point(scrollStartPoint.X -
                   currentPoint.X, scrollStartPoint.Y - currentPoint.Y);

                scrollTarget.X = scrollStartOffset.X - delta.X;
                //scrollTarget.Y = scrollStartOffset.Y - delta.Y;

                // Scroll to the new position.
                //ScrollViewer.ScrollToHorizontalOffset(scrollTarget.X);
                //ScrollViewer.ScrollToVerticalOffset(scrollTarget.Y);
                this.offset.X = scrollTarget.X;
                this.offset.Y = scrollTarget.Y;

                this.Update();
            }

            base.OnPreviewMouseMove(e);
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                this.Cursor = Cursors.Arrow;
                this.ReleaseMouseCapture();
            }

            base.OnPreviewMouseUp(e);
        }
        #endregion

        private void image1_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public delegate void UserControlMainToggle(String option);
        public UserControlMainToggle tevt = null;

        private void toggle(object sender)
        {
            if (this.tevt == null) return;
            if (sender == this.imagea)
                this.tevt("main");
            else
                this.tevt("menu");
        }

        private void imagea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.toggle(sender);
        }

        private void imageb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.toggle(sender);
        }

        private void imageweb_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            for (int i = 0; i < this.labels.Count; i++)
            {
                int which = (i % this.lab_srcq.Count);
                String path = (String)this.lab_srcq[which];
                this.lab_src_hash[this.labels[i]] = path;
                Label lbl = (Label)this.labels[i];
                

                TransformGroup ggr = new TransformGroup();
                double off = lbl.Width;
                FrameworkElement el = lbl as FrameworkElement;
                off = (1024.0 / 7 - (double)el.GetValue(Canvas.ActualWidthProperty)) / 2.0;
                
                ggr.Children.Add(new TranslateTransform(off, 0));
                //ggr.Children.Add(new ScaleTransform(2, 2));
                this.lab_tr[path] = ggr;

            }
             * */
        }

        private void image11_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }


    }
}
