using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MME.Hercules.Forms.Operator
{
    public partial class Chromakey : Form
    {
        Chromagic.ChromaKey m_chromagic;
        Bitmap m_background;
        Bitmap m_foreground;
        Bitmap m_combined;

        private string testphotopath;
        private string testbgpath;

        public Chromakey()
        {
            InitializeComponent();

            m_chromagic = new Chromagic.ChromaKey();
            m_chromagic.Hue = Convert.ToSingle(ConfigUtility.GetValue("Chromakey_Hue"));
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


        private void CompositeImages()
        {
            if (m_foreground == null || m_background == null)
                return;
               
            m_combined = new Bitmap(m_foreground.Width,m_foreground.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(m_combined);

            if (m_background != null)
                g.DrawImage(m_background, new Rectangle(0, 0, m_combined.Width, m_combined.Height));

            if (m_foreground != null)
            {
                // first, render our foreground image into a guaranteed 32 bpp argb buffer
                Bitmap chroma = new Bitmap(m_combined.Width, m_combined.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics chromag = Graphics.FromImage(chroma);
                chromag.DrawImage(m_foreground, new Rectangle(0, 0, chroma.Width, chroma.Height));
                bool b = m_chromagic.Chroma(chroma);

                g.DrawImage(chroma, new Rectangle(0, 0, chroma.Width, chroma.Height));

                composite.Image = m_combined;
            }



        }


        private void Chromakey_Load(object sender, EventArgs e)
        {
            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;

            hueupdown.Value = Convert.ToInt32(ConfigUtility.GetValue("Chromakey_Hue"));
            toleranceupdown.Value = Convert.ToInt32(ConfigUtility.GetValue("Chromakey_Tolerance"));
            minupdown.Value = Convert.ToInt32(ConfigUtility.GetValue("Chromakey_Min"));
            satupdown.Value = Convert.ToInt32(ConfigUtility.GetValue("Chromakey_Saturation"));
            maxupdown.Value = Convert.ToInt32(ConfigUtility.GetValue("Chromakey_Max"));

        }




        private void changephoto_Click(object sender, EventArgs e)
        {
            // select file
            // load file
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Open Sample Photo Image";
            fDialog.Filter = "JPG Files|*.jpg";
            fDialog.InitialDirectory = @"C:\";
            fDialog.FilterIndex = 2;
            fDialog.RestoreDirectory = true;

            if (fDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                testphotopath = fDialog.FileName;
                m_foreground = new Bitmap(testphotopath);
                photo.Image = m_foreground;
            }
            else
            {
                m_foreground = null;
                testphotopath = "";
            }

            CompositeImages();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Open Sample Background Image";
            fDialog.Filter = "JPG Files|*.jpg";
            fDialog.InitialDirectory = @"C:\";
            fDialog.FilterIndex = 2;
            fDialog.RestoreDirectory = true;

            if (fDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                testbgpath = fDialog.FileName;
                m_background = new Bitmap(testbgpath);

                testbg.Image = m_background;
            }
            else
            {
                m_background = null;
                testbgpath = "";
            }

            CompositeImages();
        }

        bool skip = false;

        private void hue_Scroll(object sender, EventArgs e)
        {
            m_chromagic.Hue = (float)hueupdown.Value;
     
            CompositeImages();
        }     

        private void saturation_Scroll(object sender, EventArgs e)
        {
            m_chromagic.Saturation = (float)satupdown.Value / 100.0f;
            CompositeImages();
        }

        private void min_Scroll(object sender, EventArgs e)
        {
            m_chromagic.MinValue = (float)minupdown.Value / 100.0f;
            CompositeImages();
        }

        private void maximum_Scroll(object sender, EventArgs e)
        {
            m_chromagic.MinValue = (float)maxupdown.Value / 100.0f;
            CompositeImages();
        }

        private void tolerance_Scroll(object sender, EventArgs e)
        {
            m_chromagic.Tolerance = (float)toleranceupdown.Value;
            CompositeImages();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConfigUtility.SetValue("Chromakey_Hue", hueupdown.Value.ToString());
            ConfigUtility.SetValue("Chromakey_Saturation", satupdown.Value.ToString());
            ConfigUtility.SetValue("Chromakey_Tolerance", toleranceupdown.Value.ToString());
            ConfigUtility.SetValue("Chromakey_Min", minupdown.Value.ToString());
            ConfigUtility.SetValue("Chromakey_Max", maxupdown.Value.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Chromakey_Load(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            composite.Image.Save("Temp\\composite.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void composite_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            m_foreground.RotateFlip(RotateFlipType.Rotate90FlipNone);
            CompositeImages();
        }

        
    
    }
}
