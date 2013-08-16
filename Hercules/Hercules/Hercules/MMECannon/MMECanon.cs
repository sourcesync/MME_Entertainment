using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMECannon
{
    public class MMECanon
    {
        public static Boolean DEBUG = false;

        public bool download_done = false;

        public string path = "c:/temp/image.jpg";

        public string connected_camera_name;

        public uint downloadImage(IntPtr directoryItem)
        {
            uint err = 0;

            IntPtr stream = IntPtr.Zero;

            // Get directory item information
            EDSDKLib.EDSDK.EdsDirectoryItemInfo dirItemInfo;
            err = EDSDKLib.EDSDK.EdsGetDirectoryItemInfo(directoryItem, out dirItemInfo);

            // Create file stream for transfer destination
            if (err == EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                err = EDSDKLib.EDSDK.EdsCreateFileStream(
                    this.path,
                    //dirItemInfo.szFileName,
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

            this.download_done = true;

            return err;
        }

        private uint objectEventHandler(uint inEvent, IntPtr inRef, IntPtr inContext)
        {


            if (EDSDKLib.EDSDK.ObjectEvent_DirItemCreated == inEvent)
            {
                return 0;
            }
            else if (EDSDKLib.EDSDK.ObjectEvent_DirItemRequestTransfer == inEvent)
            {
                uint err = this.downloadImage(inRef);

            }


            return 0;
        }


        IntPtr _camlist = IntPtr.Zero;
        IntPtr _cam = IntPtr.Zero;
        EDSDKLib.EDSDK.EdsObjectEventHandler _edsObjectEventHandler = null;


        public Boolean init()
        {
            //EDSDKLib.EDSDK _sdk = new EDSDKLib.EDSDK();
            //IntPtr _camlist = IntPtr.Zero;
            //IntPtr _cam = IntPtr.Zero;
            //EDSDKLib.EDSDK.EdsObjectEventHandler _edsObjectEventHandler = null;
            this.download_done = false;

            uint i = EDSDKLib.EDSDK.EdsInitializeSDK();
            if (MMECanon.DEBUG) System.Windows.Forms.MessageBox.Show("Init SDK status=" + i.ToString());
            if (i == 0)
            {
                i = EDSDKLib.EDSDK.EdsGetCameraList(out _camlist);
                if (MMECanon.DEBUG) System.Windows.Forms.MessageBox.Show("Get Camera List status=" + i.ToString());
                if (i == 0)
                {
                    int count = 0;
                    i = EDSDKLib.EDSDK.EdsGetChildCount(_camlist, out count);
                    if (MMECanon.DEBUG) System.Windows.Forms.MessageBox.Show("Get Camera Count status=" + i.ToString() + " count=" + count.ToString());
                    if ((i == 0) && (count > 0))
                    {

                        i = EDSDKLib.EDSDK.EdsGetChildAtIndex(_camlist, 0, out _cam);
                        if (MMECanon.DEBUG) System.Windows.Forms.MessageBox.Show("Get Child at 0 status=" + i.ToString() + " " + _cam.ToString());
                        if (i == 0)
                        {

                            i = EDSDKLib.EDSDK.EdsRelease(_camlist);
                            if (MMECanon.DEBUG) System.Windows.Forms.MessageBox.Show("Release camlist status=" + i.ToString());

                            i = EDSDKLib.EDSDK.EdsOpenSession(_cam);
                            if (MMECanon.DEBUG) System.Windows.Forms.MessageBox.Show("Open Session status=" + i.ToString());
                            if (i == 0)
                            {

                                EDSDKLib.EDSDK.EdsDeviceInfo deviceInfo;
                                i = EDSDKLib.EDSDK.EdsGetDeviceInfo(_cam, out deviceInfo);
                                if (MMECanon.DEBUG) System.Windows.Forms.MessageBox.Show("Get Device Info status=" + i.ToString());
                                if (i == 0)
                                {
                                    this.connected_camera_name = deviceInfo.szDeviceDescription;
                                    if (MMECanon.DEBUG) System.Windows.Forms.MessageBox.Show("Device Info description= " + deviceInfo.szDeviceDescription);
                                }

                                EDSDKLib.EDSDK.EdsSaveTo toPC = EDSDKLib.EDSDK.EdsSaveTo.Host;
                                uint idata = (uint)toPC;
                                int sz = sizeof(EDSDKLib.EDSDK.EdsSaveTo);
                                i = EDSDKLib.EDSDK.EdsSetPropertyData(_cam, (uint)EDSDKLib.EDSDK.PropID_SaveTo, 0, sz, idata);
                                if (MMECanon.DEBUG) System.Windows.Forms.MessageBox.Show("Set Property SaveTo sz=" + sz.ToString() + " status=" + i.ToString());
                                //err = EdsSetPropertyData(camera_, kEdsPropID_SaveTo, 0, sizeof(EdsSaveTo), &toPC);


                                _edsObjectEventHandler = new EDSDKLib.EDSDK.EdsObjectEventHandler(objectEventHandler);
                                i = EDSDKLib.EDSDK.EdsSetObjectEventHandler(_cam,
                                    EDSDKLib.EDSDK.ObjectEvent_All,
                                    _edsObjectEventHandler,
                                    new IntPtr(0));
                                //System.Windows.Forms.MessageBox.Show("After Delegate=" + i.ToString());

                                if (i == 0)
                                    return true;

                            }
                        }
                    }
                }
                /*

                i = EDSDKLib.EDSDK.EdsCloseSession(_cam);
                if (MMECanon.DEBUG) System.Windows.Forms.MessageBox.Show("Close Session status=" + i.ToString());
                

                i = EDSDKLib.EDSDK.EdsTerminateSDK();
                if (MMECanon.DEBUG) System.Windows.Forms.MessageBox.Show("Terminate SDK status=" + i.ToString());
                 * */
            }

            return false;
        }


        public Boolean takepic(String path, bool wait, out uint err)
        {
            this.download_done = false;
            if (path == null) this.path = "takepic.jpg";
            else this.path = path;

            uint i = EDSDKLib.EDSDK.EdsSendCommand(_cam, EDSDKLib.EDSDK.CameraCommand_TakePicture, 0);
            err = i;
            if (i == 0)
            {
                if (wait)
                {
                    while (!this.download_done)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(50);
                    }

                    //  TODO: should timeout here...

                    return true;
                }
                else
                {
                    return true;
                }
            }
            else return false;
        }


        public Boolean finish()
        {
            Boolean b = true;
            uint i = 0;

            if (_edsObjectEventHandler != null)
            {
                _edsObjectEventHandler = null;
            }

            if (_cam != IntPtr.Zero)
            {
                i = EDSDKLib.EDSDK.EdsRelease(_cam);
                _cam = IntPtr.Zero;
            }

            if (_camlist != IntPtr.Zero)
            {
                i = EDSDKLib.EDSDK.EdsRelease(_camlist);
                _camlist = IntPtr.Zero;
            }

            i = EDSDKLib.EDSDK.EdsTerminateSDK();
            

            b = b && (i == 0);
            return b;
        }


    }
}
