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




        [STAThread]
        static void Main(string[] args)
        {
            try
            {

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
                //PhidgetUtility.InitPhidgetBoard();
                if (!PhidgetUtility2.InitAll()) ;

                // Init camera.
                CameraUtility.InitializeCamera();


                
                /*
                //if (!string.IsNullOrEmpty(this.currentSession.EmailAddress))
                //    System.IO.File.WriteAllText(this.currentSession.PhotoPath + "\\email.txt", this.currentSession.EmailAddress);
                System.IO.File.WriteAllText("c:\\tmp\\_email.txt", "george.williams@gmail.com");

                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "EmailPublishUrl")))
                {
                    String nm = "6fa39e9d188c40e79e1ddb823942534b";
                    String str = FileUtility.PostPublishUpload(nm, 
                        "george.williams@gmail.com",
                        "C:\\eventphotos\\634722089735242885\\6fa39e9d188c40e79e1ddb823942534b.jpg", 
                        "");
                    //ConfigUtility.IncrementCounter("Email");
                }

                if (ConfigUtility.GetValue("UploadFavoritePhotoToAutolycus").Equals("1"))
                {
                    String nm = "6fa39e9d188c40e79e1ddb823942534b";
                    String str = FileUtility.PostPublishUpload(nm,
                    "C:\\eventphotos\\634722089735242885\\6fa39e9d188c40e79e1ddb823942534b.jpg", 
                    "");
                }
                */

                // Start
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Forms.User.Start());
             


            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
        }
    }
}
