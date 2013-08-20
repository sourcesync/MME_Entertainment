using System;
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

                System.Windows.Forms.Application.DoEvents();

                camera.GetDevices();


                System.Windows.Forms.Application.DoEvents();
                Thread.Sleep(2000);


                System.Windows.Forms.Application.DoEvents();

                //  connect cam
                string cname = camera.ConnectedCameraName;
                uint status = camera.Connect(ConfigUtility.GetValue("CameraName"));
                if (status!=0)
                {
                    System.Windows.Forms.MessageBox.Show("ERROR: Cannot connect to camera:" +
                        " found camera-> " + cname + " error=" + status);
                    return false;
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
