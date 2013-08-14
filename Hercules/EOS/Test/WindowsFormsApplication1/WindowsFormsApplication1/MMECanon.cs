using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class MMECanon
    {
        public static Boolean DEBUG = true;

        public static EDSDKLib.EDSDK sdk = null;
        public static IntPtr camlist = IntPtr.Zero;
        public static IntPtr cam = IntPtr.Zero;
        public static EDSDKLib.EDSDK.EdsObjectEventHandler edsObjectEventHandler = null;

        public static Boolean finish()
        {
            Boolean b = true;
            uint i=0;

            if (edsObjectEventHandler!=null)
            {
                edsObjectEventHandler = null;
            }

            if (cam != IntPtr.Zero)
            {
                i = EDSDKLib.EDSDK.EdsRelease(cam);
                b = b && (i == 0);
                cam = IntPtr.Zero;
            }

            if (camlist != IntPtr.Zero)
            {
                i = EDSDKLib.EDSDK.EdsRelease(camlist);
                b = b && (i==0);
                camlist = IntPtr.Zero;
            }

            if (sdk!=null)
            {
                i = EDSDKLib.EDSDK.EdsTerminateSDK();
                b = b && (i==0);
                sdk = null;
            }

            return b;
        }

        public static uint downloadImage(IntPtr directoryItem)
        {
            uint err = 0;

            IntPtr stream = IntPtr.Zero;

            // Get directory item information
            EDSDKLib.EDSDK.EdsDirectoryItemInfo dirItemInfo;
            err = EDSDKLib.EDSDK.EdsGetDirectoryItemInfo(directoryItem, out dirItemInfo);

            // Create file stream for transfer destination
            if (err == EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                err = EDSDKLib.EDSDK.EdsCreateFileStream(dirItemInfo.szFileName,
                    EDSDKLib.EDSDK.EdsFileCreateDisposition.CreateAlways,
                    EDSDKLib.EDSDK.EdsAccess.ReadWrite,       
                     out stream);
            }

            // Download image
            if (err == EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                err = EDSDKLib.EDSDK.EdsDownload(directoryItem, dirItemInfo.Size, stream);
            }

            // Issue notification that download is complete
            if (err == EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                err = EDSDKLib.EDSDK.EdsDownloadComplete(directoryItem);
            }

            // Release stream
            if (stream != IntPtr.Zero)
            {
                EDSDKLib.EDSDK.EdsRelease(stream);
                stream = IntPtr.Zero;
            }

            return err;
        }

        private static uint objectEventHandler(uint inEvent, IntPtr inRef, IntPtr inContext)
        {
            if (EDSDKLib.EDSDK.ObjectEvent_DirItemCreated == inEvent)
            {
                return 0;
            }
            else if (EDSDKLib.EDSDK.ObjectEvent_DirItemRequestTransfer == inEvent)
            {
                uint err = downloadImage(inRef);

                //  cleanup sdk session...
                Boolean bf = finish();

                System.Windows.Forms.MessageBox.Show(err.ToString() + " " + bf.ToString());
            }


            return 0;
        }



        public static Boolean takepic()
        {
            sdk = new EDSDKLib.EDSDK();

            uint i = EDSDKLib.EDSDK.EdsInitializeSDK();
            if ( MMECanon.DEBUG ) System.Windows.Forms.MessageBox.Show("Init SDK status=" + i.ToString());
            if (i == 0)
            {
                i = EDSDKLib.EDSDK.EdsGetCameraList(out camlist);
                if (MMECanon.DEBUG) System.Windows.Forms.MessageBox.Show("Get Camera List status=" + i.ToString());
                if (i == 0)
                {
                    int count = 0;
                    i = EDSDKLib.EDSDK.EdsGetChildCount(camlist, out count);
                    if (MMECanon.DEBUG)  System.Windows.Forms.MessageBox.Show("Get Camera Count status=" + 
                            i.ToString() + " count=" + count.ToString());
                    if ((i == 0) && (count > 0))
                    {
                        IntPtr cam = IntPtr.Zero;
                        i = EDSDKLib.EDSDK.EdsGetChildAtIndex(camlist, 0, out cam);
                        if (MMECanon.DEBUG)  System.Windows.Forms.MessageBox.Show("Get Child at 0 status=" + i.ToString() + " " + cam.ToString());
                        if (i == 0)
                        {
                            i = EDSDKLib.EDSDK.EdsOpenSession(cam);
                            if (MMECanon.DEBUG) System.Windows.Forms.MessageBox.Show("Open Session status=" + i.ToString());
                            if (i == 0)
                            {
                                EDSDKLib.EDSDK.EdsDeviceInfo deviceInfo;
                                i = EDSDKLib.EDSDK.EdsGetDeviceInfo(cam, out deviceInfo);
                                if (MMECanon.DEBUG)  System.Windows.Forms.MessageBox.Show("Get Device Info status=" + i.ToString());
                                if (i == 0)
                                {
                                    if (MMECanon.DEBUG) 
                                        System.Windows.Forms.MessageBox.Show("Device Info description= " + deviceInfo.szDeviceDescription);
                                }

                                EDSDKLib.EDSDK.EdsSaveTo toPC = EDSDKLib.EDSDK.EdsSaveTo.Host;
                                uint idata = (uint)toPC;
                                int sz = sizeof(EDSDKLib.EDSDK.EdsSaveTo);
                                i = EDSDKLib.EDSDK.EdsSetPropertyData(cam, (uint)EDSDKLib.EDSDK.PropID_SaveTo, 0, sz, idata);
                                if (MMECanon.DEBUG) System.Windows.Forms.MessageBox.Show("Set Property SaveTo sz=" + sz.ToString() + " status=" + i.ToString());

                                if (i == 0)
                                {
                                    edsObjectEventHandler = 
                                        new EDSDKLib.EDSDK.EdsObjectEventHandler(objectEventHandler);

                                    i = EDSDKLib.EDSDK.EdsSetObjectEventHandler(cam,
                                        EDSDKLib.EDSDK.ObjectEvent_All,
                                        edsObjectEventHandler,
                                        new IntPtr(0));
                                    if (MMECanon.DEBUG) System.Windows.Forms.MessageBox.Show("After Delegate=" + i.ToString());
                                    if (i == 0)
                                    {
                                        i = EDSDKLib.EDSDK.EdsSendCommand(cam, EDSDKLib.EDSDK.CameraCommand_TakePicture, 1);
                                        if (MMECanon.DEBUG) System.Windows.Forms.MessageBox.Show("After Take Pic status=" + i.ToString());

                                        if (i == 0)
                                        {
                                            //  this means clean up must happen elsewhere at the object event...
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //  cleanup any resources from above...
            finish();

            //  got here, means something failed...
            return false;

        }
    }
}
