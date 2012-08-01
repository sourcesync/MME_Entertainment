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
using System.Runtime.InteropServices;

using MME.HerculesConfig;
namespace HerculesWFPAngryBirds
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private System.Windows.Threading.DispatcherTimer simfunc = new System.Windows.Threading.DispatcherTimer();
        private System.Windows.Threading.DispatcherTimer restart_timer = new System.Windows.Threading.DispatcherTimer();

        private static int TIMEOUT = 30;
        private System.DateTime game_time = new System.DateTime();
        private int countdown = 0;

        private Polygon catapult = null;
        private Polygon[] boxes = null;
        private Ellipse cursor = null;
        private Ellipse bullet = null;
        private bool sem = false;
        private bool simstopped = false;
        private bool moving_arm = false;
        private bool restart = false;
        private int restart_count = 0;
        private System.Collections.ArrayList items =
            new System.Collections.ArrayList();
        
        private static int NumBoxes = 21;
        private static float ScreenX = 1024.0f;
        private static float ScreenY = 768.0f;
        private static float WorldX = 200.0f;
        private static float WorldY = 200.0f * (ScreenY / ScreenX);
        private static int BULLET_OFFSET = 7;

        private Point mousepos = new Point();
        private bool mouse_down = false;
        private bool ignore_mouseup = false;
        private bool winner = false;

        [DllImport("boxengine.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int BoxEngine_Init(Int32 w);

         [DllImport("boxengine.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int fnboxengine();
        
        [DllImport("boxengine.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)] 
        public static extern void BoxEngine_GetBoxData( int idx, IntPtr data );
        
        [DllImport("boxengine.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void BoxEngine_GetCatapultData(IntPtr data);
         
        [DllImport("boxengine.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]  
        public static extern void BoxEngine_SimulationLoop();
        
        [DllImport("boxengine.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]        
        public static extern void BoxEngine_MouseMotion(Int32 x, Int32 y);
        
        [DllImport("boxengine.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]              
        public static extern int BoxEngine_Mouse(Int32 button, Int32 state, Int32 x, Int32 y);
        
        [DllImport("boxengine.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void BoxEngine_GetBulletData(IntPtr on, IntPtr data);

        [DllImport("boxengine.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int BoxEngine_Stop();

        public BitmapImage GetBitMap(String path)
        {
            BitmapImage bm = null;
            bm = WindowUtility.GetScreenBitmapWPF(path); // GetBitmapWPF(path);
            return bm;
        }

        private float[] ConvertToScreen( float[] orig)
        {
            float[] converted = new float[11];

            float posx = orig[0] * (ScreenX / WorldX) + (ScreenX / 2.0f);
            float posy = (ScreenY - (orig[1] * (ScreenY / WorldY) + (ScreenY / 2.0f)));

            float angle = orig[2] * (180.0f / 3.141f);

            float v1x = orig[3] * (ScreenX / WorldX);// +(ScreenX / 2.0f);
            float v1y = (- (orig[4] * (ScreenY / WorldY)));

            float v2x = orig[5] * (ScreenX / WorldX);// + (ScreenX / 2.0f);
            float v2y = ( - (orig[6] * (ScreenY / WorldY)));

            float v3x = orig[7] * (ScreenX / WorldX);// + (ScreenX / 2.0f);
            float v3y = (- (orig[8] * (ScreenY / WorldY)));

            float v4x = orig[9] * (ScreenX / WorldX);// + (ScreenX / 2.0f);
            float v4y = ( - (orig[10] * (ScreenY / WorldY)));

            converted[0] = posx;
            converted[1] = posy;
            converted[2] = angle;
            converted[3] = v1x;
            converted[4] = v1y;
            converted[5] = v2x;
            converted[6] = v2y;
            converted[7] = v3x;
            converted[8] = v3y;
            converted[9] = v4x;
            converted[10] = v4y;

            return converted;
        }

        private float[] GetBullet(out bool bulleton)
        {
            IntPtr onptr = Marshal.AllocHGlobal(sizeof(int));
            IntPtr dataptr = Marshal.AllocHGlobal(sizeof(float) * 3);
            BoxEngine_GetBulletData(onptr, dataptr);
            
            float[] data = new float[3];
            Marshal.Copy((IntPtr)dataptr, data, 0, 3);
            Marshal.FreeHGlobal(dataptr);

            int[] on = new int[1];
            Marshal.Copy((IntPtr)onptr, on, 0, 1);
            Marshal.FreeHGlobal(onptr);

            float[] converted = new float[3];
            float posx = data[0] * (ScreenX / WorldX) + (ScreenX / 2.0f);
            float posy = (ScreenY - (data[1] * (ScreenY / WorldY) + (ScreenY / 2.0f)));
            float angle = data[2] * (180.0f / 3.141f);
            converted[0] = posx;
            converted[1] = posy;
            converted[2] = angle;

            if (on[0] > 0) bulleton = true;
            else bulleton = false;

            return converted;
        }

        private float[] GetBox(int idx)
        {
            IntPtr dataptr = Marshal.AllocHGlobal(sizeof(float) * 11);
            BoxEngine_GetBoxData(idx, dataptr);
            float[] data = new float[11];
            Marshal.Copy((IntPtr)dataptr, data, 0, 11);
            Marshal.FreeHGlobal(dataptr);
            float[] converted = ConvertToScreen(data);
            return converted;
        }

        private float[] GetCatapult()
        {
            IntPtr dataptr = Marshal.AllocHGlobal(sizeof(float) * 11);
            BoxEngine_GetCatapultData(dataptr);
            float[] data = new float[11];
            Marshal.Copy((IntPtr)dataptr, data, 0, 11);
            Marshal.FreeHGlobal(dataptr);
            float[] converted = ConvertToScreen(data);
            return converted;
        }

        private PointCollection GetBoxPointSet(float[] data)
        {
            PointCollection p = new PointCollection() 
                {   new Point( data[3], data[4] ), 
                    new Point( data[5], data[6] ), 
                    new Point( data[7], data[8] ), 
                    new Point( data[9], data[10] ) };
            return p;
        }

        void __restart(object sender, EventArgs e)
        {
            this.restart_count++;
            if (this.restart_count == 1)
            {
                return;
            }

            this.restart_timer.Stop();

            this.Stop();

            this.Restart();
            
        }

        void Finish()
        {
            this.textBlock1.Visibility = System.Windows.Visibility.Hidden;

            if (this.winner)
                this.textBlock3.Text = "WINNER!";
            else
                this.textBlock3.Text = "Times Up!";

            this.textBlock3.Visibility = System.Windows.Visibility.Visible; // timesup

            this.textBlock2.Visibility = System.Windows.Visibility.Hidden; // instructions

            this.simstopped = true;
            this.restart_count = 0;
            this.restart_timer.Start();
            this.sem = false;
        }

        void __timeout(object sender, EventArgs e)
        {
            try
            {

                if (this.simstopped) return;

                //this.simfunc.Stop();
                if (this.sem) return;
                this.sem = true;

                System.DateTime now = System.DateTime.Now;        
                System.TimeSpan span = now.Subtract( this.game_time );
                if ( span.TotalSeconds != this.countdown )
                {
                    this.countdown = (int)span.TotalSeconds;
                    int show = TIMEOUT - this.countdown;
                    this.textBlock1.Text = show.ToString();
                    if (show <= 0)
                    {
                        this.Finish();
                         return;
                    }

                }

                BoxEngine_SimulationLoop();

                this.UpdateCatapult();

                bool done = this.UpdateBoxes();

                this.UpdateBullet();

                if (done)
                {
                    this.logomed.Source = this.GetBitMap("burger-alpha-square.png");
                    this.winner = true;
                    this.Finish();
                }

                //this.simfunc.Start();
                this.sem = false;
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        private void CreateCatapult()
        {
            Polygon p = new Polygon();
            p.Stroke = Brushes.Black;
            p.Fill = Brushes.LightBlue;
            p.StrokeThickness = 1;
            p.HorizontalAlignment = HorizontalAlignment.Left;
            p.VerticalAlignment = VerticalAlignment.Center;
            p.Points = new PointCollection() { new Point(10, 10), new Point(100, 100), new Point(10, 200) };
            this.canvas_master.Children.Add(p);
            this.items.Add(p);
            
            this.catapult = p;
            this.catapult.Visibility = System.Windows.Visibility.Hidden;

            this.spatula.Source = this.GetBitMap("spatula-transp-arrow2.png");

            this.catap.Source = this.GetBitMap("catapult-transp.png");
            this.catap.RenderTransform =
                new TranslateTransform( (double)265.0f, (double)480.0f);

            this.girl.Source = this.GetBitMap("girl.png");
            this.girl.RenderTransform = new TranslateTransform((double)160.0f, (double)470.0f);
        }

        private void CreateBoxes()
        {
            this.boxes = new Polygon[NumBoxes];
            

            for (int i = 0; i < NumBoxes; i++)
            {
                Polygon p = new Polygon();
                p.Stroke = Brushes.Black;
                p.Fill = Brushes.White;
                p.StrokeThickness = 1;
                p.HorizontalAlignment = HorizontalAlignment.Left;
                p.VerticalAlignment = VerticalAlignment.Center;
                p.Points = new PointCollection() { new Point(10, 10), new Point(100, 100), new Point(10, 200) };
                this.canvas_master.Children.Add(p);
                this.boxes[i] = p;
                this.items.Add(p);
            }

            this.logomed.Source = this.GetBitMap( "logomed-transp.png" );
            //this.logomed.Visibility = System.Windows.Visibility.Hidden;
            //this.logomed.Source = this.GetBitMap("burger-alpha-square.png");
            this.burger.Source = this.GetBitMap("burger-alpha-square.png");
            this.burger.Visibility = System.Windows.Visibility.Hidden;
        }

        private void CreateBullet()
        {
            this.bullet = new Ellipse();
            this.bullet.Height = 10;
            this.bullet.Width = 20;

            // Create a blue and a black Brush
            SolidColorBrush blueBrush = new SolidColorBrush();
            blueBrush.Color = Colors.Yellow;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;

            // Set Ellipse's width and color
            this.bullet.StrokeThickness = 4;
            this.bullet.Stroke = blackBrush;
            // Fill rectangle with blue color
            this.bullet.Fill = blueBrush;

            this.canvas_master.Children.Add(this.bullet);
            this.items.Add(this.bullet);

            this.guy.Source = this.GetBitMap("guy.png");
            this.guy.Visibility = System.Windows.Visibility.Hidden;
        }

        private void CreateCursor()
        {
            this.cursor = new Ellipse();
            this.cursor.Height = 10;
            this.cursor.Width = 20;

            // Create a blue and a black Brush
            SolidColorBrush blueBrush = new SolidColorBrush();
            blueBrush.Color = Colors.Blue;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;
           

            // Set Ellipse's width and color
            this.cursor.StrokeThickness = 4;
            this.cursor.Stroke = blackBrush;
            // Fill rectangle with blue color
            this.cursor.Fill = blueBrush;

            this.canvas_master.Children.Add(this.cursor);
            this.items.Add(this.cursor);
        }


        private void UpdateCatapult()
        {
            float[] data = GetCatapult();
            PointCollection p = GetBoxPointSet(data);
            this.catapult.Points = p;

            TransformGroup grp = new TransformGroup();
            grp.Children.Add(new RotateTransform( -data[2]));
            grp.Children.Add(new TranslateTransform(data[0], data[1]));
            this.catapult.RenderTransform = grp;
            FrameworkElement el = this.catapult as FrameworkElement;
            el.SetValue(Canvas.ZIndexProperty, 1);

            TransformGroup grp2 = new TransformGroup();
            grp2.Children.Add(new TranslateTransform(-this.spatula.Width / 2.0f, -this.spatula.Height / 2.0f));
            grp2.Children.Add(new RotateTransform(-data[2]));
            grp2.Children.Add(new TranslateTransform(data[0], data[1]));
            
            this.spatula.RenderTransform = grp2;
            FrameworkElement el2 = this.spatula as FrameworkElement;
            el2.SetValue(Canvas.ZIndexProperty, 2);
        }

        private bool UpdateBoxes()
        {
            bool done = false;

            for (int i = 0; i < NumBoxes; i++)
            {
                float[] data = GetBox(i);
                PointCollection p = GetBoxPointSet(data);
                this.boxes[i].Points = p;

                TransformGroup grp = new TransformGroup();
                grp.Children.Add(new RotateTransform(-data[2]));
                grp.Children.Add( new TranslateTransform(data[0], data[1] ) );
                this.boxes[i].RenderTransform = grp;

                FrameworkElement el = this.boxes[i] as FrameworkElement;
                el.SetValue(Canvas.ZIndexProperty, 1);

                if (i == 13) // logo
                {
                    TransformGroup grp2 = new TransformGroup();
                    grp2.Children.Add(new TranslateTransform(-this.logomed.Width / 2.0f, -this.logomed.Height / 2.0f));
                    grp2.Children.Add(new RotateTransform(-data[2]));
                    grp2.Children.Add(new TranslateTransform(data[0], data[1]));
                    this.logomed.RenderTransform = grp2;
                    FrameworkElement el2 = this.logomed as FrameworkElement;
                    el2.SetValue(Canvas.ZIndexProperty, 3);
                    this.boxes[i].Visibility = System.Windows.Visibility.Hidden;

                    if (data[1] > 380)
                    {
                        done = true;
                    }

                }
                else if (i == 21)
                {
                    TransformGroup grp2 = new TransformGroup();
                    grp2.Children.Add(new TranslateTransform(-this.burger.Width / 2.0f, -this.burger.Height / 2.0f));
                    grp2.Children.Add(new RotateTransform(-data[2]));
                    grp2.Children.Add(new TranslateTransform(data[0], data[1]));
                    this.burger.RenderTransform = grp2;
                    FrameworkElement el2 = this.burger as FrameworkElement;
                    el2.SetValue(Canvas.ZIndexProperty, 3);
                    this.boxes[i].Visibility = System.Windows.Visibility.Hidden;
                }
            }

            return done;
            
        }

        private void UpdateMouseJoint(int buttonstate)
        {
            double mousex = this.mousepos.X;
            double mousey = this.mousepos.Y;

            double mjx = ((mousex*1.0f)/ScreenX)*WorldX - WorldX/2.0f;
	        double mjy = ((mousey*1.0f)/ScreenY)*(WorldX)*(ScreenY/ScreenX) - (WorldX/2.0f)*(ScreenY/ScreenX);
	        mjy = -mjy;

            int ret = -1;

            if (buttonstate != 0)
            {
                ret = BoxEngine_Mouse(buttonstate, buttonstate, (int)mjx, (int)mjy);
            }
            else
            {
                BoxEngine_MouseMotion((int)mjx, (int)mjy);
            }

        }

        private void UpdateCursor(int buttonstate )
        {
            if (this.cursor != null)
            {
                
                this.UpdateMouseJoint(buttonstate);

                FrameworkElement el = this.cursor as FrameworkElement;
                el.SetValue(Canvas.LeftProperty, this.mousepos.X);
                el.SetValue(Canvas.TopProperty, this.mousepos.Y);

                this.cursor.Visibility = System.Windows.Visibility.Hidden;
            }

            //  Update the guy...
            if (this.moving_arm)
            {
                if ((buttonstate == 0) || (buttonstate == 1))
                {
                    this.guy.Visibility = System.Windows.Visibility.Visible;

                    TransformGroup grp = new TransformGroup();

                    double offset = (BULLET_OFFSET * 1.0f / WorldY) * ScreenY;
                    grp.Children.Add(new TranslateTransform(-this.guy.Width / 2.0f, -(this.guy.Height / 2.0f)));
                    //grp.Children.Add(new RotateTransform(data[2]));
                    grp.Children.Add(new TranslateTransform(this.mousepos.X, this.mousepos.Y - (BULLET_OFFSET/WorldY)*ScreenY));
                    this.guy.RenderTransform = grp;
                    FrameworkElement el2 = this.guy as FrameworkElement;
                    el2.SetValue(Canvas.ZIndexProperty, 3);
                }
            }
        }


        private void UpdateBullet()
        {
            bool bon = false;
            float[] data = this.GetBullet(out bon);
            if ((this.bullet != null) && (bon))
            {
                FrameworkElement el = this.bullet as FrameworkElement;
                el.Visibility = System.Windows.Visibility.Visible;
                el.SetValue(Canvas.LeftProperty, (double)data[0]);
                el.SetValue(Canvas.TopProperty, (double)data[1]);
                this.bullet.Visibility = System.Windows.Visibility.Hidden;

                //if (this.moving_arm)
                {
                }
                //else
                {
                    this.guy.Visibility = System.Windows.Visibility.Visible;
                    TransformGroup grp = new TransformGroup();
                    double offset = (BULLET_OFFSET*1.0f / WorldY) * ScreenY;
                    grp.Children.Add(new TranslateTransform(-this.guy.Width / 2.0f, -(this.guy.Height / 2.0f) ));
                    grp.Children.Add(new RotateTransform(data[2]));
                    grp.Children.Add(new TranslateTransform(data[0], data[1]));
                    this.guy.RenderTransform = grp;
                    FrameworkElement el2 = this.guy as FrameworkElement;
                    el2.SetValue(Canvas.ZIndexProperty, 3);
                }
            }
            else
            {
                FrameworkElement el = this.bullet as FrameworkElement;
                el.Visibility = System.Windows.Visibility.Hidden;
                //this.guy.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            this.mousepos = e.GetPosition(this.canvas_master);

            if ((this.moving_arm) && (this.mousepos.X < (ScreenX / 2.0f)))
            {
                this.UpdateCursor(0);
            }
            else
            {
                /*
                if (this.mouse_down)
                {
                    this.ignore_mouseup = true;

                    this.UpdateCursor(1);
                }
                 * */

            }

            base.OnPreviewMouseMove(e);
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            if ( this.ignore_mouseup)
            {
                this.ignore_mouseup = false;
                this.moving_arm = false;
                return;
            }

            this.ignore_mouseup = false;

            this.mousepos = e.GetPosition(this.canvas_master);
            this.mouse_down = false;
            if (e.ChangedButton == MouseButton.Left)
            {
                if ((this.moving_arm) && (this.mousepos.X < (ScreenX / 2.0f)))
                {

                    this.rectangle1.Visibility = System.Windows.Visibility.Hidden;
                    this.UpdateCursor(1);
                }
            }
            else
            {
            }

            base.OnPreviewMouseUp(e);


            this.moving_arm = false;
        }

        private bool InBounds()
        {
            FrameworkElement el = this.rectangle1 as FrameworkElement;
            double left = (double)el.GetValue(Canvas.LeftProperty);
            double top = (double)el.GetValue(Canvas.TopProperty);
            double width = (double)el.GetValue(Canvas.WidthProperty);
            double height = (double)el.GetValue(Canvas.HeightProperty);

            double right = left +width;
            double bottom = top + height;

            if ((this.mousepos.X > left) &&
                (this.mousepos.X < right) &&
                (this.mousepos.Y > top) &&
                (this.mousepos.Y < bottom))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            this.mousepos = e.GetPosition(this.canvas_master);

            if (!this.InBounds())
            {
                // show helper box...

                this.rectangle1.Visibility = System.Windows.Visibility.Visible;

                base.OnPreviewMouseDown(e);
                return;
            }

            //  remove helper box...
            this.rectangle1.Visibility = System.Windows.Visibility.Hidden;

            this.mouse_down = true;

            //if (e.ChangedButton == MouseButton.Left)
            {
                this.UpdateCursor(2);

                if (this.mousepos.X < (ScreenX / 2.0f))
                {
                    this.moving_arm = true;
                    this.UpdateCursor(1);
                }
                else
                {
                    this.moving_arm = false;
                }
            }

                /*
            else
            {
                this.moving_arm = false;
                this.UpdateCursor(2);
            }
                 * */

            base.OnPreviewMouseDown(e);
        }

        public void Restart()
        {
            int ret = fnboxengine();

            BoxEngine_Stop();

            BoxEngine_Init(0);

            this.Init();

            if (this.simfunc != null)
            {
                this.simfunc.Stop();
                this.simstopped = false;
                this.simfunc.Start();
            }

            if (this.restart_timer != null)
            {
                this.restart_timer.Stop();
            }

            this.countdown = 0;
            this.textBlock1.Text = TIMEOUT.ToString();
            this.textBlock1.Visibility = System.Windows.Visibility.Visible;
            this.textBlock2.Inlines.Clear();
            this.textBlock2.Inlines.Add( "Move the catapult arm to launch hungry cravers at" );
            //this.textBlock2.Inlines.Add("The cravers are hungry!  Move the catapult arm to");
            this.textBlock2.Inlines.Add(new LineBreak());

            //this.textBlock2.Inlines.Add( "launch the hungry cravers at the food.  Hurry up! " );
            this.textBlock2.Inlines.Add( "WhiteCastle.  Hurry up, you don't have much time! " );
            this.textBlock2.TextAlignment = TextAlignment.Center;
            //the WhiteCastle!

            this.textBlock2.Visibility = System.Windows.Visibility.Visible; // instructions...
            this.textBlock3.Visibility = System.Windows.Visibility.Hidden; // timesup
            this.canvas_master.UpdateLayout();

            this.game_time = System.DateTime.Now;

            this.moving_arm = false;

            if (this.mouse_down)
            this.ignore_mouseup = true;
            this.restart = true;

            this.winner = false;

            this.rectangle1.Visibility = System.Windows.Visibility.Hidden;
        }

        public void Stop()
        {
            if (this.simfunc != null)
            {
                this.simfunc.Stop();
                this.simstopped = true;
                this.simfunc = null;
               
            }

            if (this.restart_timer != null)
            {
                this.restart_timer.Stop();
                this.restart_timer = null;
            }

            BoxEngine_Stop();



            while (this.items.Count > 0)
            {
                object o = this.items[0];
                UIElement el = o as UIElement;
                this.canvas_master.Children.Remove(el);
                this.items.Remove(o);
            }

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
           
            
        }

        private void Init()
        {

            this.CreateBoxes();

            this.CreateCatapult();

            this.CreateCursor();

            this.CreateBullet();

            this.restart_timer = new System.Windows.Threading.DispatcherTimer();
            this.restart_timer.Tick += new EventHandler(this.__restart);
            this.restart_timer.Interval = new TimeSpan(5000000);
            this.restart_timer.Stop();

            this.simfunc = new System.Windows.Threading.DispatcherTimer();
            this.simfunc.Tick += new EventHandler(this.__timeout);
            this.simfunc.Interval = new TimeSpan(10000);
            this.simfunc.Stop();
            this.simstopped = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }
    }
}
