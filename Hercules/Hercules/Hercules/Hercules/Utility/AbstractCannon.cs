using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MME.Hercules
{
    public class AbstractCannon
    {
        public bool DEBUG = false;

        public bool old = true;
        
        public static RDC.CameraSDK.Camera rdc_camera;
        public static MMECannon.MMECanon eos_camera;
        
        public string ConnectedCameraName;
        
        public Int32 _ZoomPos = 0;
        public Int32 ZoomPos
        {
            get { return _ZoomPos; }
            set { if (rdc_camera!=null) rdc_camera.ZoomPos = value; }
        }

        public Int32 _ShootingMode = 0;
        public Int32 ShootingMode
        {
            get { return _ShootingMode; }
            set { if (rdc_camera != null) rdc_camera.ShootingMode = value; }
        }

        public Int32 _ExposureCompensation = 0;
        public Int32 ExposureCompensation
        {
            get { return _ExposureCompensation; }
            set { if (rdc_camera != null)  rdc_camera.ExposureCompensation = value; }
        }

        public RDC.CameraSDK.prType.prptpImageQuality  _ImageQuality;
        public RDC.CameraSDK.prType.prptpImageQuality ImageQuality
        {
            get { return _ImageQuality; }
            set { if (rdc_camera != null) rdc_camera.ImageQuality = value; }
        }
                
        public Int32 _PhotoEffect = 0;
        public Int32 PhotoEffect
        {
            get { return _PhotoEffect; }
            set { if (rdc_camera != null) rdc_camera.PhotoEffect = value; }
        }

        public AbstractCannon(Boolean old)
        {
            this.old = old;

            if (!this.old)
            {
                eos_camera = new MMECannon.MMECanon();
            }
            else
            {
                rdc_camera = new RDC.CameraSDK.Camera();
            }
        }

        public bool StartSDK()
        {
            if (old)
            {
                return rdc_camera.StartSDK();
            }
            else
            {
                return eos_camera.init();
            }
        }

        public void GetDevices()
        {
            if (old)
            {
                rdc_camera.GetDevices();
                this.ConnectedCameraName = rdc_camera.ConnectedCameraName;
            }
            else
            {
                this.ConnectedCameraName = eos_camera.connected_camera_name;
            }
        }

        public uint Connect(String name)
        {
            if (old)
            {
                rdc_camera.Connect(name);
                //  TODO: should do something here to check for old api...
                return 0;
            }
            else
            {
                if (DEBUG)
                    System.Windows.Forms.MessageBox.Show("About to take test pic");

                uint err = 0;
                bool b = eos_camera.takepic("test.jpg", false, out err);
                if (!b) return err;
                else return 0;
            }
        }


        public uint Release(string path)
        {
            if (old)
            {
                System.Drawing.Bitmap bm = rdc_camera.Release(path);
                if (bm != null) return 1;
                else return 0;
            }
            else
            {
                uint err = 0;
                bool b = eos_camera.takepic(path, true, out err);
                if (!b) return err;
                else return 0;
            }
        }

        public void Disconnect()
        {
            if (DEBUG) System.Windows.Forms.MessageBox.Show("About to call disconnect");

            if (old)
            {
                rdc_camera.Disconnect();
            }
            else
            {
                //eos_camera.
            }
        }

        public void EndSDK()
        {
            if (DEBUG) System.Windows.Forms.MessageBox.Show("About to end sdk");

            if (old)
            {
                rdc_camera.EndSDK();
            }
            else
            {
                eos_camera.finish();
            }
        }
    }

}
