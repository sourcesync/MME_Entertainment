using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Xml;

namespace MME.Hercules
{
    public class FacebookUtility
    {
        // These are for hercules opn
        public static string Secret = "d1241549f2f69ddba1e7416633a7b86d";
        public static string AppId = "312129408847352";

        public static WebRequest theRequest;
		public static HttpWebResponse theResponse;


        public static string GetConfig(string key)
        {
            if (ConfigUtility.FacebookConfig == null) return String.Empty;

            XmlNodeList xnList = ConfigUtility.FacebookConfig.SelectNodes("/Hercules/string[@key='" + key.ToUpper() + "']");

            if (xnList.Count > 0)
                return xnList[0].Attributes["value"].Value;

            return String.Empty;
        }



        public static void Authenticate(string email, string pw)
        {
            WebClient wc = new WebClient();
           

            string url = string.Format("client_id={0}&client_secret={1}&grant_type=client_credentials",
                AppId,
                Secret);
        }


        public static void PhotoUpload(string access_token, string msg, string fullfile)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("access_token", access_token);
            nvc.Add("message", msg);
            nvc.Add("source", fullfile);

            FileUtility.HttpUploadFile("https://graph.facebook.com/me/photos", fullfile, "", "photo", "image/jpeg", nvc);
        }

        public static void PostWall(string access_token, string photourl, string msg, string caption, string desc, string url)
        {
            NameValueCollection nvc = new NameValueCollection();

            theRequest = WebRequest.Create("https://graph.facebook.com/me/feed");
            theRequest.Method = "POST";
            theRequest.ContentType = "text/html";
        
            // Build a string containing all the parameters
            string Parameters = string.Format("access_token={0}&picture={1}&message={2}&description={3}&link={4}&caption={5}",
                access_token,
                photourl,
                msg,
                desc,
                url,
                caption);

            // We write the parameters into the request
            bool success = true;

            try
            {
                using (StreamWriter sw = new StreamWriter(theRequest.GetRequestStream()))
                {
                    sw.Write(Parameters);
                    sw.Close();
                }

                // Execute the query
                theResponse = (HttpWebResponse)theRequest.GetResponse();

                using (StreamReader sr = new StreamReader(theResponse.GetResponseStream()))
                {
                    string response = sr.ReadToEnd();
                    sr.Close();
                }


            }
            catch {
                success = false;
            }

            // let's try trice
            if (!success)
            {                
                success = true;
                try
                {
                    using (StreamWriter sw = new StreamWriter(theRequest.GetRequestStream()))
                    {
                        sw.Write(Parameters);
                        sw.Close();
                    }

                    // Execute the query
                    theResponse = (HttpWebResponse)theRequest.GetResponse();

                    using (StreamReader sr = new StreamReader(theResponse.GetResponseStream()))
                    {
                        string response = sr.ReadToEnd();
                        sr.Close();
                    }

                }
                catch
                {
                }

            }            
        }
    }
}
