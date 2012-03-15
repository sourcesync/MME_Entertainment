using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MME.Hercules
{
    class ChromakeyUtility
    {
        private static Chromagic.ChromaKey m_chromagic = new Chromagic.ChromaKey();

        public static Bitmap CreateComposite(Bitmap photo, Bitmap background)
        {
            Bitmap m_combined;

            m_chromagic.Hue = Convert.ToSingle(ConfigUtility.GetValue("Chromakey_Hue"));
            m_chromagic.Tolerance = Convert.ToSingle(ConfigUtility.GetValue("Chromakey_Tolerance"));
            m_chromagic.Saturation = Convert.ToSingle(ConfigUtility.GetValue("Chromakey_Saturation")) / 100.0f;
            m_chromagic.MinValue = Convert.ToSingle(ConfigUtility.GetValue("Chromakey_Min")) / 100.0f;
            m_chromagic.MaxValue = Convert.ToSingle(ConfigUtility.GetValue("Chromakey_Max")) / 100.0f;
      

            m_combined = new Bitmap(photo.Width, photo.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            
            Graphics g = Graphics.FromImage(m_combined);

            if (background != null)
            {                
                g.DrawImage(background, new Rectangle(0, 0, m_combined.Width, m_combined.Height));
            }

            if (photo != null)
            {
                // first, render our foreground image into a guaranteed 32 bpp argb buffer
                Bitmap chroma = new Bitmap(m_combined.Width, m_combined.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics chromag = Graphics.FromImage(chroma);
                chromag.DrawImage(photo, new Rectangle(0, 0, chroma.Width, chroma.Height));
                bool b = m_chromagic.Chroma(chroma);

                g.DrawImage(chroma, new Rectangle(0, 0, chroma.Width, chroma.Height));

                g.Dispose();
                chromag.Dispose();
                chroma.Dispose();
                photo.Dispose();
                return m_combined;
            }
            else
            {
                g.Dispose();
                photo.Dispose();
                return null;
            }

        }

    }
}
