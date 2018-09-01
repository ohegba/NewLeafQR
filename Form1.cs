using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

using ZXing.QrCode.Internal;
using ZXing.QrCode;
using ZXing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;


namespace NewLeafQR
{
    public partial class Form1 : Form
    {
        public static TextBoxStreamWriter tbsw;
        public static Random rng = new Random();

        public static GroupBox grp;

        public static SimplePattern simple;

        int[] pallet_r = {
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
        int[] pallet_g = {
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
        int[] pallet_b = {
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
        public Form1()
        {
            InitializeComponent();
            tbsw = new TextBoxStreamWriter(textBox1);
            Console.SetOut(tbsw);
            grp = groupBox1;
        }

        public byte transfromsilly(byte bin)
        {
            byte[] trans = new byte[] { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7 };

            for (int i = 0; i < trans.Length; i++)
                if (bin == trans[i])
                    return (byte)i;

            return 0;

        }

        public byte[] rot16(byte[] sz)
        {
            byte[] sz2 = new byte[15];

            sz2[0] = sz[14];
            sz2[1] = sz[0];
            sz2[2] = sz[1];

            sz2[3] = sz[2];
            sz2[4] = sz[3];
            sz2[5] = sz[4];
            sz2[6] = sz[5];
            sz2[7] = sz[6];
            sz2[8] = sz[7];
            sz2[9] = sz[8];
            sz2[10] = sz[9];

            sz2[11] = sz[10];
            sz2[12] = sz[11];
            sz2[13] = sz[12];
            sz2[14] = sz[13];
            //sz2[15] = sz[14];

            return sz2;
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

        public static void PairSwap<T>(T[] array)
        {
            for (int i = 0; i + 1 < array.Length; i++)
            {
                T tmp = array[i];
                array[i] = array[i + 1];
                array[i + 1] = tmp;
            }
        }

        public static void getRGB( Bitmap image, int startX, int startY, int w, int h, int[] rgbArray, int offset, int scansize)
        {
            const int PixelWidth = 3;
            const PixelFormat PixelFormat = PixelFormat.Format24bppRgb;

            // En garde!
            if (image == null) throw new ArgumentNullException("image");
            if (rgbArray == null) throw new ArgumentNullException("rgbArray");
            if (startX < 0 || startX + w > image.Width) throw new ArgumentOutOfRangeException("startX");
            if (startY < 0 || startY + h > image.Height) throw new ArgumentOutOfRangeException("startY");
            if (w < 0 || w > scansize || w > image.Width) throw new ArgumentOutOfRangeException("w");
            if (h < 0 || (rgbArray.Length < offset + h * scansize) || h > image.Height) throw new ArgumentOutOfRangeException("h");

            BitmapData data = image.LockBits(new Rectangle(startX, startY, w, h), System.Drawing.Imaging.ImageLockMode.ReadOnly, PixelFormat);
            try
            {
                byte[] pixelData = new Byte[data.Stride];
                for (int scanline = 0; scanline < data.Height; scanline++)
                {
                    Marshal.Copy(data.Scan0 + (scanline * data.Stride), pixelData, 0, data.Stride);
                    for (int pixeloffset = 0; pixeloffset < data.Width; pixeloffset++)
                    {
                        // PixelFormat.Format32bppRgb means the data is stored
                        // in memory as BGR. We want RGB, so we must do some 
                        // bit-shuffling.
                        rgbArray[offset + (scanline * scansize) + pixeloffset] =
                            (pixelData[pixeloffset * PixelWidth + 2] << 16) +   // R 
                            (pixelData[pixeloffset * PixelWidth + 1] << 8) +    // G
                            pixelData[pixeloffset * PixelWidth];                // B
                    }
                }
            }
            finally
            {
                image.UnlockBits(data);
            }
        }

        public static void Shuffle<T>(T[] array)
        {
            var random = rng;
            for (int i = array.Length; i > 1; i--)
            {
                // Pick random element to swap.
                int j = random.Next(i); // 0 <= j <= i-1
                // Swap.
                T tmp = array[j];
                array[j] = array[i - 1];
                array[i - 1] = tmp;
            }
        }

        MemoryStream QRFromClipboard()
        {
            QRCodeReader redd = new QRCodeReader();
            Bitmap bmp = (Bitmap)Clipboard.GetImage();

            BitmapLuminanceSource blp = new BitmapLuminanceSource(bmp);
         

            pictureBox3.Image = bmp;

            Binarizer binarizer = new ZXing.Common.HybridBinarizer(blp);
            BinaryBitmap binBmp = new BinaryBitmap(binarizer);
            Result rr = redd.decode(binBmp);

            String dat = "";

            byte[] payload = new byte[rr.RawBytes.Length];

            FileStream ff = new FileStream("C:/DATASETS/incoming", FileMode.Create);

            for (int i = 0; i < rr.RawBytes.Length; i++)
                ff.WriteByte(rr.RawBytes[i]);

            ff.Close();



            for (int i = 0; i < payload.Length; i++)

                payload[i] = (byte)rr.RawBytes[i];


            MessageBox.Show(new String(UnicodeEncoding.Unicode.GetChars(payload)));

            MemoryStream fs = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(fs);

            fs.Write(payload, 0, payload.Length);

            fs.Seek(0, SeekOrigin.Begin);

            return fs;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Hi");

            if (!Clipboard.ContainsImage())
            {
                MessageBox.Show("No image on the clipboard!");
                return;
            }
            MemoryStream fs;
          
                
                fs = QRFromClipboard();
           
          
            
            


            BinaryReader br = new BinaryReader(fs);
            //Console.WriteLine(br.PeekChar());

            long blen = fs.Length;
            byte[] barg = new byte[blen];

            barg = br.ReadBytes((int)blen);

            BitArray bat = BitArras.FromByteArray(barg, true);
            BitArras bar = new BitArras(bat);

            bar.BlowBits(8);

            Console.WriteLine(bar.strBits(12));


            bar.BlowBits(12);


            Console.WriteLine(bar.strBits(8));

            byte[] sz = bar.ReadBytes(42, true);

            BitArras.ReverseByteArray(sz);

            String designTitle = UnicodeEncoding.Unicode.GetString(sz);

            Console.WriteLine(designTitle);
            nombo.Text = designTitle;

         

            Console.WriteLine();

            Console.WriteLine(bar.getpos());

            bar.BlowBits(16);

    
            sz = bar.ReadBytes(20, true);
            MessageBox.Show(bar.getpos() + " IAM");
            BitArras.ReverseByteArray(sz);

            String designAuthor = UnicodeEncoding.Unicode.GetString(sz);
            authbox.Text = designAuthor;

            Console.WriteLine(designAuthor);

            bar.BlowBits(16);
            


            sz = bar.ReadBytes(18, true);



            BitArras.ReverseByteArray(sz);

            String designTown = UnicodeEncoding.Unicode.GetString(sz);
            citybox.Text = "" + designTown;

            Console.WriteLine(designTown);

          


            bar.BlowBits(32);



            Console.WriteLine(bar.getpos());
            sz = bar.ReadBytes(15, false);





            bar.BlowBits(8);

            BitArray palbits = BitArras.FromByteArray(sz, true);
            if (bitord.Checked)
                BitArras.Reverse(palbits);

            BitArras arraspal = new BitArras(palbits);

            sz = arraspal.ReadBytes(15, false);
            Console.WriteLine(BitConverter.ToString(sz));

            if (revbyt.Checked)
                BitArras.ReverseByteArray(sz);

            for (int i = 0; i < 15; i++)
            {
                sz[i] = Form1.findPalIndex(sz[i]);
                Console.WriteLine(sz[i].ToString());
            }

            for (int i = 0; i < trackBar1.Value; i++)
                sz = rot16(sz);

            String ColorPalette = UTF8Encoding.UTF8.GetString(sz);

            if (shuffleBox.Checked)
                Shuffle(sz);

            List<int> cols = new List<int>();
            for (int i = 0; i < sz.Length; i++)
            {
                cols.Add((int)(Color.FromArgb(pallet_r[sz[i]], pallet_g[sz[i]], pallet_b[sz[i]]).GetBrightness()));
            }

            // Array.Sort(cols.ToArray(), sz);
            //  Array.Reverse(sz);



            Console.WriteLine(BitConverter.ToString(sz));
            bar.BlowBits(8);

            byte[] panelType = (bar.ReadBytes(1, true));

            byte ptype = panelType[0];
            Console.WriteLine("Panel Type: " + ptype);

            bar.BlowBits(16);

            //  bar.BlowBits(40);

            int leftData = ((bar.getlen() - 0) - bar.getpos()) - 16
                ;

            Console.WriteLine(leftData.ToString()+"@@@");

            byte[] habytes = bar.createHalfByteArray(leftData / 8);



            Console.WriteLine(habytes.Length.ToString());


            //sz = bar.ReadBytes(leftData/8, true);

            //BitArras.ReverseByteArray(sz);

            //Console.WriteLine(sz[0] );
            //Console.WriteLine();



            // String ImgData = UTF8Encoding.UTF8.GetString(sz);

            Color[] colors =
       new Color[]{
       Color.Blue,
       Color.OrangeRed,
       Color.Orange,
       Color.Tan,
       Color.Lavender,
       Color.Magenta,
       Color.Brown,
       Color.Yellow,
       Color.Indigo,
       Color.Blue,
       Color.ForestGreen,
       Color.Lime,
       Color.White,
       Color.SkyBlue,
       Color.Cyan,
       Color.Green
       };
            if (pairswop.Checked)
                PairSwap(sz);

            // byte tmp = sz[1];
            // sz[1] = sz[7];
            // sz[7] = tmp;

            Bitmap pal = new Bitmap(15, 2);
            groupBox1.Controls.Clear();
            for (int i = 0; i < 15; i++)
            {
                Color me = Color.FromArgb(pallet_r[sz[i]], pallet_g[sz[i]], pallet_b[sz[i]]);

                pal.SetPixel(i, 0, Color.FromArgb(pallet_r[sz[i]], pallet_g[sz[i]], pallet_b[sz[i]]));
                Button newb = new Button();

                newb.Left = 20 * i;
                newb.Top = 20;
                newb.Width = 15;

                newb.BackColor = me;
                groupBox1.Controls.Add(newb);
            }

            pictureBox2.Image = pal;

            int tms = 0;
            Bitmap pic = new Bitmap(32, 32);
            int off = 0;
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    //pic.SetPixel(j, i, colors[habytes[off]]);
                    //if (habytes[off] < 15)
                    // habytes[off] -= 1;

                    // if (habytes[off] ==255)
                    //     habytes[off] = 13;

                    byte hab = habytes[off];

                    byte uiop = transfromsilly(hab);

                    int habi = (int)hab;


                    byte bty = sz[hab];
                    // MessageBox.Show(bty.ToString());
                    int colindex = sz[uiop];

                    //  Console.WriteLine(colindex);

                    // if (colindex > 128)
                    //    colindex = colindex % 128;
                    tms++;
                    off += 1;
                    pic.SetPixel(j, i, Color.FromArgb(pallet_r[colindex], pallet_g[colindex], pallet_b[colindex]));

                }
            }

            pictureBox1.Image = pic;

            if (printRaw.Checked)
                Console.WriteLine(BitConverter.ToString(habytes));

            br.Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
            button1_Click(null, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }

        private void revbyt_CheckedChanged(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void bitord_CheckedChanged(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void pairswop_CheckedChanged(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            simple = new SimplePattern();

            NewLeafBitmap nlb = new NewLeafBitmap();
            nlb.falseColor = new Bitmap(32, 32);

            nlb.filepal = new byte[15];
            //BitmapEditor ed = new BitmapEditor();
            //ed.nlbit = nlb;
            //ed.refreshPalette();
           // ed.ShowDialog();

            nombo.Text = simple.designName;
            authbox.Text = simple.creatorName;
            citybox.Text = simple.cityName;

            button5.Enabled = true;

            pictureBox1.Image = nlb.getTrueColorBitmap();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
             
                if (!Clipboard.ContainsImage())
                {
                    MessageBox.Show("No image on the clipboard!");
                    return;
                }
                Stream fs;


                  fs = QRFromClipboard();
                 // fs = File.Open("C:/DATASETS/serialize.txt", FileMode.Open);
            
 
                simple = new SimplePattern(fs);

                nombo.Text = simple.designName;
                authbox.Text = simple.creatorName;
                citybox.Text = simple.cityName;

                button5.Enabled = true;

                pictureBox1.Image = simple.design.getTrueColorBitmap();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (simple != null)
            {
                BitmapEditor ed = new BitmapEditor();
                ed.nlbit = simple.design;
                ed.refreshPalette();
                ed.ShowDialog();

                simple.design = ed.nlbit;
                pictureBox1.Image = simple.design.getTrueColorBitmap();
            }

        }



        private void button6_Click(object sender, EventArgs e)
        {
           // simple.designName = nombo.Text;
            //simple.creatorName = authbox.Text;
            //simple.cityName = citybox.Text;
           Bitmap bbb = simple.serializePattern();
           bbb.Save("C:/DATASETS/carp.png");
           pictureBox1.Image = bbb;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }
    }


    public class BitArras
    {
        int pos = 0;
        BitArray inner;

        public int getpos()
        {
            return pos;
        }

        public static byte reverseOctet(byte b)
        {
            BitArray bb = new BitArray(new byte[] { b });
            BitArray pp = new BitArray(8);
            int j = 7;

            for (int i = 0; i < 8; i++)
            {
                pp[j] = bb[i];
                j--;
            }

            byte[] outbyte = new byte[1];
            pp.CopyTo(outbyte, 0);

            return outbyte[0];
        }

        public int getlen()
        {
            return inner.Length;
        }

        public byte[] createHalfByteArray(int bytesn)
        {
            byte[] barr = new byte[bytesn*2];

            for (int i = 0; i < barr.Length; i+=2)
            {
                byte b = (byte)read4Bits();
                barr[i] =  (byte)read4Bits();
                barr[i + 1] = b;
            }

            return barr;
        }

        public byte read4Bits()
        {
            int one, two, three, four;
            int sum = 0; 

            one =  inner[pos]?1:0;
            two = inner[pos + 1] ? 1 : 0;
            three = inner[pos + 2] ? 1 : 0;
            four = inner[pos + 3] ? 1 : 0;

            sum += one * 1;
            sum += two * 2;
            sum += three * 4;
            sum += four * 8;

            pos += 4;

            return (byte)Math.Abs((sbyte)sum);
        }

        public static BitArray FromByteArray(byte[] barg, bool rev)
        {
            int octetLen = barg.Length;
            BitArray barr = new BitArray(octetLen*8);

            for (int i = 0; i < octetLen; i++)
            {
                BitArray inter = new BitArray(new byte[] {barg[i]});
                if (rev)
                BitArras.Reverse(inter);

                for (int j = 0; j < 8; j++)
                {
                    int ind = (i * 8) + j;
                    barr[ind] = inter[j];
                   
                }
            }

            return barr;

            

        }

        public String strBits(int n)
        {
            StringBuilder sb = new StringBuilder(n);
            BitArray inter = peekbits(n);
            for (int i = 0; i < n; i++)
            {
                if ((i % 8 == 0) && i > 0)
                    sb.Append('-');
                if (inter[i])
                    sb.Append("1");
                else
                    sb.Append("0");
            
                    
            }
            return sb.ToString();
        }

        public BitArras(BitArray par)
        {
            inner = Subspace(par,0,par.Length);
            pos = 0;
        }

        public static void ReverseByteArray(byte[] array)
        {

            int length = array.Length;
            int mid = (length / 2);

            for (int i = 0; i < mid; i++)
            {
                byte bit = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = bit;
            }
        }

        public static void Reverse(BitArray array)
        {
            int length = array.Length;
            int mid = (length / 2);

            for (int i = 0; i < mid; i++)
            {
                bool bit = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = bit;
            }
        }

        public byte[] ReadBytes(int n, bool rev)
        {
            BitArray urBytes = Subspace(inner, pos, 8*n);
            byte[] byarr = new byte[n];
            
            pos += 8 * n;
            if (rev)
                BitArras.Reverse(urBytes);
            urBytes.CopyTo(byarr, 0);
            return byarr;
        }

        public Int16 ReadInt16()
        {
            BitArray urInt = Subspace(inner, pos, 16);
            pos += 16;
            byte[] byarr = new byte[2];
            urInt.CopyTo(byarr,0);
            return BitConverter.ToInt16(byarr,0);
        }

        public void BlowBits(int n)
        {
            pos += n;
        }

        public BitArray readBits(int n)
        {
            if ((n + pos) < this.inner.Length)
            {
                return Subspace(this.inner, pos, n);
            }
                return new BitArray(0);
        }

        public BitArray peekbits(int n)
        {
      
            return BitArras.Subspace(this.inner, pos, n);
        }

        public static BitArray Subspace(BitArray ba, int start, int length)
        {
            int slen = length;
            BitArray ba2 = new BitArray(slen);

            for (int i = 0; i < length; i++)
            {
                ba2[i] = ba[start+i];
            }

            return ba2;
        }
    }

    public class TextBoxStreamWriter : TextWriter
    {
        TextBox _output = null;

        public TextBoxStreamWriter(TextBox output)
        {
            _output = output;
        }

        public override void Write(char value)
        {
            base.Write(value);
            _output.AppendText(value.ToString()); // When character data is written, append it to the text box.
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }

}
