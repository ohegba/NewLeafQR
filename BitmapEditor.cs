using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewLeafQR
{
    public partial class BitmapEditor : Form
    {
        public _8BitCanvas canv;
        public NewLeafBitmap nlbit;

        public Graphics exposed;

        public Brush[] fakeBrush = new Brush[]
        {
            Brushes.AliceBlue,
            Brushes.AntiqueWhite,
            Brushes.Aqua,
            Brushes.Aquamarine,
            Brushes.Azure,
            Brushes.Beige,
            Brushes.Bisque,
            Brushes.BlanchedAlmond,
            Brushes.BurlyWood,
            Brushes.CadetBlue,
            Brushes.Chartreuse,
            Brushes.Chocolate,
            Brushes.Coral,
            Brushes.Crimson,
            Brushes.Cyan,
            Brushes.DarkTurquoise,
            Brushes.HotPink
        };

        int brushsize = 4;

        public int[] fakeBrushColors;

        public void prepareFakeBrushColors()
        {
            fakeBrushColors = new int[fakeBrush.Length];
            for (int i = 0; i < fakeBrush.Length; i++)
            {
                Pen fakePen = new Pen(fakeBrush[i]);
                fakeBrushColors[i] = fakePen.Color.ToArgb();
            }
        }

        public void replaceFakeBrushWithRealBrushColors()
        {
            for (int i = 0; i < 32; i ++)
                for (int j = 0; j < 32; j++)
                {
                    int comp = nlbit.falseColor.GetPixel(i, j).ToArgb();
                    for (int c = 0; c < fakeBrushColors.Length; c++)
                    {
                        if (fakeBrushColors[c] == comp)
                            nlbit.falseColor.SetPixel(i, j, Color.FromArgb(c));
                    }
                }
        }

        public int[] fbARGB;

        public int toolid = 0;

        public const int TOOL_PENCIL = 0;
        public const int TOOL_CLEAR = 1;

        public byte brushindex = 0;

        public Pen curPen;
        public Brush curBrush;

        public BitmapEditor()
        {
            InitializeComponent();

            prepareFakeBrushColors();

            canv = _8BitCanvas1;

            curBrush = fakeBrush[0];
            curPen = new Pen(curBrush);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void _8BitCanvas1_Click(object sender, EventArgs e)
        {
            
        }

        private void x32OctreeQuantizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = importDialog.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                Image test = Image.FromFile(importDialog.FileName);
                NewLeafBitmap tortuga = NewLeafBitmap.NLBfromBitmap((Bitmap)test);

                nlbit = tortuga;

                this.canv.setCanvasImage(tortuga.getTrueColorBitmap());
                pictureBox1.Image = canv.canvImage;
                exposed = Graphics.FromImage(nlbit.falseColor);
                refreshPalette();

            }
            

            
        }

        private void _8BitCanvas1_MouseMove(object sender, MouseEventArgs e)
        {
            if (nlbit == null)
                return;

            canv.lastClickX = e.X / canv.sidew;
            canv.lastClickY = e.Y / canv.sideh;

            

            if (toolid == TOOL_CLEAR && e.X >= 0 && e.X < canv.Width && e.Y >= 0 && e.Y < canv.Height)
                using (Graphics g = canv.CreateGraphics())
                {
                    g.DrawEllipse(Pens.Black, (e.X - (brushsize * 4)), (e.Y - (brushsize * 4)), brushsize*8, brushsize*8);
                    canv.Refresh();
                }


            int lx = canv.lastClickX;
            int ly = canv.lastClickY;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {

                

          

                if (toolid == TOOL_PENCIL)
                {
                    if (lx >= 0 && lx < 32 && ly >= 0 && ly < 32)
                        nlbit.falseColor.SetPixel(lx, ly, Color.FromArgb(brushindex));
                }



                if (toolid == TOOL_CLEAR)
                {
               
                    

                    if (curPen != null)
                    {
                        //nlbit.falseColor.SetPixel(lx, ly, Color.FromArgb(brushindex));
                      
                        using (Graphics g = Graphics.FromImage(nlbit.falseColor))
                        {
                            g.FillEllipse(curBrush, lx - (brushsize / 2), ly - (brushsize / 2), brushsize, brushsize);
                            replaceFakeBrushWithRealBrushColors();
                           
                        }
                      
                    }


                }


                this.canv.setCanvasImage(nlbit.getTrueColorBitmap());
                pictureBox1.Image = canv.canvImage;
            }
        }

        private void meep(object sender, MouseEventArgs e)
        {

            
            Button butt = (Button)sender;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
               
                ACColorChooser acog = new ACColorChooser();
                DialogResult dr = acog.ShowDialog();

                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    butt.BackColor = NewLeafBitmap.getTrueColor(acog.chosenColorIndex);
                   
                    nlbit.filepal[(Byte)butt.Tag] = acog.chosenColorIndex;

                    this.canv.setCanvasImage(nlbit.getTrueColorBitmap());
                }

                curBrush = fakeBrush[brushindex];
                curPen = new Pen(curBrush);

                pictureBox1.Refresh();

               
            }


            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                

                
                brushindex = NewLeafBitmap.transtosilly((Byte)butt.Tag);
                //MessageBox.Show(Color.Blue+"!");
                curBrush = fakeBrush[brushindex];
                curPen = new Pen(curBrush);

                 

            }


        }

        public  void refreshPalette()
        {
            groupBox1.Controls.Clear();

            for (byte i = 0; i < 15; i++)
            {
                Color me = NewLeafBitmap.getTrueColor(nlbit.filepal[i]);

                Button newb = new Button();
                newb.Tag = i;
                newb.MouseUp += new MouseEventHandler(meep);
              //  newb.Click += new EventHandler(meep);
                
                newb.Left = 20 * i;
                newb.Top = 20;
                newb.Width = 15;

                newb.BackColor = me;
                groupBox1.Controls.Add(newb);
            }

        }

        private void BitmapEditor_Load(object sender, EventArgs e)
        {
            this.canv.setCanvasImage(nlbit.getTrueColorBitmap());
            pictureBox1.Image = canv.canvImage;
            canv.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            toolid = TOOL_PENCIL;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            toolid = TOOL_CLEAR;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    sb.Append(nlbit.falseColor.GetPixel(i, j).ToArgb() + " ");
                }
                sb.AppendLine();
            }

              MessageBox.Show(sb.ToString());

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            brushsize = trackBar1.Value;
        }
    }
}
