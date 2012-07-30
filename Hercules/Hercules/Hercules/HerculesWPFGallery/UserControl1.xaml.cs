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
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Diagnostics;
using System.Windows.Media.Imaging;

using MME.HerculesConfig;

namespace HerculesWPFGallery
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {

        System.Collections.ArrayList lst = new System.Collections.ArrayList();
        int counter = 0;

        private System.Windows.Threading.DispatcherTimer pause = new System.Windows.Threading.DispatcherTimer();
        private bool ok = false;

        public UserControl1()
        {
            InitializeComponent();
        }

        public void Start()
        {
            
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "GalleryDir")))
            {
                String dir = ConfigUtility.GetConfig(ConfigUtility.Config, "GalleryDir");

                foreach (String fl in Directory.EnumerateFiles(dir))
                {
                    String lw = fl.ToLower();


                    if (lw.StartsWith(".")) continue;

                    String[] parts = lw.Split(new char[] { '\\' });
                    String fname = parts[parts.Length - 1];
                    if (fname.StartsWith(".")) continue;


                    if (lw.EndsWith("jpg") || lw.EndsWith("png"))
                    {
                        lst.Add(fl);
                    }
                }
            }

            this.counter = 0;

            if (lst.Count > 0)
            {
                ok = true;
                ShowNext();
               
            }

        }

        public void Stop()
        {
            if (this.pause != null)
                this.pause.Stop();
            ok = false;
        }

        //  handler for timer in ui thread...
        void __timeout(object sender, EventArgs e)
        {
            try
            {

                this.pause.Stop();

                this.ShowNext();
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

        public void ShowNext()

            
        {

            String path = (String)lst[this.counter];
            try
            {
                BitmapImage img = WindowUtility.GetBitmapWPF(path);
                if (this.image2.Source != null)
                {
                    this.image2.Source = null;
                }

                this.image2.Source = img;

                this.image2.Stretch = Stretch.Uniform;
                this.image2.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                this.image2.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

                double w = img.PixelWidth;
                double h = img.PixelHeight;
                double offy = (768 - h) / 2.0f;
                double offx = (1024 - w) / 2.0f;

                FrameworkElement el = this.image2 as FrameworkElement;
                //el.SetValue(Canvas.TopProperty, offy);
                //el.SetValue(Canvas.LeftProperty, offx);



            }
            catch (System.Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
                System.Windows.MessageBox.Show(path);
            }

            this.counter++;
            if (this.counter == lst.Count) this.counter = 0;

            if ( ok && (this.pause is System.Windows.Threading.DispatcherTimer) )
            {
                this.pause = new System.Windows.Threading.DispatcherTimer();
                this.pause.Tick += new EventHandler(this.__timeout);
                this.pause.Interval = new TimeSpan(0, 0, 2);
                this.pause.Start();
                //this.pause.Stop();
            }

        }

        private void image2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //this.ShowNext();
        }

    }
}
