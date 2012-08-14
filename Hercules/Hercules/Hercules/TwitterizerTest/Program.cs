using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Twitterizer;

namespace TwitterizerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            OAuthTokens tokens = new OAuthTokens();
            tokens.ConsumerKey = "Xi4yTO2Ywb1dlFmWF26g";
            tokens.ConsumerSecret = "YA5OhGSezEG8JAnAGZ7KUMxLRAJYJRDhgYDddrXelA";
            tokens.AccessToken = "630544227-ukVCxciFFdlfnLKkj0PYCKPPGNYR8HzKo1EaxdpY";
            tokens.AccessTokenSecret = "psVZa0sefPOXo9ktip4631mDbitD0TCb5io0TM5Vx0";

            //TwitterStatusCollection homeTimeline = TwitterStatus.GetHomeTimeline(tokens);

            object stuff= TwitterTimeline.HomeTimeline(tokens);

            TwitterResponse<TwitterStatus> tweetResponse = TwitterStatus.Update(tokens, "#buddymediapier60 table1 requests:this song");
            
        }
    }
}
