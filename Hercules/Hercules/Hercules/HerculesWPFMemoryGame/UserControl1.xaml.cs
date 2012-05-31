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
using MME.HerculesConfig;

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
        double top_margin = 180.0f;
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

        private System.Windows.Threading.DispatcherTimer pause = new System.Windows.Threading.DispatcherTimer();
        //private System.Threading.Timer pause = null;

        private int[,] cur_objs = new int[2, 2];

        Random random = new Random();

        bool sem = false;

        public String[][] item_paths = new String[6][] { null, null, null, null, null, null };

        private System.Collections.Hashtable bmcache = new System.Collections.Hashtable();

        public UserControl1()
        {
            InitializeComponent();

            this.LoadMenuFromConfig();

            BitmapSource src = WindowUtility.GetScreenBitmapWPF("gamebg.jpg");
            this.image2.Source = src;

        }

        public BitmapImage GetBitMap(String path)
        {
            if (path == null)
            {
                path = "memory_game.png";
            }

            BitmapImage bi = (BitmapImage)this.bmcache[path];
            if (bi == null)
            {
                //Uri uri = new Uri(path, UriKind.Absolute);
                //BitmapImage bm = new BitmapImage(uri);
                BitmapImage bm = null;
                if (path=="memory_game.png")
                {
                    bm = WindowUtility.GetScreenBitmapWPF(path);
                }
                else
                {
                    bm = WindowUtility.GetBitmapWPF(path);
                }
                this.bmcache.Add(path, bm);
                return bm;
            }
            else
            {
                return bi;
            }
        }

        public void LoadMenuFromConfig()
        {
            //  the menu...
            System.Collections.ArrayList lst = WindowUtility.GetMenu("server");

            for (int i = 0; i < lst.Count; i++)
            {
                String item = (String)lst[i];
                String[] paths = WindowUtility.GetMenuPaths(item);
                this.item_paths[i] = paths;
                //double[] costs = WindowUtility.GetMenuCosts(item);
                //this.item_cost[i] = costs;
                //this.UpdateCostHash(paths, costs);
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            double grid_y = (this.canvas_master.Height - top_margin - bottom_margin) / (num_y*1.0f);
            double grid_x = (this.canvas_master.Width - side_margin * 2.0f) / (num_x*1.0f);

            // find lesser...
            double grid_side_len = grid_x;
            if (grid_y < grid_x) grid_side_len = grid_y;

            //Uri uri = new Uri(top, UriKind.Relative);
            //BitmapImage bm = new BitmapImage(uri);
            BitmapImage bm = this.GetBitMap(null);
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
                    //uri = new Uri(top, UriKind.Relative);
                    //bm = new BitmapImage(uri);
                    bm = this.GetBitMap(null);
                    obj.Source = bm;
                    obj.MouseUp += new MouseButtonEventHandler(obj_MouseUp);

                    // Create a SolidColorBrush and use it to
                    // paint the rectangle.
                    //SolidColorBrush myBrush = new SolidColorBrush(Colors.Red);
                    //obj.Fill = myBrush;

                    images[iy, ix] = obj;

                }
            }

            if (this.pause is System.Windows.Threading.DispatcherTimer )
            {
                
                this.pause.Tick += new EventHandler(this.__timeout);
                this.pause.Interval = new TimeSpan(0, 0, 2);
                this.pause.Stop();
            }

            this.Restart();

        }

        private String ChooseRanInArr(String[] arr)
        {
            int randomNumber = random.Next(0, arr.Length);
            return arr[randomNumber];
        }

        private String ChooseRan()
        {
            //return this.sms[0];

            int randomNumber = random.Next(0, 6);
            switch (randomNumber)
            {
                case 0:
                    return ChooseRanInArr(this.item_paths[0]);
                    //return ChooseRanInArr(this.sms);
                case 1:
                    return ChooseRanInArr(this.item_paths[1]);
                    //return ChooseRanInArr(this.craves);
                case 2:
                    return ChooseRanInArr(this.item_paths[2]);
                    //return ChooseRanInArr(this.drinks);
                case 3:
                    return ChooseRanInArr(this.item_paths[3]);
                    //return ChooseRanInArr(this.bf);
                case 4:
                    return ChooseRanInArr(this.item_paths[4]);
                    //return ChooseRanInArr(this.sl);
                case 5:
                    return ChooseRanInArr(this.item_paths[5]);
                    //return ChooseRanInArr(this.sd);
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
        
        //  handler for timer in ui thread...
        void __timeout(object sender, EventArgs e )
        {
            try
            {
                write("timeout!\n");

                this.pause.Stop();

                String md = String.Format("timeout_mode{0}\n", this.mode);
                write(md);

                if (this.mode == 2)
                {
                    write("check restart\n");
                    if (this.cur_matches == (num_y * num_x))
                    {
                        this.mode = 0;
                        this.Restart();
                        return;
                    }
                }
                else if (this.mode == 3) // keep then...
                {
                    write("keep\n");
                }
                else if (this.mode == 4) // hide...
                {
                    write("hide!\n");

                    //  unpause the last cards...
                    int iy = this.cur_objs[0, 0];
                    int ix = this.cur_objs[0, 1];
                    images[iy, ix].Source = this.topbm;
                    images[iy, ix].InvalidateVisual();

                    iy = this.cur_objs[1, 0];
                    ix = this.cur_objs[1, 1];
                    images[iy, ix].Source = this.topbm;
                    images[iy, ix].InvalidateVisual();
                }

                this.mode = 0;
            }
            catch (System.Exception E)
            {
                System.Windows.MessageBox.Show(E.ToString());
            }
        }

        //  handler for thread timer...
        void _timeout(object sender)
        {
            this.pause = null;
           
            
            this.Dispatcher.Invoke(
                new System.EventHandler(this.__timeout),
                System.Windows.Threading.DispatcherPriority.Render,
                new object[] { null, null });

        }

        void write(String w)
        {
            return;

            System.IO.StreamWriter wr = new System.IO.StreamWriter("c:\\tmp\\log.txt", true);

            wr.WriteLine(w);
            wr.Flush();
            wr.Close();
        }

        void pic_click_timer(object sender)
        {
            if (!sem)
            {
                sem = true;
                int[] coord = FindIt(sender);

                //  matched already?
                if (matches[coord[0], coord[1]])
                {
                    sem = false;
                    return;
                }

                write("not mached\n");

                if (this.mode == 0)
                {
                    write("mode0\n");
                    this.cur_objs[0, 0] = coord[0];
                    this.cur_objs[0, 1] = coord[1];
                    this.mode = 1;
                }
                else if (this.mode == 1)
                {
                    write("mode1\n");

                    // can't be same as first one!
                    if ((coord[0] == this.cur_objs[0, 0]) &&
                         (coord[1] == this.cur_objs[0, 1]))
                    {
                        write("same!!\n");
                        sem = false;
                        return;
                    }

                    this.cur_objs[1, 0] = coord[0];
                    this.cur_objs[1, 1] = coord[1];

                    this.mode = 2;
                }
                else // mode==3, mode==4, we are still in pause mode...
                {
                    write("mode3 or 4\n");
                    sem = false;
                    return;
                }

                //  Show the obj...
                int iy = coord[0];
                int ix = coord[1];
                //Uri uri = new Uri(paths[iy, ix], UriKind.Relative);
                //BitmapImage bm = new BitmapImage(uri);

                BitmapImage bm = this.GetBitMap(paths[iy, ix]);
                images[iy, ix].Source = bm;
                images[iy, ix].InvalidateVisual();

                //  compare two matches...
                if (mode == 2)
                {
                    //  Check if the two match...
                    String apath = paths[this.cur_objs[0, 0], this.cur_objs[0, 1]];
                    String bpath = paths[this.cur_objs[1, 0], this.cur_objs[1, 1]];

                    String[] aparts = apath.Split( new char[] {'\\'} );
                    String[] bparts = bpath.Split( new char[] {'\\'} );

                    if ( aparts[ aparts.Length - 1 ] == bparts[ bparts.Length - 1 ] )
                    //if (apath == bpath)
                    {
                        matches[this.cur_objs[0, 0], this.cur_objs[0, 1]] = true;
                        matches[this.cur_objs[1, 0], this.cur_objs[1, 1]] = true;
                        this.cur_matches += 2;
                        if (this.cur_matches == (num_y * num_x))
                        {
                            this.mode = 2;
                            this.pause.Start(); // this will also check for end and restart game...        
                            sem = false;
                            return;
                        }
                        else
                        {
                            this.mode = 3; // a match, keep these objs displayed and go on with game...
                            this.pause.Start();
                            sem = false;
                            return;
                        }
                    }
                    else // not equal, reset the objs...
                    {
                        this.mode = 4;
                        this.pause.Start(); // pause a few seconds before they can do anything...
                        sem = false;
                        return;
                    }
                } // mode == 2

                sem = false;
            }
            else
            {
                write("sem fail\n");
            }
            
        }

        void pic_click_notimer(object sender)
        {
            try
            {
                int[] coord = FindIt(sender);

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

                    this.canvas_master.UpdateLayout();
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
                //Uri uri = new Uri(paths[iy, ix], UriKind.Relative);
                //BitmapImage bm = new BitmapImage(uri);
                BitmapImage bm = this.GetBitMap(paths[iy, ix]);
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


        void obj_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.pic_click_timer(sender);
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

            this.mode = 0;

        }

    }
}
