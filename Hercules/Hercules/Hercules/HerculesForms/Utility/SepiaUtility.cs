using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MME.Hercules
{
    public static class SepiaUtility
    {
	   

        public static void SepiaBitmap(Bitmap bmp)
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect,
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            IntPtr ptr = bmpData.Scan0;
            int numPixels = bmpData.Width * bmp.Height;
            int numBytes = numPixels * 4;
            byte[] rgbValues = new byte[numBytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, numBytes);

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                int red = rgbValues[i + 2];
                int green = rgbValues[i + 1];
                int blue = rgbValues[i + 0];
                
                rgbValues[i + 2] = (byte)Math.Min( 0.75*( (.393 * red) + (.769 * green) + (.189 * (blue))), 255.0);;
                //red             
                rgbValues[i + 1] = (byte)Math.Min( 0.75*( (.349 * red) + (.686 * green) + (.168 * (blue))), 255.0);
                //green             
                rgbValues[i + 0] = (byte)Math.Min( 0.75*( (.272 * red) + (.534 * green) + (.131 * (blue))), 255.0);
                //blue         
            }

            /*     
            for (int i = 0; i < rgbValues.Length; i += 4)     
                {         
                    rgbValues[i + 2] = (byte)((.393 * rgbValues[i + 2]) + (.769 * rgbValues[i + 1]) + (.189 * (rgbValues[i + 0]))); 
                    //red         
                    rgbValues[i + 1] = (byte)((.349 * rgbValues[i + 2]) + (.686 * rgbValues[i + 1]) + (.168 * (rgbValues[i + 0]))); 
                    //green         
                    rgbValues[i + 0] = (byte)((.272 * rgbValues[i + 2]) + (.534 * rgbValues[i + 1]) + (.131 * (rgbValues[i + 0]))); 
                    //blue          
                    if ((rgbValues[i + 2]) > 255)         
                    {             
                        rgbValues[i + 2] = 255;          
                    }          
                    if ((rgbValues[i + 1]) > 255)         
                    {             
                        rgbValues[i + 1] = 255;         
                    }         
                    if ((rgbValues[i + 0]) > 255)         
                    {             
                        rgbValues[i + 0] = 255;         
                    }     
                }      
            */

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, numBytes);
            //this.Invalidate();
            bmp.UnlockBits(bmpData);
        }

        /*
        public static Bitmap Sepia_ify( Bitmap photo )
        {
            //  Create new bitmap...
            Bitmap m_new;
            m_new = new Bitmap(photo.Width, photo.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //  Copy the photo first...
            Graphics g = Graphics.FromImage(m_new);
            g.DrawImage(photo, new Rectangle(0, 0, m_new.Width, m_new.Height));
        }
         * */
    }
}
