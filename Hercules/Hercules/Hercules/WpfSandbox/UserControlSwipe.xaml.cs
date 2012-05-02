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
namespace WpfSandbox
{
    /// <summary>
    /// Interaction logic for UserControlSwipe.xaml
    /// </summary>
    public partial class UserControlSwipe : UserControl
    {
        private Point scrollTarget;
        private Point scrollStartPoint;
        private Point scrollStartOffset;
        private Point previousPoint;
        private Vector velocity;
        private double friction = 0.95;
        private DispatcherTimer animationTimer = new DispatcherTimer();
        private Point offset;

        private System.Collections.ArrayList items = new System.Collections.ArrayList();
        private System.Collections.Hashtable pos = new System.Collections.Hashtable();

        public UserControlSwipe()
        {
            InitializeComponent();

            this.items.Add(this.image1);
            this.items.Add(this.image2);
            this.items.Add(this.image3);
            this.items.Add(this.image4);
            this.items.Add(this.image5);

            this.HashPos(this.pos, this.image1);
            this.HashPos(this.pos, this.image2);
            this.HashPos(this.pos, this.image3);
            this.HashPos(this.pos, this.image4);
            this.HashPos(this.pos, this.image5);

            friction = 0.95;

            animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            animationTimer.Tick += new EventHandler(HandleWorldTimerTick);
            animationTimer.Start();

            FrameworkElement el = this.image1 as FrameworkElement;
            double x = (double)el.GetValue(Canvas.LeftProperty);
            double y = (double)el.GetValue(Canvas.TopProperty);
            this.offset = new Point(x, y);
        }

        public void HashPos(System.Collections.Hashtable hash, object el)
        {
            FrameworkElement element = el as FrameworkElement;
            FrameworkElement canvas = element.Parent as FrameworkElement;
            double x = (double)element.GetValue(Canvas.LeftProperty);
            hash[el] = x;
        }

        public void Update()
        {
            for (int i = 0; i < this.items.Count; i++)
            {
                double x = this.offset.X;
                FrameworkElement el = this.items[i] as FrameworkElement;
                double offset = (double)this.pos[this.items[i]];
                double newx = x + offset;

                double mx = newx % 1024;
                double usex = 0.0;
                if (mx < 0)
                {
                    usex = 1024 + mx;
                }
                else
                {
                    usex = mx;
                }

                el.SetValue(Canvas.LeftProperty, usex);
                //el.SetValue(Canvas.TopProperty, this.offset.Y);
            }
        }

        #region Friction Stuff
        private void HandleWorldTimerTick(object sender, EventArgs e)
        {
            bool move = false;
            if (IsMouseCaptured)
            {
                Point currentPoint = Mouse.GetPosition(this);
                velocity = previousPoint - currentPoint;
                previousPoint = currentPoint;
                move = true;
            }
            else
            {
                if (velocity.Length > 1)
                {
                    //ScrollViewer.ScrollToHorizontalOffset(scrollTarget.X);
                    //ScrollViewer.ScrollToVerticalOffset(scrollTarget.Y);
                    scrollTarget.X -= velocity.X;
                    //scrollTarget.Y -= velocity.Y;
                    velocity *= friction;
                    move = true;

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
            //if (ScrollViewer.IsMouseOver)
            if (this.scrollViewer1.IsMouseOver)
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

    }
}
