using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace NewLeafQR
{
    public class NewLeafBitmap
    {
        public Bitmap falseColor = new Bitmap(32, 32);
        public byte[] filepal = new byte[15];
        public static List<Color> ACPaletteColors; 
        public void readBitmapFromByteArray(byte[] habytes)
        {
            int off = 0;
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    falseColor.SetPixel(j, i, Color.FromArgb(habytes[off++]));
                }

            }

        }

        public static byte findACPaletteIndexMostLike(Color c)
        {
            if (ACPaletteColors == null)
                populateACPaletteColors();

            byte targInd = 0;

            double championDistance = double.MaxValue;

            for (byte i = 0; i < ACPaletteColors.Count; i++)
            {
                double dist = RGBOctree.EuclideanRGB(c, ACPaletteColors[i]) ;
                if (dist < championDistance)
                {
                    championDistance = dist;
                    targInd = i;
                }
            }

            return targInd;

        }

        public static void populateACPaletteColors()
        {
            ACPaletteColors = new List<Color>();

            for (byte i = 0; i < 159; i++)
            {
                ACPaletteColors.Add(getTrueColor(i));
            }

        }


        public void readPaletteFromArras(BitArras bar)
        {

            byte[] sz = bar.ReadBytes(15, false);

            bar.BlowBits(8);

            BitArray palbits = BitArras.FromByteArray(sz, true);
            BitArras arraspal = new BitArras(palbits);

            sz = arraspal.ReadBytes(15, false);

            filepal = new byte[15];

            for (int i = 0; i < 15; i++)
            {
                filepal[i] = Form1.findPalIndex(sz[i]);
            }
     

        }

        public static NewLeafBitmap NLBfromBitmap(Bitmap src)
        {
            NewLeafBitmap tortimer = new NewLeafBitmap();

            double wid = 32; double hig = 32;
            Bitmap scaled = new Bitmap((int)wid, (int)hig);
            using (Graphics graphics = Graphics.FromImage(scaled))
            {
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                graphics.DrawImage(src, new Rectangle(0, 0, scaled.Width, scaled.Height));
            }

            RGBOctree tree = new RGBOctree((Bitmap)scaled, 15);

          //  MessageBox.Show("Reduced to " + tree.lastQuantizedPalette.Count + "  colors.");

            tree.QuantizeOnLastPalette((Bitmap)scaled);

            byte i = 0;
            tortimer.filepal = new byte[15];

            for (byte o = 0; o < tree.lastQuantizedPalette.Count; o++)
            {
                Color me = tree.lastQuantizedPalette[o].color;

                Button newb = new Button();

                newb.Left = 20 * o;
                newb.Top = 20;
                newb.Width = 15;

                newb.BackColor = me;
                Form1.grp.Controls.Add(newb);
            }

            foreach (RGBOctree.RGBOctreeNode rgbon in tree.lastQuantizedPalette)
            {
               

                byte mostlike = findACPaletteIndexMostLike(rgbon.color);
                tortimer.filepal[transtosilly(i)] = mostlike;

               // MessageBox.Show("Color " + rgbon.color.ToArgb() + " is most like " + tortimer.filepal[transtosilly(i)]);


                for (int j = 0; j < 32; j++)
                    for (int k = 0; k < 32; k++)
                    {
                        if (scaled.GetPixel(j,k) == rgbon.color)
                        tortimer.falseColor.SetPixel(j,k, Color.FromArgb(i));
                    }

                i++;
            }


            return tortimer;

        }

        public List<Color> getPaletteDisplayColors()
        {
            List<Color> dispCols = new List<Color>();

            for (byte i = 0; i < filepal.Length; i++)
                dispCols.Add(getTrueColorFromPal(i));

            return dispCols;
        }

        public void readPaletteFromByteArray(byte[] arr)
        {
            filepal = new byte[15];

            for (int i = 0; i < 15; i++)
            {
                filepal[i] = (findPalIndex(arr[i]));
            }
        }

        public static byte transfromsilly(byte bin)
        {
            byte[] trans = new byte[] { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7 };

            for (int i = 0; i < trans.Length; i++)
                if (bin == trans[i])
                    return (byte)i;

            return 0;

        }

        public static byte transtosilly(byte bin)
        {
            byte[] trans = new byte[] { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7 };

            return trans[bin];

        }


        public static Color getTrueColor(byte i)
        {

            return Color.FromArgb(pallet_r[i], pallet_g[i], pallet_b[i]);
        }

        public  Color getTrueColorFromPal(byte i)
        {

            return Color.FromArgb(pallet_r[filepal[i]], pallet_g[filepal[i]], pallet_b[filepal[i]]);
        }

        public Bitmap getTrueColorBitmap()
        {
            int ilen, jlen;
            ilen = falseColor.Width;
            jlen = falseColor.Height;

            Bitmap trueBmp = new Bitmap(ilen, jlen);

            for (int i = 0; i < ilen; i++)
                for (int j = 0; j < jlen; j++)
                {
                   byte bu= transfromsilly( ((byte)(falseColor.GetPixel(i, j).ToArgb())) );
                 //  MessageBox.Show(bu+"!");
                    trueBmp.SetPixel(i, j, getTrueColorFromPal(bu));
                }

            return trueBmp;
        }




        public static byte findPalIndexInv(byte palListing)
        {
            byte[] dmori_palindex = new byte[] {0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x10,0x11,0x12,0x13,0x14,0x15,0x16,0x17,0x18,0x20,0x21,0x22,0x23,0x24,0x25,0x26,0x27,0x28,
        0x30,0x31,0x32,0x33,0x34,0x35,0x36,0x37,0x38,0x40,0x41,0x42,0x43,0x44,0x45,0x46,0x47,0x48,0x50,0x51,0x52,0x53,0x54,0x55,0x56,0x57,0x58,
         0x60,0x61,0x62,0x63,0x64,0x65,0x66,0x67,0x68,0x70,0x71,0x72,0x73,0x74,0x75,0x76,0x77,0x78,0x80,0x81,0x82,0x83,0x84,0x85,0x86,0x87,0x88,
            0x90,0x91,0x92,0x93,0x94,0x95,0x96,0x97,0x98,0xa0,0xa1,0xa2,0xa3,0xa4,0xa5,0xa6,0xa7,0xa8,0xb0,0xb1,0xb2,0xb3,0xb4,0xb5,0xb6,0xb7,0xb8,
                0xc0,0xc1,0xc2,0xc3,0xc4,0xc5,0xc6,0xc7,0xc8,0xd0,0xd1,0xd2,0xd3,0xd4,0xd5,0xd6,0xd7,0xd8,0xe0,0xe1,0xe2,0xe3,0xe4,0xe5,0xe6,0xe7,0xe8,
                    0xf0,0xf1,0xf2,0xf3,0xf4,0xf5,0xf6,0xf7,0xf8,0x0f,0x1f,0x2f,0x3f,0x4f,0x5f,0x6f,0x7f,0x8f,0x9f,0xaf,0xbf,0xcf,0xdf,0xef};

          
            return dmori_palindex[palListing];
        }

        public static byte findPalIndex(byte palListing)
        {
            byte[] dmori_palindex = new byte[] {0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x10,0x11,0x12,0x13,0x14,0x15,0x16,0x17,0x18,0x20,0x21,0x22,0x23,0x24,0x25,0x26,0x27,0x28,
        0x30,0x31,0x32,0x33,0x34,0x35,0x36,0x37,0x38,0x40,0x41,0x42,0x43,0x44,0x45,0x46,0x47,0x48,0x50,0x51,0x52,0x53,0x54,0x55,0x56,0x57,0x58,
         0x60,0x61,0x62,0x63,0x64,0x65,0x66,0x67,0x68,0x70,0x71,0x72,0x73,0x74,0x75,0x76,0x77,0x78,0x80,0x81,0x82,0x83,0x84,0x85,0x86,0x87,0x88,
            0x90,0x91,0x92,0x93,0x94,0x95,0x96,0x97,0x98,0xa0,0xa1,0xa2,0xa3,0xa4,0xa5,0xa6,0xa7,0xa8,0xb0,0xb1,0xb2,0xb3,0xb4,0xb5,0xb6,0xb7,0xb8,
                0xc0,0xc1,0xc2,0xc3,0xc4,0xc5,0xc6,0xc7,0xc8,0xd0,0xd1,0xd2,0xd3,0xd4,0xd5,0xd6,0xd7,0xd8,0xe0,0xe1,0xe2,0xe3,0xe4,0xe5,0xe6,0xe7,0xe8,
                    0xf0,0xf1,0xf2,0xf3,0xf4,0xf5,0xf6,0xf7,0xf8,0x0f,0x1f,0x2f,0x3f,0x4f,0x5f,0x6f,0x7f,0x8f,0x9f,0xaf,0xbf,0xcf,0xdf,0xef};

            for (byte i = 0; i < 159; i++)
            {
                if (dmori_palindex[i] == palListing)
                    return i;
            }

            return 0;
        }

        static int[] pallet_r = {
      255, 255, 239, 255, 255, 189, 206, 156, 82, 
      255, 255, 222, 255, 255, 206, 189, 189, 140, 
      222, 255, 222, 255, 255, 189, 222, 189, 99, 
      255, 255, 255, 255, 255, 222, 189, 156, 140, 
      255, 239, 206, 189, 206, 156, 140, 82, 49, 
      255, 255, 222, 255, 255, 140, 189, 140, 82, 
      222, 206, 115, 173, 156, 115, 82, 49, 33, 
      255, 255, 222, 255, 255, 206, 156, 140, 82, 
      222, 189, 99, 156, 99, 82, 66, 33, 33, 
      189, 140, 49, 49, 0, 49, 0, 16, 0, 
      156, 99, 33, 66, 0, 82, 33, 16, 0, 
      222, 206, 140, 173, 140, 173, 99, 82, 49, 
      189, 115, 49, 99, 16, 66, 33, 0, 0, 
      173, 82, 0, 82, 0, 66, 0, 0, 0, 
      206, 173, 49, 82, 0, 115, 0, 0, 0, 
      173, 115, 99, 0, 33, 82, 0, 0, 33, 
      255, 239, 222, 206, 189, 
      173, 156, 140, 115, 99, 
      82, 66, 49, 33, 0
    };
        static int[] pallet_g = {
  239, 154, 85, 101, 0, 69, 0, 0, 32, 
  186, 117, 48, 85, 0, 101, 69, 0, 32, 
  207, 207, 101, 170, 101, 138, 69, 69, 48, 
  239, 223, 207, 186, 170, 138, 101, 85, 69, 
  207, 138, 101, 138, 0, 101, 0, 0, 0, 
  186, 154, 32, 85, 0, 85, 0, 0, 0, 
  186, 170, 69, 117, 48, 48, 32, 16, 16, 
  255, 255, 223, 255, 223, 170, 154, 117, 85, 
  186, 154, 48, 85, 0, 69, 0, 0, 16, 
  186, 154, 48, 85, 0, 48, 0, 16, 0, 
  239, 207, 101, 170, 138, 117, 85, 48, 32, 
  255, 255, 170, 223, 255, 186, 186, 154, 101, 
  223, 207, 85, 154, 117, 117, 69, 32, 16, 
  255, 255, 138, 186, 207, 154, 101, 69, 32, 
  255, 239, 207, 239, 255, 170, 170, 138, 69, 
  255, 255, 223, 255, 223, 186, 186, 138, 69, 
  255, 239, 223, 207, 186, 
  170, 154, 138, 117, 101, 
  85, 69, 48, 32, 0
};
        static int[] pallet_b = {
  255, 173, 156, 173, 99, 115, 82, 49, 49, 
  206, 115, 16, 66, 0, 99, 66, 0, 33, 
  189, 99, 33, 33, 0, 82, 0, 0, 16, 
  222, 206, 173, 140, 140, 99, 66, 49, 33, 
  255, 255, 222, 206, 255, 156, 173, 115, 66, 
  255, 255, 189, 239, 206, 115, 156, 99, 66, 
  156, 115, 49, 66, 0, 33, 0, 0, 0, 
  206, 115, 33, 0, 0, 0, 0, 0, 0, 
  255, 239, 206, 255, 255, 140, 156, 99, 49, 
  255, 255, 173, 239, 255, 140, 173, 99, 33, 
  189, 115, 16, 49, 49, 82, 0, 33, 16, 
  189, 140, 82, 140, 0, 156, 0, 0, 0, 
  255, 255, 156, 255, 255, 173, 115, 115, 66, 
  255, 255, 189, 206, 255, 173, 140, 82, 49, 
  239, 222, 173, 189, 206, 173, 156, 115, 49, 
  173, 115, 66, 0, 33, 82, 0, 0, 33, 
  255, 239, 222, 206, 189, 
  173, 156, 140, 115, 99, 
  82, 66, 49, 33, 0
};
    }
}
