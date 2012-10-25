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
                //PhidgetUtility.InitPhidgetBoard();
                if (!PhidgetUtility2.InitAll()) ;

                // Init camera.
                CameraUtility.InitializeCamera();

                //FileUtility.DJRequest("This is it!");

                /*
                String url = "http://favpic.mobi/test_post.php";
                url = "http://doingourthing-admin.group360.com/photomation.php";

                String ret =
                    FileUtility.PostPublishUploadURL(
                    "634856860824358963",
                    "eventname",
                    "C:\\eventphotos\\634856819414680465\\55ae1b2778f5488fb9074274d70df87e.jpg",
                    "55ae1b2778f5488fb9074274d70df87e.jpg",
                    url);
                    //"http://doingourthing-admin.group360.com/photomation.cfm");
        
                 * /
                 
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
