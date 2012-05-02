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
   
namespace ScrollableArea
{
      /// <summary>
      /// Demonstrates how to make a scrollable (via the mouse) area that
      /// would be useful for storing a large object, such as diagram or
      /// something like that
      /// </summary>
      public partial class MainWindow : Window
      {
          #region Data
          // Used when manually scrolling.
          private Point scrollTarget;
          private Point scrollStartPoint;
          private Point scrollStartOffset;
          private Point previousPoint;
          private Vector velocity;
          private double friction; 
          private DispatcherTimer animationTimer = new DispatcherTimer();
          #endregion
   
          #region Ctor
   
          public MainWindow()
          {
              InitializeComponent();
              this.LoadStuff();
   
              friction = 0.95;
      
              animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
              animationTimer.Tick += new EventHandler(HandleWorldTimerTick);
              animationTimer.Start();
          }
          #endregion
   
          #region Load DUMMY Items
          void LoadStuff()
          {
              //this could be any large object, imagine a diagram…
              //though for this example im just using loads
              //of Rectangles
              itemsControl.Items.Add(CreateStackPanel(Brushes.Salmon));
              itemsControl.Items.Add(CreateStackPanel(Brushes.Goldenrod));
              itemsControl.Items.Add(CreateStackPanel(Brushes.Green));
              itemsControl.Items.Add(CreateStackPanel(Brushes.Yellow));
              itemsControl.Items.Add(CreateStackPanel(Brushes.Purple));
              itemsControl.Items.Add(CreateStackPanel(Brushes.SeaShell));
              itemsControl.Items.Add(CreateStackPanel(Brushes.SlateBlue));
              itemsControl.Items.Add(CreateStackPanel(Brushes.Tomato));
              itemsControl.Items.Add(CreateStackPanel(Brushes.Violet));
              itemsControl.Items.Add(CreateStackPanel(Brushes.Plum));
              itemsControl.Items.Add(CreateStackPanel(Brushes.PapayaWhip));
              itemsControl.Items.Add(CreateStackPanel(Brushes.Pink));
              itemsControl.Items.Add(CreateStackPanel(Brushes.Snow));
              itemsControl.Items.Add(CreateStackPanel(Brushes.YellowGreen));
              itemsControl.Items.Add(CreateStackPanel(Brushes.Tan));
   
          }
   
          private StackPanel CreateStackPanel(SolidColorBrush color)
          {
   
              StackPanel sp = new StackPanel();
              sp.Orientation = Orientation.Horizontal;
   
              for (int i = 0; i < 50; i++)
              {
                  Rectangle rect = new Rectangle();
                  rect.Width = 100;
                  rect.Height = 100;
                  rect.Margin = new Thickness(5);
                  rect.Fill = i % 2 == 0 ? Brushes.Black : color;
                  sp.Children.Add(rect);
              }
              return sp;
          }
          #endregion
   
          #region Friction Stuff
          private void HandleWorldTimerTick(object sender, EventArgs e)
          {
              if (IsMouseCaptured)
              {
                  Point currentPoint = Mouse.GetPosition(this);
                  velocity = previousPoint - currentPoint;
                  previousPoint = currentPoint;
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
   
   
    
       }
  }