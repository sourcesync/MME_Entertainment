 using System;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.IO;
using System.Xml;

namespace MME.Hercules.Forms.User
{
    public partial class Developing : Form
    {
        private Session currentSession;
        private Image photo;
        private bool istable;

        public Developing(Session currentSession)
        {
            Application.DoEvents();

            InitializeComponent();
            this.currentSession = currentSession;
        }


        private void Finished_Load(object sender, EventArgs e)
        {
            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;


            if (ConfigUtility.GetValue("BoothType") == "2")
            {
                istable = true;
            }

            WindowUtility.SetScreen(pb, Hercules.Properties.Resources.THANKS_TAKING_PHOTOS_SCREEN);

            this.Refresh();

            SoundUtility.PlaySync(Hercules.Properties.SoundResources.THANK_YOU_FOR_TAKING_PHOTOS);

            if (!istable)
            {
                WindowUtility.SetScreen(pb, Hercules.Properties.Resources.DEVELOPING_PICS_SCREEN);

                this.Refresh();

                SoundUtility.Play(Hercules.Properties.SoundResources.DEVELOPING_PLEASE_WAIT);
            }

            WriteOutResults();

            CustomEmail();

            PublishToFacebook();

            if (this.istable)
            {
            }
            else
            {

                if ((!ConfigUtility.GetConfig(ConfigUtility.Config, "AllowFacebookPublish").Equals("1")) &&
                (!ConfigUtility.GetConfig(ConfigUtility.Config, "AllowEmailPublish").Equals("1")))
                {
                    Thread.Sleep(6000);
                }

                // turn on vanity light
                //gw
                //PhidgetUtility.Relay(Convert.ToInt32(ConfigUtility.GetValue("PhidgetRelay_VanityLight")),true);
            }

           
                WindowUtility.SetScreen(pb, Hercules.Properties.Resources.PHOTOS_COMPLETE_SCREEN);
            

                this.Refresh();
                SoundUtility.Play(Hercules.Properties.SoundResources.PHOTOS_COMPLETE);

                Thread.Sleep(2100);   

                //if (istable) 
                {
                    this.DialogResult = DialogResult.OK;
                }
            
        }


        private void WriteOutResults()
        {
            FileInfo fi = new FileInfo("Skins\\" + ConfigUtility.Skin + "\\output.csv");

            XmlNodeList xnList = null;
            
            if (ConfigUtility.SequenceConfig != null)
                xnList = ConfigUtility.SequenceConfig.SelectNodes("/Hercules/page");

            // write out headers if file doesn't exist
            if (!fi.Exists)
            {
                using (StreamWriter sw = fi.CreateText())
                {
                    sw.Write("Session,Date/Time,ColorType,");

                    if ((ConfigUtility.GetConfig(ConfigUtility.Config, "AllowEmailPublish").Equals("1")))
                        sw.Write("EmailAddress,");

                    if (xnList != null)
                    foreach (XmlNode node in xnList)
                    {
                        sw.Write("\"" + node.Attributes["question"].Value + "\",");
                    }

                    sw.WriteLine();
                    sw.Close();
                }
            }

            // write out headers if file doesn't exist
            using (StreamWriter sw = fi.AppendText())
            {
                sw.Write(string.Format("{0},{1},",
                    currentSession.ID.ToString(),
                     DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()));

                sw.Write(this.currentSession.SelectedColorType == ColorType.Color ? "Color," : "BW,"); 

                if ((ConfigUtility.GetConfig(ConfigUtility.Config, "AllowEmailPublish").Equals("1")))
                    sw.Write(this.currentSession.EmailAddress + ",");

                int cnt = 0;

                if (this.currentSession.Responses != null && this.currentSession.Responses.Count > 0)
                foreach (XmlNode node in xnList)
                {
                    sw.Write(string.Format("\"{0}\",",
                        this.currentSession.Responses[cnt]));

                    cnt++;
                }


                sw.WriteLine();
                sw.Close();
            }            


        }




        private void CustomEmail()
        {
            if (this.currentSession.FavoritePhoto == 0)
                return;
            
            // load the template            
            Bitmap template = new Bitmap("Skins\\" + ConfigUtility.Skin + "\\Templates\\email.jpg");

            using (Graphics grfx = Graphics.FromImage(template))
            {
                for (int i=0; i<=ConfigUtility.PhotoCount - 1; i++)
                {
                    Bitmap photo = new Bitmap(this.currentSession.PhotoPath + "\\photo" + this.currentSession.FavoritePhoto + ".jpg");
                    photo.RotateFlip(RotateFlipType.Rotate90FlipNone);

                    System.Drawing.Image mini = photo.GetThumbnailImage(268, 387, null, IntPtr.Zero);

                    // Clear handle to original file so that we can overwrite it if necessary
                    photo.Dispose();

                    grfx.DrawImage(mini, 25, 20, mini.Width, mini.Height);
                    mini.Dispose();
                }
            }

            if (this.currentSession.SelectedColorType == ColorType.BW)
                template = FileUtility.MakeGrayscale((System.Drawing.Bitmap)template);
               
            template.Save(this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename + ".jpg", ImageFormat.Jpeg);
            template.Dispose();         

            // create email textfile
            if (!string.IsNullOrEmpty(this.currentSession.EmailAddress))
                System.IO.File.WriteAllText(this.currentSession.PhotoPath + "\\email.txt", this.currentSession.EmailAddress);

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "EmailPublishUrl")))
            {
                FileUtility.PostPublishUpload(this.currentSession.FavoritePhotoFilename, this.currentSession.EmailAddress, this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename + ".jpg", "");
                ConfigUtility.IncrementCounter("Email");
            }

            if (ConfigUtility.GetValue("UploadFavoritePhotoToAutolycus").Equals("1"))
            {
                FileUtility.PostPublishUpload(this.currentSession.FavoritePhotoFilename, this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename + ".jpg", "");
            }


        }
        
     


        private void PublishToFacebook()
        {
            if ((ConfigUtility.GetConfig(ConfigUtility.Config, "AllowFacebookPublish").Equals("1")) && !string.IsNullOrEmpty(this.currentSession.FacebookAccessToken))
            {
                string response = string.Empty;
                // upload to publish server (don't need to send the email if it was sent custom way 
                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "EmailPublishUrl")))
                {
                    // try 3 times
                    response = FileUtility.HerculesUpload(DateTime.Now.Ticks.ToString(), "", this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename + ".jpg", "");

                    if (response != "1")
                        response = FileUtility.HerculesUpload(DateTime.Now.Ticks.ToString(), "", this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename + ".jpg", "");

                    if (response != "1")
                    {
                        Thread.Sleep(500);
                        response = FileUtility.HerculesUpload(DateTime.Now.Ticks.ToString(), "", this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename + ".jpg", "");
                    }

                }
                else if (!string.IsNullOrEmpty(this.currentSession.EmailAddress))
                {
                    // try 3 times
                    response = FileUtility.HerculesUpload(DateTime.Now.Ticks.ToString(), this.currentSession.EmailAddress, this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename.ToString() + ".jpg", "");

                    if (response != "1")
                        response = FileUtility.HerculesUpload(DateTime.Now.Ticks.ToString(), this.currentSession.EmailAddress, this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename.ToString() + ".jpg", "");

                    if (response != "1")
                    {
                        Thread.Sleep(500);
                    }

                    ConfigUtility.IncrementCounter("Email");
                }

                string photoUrl = string.Empty;

                //gw
                string cfg = ConfigUtility.GetConfig(ConfigUtility.Config, "PublicPhotoUrl");
                //gw

                photoUrl = string.Format(System.Web.HttpUtility.HtmlDecode(ConfigUtility.GetConfig(ConfigUtility.Config, "PublicPhotoUrl")),
                    this.currentSession.FavoritePhotoFilename,
                    ConfigUtility.Skin);

                string link = string.Empty;

                if (FacebookUtility.GetConfig("link").Equals("$CUSTOM1$"))
                    link = string.Format(ConfigUtility.GetConfig(ConfigUtility.Config, "CustomPublishUrl"),                        
                        this.currentSession.FavoritePhotoFilename,
                        ConfigUtility.Skin);
                else
                    link = FacebookUtility.GetConfig("link");


                // now tell facebook to make the post
                FacebookUtility.PostWall(this.currentSession.FacebookAccessToken,
                        photoUrl,
                        FacebookUtility.GetConfig("title"),
                        "",
                        FacebookUtility.GetConfig("desc"),
                        link);


                ConfigUtility.IncrementCounter("Facebook");
            }
        }


    
        private void Finished_FormClosed(object sender, FormClosedEventArgs e)
        {
            SoundUtility.Stop();
        }
    }
}
