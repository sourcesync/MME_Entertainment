using System;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO;
using Phidgets;
using Phidgets.Events;

namespace MME.Hercules.Forms.User
{
    public partial class Start : Form
    {
        public Session currentSession;
        private const int INTERNET_OPTION_END_BROWSER_SESSION = 42;
        


        public Start()
        {
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            
           if (ConfigUtility.IsDeveloperMode)
               this.WindowState = FormWindowState.Normal;

           if (ConfigUtility.GetValue("BoothType") == "2")
           {
               this.juststart();
               return;
           }

           //gw
            //PhidgetUtility.Relay(Convert.ToInt32(ConfigUtility.GetValue("PhidgetRelay_VanityLight")),
            //true);

            // touch start
            startArea.Parent = pb;
            WindowUtility.SetScreen(pb, Hercules.Properties.Resources.START_SCREEN);

            // Set start button
            startArea.Location = new Point(Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButtonX")),
                                Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButtonY")));

            startArea.Width = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButtonWidth"));
            startArea.Height = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButtonHeight"));

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButtonColor")))
                startArea.BackColor = ColorTranslator.FromHtml(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButtonColor"));

         
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButton2X")))
            {              
                sb2.Name = ConfigUtility.GetConfig(ConfigUtility.Config, "StartButton2Screen");

                sb2.Location = new Point(Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButton2X")),
                    Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButton2Y")));
                sb2.Width = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButton2Width"));
                sb2.Height = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButton2Height"));
            
                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButton2Color")))
                    sb2.BackColor = ColorTranslator.FromHtml(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButton2Color"));
                sb2.Click += new EventHandler(sb_Click);
                sb2.Visible = true;
                sb2.Parent = pb;
            }

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButton3X")))
            {
                sb1.Name = ConfigUtility.GetConfig(ConfigUtility.Config, "StartButton3Screen");

                sb1.Location = new Point(Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButton3X")),
                    Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButton3Y")));
                sb1.Width = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButton3Width"));
                sb1.Height = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButton3Height"));

                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButton3Color")))
                    sb1.BackColor = ColorTranslator.FromHtml(ConfigUtility.GetConfig(ConfigUtility.Config, "StartButton3Color"));
                sb1.Click += new EventHandler(sb_Click);
                sb1.Visible = true;
                sb1.Parent = pb;

            }
        }

        private DialogResult ProcessSequenceSteps(DialogResult dr)
        {
            if (ConfigUtility.SequenceConfig == null) return dr;

            XmlNodeList xnList = ConfigUtility.SequenceConfig.SelectNodes("/Hercules/page");

            if (xnList.Count == 0)
                return dr;

            string filename = string.Empty;
            string pagetype = string.Empty;

            foreach (XmlNode node in xnList)
            {
                // get page type
                pagetype =  node.Attributes["type"].Value;

                switch (pagetype)
                {
                    case "scentomatic":
                        using (Hercules.Forms.User.Scentomatic selform = new Scentomatic(node, currentSession))
                        {
                            dr = selform.ShowDialog();
                        }
                        break;

                    case "keyboard":
                        using (Hercules.Forms.User.Keyboard kbform = new Keyboard(node, currentSession))
                        {
                            dr = kbform.ShowDialog();
                        }
                        break;
                   case "video":
                        filename = xnList[0].Attributes["filename"].Value;

                        using (Hercules.Forms.User.VideoPlayer vp = new Forms.User.VideoPlayer(currentSession))
                        {
                            dr = vp.ShowDialog();
                        }

                        break;
                    case "selection":
                        
                        using (Hercules.Forms.User.Selection selform = new Selection(node, currentSession))
                        {
                            dr = selform.ShowDialog();
                        }

                        break;               
                }

                if (dr != System.Windows.Forms.DialogResult.OK)
                    return dr;
            }     
            
            return dr;
        }


        protected void sb_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);

            Thread.Sleep(800);

            pb2.Load("Skins\\" + ConfigUtility.Skin + "\\Screens\\" + pb.Name);
            pb2.BringToFront();
            pb2.Visible = true;
            this.Refresh();

            if (pb.Name == "map.jpg")
                SoundUtility.Play("mapvoice.wav");
            else
                SoundUtility.Play("specialdeal.wav");


        }

        private void Workflow()
        {
            // Start new session
            currentSession = new Session();

            
            DialogResult dr = System.Windows.Forms.DialogResult.OK;

            pb.Visible = false;

            // Turn off vanity light
            //gw PhidgetUtility.Relay(Convert.ToInt32(ConfigUtility.GetValue("PhidgetRelay_VanityLight")),
            //false);

            if (ConfigUtility.GetValue("BoothType") == "2")
            {
                MME.Hercules.WPFForms.FormWPFMaster wpffrm = null;
                while (true)
                {
                    dr = System.Windows.Forms.DialogResult.OK;

                    if (wpffrm == null)
                    {
                        wpffrm = new MME.Hercules.WPFForms.FormWPFMaster();
                    }
                    //wpffrm.ShowMain();
                    wpffrm.ShowDialog();

                    if (wpffrm.option == 1) // booth
                    {
                        // Are we supporting emailing
                        bool AllowEmailPublish = (ConfigUtility.GetConfig(ConfigUtility.Config, "AllowEmailPublish").Equals("1"));
                        if (AllowEmailPublish && dr == System.Windows.Forms.DialogResult.OK)
                        {
                            using (User.Email ef = new Email(currentSession))
                            {
                                dr = ef.ShowDialog();
                            }
                        }

                        // Start picture taking
                        if (dr == System.Windows.Forms.DialogResult.OK)
                        {
                            using (User.TakePhotos tpform = new TakePhotos(currentSession))
                            {
                                dr = tpform.ShowDialog();

                                //  in video table mode - did they click back button?...
                                if ((ConfigUtility.GetValue("BoothType") == "2") && (dr == DialogResult.Cancel))
                                {
                                    dr = DialogResult.OK; // for the outer while loop...
                                    continue;
                                }
                            }
                        }


                        bool AllowFacebookPublish = (ConfigUtility.GetConfig(ConfigUtility.Config, "AllowFacebookPublish").Equals("1"));


                        //  Force favorite to first one...
                        if (dr == System.Windows.Forms.DialogResult.OK &&
                            (!string.IsNullOrEmpty(currentSession.EmailAddress) ||
                            AllowFacebookPublish))
                        {
                            {
                                this.currentSession.FavoritePhoto = 1;
                                this.currentSession.FavoritePhotoFilename = "photo1";
                            }
                        }


                        // Are we supporting facebook

                        if (AllowFacebookPublish && dr == System.Windows.Forms.DialogResult.OK)
                        {
                            using (User.Facebook fb = new Facebook(currentSession))
                            {
                                fb.ischeckin = false;
                                dr = fb.ShowDialog();
                            }
                        }

                        // Finish up ( AND ACTUALLY SEND TO FACEBOOK! )


                        if (dr == System.Windows.Forms.DialogResult.OK)
                        {
                            using (User.Developing dd = new Developing(currentSession))
                            {
                                dd.istablepost = true;
                                dd.ispromo = false;
                                dd.ischeckin = false;
                                dr = dd.ShowDialog();
                            }
                        }


                    } // is_table, booth mode
                    else if (wpffrm.option == 4) // promo
                    {
                        // Are we supporting emailing
                        bool AllowEmailPublish = (ConfigUtility.GetConfig(ConfigUtility.Config, "AllowEmailPublish").Equals("1"));
                        if (AllowEmailPublish && dr == System.Windows.Forms.DialogResult.OK)
                        {
                            using (User.Email ef = new Email(currentSession))
                            {
                                ef.ispromo = true;
                                dr = ef.ShowDialog();
                            }
                        }

                        if (dr == System.Windows.Forms.DialogResult.OK)
                        {
                            using (User.Developing dd = new Developing(currentSession))
                            {
                                dd.istablepost = false;
                                dd.ispromo = false;
                                dd.ischeckin = true;
                                dr = dd.ShowDialog();
                            }
                        }
                    }
                    else if (wpffrm.option == 5) // checkin option...
                    {
                        bool AllowFacebookPublish = (ConfigUtility.GetConfig(ConfigUtility.Config, "AllowFacebookPublish").Equals("1"));

                        if (AllowFacebookPublish)
                        {
                            using (User.Facebook fb = new Facebook(currentSession))
                            {
                                fb.ischeckin = true;
                                dr = fb.ShowDialog();
                            }
                        }

                        if (dr == System.Windows.Forms.DialogResult.OK)
                        {
                            using (User.Developing dd = new Developing(currentSession))
                            {
                                dd.istablepost = false;
                                dd.ispromo = false;
                                dd.ischeckin = true;
                                dr = dd.ShowDialog();
                            }
                        }
                    }
                } // END WHILE 
            }
            else // NOT BOOTH MODE, NORMAL...
            {
                if (ConfigUtility.SequenceConfig != null)
                {
                    dr = ProcessSequenceSteps(dr);
                }

                // Are we requiring room number mode?            
                if (ConfigUtility.RoomNumberMode && dr == System.Windows.Forms.DialogResult.OK)
                {
                    using (User.RoomNumber rn = new RoomNumber(currentSession))
                    {
                        dr = rn.ShowDialog();
                    }
                }

                // Are we supporting emailing
                bool AllowEmailPublish = (ConfigUtility.GetConfig(ConfigUtility.Config, "AllowEmailPublish").Equals("1"));
                if (AllowEmailPublish && dr == System.Windows.Forms.DialogResult.OK)
                {
                    using (User.Email ef = new Email(currentSession))
                    {
                        dr = ef.ShowDialog();
                    }
                }


                // color types
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    // do we support bw and color?
                    ColorType colorTypes = (ColorType)Convert.ToInt32(ConfigUtility.ColorTypes);

                    //gw
                    if ((colorTypes == ColorType.BW_Color) || (colorTypes == ColorType.BW_Color_Sepia))
                    {
                        using (User.PhotoType ptform = new PhotoType(currentSession))
                        {
                            ptform.color_choice = colorTypes;
                            dr = ptform.ShowDialog(this);
                        }
                    }
                    //gw
                    /*
                    // If we support both, ask the user.
                    if (((colorTypes & ColorType.BW) == ColorType.BW) && ((colorTypes & ColorType.Color) == ColorType.Color))
                    {
                        using (User.PhotoType ptform = new PhotoType(currentSession))
                        {
                        dr = ptform.ShowDialog(this);
                    }
                    }
                    * */
                    else
                        currentSession.SelectedColorType = colorTypes;
                    //gw

                } // colortypes

                /* backgrounds*/


                // Do we have custom backgrounds for chroma key?
                int backgrounds = Convert.ToInt32(ConfigUtility.PhotoBackgrounds);

                // If we have backgrounds, ask the user to select
                if (backgrounds > 1 && dr == System.Windows.Forms.DialogResult.OK)
                {
                    using (User.Backgrounds bkform = new Backgrounds(currentSession))
                    {
                        dr = bkform.ShowDialog();
                    }
                }
                else
                {
                    if (this.currentSession.SelectedBackgrounds == null)
                        this.currentSession.SelectedBackgrounds = new System.Collections.Generic.List<int>();

                    for (int i = 0; i <= ConfigUtility.PhotoCount - 1; i++)
                        this.currentSession.SelectedBackgrounds.Add(1);
                }



                // Start picture taking
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    using (User.TakePhotos tpform = new TakePhotos(currentSession))
                    {
                        dr = tpform.ShowDialog();
                    }
                }

                bool AllowFacebookPublish = (ConfigUtility.GetConfig(ConfigUtility.Config, "AllowFacebookPublish").Equals("1"));


                // As to pick favorite if emailing
                if (dr == System.Windows.Forms.DialogResult.OK &&
                    (!string.IsNullOrEmpty(currentSession.EmailAddress) ||
                    AllowFacebookPublish))
                {
                    {
                        PickFavorite pvform = new PickFavorite(currentSession);
                        dr = pvform.ShowDialog();
                    }
                }


                // Are we supporting facebook

                if (AllowFacebookPublish && dr == System.Windows.Forms.DialogResult.OK)
                {
                    using (User.Facebook fb = new Facebook(currentSession))
                    {
                        fb.ischeckin = false;
                        dr = fb.ShowDialog();
                    }
                }

                // Finish up ( AND ACTUALLY SEND TO FACEBOOK! )


                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    using (User.Developing dd = new Developing(currentSession))
                    {
                        dr = dd.ShowDialog();
                    }
                }


                //gw
                //PhidgetUtility.Relay(Convert.ToInt32(ConfigUtility.GetValue("PhidgetRelay_VanityLight")), true);
                //gw


            } // NORMAL MODE...

            // Back to start
            pb.Visible = true;

            if (ConfigUtility.IsDeveloperMode) 
                this.Show();
        }

        private void StartForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            DialogResult dr = System.Windows.Forms.DialogResult.Cancel;

            switch (e.KeyCode)
            {
                case Keys.F1: // Operator menu
                    if (ConfigUtility.IsDeveloperMode) this.Hide();
                    using (Forms.Operator.Main main = new Operator.Main())
                    {
                        main.ShowDialog();
                    }
                    if (ConfigUtility.IsDeveloperMode) this.Show();
                    break;
                case Keys.F7:  // camera test 

                    Session tmp = new Session();
                    using (User.Backgrounds bkform = new Backgrounds(tmp))
                    {
                        dr = bkform.ShowDialog();
                    }

                    // Start picture taking
                    if (dr == System.Windows.Forms.DialogResult.OK)
                    {
                        using (User.TakePhotos tpform = new TakePhotos(tmp))
                        {
                            dr = tpform.ShowDialog();
                        }
                    }
                    break;                    
                case Keys.F8:
                    using (Operator.Camera camform = new Operator.Camera())
                    {
                        camform.ShowDialog();
                    }
                    break;
                case Keys.F9:
                    using (Operator.Chromakey ckform = new Operator.Chromakey())
                    {
                        ckform.ShowDialog();
                    }
                    break;
                case Keys.Escape:
                case Keys.F10: // Quit
                    this.Close();
                    break;
            }
        }

        public void PlaySelectionSound()
        {
            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);
            Thread.Sleep(800);
        }

        private void juststart()
        {
            PlaySelectionSound();

            // Start participant workflow
            Workflow();
        }

        private void startbutton_Click(object sender, EventArgs e)
        {
            PlaySelectionSound();

            // Start participant workflow
            Workflow();
        }

        private void startArea_Click(object sender, EventArgs e)
        {
            PlaySelectionSound();

            // Start participant workflow
            Workflow();
        }

        private void Start_FormClosed(object sender, FormClosedEventArgs e)
        {
            PhidgetUtility.Shutdown();
            //gw
            PhidgetUtility2.Shutdown();
            //gw
            CameraUtility.Shutdown();

        }

        
        private void pb2_Click_1(object sender, EventArgs e)
        {
            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);

            Thread.Sleep(800);

            pb2.Visible = false;

        }


    }
}
