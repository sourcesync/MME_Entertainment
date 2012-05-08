using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Speech;
using System.Speech.Synthesis;
using System.Xml;

namespace MME.Hercules
{
    public class SoundUtility
    {
        public static SoundPlayer soundplayer = new SoundPlayer();
        public static System.Speech.Synthesis.SpeechSynthesizer speak = new SpeechSynthesizer();


        public static bool InitSpeech()
        {       

            speak.Volume = Convert.ToInt32(ConfigUtility.GetValue("SpeechVolume"));
            speak.Rate = Convert.ToInt32(ConfigUtility.GetValue("SpeechRate"));

            // if force override voice.
            if (!string.IsNullOrEmpty(ConfigUtility.GetValue("SpeechVoice")))
            {
                try
                {
                    speak.SelectVoice(ConfigUtility.GetValue("SpeechVoice"));
                }
                catch
                {
                    return false;
                }
            }

            return true;

        }



        public static void SpeakSyncFromConfig(string key)
        {        
            if (ConfigUtility.SpeechConfig == null) return;

            XmlNodeList xnList = ConfigUtility.SpeechConfig.SelectNodes("/Hercules/string[@key='" + key.ToUpper() + "']");

            if (xnList.Count > 0)
                SoundUtility.SpeakSync(xnList[0].Attributes["text"].Value);
        }

        public static void SpeakFromConfig(string key)
        {
            if (ConfigUtility.SpeechConfig == null) return;

            XmlNodeList xnList = ConfigUtility.SpeechConfig.SelectNodes("/Hercules/string[@key='" + key.ToUpper() + "']");

            if (xnList.Count > 0)
                SoundUtility.Speak(xnList[0].Attributes["text"].Value);
        }



        public static void StopSpeaking()
        {
            speak.SpeakAsyncCancelAll();
        }

        public static void Speak(string s)
        {
            StopSpeaking();
            speak.SpeakAsync(s);
        }
        
        public static void SpeakSync(string s)
        {
            StopSpeaking();

            try
            {
                speak.Speak(s);
            }
            catch { }
        }

        public static void Stop()
        {
            soundplayer.Stop();
        }

        public static SoundPlayer LoadSound(string soundfile)
        {
            // If sound is disabled, then no play any sound
            if (!ConfigUtility.GetValue("SoundEnabled").Equals("1")) return null;

            try
            {
                soundplayer = new SoundPlayer(string.Format("Skins\\{0}\\Sounds\\{1}",
                    ConfigUtility.Skin,
                    soundfile));
                //soundplayer.
                return soundplayer;
            }
            catch
            {
                return null;
            }
        }

        public static void PlaySync(string soundfile)
        {
            // If sound is disabled, then no play any sound
            if (!ConfigUtility.GetValue("SoundEnabled").Equals("1")) return;

            try
            {
 
                soundplayer.SoundLocation = string.Format("Skins\\{0}\\Sounds\\{1}",
                    ConfigUtility.Skin,
                    soundfile);

                soundplayer.PlaySync();
            }
            catch { }
        }

        public static void Play(string soundfile)
        {
            // If sound is disabled, then no play any sound
            if (!ConfigUtility.GetValue("SoundEnabled").Equals("1")) return;

            try
            {
                soundplayer.SoundLocation = string.Format("Skins\\{0}\\Sounds\\{1}",
                    ConfigUtility.Skin,
                    soundfile);

                soundplayer.Play();
            }
            catch { }
        }
    }
}
