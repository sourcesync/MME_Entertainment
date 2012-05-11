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

using System.Windows.Threading;
using System.Diagnostics;

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

        private System.Collections.ArrayList images = new System.Collections.ArrayList();
        private System.Collections.ArrayList srcq = new System.Collections.ArrayList();
        private System.Collections.Hashtable src_hash = new System.Collections.Hashtable();

        private System.Collections.ArrayList captions = new System.Collections.ArrayList();
        private System.Collections.ArrayList cap_srcq = new System.Collections.ArrayList();
        private System.Collections.Hashtable cap_src_hash = new System.Collections.Hashtable();
        private System.Collections.Hashtable cap_tr = new System.Collections.Hashtable();

        public delegate void UserControlMainDelegate(int option);
        public UserControlMainDelegate evt = null;

        

        private int rotation = 0;

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

        public UserControlMain()
        {
            InitializeComponent();

            this.images.Add(this.image9);
            this.images.Add(this.image2);
            this.images.Add(this.image3);
            this.images.Add(this.image4);
            this.images.Add(this.image5);
            this.images.Add(this.image6);
            this.images.Add(this.image7);
            this.images.Add(this.image8);
            //this.images.Add(this.image10);

            this.captions.Add(this.imageweb2);
            this.captions.Add(this.imagegames);
            this.captions.Add(this.imagepromos);
            this.captions.Add(this.imageph);
            this.captions.Add(this.imagemenu);
            this.captions.Add(this.imagecheckin);
            this.captions.Add(this.imageevents);
            this.captions.Add(this.imageweb);


            this.srcq.Add("/HerculesWPFMain;component/Images/games-icon1.png");
            this.srcq.Add("/HerculesWPFMain;component/Images/promo-icon.png");
            this.srcq.Add("/HerculesWPFMain;component/Images/camera-icon.png");
            this.srcq.Add("/HerculesWPFMain;component/Images/menu-icon.png");
            this.srcq.Add("/HerculesWPFMain;component/Images/checkin-icon.png");
            this.srcq.Add("/HerculesWPFMain;component/Images/future-icon.png");
            this.srcq.Add("/HerculesWPFMain;component/Images/web-icon.png");

            this.cap_srcq.Add("/HerculesWPFMain;component/Images/games100.png");
            this.cap_srcq.Add("/HerculesWPFMain;component/Images/promos100.png");
            this.cap_srcq.Add("/HerculesWPFMain;component/Images/photobooth100.png");
            this.cap_srcq.Add("/HerculesWPFMain;component/Images/menu100.png");
            this.cap_srcq.Add("/HerculesWPFMain;component/Images/checkin100.png");
            this.cap_srcq.Add("/HerculesWPFMain;component/Images/events100.png");
            this.cap_srcq.Add("/HerculesWPFMain;component/Images/web100.png");

            TransformGroup gr = new TransformGroup();
            gr.Children.Add(new TranslateTransform(-11, 0));
            gr.Children.Add(new ScaleTransform(3, 3));
            this.cap_tr["/HerculesWPFMain;component/Images/games100.png"] = gr;

            this.cap_tr["/HerculesWPFMain;component/Images/promos100.png"] = gr;

            this.cap_tr["/HerculesWPFMain;component/Images/photobooth100.png"] = gr;

            this.cap_tr["/HerculesWPFMain;component/Images/menu100.png"] = gr;

            this.cap_tr["/HerculesWPFMain;component/Images/checkin100.png"] = gr;

            this.cap_tr["/HerculesWPFMain;component/Images/events100.png"] = gr;

            this.cap_tr["/HerculesWPFMain;component/Images/web100.png"] = gr;

            this.src_hash[this.image9] = "/HerculesWPFMain;component/Images/web-icon.png";
            this.src_hash[this.image2] = "/HerculesWPFMain;component/Images/games-icon1.png";
            this.src_hash[this.image3] = "/HerculesWPFMain;component/Images/promo-icon.png";
            this.src_hash[this.image4] = "/HerculesWPFMain;component/Images/camera-icon.png";
            this.src_hash[this.image5] = "/HerculesWPFMain;component/Images/menu-icon.png";
            this.src_hash[this.image6] = "/HerculesWPFMain;component/Images/checkin-icon.png";
            this.src_hash[this.image7] = "/HerculesWPFMain;component/Images/future-icon.png";
            this.src_hash[this.image8] = "/HerculesWPFMain;component/Images/web-icon.png";
            //this.src_hash[this.image10] = "/WpfSandbox;component/Images/games-icon1.png";

            this.cap_src_hash[this.imageweb2] = "/HerculesWPFMain;component/Images/web100.png";
            this.cap_src_hash[this.imagegames] = "/HerculesWPFMain;component/Images/games100.png";
            this.cap_src_hash[this.imagepromos] = "/HerculesWPFMain;component/Images/promos100.png";
            this.cap_src_hash[this.imageph] = "/HerculesWPFMain;component/Images/photobooth100.png";
            this.cap_src_hash[this.imagemenu] = "/HerculesWPFMain;component/Images/menu100.png";
            this.cap_src_hash[this.imagecheckin] = "/HerculesWPFMain;component/Images/checkin100.png";
            this.cap_src_hash[this.imageevents] = "/HerculesWPFMain;component/Images/events100.png";
            this.cap_src_hash[this.imageweb] = "/HerculesWPFMain;component/Images/web100.png";

            this.ResizeImages(1.4);

            double frac = 1024 / 7;

            for (int i = 0; i < this.images.Count; i++)
            {
                double x = (i - 1) * frac;
                double y = 320;
                FrameworkElement element = this.images[i] as FrameworkElement;
                element.SetValue(Canvas.LeftProperty, x);
                element.SetValue(Canvas.TopProperty, y);

                y = 480;
                element = this.captions[i] as FrameworkElement;
                element.SetValue(Canvas.LeftProperty, x);
                element.SetValue(Canvas.TopProperty, y);

                Image img = (Image)this.captions[i];
                String path = (String)this.cap_src_hash[img];
                img.RenderTransform = (TransformGroup)this.cap_tr[path];
            }



            friction = 0.95;

            animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            animationTimer.Tick += new EventHandler(HandleWorldTimerTick);
            animationTimer.Start();

        }

        public String[] GetPrev(object o)
        {
            //  Get current item path...
            String path = (String)this.src_hash[o];
            int idx = this.srcq.IndexOf(path);
            if (idx == 0)
                return new String[] { (String)this.srcq[this.srcq.Count - 1], (String)this.cap_srcq[this.srcq.Count - 1] };
            else
                return new String[] { (String)this.srcq[idx - 1], (String)this.cap_srcq[idx - 1] };
        }

        public String[] GetPost(object o)
        {
            //  Get current item path...
            String path = (String)this.src_hash[o];
            int idx = this.srcq.IndexOf(path);
            if (idx == this.srcq.Count - 1)
                return new String[] { (String)this.srcq[0], (String)this.cap_srcq[0] };
            else
                return new String[] { (String)this.srcq[idx + 1], (String)this.cap_srcq[idx + 1] };
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
            }
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            //a.ShowMenu();
        }


        private void image5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            return;
        }

        private void image5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.moving) return;

            String src = (String)this.src_hash[sender];
            if (src == "/HerculesWPFMain;component/Images/menu-icon.png")
            {
                if (evt != null) evt(0);   
            }
            else if (src == "/HerculesWPFMain;component/Images/camera-icon.png")
            {
                if (evt != null) evt(1);
            }
            else if (src == "/HerculesWPFMain;component/Images/web-icon.png")
            {
                if (evt != null) evt(2);
            }
            else if (src == "/HerculesWPFMain;component/Images/future-icon.png")
            {
                if (evt != null) evt(3);
            }
            else if (src == "/HerculesWPFMain;component/Images/promo-icon.png")
            {
                if (evt != null) evt(4);
            }
            else if (src == "/HerculesWPFMain;component/Images/checkin-icon.png")
            {
                if (evt != null) evt(5);
            }
            else if (src == "/HerculesWPFMain;component/Images/games-icon1.png")
            {
                if (evt != null) evt(6);
            }

        }

        public void ReplaceImage(Image img, String path)
        {
            img.Source = null;
            Uri uri = new Uri(path, UriKind.Relative);
            BitmapImage bm = new BitmapImage(uri);
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
            }

            /*reque as needed*/
            {
                Image fimg = (Image)this.images[0];
                Image fcap = (Image)this.captions[0];
                Image limg = (Image)this.images[this.images.Count - 1];
                Image lcap = (Image)this.captions[this.captions.Count - 1];

                FrameworkElement fel = fimg as FrameworkElement;
                FrameworkElement fcapel = fcap as FrameworkElement;
                double fcoordx = (double)fel.GetValue(Canvas.LeftProperty);
                FrameworkElement lel = limg as FrameworkElement;
                FrameworkElement lcapel = lcap as FrameworkElement;
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
                if (velocity.Length < 1)
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

                if (velocity.Length > 1)
                {
                    if (velocity.X > 35) velocity.X = 35;
                    else if (velocity.X < -35) velocity.X = -35;

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
            //if (this.scrollViewer1.IsMouseOver)
            {
                // Save starting point, used later when determining how much to scroll.
                scrollStartPoint = e.GetPosition(this);
                scrollStartOffset.X = this.offset.X; // ScrollViewer.HorizontalOffset;
                scrollStartOffset.Y = this.offset.Y; // ScrollViewer.VerticalOffset;

                /*
                // Update the cursor if can scroll or not.
                this.Cursor = (ScrollViewer.ExtentWidth > ScrollViewer.ViewportWidth) ||
                    (ScrollViewer.ExtentHeight > ScrollViewer.ViewportHeight) ?
                    Cursors.ScrollAll : Cursors.Arrow;
                */

                this.CaptureMouse();
            }

            base.OnPreviewMouseDown(e);
        }


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

        public delegate void UserControlMainToggle(int option);
        public UserControlMainToggle tevt = null;

        private void toggle(object sender)
        {
            if (this.tevt == null) return;
            if (sender == this.imagea)
                this.tevt(0);
            else
                this.tevt(1);
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


    }
}
