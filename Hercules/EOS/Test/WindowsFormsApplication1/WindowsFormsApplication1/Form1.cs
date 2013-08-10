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

            IntPtr camlist = IntPtr.Zero;
            i = EDSDKLib.EDSDK.EdsGetCameraList(out camlist);
            System.Windows.Forms.MessageBox.Show("Get Camera List status=" + i.ToString());

            int count = 0;
            i = EDSDKLib.EDSDK.EdsGetChildCount(camlist, out count);
            System.Windows.Forms.MessageBox.Show("Get Camera Count status=" + i.ToString() + " count=" + count.ToString());

            i = EDSDKLib.EDSDK.EdsTerminateSDK();
            System.Windows.Forms.MessageBox.Show("Terminate SDK status=" + i.ToString());
        }
    }
}
