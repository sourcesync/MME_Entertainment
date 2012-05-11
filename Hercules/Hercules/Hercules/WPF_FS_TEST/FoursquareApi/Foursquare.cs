using System;
using System.Text;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace Touchality.FoursquareApi
{
    [global::System.Serializable]
    public class FoursquareServiceException : Exception
    {
        public FoursquareServiceException() { }
        public FoursquareServiceException(string message) : base(message) { }
        public FoursquareServiceException(string message, Exception inner) : base(message, inner) { }
    }

    public class ListCompletedEventArgs<T>
    {
        public bool Cancelled { get; set; }
        public Exception Error { get; set; }
        public IList<T> Result { get; set; }
    }

    public class ItemCompletedEventArgs<T>
    {
        public bool Cancelled { get; set; }
        public Exception Error { get; set; }
        public T Result { get; set; }
    }

    public class GetVenuesCompletedEventArgs : ListCompletedEventArgs<Venue> { }
    public class GetCheckInsCompletedEventArgs : ListCompletedEventArgs<CheckIn> { }
    public class GetUserCompletedEventArgs : ItemCompletedEventArgs<User> { }
    public class GetVenueCompletedEventArgs : ItemCompletedEventArgs<Venue> { }
    public class CheckInCompletedEventArgs : ItemCompletedEventArgs<CheckIn> { }

    public static class Foursquare
    {

        public static bool UseFakeData { get { return false; } }

        #region Mock Data

        private const string GetFriendsMock = @"<?xml version=""1.0"" encoding=""UTF-8""?><checkins><checkin><id>13961958</id><created>Wed, 03 Mar 10 17:30:00 +0000</created><timezone>America/Los_Angeles</timezone><shout>Add a Shout</shout><distance>54</distance><venue><id>45268</id><name>Mix</name><primarycategory><id>79168</id><fullpathname>Nightlife:Lounge</fullpathname><nodename>Lounge</nodename><iconurl>http://foursquare.com/img/categories/nightlife/lounge.png</iconurl></primarycategory><address>3950 S. Las Vegas Blvd.</address><crossstreet>@ THEhotel</crossstreet><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.0922</geolat><geolong>-115.177</geolong></venue><user><id>217826</id><firstname>Tester</firstname><lastname>Touchality</lastname><photo>http://playfoursquare.s3.amazonaws.com/userpix_thumbs/XJCCZFIIY4LVEFTI.png</photo><gender>male</gender></user><display>Tester T. @ Mix</display></checkin><checkin><id>13683973</id><created>Tue, 02 Mar 10 10:12:54 +0000</created><timezone>America/Kentucky/Monticello</timezone><distance>3210418</distance><venue><id>648814</id><name>Zaxby's Restaurtant</name><address>Evans St.</address><city>Florence</city><state>SC</state><zip>29501</zip><geolat>34.1961767</geolat><geolong>-79.7896521</geolong></venue><user><id>134075</id><firstname>Chris</firstname><lastname>Craft</lastname><photo>http://playfoursquare.s3.amazonaws.com/userpix_thumbs/134075_1260233696145.jpg</photo><gender>male</gender></user><display>Chris C. @ Zaxby's Restaurtant</display></checkin><checkin><id>12954611</id><created>Sat, 27 Feb 10 00:17:43 +0000</created><timezone>America/Kentucky/Monticello</timezone><distance>3207021</distance><user><id>133852</id><firstname>Jamey</firstname><lastname>McElveen</lastname><photo>http://playfoursquare.s3.amazonaws.com/userpix_thumbs/N2U3KL3UTFOIX1AC.jpg</photo><gender>male</gender></user><display>Jamey M. @ [off the grid]</display></checkin><checkin><id>7752697</id><created>Sun, 31 Jan 10 21:08:10 +0000</created><timezone>America/Kentucky/Monticello</timezone><distance>3090866</distance><venue><id>364148</id><name>Dilworth</name><address>East Blvd</address><crossstreet>Park</crossstreet><city>Charlotte</city><state>NC</state><geolat>35.2037952</geolat><geolong>-80.847141</geolong></venue><user><id>150233</id><firstname>Charlotte</firstname><lastname>FourSquare</lastname><photo>http://playfoursquare.s3.amazonaws.com/userpix_thumbs/XS2TW3AXV3ND4ACZ.jpg</photo><gender>male</gender></user><display>Charlotte F. @ Dilworth</display></checkin><checkin><id>7243375</id><created>Thu, 28 Jan 10 20:49:13 +0000</created><timezone>America/Los_Angeles</timezone><shout>Hello World</shout><distance>671675</distance><venue><id>9332</id><name>The Buccaneer</name><primarycategory><id>79158</id><fullpathname>Nightlife:Dive Bar</fullpathname><nodename>Dive Bar</nodename><iconurl>http://foursquare.com/img/categories/nightlife/default.png</iconurl></primarycategory><address>2155 Polk St</address><crossstreet>at Vallejo</crossstreet><city>San Francisco</city><state>CA</state><zip>94109</zip><geolat>37.7966</geolat><geolong>-122.422</geolong><phone>4156738023</phone></venue><user><id>146868</id><firstname>Touchality</firstname><photo>http://playfoursquare.s3.amazonaws.com/userpix_thumbs/-1_1260232746039.png</photo><gender>male</gender></user><display>Touchality @ The Buccaneer</display></checkin></checkins>";
        private const string GetUserMock = @"<?xml version=""1.0"" encoding=""UTF-8""?><user><id>217826</id><firstname>Tester</firstname><lastname>Touchality</lastname><photo>http://playfoursquare.s3.amazonaws.com/userpix_thumbs/XJCCZFIIY4LVEFTI.png</photo><gender>male</gender><email>support@touchality.com</email><settings><pings>off</pings><sendtotwitter>false</sendtotwitter><sendtofacebook>false</sendtofacebook></settings><status><friendrequests>1</friendrequests></status><checkin><id>13961958</id><created>Wed, 03 Mar 10 17:30:00 +0000</created><timezone>America/Los_Angeles</timezone><shout>Add a Shout</shout><venue><id>45268</id><name>Mix</name><primarycategory><id>79168</id><fullpathname>Nightlife:Lounge</fullpathname><nodename>Lounge</nodename><iconurl>http://foursquare.com/img/categories/nightlife/lounge.png</iconurl></primarycategory><address>3950 S. Las Vegas Blvd.</address><crossstreet>@ THEhotel</crossstreet><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.0922</geolat><geolong>-115.177</geolong></venue><display>Tester T. @ Mix</display></checkin><badges><badge><id>1</id><name>Newbie</name><icon>http://foursquare.com/img/badge/newbie.png</icon><description>Congrats on your first check-in!</description></badge><badge><id>2</id><name>Adventurer</name><icon>http://foursquare.com/img/badge/adventurer.png</icon><description>You've checked into 10 different venues!</description></badge><badge><id>3</id><name>Explorer</name><icon>http://foursquare.com/img/badge/explorer.png</icon><description>You've checked into 25 different venues!</description></badge><badge><id>6</id><name>Crunked</name><icon>http://foursquare.com/img/badge/crunked.png</icon><description>That's 4+ stops in one night for you!</description></badge><badge><id>7</id><name>Local</name><icon>http://foursquare.com/img/badge/local.png</icon><description>You've been at the same place 3x in one week!</description></badge><badge><id>8</id><name>Super User</name><icon>http://foursquare.com/img/badge/superuser.png</icon><description>That's 30 checkins in a month for you!</description></badge><badge><id>10</id><name>School Night</name><icon>http://foursquare.com/img/badge/schoolnight.png</icon><description>Checking-in after 3am on a school night? Well done!</description></badge></badges><mayor><venue><id>763597</id><name>Bed Bath &amp; Beyond</name></venue><venue><id>608027</id><name>Best Buy</name></venue><venue><id>1294878</id><name>Firehouse Subs</name></venue><venue><id>1067013</id><name>Holiday Inn Express</name></venue><venue><id>695072</id><name>Ihop</name></venue><venue><id>608427</id><name>Kohls</name></venue><venue><id>763871</id><name>Olive Garden</name></venue></mayor></user>";
        private const string GetVenueMock = @"<?xml version=""1.0"" encoding=""UTF-8""?><venue><id>349399</id><name>The Rose</name><address>3930 Las Vegas Blvd S # 200B</address><crossstreet>Mandalay Bay Hotel &amp; Casino</crossstreet><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.094172</geolat><geolong>-115.174422</geolong><stats><checkins>2</checkins><herenow>0</herenow><beenhere><me>false</me><friends>false</friends></beenhere></stats></venue>";
        private const string GetCheckInMock = @"<?xml version=""1.0"" encoding=""UTF-8""?><checkin><id>14045747</id><created>Thu, 04 Mar 10 00:22:23 +0000</created><message>OK! We've got you @ House of Blues. This is your 1st checkin here!</message><venue><id>14738</id><name>House of Blues</name><primarycategory><id>78964</id><fullpathname>Arts &amp; Entertainment:Comedy Club</fullpathname><nodename>Comedy Club</nodename><iconurl>http://foursquare.com/img/categories/arts_entertainment/default.png</iconurl></primarycategory><address>3950 Las Vegas Blvd S</address><crossstreet>@ Mandalay Bay</crossstreet><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.0922</geolat><geolong>-115.177</geolong></venue><mayor><type>nochange</type><checkins>2</checkins><user><id>271984</id><firstname>Jarid</firstname><lastname>I.</lastname><photo>http://playfoursquare.s3.amazonaws.com/userpix_thumbs/EW5CXA1XT4YZDLIC.jpg</photo><gender>male</gender></user><message>Jarid I. is The Mayor of House of Blues.</message></mayor><scoring><score><points>3</points><icon>http://foursquare.com/img/scoring/3.png</icon><message>Travel bonus: 3 stops</message></score><score><points>5</points><icon>http://foursquare.com/img/scoring/1.png</icon><message>First time @ House of Blues!</message></score></scoring></checkin>";
        private const string GetVenuesMock = @"<?xml version=""1.0"" encoding=""UTF-8""?><venues><group type=""My Favorites""><venue><id>14738</id><name>House of Blues</name><primarycategory><id>78964</id><fullpathname>Arts &amp; Entertainment:Comedy Club</fullpathname><nodename>Comedy Club</nodename><iconurl>http://foursquare.com/img/categories/arts_entertainment/default.png</iconurl></primarycategory><address>3950 Las Vegas Blvd S</address><crossstreet>@ Mandalay Bay</crossstreet><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.0922</geolat><geolong>-115.177</geolong><stats><herenow>1</herenow></stats><distance>54</distance></venue><venue><id>45268</id><name>Mix</name><primarycategory><id>79168</id><fullpathname>Nightlife:Lounge</fullpathname><nodename>Lounge</nodename><iconurl>http://foursquare.com/img/categories/nightlife/lounge.png</iconurl></primarycategory><address>3950 S. Las Vegas Blvd.</address><crossstreet>@ THEhotel</crossstreet><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.0922</geolat><geolong>-115.177</geolong><stats><herenow>0</herenow></stats><distance>54</distance></venue><venue><id>14646</id><name>Coral Reef Lounge</name><address>3950 Las Vegas Blvd S</address><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.0922</geolat><geolong>-115.177</geolong><stats><herenow>0</herenow></stats><distance>54</distance></venue></group><group type=""Nearby""><venue><id>15059</id><name>rumjungle</name><primarycategory><id>79157</id><fullpathname>Nightlife:Nightclub / Discotheque</fullpathname><nodename>Nightclub / Discotheque</nodename><iconurl>http://foursquare.com/img/categories/nightlife/danceparty.png</iconurl></primarycategory><address>3950 Las Vegas Blvd S</address><crossstreet>@ Mandalay Bay</crossstreet><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.0922</geolat><geolong>-115.177</geolong><stats><herenow>0</herenow></stats><distance>54</distance></venue><venue><id>14913</id><name>Red Square</name><primarycategory><id>79092</id><fullpathname>Food:Steakhouse</fullpathname><nodename>Steakhouse</nodename><iconurl>http://foursquare.com/img/categories/food/steakhouse.png</iconurl></primarycategory><address>3950 Las Vegas Blvd S</address><crossstreet>(@ Mandalay Bay)</crossstreet><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.0922</geolat><geolong>-115.177</geolong><stats><herenow>0</herenow></stats><distance>54</distance></venue><venue><id>15032</id><name>Dragon</name><address>3950 Las Vegas Blvd S</address><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.0922</geolat><geolong>-115.177</geolong><stats><herenow>0</herenow></stats><distance>54</distance></venue><venue><id>14630</id><name>China Grill</name><address>3950 Las Vegas Blvd S</address><crossstreet>@ Mandalay Bay</crossstreet><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.0922</geolat><geolong>-115.177</geolong><stats><herenow>0</herenow></stats><distance>54</distance></venue><venue><id>14826</id><name>Moorea Ultra Beach Lounge</name><address>3950 Las Vegas Blvd S</address><crossstreet>@ Mandalay Bay</crossstreet><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.0922</geolat><geolong>-115.177</geolong><stats><herenow>0</herenow></stats><distance>54</distance></venue><venue><id>14695</id><name>Foundation Room</name><address>3950 Las Vegas Blvd S</address><crossstreet>@ Mandalay Bay</crossstreet><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.0922</geolat><geolong>-115.177</geolong><stats><herenow>0</herenow></stats><distance>54</distance></venue><venue><id>14754</id><name>Island Lounge</name><address>3950 Las Vegas Blvd S</address><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.0922</geolat><geolong>-115.177</geolong><stats><herenow>0</herenow></stats><distance>54</distance></venue><venue><id>1118816</id><name>Red White and Blue Cafe</name><address></address><city>Clark</city><state>NV</state><geolat>36.09243</geolat><geolong>-115.1769</geolong><stats><herenow>0</herenow></stats><distance>78</distance></venue><venue><id>794737</id><name>THECafe at the Mandalay</name><address></address><city>Las Vegas</city><state>NV</state><geolat>36.092992</geolat><geolong>-115.177954</geolong><stats><herenow>0</herenow></stats><distance>126</distance></venue><venue><id>651024</id><name>Evening Call</name><address>Mandalay Bay Hotel/Bay</address><city></city><state></state><geolat>36.092806</geolat><geolong>-115.176483</geolong><stats><herenow>0</herenow></stats><twitter>eveningcall</twitter><distance>134</distance></venue><venue><id>625207</id><name>The Lion King</name><address>Mandalay Bay Hotel</address><city>Las Vegas</city><state></state><geolat>36.093531</geolat><geolong>-115.177785</geolong><stats><herenow>0</herenow></stats><distance>181</distance></venue><venue><id>1044932</id><name>Urban Outfitters At Mandalay Bay</name><address>W Hacienda Ave</address><city>Las Vegas</city><state>Nevada</state><zip>89119</zip><geolat>36.0936222</geolat><geolong>-115.1773157</geolong><stats><herenow>0</herenow></stats><distance>190</distance></venue><venue><id>1269071</id><name>Mandalay Bay Convention Center Surf D</name><address>W Hacienda Ave</address><city>Las Vegas</city><state>Nevada</state><zip>89119</zip><geolat>36.0936222</geolat><geolong>-115.1773157</geolong><stats><herenow>0</herenow></stats><distance>190</distance></venue><venue><id>895578</id><name>A Robert Cromeans Salon</name><primarycategory><id>79249</id><fullpathname>Shops:Spas / Massage</fullpathname><nodename>Spas / Massage</nodename><iconurl>http://foursquare.com/img/categories/shops/default.png</iconurl></primarycategory><address>3950 Las Vegas Blvd South Suite 105</address><crossstreet>at Mandalay Bay</crossstreet><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.090305</geolat><geolong>-115.176692</geolong><stats><herenow>0</herenow></stats><phone>7026326130</phone><distance>193</distance></venue><venue><id>1100702</id><name>Raffle Cafe</name><address>Mandalay</address><city>Las Vegas</city><state>NV</state><geolat>36.091378</geolat><geolong>-115.1752998</geolong><stats><herenow>0</herenow></stats><distance>206</distance></venue><venue><id>84669</id><name>Fleur de Lys</name><primarycategory><id>79063</id><fullpathname>Food:French</fullpathname><nodename>French</nodename><iconurl>http://foursquare.com/img/categories/food/default.png</iconurl></primarycategory><address>Mandalay Bay</address><city>Las Vegas</city><state>NV</state><geolat>36.0917</geolat><geolong>-115.175</geolong><stats><herenow>0</herenow></stats><phone>7026329400</phone><distance>226</distance></venue><venue><id>364522</id><name>The powder room 43rd floor</name><address>Mandalay bay hotel</address><city>Las Vegas</city><state>NV</state><geolat>36.0916667</geolat><geolong>-115.1747222</geolong><stats><herenow>0</herenow></stats><distance>251</distance></venue><venue><id>774935</id><name>Bayside Buffet - Mandalay Bay</name><address>Mandalay Bay Hotel</address><city>Las Vegas</city><state>NV</state><geolat>36.0916667</geolat><geolong>-115.1747222</geolong><stats><herenow>0</herenow></stats><distance>251</distance></venue><venue><id>1394479</id><name>Channel Partners Conference &amp; Expo</name><address>Mandalay Bay Resort &amp; Casino</address><city>Las Vegas</city><state>NV</state><geolat>36.0916667</geolat><geolong>-115.1747222</geolong><stats><herenow>0</herenow></stats><distance>251</distance></venue><venue><id>258251</id><name>Mandalay Beach</name><address>Mandalay resort</address><crossstreet>Las Vegas Blvd</crossstreet><city>Las Vegas</city><state>NV</state><geolat>36.0916667</geolat><geolong>-115.1747222</geolong><stats><herenow>0</herenow></stats><distance>251</distance></venue><venue><id>765594</id><name>THE Lounge at THE Hotel</name><address></address><city></city><state></state><geolat>36.093935</geolat><geolong>-115.176034</geolong><stats><herenow>0</herenow></stats><distance>260</distance></venue><venue><id>589686</id><name>Mandalay Place</name><address></address><city></city><state></state><geolat>36.094426</geolat><geolong>-115.175925</geolong><stats><herenow>0</herenow></stats><distance>312</distance></venue><venue><id>82692</id><name>Raffles Cafe - Mandalay Bay</name><address>3950 S. Las Vegas Blvd.</address><city>Las Vegas</city><state>NV</state><geolat>36.0919</geolat><geolong>-115.174</geolong><stats><herenow>0</herenow></stats><distance>314</distance></venue><venue><id>773715</id><name>Digital Theater NATPE</name><address></address><city></city><state></state><geolat>36.089189</geolat><geolong>-115.176337</geolong><stats><herenow>0</herenow></stats><distance>321</distance></venue><venue><id>981184</id><name>UFC 109</name><address></address><city></city><state></state><geolat>36.089144</geolat><geolong>-115.176325</geolong><stats><herenow>0</herenow></stats><distance>326</distance></venue><venue><id>1015256</id><name>Shanghai Lilly</name><address>3950 Las Vegas Blvd S</address><city>Las Vegas</city><state>NV</state><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><distance>335</distance></venue><venue><id>85670</id><name>Strip Steak - Mandalay Bay</name><primarycategory><id>79092</id><fullpathname>Food:Steakhouse</fullpathname><nodename>Steakhouse</nodename><iconurl>http://foursquare.com/img/categories/food/steakhouse.png</iconurl></primarycategory><address>3950 S. Las Vegas Blvd</address><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><phone>7026327414</phone><twitter>STRIPSTEAKLV</twitter><distance>335</distance></venue><venue><id>389609</id><name>HP Tech Forum 2010</name><address>3950 Las Vegas Boulevard South Las Vegas, NV 89119</address><city>Las Vegas</city><state>NV</state><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><twitter>HPTechForum2010</twitter><distance>335</distance></venue><venue><id>1154522</id><name>MIX Restaurant</name><address>3950 Las Vegas Boulevard South</address><city>Las Vegas</city><state>NV</state><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><phone>7026327777</phone><distance>335</distance></venue><venue><id>1275690</id><name>Dean Martin's Wild Party Slot Machine</name><address></address><city></city><state></state><geolat>36.089063</geolat><geolong>-115.176325</geolong><stats><herenow>0</herenow></stats><distance>335</distance></venue><venue><id>998490</id><name>Mandalay Bay Theatre</name><address>3950 Las Vegas Blvd S</address><city>Las Vegas</city><state>Nevada</state><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><distance>335</distance></venue><venue><id>248623</id><name>Starbucks - Mandalay Bay</name><primarycategory><id>79052</id><fullpathname>Food:Coffee Shop</fullpathname><nodename>Coffee Shop</nodename><iconurl>http://foursquare.com/img/categories/food/coffeeshop.png</iconurl></primarycategory><address>3950 Las Vegas Blvd S</address><crossstreet>W Russell St</crossstreet><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><distance>335</distance></venue><venue><id>248776</id><name>The Noodle Shop in Mandalay Bay</name><primarycategory><id>79051</id><fullpathname>Food:Chinese</fullpathname><nodename>Chinese</nodename><iconurl>http://foursquare.com/img/categories/food/default.png</iconurl></primarycategory><address>3950 Las Vegas Blvd S</address><crossstreet>W Russell</crossstreet><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><phone>7026324934</phone><distance>335</distance></venue><venue><id>442198</id><name>Mandalay Bay - Sports Book</name><address>3950 S Las Vegas Blvd</address><city>Las Vegas</city><state>NV</state><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><distance>335</distance></venue><venue><id>14805</id><name>Mandalay Bay Hotel and Casino</name><primarycategory><id>78963</id><fullpathname>Arts &amp; Entertainment:Casino</fullpathname><nodename>Casino</nodename><iconurl>http://foursquare.com/img/categories/arts_entertainment/casino.png</iconurl></primarycategory><address>3950 Las Vegas Blvd S</address><crossstreet>at W Russell Rd</crossstreet><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>1</herenow></stats><phone>8776327800</phone><twitter>mandalaybay_LV</twitter><distance>335</distance></venue><venue><id>661686</id><name>The Spa at Mandalay Bay</name><address>3950 Las Vegas Blvd S</address><city>las vegas</city><state>nv</state><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><distance>335</distance></venue><venue><id>149890</id><name>canters deli</name><address>3950 las vegas blvd s</address><crossstreet>Mandalay Bay</crossstreet><city>Las Vegas</city><state>NV</state><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><distance>335</distance></venue><venue><id>502403</id><name>Mandalay Bay - Poker Room</name><address>3950 las vegas blvd s.</address><city>Las vegas</city><state>NV</state><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><distance>335</distance></venue><venue><id>606440</id><name>Mandalay Bay Convention Center</name><address>3950 Las Vegas Blvd S</address><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><phone>7026327777</phone><distance>335</distance></venue><venue><id>148653</id><name>Orchid Lounge</name><address>3950 Las Vegas Blvd. South</address><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><distance>335</distance></venue><venue><id>127156</id><name>Shop.org Annual Summit 2009</name><address>3950 Las Vegas Blvd.</address><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><twitter>shoporgsummit</twitter><distance>335</distance></venue><venue><id>549189</id><name>aureole</name><address>3950 Las vegas blvd.S</address><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><phone>7025051750</phone><distance>335</distance></venue><venue><id>465230</id><name>Mizuya</name><address>3950 S. Las Vegas Blvd.</address><city>Las Vegas</city><state>NV</state><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><distance>335</distance></venue><venue><id>605375</id><name>HP Technology Forum 2010</name><address>3950 Las Vegas Boulevard South Las Vegas, NV 89119</address><city>Las Vegas</city><state>Nevada</state><zip>USA</zip><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><twitter>Connect_WW</twitter><distance>335</distance></venue><venue><id>814991</id><name>Aureole Fine Dining</name><address>3950 So Las Vegas Blvd</address><crossstreet>Tropicana</crossstreet><city>Las Vegas</city><state>nv</state><geolat>36.091854</geolat><geolong>-115.173773</geolong><stats><herenow>0</herenow></stats><distance>335</distance></venue><venue><id>383444</id><name>LUSH (Mandalay Bay)</name><address>3930 South Las Vegas Blvd</address><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.094172</geolat><geolong>-115.174422</geolong><stats><herenow>0</herenow></stats><phone>7022275874</phone><distance>373</distance></venue><venue><id>1192327</id><name>Burger Bar at Mandalay Place</name><address>3930 Las Vegas Blvd South</address><city>Las Vegas</city><state>Nevada</state><geolat>36.094172</geolat><geolong>-115.174422</geolong><stats><herenow>0</herenow></stats><distance>373</distance></venue><venue><id>381911</id><name>Burger Bar (Mandalay Bay)</name><address>3930 Las Vegas Blvd S</address><city>Las Vegas</city><state>NV</state><zip>89136</zip><geolat>36.094172</geolat><geolong>-115.174422</geolong><stats><herenow>0</herenow></stats><distance>373</distance></venue><venue><id>349399</id><name>The Rose</name><address>3930 Las Vegas Blvd S # 200B</address><crossstreet>Mandalay Bay Hotel &amp; Casino</crossstreet><city>Las Vegas</city><state>NV</state><zip>89119</zip><geolat>36.094172</geolat><geolong>-115.174422</geolong><stats><herenow>0</herenow></stats><distance>373</distance></venue><venue><id>975658</id><name>taco tequilas at luxor</name><address></address><city>Las Vegas</city><state></state><geolat>36.095242</geolat><geolong>-115.175861</geolong><stats><herenow>0</herenow></stats><distance>398</distance></venue></group></venues>";

        #endregion

        #region Helper Methods

        private static string GetAuthHeader()
        {
            return authHeader;
        }

        private static string authHeader = string.Empty;
        public static void SetCredentials(string username, string password)
        {
            var base64Creds = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password)));
            authHeader = "Basic " + base64Creds;
        }

        public static WebClient GetWebClient(bool authenicated)
        {
            var webClient = new WebClient();
            if (authenicated)
            {
                webClient.Headers["Authorization"] = GetAuthHeader();
            }
            return webClient;
        }

        public static double ToDouble(string value, double dflt)
        {
            double result = double.NaN;
            if (!double.TryParse(value, out result))
            {
                return dflt;
            }
            return result;
        }

        #endregion

        #region SignIn
        public static void SignIn(GetUserCallback callback, string username, string password)
        {
            SetCredentials(username, password);
            GetUser(callback);
        }
        #endregion

        #region GetVenues
        public delegate void GetVenuesCallback(GetVenuesCompletedEventArgs e);

        public static void GetVenues(GetVenuesCallback callback, double geolat, double geolong)
        {
            GetVenues(callback, null, geolat, geolong);
        }

        public static void GetVenues(GetVenuesCallback callback, string query, double geolat, double geolong)
        {
            string url;
            if (string.IsNullOrEmpty(query))
            {
                url = string.Format("http://api.foursquare.com/v1/venues?geolat={0}&geolong={1}&l=50", geolat, geolong);
            }
            else
            {
                query = Uri.EscapeUriString(query);
                url = string.Format("http://api.foursquare.com/v1/venues?geolat={0}&geolong={1}&l=50&q=", geolat, geolong, query);
            }

            if (UseFakeData)
            {
                var venues = ParseGetVenues(GetVenuesMock);
                callback(new GetVenuesCompletedEventArgs
                {
                    Cancelled = false,
                    Error = null,
                    Result = venues,
                });
                return;
            }

            var webClient = GetWebClient(true);

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(
                delegate(object sender, DownloadStringCompletedEventArgs e)
                {
                    GetVenuesCallback cb = e.UserState as GetVenuesCallback;
                    if (e.Error == null)
                    {
                        try
                        {
                            var venues = ParseGetVenues(e.Result);
                            cb(new GetVenuesCompletedEventArgs
                            {
                                Cancelled = e.Cancelled,
                                Error = e.Error,
                                Result = venues,
                            });
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        Debug.WriteLine(e.Result);
                    }
                    else
                    {
                        cb(new GetVenuesCompletedEventArgs
                        {
                            Cancelled = e.Cancelled,
                            Error = e.Error,
                            Result = null,
                        });
                        Debug.WriteLine(e.Error);
                    }
                });

            webClient.DownloadStringAsync(new Uri(url), callback);
        }

        private static IList<Venue> ParseGetVenues(string xml)
        {
            try
            {
                var dom = XDocument.Parse(xml);

                var items = from item in dom.Descendants("venue")
                            select new Venue
                            {
                                id = (string)item.Element("id"),
                                phone = (string)item.Element("phone"),
                                name = (string)item.Element("name"),
                                address = string.IsNullOrEmpty((string)item.Element("address")) ? "3950 Las Vegas Blvd S" : (string)item.Element("address"),
                                crossstreet = (string)item.Element("crossstreet"),
                                geolat = (string)item.Element("geolat"),
                                geolong = (string)item.Element("geolong"),
                                distance = (string)item.Element("distance"),
                                city = (string)item.Element("city"),
                                state = (string)item.Element("state"),
                                zip = (string)item.Element("zip"),
                                twitter = (string)item.Element("twitter"),
                            };

                return items.ToList<Venue>();
            }
            catch
            {
                return new List<Venue>();
            }
        }

        #endregion

        #region GetCheckIns

        public delegate void GetCheckInsCallback(GetCheckInsCompletedEventArgs e);

        public static void GetCheckIns(GetCheckInsCallback callback, double geolat, double geolong)
        {
            var url = string.Format("http://api.foursquare.com/v1/checkins?geolat={0}&geolong={1}", geolat, geolong);

            if (UseFakeData)
            {
                var checkIns = ParseGetCheckIns(GetFriendsMock);
                callback(new GetCheckInsCompletedEventArgs
                {
                    Cancelled = false,
                    Error = null,
                    Result = checkIns,
                });
                return;
            }

            var webClient = GetWebClient(true);

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(
                delegate(object sender, DownloadStringCompletedEventArgs e)
                {
                    GetCheckInsCallback cb = e.UserState as GetCheckInsCallback;
                    if (e.Error == null)
                    {
                        try
                        {
                            var checkIns = ParseGetCheckIns(e.Result);
                            cb(new GetCheckInsCompletedEventArgs
                            {
                                Cancelled = e.Cancelled,
                                Error = e.Error,
                                Result = checkIns,
                            });
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        Debug.WriteLine(e.Result);
                    }
                    else
                    {
                        cb(new GetCheckInsCompletedEventArgs
                        {
                            Cancelled = e.Cancelled,
                            Error = e.Error,
                            Result = null,
                        });
                        Debug.WriteLine(e.Error);
                    }
                });

            webClient.DownloadStringAsync(new Uri(url), callback);
        }

        private static IList<CheckIn> ParseGetCheckIns(string xml)
        {
            try
            {
                var dom = XDocument.Parse(xml);

                var items = from item in dom.Descendants("checkin")
                            select new CheckIn
                            {
                                id = (string)item.Element("id"),
                                created = (string)item.Element("created"),
                                timezone = (string)item.Element("timezone"),
                                ismayor = (string)item.Element("ismayor"),
                                display = (string)item.Element("display"),
                                distance = ToDouble(item.Element("distance").Value, double.NaN),
                                user = new User
                                {
                                    id = (string)item.Element("user").Element("id"),
                                    photo = (string)item.Element("user").Element("photo"),
                                    firstname = (string)item.Element("user").Element("firstname"),
                                    lastname = (string)item.Element("user").Element("lastname")
                                },
                                venue = new Venue
                                {
                                    name = item.Element("venue") != null ? (string)item.Element("venue").Element("name") : "Off the Grid",
                                    address = item.Element("venue") != null ? (string)item.Element("venue").Element("address") : string.Empty,
                                    geolat = item.Element("venue") != null ? (string)item.Element("venue").Element("geolat") : string.Empty,
                                    geolong = item.Element("venue") != null ? (string)item.Element("venue").Element("geolong") : string.Empty,
                                }
                            };

                return items.ToList<CheckIn>();

            }
            catch (Exception)
            {
                return new List<CheckIn>();
            }
        }

        //venue = new Venue
        //{
        //    id = (string)item.Element("venue").Element("id"),
        //    name = (string)item.Element("venue").Element("name") ?? "Off the Grid",
        //    address = (string)item.Element("venue").Element("address"),
        //    crossstreet = (string)item.Element("venue").Element("crossstreet"),
        //    geolat = (string)item.Element("venue").Element("geolat"),
        //    geolong = (string)item.Element("venue").Element("geolong"),
        //    distance = (string)item.Element("venue").Element("distance"),
        //    state = (string)item.Element("venue").Element("state"),
        //    zip = (string)item.Element("venue").Element("zip"),
        //    phone = (string)item.Element("venue").Element("phone"),
        //    twitter = (string)item.Element("venue").Element("twitter"),
        //}

        #endregion

        #region Get User

        public delegate void GetUserCallback(GetUserCompletedEventArgs e);

        public static void GetUser(GetUserCallback callback)
        {
            GetUser(callback, null);
        }

        public static void GetUser(GetUserCallback callback, string id)
        {
            string url;
            if (id == null)
            {
                url = "http://api.foursquare.com/v1/user?badges=1&mayor=1";
            }
            else
            {
                url = string.Format("http://api.foursquare.com/v1/user?uid={0}&badges=1&mayor=1", id);
            }

            if (UseFakeData)
            {
                var checkIns = ParseGetUser(GetUserMock);
                callback(new GetUserCompletedEventArgs
                {
                    Cancelled = false,
                    Error = null,
                    Result = checkIns,
                });
                return;
            }

            var webClient = GetWebClient(true);

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(
                delegate(object sender, DownloadStringCompletedEventArgs e)
                {
                    GetUserCallback cb = e.UserState as GetUserCallback;
                    if (e.Error == null)
                    {
                        try
                        {
                            //<error>Foursquare is over capacity, please try again in a few minutes!</error>
                            if (e.Result.StartsWith("<error>"))
                            {
                                cb(new GetUserCompletedEventArgs
                                {
                                    Cancelled = e.Cancelled,
                                    Error = new FoursquareServiceException(e.Result.Replace("<error>", string.Empty).Replace("</error>", string.Empty)),
                                    Result = null,
                                });
                                return;
                            }

                            var result = ParseGetUser(e.Result);
                            cb(new GetUserCompletedEventArgs
                            {
                                Cancelled = e.Cancelled,
                                Error = e.Error,
                                Result = result,
                            });
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        Debug.WriteLine(e.Result);
                    }
                    else
                    {
                        cb(new GetUserCompletedEventArgs
                        {
                            Cancelled = e.Cancelled,
                            Error = e.Error,
                            Result = null,
                        });
                        Debug.WriteLine(e.Error);
                    }
                });

            webClient.DownloadStringAsync(new Uri(url), callback);
        }

        private static User ParseGetUser(string xml)
        {
            try
            {
                var dom = XDocument.Parse(xml);

                var items = from item in dom.Descendants("user")
                            select new User
                            {
                                id = (string)item.Element("id"),
                                firstname = (string)item.Element("firstname"),
                                lastname = (string)item.Element("lastname"),
                                email = (string)item.Element("email"),
                                friendstatus = (string)item.Element("friendstatus"),
                                gender = (string)item.Element("gender"),
                                phone = (string)item.Element("phone"),
                                photo = (string)item.Element("photo"),
                            };


                var user = items.FirstOrDefault<User>();

                var badges = from item in dom.Descendants("user").Descendants("badge")
                             select new Badge
                             {
                                 id = (string)item.Element("id"),
                                 name = (string)item.Element("name"),
                                 icon = (string)item.Element("icon"),
                                 description = (string)item.Element("description"),
                             };

                if (badges.Count() > 0)
                    user.badges = badges.ToList<Badge>();
                else
                    user.badges = new List<Badge>();

                var checkIn = from item in dom.Descendants("user").Descendants("checkin")
                              select new CheckIn
                              {
                                  id = (string)item.Element("id"),
                                  created = (string)item.Element("created"),
                                  timezone = (string)item.Element("timezone"),
                                  shout = (string)item.Element("shout"),
                                  display = (string)item.Element("display"),
                              };

                user.checkin = checkIn.FirstOrDefault<CheckIn>();

                try
                {
                    var q = from item in dom.Descendants("user").Descendants("checkin").Descendants("venue")
                            select new Venue
                            {
                                id = (string)item.Element("id"),
                                name = (string)item.Element("name"),
                                address = (string)item.Element("address"),
                                city = (string)item.Element("city"),
                                state = (string)item.Element("state"),
                                zip = (string)item.Element("zip"),
                                geolat = (string)item.Element("geolat"),
                                geolong = (string)item.Element("geolong"),
                            };
                    user.venue = q.FirstOrDefault<Venue>();
                }
                catch
                {
                }


                return user;

            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Get Venue

        public delegate void GetVenueCallback(GetVenueCompletedEventArgs e);

        public static void GetVenue(GetVenueCallback callback, string vid)
        {
            string url = string.Format("http://api.foursquare.com/v1/venue?vid={0}", vid);

            if (UseFakeData)
            {
                var checkIns = ParseGetVenue(GetVenueMock);
                callback(new GetVenueCompletedEventArgs
                {
                    Cancelled = false,
                    Error = null,
                    Result = checkIns,
                });
                return;
            }

            var webClient = GetWebClient(true);

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(
                delegate(object sender, DownloadStringCompletedEventArgs e)
                {
                    GetVenueCallback cb = e.UserState as GetVenueCallback;
                    if (e.Error == null)
                    {
                        try
                        {
                            var result = ParseGetVenue(e.Result);
                            cb(new GetVenueCompletedEventArgs
                            {
                                Cancelled = e.Cancelled,
                                Error = e.Error,
                                Result = result,
                            });
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        Debug.WriteLine(e.Result);
                    }
                    else
                    {
                        cb(new GetVenueCompletedEventArgs
                        {
                            Cancelled = e.Cancelled,
                            Error = e.Error,
                            Result = null,
                        });
                        Debug.WriteLine(e.Error);
                    }
                });

            webClient.DownloadStringAsync(new Uri(url), callback);
        }

        private static Venue ParseGetVenue(string xml)
        {
            try
            {
                var dom = XDocument.Parse(xml);

                var items = from item in dom.Descendants("venue")
                            select new Venue
                            {
                                id = (string)item.Element("id"),
                                phone = (string)item.Element("phone"),
                                name = (string)item.Element("name"),
                                address = (string)item.Element("address"),
                                crossstreet = (string)item.Element("crossstreet"),
                                geolat = (string)item.Element("geolat"),
                                geolong = (string)item.Element("geolong"),
                                distance = (string)item.Element("distance"),
                                city = (string)item.Element("city"),
                                state = (string)item.Element("state"),
                                zip = (string)item.Element("zip"),
                                twitter = (string)item.Element("twitter"),
                            };

                return items.FirstOrDefault<Venue>();

            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region CheckIn

        public delegate void CheckInCallback(CheckInCompletedEventArgs e);

        /// <summary>
        ///  Allows you to check-in to a place.
        /// </summary>
        /// <param name="callback">receives results of the call</param>
        /// <param name="vid">(optional) ID of the venue where you want to check-in</param>
        /// <param name="venue">(optional) not necessary if you are 'shouting' or have a vid) if you don't have a venue ID or would rather prefer a 'venueless' checkin, pass the venue name as a string using this parameter. it will become an 'orphan' (no address or venueid but with geolat, geolong)</param>
        /// <param name="shout">(optional) a message about your check-in. the maximum length of this field is 140 characters</param>
        /// <param name="isPrivate">don't show your friends OR show everyone</param>
        /// <param name="sendToTwitter">send to Twitter OR don't send to Twitter</param>
        /// <param name="sendToFacebook">send to Facebook OR don't send to Facebook</param>
        /// <param name="geoLat">(optional, but recommended) latitude</param>
        /// <param name="getLong">(optional, but recommended) longitude</param>
        public static void CheckIn(CheckInCallback callback, string vid, string venue, string shout, bool isPrivate, bool sendToTwitter, bool sendToFacebook, double geoLat, double getLong)
        {
            if (UseFakeData)
            {
                var checkIns = ParseCheckIn(GetCheckInMock);
                callback(new CheckInCompletedEventArgs
                {
                    Cancelled = false,
                    Error = null,
                    Result = checkIns,
                });
                return;
            }

            string url = "http://api.foursquare.com/v1/checkin";

            shout = Uri.EscapeDataString(shout ?? string.Empty);
            venue = Uri.EscapeDataString(venue ?? string.Empty);


            var data = string.Format("vid={0}&venue={1}&shout={2}&private={3}&twitter={4}&facebook={5}&geolat={6}&geolong{7}", vid, venue, shout, isPrivate?1:0, sendToTwitter?1:0, sendToFacebook?1:0, geoLat, getLong);

            var webClient = GetWebClient(true);
            webClient.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            webClient.UploadStringCompleted += new UploadStringCompletedEventHandler(
                delegate(object sender, UploadStringCompletedEventArgs e)
                {
                    CheckInCallback cb = e.UserState as CheckInCallback;
                    if (e.Error == null)
                    {
                        try
                        {
                            var checkIns = ParseCheckIn(e.Result);
                            cb(new CheckInCompletedEventArgs
                            {
                                Cancelled = e.Cancelled,
                                Error = e.Error,
                                Result = checkIns,
                            });
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        Debug.WriteLine(e.Result);
                    }
                    else
                    {
                        cb(new CheckInCompletedEventArgs
                        {
                            Cancelled = e.Cancelled,
                            Error = e.Error,
                            Result = null,
                        });
                        Debug.WriteLine(e.Error);
                    }
                });

            webClient.UploadStringAsync(new Uri(url), "POST", data, callback);
        }

        private static CheckIn ParseCheckIn(string xml)
        {
            try
            {
                var dom = XDocument.Parse(xml);

                var items = from item in dom.Descendants("checkin")
                            select new CheckIn
                            {
                                id = (string)item.Element("id"),
                                message = (string)item.Element("message"),
                                created = (string)item.Element("created"),
                            };

                return items.FirstOrDefault<CheckIn>();

            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}