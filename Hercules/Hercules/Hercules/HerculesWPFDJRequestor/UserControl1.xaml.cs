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

using Twitterizer;
namespace HerculesWPFDJRequestor
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public String entry = "";

        public UserControl1()
        {
            InitializeComponent();

        }

        public void Stop()
        {
            this.Keyboard.Visibility = System.Windows.Visibility.Hidden;
            this.Keyboard.IsOpen = false;

            this.textBox1.Text = "";

            this.textBlock1.Visibility = System.Windows.Visibility.Hidden;
            this.image2.Visibility = System.Windows.Visibility.Hidden;
            
        }

        public void Start()
        {
            this.Keyboard.Visibility = System.Windows.Visibility.Hidden;
            this.Keyboard.IsOpen = false;
            this.entry = "";

            this.textBox1.Text = "";

            this.textBlock1.Visibility = System.Windows.Visibility.Hidden;
            this.image2.Visibility = System.Windows.Visibility.Hidden;
        }

        private void details()
        {
            OAuthTokens tokens = new OAuthTokens();
            tokens.AccessToken = "630544227-8uA3ET1jVzw23h9lmai74zrlPL6ozrpg4l2bbLcW";
            tokens.AccessTokenSecret = "G3KQ8sdp9jwlXYGuhTh1UFiPJvXav3t2YlQ8SWY0bBI";
            tokens.ConsumerKey = "Xi4yTO2Ywb1dlFmWF26g";
            tokens.ConsumerSecret = "YA5OhGSezEG8JAnAGZ7KUMxLRAJYJRDhgYDddrXelA";

            TwitterResponse<TwitterUser> showUserResponse = TwitterUser.Show(tokens, "twit_er_izer");
            if (showUserResponse.Result == RequestResult.Success)
            {
                //DisplayUserDetails(showUserResponse.ResponseObject);
            }
            else
            {
                //DisplayError(showUserResponse.ErrorMessage);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Keyboard.Visibility = System.Windows.Visibility.Visible;
            this.Keyboard.IsOpen = true;

            this.textBox1.Focus();


            this.textBlock1.Visibility = System.Windows.Visibility.Hidden;
            this.image2.Visibility = System.Windows.Visibility.Hidden;
            //this.details();
        }

        private void Keyboard_TextInput(object sender, TextCompositionEventArgs e)
        {
            String txt = e.ControlText;
            this.entry += txt;
        }

        private void Keyboard_KeyDown(object sender, KeyEventArgs e)
        {
            String ky = e.Key.ToString();
            this.entry += ky;
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void textBox1_TextInput(object sender, TextCompositionEventArgs e)
        {
            String txt = e.Text;
        }

        private void textBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void textBox1_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }

        private void textBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.SendIt();
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Keyboard.Visibility = System.Windows.Visibility.Visible;
            this.Keyboard.IsOpen = true;

            this.textBox1.Focus();


            this.textBlock1.Visibility = System.Windows.Visibility.Hidden;
            this.image2.Visibility = System.Windows.Visibility.Hidden;
        }

        private void SendIt()
        {
            this.Keyboard.Visibility = System.Windows.Visibility.Hidden;
            this.Keyboard.IsOpen = false;

            this.textBlock1.Text = "Great Choice that's my favorite too!";

            this.textBlock1.Visibility = System.Windows.Visibility.Visible;
            this.image2.Visibility = System.Windows.Visibility.Visible;

        }

        private void button1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Keyboard.Visibility = System.Windows.Visibility.Visible;
            this.Keyboard.IsOpen = true;

            this.textBox1.Focus();


            this.textBlock1.Visibility = System.Windows.Visibility.Hidden;
            this.image2.Visibility = System.Windows.Visibility.Hidden;
        }

        private void button2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Keyboard.Visibility = System.Windows.Visibility.Visible;
            this.Keyboard.IsOpen = true;

            this.textBox1.Focus();


            this.textBlock1.Visibility = System.Windows.Visibility.Hidden;
            this.image2.Visibility = System.Windows.Visibility.Hidden;
        }

        private void button1_TouchDown(object sender, TouchEventArgs e)
        {
            this.Keyboard.Visibility = System.Windows.Visibility.Visible;
            this.Keyboard.IsOpen = true;

            this.textBox1.Focus();


            this.textBlock1.Visibility = System.Windows.Visibility.Hidden;
            this.image2.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
