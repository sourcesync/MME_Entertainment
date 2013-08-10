using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDSDKLib;

namespace CCCBSv2
{
    class CC : EDSDK
    {
        IntPtr cRef;

        // Initilizes, takes picture and uninitilizes 
        public void RunSeq()
        {
            uint err;
            err = Initialize();
            if (err != EDS_ERR_OK)
            {
                return;
            }
            err = TakePicture();
            if (err != EDS_ERR_OK)
            {
                return;
            }
            Uninitilize();
        }

        public uint Initialize()
        {
            // err init
            uint err;
            // Initilize SDK.
            // SDK initilizion is needed to use EDSDK functions
            err = EdsInitializeSDK();

            // In case of error pass it on...
            if (err != EDS_ERR_OK)
            {
                return err;
            }

            // get first camera connected to computer
            err = GetFirstCamera();

            if (err != EDS_ERR_OK)
            {
                return err;
            }

            // Open session to camera
            EdsOpenSession(cRef);

            // Set object event handler, We need this event handler to retrieve pictures from camera
            err = EdsSetObjectEventHandler(cRef, ObjectEvent_All, ObjectEventHandler, IntPtr.Zero);

            if (err != EDS_ERR_OK)
            {
                return err;
            }

            // We don't want to save pictures to the camera so lets set save target to host
            err = EdsSetPropertyData(cRef, PropID_SaveTo, 0, 4, 2);

            return err;
        }

        // uninitilizes edsdk
        public uint Uninitilize()
        {
            uint err;
            err = EdsCloseSession(cRef);
            if (err != EDS_ERR_OK)
            {
                return err;
            }
            err = EdsTerminateSDK();

            return err;
        }

        // gets the first camera to cRef
        private uint GetFirstCamera()
        {
            // Variable initilazions
            IntPtr cameraList;
            uint err;
            int count;

            // Get list of cameras;
            err = EdsGetCameraList(out cameraList);

            if (err != EDS_ERR_OK)
            {
                return err;
            }

            // Get count of cameras in list
            err = EdsGetChildCount(cameraList, out count);

            if (err != EDS_ERR_OK)
            {
                return err;
            }
            if (count == 0)
            {
                return EDS_ERR_DEVICE_NOT_FOUND;
            }

            // Get first camera to public cRef
            err = EdsGetChildAtIndex(cameraList, 0, out cRef);

            if (err != EDS_ERR_OK)
            {
                return err;
            }

            // release camera list
            if (cameraList != null)
            {
                EdsRelease(cameraList);
            }

            // everything went just fine
            return EDS_ERR_OK;
        }

        public uint ObjectEventHandler(uint inEvent, IntPtr inRef, IntPtr inContext)
        {
            switch (inEvent)
            {
                // in case of a download request
                case ObjectEvent_DirItemRequestTransfer:
                    DownloadImage(inRef);
                    break;
                default:
                    break;
            }
            return EDS_ERR_OK;
        }

        // Downloads the picture from camera's buffer to the computer
        private uint DownloadImage(IntPtr dItem)
        {

            EdsDirectoryItemInfo dirInfo;
            uint err;
            IntPtr stream = new IntPtr();
            err = EdsGetDirectoryItemInfo(dItem, out dirInfo);

            if (err == EDS_ERR_OK)
            {
                err = EdsCreateFileStream(dirInfo.szFileName, EdsFileCreateDisposition.CreateAlways, EdsAccess.ReadWrite, out stream);
            }
            if (err == EDS_ERR_OK)
            {
                err = EdsDownload(dItem, dirInfo.Size, stream);
            }
            if (err == EDS_ERR_OK)
            {
                err = EdsDownloadComplete(dItem);
            }
            if (stream != null)
            {
                EdsRelease(stream);
            }
            return err;
        }

        // Takes picture
        public uint TakePicture()
        {
            return EdsSendCommand(cRef, CameraCommand_TakePicture, 0);
        }
    }
}