using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                        if (i == 0)
                        {
                            System.Windows.Forms.MessageBox.Show("Get Child at 0 status=" + i.ToString());

                            i = EDSDKLib.EDSDK.EdsOpenSession( cam );
                            if (i == 0)
                            {
                                System.Windows.Forms.MessageBox.Show("Open Session status=" + i.ToString() + " about to take pic!");

                                //uint saveTo = (uint)EDSDKLib.EDSDK.EdsSaveTo.Host;

                                //i = EDSDKLib.EDSDK.EdsSetPropertyData(cam, EDSDKLib.EDSDK.Ed 0, sizeof(saveTo), &saveTo);

                                i = EDSDKLib.EDSDK.EdsSendCommand(cam, EDSDKLib.EDSDK.CameraCommand_TakePicture, 0);
                                //  kkEdsCameraCommand_TakePicture , 0);


                                System.Windows.Forms.MessageBox.Show("After Take Pic status=" + i.ToString());
                            }
                        }
                    }
                }

                i = EDSDKLib.EDSDK.EdsTerminateSDK();
                System.Windows.Forms.MessageBox.Show("Terminate SDK status=" + i.ToString());
            }
                
        }
    }
}
