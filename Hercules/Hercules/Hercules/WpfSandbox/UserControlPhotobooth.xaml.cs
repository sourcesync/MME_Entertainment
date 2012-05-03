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
using WPFCSharpWebCam;

namespace WpfSandbox
{
    /// <summary>
    /// Interaction logic for UserControlPhotobooth.xaml
    /// </summary>
    public partial class UserControlPhotobooth : UserControl
    {
        private WPFCSharpWebCam.WebCam front = null;
        private WPFCSharpWebCam.WebCam opposite = null;

        //private WpfCap.CapPlayer fplayer = null;
        //private WpfCap.CapPlayer oplayer = null;

        public UserControlPhotobooth()
        {
            InitializeComponent();

            //fplayer = new WpfCap.CapPlayer();
           
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (front == null)
            {
                front = new WebCam();
                front.InitializeWebCam(ref imageFront);
                //front.Start();
            }

            if (opposite == null)
            {
                //opposite = new WebCam();
                //opposite.InitializeWebCam(ref imageOpposite);
                //opposite.Start();
            }
             
        }

        public void Start()
        {
            if (front != null) front.Start();
            if (opposite != null) opposite.Start();
        }

        public void Stop()
        {
            if (front != null) front.Stop();
            if (opposite != null) opposite.Stop();
        }

        private void imageFront_Unloaded(object sender, RoutedEventArgs e)
        {
            
            if (front != null)
            {
                front.Stop();
            }

            if (opposite != null)
            {
                opposite.Stop();
            }
             
        }
    }
}
