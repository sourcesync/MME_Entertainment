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
using System.Threading;

namespace ScentTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MME.Hercules.PhidgetUtility.InitPhidgetBoard();

            MME.Hercules.PhidgetUtility.Relay(0, true);
            System.Threading.Thread.Sleep(500);
            MME.Hercules.PhidgetUtility.Relay(0, false);

            MME.Hercules.PhidgetUtility.Relay(1, true);
            System.Threading.Thread.Sleep(500);
            MME.Hercules.PhidgetUtility.Relay(1, false);

        }
    }
}
