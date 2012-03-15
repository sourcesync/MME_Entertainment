using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace MME.Hercules
{
    public class ConfigUtility
    {
        public static XmlDocument Config;
        public static XmlDocument SpeechConfig;
        public static XmlDocument QuestionsConfig;
        public static XmlDocument FacebookConfig;
        public static XmlDocument SequenceConfig;

        public static List<Bitmap> Backgrounds = null;

        private static int? photoBackgrounds;
        private static int? timeout;
        private static int? maxCopies;
        private static string storeImagesPath;
        private static string skin;
        private static bool? cameraEnabled;
        private static bool? roomNumberMode;
        private static int? photoCount;
        private static int? colorTypes;


        /// <summary>
        /// Used for simple configs.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfig(XmlDocument doc, string key)
        {
            if (doc == null) return String.Empty;

            XmlNodeList xnList = doc.SelectNodes("/Hercules/add[@key='" + key + "']");

            if (xnList.Count > 0)
                return xnList[0].Attributes["value"].Value;
            
            return String.Empty;
        }


        public static string GetConfig2(XmlDocument doc, string el, string key)
        {
            if (doc == null) return String.Empty;

            XmlNodeList xnList = doc.SelectNodes("/Hercules/" + el ); //"[@key='" + key + "']");

            if (xnList.Count > 0)
                if ( xnList[0].Attributes[key]!=null )
                    return xnList[0].Attributes[key].Value;
                //return xnList[0].Attributes["value"].Value;

            return String.Empty;
        }

        public static void IncrementCounter(string counter)
        {
            int current = Convert.ToInt32(ConfigUtility.GetValue(counter + "Count"));

            current++;

            SetValue(counter + "Count", current.ToString());
        }

        public static void SetValue(string key, string value)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string GetValue(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get(key)))
                    return ConfigurationManager.AppSettings.Get(key);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Hercules", ex.Message);
            }

            return string.Empty;


        }

        public static int Timeout
        {
            get
            {
                if (timeout == null)
                    timeout = Convert.ToInt32(GetValue("Timeout"));

                return timeout.Value;
            }
            set
            {
                timeout = value;
            }
        }


        public static string StoreImagesPath
        {
            get
            {
                if (String.IsNullOrEmpty(storeImagesPath))
                    storeImagesPath = GetValue("StoreImagesPath");

                return storeImagesPath;
            }
            set
            {
                storeImagesPath = value;
            }
        }

        public static string Skin
        {
            get
            {
                if (string.IsNullOrEmpty(skin))
                    skin = GetValue("Skin");

                if (string.IsNullOrEmpty(skin))
                    return "Default";
                else
                    return skin;
            }
        }

        public static bool CameraEnabled
        {
            get
            {
                if (cameraEnabled == null)
                    cameraEnabled = GetValue("CameraEnabled").Equals("1");

                return cameraEnabled.Value;                
            }
            set
            {
                cameraEnabled = value;
            }
        }

        public static bool IsDeveloperMode
        {
            get
            {
                return GetValue("DeveloperMode").Equals("1");
            }
        }

        public static bool RoomNumberMode
        {
            get
            {
                if (roomNumberMode == null)
                    roomNumberMode = ConfigUtility.GetConfig(ConfigUtility.Config, "ROOM_NUMBER_MODE").Equals("1");

                return roomNumberMode.Value;
            }
        }


        public static bool AllowFacebookPublish
        {
            get
            {
                return GetValue("AllowFacebookPublish").Equals("1");
            }
        }


        public static int PhotoCount
        {
            get
            {
                if (photoCount == null)
                    photoCount = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "PHOTO_COUNT"));

                return photoCount.Value;
            }
        }

        public static int MaxCopies
        {
            get
            {
                if (maxCopies == null)
                    maxCopies = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "MAX_COPIES"));               

                return maxCopies.Value;
            }
            set
            {
                maxCopies = value;
            }
        }

        public static int ColorTypes
        {
            get
            {
                if (colorTypes == null)
                    colorTypes = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "COLOR_TYPES"));

                return colorTypes.Value;
            }
        }
     
        public static int PhotoBackgrounds
        {
            get
            {
                if (photoBackgrounds == null)
                    photoBackgrounds = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "BACKGROUNDS"));               

                return photoBackgrounds.Value;
            }
        }

    }
}
