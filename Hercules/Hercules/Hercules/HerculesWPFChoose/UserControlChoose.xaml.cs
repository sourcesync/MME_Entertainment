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
using MME.HerculesConfig;

namespace HerculesWPFChoose
{
    

    /// <summary>
    /// Interaction logic for UserControlChoose.xaml
    /// </summary>
    public partial class UserControlChoose : UserControl
    {

        public delegate void UserControlChooseDelegate(int option);
        public UserControlChooseDelegate evt = null;

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
        public int mode = 0; // buying...
        private double total = 0.0;

        //  remember last position of checkout controls...
        private System.Collections.Hashtable checkout_loc = new System.Collections.Hashtable();

        //  template structures...
        private System.Collections.Hashtable purchase_poshash = new System.Collections.Hashtable();
        private System.Collections.Hashtable poshash = new System.Collections.Hashtable();
        private System.Collections.ArrayList imgs = new System.Collections.ArrayList();
        private System.Collections.ArrayList display_cart = new System.Collections.ArrayList();
        private System.Collections.Hashtable cost = new System.Collections.Hashtable();

        private System.Collections.ArrayList cart = new System.Collections.ArrayList();

        private System.Collections.Hashtable des_hash = new System.Collections.Hashtable();

        //  specific menus...

        //  sm...
        private String[] sms = { "/HerculesWPFChoose;component/Images/sm1.jpg",
                                       "/HerculesWPFChoose;component/Images/sm2.jpg",
                                       "/HerculesWPFChoose;component/Images/sm3.jpg",
                                       "/HerculesWPFChoose;component/Images/sm4.jpg",
                                        "/HerculesWPFChoose;component/Images/sm5.jpg",
                                        "/HerculesWPFChoose;component/Images/sm6.jpg" };
        private double[] smcost = { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

        private String[] craves = { "/HerculesWPFChoose;component/Images/50_50_Crave_Case.jpg",
                                       "/HerculesWPFChoose;component/Images/Brown-bag-left.jpg",
                                       "/HerculesWPFChoose;component/Images/Crave_Case_with_Cheese.jpg",
                                       "/HerculesWPFChoose;component/Images/menu_meal4.jpg",
                                        "/HerculesWPFChoose;component/Images/Original_Crave_Case.jpg",
                                        "/HerculesWPFChoose;component/Images/Variety_Crave_Case.jpg" };
        private double[] crave_cost = { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

        private String[] drinks = { "/HerculesWPFChoose;component/Images/menu_drinks_coffee.jpg",
                                       "/HerculesWPFChoose;component/Images/menu_drinks_coke.jpg",
                                       "/HerculesWPFChoose;component/Images/menu_drinks_hot_chocolate.jpg",
                                       "/HerculesWPFChoose;component/Images/menu_drinks_shakes.jpg",
                                        "/HerculesWPFChoose;component/Images/menu_drinks_tea.jpg"};
        private double[] drink_cost = { 1.0, 1.0, 1.0, 1.0, 1.0 };

        private String[] bf = { "/HerculesWPFChoose;component/Images/BaconEggCheese_Bun.jpg",
                                       "/HerculesWPFChoose;component/Images/BaconEggCheese_Toast.jpg",
                                       "/HerculesWPFChoose;component/Images/EggCheese_Bun.jpg",
                                       "/HerculesWPFChoose;component/Images/EggCheese_Toast.jpg",
                                        "/HerculesWPFChoose;component/Images/FrenchToast.jpg",
                                         "/HerculesWPFChoose;component/Images/HashRounds.jpg",
                                          "/HerculesWPFChoose;component/Images/menu_bfast_oj.jpg",
                                           "/HerculesWPFChoose;component/Images/SausageCheese_Bun.jpg",
                                            "/HerculesWPFChoose;component/Images/SausageCheese_Toast.jpg",
                                             "/HerculesWPFChoose;component/Images/SausageEggCheese_Bun.jpg",
                                              "/HerculesWPFChoose;component/Images/SausageEggCheese_Toast.jpg"};
        private double[] bf_cost = { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

        private String[] sl = { "/HerculesWPFChoose;component/Images/BCNCHZBRGR.jpg",
                                       "/HerculesWPFChoose;component/Images/CHKNBRSTCHZ.jpg",
                                       "/HerculesWPFChoose;component/Images/CHKNRNGCHZ.jpg",
                                       "/HerculesWPFChoose;component/Images/CHZBRGR.jpg",
                                        "/HerculesWPFChoose;component/Images/DBLBCNCHZBRGR.jpg",
                                         "/HerculesWPFChoose;component/Images/DBLCHZBRGR.jpg",
                                          "/HerculesWPFChoose;component/Images/DBLJALCHZBRGR.jpg",
                                           "/HerculesWPFChoose;component/Images/FISHCHZ.jpg",
                                            "/HerculesWPFChoose;component/Images/JALCHZBRGR.jpg",
                                             "/HerculesWPFChoose;component/Images/ORIG.jpg",
                                              "/HerculesWPFChoose;component/Images/ORIGINALDOUBLE.jpg"};
        private double[] sl_cost = { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

        private String[] sd = { "/HerculesWPFChoose;component/Images/menu_sides_chxnrings.jpg",
                                       "/HerculesWPFChoose;component/Images/menu_sides_chzfries.jpg",
                                       "/HerculesWPFChoose;component/Images/menu_sides_chzstx.jpg",
                                       "/HerculesWPFChoose;component/Images/menu_sides_fishnibs.jpg",
                                       "/HerculesWPFChoose;component/Images/menu_sides_fries.jpg",
                                        "/HerculesWPFChoose;component/Images/menu_sides_onionrings.jpg",
                                        "/HerculesWPFChoose;component/Images/OnionChips.jpg" };
        private double[] sd_cost = { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

        private int rotation = 0;

        public String[][] item_paths = new String[6][] {null,null,null,null,null,null};
        public double[][] item_cost = new double[6][] { null, null, null, null, null, null };

        public System.Collections.Hashtable bmcache = new System.Collections.Hashtable();

        private bool semaphore = false;

        public void SetRotation(int i)
        {
            this.rotation = i;
            if (i == 0)
            {
                this.RenderTransform = new RotateTransform(0, 1024 / 2.0, 768 / 2.0);
            }
            else
            {
                this.RenderTransform = new RotateTransform(180, 1024 / 2.0, 768 / 2.0);
            }
        }

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

           


            //  white castle specific...
            this.item_paths[0] = this.sms;
            this.item_paths[1] = this.craves;
            this.item_paths[2] = this.drinks;
            this.item_paths[3] = this.bf;
            this.item_paths[4] = this.sd;
            this.item_paths[5] = this.sl;


            //
            //  load from config...
            //
            this.LoadMenuFromConfig();

            //  Specific menus...
            //this.UpdateCostHash(this.sms, this.smcost);
            //this.UpdateCostHash(this.craves, this.crave_cost);
            //this.UpdateCostHash(this.drinks, this.drink_cost);
            //this.UpdateCostHash(this.bf, this.bf_cost);
            //this.UpdateCostHash(this.sl, this.sl_cost);
            //this.UpdateCostHash(this.sd, this.sd_cost);

            //this.UpdateCostHash(this.item_paths[0], this.smcost);
            //this.UpdateCostHash(this.item_paths[1], this.crave_cost);
            //this.UpdateCostHash(this.item_paths[2], this.drink_cost);
            //this.UpdateCostHash(this.item_paths[3], this.bf_cost);
            //this.UpdateCostHash(this.item_paths[4], this.sl_cost);
            //this.UpdateCostHash(this.item_paths[5], this.sl_cost);

            //  Initial menu...
            //this.SetOption(0);
            this.cur_option = 0;

            friction = 0.95;

            animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            animationTimer.Tick += new EventHandler(HandleWorldTimerTick);
            animationTimer.Start();

            ScrollViewer.Visibility = System.Windows.Visibility.Hidden;
            //ScrollViewer.ScrollChanged += new ScrollChangedEventHandler(ScrollViewer_ScrollChanged);


            this.HashLoc(this.checkout_loc, this.textBox1);
            this.HashLoc(this.checkout_loc, this.textBox3);
            this.HashLoc(this.checkout_loc, this.textBox99);
            this.HashLoc(this.checkout_loc, this.textBoxSubTotal);
            this.HashLoc(this.checkout_loc, this.textBoxTax);
            this.HashLoc(this.checkout_loc, this.textBoxTotalAmount);


            BitmapSource src = WindowUtility.GetScreenBitmapWPF("menu_select.png");
            this.image1.Source = src;

            src = WindowUtility.GetScreenBitmapWPF("checkout_bg.png");
            this.imagecheckout.Source = src;

            this.des_hash["bellapesca.jpg"] = "Bella Pesca- Champagne, Elderflower Liqueur, \nGrapefruit & Lychee Juices, Splash of Chambord $19";
            this.des_hash["ayala.jpg"] = "Rosé Champagne - Ayala, \"Rosé Majeur,\" Aÿ NV $30";
            this.des_hash["amstel.jpg"] = "Amstel Light, Holland $9";
            this.des_hash["bahnmi.jpg"] = "Vietnamese Banh Mi Sandwich $15";
                    ///French Bread, Country Pâté and Char Su Pork, Pickled Daikon";
            this.des_hash["applepie.jpg"] = "Warm Apple Pie - Tahitian Chantilly Crème, Caramel Sauce  $12";
            this.des_hash["einbacher-beer.jpg"] = "Einbecker Beer, \"Brauherren Alkoholfrei,\" $9";

           // this.label1.N
        }

        void write(String w)
        {
            return;

            System.IO.StreamWriter wr = new System.IO.StreamWriter("c:\\tmp\\log.txt", true);

            wr.WriteLine(w);
            wr.Flush();
            wr.Close();
        }

        public void LoadMenuFromConfig()
        {
            //  the menu...
            System.Collections.ArrayList lst = WindowUtility.GetMenu("server");

            for ( int i=0;i< lst.Count; i++ )
            {
                String item = (String)lst[i];
                String[] paths = WindowUtility.GetMenuPaths( item );
                this.item_paths[i] = paths;
                double[] costs = WindowUtility.GetMenuCosts( item );
                this.item_cost[i] = costs;

                this.UpdateCostHash( paths, costs );
            }

        }

        public void HashLoc( System.Collections.Hashtable hash, System.Windows.Controls.TextBox o )
        {
            double x = Canvas.GetLeft(o);
            double y = Canvas.GetTop(o);
            hash[o] = new double[] {x,y};
        }

        public void RestoreHashLoc(System.Collections.Hashtable hash, object o)
        {
            double[] loc = (double[])this.checkout_loc[o];
            FrameworkElement el = o as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, loc[0]);
            el.SetValue(Canvas.TopProperty, loc[1]);
        }

        public void RestoreCheckoutControls()
        {
            this.RestoreHashLoc(this.checkout_loc, this.textBox1);
            this.RestoreHashLoc(this.checkout_loc, this.textBox3);
            this.RestoreHashLoc(this.checkout_loc, this.textBox99);
            this.RestoreHashLoc(this.checkout_loc, this.textBoxSubTotal);
            this.RestoreHashLoc(this.checkout_loc, this.textBoxTax);
            this.RestoreHashLoc(this.checkout_loc, this.textBoxTotalAmount);
        }

        public void OffsetControl(object o, double offset)
        {
            FrameworkElement el = o as FrameworkElement;
            double x = (double)el.GetValue(Canvas.LeftProperty);

            x += offset;
            el.SetValue(Canvas.LeftProperty, x);
        }

        public void Restart()
        {
            this.canvas_checkout.Visibility = System.Windows.Visibility.Hidden;
            this.canvas_choose.Visibility = System.Windows.Visibility.Visible;

            this.textBox1.Foreground = new SolidColorBrush(Colors.Yellow);
            this.textBox3.Foreground = new SolidColorBrush(Colors.Yellow);
            this.textBox99.Foreground = new SolidColorBrush(Colors.Yellow);
            this.textBoxSubTotal.Foreground = new SolidColorBrush(Colors.Yellow);
            this.textBoxTax.Foreground = new SolidColorBrush(Colors.Yellow);
            this.textBoxTotalAmount.Foreground = new SolidColorBrush(Colors.Yellow);
            this.RestoreCheckoutControls();

            foreach (Image img in this.imgs)
            {
                img.Visibility = System.Windows.Visibility.Visible;
            }
            foreach (Image img in this.display_cart)
            {
                img.Visibility = System.Windows.Visibility.Hidden;
            }

            this.textBoxItems.Visibility = System.Windows.Visibility.Hidden;
            this.labelPay.Visibility = System.Windows.Visibility.Hidden;
            this.labelMessage.Visibility = System.Windows.Visibility.Hidden;

            this.cart.Clear();
            this.total = 0.0;
            this.mode = 0;

            this.redraw_cart();
            this.update_cost();
        }

        private void UpdateCostHash(String[] paths, double[] cst)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                this.cost[paths[i]] = cst[i];
            }
        }

        /*
        private String[] GetOptionSource(int option)
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
         * */
        private String[] GetOptionSource(int option)
        {
            return this.item_paths[option];
        }

        public BitmapImage GetBitMap(String path)
        {
            BitmapImage bi = (BitmapImage)this.bmcache[path];
            if (bi == null)
            {
                //Uri uri = new Uri(path, UriKind.Absolute);
                //BitmapImage bm = new BitmapImage(uri);

                BitmapImage bm = WindowUtility.GetBitmapWPF(path);

                this.bmcache.Add(path, bm);
                return bm;
            }
            else
            {
                return bi;
            }
        }


        public int GetIconIndex(String name)
        {

            System.Collections.ArrayList icon_pngs = WindowUtility.GetMenuIconFilenames();

            String check = string.Format("{0}.png", name);
            for (int i = 0; i < icon_pngs.Count; i++)
            {
                String icon = (String)icon_pngs[i];
                if (icon.EndsWith(check)) return i;
            }
            return -1;
        }

        public void SetOption(String option)
        {
            this.cur_option = GetIconIndex(option);

            String[] src = this.GetOptionSource(this.cur_option);

            int max = src.Length;
            if (max > MAX) max = MAX;

            for (int i = 0; i < max; i++)
            {
                Image img = (Image)this.imgs[i];
                img.Visibility = System.Windows.Visibility.Visible;
                String path = (String)src[i];
                //Uri uri = new Uri(path, UriKind.Relative);
                //BitmapImage bm = new BitmapImage(uri);
                BitmapImage bm = this.GetBitMap(path);
                if (img.Source != null) img.Source = null;
                img.Source = bm;
            }
            for (int i = src.Length; i < MAX; i++)
            {
                Image img = (Image)this.imgs[i];
                img.Visibility = System.Windows.Visibility.Hidden;
            }

            this.label1.Visibility = System.Windows.Visibility.Hidden;

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
            write("mouse down! - " + sender.ToString());

            if (this.selected!=null) return;

            Image img = (Image)sender;
            this.selected = img; // this.image2;
            FrameworkElement element = this.selected as FrameworkElement;
            FrameworkElement canvas = element.Parent as FrameworkElement;
            Point pos = e.GetPosition(canvas);

            double ix = (double)element.GetValue(Canvas.LeftProperty);
            double iy = (double)element.GetValue(Canvas.TopProperty);

            this.diff.X = pos.X - ix;
            this.diff.Y = pos.Y - iy;

            String path = String.Empty;
            if (sender == this.image2)
            {
                path = this.item_paths[this.cur_option][0];
            }
            else if (sender == this.image3)
            {
                path = this.item_paths[this.cur_option][1];
            }
            else if (sender == this.image4)
            {
                path = this.item_paths[this.cur_option][2];
            }
            else if (sender == this.image5)
            {
                path = this.item_paths[this.cur_option][3];
            }
            else if (sender == this.image6)
            {
                path = this.item_paths[this.cur_option][4];
            }
            else if (sender == this.image7)
            {
                path = this.item_paths[this.cur_option][5];
            }

            this.label1.Visibility = System.Windows.Visibility.Hidden;
            if (path != String.Empty)
            {
                String[] parts = path.Split(new char[] { '\\' });
                String fname = parts[parts.Length - 1];
                if (this.des_hash[fname]!=null)
                {
                    this.label1.Visibility = System.Windows.Visibility.Visible;
                    this.label1.Content = this.des_hash[fname];
                    
                }
            }

            
        }


        private void image_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.selected != null)
            {
                write("mouse move selected! - " + sender.ToString());

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
           // WindowWhiteCastle w = WindowWhiteCastle.getParent(this);
            double subtotal = 0.0;
            for (int i = 0; i < cart.Count; i++)
            {
                String item = (String)cart[i];
                double cst = (double)this.cost[item];
                subtotal += cst;
            }

            double tax = subtotal * 0.08;

            this.total = subtotal + tax;

            this.textBoxSubTotal.Text = String.Format("{0:0.00}", subtotal);
            this.textBoxTax.Text = String.Format("{0:0.00}", tax);
            this.textBoxTotalAmount.Text = String.Format("{0:0.00}", total);
        }

        private void redraw_cart()
        {
            //WindowWhiteCastle w = WindowWhiteCastle.getParent(this);
            for (int i = 0; i < cart.Count; i++)
            {
                Image timg = (Image)this.display_cart[i];
                String path = (String)cart[i];
                //Uri uri = new Uri(path, UriKind.Relative);
                //BitmapImage bm = new BitmapImage(uri);
                BitmapImage bm = this.GetBitMap(path);
                if (timg.Source != null) timg.Source = null;
                timg.Source = bm;
                timg.Visibility = System.Windows.Visibility.Visible;
            }

            for (int i = cart.Count; i < this.display_cart.Count; i++)
            {
                Image timg = (Image)this.display_cart[i];
                timg.Visibility = System.Windows.Visibility.Hidden;
            }

            String txt = String.Format("{0} Items In Your Cart", cart.Count);
            this.textBoxItems.Text = txt;
        }

        private void buy(object o)
        {
            //WindowWhiteCastle w = WindowWhiteCastle.getParent(this);
            if (cart.Count == this.display_cart.Count)
            {
                cart.RemoveAt(0);
            }
            int idx = this.imgs.IndexOf(o);
            String[] src = this.GetOptionSource(this.cur_option);
            String path = (String)src[idx];
            cart.Add(path);

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

                    write("shop test passed! - " + sender.ToString());
                    return true;
                }
                else
                {

                    write("shop test failed inside1! - " + sender.ToString());
                    return false;
                }
            }
            else
            {
                write("shop test failed inside2! - " + sender.ToString());
                return false;
            }
        }

        private void drag_cancel(object sender)
        {
            if (this.selected != null)
            {
                if (this.poshash[this.selected] != null)
                {
                    Point p = (Point)this.poshash[this.selected];
                    FrameworkElement element = this.selected as FrameworkElement;
                    element.SetValue(Canvas.LeftProperty, p.X);
                    element.SetValue(Canvas.TopProperty, p.Y);
                }
                this.selected = null;
            }

            this.label1.Visibility = System.Windows.Visibility.Hidden;
        }


        private void image_MouseUp(object sender, MouseButtonEventArgs e)
        {

            write("mouse up! - " + sender.ToString());

            if (this.selected != null)
            {
                if (!this.shop_test(sender, e))
                {
                    write("shop test failed! - " + sender.ToString());
                    this.drag_cancel(sender);
                }
            }
        }

        private void image_MouseLeave(object sender, MouseEventArgs e)
        {
            this.drag_cancel(sender);
        }


        private void RemoveItem(object sender)
        {
            int idx = this.display_cart.IndexOf(this.purchased);

            //WindowWhiteCastle w = WindowWhiteCastle.getParent(this);
            cart.RemoveAt(idx);

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

            this.label1.Visibility = System.Windows.Visibility.Hidden;

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
            //WindowWhiteCastle w = WindowWhiteCastle.getParent(this);
            //w.ShowMenu();
            if (this.evt != null) this.evt(0);
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
            //bool move = false;
            if (IsMouseCaptured)
            {
                Point currentPoint = Mouse.GetPosition(this);
                velocity = previousPoint - currentPoint;
                previousPoint = currentPoint;
                //move = true;
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
                    //move = true;
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


        public delegate void UserControlChooseToggle(int option);
        public UserControlChooseToggle tevt = null;

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

        private void imageCheckout_MouseUp(object sender, MouseButtonEventArgs e)
        {
            double offset = -250.0;
            this.OffsetControl(this.textBox1, offset);
            this.OffsetControl(this.textBox3, offset);
            this.OffsetControl(this.textBox99, offset);
            this.OffsetControl(this.textBoxSubTotal, offset);
            this.OffsetControl(this.textBoxTax, offset);
            this.OffsetControl(textBoxTotalAmount, offset);

            this.textBox1.Foreground = new SolidColorBrush(Colors.Blue);
                    this.textBox3.Foreground = new SolidColorBrush(Colors.Blue);
                    this.textBox99.Foreground = new SolidColorBrush(Colors.Blue);
                    this.textBoxSubTotal.Foreground = new SolidColorBrush(Colors.Blue);
                    this.textBoxTax.Foreground = new SolidColorBrush(Colors.Blue);
                    this.textBoxTotalAmount.Foreground = new SolidColorBrush(Colors.Blue);

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "CheckoutTextColor")))
            {
                String color = ConfigUtility.GetConfig(ConfigUtility.Config, "CheckoutTextColor");
                if (color=="yellow")
                {
                    this.textBox1.Foreground = new SolidColorBrush(Colors.Yellow);
                    this.textBox3.Foreground = new SolidColorBrush(Colors.Yellow);
                    this.textBox99.Foreground = new SolidColorBrush(Colors.Yellow);
                    this.textBoxSubTotal.Foreground = new SolidColorBrush(Colors.Yellow);
                    this.textBoxTax.Foreground = new SolidColorBrush(Colors.Yellow);
                    this.textBoxTotalAmount.Foreground = new SolidColorBrush(Colors.Yellow);
                }
            }

            this.canvas_checkout.Visibility = System.Windows.Visibility.Visible;
            this.canvas_choose.Visibility = System.Windows.Visibility.Hidden;

            this.mode = 1; // checkout...

            foreach ( Image img in this.imgs )
            {
                img.Visibility = System.Windows.Visibility.Hidden;
            }
            foreach (Image img in this.display_cart) 
            {
                img.Visibility = System.Windows.Visibility.Hidden;
            }

            this.textBoxItems.Visibility = System.Windows.Visibility.Hidden;
            this.labelPay.Visibility = System.Windows.Visibility.Visible;
            this.labelMessage.Visibility = System.Windows.Visibility.Visible;

            this.labelPay.Content =
                String.Format("Your Total Is ${0:0.00}", this.total);
            this.labelMessage.Content = "Please Pay And Pick Up Your Order Now.";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.label1.Visibility = System.Windows.Visibility.Hidden;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.selected != null)
            {
               // write("mouse move selected! - " + sender.ToString());

                FrameworkElement element = this.selected as FrameworkElement;
                FrameworkElement canvas = element.Parent as FrameworkElement;

                Point cur_mousepos = e.GetPosition(canvas);

                double newx = cur_mousepos.X - this.diff.X;
                double newy = cur_mousepos.Y - this.diff.Y;

                element.SetValue(Canvas.LeftProperty, newx);
                element.SetValue(Canvas.TopProperty, newy);
            }
        }

        private void label1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.selected != null)
            {
                if (!this.shop_test(sender, e))
                {
                    write("shop test failed! - " + sender.ToString());
                    this.drag_cancel(sender);
                }
            }
        }
    }
}
