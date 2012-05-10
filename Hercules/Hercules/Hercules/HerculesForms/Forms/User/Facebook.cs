﻿using System;
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
        //gw

        public Facebook(Session currentSession)
        {
            Application.DoEvents();

            InitializeComponent();

            this.currentSession = currentSession;
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
            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;

            SoundUtility.StopSpeaking();

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);

            keyboard.SetKeyboard(false);

            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0);
            WindowUtility.ClearCache();

            SoundUtility.Play("usefacebook.wav");
            

/*
            //orig webBrowser1.Url = new Uri("https://graph.afacebook.com/oauth/authorize?client_id=119375921469000&redirect_uri=" +
 */

            /*GW
            webBrowser1.Url = new Uri("https://graph.facebook.com/oauth/authorize?client_id=262792687134194&redirect_uri=" +         
                System.Web.HttpUtility.UrlEncode("https://www.facebook.com/connect/login_success.html") + 
                "&type=user_agent&display=popup&scope=publish_stream");
            */

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

            pb2.Load(string.Format("Skins\\{0}\\Screens\\usefacebook.jpg",
                ConfigUtility.Skin));

            fbyes.Load(string.Format("Skins\\{0}\\Screens\\fbyes.jpg",
                ConfigUtility.Skin));

            fbno.Load(string.Format("Skins\\{0}\\Screens\\fbno.jpg",
            ConfigUtility.Skin));


            notice.ForeColor = Color.Black;         

            this.Refresh();

        }

        protected void WelcomeSpeech()
        {
            SoundUtility.Play("facebooksignon.wav");
        }


        private void skip_Click(object sender, EventArgs e)
        {
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

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
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

                    /*GW
                    webBrowser1.Url = new Uri("https://graph.facebook.com/oauth/authorize?client_id=119375921469000&redirect_uri=" + 
                        System.Web.HttpUtility.UrlEncode("http://www.facebook.com/connect/login_success.html") + 
                        "&type=user_agent&display=popup");
                        //"&type=user_agent&display=none");
                     * */
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
            fbno.BackColor = Color.Red;
            this.Refresh();
            
            SoundUtility.StopSpeaking();
            SoundUtility.PlaySync(Hercules.Properties.SoundResources.SELECTION_BUTTON);


            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }

}