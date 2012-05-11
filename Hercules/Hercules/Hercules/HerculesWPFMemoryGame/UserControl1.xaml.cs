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

namespace HerculesWPFMemoryGame
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        int num_y = 3;
        int num_x = 6;
        private double[,,] centers = null;
        double top_margin = 130.0f;
        double bottom_margin = 30.0f;
        double side_margin = 30.0f;
        double item_margin = 10.0f;
        private Image[,] images = null;
        private String[,] paths = null;
        private bool[,] matches = null;
        private int cur_matches = 0;
        private int mode = 0;
        private String[] sms = { "/HerculesWPFMemoryGame;component/Images/sm1.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/sm2.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/sm3.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/sm4.jpg",
                                        "/HerculesWPFMemoryGame;component/Images/sm5.jpg",
                                        "/HerculesWPFMemoryGame;component/Images/sm6.jpg" };
        private String[] craves = { "/HerculesWPFMemoryGame;component/Images/50_50_Crave_Case.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/Brown-bag-left.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/Crave_Case_with_Cheese.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/menu_meal4.jpg",
                                        "/HerculesWPFMemoryGame;component/Images/Original_Crave_Case.jpg",
                                        "/HerculesWPFMemoryGame;component/Images/Variety_Crave_Case.jpg" };
        private String[] drinks = { "/HerculesWPFMemoryGame;component/Images/menu_drinks_coffee.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/menu_drinks_coke.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/menu_drinks_hot_chocolate.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/menu_drinks_shakes.jpg",
                                        "/HerculesWPFMemoryGame;component/Images/menu_drinks_tea.jpg"};
        private String[] bf = { "/HerculesWPFMemoryGame;component/Images/BaconEggCheese_Bun.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/BaconEggCheese_Toast.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/EggCheese_Bun.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/EggCheese_Toast.jpg",
                                        "/HerculesWPFMemoryGame;component/Images/FrenchToast.jpg",
                                         "/HerculesWPFMemoryGame;component/Images/HashRounds.jpg",
                                          "/HerculesWPFMemoryGame;component/Images/menu_bfast_oj.jpg",
                                           "/HerculesWPFMemoryGame;component/Images/SausageCheese_Bun.jpg",
                                            "/HerculesWPFMemoryGame;component/Images/SausageCheese_Toast.jpg",
                                             "/HerculesWPFMemoryGame;component/Images/SausageEggCheese_Bun.jpg",
                                              "/HerculesWPFMemoryGame;component/Images/SausageEggCheese_Toast.jpg"};
        private String[] sl = { "/HerculesWPFMemoryGame;component/Images/BCNCHZBRGR.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/CHKNBRSTCHZ.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/CHKNRNGCHZ.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/CHZBRGR.jpg",
                                        "/HerculesWPFMemoryGame;component/Images/DBLBCNCHZBRGR.jpg",
                                         "/HerculesWPFMemoryGame;component/Images/DBLCHZBRGR.jpg",
                                          "/HerculesWPFMemoryGame;component/Images/DBLJALCHZBRGR.jpg",
                                           "/HerculesWPFMemoryGame;component/Images/FISHCHZ.jpg",
                                            "/HerculesWPFMemoryGame;component/Images/JALCHZBRGR.jpg",
                                             "/HerculesWPFMemoryGame;component/Images/ORIG.jpg",
                                              "/HerculesWPFMemoryGame;component/Images/ORIGINALDOUBLE.jpg"};
        private String[] sd = { "/HerculesWPFMemoryGame;component/Images/menu_sides_chxnrings.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/menu_sides_chzfries.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/menu_sides_chzstx.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/menu_sides_fishnibs.jpg",
                                       "/HerculesWPFMemoryGame;component/Images/menu_sides_fries.jpg",
                                        "/HerculesWPFMemoryGame;component/Images/menu_sides_onionrings.jpg",
                                        "/HerculesWPFMemoryGame;component/Images/OnionChips.jpg" };

        private String top = "/HerculesWPFMemoryGame;component/Images/whitecastle-blue.png";

        private BitmapImage topbm = null;

        //private System.Windows.Threading.DispatcherTimer pause = null;
        private System.Threading.Timer pause = null;

        private int[,] cur_objs = new int[2, 2];

        Random random = new Random();

        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            double grid_y = (this.canvas_master.Height - top_margin - bottom_margin) / (num_y*1.0f);
            double grid_x = (this.canvas_master.Width - side_margin * 2.0f) / (num_x*1.0f);

            // find lesser...
            double grid_side_len = grid_x;
            if (grid_y < grid_x) grid_side_len = grid_y;

            Uri uri = new Uri(top, UriKind.Relative);
            BitmapImage bm = new BitmapImage(uri);
            this.topbm = bm;

            //  populate centers...
            centers = new double[num_y,num_x,2];
            images = new Image[num_y, num_x];
            matches = new bool[num_y, num_x];
            paths = new String[num_y, num_x];
            for (int iy = 0; iy < num_y; iy++)
            {
                for (int ix = 0; ix < num_x; ix++)
                {
                    double center_x = side_margin + grid_side_len * (ix + 0.5);
                    double center_y = top_margin + grid_side_len * (iy + 0.5);
                    centers[iy, ix, 0] = center_x;
                    centers[iy, ix, 1] = center_y;

                    double side_len = grid_side_len - item_margin;

                    //Rectangle obj = new Rectangle();

                    //  image size and pos...
                    Image obj = new Image();
                    obj.Width = side_len;
                    obj.Height = side_len;
                    this.canvas_master.Children.Add(obj);
                    FrameworkElement el = obj as FrameworkElement;
                    el.SetValue(Canvas.TopProperty, center_y - side_len/2.0);
                    el.SetValue(Canvas.LeftProperty, center_x - side_len/2.0);

                    //  image source...
                    //String path = this.sd[1];
                    uri = new Uri(top, UriKind.Relative);
                    bm = new BitmapImage(uri);
                    obj.Source = bm;
                    obj.MouseUp += new MouseButtonEventHandler(obj_MouseUp);

                    // Create a SolidColorBrush and use it to
                    // paint the rectangle.
                    //SolidColorBrush myBrush = new SolidColorBrush(Colors.Red);
                    //obj.Fill = myBrush;

                    images[iy, ix] = obj;

                }
            }

            /*
            this.pause = new System.Windows.Threading.DispatcherTimer();
            this.pause.
            this.pause.Tick += new EventHandler(this._timeout);
            this.pause.Interval = new TimeSpan(0, 0, 2);
              */

            
        }

        private String ChooseRanInArr(String[] arr)
        {
            int randomNumber = random.Next(0, arr.Length);
            return arr[randomNumber];
        }

        private String ChooseRan()
        {
            
            int randomNumber = random.Next(0, 6);
            switch (randomNumber)
            {
                case 0:
                    return ChooseRanInArr(this.sms);
                case 1:
                    return ChooseRanInArr(this.craves);
                case 2:
                    return ChooseRanInArr(this.drinks);
                case 3:
                    return ChooseRanInArr(this.bf);
                case 4:
                    return ChooseRanInArr(this.sl);
                case 5:
                    return ChooseRanInArr(this.sd);
            }
            return "";
        }
        

        private int[] FindIt(object o)
        {
            for (int iy = 0; iy < num_y; iy++)
            {
                for (int ix = 0; ix < num_x; ix++)
                {
                    if (images[iy, ix] == o)
                        return new int[] { iy, ix };
                }
            }
            return null;
        }
        
        //void _timeout(object sender, EventArgs e)

        void __timeout(object sender, EventArgs e )
        {

            this.mode = 0;

            if (this.cur_matches == (num_y * num_x))
            {
                this.Restart();
                return;
            }

            int iy = this.cur_objs[0, 0];
            int ix = this.cur_objs[0, 1];
            images[iy, ix].Source = this.topbm;

            iy = this.cur_objs[1, 0];
            ix = this.cur_objs[1, 1];
            images[iy, ix].Source = this.topbm;
        }

        
        void _timeout(object sender)
        {
            //this.pause.Stop();
            //System.Windows.MessageBox.Show("timeout!");
            this.pause = null;

            
            
            this.Dispatcher.Invoke(
                new System.EventHandler(this.__timeout),
                System.Windows.Threading.DispatcherPriority.Render,
                new object[] { null, null });




        }

        void obj_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int[] coord = FindIt(sender);

                //System.Windows.MessageBox.Show("a");

                //  matched already?
                if (matches[coord[0], coord[1]]) return;

                if (this.mode == 3) // make last disappear...
                {
                    int iiy = this.cur_objs[0, 0];
                    int iix = this.cur_objs[0, 1];
                    images[iiy, iix].Source = this.topbm;

                    iiy = this.cur_objs[1, 0];
                    iix = this.cur_objs[1, 1];
                    images[iiy, iix].Source = this.topbm;

                    this.mode = 0;
                }


                if (this.mode == 0)
                {
                    this.cur_objs[0, 0] = coord[0];
                    this.cur_objs[0, 1] = coord[1];
                    this.mode = 1;
                }
                else if (this.mode == 1)
                {
                    this.cur_objs[1, 0] = coord[0];
                    this.cur_objs[1, 1] = coord[1];

                    this.mode = 2;
                }

                //  Show the obj...
                int iy = coord[0];
                int ix = coord[1];
                Uri uri = new Uri(paths[iy, ix], UriKind.Relative);
                BitmapImage bm = new BitmapImage(uri);
                images[iy, ix].Source = bm;

                //  compare two matches...
                if (mode == 2)
                {
                    //  Check if the two match...
                    String apath = paths[this.cur_objs[0, 0], this.cur_objs[0, 1]];
                    String bpath = paths[this.cur_objs[1, 0], this.cur_objs[1, 1]];
                    if (apath == bpath)
                    {
                        matches[this.cur_objs[0, 0], this.cur_objs[0, 1]] = true;
                        matches[this.cur_objs[1, 0], this.cur_objs[1, 1]] = true;
                        this.cur_matches += 2;
                        if (this.cur_matches == (num_y * num_x))
                        {
                            this.canvas_master.UpdateLayout();
                            //Application.Current.Dispatcher.Invoke( System.Windows.Threading.DispatcherPriority.Normal,
                            //              new Action(delegate { }));
                            System.Threading.Thread.Sleep(1000);
                            this.Restart();
                            return;
                        }
                        else
                        {
                            this.mode = 0; // go on...
                        }
                    }
                    else
                    {
                        this.mode = 3; // make them disappear next time...
                    }

                }
            }
            catch (System.Exception E)
            {
                System.Windows.MessageBox.Show(E.ToString());
            }
        }

        public void Restart()
        {
            this.cur_matches = 0;

            //  iterate clear...
            System.Collections.ArrayList items = new System.Collections.ArrayList();
            for (int iy = 0; iy < num_y; iy++)
            {
                for (int ix = 0; ix < num_x; ix++)
                {
                    matches[iy, ix] = false;
                    paths[iy, ix] = ""; 
                    images[iy, ix].Source = this.topbm;
                    int[] pair = new int[2] { iy, ix };
                    items.Add( pair );
                }
            }

            while ( items.Count > 0 )
            {
                String path = this.ChooseRan();

                int randomNumber = random.Next(0, items.Count );
                int[] a = (int[])items[ randomNumber ];
                items.RemoveAt( randomNumber );
                this.paths[ a[0],a[1] ] = path;

                randomNumber = random.Next(0, items.Count );
                int[] b = (int[])items[ randomNumber ];
                items.RemoveAt( randomNumber );
                this.paths[ b[0],b[1] ] = path;

            }

        }

    }
}
