using System;
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
                PhidgetUtility.InitPhidgetBoard();

                // Init camera.
                CameraUtility.InitializeCamera();


                //gw
                //Facebook.FacebookClient client = new Facebook.FacebookClient(FacebookUtility.AppId, FacebookUtility.Secret);
                //Facebook.FacebookClient client2 = new Facebook.FacebookClient(client.AccessToken);
                //dynamic me = client.Get("me");
                //gw

                

                // Start
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Forms.User.Start());
                
                //Session session = new Session();
                //session.EmailAddress = "george.williams@gmail.com";
                //Application.Run(new Forms.User.Facebook(session));
            }
            catch (System.Exception e)
            {

            }
        }
    }
}
