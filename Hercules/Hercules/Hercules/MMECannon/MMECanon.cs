using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMECannon
{
    public class MMECanon
    {
        public bool DEBUG = true;

        public bool download_done = false;

        public string path = "c:/temp/image.jpg";

        public string connected_camera_name;

        public System.Windows.Forms.PictureBox pb;

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

        private uint propertyEventHandler(uint inEvent, uint propID, uint q, IntPtr inContext)
        {
            if (DEBUG) System.Windows.Forms.MessageBox.Show("got prop " + inEvent.ToString() + " " + 
                propID.ToString() + " " + q.ToString());

            if (this.pb!=null)
            {
                while (true)
                {
                    downloadEvfData(_cam, this.pb);
                    System.Windows.Forms.Application.DoEvents();
                }
            }

            return 0;
        }

        IntPtr _camlist = IntPtr.Zero;
        IntPtr _cam = IntPtr.Zero;
        EDSDKLib.EDSDK.EdsObjectEventHandler _edsObjectEventHandler = null;

        EDSDKLib.EDSDK.EdsPropertyEventHandler _edsPropertyEventHandler = null;

        public Boolean init(bool debug)
        {
            this.DEBUG = debug;
            this.download_done = false;

            uint i = EDSDKLib.EDSDK.EdsInitializeSDK();
            if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Init SDK status=" + i.ToString());
            if (i == 0)
            {
                i = EDSDKLib.EDSDK.EdsGetCameraList(out _camlist);
                if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Get Camera List status=" + i.ToString());
                if (i == 0)
                {
                    int count = 0;
                    i = EDSDKLib.EDSDK.EdsGetChildCount(_camlist, out count);
                    if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Get Camera Count status=" + i.ToString() + " count=" + count.ToString());
                    if ((i == 0) && (count > 0))
                    {

                        i = EDSDKLib.EDSDK.EdsGetChildAtIndex(_camlist, 0, out _cam);
                        if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Get Child at 0 status=" + i.ToString() + " " + _cam.ToString());
                        if (i == 0)
                        {

                            i = EDSDKLib.EDSDK.EdsRelease(_camlist);
                            if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Release camlist status=" + i.ToString());

                            i = EDSDKLib.EDSDK.EdsOpenSession(_cam);
                            if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Open Session status=" + i.ToString());
                            if (i == 0)
                            {
                                // get device info...
                                EDSDKLib.EDSDK.EdsDeviceInfo deviceInfo;
                                i = EDSDKLib.EDSDK.EdsGetDeviceInfo(_cam, out deviceInfo);
                                if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Get Device Info status=" + i.ToString());
                                if (i == 0)
                                {
                                    this.connected_camera_name = deviceInfo.szDeviceDescription;
                                    if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Device Info description= " + deviceInfo.szDeviceDescription);
                                }

                                /* DEVICE IS BUSY ERROR
                                // set camera param...
                                int ssz = sizeof(uint);
                                uint vval = EDSDKLib.EDSDK.AEMode_Mamual;
                                i = EDSDKLib.EDSDK.EdsSetPropertyData(_cam, (uint)EDSDKLib.EDSDK.PropID_AEModeSelect, 0, ssz, vval);
                                if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Set Property AEMode sz=" + 
                                    ssz.ToString() + " vval=" + vval + " status=" + i.ToString());
                                 * */

                                /* ERR, NOT SUPPORTED ERR
                                // set camera param...
                                int ssz = sizeof(uint);
                                uint vval = EDSDKLib.EDSDK.AEMode_Mamual;
                                i = EDSDKLib.EDSDK.EdsSetPropertyData(_cam, (uint)EDSDKLib.EDSDK.PropID_AEMode, 0, ssz, vval);
                                if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Set Property AEMode sz=" +
                                    ssz.ToString() + " vval=" + vval + " status=" + i.ToString());
                                 * */

                                /*
                                int ssz = sizeof(uint);
                                uint vval = 0;
                                i = EDSDKLib.EDSDK.EdsSetPropertyData(_cam, (uint)EDSDKLib.EDSDK.PropID_ExposureCompensation, 
                                    0, ssz, vval);
                                if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Set Property EM sz=" +
                                    ssz.ToString() + " vval=" + vval + " status=" + i.ToString());
                                 * */
                               

                                EDSDKLib.EDSDK.EdsSaveTo toPC = EDSDKLib.EDSDK.EdsSaveTo.Host;
                                uint idata = (uint)toPC;
                                int sz = sizeof(EDSDKLib.EDSDK.EdsSaveTo);
                                i = EDSDKLib.EDSDK.EdsSetPropertyData(_cam, (uint)EDSDKLib.EDSDK.PropID_SaveTo, 0, sz, idata);
                                if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Set Property SaveTo sz=" + sz.ToString() + " status=" + i.ToString());
                                //err = EdsSetPropertyData(camera_, kEdsPropID_SaveTo, 0, sizeof(EdsSaveTo), &toPC);

                                //  Set capacity might help...
                                EDSDKLib.EDSDK.EdsCapacity capacity = new EDSDKLib.EDSDK.EdsCapacity();
                                capacity.NumberOfFreeClusters = 0x7FFFFFFF;
                                capacity.BytesPerSector = 0x1000;
                                capacity.Reset = 1;
                                i = EDSDKLib.EDSDK.EdsSetCapacity(_cam, capacity);
                                if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Set Capacity status=" + i.ToString());

                                if (i == 0)
                                {
                                    _edsObjectEventHandler = new EDSDKLib.EDSDK.EdsObjectEventHandler(objectEventHandler);
                                    i = EDSDKLib.EDSDK.EdsSetObjectEventHandler(_cam,
                                        EDSDKLib.EDSDK.ObjectEvent_All,
                                        _edsObjectEventHandler,
                                        new IntPtr(0));
                                }

                                if (i == 0)
                                    return true;

                            }
                        }
                    }
                }
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


        public uint downloadEvfData(IntPtr camera, System.Windows.Forms.PictureBox pb )
        {
            uint err = 0;
            //EdsStreamRef stream = NULL;
            IntPtr stream = IntPtr.Zero;
            //EdsEvfImageRef = NULL;
            IntPtr image = IntPtr.Zero;

            // Create memory stream.
            //err = EdsCreateMemoryStream(0, &stream);
            err = EDSDKLib.EDSDK.EdsCreateMemoryStream(0, out stream);
            if (DEBUG) System.Windows.Forms.MessageBox.Show("eds create mem stream=" + err.ToString());
            if (err==0)
            {
                //err = EdsCreateEvfImageRef(stream, &evfImage);
                err = EDSDKLib.EDSDK.EdsCreateEvfImageRef(stream, out image);
                if (DEBUG) System.Windows.Forms.MessageBox.Show("eds create evt image ref=" + err.ToString());

                if (err == 0)
                {
                    err = EDSDKLib.EDSDK.EdsDownloadEvfImage(camera, image);
                    if (DEBUG) System.Windows.Forms.MessageBox.Show("eds download evt image=" + err.ToString());
                    if (err == 0)
                    {
                        uint length=0;
                        err = EDSDKLib.EDSDK.EdsGetLength(stream, out length);
                        if (DEBUG) System.Windows.Forms.MessageBox.Show("get length image=" + err.ToString() +
                            " " + length.ToString());
                    
                        if ( (err==0)&&(length>0) )
                        {
                            IntPtr jpgPointer = IntPtr.Zero;
                            err = EDSDKLib.EDSDK.EdsGetPointer(stream, out jpgPointer);
                            if (this.DEBUG) System.Windows.Forms.MessageBox.Show("get jptPointer=" + err.ToString());
                            if (err==0)
                            {
                                System.Drawing.Bitmap bm = null;
                                unsafe
                                {
                                    byte* bt = (byte*)jpgPointer.ToPointer();
                                    System.IO.UnmanagedMemoryStream ums = new System.IO.UnmanagedMemoryStream
                                        (bt, length, length, System.IO.FileAccess.Read);
                                    bm = new System.Drawing.Bitmap(ums, true);
                                    if (this.DEBUG)  System.Windows.Forms.MessageBox.Show(bm.RawFormat.ToString() +
                                        " " + bm.Size.ToString());
                                    //bm = new System.Drawing.Bitmap(
                                }

                                if (pb!=null)
                                {
                                    pb.Image = bm;
                                    System.Windows.Forms.Application.DoEvents();
                                    //downloadEvfData(camera, pb);
                                }
                            }
                        }
                    }
                }

                EDSDKLib.EDSDK.EdsRelease(stream);
                EDSDKLib.EDSDK.EdsRelease(image);

            }
            return err;
        }

        /*
        public unsafe static System.Drawing.Bitmap GetEvfImage(IntPtr evfStream)
        {
            IntPtr jpgPointer;
            uint err;
            uint length = 0;
            System.Drawing.Bitmap i = null;

            err = EDSDK.EdsGetPointer(evfStream, out jpgPointer);

            if (err == EDSDK.EDS_ERR_OK)
                err = EDSDK.EdsGetLength(evfStream, out length);

            if (err == EDSDK.EDS_ERR_OK)
            {
                if (length != 0)
                {
                    UnmanagedMemoryStream ums = new UnmanagedMemoryStream
                    ((byte*)jpgPointer.ToPointer(), length, length, FileAccess.Read);
                    i = new Bitmap(ums, true);
                }
            }
            return i;
        }
        */

        public uint startLiveview(out uint err, System.Windows.Forms.PictureBox pb)
        {
            this.pb = pb;

            uint i = EDSDKLib.EDSDK.EdsInitializeSDK();
            if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Init SDK status=" + i.ToString());
            if (i == 0)
            {
                i = EDSDKLib.EDSDK.EdsGetCameraList(out _camlist);
                if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Get Camera List status=" + i.ToString());
                if (i == 0)
                {
                    int count = 0;
                    i = EDSDKLib.EDSDK.EdsGetChildCount(_camlist, out count);
                    if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Get Camera Count status=" + i.ToString() + " count=" + count.ToString());
                    if ((i == 0) && (count > 0))
                    {

                        i = EDSDKLib.EDSDK.EdsGetChildAtIndex(_camlist, 0, out _cam);
                        if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Get Child at 0 status=" + i.ToString() + " " + _cam.ToString());
                        if (i == 0)
                        {

                            i = EDSDKLib.EDSDK.EdsRelease(_camlist);
                            if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Release camlist status=" + i.ToString());

                            i = EDSDKLib.EDSDK.EdsOpenSession(_cam);
                            if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Open Session status=" + i.ToString());
                            if (i == 0)
                            {

                                EDSDKLib.EDSDK.EdsDeviceInfo deviceInfo;
                                i = EDSDKLib.EDSDK.EdsGetDeviceInfo(_cam, out deviceInfo);
                                if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Get Device Info status=" + i.ToString());
                                if (i == 0)
                                {
                                    this.connected_camera_name = deviceInfo.szDeviceDescription;
                                    if (this.DEBUG) System.Windows.Forms.MessageBox.Show("Device Info description= " + deviceInfo.szDeviceDescription);
                                }

                                // Get the output device for the live view image

                                IntPtr ptr = System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeof(UInt32));
                                i = EDSDKLib.EDSDK.EdsGetPropertyData(_cam, EDSDKLib.EDSDK.PropID_Evf_OutputDevice, 0,
                                    sizeof(UInt32), ptr);
                                if (this.DEBUG) System.Windows.Forms.MessageBox.Show("EDS GET PROPERTY DATA=" + i.ToString());

                                if (i == 0)
                                {
                                    _edsPropertyEventHandler = new EDSDKLib.EDSDK.EdsPropertyEventHandler( propertyEventHandler );

                                    i = EDSDKLib.EDSDK.EdsSetPropertyEventHandler(_cam,
                                        EDSDKLib.EDSDK.PropertyEvent_All,
                                        _edsPropertyEventHandler,
                                        new IntPtr(0));
                                    if (this.DEBUG) System.Windows.Forms.MessageBox.Show("SET PROP EVT HANDLER=" + i.ToString());

                                    if (i == 0)
                                    {
                                        UInt32 device = (UInt32)System.Runtime.InteropServices.Marshal.ReadInt32(ptr);
                                        device |= EDSDKLib.EDSDK.EvfOutputDevice_PC;
                                        i = EDSDKLib.EDSDK.EdsSetPropertyData(_cam,
                                            EDSDKLib.EDSDK.PropID_Evf_OutputDevice, 0,
                                            sizeof(UInt32), device);
                                        if (this.DEBUG)  System.Windows.Forms.MessageBox.Show("EDS SET PROPERTY DATA=" + i.ToString() + " " +
                                            device.ToString() + " ");

                                        i = EDSDKLib.EDSDK.EdsGetPropertyData(_cam, EDSDKLib.EDSDK.PropID_Evf_OutputDevice, 0,
                                            sizeof(UInt32), ptr);
                                        device = (UInt32)System.Runtime.InteropServices.Marshal.ReadInt32(ptr);
                                        if (this.DEBUG) System.Windows.Forms.MessageBox.Show("EDS GET PROPERTY DATA 2=" + i.ToString() + " " + device.ToString());

                                    
                                    }
                                }
                            }
                        }
                    }
                }
            }
            err = i;
            return i;
        }
    }
        
}
               
