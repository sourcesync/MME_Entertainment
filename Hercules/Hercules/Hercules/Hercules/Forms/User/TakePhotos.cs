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
        private System.Threading.TimerCallback flip_cb;
        private System.Threading.Timer flip_timer;
        private int static_orientation = 0;
        private int global_orientation = 0;
        private int mode = 0;
        private MME.Hercules.WPFForms.OffScreenRender offscreen = null;
        private int num_cams = 0;
        private CustomControl.OrientAbleTextControls.OrientedTextLabel infof;
        private CustomControl.OrientAbleTextControls.OrientedTextLabel labelLikef;
        private CustomControl.OrientAbleTextControls.OrientedTextLabel labelAgainf;
        private CustomControl.OrientAbleTextControls.OrientedTextLabel labell1f;
        private CustomControl.OrientAbleTextControls.OrientedTextLabel labell2f;
        private CustomControl.OrientAbleTextControls.OrientedTextLabel labell3f;
        private CustomControl.OrientAbleTextControls.OrientedTextLabel labell4f;
        private CustomControl.OrientAbleTextControls.OrientedTextLabel labelr1f;
        private CustomControl.OrientAbleTextControls.OrientedTextLabel labelr2f;
        private CustomControl.OrientAbleTextControls.OrientedTextLabel labelr3f;
        private CustomControl.OrientAbleTextControls.OrientedTextLabel labelr4f;
        //gw

        //public void Flip(object o, System.Timers.ElapsedEventArgs a)
        public void Flip(object o)
        {
            this.Invoke(new System.EventHandler(this._Flip));
        }

        public void _Flip(object o, System.EventArgs a)
        //public void _Flip(object o, 
        {
            if (this.offscreen != null)
            {
                this.offscreen.Refresh();

                using (Bitmap bitmap = new Bitmap(this.offscreen.pnl.memGraphics.memoryBitmap))
                {
                    if (this.pictureBoxFlip.Image != null)
                    {
                        this.pictureBoxFlip.Image.Dispose();
                    }
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
                    this.pictureBoxFlip.Image = bitmap;
                    this.pictureBoxFlip.Refresh();
                }
            }
            else
            {

                // Create a Bitmap of the same dimension of panelVideoPreview (Width x Height)
                Panel pnl = null;
                if (this.offscreen != null)
                    pnl = this.offscreen.pnl;
                else
                    pnl = this.vidPanel[0];

                using (Bitmap bitmap = new Bitmap(pnl.Width, pnl.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        // Get the paramters to call g.CopyFromScreen and get the image
                        Rectangle rectanglePanelVideoPreview = pnl.Bounds;
                        Point sourcePoints = pnl.PointToScreen(new Point(pnl.ClientRectangle.X, pnl.ClientRectangle.Y));
                        g.CopyFromScreen(sourcePoints, Point.Empty, rectanglePanelVideoPreview.Size);
                    }

                    if (this.pictureBoxFlip.Image != null)
                    {
                        this.pictureBoxFlip.Image.Dispose();
                    }
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
                    this.pictureBoxFlip.Image = bitmap;
                    this.pictureBoxFlip.Refresh();
                }
            }
        }

        public TakePhotos(Session currentSession)
        {
            InitializeComponent();


            this.vidPanel[0] = this.panelVideoOne;
            this.vidPanel[1] = this.panelVideoOne;

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

            if (istable)
            {
                this.MakeFlipLabels();
                info.Size = new Size(850, 45);
            }
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
                        //Thread.Sleep(countdownwait);
                        //SoundUtility.Play(Hercules.Properties.SoundResources.COUNTDOWN);
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

        private void ShowCountdown(bool use_images, int which, bool right)
        {
            /*
            int count = 0;
            while (true)
            {
                count += 1;
                System.Threading.Thread.Sleep(5);
                Application.DoEvents();
                if (count > ((1000.0 / 5) * 4))
                    return;
            }
             * */

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
                if (this.static_orientation == 0)
                {
                    this.labell4.Visible = true;
                    if (right)
                        this.labelr4.Visible = true;
                }
                else
                {
                    this.labell4f.Visible = true;
                    if (right) this.labelr4f.Visible = true;
                }
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
                if (this.static_orientation == 0)
                {
                    this.labell4.Visible = false;
                    if (right) this.labelr4.Visible = false;
                    this.labell3.Visible = true;
                    if (right) this.labelr3.Visible = true;
                }
                else
                {
                    this.labell4f.Visible = false;
                    if (right) this.labelr4f.Visible = false;
                    this.labell3f.Visible = true;
                    if (right) this.labelr3f.Visible = true;
                }
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
                if (this.static_orientation == 0)
                {
                    this.labell3.Visible = false;
                    if (right) this.labelr3.Visible = false;
                    this.labell2.Visible = true;
                    if (right) this.labelr2.Visible = true;
                }
                else
                {
                    this.labell3f.Visible = false;
                    if (right) this.labelr3f.Visible = false;
                    this.labell2f.Visible = true;
                    if (right) this.labelr2f.Visible = true;
                }
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
                if (this.static_orientation==0)
                {
                    this.labell2.Visible = false;
                    if (right) this.labelr2.Visible = false;
                    this.labell1.Visible = true;
                    if (right) this.labelr1.Visible = true;
                }
                else
                {
                    this.labell2f.Visible = false;
                    if (right) this.labelr2f.Visible = false;
                    this.labell1f.Visible = true;
                    if (right) this.labelr1f.Visible = true;
                }
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

            if (this.static_orientation == 0)
            {
                this.labell1.Visible = false;
                if (right) this.labelr1.Visible = false;
            }
            else
            {
                this.labell1f.Visible = false;
                if (right) this.labelr1f.Visible = false;
            }
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
            if (this.flip_timer != null)
            {
                this.flip_timer.Dispose();
                this.flip_timer = null;
            }

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

                //gw
                _job[i] = null;
                //gw
            }
        }
        private void startpreview(int i)
        {
            StopJob(i);

            EncoderDevice video = null;
            EncoderDevice audio = null;

            String cam = camname[i];

            GetSelectedVideoAndAudioDevices(cam, out video);

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
                
                //_job[i].OutputFormat.VideoProfile.Size = new Size(sp.Size.Width, sp.Size.Height);

                _job[i].Status += new EventHandler<EncodeStatusEventArgs>(TakePhotos_Status);

                // Display the video device properties set
                //toolStripStatusLabel1.Text = sp.Size.Width.ToString() + "x" + sp.Size.Height.ToString() + "  " + sp.FrameRate.ToString() + " fps";

                //gw - match the aspect of the requested vid size...
                this.vidPanel[i].Size = new Size(this.vidPanel[i].Size.Width, (int)(this.vidPanel[i].Size.Width * 480.0 / 640));
                
                // Sets preview window to winform panel hosted by xaml window
                this.vidPanel[i].Visible = true;

                if (this.offscreen != null)
                {
                    _deviceSource[i].PreviewWindow = new PreviewWindow(new HandleRef(this.offscreen.pnl, this.offscreen.pnl.Handle));
                    //_deviceSource[i].PreviewWindow = new PreviewWindow(new HandleRef(this.offscreen, this.offscreen.Handle));
                }
                else
                {
                    _deviceSource[i].PreviewWindow = new PreviewWindow(new HandleRef(this.vidPanel[i], this.vidPanel[i].Handle));
                }

                // Make this source the active one
                _job[i].ActivateSource(_deviceSource[i]);

                //btnStartStopRecording.Enabled = true;
                //btnGrabImage.Enabled = true;

                //toolStripStatusLabel1.Text = "Preview activated";

                //flip
                if (this.offscreen!=null)
                {
                    this.flip_cb = new System.Threading.TimerCallback(this.Flip);
                    this.flip_timer = new System.Threading.Timer(this.flip_cb, null, TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(100));
                    this.pictureBoxFlip.Visible = true;
                }
            }
            else
            {
                // Gives error message as no audio and/or video devices found
                MessageBox.Show("No Video/Audio capture devices have been found.", "Warning");
                //toolStripStatusLabel1.Text = "No Video/Audio capture devices have been found.";
            }
        }

        void TakePhotos_Status(object sender, EncodeStatusEventArgs e)
        {
            System.Console.WriteLine("yo");
        }

        private int SetupVideo()
        {
            int numcams = 0;
            
            foreach (EncoderDevice edv in EncoderDevices.FindDevices(EncoderDeviceType.Video))
            {
                numcams++;
                if (numcams == 1)
                {
                    this.camname[0] = edv.Name;
                    if (ConfigUtility.IsDeveloperMode)
                    {
                        System.Windows.Forms.MessageBox.Show("cam1=" + edv.Name);
                    }
                }
                else if (numcams==2)
                {
                    this.camname[1] = edv.Name;
                    if (ConfigUtility.IsDeveloperMode)
                    {
                        System.Windows.Forms.MessageBox.Show("cam2=" + edv.Name);
                    }
                    break;
                }
            }

            if ( numcams==2)
            {
                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "SwapWebCams")))
                {
                    String tmpname = this.camname[0];
                    this.camname[0] = this.camname[1];
                    this.camname[1] = tmpname;
                }
            }


            if (numcams == 1)
            {
                this.panelVideoOne.Visible = true;
                //this.panelVideoOne.Visible = false;
            }
            else if (numcams == 2)
            {
                this.panelVideoOne.Visible = true;
                //this.panelVideoTwo.Visible = true;
            }

            return numcams;
        }

        private void GrabImage(int i)
        {
            if (this.offscreen != null)
            {
                using (Bitmap bitmap = this.offscreen.pnl.memGraphics.memoryBitmap)
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        // Get the paramters to call g.CopyFromScreen and get the image
                        //Rectangle rectanglePanelVideoPreview = pnl.Bounds;
                        //Point sourcePoints = pnl.PointToScreen(new Point(pnl.ClientRectangle.X, pnl.ClientRectangle.Y));
                        //g.CopyFromScreen(sourcePoints, Point.Empty, rectanglePanelVideoPreview.Size);
                    }

                    string strGrabFileName = this.currentSession.PhotoPath + "\\" +
                            "photo" + (1) + ".jpg";
                    //String.Format("C:\\Snapshot_{0:yyyyMMdd_hhmmss}.jpg", DateTime.Now);
                    //toolStripStatusLabel1.Text = strGrabFileName;


                    bitmap.Save(strGrabFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

            }
            else
            {

                // Create a Bitmap of the same dimension of panelVideoPreview (Width x Height)
                Panel pnl = null;
                if (this.offscreen != null)
                    pnl = this.offscreen.pnl;
                else
                    pnl = this.vidPanel[i];

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
        }

        private void Orient(int mode)
        {
            //bg...
            if (this.static_orientation == 0)
            {
                WindowUtility.SetScreen(pb, Hercules.Properties.Resources.TAKEPHOTO_SCREEN);
            }
            else
            {
                WindowUtility.SetScreen(pb, Hercules.Properties.Resources.TAKEPHOTO_FLIPPED_SCREEN);
            }

            //backbutton
            if (static_orientation == 0)
            {
                this.pictureBoxBack.Location = new Point(4, 719);
            }
            else
            {
                this.pictureBoxBack.Location = new Point(1024 - (4 + this.pictureBoxBack.Size.Width), 
                    768 - (719 + this.pictureBoxBack.Size.Height));
            }

            //info
            if (static_orientation == 0)
            {
                infof.Visible = false;
                info.Visible = true;
                if (mode==0)
                    info.Location = new Point(100, 259);
                else
                    info.Location = new Point(100, 259);
            }
            else
            {
                infof.Visible = true;
                info.Visible = false;
                if (mode==0)
                    infof.Location = new Point(1024 - (100 + infof.Size.Width), 768 - (259 + infof.Size.Height));
                else
                    infof.Location = new Point(1024-(100+infof.Size.Width), //TODO: FUDGE FACTOR
                        768-(259+infof.Size.Height));
            }

            //numbers
            //this.labell1.Visible = true;
            //this.labelr2.Visible = true;
            if (static_orientation == 0)
            {
                int x = 118;
                int y = 316;
                int offset = 40;
                this.labell1.Location = new Point(x - offset, y);
                this.labell2.Location = new Point(x - offset, y);
                this.labell3.Location = new Point(x - offset, y);
                this.labell4.Location = new Point(x - offset, y);

                x = 678;
                y = 309;
                this.labelr1.Location = new Point(x + offset, y);
                this.labelr2.Location = new Point(x + offset, y);
                this.labelr3.Location = new Point(x + offset, y);
                this.labelr4.Location = new Point(x + offset, y);
            }
            else
            {
                int x = 118;
                int y = 316;
                int offset = 40;
                int xx = x + offset - 70; // TODO: WEIRD FUDGE FACTOR!
                int xf = 1024 - (xx + this.labell1f.Size.Width);
                int yf = 768 - (y + this.labell1f.Size.Height);
                this.labell1f.Location = new Point(xf, yf);
                this.labell2f.Location = new Point(xf, yf);
                this.labell3f.Location = new Point(xf, yf);
                this.labell4f.Location = new Point(xf, yf);

                x = 678;
                y = 309;
                xx = x + offset;
                xf = 1024 - (xx + this.labelr1.Size.Width);
                yf = 768 - (y + this.labelr1.Size.Height);
                this.labelr1f.Location = new Point(xf, yf);
                this.labelr2f.Location = new Point(xf, yf);
                this.labelr3f.Location = new Point(xf, yf);
                this.labelr4f.Location = new Point(xf, yf);
            }

            //vidpanels...
            if (static_orientation == 0)
            { 
                this.vidPanel[0].Location = new Point(330, 321);
                this.vidPanel[1].Location = new Point(330, 321);
                //this.vidPanel[0].Location = new Point(330+600, 321);
                //this.vidPanel[1].Location = new Point(330+600, 321);
            }
            else
            {
                this.vidPanel[0].Location = new Point(1024 - (330 + this.vidPanel[0].Size.Width),
                    768 - (321 + this.vidPanel[0].Size.Height));
                this.vidPanel[1].Location = new Point(1024 - (330 + this.vidPanel[1].Size.Width),
                    768 - (321 + this.vidPanel[0].Size.Height));
             
            }


            //  Match the snapshot preview to video preview...
            this.preview.Location = this.vidPanel[0].Location;
            this.preview.Size = this.vidPanel[0].Size;

            // prompt...
            if (static_orientation == 0)
            {
                this.pictureBoxLike.Location = new Point(94, 598);
                this.pictureBoxAgain.Location = new Point(479, 598);
                this.labelLike.Location = new Point(169, 610);
                this.labelAgain.Location = new Point(550, 610);
                if (mode == 0)
                {
                    this.pictureBoxLike.Visible = false;
                    this.pictureBoxAgain.Visible = false;
                    this.labelLike.Visible = false;
                    this.labelLikef.Visible = false;
                    this.labelAgain.Visible = false;
                    this.labelAgainf.Visible = false;
                }
                else
                {
                    this.pictureBoxLike.Visible = true;
                    this.pictureBoxAgain.Visible = true;
                    this.pictureBoxLike.Image = Hercules.Properties.ImageResources.plain_icon75x75;
                    this.pictureBoxAgain.Image = Hercules.Properties.ImageResources.plain_icon75x75;
                    this.labelLike.Visible = true;
                    this.labelLikef.Visible = false;
                    this.labelAgain.Visible = true;
                    this.labelAgainf.Visible = false;
                }
            }
            else
            {
                this.pictureBoxLike.Location = new Point( 1024 - (94 + this.pictureBoxLike.Size.Width), 
                    768 - (598 + this.pictureBoxLike.Size.Height ) );
                this.pictureBoxAgain.Location = new Point(1024 - (479 + this.pictureBoxLike.Size.Width),
                    768 - (598 + this.pictureBoxLike.Size.Height));      
                this.labelLikef.Location = new Point(1024 - (169 + this.labelLikef.Size.Width), 
                    768 - (610 + this.labelLikef.Size.Height));
                this.labelAgainf.Location = new Point(1024 - (550+ this.labelAgainf.Size.Width),
                    768 - (610 + this.labelAgainf.Size.Height));

                if (mode == 0)
                {
                    this.pictureBoxLike.Visible = false;
                    this.pictureBoxAgain.Visible = false;
                    this.labelLike.Visible = false;
                    this.labelLikef.Visible = false;
                    this.labelAgain.Visible = false;
                    this.labelAgainf.Visible = false;
                }
                else
                {
                    this.pictureBoxLike.Visible = true;
                    this.pictureBoxAgain.Visible = true;
                    this.pictureBoxLike.Image = Hercules.Properties.ImageResources.plain_icon75x75;
                    this.pictureBoxAgain.Image = Hercules.Properties.ImageResources.plain_icon75x75;
                    this.labelLike.Visible = false;
                    this.labelLikef.Visible = true;
                    this.labelAgain.Visible = false;
                    this.labelAgainf.Visible = true;
                }
            }
        }

        private CustomControl.OrientAbleTextControls.OrientedTextLabel MakeFlipLabel(System.Windows.Forms.Label lbl)
        {
            this.SuspendLayout();
            CustomControl.OrientAbleTextControls.OrientedTextLabel lblf = new
                CustomControl.OrientAbleTextControls.OrientedTextLabel();
            lblf.Location = new Point(lbl.Location.X, lbl.Location.Y);
            lblf.Size = new Size(lbl.Size.Width, lbl.Size.Height);
            lblf.Font = new Font(lbl.Font.FontFamily, lbl.Font.SizeInPoints, lbl.Font.Style);
            

            lblf.ForeColor = lbl.ForeColor;
            lblf.BackColor = lbl.BackColor;
            lblf.Visible = lbl.Visible;
            lblf.TextAlign = lbl.TextAlign;
            lblf.Text = lbl.Text;
            lblf.RotationAngle = 180.0f;
            this.Controls.Add(lblf);
            lblf.BringToFront();
            this.ResumeLayout();
            lblf.Parent = this.pb;
            return lblf;   
        }

        private void MakeFlipLabels()
        {
            this.infof = this.MakeFlipLabel(this.info);
            this.infof.Parent = this.pb;

            this.labelLikef = this.MakeFlipLabel(this.labelLike);
            this.labelAgainf = this.MakeFlipLabel(this.labelAgain);

            this.labell1f = this.MakeFlipLabel(this.labell1);
            this.labell2f = this.MakeFlipLabel(this.labell2);
            this.labell3f = this.MakeFlipLabel(this.labell3);
            this.labell4f = this.MakeFlipLabel(this.labell4);

            this.labelr1f = this.MakeFlipLabel(this.labelr1);
            this.labelr2f = this.MakeFlipLabel(this.labelr2);
            this.labelr3f = this.MakeFlipLabel(this.labelr3);
            this.labelr4f = this.MakeFlipLabel(this.labelr4);
        }

        private void DoWebCam_Loaded()
        {
            this.get_rotation();

            Directory.CreateDirectory(this.currentSession.PhotoPath);

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            if (ConfigUtility.IsDeveloperMode)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Fixed3D;
            }

            if (false)
            {
                this.offscreen = new MME.Hercules.WPFForms.OffScreenRender();
                offscreen.Show();
                offscreen.BringToFront();
            }

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "TEXT_COLOR")))
            {
                String str = ConfigUtility.GetConfig(ConfigUtility.Config, "TEXT_COLOR");
                info.ForeColor = ColorTranslator.FromHtml(str);
            }
            else
            {
                info.ForeColor = System.Drawing.Color.Black;
            }

            //  Set message text properties...
            
            infof.ForeColor = info.ForeColor;
            //info.BackColor = System.Drawing.Color.Red;
            //infof.BackColor = System.Drawing.Color.Red;
            info.Visible = true;
            info.Parent = pb;
            info.AutoSize = false;
            info.Text = "Get Ready.  We are about to take your picture!";
            info.TextAlign = ContentAlignment.MiddleCenter;
            Font nf = new Font(FontFamily.GenericSansSerif, 30);
            info.Font = nf;
            infof.Text = info.Text;
            infof.Size = info.Size;
            infof.Font = new Font(info.Font.FontFamily, info.Font.SizeInPoints, info.Font.Style);


            //  Put elements into place based on orientation...
            this.Orient(this.mode);

            //  Set the flip buttons...
            Bitmap bm = Hercules.Properties.ImageResources.FLIP_THUMB_30x30_flip;
            this.pictureBoxFlipTop.Image = bm;
            this.pictureBoxFlipTop.Visible = true;
            this.pictureBoxFlipTop.Parent = this.pb;

            bm = Hercules.Properties.ImageResources.FLIP_THUMB_30x30;
            this.pictureBoxFlipBottom.Image = bm;
            this.pictureBoxFlipBottom.Visible = true;
            this.pictureBoxFlipBottom.Parent = this.pb;

            //  Back Button...
            bm = Hercules.Properties.ImageResources.back_button;
            this.pictureBoxBack.Image = bm;
            this.pictureBoxBack.Parent = pb;
            this.pictureBoxBack.Visible = true;

            this.pictureBoxLike.Parent = pb;
            this.pictureBoxAgain.Parent = pb;

            //  Determine if meta buttons are enabled...
            if (this.mode == 0)
            {
                this.pictureBoxBack.Visible = false;
                this.pictureBoxFlipBottom.Visible = false;
                this.pictureBoxFlipTop.Visible = false;
            }
            else // prompt mode...
            {
                this.pictureBoxBack.Visible = true;
                this.pictureBoxFlipBottom.Visible = true;
                this.pictureBoxFlipTop.Visible = true;
            }



            //  Prep the prompt buttons...
            this.pictureBoxAgain.Visible = false;
            this.pictureBoxLike.Visible = false;
            this.labelLike.Visible = false;
            this.labelAgain.Visible = false;
            labelAgain.ForeColor = info.ForeColor;
            labelAgainf.ForeColor = labelAgain.ForeColor;
            labelAgain.Parent = pb;
            labelLike.ForeColor = info.ForeColor;
            labelLikef.ForeColor = labelLike.ForeColor;
            labelLike.Parent = pb;
            

            //  Make sure snapshot preview is off...
            this.preview.Visible = false;

            //  More number panel config to ensure transparency...
            //gw
            this.labell1.Parent = pb;
            this.labell1.ForeColor = info.ForeColor;
            this.labell1f.ForeColor = this.labell1.ForeColor;
            this.labell2.Parent = pb;
            this.labell2.ForeColor = info.ForeColor;
            this.labell2f.ForeColor = this.labell2.ForeColor;
            this.labell3.Parent = pb;
            this.labell3.ForeColor = info.ForeColor;
            this.labell3f.ForeColor = this.labell3.ForeColor;
            this.labell4.Parent = pb;
            this.labell4.ForeColor = info.ForeColor;
            this.labell4f.ForeColor = this.labell4.ForeColor;
            this.labelr1.Parent = pb;
            this.labelr1.ForeColor = info.ForeColor;
            this.labelr1f.ForeColor = info.ForeColor;
            this.labelr2.Parent = pb;
            this.labelr2.ForeColor = info.ForeColor;
            this.labelr2f.ForeColor = info.ForeColor;
            this.labelr3.Parent = pb;
            this.labelr3.ForeColor = info.ForeColor;
            this.labelr3f.ForeColor = info.ForeColor;
            this.labelr4.Parent = pb;
            this.labelr4.ForeColor = info.ForeColor;
            this.labelr4f.ForeColor = info.ForeColor;
            
            //  Setup video preview...
            if (istable)
            {
                this.num_cams = SetupVideo();
                if (( this.num_cams == 0) || (this.num_cams > 2))
                {
                    this.DialogResult = DialogResult.No;
                    return;
                }

                if (this.num_cams == 1)
                {
                    this.startpreview(0);
                }
                else if (this.num_cams == 2)
                {
                    if (this.global_orientation == 1)
                    {
                        this.startpreview(0);
                    }
                    else
                    {
                        this.startpreview(1);
                    }
                }
            }

            //  Match the snapshot preview to video preview...
            this.preview.Location = this.vidPanel[0].Location;
            this.preview.Size = this.vidPanel[0].Size;

            this.Refresh();
            Application.DoEvents();
            System.Threading.Thread.Sleep(1);
            System.Threading.Thread.Sleep(1000);

            //  Get Ready...
            SoundUtility.PlaySync(Hercules.Properties.SoundResources.GET_READY);
            if (this.static_orientation == 0)
            {
                this.info.Visible = true;
                this.infof.Visible = false;
            }
            else
            {
                this.info.Visible = false;
                this.infof.Visible = true;
            }

            /*
            System.Threading.Thread.Sleep(1000);

            //SoundUtility.Play(Hercules.Properties.SoundResources.COUNTDOWN);
            SoundUtility.Play(Hercules.Properties.SoundResources.FOUR_COUNT);
            System.Threading.Thread.Sleep(1000);

            SoundUtility.Play(Hercules.Properties.SoundResources.THREE_COUNT);
            System.Threading.Thread.Sleep(1000);

            SoundUtility.Play(Hercules.Properties.SoundResources.TWO_COUNT);
            System.Threading.Thread.Sleep(1000);

            SoundUtility.Play(Hercules.Properties.SoundResources.ONE_COUNT);
            System.Threading.Thread.Sleep(1000);

             * */

            //  Present the countodown...
            this.ShowCountdown(false, 0, true);
              
            //  Grab the image...
            SoundUtility.PlaySync(Hercules.Properties.SoundResources.CAMERA_CLICK);
            this.GrabImage(0);

            //  Show the snapshot preview...
            this.preview.Image = FileUtility.LoadBitmap(this.currentSession.PhotoPath + "\\photo" + (1) + ".jpg");
            this.preview.Visible = true;
            
            //  Turn off cam preview...
            this.vidPanel[0].Visible = false;
            this.vidPanel[1].Visible = false;

            //  Change the message...
            info.Text = "Do You Like Your Picture?";
            info.TextAlign = ContentAlignment.MiddleCenter;
            infof.Text = info.Text;
            infof.TextAlign = info.TextAlign;

            //  Change mode here...
            this.mode = 1;
            Orient(1);
            this.pictureBoxBack.Visible = true;
            this.pictureBoxFlipBottom.Visible = true;
            this.pictureBoxFlipTop.Visible = true;

            //  Stop the offscreen...
            if (this.offscreen!=null)
            {
                this.offscreen.Dispose();
                this.offscreen = null;
            }

            //  Refresh user interface...
            this.Refresh();
            Application.DoEvents();

            //  Stop the preview jobs...
            this.StopJob(0);
            this.StopJob(1);

            //  Wait...
            //System.Threading.Thread.Sleep(4000);

            //  TODO: I think its important to call this...
            DevelopPhotos();


            /*
            if (preview.Image != null)
                preview.Image.Dispose();

            if (pb.Image != null)
                pb.Image.Dispose();


            DialogResult = System.Windows.Forms.DialogResult.OK;
             * */
        }


        private void TakePhotos_Load(object sender, EventArgs e)
        {
            if (istable)
            {
                this.DoWebCam_Loaded();
                return;
            }
            this.preview.Visible = false;

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
                ShowCountdown(use_images, 0, false);
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
                        ShowCountdown(use_images, i + 1, false);
                    }
                }
                else
                {
                    /*was commented*/
                    if (i < ConfigUtility.PhotoCount - 1)
                    {

                        int countdownwait = Convert.ToInt32(ConfigUtility.GetValue("CountdownWait"));
                        Thread.Sleep(countdownwait);
                        SoundUtility.PlaySync(Hercules.Properties.SoundResources.COUNTDOWN);
                    }
                     
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

            //gw if (ConfigUtility.MaxCopies > 1)
            if ( currentSession.MaxCopies >= 1 )
            {
                printDoc.PrinterSettings.Copies = (short)currentSession.MaxCopies;
                    //gw (short)ConfigUtility.MaxCopies;
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

            //if (prompts != null)
            //    prompts.Abort();

            prompts = null;
            thread = null;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBoxLike_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
        }

        private void pictureBoxAgain_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            
        }

        private void pictureBoxAgain_Click(object sender, EventArgs e)
        {
            //reset mode...
            this.mode = 0;
            this.TakePhotos_Load(this, null);
        }

        private void pictureBoxLike_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.StopJob(0);
            this.StopJob(1);
            return;
        }

        private void pictureBoxBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.StopJob(0);
            this.StopJob(1);
            return;
        }

        private void rotate_via_graphics()
        {
            try
            {

                // initialize the DEVMODE structure

                DEVMODE dm = new DEVMODE();
                dm.dmDeviceName = new string(new char[32]);
                dm.dmFormName = new string(new char[32]);
                dm.dmSize = (short)Marshal.SizeOf(dm);

                if (0 != NativeMethods.EnumDisplaySettings(
                    null,
                    NativeMethods.ENUM_CURRENT_SETTINGS,
                    ref dm))
                {
                    /*
                    System.Windows.Forms.MessageBox.Show(
                        String.Format("res->{0},{1}",
                        dm.dmPelsWidth, dm.dmPelsHeight));
                    */

                    // swap width and height
                    //int temp = dm.dmPelsHeight;
                    //dm.dmPelsHeight = dm.dmPelsWidth;
                    //dm.dmPelsWidth = temp;

                    int new_orientation = 0;


                    // determine new orientation
                    switch (dm.dmDisplayOrientation)
                    {
                        case NativeMethods.DMDO_DEFAULT:
                            dm.dmDisplayOrientation = NativeMethods.DMDO_180;
                            new_orientation = 1;
                            break;
                        case NativeMethods.DMDO_270:
                            dm.dmDisplayOrientation = NativeMethods.DMDO_180;
                            break;
                        case NativeMethods.DMDO_180:
                            dm.dmDisplayOrientation = NativeMethods.DMDO_DEFAULT;
                            new_orientation = 0;
                            break;
                        case NativeMethods.DMDO_90:
                            dm.dmDisplayOrientation = NativeMethods.DMDO_DEFAULT;
                            break;
                        default:
                            // unknown orientation value
                            // add exception handling here
                            break;
                    }

                    bool worked = true;
                    int iRet = NativeMethods.ChangeDisplaySettings(ref dm, 0);
                    if (NativeMethods.DISP_CHANGE_SUCCESSFUL != iRet)
                    {
                        // add exception handling here
                        System.Windows.Forms.MessageBox.Show("cannot change display");
                        worked = false;
                    }

                    if (worked)
                    {
                        this.global_orientation = new_orientation;
                    }

                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("cannot enum display");
                }

            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }


        private void get_rotation()
        {
            try
            {

                // initialize the DEVMODE structure

                DEVMODE dm = new DEVMODE();
                dm.dmDeviceName = new string(new char[32]);
                dm.dmFormName = new string(new char[32]);
                dm.dmSize = (short)Marshal.SizeOf(dm);

                if (0 != NativeMethods.EnumDisplaySettings(
                    null,
                    NativeMethods.ENUM_CURRENT_SETTINGS,
                    ref dm))
                {

                    // determine new orientation
                    switch (dm.dmDisplayOrientation)
                    {
                        case NativeMethods.DMDO_DEFAULT:
                            this.global_orientation = 0;
                            break;
                       
                        case NativeMethods.DMDO_180:
                            dm.dmDisplayOrientation = NativeMethods.DMDO_DEFAULT;
                            this.global_orientation = 1;
                            break;
                       
                        default:
                            // unknown orientation value
                            // add exception handling here
                            break;
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("cannot enum display");
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        private void pictureBoxFlipTop_Click(object sender, EventArgs e)
        {
            rotate_via_graphics();
            return;

            if (static_orientation != 1)
            {
                static_orientation = 1;
                this.Orient(this.mode);
            }
        }

        private void pictureBoxFlipBottom_Click(object sender, EventArgs e)
        {
            rotate_via_graphics();
            return;

            if (static_orientation != 0)
            {
                static_orientation = 0;
                this.Orient(this.mode);
            }
        }
    }


    public class NativeMethods
    {
        // PInvoke declaration for EnumDisplaySettings Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern int EnumDisplaySettings(
            string lpszDeviceName,
            int iModeNum,
            ref DEVMODE lpDevMode);

        // PInvoke declaration for ChangeDisplaySettings Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern int ChangeDisplaySettings(
            ref DEVMODE lpDevMode,
            int dwFlags);

        // add more functions as needed …

        // constants
        public const int ENUM_CURRENT_SETTINGS = -1;
        public const int DMDO_DEFAULT = 0;
        public const int DMDO_90 = 1;
        public const int DMDO_180 = 2;
        public const int DMDO_270 = 3;
        // add more constants as needed …

        //gw
        public const int DISP_CHANGE_SUCCESSFUL = 0;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct DEVMODE
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmDeviceName;

        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public int dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmFormName;

        public short dmLogPixels;
        public short dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    };

}
