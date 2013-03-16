using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Specialized;

namespace MME.Hercules
{
    class FileUtility
    {
        public static void WriteResult(Session currentSession, string question, string answer)
        {
            FileInfo fi = new FileInfo("Skins\\" + ConfigUtility.Skin + "\\output.csv");

            using (StreamWriter sw = fi.AppendText())
            {
                sw.WriteLine(string.Format("{0},{1},\"{2}\",\"{3}\"",
                currentSession.ID.ToString(),
                DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(),
                question,
                answer));

                sw.Close();
            }            
        }

        private Image ResizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        public static Bitmap MakeGrayscale(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][] 
      {
         new float[] {.3f, .3f, .3f, 0, 0},
         new float[] {.59f, .59f, .59f, 0, 0},
         new float[] {.11f, .11f, .11f, 0, 0},
         new float[] {0, 0, 0, 1, 0},
         new float[] {0, 0, 0, 0, 1}
      });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //System.Windows.Forms.MessageBox.Show(original.Width.ToString());

            //draw the original image on the new image
            //using the grayscale color matrix
            try
            {
                g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                   0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            //dispose the Graphics object
            
            //gw...
            //g.Dispose();
            //gw...
            return newBitmap;
        }


        public static Bitmap LoadBitmap(string path)
        {
            //Open file in read only mode
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            //Get a binary reader for the file stream
            using (BinaryReader reader = new BinaryReader(stream))
            {
                //copy the content of the file into a memory stream
                var memoryStream = new MemoryStream(reader.ReadBytes((int)stream.Length));
                //make a new Bitmap object the owner of the MemoryStream
                return new Bitmap(memoryStream);
            }
        }

        public static string HerculesUpload(string uniqueid, string email, string file, string filename)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("imageid", uniqueid);
            nvc.Add("email", email);
            nvc.Add("event", ConfigUtility.Skin);

            System.Net.WebClient Client = new System.Net.WebClient();

            Client.Headers.Add("Content-Type", "binary/octet-stream");

            byte[] result = Client.UploadFile(ConfigUtility.GetConfig(ConfigUtility.Config, "HerculesPublishUrl"), "POST", file);

            string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length); 

            return "1";
        }

        public static string DJRequest(string song)
        {
            String prefix = ConfigUtility.GetConfig(ConfigUtility.Config, "TablePrefix");

            String email = ConfigUtility.GetConfig(ConfigUtility.Config, "RequestEmail");

            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("email", email);
            nvc.Add("song", prefix + song);



            String url = ConfigUtility.GetConfig(ConfigUtility.Config, "DJRequestUrl");

            String str =  HttpUpload(url, nvc);

            return str;
        }

        public static string PostPublishUploadURL(string uniqueid,
            String eventstr, string file, string filename, string location,
                String emailaddr, String fblogin, String datestr, string url)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("imageid", uniqueid);
            nvc.Add("event", eventstr);
            nvc.Add("location", location);
            nvc.Add("email", emailaddr);
            nvc.Add("fblogin", fblogin);
            nvc.Add("date", datestr);

            return HttpUploadFile(url, file, filename, "photo", "image/jpeg", nvc);
        }

        public static string PostPublishUploadWithUrl(string uniqueid, string email, string file, string filename, string url)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("imageid", uniqueid);
            nvc.Add("email", email);
            nvc.Add("event", ConfigUtility.Skin);

            return HttpUploadFile(url, file, filename, "photo", "image/jpeg", nvc);
        
        }

        public static string PostPublishUpload(string uniqueid, string file, string filename)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("imageid", uniqueid);
            nvc.Add("event", ConfigUtility.Skin);

            return HttpUploadFile(ConfigUtility.GetConfig(ConfigUtility.Config, "AutolycusPublishUrl"), file, filename, "photo", "image/jpeg", nvc);
        }

        public static string PostPublishUpload(string uniqueid, string email, string file, string filename)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("imageid", uniqueid);
            nvc.Add("email", email);
            nvc.Add("event", ConfigUtility.Skin);

            return HttpUploadFile(ConfigUtility.GetConfig(ConfigUtility.Config, "EmailPublishUrl"), file, filename, "photo", "image/jpeg", nvc);
        }

        public static string Promo(string uniqueid, string email)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("imageid", uniqueid);
            nvc.Add("email", email);
            nvc.Add("event", ConfigUtility.Skin);

            String url = ConfigUtility.GetConfig(ConfigUtility.Config, "PromoUrl");
            return HttpUpload(url,nvc);
        }

        public static string HttpUpload(string url, NameValueCollection nvc )
        {
             //gw
            if (url=="") return "";
            //gw

            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);
            WebResponse wresp = null;
            string response = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                response = reader2.ReadToEnd();                
            }
            catch (Exception ex)
            {
                response = "ERROR: " + ex.Message;
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }

            return response;
        }

        

        public static string HttpUploadFile(string url, string file, string filename, string paramName, string contentType, NameValueCollection nvc)
        {
            //gw
            if (url=="") return "";
            //gw

            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            //System.Windows.Forms.MessageBox.Show("trying to open " + file);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            string response = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                response = reader2.ReadToEnd();                
            }
            catch (Exception ex)
            {
                response = "ERROR: " + ex.Message;
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }

            return response;
        }



        public static void ClearTempDirectory()
        {
            if (!Directory.Exists("Temp"))
                Directory.CreateDirectory("Temp");
            else
            {
                System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo("Temp");
                foreach (FileInfo file in downloadedMessageInfo.GetFiles()) { file.Delete(); } 
            }
        }
    }
}
