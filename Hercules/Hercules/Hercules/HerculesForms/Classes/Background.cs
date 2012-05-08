using System;
using System.Windows.Forms;

namespace MME.Hercules
{
    public class Background
    {
        public int id;
        public PictureBox bg;
        public bool Selected;
        public int Count;
        public Label lb;

        public Background(PictureBox pb, int id)
        {
            this.bg = pb;
            this.id = id;
        }
    }
}
