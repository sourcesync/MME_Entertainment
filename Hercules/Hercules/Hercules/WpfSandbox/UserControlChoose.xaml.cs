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
using System.Threading;

using System.Windows.Threading;
using System.Diagnostics;

namespace WpfSandbox
{
    /// <summary>
    /// Interaction logic for UserControlChoose.xaml
    /// </summary>
    public partial class UserControlChoose : UserControl
    {
        private Point scrollTarget;
        private Point scrollStartPoint;
        private Point scrollStartOffset;
        private Point previousPoint;
        private Vector velocity;
        private double friction = 0.95;
        private DispatcherTimer animationTimer = new DispatcherTimer();

        private const int MAX = 6;

        private Image selected = null;
        private Image purchased = null;
        private Point diff;
        private int cur_option = 0;

        //  template structures...
        private System.Collections.Hashtable purchase_poshash = new System.Collections.Hashtable();
        private System.Collections.Hashtable poshash = new System.Collections.Hashtable();
        private System.Collections.ArrayList imgs = new System.Collections.ArrayList();
        private System.Collections.ArrayList display_cart = new System.Collections.ArrayList();
        private System.Collections.Hashtable cost = new System.Collections.Hashtable();

        //  specific menus...

        //  sm...
        private String[] sms = { "/WpfSandbox;component/Images/sm1.jpg",
                                       "/WpfSandbox;component/Images/sm2.jpg",
                                       "/WpfSandbox;component/Images/sm3.jpg",
                                       "/WpfSandbox;component/Images/sm4.jpg",
                                        "/WpfSandbox;component/Images/sm5.jpg",
                                        "/WpfSandbox;component/Images/sm6.jpg" };
        private double[] smcost = { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

        private String[] craves = { "/WpfSandbox;component/Images/50_50_Crave_Case.jpg",
                                       "/WpfSandbox;component/Images/Brown-bag-left.jpg",
                                       "/WpfSandbox;component/Images/Crave_Case_with_Cheese.jpg",
                                       "/WpfSandbox;component/Images/menu_meal4.jpg",
                                        "/WpfSandbox;component/Images/Original_Crave_Case.jpg",
                                        "/WpfSandbox;component/Images/Variety_Crave_Case.jpg" };
        private double[] crave_cost = { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

        private String[] drinks = { "/WpfSandbox;component/Images/menu_drinks_coffee.jpg",
                                       "/WpfSandbox;component/Images/menu_drinks_coke.jpg",
                                       "/WpfSandbox;component/Images/menu_drinks_hot_chocolate.jpg",
                                       "/WpfSandbox;component/Images/menu_drinks_shakes.jpg",
                                        "/WpfSandbox;component/Images/menu_drinks_tea.jpg"};
        private double[] drink_cost = { 1.0, 1.0, 1.0, 1.0, 1.0  };

        private String[] bf = { "/WpfSandbox;component/Images/BaconEggCheese_Bun.jpg",
                                       "/WpfSandbox;component/Images/BaconEggCheese_Toast.jpg",
                                       "/WpfSandbox;component/Images/EggCheese_Bun.jpg",
                                       "/WpfSandbox;component/Images/EggCheese_Toast.jpg",
                                        "/WpfSandbox;component/Images/FrenchToast.jpg",
                                         "/WpfSandbox;component/Images/HashRounds.jpg",
                                          "/WpfSandbox;component/Images/menu_bfast_oj.jpg",
                                           "/WpfSandbox;component/Images/SausageCheese_Bun.jpg",
                                            "/WpfSandbox;component/Images/SausageCheese_Toast.jpg",
                                             "/WpfSandbox;component/Images/SausageEggCheese_Bun.jpg",
                                              "/WpfSandbox;component/Images/SausageEggCheese_Toast.jpg"};
        private double[] bf_cost = { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

        private String[] sl = { "/WpfSandbox;component/Images/BCNCHZBRGR.jpg",
                                       "/WpfSandbox;component/Images/CHKNBRSTCHZ.jpg",
                                       "/WpfSandbox;component/Images/CHKNRNGCHZ.jpg",
                                       "/WpfSandbox;component/Images/CHZBRGR.jpg",
                                        "/WpfSandbox;component/Images/DBLBCNCHZBRGR.jpg",
                                         "/WpfSandbox;component/Images/DBLCHZBRGR.jpg",
                                          "/WpfSandbox;component/Images/DBLJALCHZBRGR.jpg",
                                           "/WpfSandbox;component/Images/FISHCHZ.jpg",
                                            "/WpfSandbox;component/Images/JALCHZBRGR.jpg",
                                             "/WpfSandbox;component/Images/ORIG.jpg",
                                              "/WpfSandbox;component/Images/ORIGINALDOUBLE.jpg"};
        private double[] sl_cost = { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

        private String[] sd = { "/WpfSandbox;component/Images/menu_sides_chxnrings.jpg",
                                       "/WpfSandbox;component/Images/menu_sides_chzfries.jpg",
                                       "/WpfSandbox;component/Images/menu_sides_chzstx.jpg",
                                       "/WpfSandbox;component/Images/menu_sides_fishnibs.jpg",
                                       "/WpfSandbox;component/Images/menu_sides_fries.jpg",
                                        "/WpfSandbox;component/Images/menu_sides_onionrings.jpg",
                                        "/WpfSandbox;component/Images/OnionChips.jpg" };
        private double[] sd_cost = { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

        public UserControlChoose()
        {
            InitializeComponent();

            this.textBox1.BorderThickness = new System.Windows.Thickness(0.0);
            this.textBoxItems.BorderThickness = new System.Windows.Thickness(0.0);
            this.textBox3.BorderThickness = new System.Windows.Thickness(0.0);
            this.textBox99.BorderThickness = new System.Windows.Thickness(0.0);
            this.textBoxSubTotal.BorderThickness = new System.Windows.Thickness(0.0);
            this.textBoxTax.BorderThickness = new System.Windows.Thickness(0.0);
            this.textBoxTotalAmount.BorderThickness = new System.Windows.Thickness(0.0);

            //  Template objects...
            this.poshash[this.image2] = this.GetElementPos(this.image2);
            this.poshash[this.image3] = this.GetElementPos(this.image3);
            this.poshash[this.image4] = this.GetElementPos(this.image4);
            this.poshash[this.image5] = this.GetElementPos(this.image5);
            this.poshash[this.image6] = this.GetElementPos(this.image6);
            this.poshash[this.image7] = this.GetElementPos(this.image7);

            imgs.Add(this.image2);
            imgs.Add(this.image3);
            imgs.Add(this.image4);
            imgs.Add(this.image5);
            imgs.Add(this.image6);
            imgs.Add(this.image7);

            this.purchase_poshash[this.image8] = this.GetElementPos(this.image8);
            this.purchase_poshash[this.image9] = this.GetElementPos(this.image9);
            this.purchase_poshash[this.image10] = this.GetElementPos(this.image10);
            this.purchase_poshash[this.image11] = this.GetElementPos(this.image11);
            this.purchase_poshash[this.image12] = this.GetElementPos(this.image12);
            this.purchase_poshash[this.image13] = this.GetElementPos(this.image13);

            display_cart.Add(this.image8);
            display_cart.Add(this.image9);
            display_cart.Add(this.image10);
            display_cart.Add(this.image11);
            display_cart.Add(this.image12);
            display_cart.Add(this.image13);

            //  Specific menus...
            this.UpdateCostHash(this.sms, this.smcost);
            this.UpdateCostHash(this.craves, this.crave_cost);
            this.UpdateCostHash(this.drinks, this.drink_cost);
            this.UpdateCostHash(this.bf, this.bf_cost);
            this.UpdateCostHash(this.sl, this.sl_cost);
            this.UpdateCostHash(this.sd, this.sd_cost);

            //  Initial menu...
            this.SetOption(0);


            friction = 0.95;

            animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            animationTimer.Tick += new EventHandler(HandleWorldTimerTick);
            animationTimer.Start();

            ScrollViewer.Visibility = System.Windows.Visibility.Hidden;
            //ScrollViewer.ScrollChanged += new ScrollChangedEventHandler(ScrollViewer_ScrollChanged);
        }

        private void UpdateCostHash(String[] paths, double[] cst)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                this.cost[paths[i]] = cst[i];
            }
        }

        private String[] GetOptionSource( int option )
        {
            String[] src = null;
            switch (option)
            {
                case 0:
                    src = this.sms;
                    break;
                case 1:
                    src = this.craves;
                    break;
                case 2:
                    src = this.drinks;
                    break;
                case 3:
                    src = this.bf;
                    break;
                case 4:
                    src = this.sl;
                    break;
                case 5:
                    src = this.sd;
                    break;
                default:
                    src = this.sms;
                    break;
            }
            return src;
        }

        public void SetOption(int option)
        {
            this.cur_option = option;

            String[] src = this.GetOptionSource(option);

            int max = src.Length;
            if (max > MAX) max = MAX;

            for (int i = 0; i < max; i++)
            {
                Image img = (Image)this.imgs[i];
                img.Visibility = System.Windows.Visibility.Visible;
                String path = (String)src[i];
                Uri uri = new Uri(path, UriKind.Relative);
                BitmapImage bm = new BitmapImage(uri);
                if (img.Source != null) img.Source = null;
                img.Source = bm;
            }
            for (int i = src.Length; i < MAX; i++)
            {
                Image img = (Image)this.imgs[i];
                img.Visibility = System.Windows.Visibility.Hidden;
            }


        }

        private Point GetElementPos(object o)
        {
            FrameworkElement element = o as FrameworkElement;
            double ix = (double)element.GetValue(Canvas.LeftProperty);
            double iy = (double)element.GetValue(Canvas.TopProperty);
            return new Point(ix, iy);
        }

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            this.selected = img; // this.image2;
            FrameworkElement element = this.selected as FrameworkElement;
            FrameworkElement canvas = element.Parent as FrameworkElement;
            Point pos = e.GetPosition(canvas);

            double ix = (double)element.GetValue(Canvas.LeftProperty);
            double iy = (double)element.GetValue(Canvas.TopProperty);

            this.diff.X = pos.X - ix;
            this.diff.Y = pos.Y - iy;
        }


        private void image_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.selected != null)
            {
                FrameworkElement element = this.selected as FrameworkElement;
                FrameworkElement canvas = element.Parent as FrameworkElement;
                
                Point cur_mousepos = e.GetPosition(canvas);

                double newx = cur_mousepos.X - this.diff.X;
                double newy = cur_mousepos.Y - this.diff.Y;

                element.SetValue(Canvas.LeftProperty, newx);
                element.SetValue(Canvas.TopProperty, newy);
            }
            else if (this.purchased != null)
            {
                FrameworkElement element = this.purchased as FrameworkElement;
                FrameworkElement canvas = element.Parent as FrameworkElement;

                Point cur_mousepos = e.GetPosition(canvas);

                double newx = cur_mousepos.X - this.diff.X;
                double newy = cur_mousepos.Y - this.diff.Y;

                element.SetValue(Canvas.LeftProperty, newx);
                element.SetValue(Canvas.TopProperty, newy);
            }
        }

        private void update_cost()
        {
            WindowWhiteCastle w = WindowWhiteCastle.getParent(this);
            double subtotal = 0.0;
            for (int i = 0; i < w.cart.Count; i++)
            {
                String item = (String)w.cart[i];
                double cst = (double)this.cost[item];
                subtotal += cst;
            }

            double tax = subtotal * 0.08;

            double total = subtotal + tax;

            this.textBoxSubTotal.Text = String.Format("{0:0.00}", subtotal);
            this.textBoxTax.Text = String.Format("{0:0.00}", tax);
            this.textBoxTotalAmount.Text = String.Format("{0:0.00}", total);
        }

        private void redraw_cart()
        {
            WindowWhiteCastle w = WindowWhiteCastle.getParent(this);
            for (int i = 0; i < w.cart.Count; i++)
            {
                Image timg = (Image)this.display_cart[i];
                String path = (String)w.cart[i];
                Uri uri = new Uri(path, UriKind.Relative);
                BitmapImage bm = new BitmapImage(uri);
                if (timg.Source != null) timg.Source = null;
                timg.Source = bm;
                timg.Visibility = System.Windows.Visibility.Visible;
            }

            for (int i = w.cart.Count; i < this.display_cart.Count; i++)
            {
                Image timg = (Image)this.display_cart[i];
                timg.Visibility = System.Windows.Visibility.Hidden;
            }

            String txt = String.Format("{0} Items In Your Cart", w.cart.Count);
            this.textBoxItems.Text = txt;
        }

        private void buy(object o)
        {
            WindowWhiteCastle w = WindowWhiteCastle.getParent(this);
            if (w.cart.Count == this.display_cart.Count)
            {
                w.cart.RemoveAt(0);
            }
            int idx = this.imgs.IndexOf(o);
            String[] src = this.GetOptionSource(this.cur_option);
            String path = (String)src[idx];
            w.cart.Add(path);

            this.redraw_cart();
            this.update_cost();
        }

        private bool shop_test(object sender, MouseButtonEventArgs e)
        {
            if (this.selected != null)
            {
                FrameworkElement element = this.selected as FrameworkElement;
                FrameworkElement canvas = element.Parent as FrameworkElement;
                Point cur_mousepos = e.GetPosition(canvas);
                Point box = this.GetElementPos(this.rectangle1);

                if ((cur_mousepos.X > box.X) && (cur_mousepos.Y > box.Y) &&
                    (cur_mousepos.X < (box.X + this.rectangle1.Width)) &&
                    (cur_mousepos.X < (box.Y + this.rectangle1.Height)))
                {
                    this.buy(this.selected);
                    this.drag_cancel(this.selected);
                    return true;
                }
            }
            return false;
        }

        private void drag_cancel(object sender)
        {
            if (this.selected != null)
            {
                if (this.poshash[this.selected] != null)
                {
                    Point p = (Point)this.poshash[sender];
                    FrameworkElement element = this.selected as FrameworkElement;
                    element.SetValue(Canvas.LeftProperty, p.X);
                    element.SetValue(Canvas.TopProperty, p.Y);
                }
                this.selected = null;
            }
        }


        private void image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!this.shop_test(sender, e))
            {
                this.drag_cancel(sender);
            }
        }

        private void image_MouseLeave(object sender, MouseEventArgs e)
        {
            this.drag_cancel(sender);
        }


        private void RemoveItem(object sender)
        {
            int idx = this.display_cart.IndexOf(this.purchased);

            WindowWhiteCastle w = WindowWhiteCastle.getParent(this);
            w.cart.RemoveAt(idx);

            this.redraw_cart();
            this.update_cost();
        }

        private void purchase_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.purchased != null)
            {
                FrameworkElement element = this.purchased as FrameworkElement;
                FrameworkElement canvas = element.Parent as FrameworkElement;

                Point cur_mousepos = e.GetPosition(canvas);

                double newx = cur_mousepos.X - this.diff.X;
                double newy = cur_mousepos.Y - this.diff.Y;

                element.SetValue(Canvas.LeftProperty, newx);
                element.SetValue(Canvas.TopProperty, newy);
            }
        }

        private void purchased_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            this.purchased = img; // this.image2;
            FrameworkElement element = this.purchased as FrameworkElement;
            FrameworkElement canvas = element.Parent as FrameworkElement;
            Point pos = e.GetPosition(canvas);

            double ix = (double)element.GetValue(Canvas.LeftProperty);
            double iy = (double)element.GetValue(Canvas.TopProperty);

            this.diff.X = pos.X - ix;
            this.diff.Y = pos.Y - iy;
        }

        private void purchased_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!this.purchased_remove_test(sender, e))
            {
                this.purchased_drag_cancel(sender);
            }
        }


        private bool purchased_remove_test(object sender, MouseButtonEventArgs e)
        {
            if (this.purchased != null)
            {
                FrameworkElement element = this.purchased as FrameworkElement;
                FrameworkElement canvas = element.Parent as FrameworkElement;
                Point cur_mousepos = e.GetPosition(canvas);
                Point box = this.GetElementPos(this.rectangle2);

                if ((cur_mousepos.X > box.X) && (cur_mousepos.Y > box.Y) &&
                    (cur_mousepos.X < (box.X + this.rectangle2.Width)) &&
                    (cur_mousepos.Y < (box.Y + this.rectangle2.Height)))
                {
                    this.RemoveItem(this.purchased);
                    this.purchased_drag_cancel(this.purchased);
                    return true;
                }
            }
            return false;
        }

        private void purchased_drag_cancel(object sender)
        {
            if (this.purchased != null)
            {
                if (this.purchase_poshash[this.purchased] != null)
                {
                    Point p = (Point)this.purchase_poshash[sender];
                    FrameworkElement element = this.purchased as FrameworkElement;
                    element.SetValue(Canvas.LeftProperty, p.X);
                    element.SetValue(Canvas.TopProperty, p.Y);
                }
                this.purchased = null;
            }
        }

        private void imageback_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowWhiteCastle w = WindowWhiteCastle.getParent(this);
            w.ShowMenu();
        }

        #region Mouse Events
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (ScrollViewer.IsMouseOver)
            {
                // Save starting point, used later when determining how much to scroll.
                scrollStartPoint = e.GetPosition(this);
                scrollStartOffset.X = ScrollViewer.HorizontalOffset;
                scrollStartOffset.Y = ScrollViewer.VerticalOffset;

                // Update the cursor if can scroll or not.
                this.Cursor = (ScrollViewer.ExtentWidth > ScrollViewer.ViewportWidth) ||
                    (ScrollViewer.ExtentHeight > ScrollViewer.ViewportHeight) ?
                    Cursors.ScrollAll : Cursors.Arrow;

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

                scrollTarget.X = scrollStartOffset.X + delta.X;
                scrollTarget.Y = scrollStartOffset.Y + delta.Y;

                // Scroll to the new position.
                ScrollViewer.ScrollToHorizontalOffset(scrollTarget.X);
                ScrollViewer.ScrollToVerticalOffset(scrollTarget.Y);
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
                    ScrollViewer.ScrollToHorizontalOffset(scrollTarget.X);
                    ScrollViewer.ScrollToVerticalOffset(scrollTarget.Y);
                    scrollTarget.X += velocity.X;
                    scrollTarget.Y += velocity.Y;
                    velocity *= friction;
                    move = true;
                }
            }

            /*
            if (move)
            {
                ScrollViewer.scrp

                FrameworkElement element = this.selected as FrameworkElement;
                FrameworkElement canvas = element.Parent as FrameworkElement;
                
                Point cur_mousepos = e.GetPosition(canvas);

                double newx = cur_mousepos.X - this.diff.X;
                double newy = cur_mousepos.Y - this.diff.Y;

                element.SetValue(Canvas.LeftProperty, newx);
                element.SetValue(Canvas.TopProperty, newy);
             */
        }

        public double Friction
        {
            get { return 1.0 - friction; }
            set { friction = Math.Min(Math.Max(1.0 - value, 0), 1.0); }
        }
        #endregion

    }
}
