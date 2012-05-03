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

namespace HerculesWPFImageButton
{
    

    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public delegate void MouseDown();
        public MouseDown evt = null;

        private void image1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.evt != null) this.evt();
        }

        public UserControl1()
        {
            InitializeComponent();
        }

    }
}
