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

namespace KeyboardTester
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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //WpfKb.Controls.FloatingTouchScreenKeyboard ky = new WpfKb.Controls.FloatingTouchScreenKeyboard();
            //this.AddChild(ky);
        }

        private void Keyboard_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Keyboard_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Keyboard_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Keyboard_TextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
