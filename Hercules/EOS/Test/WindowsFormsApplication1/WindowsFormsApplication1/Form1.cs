using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //EDSDKLib.EDSDK.EdsError 
        uint downloadImage(IntPtr directoryItem)
        {
            //EDSDKLib.EDSDK.EdsError err = EDSDKLib.EDSDK.EDS_ERR_OK;
            uint err = 0;

            //EDSDKLib.EDSDK.EdsStreamRef stream = NULL;
            IntPtr stream = IntPtr.Zero;

            // Get directory item information
            EDSDKLib.EDSDK.EdsDirectoryItemInfo dirItemInfo;
            err = EDSDKLib.EDSDK.EdsGetDirectoryItemInfo(directoryItem, out dirItemInfo);

            // Create file stream for transfer destination
            if (err == EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                err = EDSDKLib.EDSDK.EdsCreateFileStream(dirItemInfo.szFileName,
                    EDSDKLib.EDSDK.EdsFileCreateDisposition.CreateAlways,
                    //EDSDKLib.EDSDK.kEdsFile_CreateAlways,
                    EDSDKLib.EDSDK.EdsAccess.ReadWrite,
                    //EDSDKLib.EDSDK.kEdsAccess_ReadWrite,          
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

        private uint objectEventHandler(uint inEvent, IntPtr inRef, IntPtr inContext) 
        {
            if (EDSDKLib.EDSDK.ObjectEvent_DirItemCreated == inEvent) 
            {
                //  ownload the image
                return 0;
            }

            return 0;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            EDSDKLib.EDSDK sdk = new EDSDKLib.EDSDK();
            //sdk.

            uint i = EDSDKLib.EDSDK.EdsInitializeSDK();
            System.Windows.Forms.MessageBox.Show("Init SDK status=" + i.ToString());
            if (i == 0)
            {
                IntPtr camlist = IntPtr.Zero;
                i = EDSDKLib.EDSDK.EdsGetCameraList(out camlist);
                System.Windows.Forms.MessageBox.Show("Get Camera List status=" + i.ToString());
                if (i == 0)
                {
                    int count = 0;
                    i = EDSDKLib.EDSDK.EdsGetChildCount(camlist, out count);
                    System.Windows.Forms.MessageBox.Show("Get Camera Count status=" + i.ToString() + " count=" + count.ToString());
                    if ((i == 0) && (count > 0))
                    {

                        IntPtr cam = IntPtr.Zero;
                        i = EDSDKLib.EDSDK.EdsGetChildAtIndex(camlist, 0, out cam);
                        System.Windows.Forms.MessageBox.Show("Get Child at 0 status=" + i.ToString() + " " + cam.ToString());
                        if (i == 0)
                        {

                            i = EDSDKLib.EDSDK.EdsOpenSession(cam);
                            System.Windows.Forms.MessageBox.Show("Open Session status=" + i.ToString() );
                            if (i == 0)
                            {

                                //uint saveTo = (uint)EDSDKLib.EDSDK.EdsSaveTo.Host;

                                //EdsDeviceInfo deviceInfo;
                                EDSDKLib.EDSDK.EdsDeviceInfo deviceInfo;
                                i = EDSDKLib.EDSDK.EdsGetDeviceInfo( cam, out deviceInfo );
                                System.Windows.Forms.MessageBox.Show("Get Device Info status=" + i.ToString());
                                if (i == 0)
                                {
                                    System.Windows.Forms.MessageBox.Show("Device Info description= " + deviceInfo.szDeviceDescription);
                                }

                                EDSDKLib.EDSDK.EdsSaveTo toPC = EDSDKLib.EDSDK.EdsSaveTo.Camera;
                                uint idata = (uint)toPC;
                                int sz = sizeof(EDSDKLib.EDSDK.EdsSaveTo);
                                i = EDSDKLib.EDSDK.EdsSetPropertyData(cam, (uint)EDSDKLib.EDSDK.PropID_SaveTo, 0, sz,  idata);
                                System.Windows.Forms.MessageBox.Show("Set Property SaveTo sz=" + sz.ToString() + " status=" + i.ToString());
                                //err = EdsSetPropertyData(camera_, kEdsPropID_SaveTo, 0, sizeof(EdsSaveTo), &toPC);


                                EDSDKLib.EDSDK.EdsObjectEventHandler edsObjectEventHandler = 
                                    new EDSDKLib.EDSDK.EdsObjectEventHandler(objectEventHandler);

                                i = EDSDKLib.EDSDK.EdsSetObjectEventHandler(cam, 
                                    EDSDKLib.EDSDK.ObjectEvent_All, 
                                    edsObjectEventHandler, 
                                    new IntPtr(0));
                                System.Windows.Forms.MessageBox.Show("After Delegate=" + i.ToString());

                                //i = EDSDKLib.EDSDK.EdsSetPropertyData(cam, EDSDKLib.EDSDK.PropID_SaveTo, 0, sizeof(saveTo), &saveTo);
                                i = EDSDKLib.EDSDK.EdsSendCommand( cam, EDSDKLib.EDSDK.CameraCommand_TakePicture, 1);
                                System.Windows.Forms.MessageBox.Show("After Take Pic status=" + i.ToString());

                                i = EDSDKLib.EDSDK.EdsCloseSession(cam);
                                if (i == 0)
                                {
                                    System.Windows.Forms.MessageBox.Show("Close Session status=" + i.ToString() );
                                }
                            }
                        }
                    }
                }

                i = EDSDKLib.EDSDK.EdsTerminateSDK();
                System.Windows.Forms.MessageBox.Show("Terminate SDK status=" + i.ToString());
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MMECanon cannon = new MMECanon();
            Boolean b = cannon.takepic();
            System.Windows.Forms.MessageBox.Show(b.ToString());
        }

        MMECanon cannon = null;


        private void button4_Click(object sender, EventArgs e)
        {
            cannon = new MMECanon();
            Boolean b = cannon.init();
            System.Windows.Forms.MessageBox.Show(b.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Boolean b = cannon.takepic();
            System.Windows.Forms.MessageBox.Show(b.ToString());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Boolean b = cannon.finish();
            System.Windows.Forms.MessageBox.Show(b.ToString());
        }

        Thread t1 = null;

        private void ThreadTakePic()
        {
            cannon = new MMECanon();
            Boolean b = cannon.init();
            System.Console.WriteLine("init " + b.ToString());

            if (b)
            {
                b = cannon.takepic();
                System.Console.WriteLine("take " + b.ToString());

            }

            b = cannon.finish();
            System.Console.WriteLine("finish " + b.ToString());

        }

        private void ThreadTakePic2()
        {

            Boolean b = cannon.takepic();
            System.Console.WriteLine("take " + b.ToString());

            /*
            while (!cannon.download_done)
            {
                System.Threading.Thread.Sleep(10);
            }
             * */

        }



        private void ThreadWait()
        {

            while (!cannon.download_done)
            {
                System.Threading.Thread.Sleep(10);
                Application.DoEvents();
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            t1 = new Thread(ThreadTakePic);
            t1.Start();

            t1.Join();

            bool b = false;
        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            Boolean b = cannon.takepic();
            if (b)
            {

                t1 = new Thread(ThreadWait);
                t1.Start();

                t1.Join();
            }
        }

        private void cam_event(object o, System.EventArgs args)
        {
            Boolean b = cannon.takepic();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Delegate del = new System.EventHandler( this.cam_event );
            this.button1.Invoke(del);
        }
    }
}
