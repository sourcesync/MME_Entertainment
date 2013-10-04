﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Speech.Synthesis;

/*
 * 
 * Hercules - written by Ron Stone
 * 
 */

namespace MME.Hercules
{
    static class Program
    {
        public static bool config_offline = false;
        public static bool is_offline = false;
        public static System.Threading.Thread offline_thread = null;

        private static void LoadConfig(ref XmlDocument config, string name, bool required)
        {
            try
            {
                if (System.IO.File.Exists(string.Format("Skins\\{0}\\{1}.xml", ConfigUtility.Skin, name)))
                {
                    if (config == null)
                        config = new XmlDocument();

                    config.Load(string.Format("Skins\\{0}\\{1}.xml", ConfigUtility.Skin, name));
                }
                else if (required)
                {
                    MessageBox.Show("Missing skin " + name + ".xml.  This file is required.", "Cannot continue");
                    Environment.Exit(0);
                }
            }
            catch
            {
                MessageBox.Show("Improperly formatted " + name + ".xml.");
                Environment.Exit(0);
            }
        }


        static bool ProcessOfflineDir(String dir)
        {
            bool b = true;

            //  read the content of the email file 
            String offline_file = System.IO.Path.Combine(new string[2] { dir, "OFFLINE.txt" });
            String emailaddresses = System.IO.File.ReadAllText(offline_file);
            emailaddresses = emailaddresses.Trim();

            //  read the contents of the favephoto file...
            offline_file = System.IO.Path.Combine(new string[2] { dir, "OFFLINE_FAVEPHOTO.txt" });
            String favephoto = System.IO.File.ReadAllText(offline_file);
            favephoto = favephoto.Trim();

            //  read the contents of the photopath file...
            offline_file = System.IO.Path.Combine(new string[2] { dir, "OFFLINE_PHOTOPATH.txt" });
            String photopath = System.IO.File.ReadAllText(offline_file);
            photopath = photopath.Trim();

            //  iterate over all emails...
            String[] emails = emailaddresses.Split(new char[] { ';' });

            for (int i = 0; i < emails.Length; i++)
            {
                String email = emails[i];
                email = email.Trim();
                if (email == "") continue;

                FileUtility.PostPublishUpload(favephoto, email,
                    photopath + "\\" + favephoto + ".jpg", "");
            }

            return true;
        }

        static void OfflineTasks()
        {
            while (true)
            {
                if ( Program.config_offline)
                {
                    System.Threading.Thread.Sleep(1000);
                    continue;
                }
                else if (Program.is_offline)
                {
                    Program.is_offline = MME.Hercules.Forms.User.Start.GetActualOffline();
                    if (Program.is_offline)
                    {
                        System.Threading.Thread.Sleep(1000);
                        continue;
                    }
                }

                //  looks like we are online, check for offline tasks...

                try
                {
                    //  Get all the event photo directories...
                    String eventdir = ConfigUtility.GetValue("StoreImagesPath");
                    var dirs = System.IO.Directory.EnumerateDirectories(eventdir);
                    String[] dirlist = dirs.ToArray();
                    for (int i=0;i<dirlist.Length;i++)
                    {
                        String dir = dirlist[i];

                        //  See if it needs offline processing....
                        String offline_file = System.IO.Path.Combine(new string[2] { dir, "OFFLINE.txt" });

                        if (System.IO.File.Exists(offline_file))
                        {
                            ProcessOfflineDir(dir);

                            //  delete the offline marker, whatever happens...
                            try
                            {
                                System.IO.File.Delete(offline_file);
                            }
                            catch
                            {
                                System.Windows.Forms.MessageBox.Show("There was a critical problem deleting an offline marker file.");
                                Application.Exit();
                            }
                        }

                    }

                }
                catch
                {
                    System.Threading.Thread.Sleep(500);
                }

            }

        }
        

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if ( !ProcessChecker.IsOnlyProcess2("Hercules") )
                {
                    //System.Windows.Forms.MessageBox.Show("Another Hercules Is Running");
                    System.Environment.Exit(1);
                }
                
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Load configuration files to memory
                LoadConfig(ref ConfigUtility.Config, "configuration", true);
                LoadConfig(ref ConfigUtility.SpeechConfig, "speech", false);
                LoadConfig(ref ConfigUtility.FacebookConfig, "facebook", false);
                LoadConfig(ref ConfigUtility.QuestionsConfig, "questions", false);
                LoadConfig(ref ConfigUtility.SequenceConfig, "sequence", false);

                // Set last start up date.
                ConfigUtility.SetValue("LastStartUpDate", DateTime.Now.ToString());

                // Init speech synthesizer
                if (ConfigUtility.SpeechConfig != null)
                {
                    bool success = SoundUtility.InitSpeech();

                    if (!success)
                    {
                        string available = string.Empty;

                        System.Collections.ObjectModel.ReadOnlyCollection<InstalledVoice> voices = SoundUtility.speak.GetInstalledVoices();

                        foreach (InstalledVoice voice in voices)
                            available += voice.VoiceInfo.Name + "\n";

                        MessageBox.Show("Invalid voice.  Please check the config and try again.  Available voices are:\n\n" + available);
                        Environment.Exit(0);
                    }
                }

                ConfigUtility.Backgrounds = new List<System.Drawing.Bitmap>();

                // pre load backgrounds
                for (int i = 0; i < ConfigUtility.PhotoBackgrounds; i++)
                {
                    System.Drawing.Bitmap bm = FileUtility.LoadBitmap("Skins\\" + ConfigUtility.Skin + "\\Backgrounds\\bg" +
                        (i + 1).ToString() + ".jpg");

                    bm.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);

                    ConfigUtility.Backgrounds.Add(bm);
                }

                // Init Phidgetboard if using it
                PhidgetUtility.InitPhidgetBoard();
                PhidgetUtility2.InitAll() ;

                // Init camera...
                if (!CameraUtility.InitializeCamera())
                {
                    System.Windows.Forms.MessageBox.Show("Cannot start camera. Please restart this software.");
                    Environment.Exit(1);
                    return;
                }

                // Init bill collector...
                if (!MME.Hercules.Utility.BillCollector.Initialize(null, null))
                {
                    System.Windows.Forms.MessageBox.Show("Cannot start bill collector. Please restart this software.");
                    Environment.Exit(1);
                    return;
                }


                //  Start the offline thread...
                config_offline = MME.Hercules.Forms.User.Start.GetConfigOffline();
                is_offline = MME.Hercules.Forms.User.Start.GetActualOffline();

                System.Threading.ThreadStart start = new System.Threading.ThreadStart( Program.OfflineTasks );
                offline_thread = new System.Threading.Thread(start);
                offline_thread.Start();

                /*
                //FileUtility.DJRequest("This is it!");

                
                //String url = "http://favpic.mobi/test_post.php";

                //String url = "http://favpic.mobi/hercules_test.php";

                String url = "http://photomation.mmeink.com/hercules_send.php";

                //String url = "http://autolycus.mmeentertainment.com/hercules_test.php";

                //FileUtility.PostPublishUpload(this.currentSession.FavoritePhotoFilename, email,
                          // this.currentSession.PhotoPath + "\\" + this.currentSession.FavoritePhotoFilename + ".jpg", "");
                FileUtility.PostPublishUploadWithUrl("55ae1b2778f5488fb9074274d70df87e.jpg", "george@devnullenterprises.com",
                           "C:\\eventphotos\\634856819414680465\\55ae1b2778f5488fb9074274d70df87e.jpg", "", url);

                
                //surl = "http://doingourthing-admin.group360.com/photomation.php";

                String ret =
                    FileUtility.PostPublishUploadURL(
                    "634856860824358963",
                    "eventname",
                    "C:\\eventphotos\\634856819414680465\\55ae1b2778f5488fb9074274d70df87e.jpg",
                    "55ae1b2778f5488fb9074274d70df87e.jpg",
                    "chicago",
                    "email@email.com",
                    "email@email.com",
                    "a date",

                    url);
                    //"http://doingourthing-admin.group360.com/photomation.cfm");
        
               */
                 
                /*
                //if (!string.IsNullOrEmpty(this.currentSession.EmailAddress))
                //    System.IO.File.WriteAllText(this.currentSession.PhotoPath + "\\email.txt", this.currentSession.EmailAddress);
                System.IO.File.WriteAllText("c:\\tmp\\_email.txt", "george@devnullenterprises.com");

                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "EmailPublishUrl")))
                {
                    String nm = "0d990439657242148d26ca844cd90927";
                    String str = FileUtility.PostPublishUpload(nm, 
                        //"jtli33@aol.com",
                        "george@devnullenterprises.com",
                        "c:\\eventphotos\\634733123411250000\\0d990439657242148d26ca844cd90927.jpg", 
                        "");
                    //ConfigUtility.IncrementCounter("Email");
                }   

                if (ConfigUtility.GetValue("UploadFavoritePhotoToAutolycus").Equals("1"))
                {
                    String nm = "0d990439657242148d26ca844cd90927";
                    String str = FileUtility.PostPublishUpload(nm,
                        "c:\\eventphotos\\634733123411250000\\0d990439657242148d26ca844cd90927.jpg", 
                    "");
                }
                */
                

                //  Initialize bill collector...
                


                // Start
                Application.Run(new Forms.User.Start());
             
                

            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
        }
    }
}
