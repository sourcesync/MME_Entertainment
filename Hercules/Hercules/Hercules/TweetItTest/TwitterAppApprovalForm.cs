// TwitterAppApprovalForm.cs
// ------------------------------------------------------------------
//
// Pops up an embedded web browser to get user approval for the plugin as
// a twitter app. The normal oath 1.0a steps are:
//
// 1. get a Request Token (GET http://twitter/oauth/request_token)
//
// 2. open a web browser to the authorization page, passing the request token
//    received from the prior step.  The user must then click
//    the approve button.
//
// 3. get a PIN on the response page from that HTML Form.
//
// 4. using that PIN, do a GET http://twitter/oauth/access_token, and
//    get an access token and secret.
//
// 5. Use those things to produce an OAuth authorization header,
//    in an oauth-protected request.
//
//
// The user experience imagined and recommended by Twitter requires the
// user to go to a different web page (an external browser).  In the
// base case, Twitter expects the user to manually open the browser
// themselves, and cut/paste the required URL into the address bar.
// Then the user needs to click the :Allow: button and copy/paste the
// PIN back into the Windows form app. Then close the web browser, and
// click the next button in the Windows Forms app, and so on.  This is
// just waaaaay too many context flips.
//
// Using an embedded web browser simplifies the UI flow significantly.
// The user just needs to click the "Allow" button and the rest of that
// nonsense is hidden from him.  There's no need for the user to be
// involved in copy/paste of the initial URL into the external browser
// address bar, or copy/paste of the pin from the html response page,
// back into the windows form to allow the next REST request.  With an
// embedded web browser all that can be automated.
//
// The result is a smoother UI, fewer user instructions, fewer error cases,
// and a better overall experience, while still remaining compliant with
// OAuth 1.0a.
//
//
// This code is licensed under the MS-PL. See the accompanying
// License.txt file for details.
//
//
// Author     : Dino
// Created    : Tue Dec 14 15:21:59 2010
// Last Saved : <2010-December-16 11:26:24>
//
// ------------------------------------------------------------------
//
// Copyright (c) 2010 by Dino Chiesa
// All rights reserved!
//
// ------------------------------------------------------------------

namespace Ionic.Twitter
{
    using System;
    using System.Windows.Forms;
    using System.Reflection;
    using RE=System.Text.RegularExpressions;

    class TwitterAppApprovalForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.WebBrowser web1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblInstructions;

        public static readonly string
            URL_REQUEST_TOKEN       = "https://api.twitter.com/oauth/request_token",
            URL_AUTHORIZE           = "https://api.twitter.com/oauth/authorize?oauth_token=",
            URL_ACCESS_TOKEN        = "https://api.twitter.com/oauth/access_token",
            URL_VERIFY_CREDS        = "https://api.twitter.com/1/account/verify_credentials.json",
            AUTHENTICATION_REALM    = "http://api.twitter.com/";

        private string _instructions1 =
              "Click the Next button to open a web page on Twitter.com. It will allow you to approve\n" +
              "this app as a Twitter app. This is the only time you will have\n" +
              "to do this.";

        private string _instructions2 =
              "Something has gone wrong while trying to approve this app.\n" +
              "You'll need to try again.";

        private OAuth.Manager _oauth;

        public TwitterAppApprovalForm(OAuth.Manager oauth)
        {
            InitializeComponent();
            _oauth= oauth;
            GetRequestToken();
        }

        private void InitializeComponent()
        {
            // pop Twitter's OAuth authentication web page here
            var f = this;
            var cursor = f.Cursor;
            f.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            string authzUrlStub =
                URL_AUTHORIZE
                .Substring(0, URL_AUTHORIZE.LastIndexOf('?'));
            Tracing.Trace("authzUrlStub = '{0}'", authzUrlStub);
            f.SuspendLayout();

            // event handlers
            WebBrowserDocumentCompletedEventHandler docCompleted = (sender, e) => {
                Tracing.Trace("web1.completed, url = '{0}'", web1.Url.ToString());
                var url2 = web1.Url.ToString();

                // It's possible there will be multiple pages in the flow.
                if (url2.StartsWith(URL_AUTHORIZE))
                {
                    // The login page has been displayed completely
                    f.Cursor = cursor;
                    return;
                }

                if (url2==authzUrlStub)
                {
                    // the user has clicked the "allow" or "deny" button
                    if (web1.DocumentText.Contains("you've denied"))
                    {
                        // deny
                        Tracing.Trace("Access denied.");
                        web1.Visible = false;
                        _oauth["token"] = ""; // forget the request token
                        _oauth["token_secret"] = ""; // forget the secret
                        f.DialogResult = DialogResult.Cancel;
                        f.Close();
                        // The caller is responsible for popping a
                        // notification - like a MessageBox saying "you
                        // need to authorize (this app) in order to
                        // upload".
                    }

                    else if (web1.DocumentText.Contains("You've granted access"))
                    {
                        var divMarker = "<div id=\"oauth_pin\">";
                        var index = web1.DocumentText.LastIndexOf(divMarker) +
                        divMarker.Length;
                        var snip = web1.DocumentText.Substring(index);
                        var pin = RE.Regex.Replace(snip,"(?s)[^0-9]*([0-9]+).*", "$1").Trim();
                        Tracing.Trace("Approval. PIN: '{0}'", pin);
                        web1.Visible = false; // all done with the web UI
                        GetAccessToken(pin);
                    }
                }
            };

            WebBrowserNavigatingEventHandler navigating = (sender,e) => {
                Tracing.Trace("web1.navigating, url = '{0}'", web1.Url.ToString());
                f.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                f.Update();
            };

            // The embedded browser can navigate through HTTP 302
            // redirects, download images, and so on. The display will
            // initially be blank while it is waiting for downloads and
            // redirects. Also, after the user clicks "Login", there's a
            // delay.  In those cases we want the wait cursor. Only turn
            // it off if the status text is "Done."
            EventHandler statusChanged = (sender,e) => {
                var t = web1.StatusText;
                if (t == "Done")
                    f.Cursor = cursor;
                else if (!String.IsNullOrEmpty(t))
                {
                    Tracing.Trace("web1.status = '{0}'", t);
                    f.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                }
            };

            web1 =  new System.Windows.Forms.WebBrowser();
            btnCancel = new System.Windows.Forms.Button();
            lblInstructions = new System.Windows.Forms.Label();
            //
            // web1
            //
            web1.Location = new System.Drawing.Point(4, 86);
            web1.Name = "web1";
            web1.DocumentCompleted += docCompleted;
            web1.Navigating += navigating;
            web1.StatusTextChanged += statusChanged;
            web1.Dock = DockStyle.Fill;
            //
            // lblInstructions
            //
            lblInstructions.Text = _instructions1;
            lblInstructions.ForeColor = System.Drawing.Color.Red;
            lblInstructions.AutoSize = true;
            lblInstructions.Visible = false;
            lblInstructions.Size = new System.Drawing.Size(576, 46);
            lblInstructions.Location = new System.Drawing.Point(4, 6);
            //
            // btnCancel
            //
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(368, 94);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(68, 23);
            btnCancel.TabIndex = 71;
            btnCancel.Visible = false;
            btnCancel.Text = "&Close";
            btnCancel.UseVisualStyleBackColor = true;
            //
            // Form
            //
            f.Controls.Add(web1);
            f.Controls.Add(lblInstructions);
            f.Controls.Add(btnCancel);
            f.Name = "Authorize";
            f.Text = "Authorize the TweetIt app";
            // f.Icon = ...
            // size to accommodate the twitter confirmation dialog
            f.MinimumSize = new System.Drawing.Size(820, 524);
            f.MaximumSize = new System.Drawing.Size(820, 524);
            f.ResumeLayout(false);
        }


        private void GetRequestToken()
        {
            Cursor cursor = this.Cursor;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.Update(); // show the wait cursor while sending request out...

            Tracing.Trace("Request Token : '{0}'", URL_REQUEST_TOKEN);

            var response =
                this._oauth.AcquireRequestToken(URL_REQUEST_TOKEN, "POST");

            Tracing.Trace("Request token response: {0}", response.AllText);
            if (!String.IsNullOrEmpty(response["oauth_token"]))
            {
                var uriString = URL_AUTHORIZE + response["oauth_token"];
                Tracing.Trace("web1.open url = '{0}'", uriString);
                web1.Url = new Uri(uriString);
            }
            else
            {
                web1.Visible = false;
                lblInstructions.Visible = true;
                lblInstructions.Text = _instructions2;
                btnCancel.Visible = true;
            }

            this.Cursor = cursor;
        }


        private void GetAccessToken(string pin)
        {
            Cursor cursor = this.Cursor;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            Tracing.Trace("Requesting Access token... {0}", URL_ACCESS_TOKEN);
            var response =
                this._oauth.AcquireAccessToken(URL_ACCESS_TOKEN,
                                               "POST",
                                               pin);

            Tracing.Trace("Access token response: {0}", response.AllText);
            if (!String.IsNullOrEmpty(response["oauth_token"]))
            {
                Tracing.Trace("good response.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                web1.Visible = false;
                this.lblInstructions.Text = _instructions2;
                lblInstructions.Visible = true;
                btnCancel.Visible = true;
            }

            this.Cursor = cursor;
        }
    }


    public static class Tracing
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int pid);

        //[System.Diagnostics.Conditional("Trace")]
        private static void SetupDebugConsole()
        {
            if ( !AttachConsole(-1) )  // Attach to a parent process console
                AllocConsole(); // Alloc a new console

            _process= System.Diagnostics.Process.GetCurrentProcess();
            System.Console.WriteLine();
            _initialized= true;
        }

        [System.Diagnostics.Conditional("Trace")]
        public static void Trace(string format, params object[] args)
        {
            if (!_initialized)
            {
                SetupDebugConsole();
            }

            System.Console.Write("{0:D5} ", _process.Id);
            System.Console.WriteLine(format, args);
        }

        private static System.Diagnostics.Process _process;
        private static bool _initialized = false;
    }

}