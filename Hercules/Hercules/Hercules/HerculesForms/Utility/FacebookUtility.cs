﻿using System;
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
            /*
            try
            {
                HttpWebRequest restRequest;
                HttpWebResponse restResponse;

                string uurl = "https://graph.facebook.com/me/feed?access_token={0}";
                string accessUrl = String.Format(uurl, access_token as string);

                restRequest = (HttpWebRequest)WebRequest.Create(accessUrl);
                restRequest.Method = "POST";

                var body = Encoding.UTF8.GetBytes("message=test post");

                restRequest.ContentLength = body.Length;

                Stream postStream = restRequest.GetRequestStream();
                postStream.Write(body, 0, body.Length);
                postStream.Close();
            }
            catch (System.Exception e)
            {
            }
            return;
            */


            NameValueCollection nvc = new NameValueCollection();

            theRequest = WebRequest.Create("https://graph.facebook.com/me/feed");
            theRequest.Method = "POST";
            theRequest.ContentType = "text/html";
        
            /*GW
            string pphotourl = System.Web.HttpUtility.UrlEncode(  photourl );
            string pmsg = System.Web.HttpUtility.UrlEncode(msg);
            string pdesc = System.Web.HttpUtility.UrlEncode(desc);
            string purl = System.Web.HttpUtility.UrlEncode(url);
            string pcaption = System.Web.HttpUtility.UrlEncode(caption);
           

           // string Parameters = string.Format("access_token={0}&picture={1}",
           //     access_token, pphotourl);

            
          
         
            // Build a string containing all the parameters
            string Parameters = string.Format("access_token={0}&picture={1}&message={2}&description={3}&link={4}&caption={5}",
                access_token,
                //photourl,
                pphotourl,
                //msg,
                pmsg,
                //desc,
                pdesc,
                //url,
                purl,
                //caption);
                pcaption);
             

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
            catch (System.Exception e)
            {
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
             * */
        }
    }
}