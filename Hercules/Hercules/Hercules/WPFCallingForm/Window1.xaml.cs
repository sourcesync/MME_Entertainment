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
using System.Windows.Shapes;

namespace WPFCallingForm
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            //MME.Hercules.Forms.User.Facebook fc = new MME.Hercules.Forms.User.Facebook(null);

            /*
            //this.wfh.Child = fc; 
            System.Windows.Forms.Integration.WindowsFormsHost.EnableWindowsFormsInterop();    // Calling this lets modeless windows form receive keyboard events (had to add reference to WindowsFormsIntegration)

            System.Windows.Forms.Application.EnableVisualStyles();          //      had to reference System.Windows.Forms

            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
             * */

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.RenderTransform = new RotateTransform(25);
            this.wfh.RenderTransform = new RotateTransform(25);
            this.button1.RenderTransform = new RotateTransform(25);
        }
    }
}
