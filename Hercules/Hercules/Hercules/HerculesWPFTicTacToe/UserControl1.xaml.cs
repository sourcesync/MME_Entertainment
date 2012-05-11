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

namespace HerculesWPFTicTacToe
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        int num_y = 3;
        int num_x = 3;
        private double[, ,] centers = null;
        double top_margin = 130.0f;
        double bottom_margin = 30.0f;
        double side_margin = 30.0f;
        double item_margin = 10.0f;
        private Image[,] images = null;
        private Rectangle[,] rects = null;
        //private String[,] paths = null;
        private int[,] xo = null;
        private int cur_show = 0;
        private int mode = 0;
        double grid_side_len;
        private double offsetx = 200;
        System.Collections.ArrayList renders = new System.Collections.ArrayList();

        SolidColorBrush myRedBrush = new SolidColorBrush(Colors.Red);
        SolidColorBrush myYellowBrush = new SolidColorBrush(Colors.Yellow);
        SolidColorBrush myGreenBrush = new SolidColorBrush(Colors.Green);
        SolidColorBrush myTransparentBrush = new SolidColorBrush(Colors.Transparent);
        SolidColorBrush myBlackBrush = new SolidColorBrush(Colors.Black);

        private Board board = null;

        private System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            double grid_y = (this.canvas_master.Height - top_margin - bottom_margin) / (num_y * 1.0f);
            double grid_x = (this.canvas_master.Width - side_margin * 2.0f) / (num_x * 1.0f);

            // find lesser...
            this.grid_side_len = grid_x;
            if (grid_y < grid_x) grid_side_len = grid_y;

            //Uri uri = new Uri(top, UriKind.Relative);
            //BitmapImage bm = new BitmapImage(uri);
            //this.topbm = bm;

            //  populate centers...
            centers = new double[num_y, num_x, 2];
            rects = new Rectangle[num_y, num_x];
            images = new Image[num_y, num_x];
            xo = new int[num_y, num_x];
            //paths = new String[num_y, num_x];
            for (int iy = 0; iy < num_y; iy++)
            {
                for (int ix = 0; ix < num_x; ix++)
                {
                    double center_x = side_margin + grid_side_len * (ix + 0.5) + offsetx ;
                    double center_y = top_margin + grid_side_len * (iy + 0.5);
                    centers[iy, ix, 0] = center_x;
                    centers[iy, ix, 1] = center_y;

                    double side_len = grid_side_len - item_margin;

                    //Rectangle obj = new Rectangle();

                    //  image size and pos...
                    
                    Image obj = new Image();
                    obj.Width = side_len;
                    obj.Height = side_len;
                    //this.canvas_master.Children.Add(obj);
                    FrameworkElement el = obj as FrameworkElement;
                    el.SetValue(Canvas.TopProperty, center_y - side_len / 2.0);
                    el.SetValue(Canvas.LeftProperty, center_x - side_len / 2.0);
                    images[iy, ix] = obj;
                    obj.MouseUp += new MouseButtonEventHandler(obj_MouseUp);
                    

                    //Rectangle obj = new Rectangle();
                    Rectangle obj2 = new Rectangle();
                    obj2.Width = side_len;
                    obj2.Height = side_len;
                    this.canvas_master.Children.Add(obj2);
                    el = obj2 as FrameworkElement;
                    el.SetValue(Canvas.TopProperty, center_y - side_len / 2.0);
                    el.SetValue(Canvas.LeftProperty, center_x - side_len / 2.0);
                    obj2.Fill = myTransparentBrush;
                    rects[iy, ix] = obj2;
                    obj2.MouseUp += new MouseButtonEventHandler(obj_MouseUp);

                    //  image source...
                    //String path = this.sd[1];
                    //uri = new Uri(top, UriKind.Relative);
                    //bm = new BitmapImage(uri);
                    //obj.Source = bm;

                    // Create a SolidColorBrush and use it to
                    // paint the rectangle.
                    //SolidColorBrush myBrush = new SolidColorBrush(Colors.Red);
                    //obj.Fill = myBrush;

                    

                }
            }

            /*
            if (this.pause is System.Windows.Threading.DispatcherTimer)
            {

                this.pause.Tick += new EventHandler(this.__timeout);
                this.pause.Interval = new TimeSpan(0, 0, 2);
            }
            */

           
            Line l = new Line();
            l.X1 = side_margin + offsetx;
            l.Y1 = top_margin + grid_side_len;
            l.X2 = side_margin + grid_side_len * 3.0 + offsetx;
            l.Y2 = top_margin + grid_side_len;
            l.StrokeThickness = 5;
            l.Stroke = this.myBlackBrush;
            this.canvas_master.Children.Add( l );

            l = new Line();
            l.X1 = side_margin + offsetx;
            l.Y1 = top_margin + grid_side_len*2;
            l.X2 = side_margin + grid_side_len * 3.0 + offsetx;
            l.Y2 = top_margin + grid_side_len*2;
            l.StrokeThickness = 5;
            l.Stroke = this.myBlackBrush;
            this.canvas_master.Children.Add(l);

            l = new Line();
            l.X1 = side_margin + grid_side_len + offsetx;
            l.Y1 = top_margin;
            l.X2 = side_margin + grid_side_len + offsetx;
            l.Y2 = top_margin + grid_side_len * 3;
            l.StrokeThickness = 5;
            l.Stroke = this.myBlackBrush;
            this.canvas_master.Children.Add(l);

            l = new Line();
            l.X1 = side_margin + grid_side_len * 2 + offsetx;
            l.Y1 = top_margin;
            l.X2 = side_margin + grid_side_len * 2 + offsetx;
            l.Y2 = top_margin + grid_side_len * 3;
            l.StrokeThickness = 5;
            l.Stroke = this.myBlackBrush;
            this.canvas_master.Children.Add(l);

            this.timer.Tick += new EventHandler(timer_Tick);
            this.timer.Interval = new TimeSpan(0, 0, 1);
            this.timer.Stop();

            this.board = new Board(num_y);

            this.Restart();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.Restart();
        }


        private int[] FindIt(object o)
        {
            for (int iy = 0; iy < num_y; iy++)
            {
                for (int ix = 0; ix < num_x; ix++)
                {
                    if (rects[iy, ix] == o)
                        return new int[] { iy, ix };
                }
            }
            return null;
        }

        public void Restart()
        {
            this.cur_show = 0;

            while ( this.renders.Count>0 )
            {
                object o = this.renders[0];
                System.Windows.UIElement el = o as System.Windows.UIElement;
                this.canvas_master.Children.Remove(el);
                this.renders.RemoveAt(0);
            }

            //  iterate clear...
            
            for (int iy = 0; iy < num_y; iy++)
            {
                for (int ix = 0; ix < num_x; ix++)
                {
                    xo[iy, ix] = 0;
                    rects[iy, ix].Fill =myTransparentBrush;
                }
            }

            this.mode = 1;

            this.board = new Board(num_y);
        }

        private void _restart(object o, EventArgs evt)
        {
            this.Restart();
        }

        //Apply a move to the GameBoard and the appropriate SquareControl
        private void MakeMove( Move move)
        {
            if (board.aiBoard[move.iCol, move.iRow] != Board.Empty)
                return;

            int CurrentPlayer = this.mode;

            //make move on the Board
            board.MakeMove(CurrentPlayer, move);

            /*
            //update SquareControls
            if (CurrentPlayer == TicTacToe.Computer)
                aSquares[move.iCol, move.iRow].iContents = ComputerFigure;
            else
                aSquares[move.iCol, move.iRow].iContents = -ComputerFigure;
            aSquares[move.iCol, move.iRow].Invalidate();
            */
            if (CurrentPlayer == 1)
            {
                //rects[move.iCol, move.iRow].Fill = myYellowBrush;
                double center_x = this.centers[move.iCol, move.iRow, 0];
                double center_y = this.centers[move.iCol, move.iRow, 1];

                Ellipse el = new Ellipse();
                el.Stroke = this.myBlackBrush;
                el.StrokeThickness = 15.0f;
                //el.Fill = this.myBlackBrush;
                FrameworkElement ll = el as FrameworkElement;
                ll.Width = grid_side_len - item_margin;
                ll.Height = grid_side_len - item_margin;
                ll.SetValue(Canvas.TopProperty, center_y - (grid_side_len - item_margin) / 2.0);
                ll.SetValue(Canvas.LeftProperty, center_x - (grid_side_len - item_margin) / 2.0);
                this.canvas_master.Children.Add(ll);
                this.renders.Add(el);

            }
            else
            {
                
                //rects[move.iCol, move.iRow].Fill = myGreenBrush;
                double center_x = this.centers[move.iCol, move.iRow, 0];
                double center_y = this.centers[move.iCol, move.iRow, 1];

                Line l = new Line();
                l.X1 = center_x - grid_side_len / 2.0f + this.item_margin;
                l.X2 = center_x + grid_side_len / 2.0f - this.item_margin;
                l.Y1 = center_y - grid_side_len / 2.0f + this.item_margin;
                l.Y2 = center_y + grid_side_len / 2.0f - this.item_margin;
                l.StrokeThickness = 15.0f;
                l.Stroke = this.myGreenBrush;
                this.canvas_master.Children.Add(l);
                this.renders.Add(l);

                l = new Line();
                l.X1 = center_x - grid_side_len / 2.0f + this.item_margin;
                l.X2 = center_x + grid_side_len / 2.0f - this.item_margin;
                l.Y1 = center_y + grid_side_len / 2.0f - this.item_margin;
                l.Y2 = center_y - grid_side_len / 2.0f + this.item_margin;
                l.StrokeThickness = 15.0f;
                l.Stroke = this.myGreenBrush;
                this.canvas_master.Children.Add(l);
                this.renders.Add(l);
            }
            rects[move.iCol, move.iRow].InvalidateVisual();

            

            //Check for endgame, switch players
            board.CheckBoard();
            CurrentPlayer = -CurrentPlayer;
            this.mode = CurrentPlayer;

            //If game is over, update the form text
            if (board.BoardState != GameState.InProgress)
            {
                this.timer.Start();   
                //this.Dispatcher.Invoke(new System.EventHandler(this._restart), new object[] { null, null });   
            }
        }

        void obj_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (board.BoardState != GameState.InProgress)
                return;

            int[] which = FindIt(sender);

            //Make the move
            //Move move = new Move(Square.iCol, Square.iRow);
            Move move = new Move(which[0], which[1]);
            MakeMove(move);

        }
    }
}
