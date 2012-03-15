using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RDC.CameraSDK;
using System.Threading;

namespace MME.Hercules.Forms.Operator
{
    public partial class Camera : Form
    {
        private RDC.CameraSDK.Camera camera;
        private List<int> brightnessValues;
        private Dictionary<string, int> exposureModesList;
        private Dictionary<string, int> photoEffectsList;
        private Dictionary<string, byte> AFDistanceList;
        private Dictionary<string, prType.prptpMetering> meteringModeList;
        private Dictionary<string, UInt32> imageSizeList;
        private Dictionary<string, prType.prptpImageQuality> imageQualityList;

        public Camera()
        {
            InitializeComponent();

            this.camera = CameraUtility.camera;

            brightnessValues = new List<int>();
            exposureModesList = new Dictionary<string, int>();

            camera_Connected(null, null);
            
           // camera.ReceivedFrame += new RDC.CameraSDK.Camera.ReceivedFrameEventHandler(camera_ReceivedFrame);           
            //camera.ImageAcquired += new RDC.CameraSDK.Camera.ImageAcquiredEventHandler(camera_ImageAcquired);


        }

        void camera_ImageAcquired(object sender, StreamEventArgs e)
        {
            camera.StartLiveViewer();
        }

        void camera_ReceivedFrame(object sender, StreamEventArgs e)
        {
            pictureBox1.Image = e.frame;
         
        }

        void camera_Connected(object sender, ConnectionEventArgs e)
        {
            camera.StartLiveViewer();
            zoom.Maximum = camera.MaxOpticalZoomPos;

            zoom.Value = camera.ZoomPos;

            brightnessValues.Clear();

            foreach (int u in camera.GetExposureCompensation().Values)
            {
                if (!brightnessValues.Contains(u))
                    brightnessValues.Add(u);
            }

            brightnessValues.Sort();

            brightness.Maximum = brightnessValues.Count - 1;

            string test = "";
            foreach (int f in brightnessValues)
            {
                test += f.ToString() + ", ";
            }

            MessageBox.Show(test);

            brightness.Value = brightnessValues.IndexOf(camera.ExposureCompensation);


            photoEffectsList = camera.GetPhotoEffects();

            photoEffects.Items.Clear();

            if (photoEffectsList != null)
            {
                foreach (string key in photoEffectsList.Keys)
                {
                    photoEffects.Items.Add(key);
                }
            }

            exposureModesList = camera.GetShootingModes();

            exposureModes.Items.Clear();

            if (exposureModesList != null)
            {
                foreach (string key in exposureModesList.Keys)
                {
                    exposureModes.Items.Add(key);
                }
            }

            AFDistance.Items.Clear();

            AFDistanceList = camera.GetAFDistances();

            if (AFDistanceList != null)
            {
                foreach (string key in AFDistanceList.Keys)
                {
                    AFDistance.Items.Add(key);
                }
            }

            meteringModeList = camera.GetMeteringModes();

            meteringModes.Items.Clear();

            if (meteringModeList != null)
            {
                foreach (string key in meteringModeList.Keys)
                {
                    meteringModes.Items.Add(key);
                }
            }

            imageSizes.Items.Clear();

            imageSizeList = camera.GetImageSizeFormats();

            if (imageSizeList != null)
            {
                foreach (string key in imageSizeList.Keys)
                {
                    imageSizes.Items.Add(key);
                }
            }

            imageQualityList = camera.GetImageQualityFormats();
            imageQualities.Items.Clear();

            if (imageQualityList != null)
            {
                foreach (string key in imageQualityList.Keys)
                {
                    imageQualities.Items.Add(key);
                }
            }

        }

  

        private void OnChangeZoom(object sender, EventArgs e)
        {
            camera.ZoomPos = zoom.Value;
            //ConfigUtility.SetValue("ZoomPos", zoom.Value.ToString());
        }

        private void brightness_Scroll(object sender, EventArgs e)
        {
            if (brightnessValues.Count >= brightness.Value)
            {
                MessageBox.Show(brightnessValues[brightness.Value].ToString());
                camera.ExposureCompensation = brightnessValues[brightness.Value];
              //  ConfigUtility.SetValue("ExposureCompensation", brightnessValues[brightness.Value].ToString());
            }
        }

        private void photoEffects_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ef = photoEffects.SelectedItem as string;

            if (ef == "BW" || ef == "Off")
                MessageBox.Show(photoEffectsList[ef].ToString());

            if (ef != null && photoEffectsList.ContainsKey(ef))
            {
                camera.PhotoEffect = photoEffectsList[ef];
            }
        }

        private void exposureModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mode = exposureModes.SelectedItem as string;

            if (mode != null && exposureModesList.ContainsKey(mode))
            {
                camera.ShootingMode = exposureModesList[mode];
            }
        }

        private void AFDistance_SelectedIndexChanged(object sender, EventArgs e)
        {
            string distance = AFDistance.SelectedItem as string;

            if (distance != null && AFDistanceList.ContainsKey(distance))
            {
                camera.AFDistance = (prType.prptpAFDistance)AFDistanceList[distance];
            }
        }

        private void meteringModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mode = meteringModes.SelectedItem as string;

            if (mode != null && meteringModeList.ContainsKey(mode))
            {
                camera.MeteringMode = meteringModeList[mode];
            }

        }

        private void Camera_Load(object sender, EventArgs e)
        {
            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;
        }

        private void TakePicture()
        {
           
        }       

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
     

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;



        }

        private void Camera_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            camera.AsyncRelease("ron.jpg");
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
        }

        private void imageSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mode = imageSizes.SelectedItem as string;

            if (mode != null && imageSizeList.ContainsKey(mode))
            {
                MessageBox.Show(Convert.ToInt32(imageSizeList[mode]).ToString());
            }

        }

        private void imageQualities_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mode = imageQualities.SelectedItem as string;

            if (mode != null && imageQualityList.ContainsKey(mode))
            {
                MessageBox.Show(Convert.ToInt32(imageQualityList[mode]).ToString());
            }
        }

       
       
    }
}
