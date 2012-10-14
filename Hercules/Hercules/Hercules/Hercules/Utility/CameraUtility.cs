using System;
using System.Threading;

namespace MME.Hercules
{
    public class CameraUtility
    {
        public static RDC.CameraSDK.Camera camera;

        public static void InitializeCamera()
        {
            try
            {
                if (!ConfigUtility.GetValue("CameraEnabled").Equals("1"))
                    return;

                if (ConfigUtility.GetValue("CameraName").Equals("Web"))
                    return;

                // Start Camera
                camera = new RDC.CameraSDK.Camera();
                camera.StartSDK();


                camera.GetDevices();
                Thread.Sleep(1000);

                //gw
                string cname = camera.ConnectedCameraName;


                camera.Connect(ConfigUtility.GetValue("CameraName"));

                camera.ZoomPos = Convert.ToInt32(ConfigUtility.GetValue("ZoomPos"));
                camera.ShootingMode = Convert.ToInt32(ConfigUtility.GetValue("ShootingMode"));
                
                //System.Windows.Forms.MessageBox.Show(camera.ShootingMode.ToString());
                //System.Windows.Forms.MessageBox.Show(ConfigUtility.GetValue("ShootingMode"));
                //System.Windows.Forms.MessageBox.Show(camera.ShootingMode.ToString());
                camera.ExposureCompensation = Convert.ToInt32(ConfigUtility.GetValue("ExposureCompensation"));
                camera.ImageQuality = RDC.CameraSDK.prType.prptpImageQuality.Normal;

                //camera.

                // Default to bw if forcing to bw.
                CameraUtility.camera.PhotoEffect = 0;
            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show("error!");
                System.Windows.Forms.MessageBox.Show(e.ToString());
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

            camera.Disconnect();
            camera.EndSDK();
        }
    }
}
