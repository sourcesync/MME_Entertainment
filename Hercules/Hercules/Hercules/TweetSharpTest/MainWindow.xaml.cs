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

using TweetSharp;

namespace TweetSharpTest
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
            /*
            TwitterService service = new TwitterService();
            IEnumerable<TwitterStatus> tweets = service.ListTweetsOnPublicTimeline();
            foreach (var tweet in tweets)
            {
                Console.WriteLine("{0} says '{1}'", tweet.User.ScreenName, tweet.Text);
            }
            */

            // Pass your credentials to the service
            TwitterService service = new TwitterService("Xi4yTO2Ywb1dlFmWF26g", "YA5OhGSezEG8JAnAGZ7KUMxLRAJYJRDhgYDddrXelA");
            service.AuthenticateWith("630544227-8uA3ET1jVzw23h9lmai74zrlPL6ozrpg4l2bbLcW", "G3KQ8sdp9jwlXYGuhTh1UFiPJvXav3t2YlQ8SWY0bBI");

            // Step 4 - User authenticates using the Access Token
            //service.AuthenticateWith(access.Token, access.TokenSecret);
            IEnumerable<TwitterStatus> mentions = service.ListTweetsMentioningMe();

            TwitterDirectMessage td = service.SendDirectMessage("Dr. Ridiculous ‏@Thinkridiculous", "hi!");
        }
    }
}
