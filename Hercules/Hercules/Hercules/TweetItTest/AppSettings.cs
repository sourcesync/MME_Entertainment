// AppSettings.cs
// ------------------------------------------------------------------
//
// Manages application "settings" for TweetIt. These are stored in the
// AppData folder for the user, typically
// c:\users\NAME\AppData\Roaming\Dino Chiesa\TweetIt\settings.xml. The
// main things that get stored are the oauth access token and token
// secret. This allows a simpler, quicker flow, after the initial run of
// the app.
//
// Author     : Dino
// Created    : Mon Oct 10 18:31:41 2011
// Last Saved : <2012-May-10 17:32:09>
//
// ------------------------------------------------------------------
//
// Copyright (c) 2011,2012 by Dino Chiesa
// All rights reserved!
//
// ------------------------------------------------------------------
//

using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

namespace Ionic.Twitter
{
    sealed class AppSettings
    {
        private readonly static AppSettings _instance = AppSettings.Load();
        public static AppSettings Instance  { get { return _instance; } }
        static AppSettings() { /* required for lazy init */ }

        private AppSettings()
        {
            LastSaved = System.DateTime.FromFileTimeUtc(0L);
        }

        public string Token;  // [XmlElement("oauth_access_token")]
        public string Secret; // [XmlElement("oauth_token_secret")]

        public DateTime LastSaved;

        private static string fullpath;
        private static string FullPath
        {
            get
            {
                if (fullpath==null)
                {
                    var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                    var apppath = Path.Combine(appdata, "Dino Chiesa");

                    apppath = Path.Combine(apppath, "TweetIt");
                    fullpath= Path.Combine(apppath, "settings.xml");
                }
                return fullpath;
            }
        }

        public bool Completed
        {
            get
            {
                return (this.Token != null) &&  (this.Secret != null);
            }
        }


        private static AppSettings Load()
        {
            if (!File.Exists(FullPath))
                return new AppSettings();

            var tr = new XmlTextReader(FullPath);
            var xdoc = XDocument.Load(tr);

            var settings = new AppSettings();
            var r = xdoc.Descendants("oauth_access_token");
            if (r.Count()==1)
                settings.Token = r.First().Value;

            r = xdoc.Descendants("oauth_token_secret");
            if (r.Count()==1)
                settings.Secret = r.First().Value;

            r = xdoc.Descendants("LastSaved");
            if (r.Count()==1)
                settings.LastSaved = DateTime.Parse(r.First().Value);

            return settings;
        }


        public void Save()
        {
            // ensure directory exists
            var dir = Path.GetDirectoryName(FullPath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            this.LastSaved = DateTime.Now;
            var xdoc = new XDocument
                (new XElement("AppSettings",
                              new XElement("oauth_access_token", this.Token),
                              new XElement("oauth_token_secret", this.Secret),
                              new XElement("LastSaved", this.LastSaved.ToString())));
            xdoc.Save(FullPath);
        }

    }

}
