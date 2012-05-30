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

namespace HerculesWPFMenu
{
    /// <summary>
    /// Interaction logic for UserControlMenu.xaml
    /// </summary>
    public partial class UserControlMenu : UserControl
    {

        public delegate void UserControlMenuDelegate(String option);
        public UserControlMenuDelegate evt = null;
        private int rotation = 0;
        private System.Collections.Hashtable button_hash = new System.Collections.Hashtable();

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

        public UserControlMenu()
        {
            InitializeComponent();


            BitmapSource src = WindowUtility.GetScreenBitmapWPF("menu_choose.png");
            this.image1.Source = src;

            System.Collections.ArrayList results = null;
            System.Collections.ArrayList idxs =this.PositionMenu(ref results);

            this.LoadMenuFromConfig( idxs, results );


        }

        public void ChangePos(object o, double[] pos)
        {
            FrameworkElement el = o as FrameworkElement;
            el.SetValue(Canvas.LeftProperty, pos[0]);
            el.SetValue(Canvas.TopProperty, pos[1]);
        }

        public System.Collections.ArrayList PositionMenu( ref System.Collections.ArrayList results )
        {
            System.Collections.ArrayList icons = null;

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "MenuCoords")))
            {
                String menucoords = ConfigUtility.GetConfig(ConfigUtility.Config, "MenuCoords");
                String[] pairs = menucoords.Split(new char[] { ':' });
                results = new System.Collections.ArrayList();
                icons = new System.Collections.ArrayList();
                foreach (String pair in pairs)
                {
                    String[] stuff = pair.Split(new char[] { '-' });
                    String menu= stuff[0];
                    String[] xy = stuff[1].Split(new char[] { ',' });

                    results.Add(new double[] { double.Parse(xy[0]), double.Parse(xy[1]) });
                    icons.Add(menu);
                }

                return icons;
            }
            else
            {
                return null;
            }
        }

        public int GetIconIndex( String name )
        {
            
            System.Collections.ArrayList icon_pngs = WindowUtility.GetMenuIconFilenames();

            String check = string.Format("{0}.png", name);
            for (int i=0;i< icon_pngs.Count; i++)
            {
                String icon = (String)icon_pngs[i];
                if (icon.EndsWith(check)) return i;
            }
            return -1;
        }

        public void LoadIcon( int i, Image img, 
            System.Collections.ArrayList icon_pngs,
            System.Collections.ArrayList icons,
            System.Collections.ArrayList coords_arr )
        {
                String icon = (String)icons[i];
                double[] coords = (double[])coords_arr[i];
                ChangePos(img, coords);
                int idx = GetIconIndex(icon);
                BitmapSource bm = WindowUtility.GetBitmapWPF((String)icon_pngs[idx]);
                img.Source = bm;

                this.button_hash.Add(img, icon);
        }

        public void LoadMenuFromConfig( System.Collections.ArrayList icons, 
            System.Collections.ArrayList coords_arr )
        {
            //  the menu...
            System.Collections.ArrayList lst = WindowUtility.GetMenu("server");

            //  the menu pngs..
            System.Collections.ArrayList icon_pngs = WindowUtility.GetMenuIconFilenames();

            if (icons != null)
            {
                this.LoadIcon(0, this.image2, icon_pngs, icons, coords_arr);
                this.LoadIcon(1, this.image3, icon_pngs, icons, coords_arr);
                this.LoadIcon(2, this.image4, icon_pngs, icons, coords_arr);
                this.LoadIcon(3, this.image5, icon_pngs, icons, coords_arr);
                this.LoadIcon(4, this.image6, icon_pngs, icons, coords_arr);
                this.LoadIcon(5, this.image7, icon_pngs, icons, coords_arr);
                //this.LoadIcon(6, this.image8, icon_pngs, icons, coords_arr);
            }
        }


        private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (evt != null) this.evt(null);
        }

        private void Clicked(object sender)
        {
            Image img = (Image)sender;
            String icon = "server";
            if (sender != this.image8)
            {
                icon = (String)this.button_hash[sender];
            }
            if (evt != null) this.evt(icon);
        }

        private void image5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Clicked(sender);
        }

        private void image3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Clicked(sender);
        }

        private void image4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Clicked(sender);
        }

        private void image2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Clicked(sender);
        }

        private void image6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Clicked(sender);
        }

        private void image7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Clicked(sender);
        }

        private void image8_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Clicked(sender);
        }

        public delegate void UserControlMenuToggle(int option);
        public UserControlMenuToggle tevt = null;

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
    }

    
       
}
