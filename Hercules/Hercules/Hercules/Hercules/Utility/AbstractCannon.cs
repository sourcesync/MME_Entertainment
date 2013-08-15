using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MME.Hercules
{
    public class AbstractCannon
    {
        public bool old = true;
        
        public static RDC.CameraSDK.Camera rdc_camera;
        public static MMECannon.MMECanon eos_camera;
        
        public string ConnectedCameraName;
        
        public Int32 _ZoomPos = 0;
        public Int32 ZoomPos
        {
            get { return _ZoomPos; }
            set { rdc_camera.ZoomPos = value; }
        }

        public Int32 _ShootingMode = 0;
        public Int32 ShootingMode
        {
            get { return _ShootingMode; }
            set { rdc_camera.ShootingMode = value; }
        }

        public Int32 _ExposureCompensation = 0;
        public Int32 ExposureCompensation
        {
            get { return _ExposureCompensation; }
            set { rdc_camera.ExposureCompensation = value; }
        }

        public RDC.CameraSDK.prType.prptpImageQuality  _ImageQuality;
        public RDC.CameraSDK.prType.prptpImageQuality ImageQuality
        {
            get { return _ImageQuality; }
            set { rdc_camera.ImageQuality = value; }
        }
                
        public Int32 _PhotoEffect = 0;
        public Int32 PhotoEffect
        {
            get { return _PhotoEffect; }
            set { rdc_camera.PhotoEffect = value; }
        }

        public AbstractCannon(Boolean old)
        {
            this.old = old;

            if (!this.old)
            {
                eos_camera = new MMECannon.MMECanon();
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

        public bool Connect(String name)
        {
            if (old)
            {
                rdc_camera.Connect(name);
                //  TODO: should do something here to check for old api...
                return true;
            }
            else
            {
                return eos_camera.takepic("test.jpg", false);
            }
        }


        public bool Release(string path)
        {
            if (old)
            {
                System.Drawing.Bitmap bm = rdc_camera.Release(path);
                if (bm != null) return true;
                else return false;
            }
            else
            {
                return eos_camera.takepic(path, true);
            }
        }

        public void Disconnect()
        {
            if (old)
            {
                rdc_camera.Disconnect();
            }
            else
            {

            }
        }

        public void EndSDK()
        {
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
