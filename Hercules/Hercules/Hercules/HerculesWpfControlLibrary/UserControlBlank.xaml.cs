﻿using System;
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

namespace HerculesWpfControlLibrary
{
    /// <summary>
    /// Interaction logic for UserControlBlank.xaml
    /// </summary>
    public partial class UserControlBlank : UserControl
    {
        public UserControlBlank()
        {
            InitializeComponent();

            this.image1.Visibility = System.Windows.Visibility.Visible;
            this.image2.Visibility = System.Windows.Visibility.Visible;
        }


        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //WindowWhiteCastle w = WindowWhiteCastle.getParent(this);
            //w.ShowMain();
        }
    }
}