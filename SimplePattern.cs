using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using ZXing.QrCode;
using ZXing;
using Gma.QrCodeNet.Encoding.Windows.Render;


namespace NewLeafQR
{
    public class SimplePattern
    {
        public String designName;
        public String creatorName;
        public String cityName;

        public const byte QR_MAGIC = 0x40;
        public const short QR_SIZE = 0x26C;

        public int designType;

        public NewLeafBitmap design;

        private static String ReadUnicodeString(BitArras bar, int bytes)
        {
            byte[] sz = bar.ReadBytes(bytes, true);
            BitArras.ReverseByteArray(sz);
            return  UnicodeEncoding.Unicode.GetString(sz);
        }

        public SimplePattern(Stream fs)
        {
            BinaryReader br = new BinaryReader(fs);

            long blen = fs.Length;
            byte[] barg = new byte[blen];

            barg = br.ReadBytes((int)blen);

            

            BitArray bat = BitArras.FromByteArray(barg, true);
            BitArras bar = new BitArras(bat);

            

           byte[] heed = bar.ReadBytes(1, true);

            if (heed[0] == 0x70)
            {
                bar.ReadBytes(4, true);
            }

            //if (bar.ReadBytes(1, true)[0] != QR_MAGIC)
            //  throw new Exception("This is not a single pattern QR code.\n Pro designs are not yet supported.");
          //  bar.BlowBits(8);
            bar.BlowBits(12);

            designName = ReadUnicodeString(bar, 42);

            bar.BlowBits(16);
  
            creatorName = ReadUnicodeString(bar, 20);
            MessageBox.Show(bar.getpos() + " IAM");
            bar.BlowBits(16);


            cityName = ReadUnicodeString(bar, 18);

            

            bar.BlowBits(32);

            design = new NewLeafBitmap();
            design.readPaletteFromArras(bar);

            bar.BlowBits(16);

            byte[] panelType = (bar.ReadBytes(1, true));
            designType = panelType[0];

            bar.BlowBits(8);

            int leftData = ((bar.getlen() - 0) - bar.getpos()) - 16;
            MessageBox.Show((leftData).ToString());
            byte[] habytes = bar.createHalfByteArray(leftData / 8);
            int counter = 0;
            for (int i = 0; i < 32; i++)
                for (int j = 0; j < 32; j++)
                    design.falseColor.SetPixel(j,i, Color.FromArgb(((int)(habytes[counter++]))));

            FileStream fs2 = new FileStream("C:/DATASETS/QRDUMP", FileMode.Create);
            fs.Seek(0, SeekOrigin.
            Begin);

            fs.CopyTo(fs2);
            fs2.Close();



        }

        public SimplePattern()
        {
            designName = "MyDesign";
            creatorName = "TomNook";
            cityName = "MyTown";

            designType = 0x09;

        }

        private byte[] padBytes(int n)
        {
            return new byte[n];
        }


        public byte[] copyOctetwiseReverse(byte[] a)
        {
            byte[] b = new byte[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                b[i] = BitArras.reverseOctet(a[i]);
            }
            return b;
        }







        private byte calcParity(byte[] data){
    byte parity;

    parity = data[0];
    for (int i=1;i<data.Length;i++)
        parity = (byte) (parity ^ data[i]);//XOR

        return parity;
    }

private static  String ENCORD_NAME = "ISO-8859-1";




        public Bitmap serializePattern()
        {
            MemoryStream ms = new MemoryStream();

            BinaryWriter br = new BinaryWriter(ms);

            

            br.Write(padBytes(0));

            byte[] nameBytes = UnicodeEncoding.Unicode.GetBytes(designName);
            int pad = 41 - nameBytes.Length;

            br.Write(nameBytes);
            if (pad > 0)
            br.Write(padBytes(pad));

            // These are *1 and 2, the first two bytes of the unique id.
            br.Write(padBytes(2));

            byte[] creatorBytes = UnicodeEncoding.Unicode.GetBytes(creatorName);
            pad = 20 - nameBytes.Length;
             
            

            br.Write(creatorBytes);
            if (pad > 0)
            br.Write(padBytes(pad));

            // Special bytes 3 and 4.
            br.Write(padBytes(2));

            byte[] cityBytes = UnicodeEncoding.Unicode.GetBytes(cityName);
       
            pad = 18 - nameBytes.Length;

            br.Write(cityBytes);
            if (pad > 0)
            br.Write(padBytes(pad));

            br.Write(padBytes(4));

            for (int i = 0; i < 15; i++)
            {
                br.Write(new byte[]{NewLeafBitmap.findPalIndexInv(this.design.filepal[i])});
            }

            // Special byte 7.
            br.Write(padBytes(1));

            br.Write(new byte[] { (byte)0x0A });
            br.Write(new byte[] { (byte)0x09 });

            br.Write(padBytes(2));

            

            BitArray halfBytes = new BitArray(1024*4);

            int counter = 0;

            List<byte> blist = new List<byte>();

            for (int i = 0; i < 32; i++)
                for (int j = 0; j < 32; j++)
                {
                    blist.Add((byte)(this.design.falseColor.GetPixel(j,i).ToArgb()));
                   
                }

            int counterR = 0;
            for (int i = 0; i < blist.Count; )
            {
                BitArray halfWord = new BitArray(new byte[] { blist[i] });
                halfBytes[counterR + 4] =     halfWord[0];
                halfBytes[counterR + 5]= halfWord[1];
                halfBytes[counterR + 6] = halfWord[2];
                halfBytes[counterR + 7] = halfWord[3];
                halfWord = new BitArray(new byte[] { blist[i + 1] });
                halfBytes[counterR + 0] = halfWord[0];
                halfBytes[counterR + 1] = halfWord[1];
                halfBytes[counterR + 2] = halfWord[2];
                halfBytes[counterR + 3] = halfWord[3];
                counterR += 8;

                i += 2;
            }



            FileStream fs = File.Open("C:/DATASETS/serialize.txt", FileMode.Create);
           // ms.CopyTo(fs);

            byte[] junk = copyOctetwiseReverse(ms.ToArray());
            BitArray a = new BitArray(junk);

          //  a = a.Append(new BitArray(4));

            BitArray ab = a.Append(halfBytes);

            BitArray header = new BitArray(new byte[]{QR_MAGIC});
            header.Reverse();
            BitArray header2 = new BitArray(new byte[] { 38 });
            header2.Reverse();

            BitArray header3 = new BitArray(4);
            
            header3.Set(1, true);
            header3.Set(1, true);
            header3.Set(0, true);
            header3.Set(0, true);


            //header = header2.Prepend(header);
            

             BitArray all = header.Append(header2);
             all = all.Append(header3);

             all = all.Append(ab);
             
            byte[] omni = all.ToByteArray();

            MessageBox.Show(BitConverter.ToString(omni));

            fs.Write(omni,0,omni.Length);

           // fs.WriteByte(0xEC);
          //  fs.WriteByte(0x11);
           // fs.WriteByte(0xEC);
           // fs.WriteByte(0x11);
            System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");

            QRCodeWriter qrw = new QRCodeWriter();

           // for (int i = 0; i < omni.Length; i++)
           // {
           //     omni[i] = (byte)((sbyte)omni[i]);
           // }

            Dictionary<EncodeHintType, object> h = new Dictionary<EncodeHintType, object>();
            h.Add(ZXing.EncodeHintType.CHARACTER_SET, "CP437");

            h.Add(ZXing.EncodeHintType.DISABLE_ECI, true);
            h.Add(ZXing.EncodeHintType.ERROR_CORRECTION,ZXing.QrCode.Internal.ErrorCorrectionLevel.H);

            ZXing.QrCode.QrCodeEncodingOptions qrec = new QrCodeEncodingOptions();

            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = 188,
                    Width = 188,
                    CharacterSet = "CP437",
                    PureBarcode = true,
                    ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.L,
                    DisableECI = false,
                    
                }
                
                
            };

         
            char[] chars = new char[omni.Length];

            for (int i = 0; i < omni.Length; i++)
            {
                chars[i] = (char) omni[i];
            }


          
            ZXing.Common.BitMatrix bmx =  writer.
               Encode(Encoding.GetEncoding("CP437").GetString(omni));//qrw.encode(new String(), ZXing.BarcodeFormat.QR_CODE, 300, 300);

            //bmx = createBinQRCode(omni, omni.Length, 0);

            fs.Close();

            
            ms.Close();

          //return bmx.ToBitmap();



           ZXing.BarcodeWriter ss= new ZXing.BarcodeWriter();


           ZXing.IBarcodeWriter reader = new BarcodeWriter();

     ZXing.Rendering.BitmapRenderer bmpr = new ZXing.Rendering.BitmapRenderer();
     MessageBox.Show(Encoding.GetEncoding("CP437").GetString(omni).Substring(0, 10));
     return bmpr.Render(bmx, writer.Format,  Encoding.GetEncoding("CP437").GetString(omni));
    
        //   Gma.QrCodeNet.Encoding.QrEncoder hi = new Gma.QrCodeNet.Encoding.QrEncoder();
         //  Gma.QrCodeNet.Encoding.QrCode qr = new Gma.QrCodeNet.Encoding.QrCode();
     //
         
       
         //  hi.ErrorCorrectionLevel = Gma.QrCodeNet.Encoding.ErrorCorrectionLevel.L;
           


         //  hi.TryEncode(omni, out qr);


         //  SVGRenderer svg = new SVGRenderer(new FixedModuleSize(6, QuietZoneModules.Two), new FormColor(Color.Black), new FormColor(Color.White));
       
           // FileStream FSX = new FileStream("C:/DATASETS/QR.svg", FileMode.Create);
            //svg.WriteToStream(qr.Matrix, FSX);
          //  FSX.Close();

         // Bitmap bmp2 = new Bitmap(qr.Matrix.Width, qr.Matrix.Height);
         //  for (int i = 0; i < qr.Matrix.Width; i++)
         //      for (int j = 0; j < qr.Matrix.Height; j++)
         //          bmp2.SetPixel(i, j, qr.Matrix.InternalArray[i, j] ? Color.Black : Color.White);

         //  bmp2.Save("C:/DATASETS/gma");

        //   return bmp2;
        

        }

    }
}
