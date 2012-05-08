using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Media;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.Live;
using Microsoft.Expression.Encoder;

namespace MME.Hercules.Forms.User
{
    public partial class TakePhotos : Form
    {
        private Thread thread;
        private Thread prompts;
        private Session currentSession;
        private List<string> selected = new List<string>();
        private int current = 0;
        private bool ClearToStart = false;

        //gw
        private bool show_countdown = false;
        private bool PicProcessed = false;
        private Image proc_image = null;
        private Thread proc_thread = null;
        //private SoundPlayer one = null;
        //private SoundPlayer two = null;
        //private SoundPlayer three = null; 
        //private SoundPlayer four = null;
        private Image onei = null;
        private Image twoi = null;
        private Image threei = null;
        private Image fouri = null;
        private SoundPlayer countdown = null;
        private bool istable = false; 
        String[] camname ={ "",""};
        private LiveJob[] _job = {null,null};
        private LiveDeviceSource[] _deviceSource = {null,null};
        private bool[] _bStartedRecording = {false,false};
        private Panel[] vidPanel = {null,null};
        //gw

        public TakePhotos(Session currentSession)
        {
            InitializeComponent();

            this.vidPanel[0] = this.panelVideoOne;
            this.vidPanel[1] = this.panelVideoTwo;

            if (ConfigUtility.GetValue("BoothType") == "2")
            {
                istable = true;

                this.info.Visible = false;
            }


            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.currentSession = currentSession;


            //gw
            if (ConfigUtility.GetValue("SHOW_COUNTDOWN").Equals("1")) // show countdown images...
            {
                this.show_countdown = true;
                onei = WindowUtility.GetScreenImage(Hercules.Properties.Resources.NUMBER_ONE);
                twoi = WindowUtility.GetScreenImage(Hercules.Properties.Resources.NUMBER_TWO);
                threei = WindowUtility.GetScreenImage(Hercules.Properties.Resources.NUMBER_THREE);
                fouri = WindowUtility.GetScreenImage(Hercules.Properties.Resources.NUMBER_FOUR);
            }
            //gw
        }

        protected void CountdownTimer()
        {
            SoundUtility.Play(Hercules.Properties.SoundResources.COUNTDOWN);
        }

        void Writelog(string s)
        {
            StreamWriter SW;
            SW = File.AppendText("systemlog.txt");
            SW.WriteLine(DateTime.Now.ToLongTimeString() + ": " + s);

            SW.Close();
        }



        private void Pause()
        {
           
        }

        public void PromptThread()
        {
            int countdownwait = 0;
            if (!this.show_countdown)
            {
                countdownwait = Convert.ToInt32(ConfigUtility.GetValue("CountdownWait"));

                if (this.currentSession.SelectedColorType == ColorType.BW)
                {
                    countdownwait += Convert.ToInt32(ConfigUtility.GetValue("CountdownBW"));
                }
            }

            while (1==1)
            {
                if (ClearToStart)
                {
                    //gw
                    if (!this.show_countdown)
                    {
                        Thread.Sleep(countdownwait);
                        SoundUtility.Play(Hercules.Properties.SoundResources.COUNTDOWN);
                    }
                    else
                    {
                        SoundUtility.PlaySync(Hercules.Properties.SoundResources.COUNTDOWN);
                    }

                    //gw
                    ClearToStart = false;
                }
                else
                {
                    Thread.Sleep(10);
                }


                if (current == ConfigUtility.PhotoCount)
                {
                    Thread.Sleep(4000);
                    SoundUtility.Play(Hercules.Properties.SoundResources.PROCESSING_PHOTOS);
                    break;
                }
            }
        }

        private void LoadPoses()
        {
            for (int i = 0; i < ConfigUtility.PhotoCount; i++)
            {
                if (ConfigUtility.PhotoBackgrounds > 0)
                {
                    string ctl_name = "pictureBox" + (i+3).ToString();
                    Control[] ctl = this.Controls.Find( ctl_name, true );
                    if (ctl.Length == 1)
                    {
                        PictureBox pb = (PictureBox)ctl[0];
                        pb.Visible = true;
                        int bgindex = this.currentSession.SelectedBackgrounds[i];
                        string fname_base = "bg" + (bgindex).ToString();
                        pb.Load(string.Format("Skins\\{0}\\Backgrounds\\{1}pose.jpg",
                            ConfigUtility.Skin,
                            fname_base));
                    }
                }
            }
        }

        private void ShowCountdown(bool use_images, int which)
        {
            this.pictureBox2.Visible = false;
            this.pictureBox1.Visible = false;
            if (use_images)
            {
                this.pictureBox1.Visible = true;
            }
            if (use_images)
            {
                this.pictureBox1.Image = this.fouri;
                //WindowUtility.SetScreen(this.pictureBox1, Hercules.Properties.Resources.NUMBER_FOUR);
                //WindowUtility.SetScreen(this.pictureBox2, Hercules.Properties.Resources.NUMBER_FOUR);
            }
            else
            {
                this.labell4.Visible = true;
                //this.labelr4.Visible = true;
            }
            this.Invalidate();
            this.Refresh();
            Thread.Sleep(520); 
            //SoundUtility.PlaySync(Hercules.Properties.SoundResources.FOUR_COUNT);
            //four.PlaySync();

            if (use_images)
            {
                this.pictureBox1.Image = this.threei;
                //WindowUtility.SetScreen(this.pictureBox1, Hercules.Properties.Resources.NUMBER_THREE);
                //WindowUtility.SetScreen(this.pictureBox2, Hercules.Properties.Resources.NUMBER_THREE);
            }
            else
            {
                this.labell4.Visible = false;
                //this.labelr4.Visible = false;
                this.labell3.Visible = true;
                //this.labelr3.Visible = false;
            }
            this.Invalidate();
            this.Refresh();
            Thread.Sleep(520);
            //three.PlaySync();
            //Thread.Sleep(10);
            //SoundUtility.PlaySync(Hercules.Properties.SoundResources.THREE_COUNT);
            //Thread.Sleep(750);

            if (use_images)
            {
                this.pictureBox1.Image = this.twoi;
                //WindowUtility.SetScreen(this.pictureBox1, Hercules.Properties.Resources.NUMBER_TWO);
                //WindowUtility.SetScreen(this.pictureBox2, Hercules.Properties.Resources.NUMBER_TWO);
            }
            else
            {
                this.labell3.Visible = false;
                //this.labelr3.Visible = false;
                this.labell2.Visible = true;
                //this.labelr2.Visible = false;
            }
            this.Invalidate();
            this.Refresh();
            //Thread.Sleep(10);
            Thread.Sleep(520);
            //two.PlaySync();
            //SoundUtility.PlaySync(Hercules.Properties.SoundResources.TWO_COUNT);
            //Thread.Sleep(750);

            if (use_images)
            {
                this.pictureBox1.Image = this.onei;
                //WindowUtility.SetScreen(this.pictureBox1, Hercules.Properties.Resources.NUMBER_ONE);
                //WindowUtility.SetScreen(this.pictureBox2, Hercules.Properties.Resources.NUMBER_ONE);
            }
            else
            {
                this.labell2.Visible = false;
                //this.labelr2.Visible = false;
                this.labell1.Visible = true;
                //this.labelr1.Visible = true;
            }
            this.Invalidate();
            this.Refresh();
            //Thread.Sleep(10);
            Thread.Sleep(520);
            //SoundUtility.PlaySync(Hercules.Properties.SoundResources.ONE_COUNT);
            //one.PlaySync();
            //Thread.Sleep(750);

            this.pictureBox1.Visible = false;
            this.pictureBox2.Visible = false;
            this.Refresh();

            this.labell1.Visible = false;
            //this.labelr1.Visible = false;
        }

        private void ProcessPicThread(object _i)
        {
            int i = (int)_i;
            if (i < ConfigUtility.PhotoCount - 1 && ConfigUtility.CameraEnabled)
                Thread.Sleep(Convert.ToInt32(ConfigUtility.GetValue("CameraWait")));

            Image image = null;

            if (ConfigUtility.PhotoBackgrounds > 0)
            {
                //this.preview.Image = ChromakeyUtility.CreateComposite(
                image = ChromakeyUtility.CreateComposite(
                    FileUtility.LoadBitmap(this.currentSession.PhotoPath + "\\forephoto" + (i + 1) + ".jpg"),
                    ConfigUtility.Backgrounds[this.currentSession.SelectedBackgrounds[i] - 1]);

                //gw
                if (this.currentSession.SelectedColorType == ColorType.Sepia)
                {
                    //Bitmap btm = new Bitmap(this.preview.Image);
                    Bitmap btm = new Bitmap(image);
                    SepiaUtility.SepiaBitmap(btm);
                    image.Dispose();
                    image = null;
                    image = btm;
                    //this.preview.Image.Dispose();
                    //this.preview.Image = null;
                    //this.preview.Image = btm;
                }
                //gw

                //this.preview.Image.Save(this.currentSession.PhotoPath + "\\photo" + (i + 1) + ".jpg",
                image.Save(this.currentSession.PhotoPath + "\\photo" + (i + 1) + ".jpg",
                    System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            //gw
            if (ConfigUtility.CameraEnabled)
            //gw
            {
                //this.preview.Image.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
            }

            if (this.currentSession.SelectedColorType == ColorType.BW)
            {
                //this.preview.Image = FileUtility.MakeGrayscale((System.Drawing.Bitmap)this.preview.Image);
                image = FileUtility.MakeGrayscale((System.Drawing.Bitmap)image);
            }

            
            this.proc_image = image;

            this.PicProcessed = true;
        }

        private void GetSelectedVideoAndAudioDevices(String camname, out EncoderDevice video)
        {
            video = null;


            // Get the selected video device            
            foreach (EncoderDevice edv in EncoderDevices.FindDevices(EncoderDeviceType.Video))
            {
                if (String.Compare(edv.Name, camname) == 0)
                {
                    video = edv;
                    break;
                }
            }
        }

        private void StopRecording(int i)
        {
            // Is it Recoring ?
            if (_bStartedRecording[i])
            {
                // Yes
                // Stops encoding
                _job[i].StopEncoding();
                //btnStartStopRecording.Text = "Start Recording";
                //toolStripStatusLabel1.Text = "";
                _bStartedRecording[i] = false;
            }
            else
            {
                // Sets up publishing format for file archival type
                FileArchivePublishFormat fileOut = new FileArchivePublishFormat();

                // Sets file path and name
                fileOut.OutputFileName = String.Format("C:\\WebCam{0:yyyyMMdd_hhmmss}.wmv", DateTime.Now);

                // Adds the format to the job. You can add additional formats as well such as
                // Publishing streams or broadcasting from a port
                _job[i].PublishFormats.Add(fileOut);

                // Start encoding
                _job[i].StartEncoding();

                //btnStartStopRecording.Text = "Stop Recording";
                //toolStripStatusLabel1.Text = fileOut.OutputFileName;
                _bStartedRecording[i] = true;
            }
        }


        void StopJob(int i)
        {
            // Has the Job already been created ?
            if (_job[i] != null)
            {
                // Yes
                // Is it capturing ?
                //if (_job.IsCapturing)
                if (_bStartedRecording[i])
                {
                    // Yes
                    // Stop Capturing
                    StopRecording(i);
                }

                _job[i].StopEncoding();

                // Remove the Device Source and destroy the job
                _job[i].RemoveDeviceSource(_deviceSource[i]);

                // Destroy the device source
                _deviceSource[i].PreviewWindow = null;
                _deviceSource[i] = null;
            }
        }
        private void startpreview(int i)
        {
            EncoderDevice video = null;
            EncoderDevice audio = null;

            String cam = camname[i];

            GetSelectedVideoAndAudioDevices(cam, out video);
            StopJob(i);

            if (video == null)
            {
                return;
            }

            // Starts new job for preview window
            _job[i] = new LiveJob();

            // Checks for a/v devices
            //if (video != null && audio != null)
                if (video != null )
            {
                // Create a new device source. We use the first audio and video devices on the system
                _deviceSource[i] = _job[i].AddDeviceSource(video, audio);

                {
                    // No
                    // Setup the video resolution and frame rate of the video device
                    // NOTE: Of course, the resolution and frame rate you specify must be supported by the device!
                    // NOTE2: May be not all video devices support this call, and so it just doesn't work, as if you don't call it (no error is raised)
                    // NOTE3: As a workaround, if the .PickBestVideoFormat method doesn't work, you could force the resolution in the 
                    //        following instructions (called few lines belows): 'panelVideoPreview.Size=' and '_job.OutputFormat.VideoProfile.Size=' 
                    //        to be the one you choosed (640, 480).
                    _deviceSource[i].PickBestVideoFormat(new Size(640, 480), 15);
                }

                // Get the properties of the device video
                SourceProperties sp = _deviceSource[i].SourcePropertiesSnapshot();

                // Resize the preview panel to match the video device resolution set
                //this.panelVideoOne.Size = new Size(sp.Size.Width, sp.Size.Height);

                // Setup the output video resolution file as the preview
                _job[i].OutputFormat.VideoProfile.Size = new Size(sp.Size.Width, sp.Size.Height);

                // Display the video device properties set
                //toolStripStatusLabel1.Text = sp.Size.Width.ToString() + "x" + sp.Size.Height.ToString() + "  " + sp.FrameRate.ToString() + " fps";

                // Sets preview window to winform panel hosted by xaml window
                this.vidPanel[i].Visible = true;
                _deviceSource[i].PreviewWindow = new PreviewWindow(new HandleRef( this.vidPanel[i], this.vidPanel[i].Handle));


                // Make this source the active one
                _job[i].ActivateSource(_deviceSource[i]);

                //btnStartStopRecording.Enabled = true;
                //btnGrabImage.Enabled = true;

                //toolStripStatusLabel1.Text = "Preview activated";
            }
            else
            {
                // Gives error message as no audio and/or video devices found
                MessageBox.Show("No Video/Audio capture devices have been found.", "Warning");
                //toolStripStatusLabel1.Text = "No Video/Audio capture devices have been found.";
            }
        }

        private int SetupVideo()
        {
            int i = 0;
            int numcams = 0;
            
            foreach (EncoderDevice edv in EncoderDevices.FindDevices(EncoderDeviceType.Video))
            {
                numcams++;
                if (i == 0)
                {
                    this.camname[0] = edv.Name;
                    //break;
                }
                else if (i == 1)
                {
                    this.camname[2] = edv.Name;
                    break;
                }
            }

            if (numcams == 1)
            {
                this.panelVideoOne.Visible = true;
                this.panelVideoTwo.Visible = false;
            }
            else if (numcams == 1)
            {
                this.panelVideoOne.Visible = true;
                this.panelVideoTwo.Visible = true;
            }

            return numcams;
        }

        private void GrabImage(int i)
        {
            // Create a Bitmap of the same dimension of panelVideoPreview (Width x Height)
            Panel pnl = this.vidPanel[i];
            using (Bitmap bitmap = new Bitmap(pnl.Width, pnl.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    // Get the paramters to call g.CopyFromScreen and get the image
                    Rectangle rectanglePanelVideoPreview = pnl.Bounds;
                    Point sourcePoints = pnl.PointToScreen(new Point(pnl.ClientRectangle.X, pnl.ClientRectangle.Y));
                    g.CopyFromScreen(sourcePoints, Point.Empty, rectanglePanelVideoPreview.Size);
                }

                string strGrabFileName = this.currentSession.PhotoPath + "\\" +
                            "photo" + (1) + ".jpg";
                    //String.Format("C:\\Snapshot_{0:yyyyMMdd_hhmmss}.jpg", DateTime.Now);
                //toolStripStatusLabel1.Text = strGrabFileName;
                
                bitmap.Save(strGrabFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void DoWebCam_Loaded()
        {

            Directory.CreateDirectory(this.currentSession.PhotoPath);

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;

            WindowUtility.SetScreen(pb, Hercules.Properties.Resources.TAKEPHOTO_SCREEN);
            info.ForeColor = System.Drawing.Color.Black;


            if (istable)
            {
                int numcams = SetupVideo();
                if ((numcams == 0) || (numcams > 2))
                {
                    this.DialogResult = DialogResult.No;
                    return;
                }

                if (numcams == 1)
                {
                    this.startpreview(0);
                }
                else if (numcams == 2)
                {
                    this.startpreview(0);
                    this.startpreview(1);
                }
            }

            this.Refresh();
            Application.DoEvents();
            System.Threading.Thread.Sleep(1);

            System.Threading.Thread.Sleep(1000);
            SoundUtility.PlaySync(Hercules.Properties.SoundResources.GET_READY);

            System.Threading.Thread.Sleep(1000);

            SoundUtility.Play(Hercules.Properties.SoundResources.COUNTDOWN);
            System.Threading.Thread.Sleep(4000);


            SoundUtility.PlaySync(Hercules.Properties.SoundResources.CAMERA_CLICK);

            this.GrabImage(0);

            this.preview.Image = FileUtility.LoadBitmap(this.currentSession.PhotoPath + "\\photo" + (1) + ".jpg");
            this.preview.Visible = true;
            this.vidPanel[0].Visible = false;
            this.vidPanel[1].Visible = false;
            this.Refresh();
            Application.DoEvents();


            //this.StopRecording(0);
            //this.StopRecording(1);
            this.StopJob(0);
            this.StopJob(1);

            System.Threading.Thread.Sleep(2000);

            DevelopPhotos();

            if (preview.Image != null)
                preview.Image.Dispose();

            if (pb.Image != null)
                pb.Image.Dispose();


            DialogResult = System.Windows.Forms.DialogResult.OK;
        }


        private void TakePhotos_Load(object sender, EventArgs e)
        {
            if (istable)
            {
                this.DoWebCam_Loaded();
                return;
            }

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;

            //gw
            bool show_pose = ConfigUtility.GetValue("SHOW_POSE_THUMBNAILS").Equals("1");
            //gw

            //gw
            if ( show_pose && (ConfigUtility.PhotoBackgrounds > 0) )
            {
                this.LoadPoses();
            }
            //gw


            WindowUtility.SetScreen(pb, Hercules.Properties.Resources.TAKEPHOTO_SCREEN);
            info.ForeColor = System.Drawing.Color.Black;


            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "TEXT_COLOR")))
                info.ForeColor = ColorTranslator.FromHtml(ConfigUtility.GetConfig(ConfigUtility.Config, "TEXT_COLOR"));

            info.Parent = pb;

            info.Text = "Align your eyes with the camera.  Get Ready!!";

            
            //gw
            this.labell1.Parent = pb;
            this.labell1.ForeColor = info.ForeColor;
            this.labell2.Parent = pb;
            this.labell2.ForeColor = info.ForeColor;
            this.labell3.Parent = pb;
            this.labell3.ForeColor = info.ForeColor;
            this.labell4.Parent = pb;
            this.labell4.ForeColor = info.ForeColor;
            this.labelr1.Parent = pb;
            this.labelr1.ForeColor = info.ForeColor;
            this.labelr2.Parent = pb;
            this.labelr2.ForeColor = info.ForeColor;
            this.labelr3.Parent = pb;
            this.labelr3.ForeColor = info.ForeColor;
            this.labelr4.Parent = pb;
            this.labelr4.ForeColor = info.ForeColor;
            //gw
            this.Refresh();            

            Thread.Sleep(800);

            SoundUtility.PlaySync(Hercules.Properties.SoundResources.GET_READY);

            this.Refresh();

            //gw - this was here...
            Thread.Sleep(800);
            //gw

            // Create new directory
            Directory.CreateDirectory(this.currentSession.PhotoPath);

            info.Text = string.Format(Hercules.Properties.Resources.SMILE_TAKING_PHOTO_TEXT, WindowUtility.FriendlyOrder(1));
            this.Refresh();

            //gw
            if (show_pose)
            {
                // highlight next...
                string ctl_name = "pictureBox" + (3).ToString();
                Control[] ctl = this.Controls.Find(ctl_name, true);
                if (ctl.Length == 1)
                {
                    PictureBox posepb = (PictureBox)ctl[0];
                    posepb.Visible = true;
                    posepb.Width = 145;
                    posepb.Height = 121;
                    posepb.Location = new Point(posepb.Location.X - 3, posepb.Location.Y - 3);
                    posepb.BackColor = System.Drawing.Color.Red;
                    posepb.Padding = new Padding(3, 3, 3, 3);
                    this.Refresh();
                }
            }
            //gw

            //gw
            //  show countdown...
            
            bool use_images = false;
            if ( ConfigUtility.GetValue("SHOW_COUNTDOWN").Equals("1") )
            {
                this.show_countdown = true;
                use_images = true;
            }
            if (ConfigUtility.GetValue("SHOW_COUNTDOWN").Equals("2"))
            {
                this.show_countdown = true;
            }
            //gw
                
            //gw
            if (!this.show_countdown)
            {
             
                SoundUtility.Play(Hercules.Properties.SoundResources.COUNTDOWN);
                Thread.Sleep(Convert.ToInt32(ConfigUtility.GetValue("CountdownTime")));
            }
            // gw


            prompts = new Thread(PromptThread);
            prompts.Start();

            //gw
            if (this.show_countdown)
            {
                ClearToStart = true;
                ShowCountdown(use_images, 0);
            }
            //gw

            // cycle through picture count
            for (int i = 0; i < ConfigUtility.PhotoCount; i++)
            {
                current = (i + 1);


                if (ConfigUtility.CameraEnabled)
                {
                    if (CameraUtility.camera != null && !string.IsNullOrEmpty(CameraUtility.camera.ConnectedCameraName))
                        CameraUtility.camera.Release(this.currentSession.PhotoPath + "\\" +
                            "forephoto" + (i + 1) + ".jpg");
                }
                else
                {
                    // Simulate a photo being taken
                    SoundUtility.PlaySync(Hercules.Properties.SoundResources.CAMERA_CLICK);

                    File.Copy(ConfigUtility.GetValue("testphoto"), this.currentSession.PhotoPath + "\\" +
                         "forephoto" + (i + 1).ToString() + ".jpg");

                }

               
                if (i < ConfigUtility.PhotoCount - 1)
                {
                    info.Text = string.Format(Hercules.Properties.Resources.SMILE_TAKING_PHOTO_TEXT, WindowUtility.FriendlyOrder(current + 1));
                    this.Refresh();
                }

                //  THREADED...

                if (this.show_countdown) // launch image processing in different thread...
                {
                    this.PicProcessed = false;
                    proc_thread = new Thread(ProcessPicThread);
                    proc_thread.Priority = ThreadPriority.Lowest;
                    proc_thread.Start(i);

                    while (true)  // spin wait until image processing is done...
                    {
                        if (this.PicProcessed == true)
                        {

                            this.preview.Image = this.proc_image;
                            if (ConfigUtility.CameraEnabled)               
                            {
                                this.preview.Image.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                            }
                            this.preview.Visible = true;
                            this.preview.BringToFront();
                            this.Invalidate();
                            this.Refresh();
                            Thread.Sleep(1);

                            break;
                        }
                        else
                        {
                            Thread.Sleep(10);
                        }
                    } // end while...
                }
                else //  do image processing here...
                {

                    if (i < ConfigUtility.PhotoCount - 1 && ConfigUtility.CameraEnabled)
                        Thread.Sleep(Convert.ToInt32(ConfigUtility.GetValue("CameraWait")));

                    if (ConfigUtility.PhotoBackgrounds > 0)
                    {
                        this.preview.Image = ChromakeyUtility.CreateComposite(
                            FileUtility.LoadBitmap(this.currentSession.PhotoPath + "\\forephoto" + (i + 1) + ".jpg"),
                            ConfigUtility.Backgrounds[this.currentSession.SelectedBackgrounds[i] - 1]);

                        //gw
                        if (this.currentSession.SelectedColorType == ColorType.Sepia)
                        {
                            Bitmap btm = new Bitmap(this.preview.Image);
                            SepiaUtility.SepiaBitmap(btm);
                            this.preview.Image.Dispose();
                            this.preview.Image = null;
                            this.preview.Image = btm;
                        }
                        //gw

                        this.preview.Image.Save(this.currentSession.PhotoPath + "\\photo" + (i + 1) + ".jpg",
                            System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                    //gw
                    if (ConfigUtility.CameraEnabled)
                    //gw
                    {
                        this.preview.Image.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                    }

                    if (this.currentSession.SelectedColorType == ColorType.BW)
                        this.preview.Image = FileUtility.MakeGrayscale((System.Drawing.Bitmap)this.preview.Image);


                }
                //THREADED

                this.ClearToStart = true;

                if (this.show_countdown)
                {
                    if (i < ConfigUtility.PhotoCount - 1)
                    {
                        ShowCountdown(use_images, i + 1);
                    }
                }
                else
                {
                    /*
                    if (i < ConfigUtility.PhotoCount - 1)
                    {
                        SoundUtility.PlaySync(Hercules.Properties.SoundResources.COUNTDOWN);
                    }
                     * */
                }

                //gw
                if (show_pose)
                {
                    // remove this one...
                    string ctl_name = "pictureBox" + (i + 3).ToString();
                    Control[] ctl = this.Controls.Find(ctl_name, true);
                    if (ctl.Length == 1)
                    {
                        PictureBox posepb = (PictureBox)ctl[0];
                        posepb.Visible = false;
                    }

                    // highlight next...
                    ctl_name = "pictureBox" + (i + 1 + 3).ToString();
                    ctl = this.Controls.Find(ctl_name, true);
                    if (ctl.Length == 1)
                    {
                        PictureBox posepb = (PictureBox)ctl[0];
                        posepb.Visible = true;
                        posepb.Width = 145;
                        posepb.Height = 121;
                        posepb.Location = new Point(posepb.Location.X - 3, posepb.Location.Y - 3);
                        posepb.BackColor = System.Drawing.Color.Red;
                        posepb.Padding = new Padding(3, 3, 3, 3);
                    }
                }
                //gw

                this.preview.Visible = true;
                this.Refresh();

            }

            if (preview.Image != null)
                preview.Image.Dispose();

            if (pb.Image != null)
                pb.Image.Dispose();

            
            {
                DevelopPhotos();
                PrintPhotos();
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }



        private void DevelopPhotos()
        {
            int x = 0;
            int y = 0;


            // six layout
            int thumbwidth = 0;
            int thumbheight = 0;
            int rotate = 0;

            // load the template

            Bitmap template = new Bitmap("Skins\\" + ConfigUtility.Skin + "\\Templates\\" + ConfigUtility.PhotoCount + "photo.jpg");

            using (Graphics grfx = Graphics.FromImage(template))
            {
                int pcount = ConfigUtility.PhotoCount;
                if (istable) pcount = 1;

                for (int i = 0; i <= pcount - 1; i++)
                {
                    x = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, string.Format("Photo{0}_X", (i + 1).ToString())));
                    y = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, string.Format("Photo{0}_Y", (i + 1).ToString())));

                    thumbwidth = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, string.Format("Photo{0}_W", (i + 1).ToString())));
                    thumbheight = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, string.Format("Photo{0}_H", (i + 1).ToString())));

                    String path = this.currentSession.PhotoPath + "\\photo" + (i + 1).ToString() + ".jpg";
                    Bitmap photo = new Bitmap(path);

                    // initial camera flip
                    photo.RotateFlip(RotateFlipType.Rotate90FlipNone);

                    rotate = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, string.Format("Photo{0}_Rotate", (i + 1).ToString())));
                    
                    if (rotate == 90)
                        photo.RotateFlip(RotateFlipType.Rotate90FlipNone);

                    if (rotate >= 180)
                        photo.RotateFlip(RotateFlipType.Rotate90FlipNone);

                    if (rotate >= 270)
                        photo.RotateFlip(RotateFlipType.Rotate90FlipNone);

                    System.Drawing.Image mini = photo.GetThumbnailImage(thumbwidth, thumbheight, null, IntPtr.Zero);

                    // Clear handle to original file so that we can overwrite it if necessary
                    photo.Dispose();

                    if (this.currentSession.SelectedColorType == ColorType.BW)
                        grfx.DrawImage(FileUtility.MakeGrayscale((Bitmap)mini), x, y, mini.Width, mini.Height);
                    else
                        grfx.DrawImage(mini, x, y, mini.Width, mini.Height);

                    mini.Dispose();

                }
            }

            template.Save(this.currentSession.PhotoPath + "\\print.jpg", ImageFormat.Jpeg);
            template.Dispose();

            if (this.currentSession.SelectedColorType == ColorType.BW)
                ConfigUtility.IncrementCounter("BW");
            //gw
            else if (this.currentSession.SelectedColorType == ColorType.Color)
                ConfigUtility.IncrementCounter("Color");
            else 
                ConfigUtility.IncrementCounter("Sepia");
            //gw
        }



        private void PrintPhotos()
        {
            if (!ConfigUtility.GetValue("PrinterEnabled").Equals("1"))
                return;

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintController = new System.Drawing.Printing.StandardPrintController();
            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            printDoc.DefaultPageSettings.Landscape = false;

            if (ConfigUtility.MaxCopies > 1)
            {
                printDoc.PrinterSettings.Copies = (short)ConfigUtility.MaxCopies;
            }

    
            printDoc.Print();
        }

        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Image photo = Image.FromFile(this.currentSession.PhotoPath + "\\print.jpg");
            e.Graphics.DrawImage(photo, 0, 0, 400, 600);
            photo.Dispose();
        }







        private void TakePhotos_FormClosed(object sender, FormClosedEventArgs e)
        {
            SoundUtility.Stop();
            SoundUtility.StopSpeaking();

            if (pb.Image != null)
                pb.Image.Dispose();

            if (thread != null)
                thread.Abort();

            if (prompts != null)
                prompts.Abort();

            prompts = null;
            thread = null;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
