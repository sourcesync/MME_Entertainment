using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MME.Hercules.Forms.User
{
    public partial class PickFavorite : Form
    {
        private List<Background> bgs = new List<Background>();
        private Session currentSession;

        public PickFavorite(Session currentSession)
        {
            Application.DoEvents();

            InitializeComponent();

            this.currentSession = currentSession;
        }

        private void LoadPhotos()
        {
            bgs.Clear();

            int startx = (ConfigUtility.PhotoCount == 4) ? 280 : 170;
            int y = 230;
            int x = startx;
            int padding = 15;
            int xspacing = 25;
            int yspacing = 22;

            if (ConfigUtility.PhotoCount <= 2)
                y += 50;

            for (int i = 0; i < ConfigUtility.PhotoCount; i++)
            {

                PictureBox bg = new PictureBox();

                bg.Name = "bg" + (i + 1).ToString();
                bg.Load(string.Format(this.currentSession.PhotoPath + "\\photo{0}.jpg",
                    i+1));
                bg.Width = 160 + (padding * 2);
                bg.Height = 225 + (padding * 2);
                bg.Parent = pb;
                bg.BackColor = System.Drawing.Color.Transparent;
                bg.Padding = new Padding(padding);
                bg.SizeMode = PictureBoxSizeMode.Zoom;

                //gw
                if (CameraUtility.IsConnected())
                //gw
                {
                    bg.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }

                if (this.currentSession.SelectedColorType == ColorType.BW)
                    bg.Image = FileUtility.MakeGrayscale((System.Drawing.Bitmap)bg.Image);

                if (i == 0)
                    x -= bg.Width;

                if ((i == 2 && ConfigUtility.PhotoCount == 4) || (i == 3 && ConfigUtility.PhotoCount == 6))
                {
                    x = startx - bg.Width;
                    y += bg.Height + yspacing;
                }

                x += (bg.Width + xspacing);

                bg.Location = new Point(x, y);
                bg.Click += new EventHandler(bg_Click);

                Background background = new Background(bg, 0);
                bgs.Add(background);

                this.Controls.Add(bg);

                this.Refresh();
            }

            foreach (Background b in bgs)
                b.bg.BringToFront();

        }

        private void bg_Click(object sender, EventArgs e)
        {
            PictureBox bg = (PictureBox)sender;

            // set favorite
            this.currentSession.FavoritePhoto = Convert.ToInt32(bg.Name.Replace("bg", ""));
            this.currentSession.FavoritePhotoFilename = Guid.NewGuid().ToString().Replace("-", "");

            //System.Windows.Forms.MessageBox.Show("favorite " + this.currentSession.FavoritePhoto + " " + this.currentSession.FavoritePhotoFilename);

            bg.BackColor = Color.Red;
            this.Refresh();

            if (pb.Image != null)
                pb.Image.Dispose();

            foreach (Background b in bgs)
            {
                if (b.bg != null && b.bg.Image != null)
                    b.bg.Image.Dispose();
            }

            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);


            
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void PickFavorite_Load(object sender, EventArgs e)
        {
            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;

            WindowUtility.SetScreen(pb, Hercules.Properties.Resources.PICK_FAVORITE_SCREEN);

            this.Refresh();

            SoundUtility.Play(Hercules.Properties.SoundResources.PICK_FAVORITE);

            LoadPhotos();


        }

        private void PickFavorite_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 27)
            {
                Application.Exit();
            }
        }
    }
}
