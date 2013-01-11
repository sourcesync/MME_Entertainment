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
using System.Windows.Media.Animation;

namespace HerculesWPFSpinTheBottle
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        //public DoubleAnimation anim = new DoubleAnimation(0.0, 360.0, new Duration(TimeSpan.FromSeconds(10)));

        //RotateTransform rt = null;
        //public bool done = false;
        //public bool timedout = false;

        //private System.Windows.Threading.DispatcherTimer pause = null;

        private float lastAngle = 0.0f;
        DoubleAnimation anim = null;

        public Boolean done = true;

        public UserControl1()
        {
            InitializeComponent();

            /*
            this.anim.RepeatBehavior = RepeatBehavior.Forever;

            TransformGroup grp = new TransformGroup();

            RotateTransform rt = new RotateTransform();

            TranslateTransform tr = new TranslateTransform(this.image3.ActualWidth / 2.0f, this.image3.ActualHeight / 2.0f);

            grp.Children.Add(rt);
            //grp.Children.Add(tr);


            this.image1.RenderTransform = grp;
            this.image1.RenderTransformOrigin = new Point(0.5, 0.5);


            //this.image4.RenderTransform = grp;
            //this.image4.RenderTransformOrigin = new Point(0.5, 0.5);
            //this.image3.RenderTransformOrigin = new Point(this.image3.ActualWidth / 2.0f, this.image3.ActualHeight / 2.0f);

            rt.BeginAnimation(RotateTransform.AngleProperty, this.anim);
            */
        }

        private void image3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!done) return;
            done = false;

            System.Random ran = new System.Random();
            int secs = ran.Next(3, 6);
            int angle = ran.Next(420, 4000);
            float from_angle = this.lastAngle;
            float to_angle = from_angle + angle;
            this.lastAngle = to_angle;

            //DoubleAnimation anim = new DoubleAnimation(0.0, 360.0, new Duration(TimeSpan.FromSeconds(10)));
            anim = new DoubleAnimation(from_angle, to_angle,
                new Duration(TimeSpan.FromSeconds(secs)));
            anim.DecelerationRatio = 0.4f;
            anim.Completed += new EventHandler(anim_Completed);


            //anim.RepeatBehavior = RepeatBehavior.Forever;

            TransformGroup grp = new TransformGroup();

            RotateTransform rt = new RotateTransform();

            TranslateTransform tr = new TranslateTransform(this.image3.ActualWidth / 2.0f, this.image3.ActualHeight / 2.0f);

            grp.Children.Add(rt);
            //grp.Children.Add(tr);


            this.image1.RenderTransform = grp;
            this.image1.RenderTransformOrigin = new Point(0.5, 0.5);


            //this.image4.RenderTransform = grp;
            //this.image4.RenderTransformOrigin = new Point(0.5, 0.5);
            //this.image3.RenderTransformOrigin = new Point(this.image3.ActualWidth / 2.0f, this.image3.ActualHeight / 2.0f);

           
            rt.BeginAnimation(RotateTransform.AngleProperty, anim);
        }

        void anim_Completed(object sender, EventArgs e)
        {
            done = true;
        }
    }
}
