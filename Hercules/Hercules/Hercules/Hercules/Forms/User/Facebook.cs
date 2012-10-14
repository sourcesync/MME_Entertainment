using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.Threading;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;

namespace MME.Hercules.Forms.User
{
    public partial class Facebook : Form
    {
        private Session currentSession = null;
        private string currentFBTextbox = "email";
        private bool FirstTime = true;
        private bool terminating = false;

        private const int INTERNET_OPTION_END_BROWSER_SESSION = 42;

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);


        //gw
        private delegate void DialogDoneHandler();
        private Thread delayDoneThread = null;
        public bool istable = false;
        public bool ischeckin = false;
        public bool yes_clicked = false;
        public bool skip_clicked = false;
        //gw

        public Facebook(Session currentSession)
        {
            Application.DoEvents();

            InitializeComponent();

            this.currentSession = currentSession;

            if (ConfigUtility.GetValue("BoothType") == "2")
            {
                istable = true;
            }
        }

        private void DialogDone()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void ThreadedDelayDone()
        {
            Thread.Sleep(1000);
            this.Invoke(new DialogDoneHandler(this.DialogDone));
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private void Facebook_Load(object sender, EventArgs e)
        {

            // possibly adjust the buttons
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "FBBUTTON_OFFSET")))
            {
                String offset = ConfigUtility.GetConfig(ConfigUtility.Config, "FBBUTTON_OFFSET");

                int yoff = int.Parse( offset );

                this.fbno.Location = new Point(this.fbno.Location.X, this.fbno.Location.Y + yoff);
                this.fbyes.Location = new Point(this.fbyes.Location.X, this.fbyes.Location.Y + yoff);
            }

            //possibly adjust the keyboard...
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "FBBUTTON_KEYBOARD_OFFSET")))
            {
                String offset = ConfigUtility.GetConfig(ConfigUtility.Config, "FBBUTTON_KEYBOARD_OFFSET");

                int yoff = int.Parse( offset );

                this.keyboard.Location = new Point(this.keyboard.Location.X, this.keyboard.Location.Y + yoff);
            }

            //possibly adjust the browser area...
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "FBBUTTON_KEYBOARD_OFFSET")))
            {
                String offset = ConfigUtility.GetConfig(ConfigUtility.Config, "FBBUTTON_KEYBOARD_OFFSET");

                int yoff = int.Parse(offset);

                this.webBrowser1.Location = new Point(this.webBrowser1.Location.X, this.webBrowser1.Location.Y + yoff);

                this.notice.Location = new Point(this.notice.Location.X, this.notice.Location.Y + yoff);
            }


            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "FB_FINISH_OFFSET")))
            {
                String offset = ConfigUtility.GetConfig(ConfigUtility.Config, "FB_FINISH_OFFSET");

                int yoff = int.Parse(offset);

                this.skip.Location = new Point(this.skip.Location.X, yoff);
                this.finished.Location = new Point(this.finished.Location.X, yoff);
            }

            if (ConfigUtility.IsDeveloperMode)
            {
                this.WindowState = FormWindowState.Normal;
                //gw
                this.FormBorderStyle = FormBorderStyle.Fixed3D;
                //gw
            }

            //gw
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //gw

            SoundUtility.StopSpeaking();

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);

            keyboard.SetKeyboard(false);

            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0);
            WindowUtility.ClearCache();

            //gw
            if (!this.ischeckin)
            {
                SoundUtility.Play("usefacebook.wav");
            }
            //gw

/*
            //orig webBrowser1.Url = new Uri("https://graph.afacebook.com/oauth/authorize?client_id=119375921469000&redirect_uri=" +
 */

            if (ischeckin)
            {
                //webBrowser1.Url = new Uri("http://touch.facebook.com");

                Uri uri = 
                    new Uri("https://graph.facebook.com/oauth/authorize?client_id=262792687134194&redirect_uri=" +
                        System.Web.HttpUtility.UrlEncode("https://www.facebook.com/connect/login_success.html") +
                        "&type=user_agent&display=popup&scope=publish_checkins");
                System.Console.WriteLine(uri.ToString());
                webBrowser1.Url = uri;
            }
            else
            {
                webBrowser1.Url = new Uri("https://graph.facebook.com/oauth/authorize?client_id=262792687134194&redirect_uri=" +
                    System.Web.HttpUtility.UrlEncode("https://www.facebook.com/connect/login_success.html") +
                    "&type=user_agent&display=popup&scope=publish_stream");
            }

            /*
            webBrowser1.Url = new Uri("https://www.facebook.com/dialog/oauth?client_id=262792687134194&" +
                "redirect_uri=" + 
                System.Web.HttpUtility.UrlEncode("https://www.facebook.com/connect/login_success.html") + "&" +
                "response_type=token&scope=publish_stream");
            */

            alertbox.BackColor = System.Drawing.ColorTranslator.FromHtml("#6B86B5");

            keyboard.Parent = pb;
            notice.Parent = pb;


            

            pb.Load(string.Format("Skins\\{0}\\Screens\\facebook.jpg",
                ConfigUtility.Skin));

            if ( this.ischeckin )
                pb2.Load(string.Format("Skins\\{0}\\Screens\\usefacebook-checkin.jpg",
                    ConfigUtility.Skin));
            else
                pb2.Load(string.Format("Skins\\{0}\\Screens\\usefacebook.jpg",
                    ConfigUtility.Skin));

            fbyes.Load(string.Format("Skins\\{0}\\Screens\\fbyes.jpg",
                ConfigUtility.Skin));

            fbno.Load(string.Format("Skins\\{0}\\Screens\\fbno.jpg",
            ConfigUtility.Skin));


            notice.ForeColor = Color.Black;

            if (istable)
            {
                this.labelQuestion.Visible = true;
                this.labelQuestion.AutoSize = false;
                this.labelQuestion.TextAlign = ContentAlignment.MiddleCenter;

                if (!this.ischeckin)
                {
                    bool bypass_facebook_post_text = false;
                    if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "BypassFacebookPostText")))
                    {
                        bypass_facebook_post_text = true;
                    }
                    if (bypass_facebook_post_text)
                        this.labelQuestion.Visible = false;
                    else
                        this.labelQuestion.Visible = true;

                    if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "TEXT_COLOR")))
                    {
                        String str = ConfigUtility.GetConfig(ConfigUtility.Config, "TEXT_COLOR");
                        this.labelQuestion.ForeColor = ColorTranslator.FromHtml(str);
                    }

                    this.labelQuestion.Text = "Would you like to post your photo to FaceBook?";

                    if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "FB_POST_TEXT")))
                    {
                        String text = ConfigUtility.GetConfig(ConfigUtility.Config, "FB_POST_TEXT");
                        this.labelQuestion.Text = text;
                    }
                }
                else
                {
                    bool bypass_facebook_checkin_text = false;
                    if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "BypassFacebookCheckinText")))
                    {
                        bypass_facebook_checkin_text = true;
                    }
                    if (bypass_facebook_checkin_text)
                        this.labelQuestion.Visible = false;
                    else
                        this.labelQuestion.Visible = true;

                    this.labelQuestion.Text = ""; // // "Would you like to check-in to FaceBook?";
                }
                this.labelQuestion.ForeColor = System.Drawing.Color.White;
                this.labelQuestion.BackColor = System.Drawing.Color.Transparent;
                this.labelQuestion.Parent = this.pb2;
                this.labelQuestion.BringToFront();

                Bitmap bm = Hercules.Properties.ImageResources.back_button;
                this.pictureBoxBack.Image = bm;
                this.pictureBoxBack.BackColor = System.Drawing.Color.Transparent;
                this.pictureBoxBack.Parent = pb2;
                this.pictureBoxBack.Visible = true;

                bm = Hercules.Properties.ImageResources.q_button;
                this.pictureBox1.Image = bm;
                this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
                this.pictureBox1.Parent = pb2;
                this.pictureBox1.Visible = true;
            }

            this.Refresh();

        }

        protected void WelcomeSpeech()
        {
            SoundUtility.Play("facebooksignon.wav");
        }


        private void skip_Click(object sender, EventArgs e)
        {
            this.skip_clicked = true;

            SoundUtility.StopSpeaking();
            SoundUtility.PlaySync(Hercules.Properties.SoundResources.SELECTION_BUTTON);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }




        private void finished_Click(object sender, EventArgs e)
        {
            SoundUtility.StopSpeaking();

            
            HtmlElementCollection inputs = webBrowser1.Document.GetElementsByTagName("input");

            foreach (HtmlElement el in inputs)
            {
                if (el.Name == "grant_clicked")
                {

                    el.InvokeMember("click");
                    return;
                }
            }

            HtmlElement login = webBrowser1.Document.GetElementById("login");

            if (login != null)
            {
                login.InvokeMember("click");

            }

            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);
        }

        private void checkin_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlElement body =  webBrowser1.Document.Body;
            string txt = body.OuterHtml;
            //Console.WriteLine(txt);

            HtmlElementCollection els = webBrowser1.Document.GetElementsByTagName("submit");
            foreach (HtmlElement el in els)
            {
                String nm = el.Name;
            }

            els = webBrowser1.Document.GetElementsByTagName("input");
            foreach (HtmlElement el in els)
            {
                String nm = el.Name;
                Console.WriteLine("input->" + nm + "\n");
            }

            els = webBrowser1.Document.GetElementsByTagName("button");
            foreach (HtmlElement el in els)
            {
                if (el.OuterHtml.ToLower().Contains("check in"))
                {
                    el.InvokeMember("click");
                }
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (false) //( ischeckin )
            {
                checkin_DocumentCompleted(sender, e);
                return;
            }


            int pos = webBrowser1.Url.ToString().IndexOf("access_token=");
            bool bOK = false;
            bool bFakeClicked = false;

            //gw
            if (pos <= 0) // a page other than access token...
            {
                
                //  look for initial app allow authorization page...
                HtmlElementCollection inputs = webBrowser1.Document.GetElementsByTagName("input");
                foreach (HtmlElement el in inputs)
                {
                    if (el.Name == "grant_required_clicked")
                    {
                        el.InvokeMember("click");
                        bFakeClicked = true;
                        //return;
                    }
                }

                //  look for initial app allow authentication page...
                inputs = webBrowser1.Document.GetElementsByTagName("input");
                foreach (HtmlElement el in inputs)
                {
                    if (el.Name == "grant_clicked")
                    {
                        el.InvokeMember("click");
                        bFakeClicked = true;
                        
                    }
                }
                 

                /*
                //  look for final success page...
                HtmlElementCollection all = webBrowser1.Document.GetElementsByTagName("body");
                foreach (HtmlElement el in inputs)
                {
                    if (el.InnerText != null)
                    {
                        if (el.InnerText.ToLower().Contains("success"))
                        {
                            this.webBrowser1.Visible = false;
                            this.Refresh();
                            Thread.Sleep(1);
                            bOK = true;
                        }
                    }
                }
                 * */
            }

            if (bFakeClicked)
            {
                this.webBrowser1.Visible = false;
                this.Refresh();
                Thread.Sleep(1);
                return;
            }
            //gw

            
            if (pos != -1) // access token !!
            { 
                string at = webBrowser1.Url.ToString().Substring(pos + 13, webBrowser1.Url.ToString().Length - (pos + 13));
                this.currentSession.FacebookAccessToken = at.Substring(0, at.IndexOf('&'));

                // get expires
                string expires = webBrowser1.Url.ToString().Substring(webBrowser1.Url.ToString().IndexOf("&expires_in="), webBrowser1.Url.ToString().Length - webBrowser1.Url.ToString().IndexOf("&expires_in="));

                this.currentSession.FacebookExpires = expires.Replace("&expires_in=", "");

                //DialogResult = System.Windows.Forms.DialogResult.OK;
                bOK = true;
                this.webBrowser1.Visible = false;
                //this.Refresh();
                //Thread.Sleep(1);
                
            }

            System.Windows.Forms.HtmlDocument HTMLDocument = webBrowser1.Document;

            

            foreach (System.Windows.Forms.HtmlElement el in HTMLDocument.GetElementsByTagName("div"))
            {
                if (el.Id == "error")
                {
                    email.Text = "";
                    pass.Text = "";
                    webBrowser1.Url = new Uri("https://graph.facebook.com/oauth/authorize?client_id=119375921469000&redirect_uri=" + 
                        System.Web.HttpUtility.UrlEncode("http://www.facebook.com/connect/login_success.html") + 
                        "&type=user_agent&display=popup");
                        //"&type=user_agent&display=none");
                    alertbox.BringToFront();
                    alertbox.Visible = true;

                    return;
                }
            }
            foreach (System.Windows.Forms.HtmlElement el in HTMLDocument.GetElementsByTagName("p"))
            {
                if (el.GetAttribute("className").IndexOf("reset_password") != -1)
                {
                    el.OuterHtml = string.Empty;
                    break;
                }
            }

            foreach (System.Windows.Forms.HtmlElement el in HTMLDocument.GetElementsByTagName("div"))
            {
                if (el.GetAttribute("className").Equals("persistent"))
                {
                    el.OuterHtml = string.Empty;
                    break;
                }
            }


            foreach (System.Windows.Forms.HtmlElement el in HTMLDocument.Links)
            {

                if (el.Id == "reg_btn_link")
                    el.OuterHtml = string.Empty;
                else
                    el.OuterHtml = el.InnerText;


            }

            HtmlElement em = HTMLDocument.GetElementById("email");
            if (em != null)
            {
                if (FirstTime)
                {
                    if (this.currentSession == null)
                        email.Text = "george.williams@gmail.com";
                    else
                     email.Text = this.currentSession.EmailAddress;
                    em.SetAttribute("Value", email.Text);

                    FirstTime = false;
                }

                em.GotFocus += new HtmlElementEventHandler(tbel_Focus);

            }

            HtmlElement pw = HTMLDocument.GetElementById("pass");
            if (pw != null)
            {
                pw.GotFocus += new HtmlElementEventHandler(tbel_Focus);
            }

            if (!keyboard.IsEnabled)
                keyboard.SetKeyboard(true);

            if (bOK)
            {
                this.delayDoneThread = new Thread(this.ThreadedDelayDone);
                this.delayDoneThread.Start();
            }
        }

        protected void tbel_Focus(object sender, HtmlElementEventArgs e)
        {
            HtmlElement el = (HtmlElement)sender;
            HtmlElement em = webBrowser1.Document.GetElementById("email");
            HtmlElement pw = webBrowser1.Document.GetElementById("pass");

            if (em != null)
                em.Style = "";

            if (pw != null)
                pw.Style = "";

            if (el.Id == "email")
            {
                el.Focus();
                el.Style = "font-size:140%;font-weight:bold;border:4px solid red;width:250px;";
                keyboard.CurrentTextBox = email;
                currentFBTextbox = el.Id;
            }
            else if (el.Id == "pass")
            {
                el.Focus();
                el.Style = "font-size:140%;font-weight:bold;border:4px solid red;width:250px;";
                keyboard.CurrentTextBox = pass;
                currentFBTextbox = el.Id;
            }
            else
            {
                currentFBTextbox = "email";

                if (em != null)
                {
                    em.Style = "font-size:140%;font-weight:bold;border:4px solid red;width:250px;";
                    em.Focus();
                }

                keyboard.CurrentTextBox = email;
            }
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string url = e.Url.ToString();


            if (false)
            {

                if (url.IndexOf("api.facebook.com/method/auth.expireSession") != -1)
                {
                    e.Cancel = true;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    return;
                }

                if (url.IndexOf("error_reason=user_denied") != -1)
                {
                    e.Cancel = true;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            alertbox.Visible = false;
        }

        private Control FindControl(string strControlName)
        {
            if (strControlName.Length == 0 || this.Controls.Find(strControlName, true).Length == 0)
                return null;

            return this.Controls.Find(strControlName, true)[0];
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            HtmlElement el = webBrowser1.Document.GetElementById(currentFBTextbox);
            el.SetAttribute("Value", FindControl(currentFBTextbox).Text);
        }

        private void Facebook_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (webBrowser1.Document != null)
                webBrowser1.Document.Cookie = "";
        }

        private void fbyes_Click(object sender, EventArgs e)
        {
            this.yes_clicked = true;

            fbyes.BackColor = Color.Red;
            this.Refresh(); 
            
            SoundUtility.Stop();
            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);

            Thread.Sleep(800);

            Thread t = new Thread(WelcomeSpeech);
            t.Start();

            usepanel.Visible = false;
        }

        private void fbno_Click(object sender, EventArgs e)
        {
            this.yes_clicked = false;

            fbno.BackColor = Color.Red;
            this.Refresh();
            
            SoundUtility.StopSpeaking();
            SoundUtility.PlaySync(Hercules.Properties.SoundResources.SELECTION_BUTTON);


            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void pictureBoxBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            return;
        }

        private void pictureBoxQuit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            System.Environment.Exit(0);
            return;
        }
    }

}
