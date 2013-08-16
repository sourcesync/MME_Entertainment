﻿using System;
using System.Threading;

namespace MME.Hercules
{
    public class CameraUtility
    {
        //public static RDC.CameraSDK.Camera camera;
        public static AbstractCannon camera;

        public static bool InitializeCamera()
        {
            try
            {
                if (!ConfigUtility.GetValue("CameraEnabled").Equals("1"))
                    return true;

                if (ConfigUtility.GetValue("CameraName").Equals("Web"))
                    return true;

                //gw - determine if we use old or new sdk...
                string cam_for_sdk = ConfigUtility.GetValue("CameraName");
                bool old_sdk = true;
                if (cam_for_sdk.ToLower().Contains("eos"))
                    old_sdk = false;


                //  start sdk
                //gw camera = new RDC.CameraSDK.Camera();
                camera = new AbstractCannon(old_sdk);
                if (!camera.StartSDK())
                {
                    System.Windows.Forms.MessageBox.Show("ERROR: Cannot initialize Cannon SDK!");
                    return false;
                }
                camera.GetDevices();
                Thread.Sleep(1000);

                //  connect cam
                string cname = camera.ConnectedCameraName;
                if (!camera.Connect(ConfigUtility.GetValue("CameraName")))
                {
                    System.Windows.Forms.MessageBox.Show("ERROR: Cannot connect to camera!" +
                        ConfigUtility.GetValue("CameraName") + " but found " + cname);
                }

                //  parameters...
                camera.ZoomPos = Convert.ToInt32(ConfigUtility.GetValue("ZoomPos"));
                camera.ShootingMode = Convert.ToInt32(ConfigUtility.GetValue("ShootingMode"));
                camera.ExposureCompensation = Convert.ToInt32(ConfigUtility.GetValue("ExposureCompensation"));
                camera.ImageQuality = RDC.CameraSDK.prType.prptpImageQuality.Normal;

                // Default to bw if forcing to bw.
                CameraUtility.camera.PhotoEffect = 0;

                return true;
            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show("CameraUtility Error!");
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return false;
            }
        }

        public static bool IsConnected()
        {
            return (camera != null && camera.ConnectedCameraName.Equals(ConfigUtility.GetValue("CameraName")));
        }


        public static void TakePicture(string filepath)
        {
            if (!ConfigUtility.GetValue("CameraEnabled").Equals("1"))
                return;

            camera.Release(filepath);
        }

        public static void Shutdown()
        {
            if (!ConfigUtility.GetValue("CameraEnabled").Equals("1"))
                return;

            //gw camera.Disconnect();
            //gw camera.EndSDK();
        }
    }
}
