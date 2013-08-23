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
        public bool ischeckin = false;
        public bool ispromo = false;
        public bool isbooth = false;
        public bool facebook_publish = false;
        public System.Timers.Timer timer = null;

        public Developing(Session currentSession)
        {
            //Application.DoEvents();

            InitializeComponent();
            this.currentSession = currentSession;
        }


        private void Finished_Load(object sender, EventArgs e)
        {
            
           
            this.Invoke(new System.EventHandler(this._Finished_Load));
            
        }

        

        private void _Finished_Load(object sender, EventArgs e)
        {
            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;


            if (ConfigUtility.GetValue("BoothType") == "2")
            {
                istable = true;
            }


            if (!istable)
            {
                WindowUtility.SetScreen(pb, Hercules.Properties.Resources.THANKS_TAKING_PHOTOS_SCREEN);

                // adjust form/pb sizes...
                Size sz = WindowUtility.GetScreenSize(Hercules.Properties.Resources.THANKS_TAKING_PHOTOS_SCREEN);
                this.Size = sz;
                pb.Size = sz;

                this.Refresh();
                this.Invalidate();

                SoundUtility.Play(Hercules.Properties.SoundResources.THANK_YOU_FOR_TAKING_PHOTOS);
                Application.DoEvents();
                Thread.Sleep(1);

                this.label1.Visible = false;

            }
            else
            {
                if (this.ischeckin)
                {
                }
                else if (!this.ispromo)
                {
                    WindowUtility.SetScreen(pb, Hercules.Properties.Resources.DEVELOPING_PICS_SCREEN);
                }
                else
                {
                    WindowUtility.SetScreen(pb, Hercules.Properties.Resources.PROMO_COMPLETE);
                }
            }

            this.Refresh();
            Application.DoEvents();
            System.Threading.Thread.Sleep(1);
           

            if (istable)
            {
                //pb.Load(string.Format("Skins\\{0}\\Screens\\facebook.jpg",
                 //   ConfigUtility.Skin));

                pb.Load(string.Format("Skins\\{0}\\Screens\\generic-bg.jpg", ConfigUtility.Skin));
                //GW this.label1.Parent = this.pb;
                this.label1.BackColor = System.Drawing.Color.Transparent;
                String str = ConfigUtility.GetConfig(ConfigUtility.Config, "TEXT_COLOR");
                this.label1.ForeColor = ColorTranslator.FromHtml(str);
                this.label1.BringToFront();
                this.label1.Visible = true;
                this.label1.AutoSize = true;
                if (this.ischeckin)
                    this.label1.Text = "Checking-In...Please Wait...";
                else
                {
                    bool bypass_developing_text = false;
                    if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "BypassDevelopingText")))
                    {
                        bypass_developing_text = true;
                    }
                    if (bypass_developing_text)
                        this.label1.Visible = false;
                    else
                        this.label1.Visible = true;
                    
                    this.label1.Text = "Developing Photos...Please Wait...";
                }
                int space = (int)(1024 - this.label1.Size.Width);
                this.label1.Location = new Point( (int)(space/2.0), this.label1.Location.Y);  
            }

            this.Invalidate();
            this.Refresh();
            Application.DoEvents();
            //Thread.Sleep(2000);
            System.Threading.Thread.Sleep(1);
           // Thread.Sleep(2000);

            if (!istable)
            {
                // SoundUtility.Play(Hercules.Properties.SoundResources.DEVELOPING_PLEASE_WAIT);
            }

            this.Refresh();
            Application.DoEvents();

            this.backgroundWorker1.RunWorkerAsync();
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);

            return;


            //
            //  We Stopped doing all this...
            //

            if (!istable)
            {

                if ((!ConfigUtility.GetConfig(ConfigUtility.Config, "AllowFacebookPublish").Equals("1")) &&
                (!ConfigUtility.GetConfig(ConfigUtility.Config, "AllowEmailPublish").Equals("1")))
                {
                    Thread.Sleep(6000);
                }

                // turn on vanity light
                //gw
                //PhidgetUtility.Relay(Convert.ToInt32(ConfigUtility.GetValue("PhidgetRelay_VanityLight")),true);

                if (this.ischeckin)
                {

                }
                else if (this.ispromo)
                {
                    WindowUtility.SetScreen(pb, Hercules.Properties.Resources.PROMO_COMPLETE);

                }
                else
                {
                    WindowUtility.SetScreen(pb, Hercules.Properties.Resources.PHOTOS_COMPLETE_SCREEN);


                }

                this.Refresh();

                if (!this.ischeckin)
                {
                    SoundUtility.Play(Hercules.Properties.SoundResources.PHOTOS_COMPLETE);

                    Thread.Sleep(2100);
                }
            }
            else // is table
            {
                if (this.ischeckin)
                {
                    WindowUtility.SetScreen(pb, "generic-bg.jpg");
                }
                else if (this.ispromo)
                {
                    WindowUtility.SetScreen(pb, Hercules.Properties.Resources.PROMO_COMPLETE);
                }
                else
                {
                    WindowUtility.SetScreen(pb, Hercules.Properties.Resources.PHOTOS_COMPLETE_SCREEN);
                }

                this.label1.Parent = this.pb;
                this.label1.BackColor = System.Drawing.Color.Transparent;
                String str = ConfigUtility.GetConfig(ConfigUtility.Config, "TEXT_COLOR");
                this.label1.ForeColor = ColorTranslator.FromHtml(str);
                //this.label1.ForeColor = System.Drawing.Color.White;
                this.label1.BringToFront();
                this.label1.Visible = true;
                this.label1.AutoSize = true;
                    this.label1.Visible = true;
                    if (this.ischeckin)
                    {
                        String tstr = ConfigUtility.GetConfig(ConfigUtility.Config, "CheckinThankyouText");
                        this.label1.Text = tstr;
                    }
                    else if (this.ispromo)
                    {
                        bool bypass_developing_text = false;
                        if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "BypassThanksText")))
                        {
                            bypass_developing_text = true;
                        }
                        if (bypass_developing_text)
                            this.label1.Visible = false;
                        else
                            this.label1.Visible = true;
                        this.label1.Text = "Your Promotional Offer Will Be Emailed To You Shortly.";
                    }
                    else
                    {
                        bool bypass_developing_text = false;
                        if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "BypassThanksText")))
                        {
                            bypass_developing_text = true;
                        }
                        if (bypass_developing_text)
                            this.label1.Visible = false;
                        else
                            this.label1.Visible = true;

                        this.label1.Text = "Thanks!";
                        if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "ThanksText")))
                        {
                            this.label1.Text = ConfigUtility.GetConfig(ConfigUtility.Config, "ThanksText");
                        }
                    }
                    int space = (int)(1024 - this.label1.Size.Width);
                    this.label1.Location = new Point( (int)(space/2.0), this.label1.Location.Y);
                    this.Refresh();
                    Application.DoEvents();
                    Thread.Sleep(2900);
            }

            this.Refresh();
           
            this.DialogResult = DialogResult.OK;
            
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Invoke(new System.EventHandler(this._Done));
        }

        void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.commit_data();
        }

        void commit_data()
        {
            if (!istable)
            {
                WriteOutResults();

                CustomEmail();

                PublishToFacebook();
            }
            else
            {
                if (ischeckin)
                {
                    CheckinToFacebook();
                }

                if (ispromo)
                {
                    PromoEmail();
                }

                if (isbooth)
                {
                    WriteOutResults();

                    CustomEmail();

                    PublishToFacebook();
                }
            }
        }


         
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
           
            this.timer.Stop();

            this.Invoke(new System.EventHandler(this._Done));
        }

        void _Done(object o, EventArgs args)
        {
            this.DialogResult = DialogResult.OK;
            return;
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



        private void PromoEmail()
        {
           
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "PromoUrl")))
            {
                String code = ConfigUtility.GetConfig(ConfigUtility.Config, "PromoCode");
                FileUtility.Promo(code, this.currentSession.EmailAddress);
                //ConfigUtility.IncrementCounter("Email");
            }
        }

        private void CustomEmail()
        {
            if (this.currentSession.FavoritePhoto == 0)
                return;
            
            // load the template            
            Bitmap template = null;
            if (ConfigUtility.GetValue("CameraName").Equals("Web")) // web cam, not table
            {
                template = new Bitmap("Skins\\" + ConfigUtility.Skin + "\\Templates\\email-cropped.jpg");
            }
            else if (!this.istable)
            {
                template = new Bitmap("Skins\\" + ConfigUtility.Skin + "\\Templates\\email.jpg");
            }
            else
            {
                template = new Bitmap("Skins\\" + ConfigUtility.Skin + "\\Templates\\email-cropped.jpg");
            }

            using (Graphics grfx = Graphics.FromImage(template))
            {
                for (int i=0; i<=ConfigUtility.PhotoCount - 1; i++)
                {
                    String path = this.currentSession.PhotoPath + "\\photo" + this.currentSession.FavoritePhoto + ".jpg";
                    //System.Windows.Forms.MessageBox.Show("bitmap from fav " + path);

                    Bitmap photo = new Bitmap(path);

                    //345,258
                    if (ConfigUtility.GetValue("CameraName").Equals("Web")) // web cam, not table
                    {

                        System.Drawing.Image mini = photo.GetThumbnailImage(270, 218, null, IntPtr.Zero);

                        // Clear handle to original file so that we can overwrite it if necessary
                        photo.Dispose();

                        int embed_x = 25;
                        int embed_y = 20;
                        if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "TemplateEmbedX")))
                        {
                            embed_x = int.Parse(ConfigUtility.GetConfig(ConfigUtility.Config, "TemplateEmbedX"));
                        }

                        if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "TemplateEmbedY")))
                        {
                            embed_y = int.Parse(ConfigUtility.GetConfig(ConfigUtility.Config, "TemplateEmbedY"));
                        }

                        grfx.DrawImage(mini, embed_x, embed_y, mini.Width, mini.Height);
                        mini.Dispose();

                        mini.Dispose();
                    }
                    else  if (!this.istable) // normal
                    {
                        photo.RotateFlip(RotateFlipType.Rotate90FlipNone);

                        //320,480

                        System.Drawing.Image mini = photo.GetThumbnailImage(268, 387, null, IntPtr.Zero);

                        // Clear handle to original file so that we can overwrite it if necessary
                        photo.Dispose();

                        grfx.DrawImage(mini, 25, 20, mini.Width, mini.Height);
                        mini.Dispose();
                    }
                    else // vid table
                    {
                        //System.Drawing.Image mini = photo.GetThumbnailImage(270, 218, null, IntPtr.Zero);

                        System.Drawing.Image mini = photo.GetThumbnailImage(270, 218, null, IntPtr.Zero);

                        // Clear handle to original file so that we can overwrite it if necessary
                        photo.Dispose();

                        grfx.DrawImage(mini,
                            (int)((1024 - mini.Width) / 2.0f),
                            (int)((768 - mini.Height) / 2.0f),
                            mini.Width, mini.Height);

                        mini.Dispose();
                    }
                }
            }

            if (this.currentSession.SelectedColorType == ColorType.BW)
                template = FileUtility.MakeGrayscale((System.Drawing.Bitmap)template);

            String svpath = this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename + ".jpg";
            //System.Windows.Forms.MessageBox.Show("template save " + svpath);
            template.Save(svpath, ImageFormat.Jpeg);
            template.Dispose();         

            /*
            //  HACK FOR GOOGLE EVENT
            //  WE COPY THE ORIG TO THE DEST...
            String origpath = this.currentSession.PhotoPath + "\\photo" + this.currentSession.FavoritePhoto + ".jpg";
            String destpath = this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename + ".jpg";
            File.Delete(destpath);
            File.Copy(origpath, destpath);
            */

            // create email textfile
            if (!string.IsNullOrEmpty(this.currentSession.EmailAddress))
                System.IO.File.WriteAllText(this.currentSession.PhotoPath + "\\email.txt", this.currentSession.EmailAddress);

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "EmailPublishUrl")))
            {
                String p1 = this.currentSession.FavoritePhotoFilename;
                String p2 = this.currentSession.EmailAddress;
                String p3 = this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename + ".jpg";


                bool offline = (this.currentSession.ConfigOffline || this.currentSession.IsOffline);
                /*
                bool config_offline = false;
                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "OFFLINE")))
                {
                    String val = ConfigUtility.GetConfig(ConfigUtility.Config, "OFFLINE");
                    if (val == "1")
                        config_offline = true;
                }
                */
                if (!offline)
                {
                    // FileUtility.PostPublishUpload(this.currentSession.FavoritePhotoFilename, this.currentSession.EmailAddress,
                      //      this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename + ".jpg", "");

                    //  iterate over all emails...
                    String[] emails = this.currentSession.EmailAddress.Split(new char[] { ';' });

                    for (int i=0;i<emails.Length;i++)
                    {
                        String email = emails[i];
                        email = email.Trim();
                        if (email == "") continue;

                        FileUtility.PostPublishUpload(this.currentSession.FavoritePhotoFilename, email,
                            this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename + ".jpg", "");
                    }
                }
                ConfigUtility.IncrementCounter("Email");



                //if (config_offline)
                if (offline)
                {
                    //  Write the email address to that photo directory...
                    String pth = this.currentSession.PhotoPath + "\\OFFLINE.txt";
                    if ((!System.IO.File.Exists(pth)) && (this.currentSession.EmailAddress != null))
                    {
                        StreamWriter email_file = System.IO.File.CreateText(pth);
                        email_file.WriteLine(this.currentSession.EmailAddress);
                        email_file.Flush();
                        email_file.Close();
                    }

                    //  Write the favorite photo filename to that directory
                    pth = this.currentSession.PhotoPath + "\\OFFLINE_FAVEPHOTO.txt";
                    if ((!System.IO.File.Exists(pth)) && (this.currentSession.FavoritePhotoFilename != null))
                    {
                        StreamWriter favephote_file = System.IO.File.CreateText(pth);
                        favephote_file.WriteLine(this.currentSession.FavoritePhotoFilename);
                        favephote_file.Flush();
                        favephote_file.Close();
                    }

                    //  Write the favorite photo path to that directory
                    pth = this.currentSession.PhotoPath + "\\OFFLINE_PHOTOPATH.txt";
                    if ((!System.IO.File.Exists(pth)) && (this.currentSession.PhotoPath != null))
                    {
                        StreamWriter photopath_file = System.IO.File.CreateText(pth);
                        photopath_file.WriteLine(this.currentSession.PhotoPath);
                        photopath_file.Flush();
                        photopath_file.Close();
                    }
                }

            }

            if (ConfigUtility.GetValue("UploadFavoritePhotoToAutolycus").Equals("1"))
            {

                bool offline = (this.currentSession.ConfigOffline || this.currentSession.IsOffline);

                /*
                bool offline = false;
                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "OFFLINE")))
                {
                    String val = ConfigUtility.GetConfig(ConfigUtility.Config, "OFFLINE");
                    if (val == "1")
                        offline = true;
                }
                 * */

                if (!offline)
                    FileUtility.PostPublishUpload(this.currentSession.FavoritePhotoFilename, 
                        this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename + ".jpg", 
                        "");
            }

            String url = ConfigUtility.GetConfig(ConfigUtility.Config, "CustomIntegrationURL");

            if ( ! String.IsNullOrEmpty(url) )
            {

                bool offline = (this.currentSession.ConfigOffline || this.currentSession.IsOffline);
                /*
                bool config_offline = false;
                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "OFFLINE")))
                {
                    String val = ConfigUtility.GetConfig(ConfigUtility.Config, "OFFLINE");
                    if (val == "1")
                        config_offline = true;
                }
                 * */

                if (!offline)
                {
                    

                    String location = ConfigUtility.GetValue("CustomLocation");
                    if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "CustomLocation")))
                    {
                        location = ConfigUtility.GetConfig(ConfigUtility.Config, "CustomLocation");
                    }

                    String birthday = this.currentSession.BirthDate.ToShortDateString();

                    String emailaddress = this.currentSession.EmailAddress;
                    if (emailaddress == null) emailaddress = "NA";

                    String fblogin = this.currentSession.EmailAddress;
                    if (fblogin == null) fblogin = "NA";

                    String photopath = this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename + ".jpg";

                    String photoname = this.currentSession.FavoritePhotoFilename;

                    String ret =
                        FileUtility.PostPublishUploadURL(
                            "634856860824358963",
                            "eventname",
                            photopath,//"C:\\eventphotos\\634856819414680465\\55ae1b2778f5488fb9074274d70df87e.jpg",
                            photoname,//"55ae1b2778f5488fb9074274d70df87e.jpg",
                            location,
                            emailaddress,
                            fblogin,
                            birthday,
                            url);
                }
            }
        }


        private void CheckinToFacebook()
        {
            if ((ConfigUtility.GetConfig(ConfigUtility.Config, "AllowFacebookPublish").Equals("1")) && !string.IsNullOrEmpty(this.currentSession.FacebookAccessToken))
            {

                bool offline = (this.currentSession.ConfigOffline || this.currentSession.IsOffline);
                /*
                bool offline = false;
                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "OFFLINE")))
                {
                    String val = ConfigUtility.GetConfig(ConfigUtility.Config, "OFFLINE");
                    if (val == "1")
                        offline = true;
                }
                 * */

                if (!offline)
                {
                    // now tell facebook to make the post
                    FacebookUtility.PostCheckin(this.currentSession.FacebookAccessToken);


                    ConfigUtility.IncrementCounter("Facebook");
                }
            }
        }

         
        private void PublishToFacebook()
        {
            if (!this.facebook_publish) return;

            if ((ConfigUtility.GetConfig(ConfigUtility.Config, "AllowFacebookPublish").Equals("1")) && !string.IsNullOrEmpty(this.currentSession.FacebookAccessToken))
            {
                string response = string.Empty;
                // upload to publish server (don't need to send the email if it was sent custom way 
                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "EmailPublishUrl")))
                {
                    
                    // debugging so that it works without web cam...
                    if (!File.Exists(this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename + ".jpg"))
                    {
                        File.Copy(
                            ConfigUtility.GetValue("testphoto"),
                                this.currentSession.PhotoPath + "\\" +
                                this.currentSession.FavoritePhotoFilename + ".jpg");
                    }

                    bool offline = (this.currentSession.ConfigOffline || this.currentSession.IsOffline);
                    /*
                    if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "OFFLINE")))
                    {
                        String val = ConfigUtility.GetConfig(ConfigUtility.Config, "OFFLINE");
                        if (val == "1")
                            offline = true;
                    }
                     * */

                    if (!offline)
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

                }
                else if (!string.IsNullOrEmpty(this.currentSession.EmailAddress))
                {

                    bool offline = (this.currentSession.ConfigOffline || this.currentSession.IsOffline);
                    /*
                    bool offline = false;
                    if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "OFFLINE")))
                    {
                        String val = ConfigUtility.GetConfig(ConfigUtility.Config, "OFFLINE");
                        if (val == "1")
                            offline = true;
                    }
                     * */

                    if (!offline)
                    {

                        // try 3 times
                        response = FileUtility.HerculesUpload(DateTime.Now.Ticks.ToString(), this.currentSession.EmailAddress, this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename.ToString() + ".jpg", "");

                        if (response != "1")
                            response = FileUtility.HerculesUpload(DateTime.Now.Ticks.ToString(), this.currentSession.EmailAddress, this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename.ToString() + ".jpg", "");

                        if (response != "1")
                        {
                            Thread.Sleep(500);
                        }
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


                bool offl = (this.currentSession.ConfigOffline || this.currentSession.IsOffline);
                /*
                bool offl = false;
                    if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "OFFLINE")))
                    {
                        String val = ConfigUtility.GetConfig(ConfigUtility.Config, "OFFLINE");
                        if (val == "1")
                            offl = true;
                    }
                 * */

                if (!offl)
                {
                    // now tell facebook to make the post
                    FacebookUtility.PostWall(this.currentSession.FacebookAccessToken,
                            photoUrl,
                            FacebookUtility.GetConfig("title"),
                            "",
                            FacebookUtility.GetConfig("desc"),
                            link);
                }

                ConfigUtility.IncrementCounter("Facebook");
            }
        }


    
        private void Finished_FormClosed(object sender, FormClosedEventArgs e)
        {
            SoundUtility.Stop();
        }
    }
}
