#define Trace

// TweetIt.cs
// ------------------------------------------------------------------
//
// Post a status + optional image to twitter from the Windows command
// line.
//
// This code is licensed under the MS-PL. See the accompanying
// License.txt file for details.
//
//
// Author     : Dino
// Created    : Mon Oct 10 18:31:41 2011
// Last Saved : <2012-May-11 15:38:45>
//
// ------------------------------------------------------------------
//
// Copyright (c) 2011,2012 by Dino Chiesa
// All rights reserved!
//
// ------------------------------------------------------------------
//
// flymake: nmake CheckSyntax
//


using System;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Text;
using System.Reflection;

/*
// to allow fast ngen
[assembly: AssemblyTitle("TweetIt.cs")]
[assembly: AssemblyDescription("Tweets a message (and optionally an image) from the Windows cmd line")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Dino Chiesa")]
[assembly: AssemblyProduct("Tools")]
[assembly: AssemblyCopyright("Copyright © Dino Chiesa 2011,2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("2012.5.11.1")]
*/

namespace Ionic.Twitter
{
    public class TweetIt
    {
        bool verbose;
        string message;
        string imageFile;
        string twitterUrl1 = "http://api.twitter.com/1/statuses/update.xml?status=";
        string twitterUrl2 = "https://upload.twitter.com/1/statuses/update_with_media.xml";

        // Attention: These values are specific to the TweetIt app.
        // Each app should have its own values here, so the approval
        // dialog presented by Twitter reports the correct app name.
        // To get your own consumer key and secret for YOUR APP, visit
        // http://dev.twitter.com
        static readonly string TWITTER_CONSUMER_KEY    = "FXJ0DIH50S7ZpXD5HXlalQ",
            TWITTER_CONSUMER_SECRET = "sjTual5oaF7tx6SUwBwyckdCx5aCycE9XkUIGc8CdE";

        OAuth.Manager oauth;
        AppSettings settings = AppSettings.Instance;

        public TweetIt () {}
        public TweetIt (string[] args)
        {
            for (int i=0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-v":
                        verbose= true;
                        break;

                    case "-i":
                        i++;
                        if (args.Length <= i) throw new ArgumentException(args[i-1]);
                        imageFile = args[i];
                        break;

                    default:
                        if (message != null)
                            throw new ArgumentException(args[i]);

                        message = args[i];
                        break;
                }
            }
        }

        private string GetTwitterUpdateUrl()
        {
            // target URL varies depending on whether there is an image to upload.
            return (imageFile == null)
                ? twitterUrl1 + OAuth.Manager.UrlEncode(message)
                : twitterUrl2;
        }

        private static string GetMimeType(String filename)
        {
            var extension = System.IO.Path.GetExtension(filename).ToLower();
            var regKey =  Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(extension);

            string result =
                ((regKey != null) && (regKey.GetValue("Content Type") != null))
                ? regKey.GetValue("Content Type").ToString()
                : "image/unknown" ;
            return result;
        }

        private static string GetMultipartBoundary()
        {
            return "~~~~~~" +
                Guid.NewGuid().ToString().Substring(18).Replace("-","") +
                "~~~~~~";
        }

        private void Tweet()
        {
            var url = GetTwitterUpdateUrl();
            var request = (HttpWebRequest)WebRequest.Create(url);
            var authzHeader = oauth.GenerateAuthzHeader(url, "POST");
            request.Method = "POST";
            request.PreAuthenticate = true;
            request.AllowWriteStreamBuffering = true;
            request.Headers.Add("Authorization", authzHeader);

            if (imageFile != null)
            {
                string boundary = GetMultipartBoundary(),
                    separator = "--" + boundary,
                    footer = "\r\n" + separator + "--\r\n",
                    shortFileName = Path.GetFileName(imageFile),
                    fileContentType = GetMimeType(shortFileName),
                    fileHeader = string.Format("Content-Disposition: file; " +
                                               "name=\"media\"; filename=\"{0}\"",
                                               shortFileName);
                var encoding = System.Text.Encoding.GetEncoding("iso-8859-1");

                var contents = new System.Text.StringBuilder();
                contents.AppendLine(separator)
                    .AppendLine("Content-Disposition: form-data; name=\"status\"")
                    .AppendLine();
                contents.AppendLine(message);
                contents.AppendLine(separator);
                contents.AppendLine(fileHeader);
                contents.AppendLine(string.Format("Content-Type: {0}", fileContentType));
                contents.AppendLine();

                // actually send the request
                request.ServicePoint.Expect100Continue = false;
                request.ContentType = "multipart/form-data; boundary=" + boundary;

                using (var s = request.GetRequestStream())
                {
                    byte[] bytes = encoding.GetBytes(contents.ToString());
                    s.Write(bytes, 0, bytes.Length);
                    bytes = File.ReadAllBytes(imageFile);
                    s.Write(bytes, 0, bytes.Length);
                    bytes = encoding.GetBytes(footer);
                    s.Write(bytes, 0, bytes.Length);
                }
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    MessageBox.Show("There's been a problem trying to tweet:" +
                                    Environment.NewLine +
                                    response.StatusDescription +
                                    Environment.NewLine +
                                    Environment.NewLine +
                                    "You will have to tweet manually." +
                                    Environment.NewLine);
            }
        }

        private bool VerifyAuthentication()
        {
            if (!settings.Completed)
            {
                var dlg = new TwitterAppApprovalForm(this.oauth);
                dlg.ShowDialog();
                if (dlg.DialogResult == DialogResult.OK)
                {
                    settings.Token = this.oauth["token"];
                    settings.Secret = this.oauth["token_secret"];
                    settings.Save();
                    if (verbose)
                        Console.WriteLine("the token and secret have been stored.");
                }
            }
            else
            {
                if (verbose)
                {
                    Console.WriteLine("the token and secret have been retrieved from storage.");
                    Console.WriteLine("token       : {0}", settings.Token);
                    Console.WriteLine("token secret: {0}", settings.Secret);
                }
            }

            if (!settings.Completed)
            {
                MessageBox.Show("You must approve this app for use with Twitter\n" +
                                "before updating your status with it.\n\n",
                                "No Authorizaiton for TweetIt",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return false;
            }

            // apply stored information into the oauth manager
            oauth["token"]           = settings.Token;
            oauth["token_secret"]    = settings.Secret;

            return true;
        }


        void SetupOAuth()
        {
            oauth = new OAuth.Manager();
            oauth["consumer_key"] = TWITTER_CONSUMER_KEY;
            oauth["consumer_secret"] = TWITTER_CONSUMER_SECRET;
        }


        public void Run()
        {
            if (message == null)
            {
                Usage();
                return;
            }

            SetupOAuth();

            if (VerifyAuthentication())
            {
                Tweet();
            }
        }

        public static void Usage()
        {
            Console.WriteLine("\nTweetIt: Post a Tweet from the command line.\n");
            Console.WriteLine("Usage:\n  TweetIt [options] \"message here\"");
            Console.WriteLine("  options:\n" +
                              "    -v               verbose\n" +
                              "    -i <imagefile>   post an image with the tweet.\n");
        }

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                new TweetIt(args)
                    .Run();
            }
            catch (System.Exception exc1)
            {
                Console.WriteLine("Exception: {0}", exc1.ToString());
                Usage();
            }
        }
    }
}
