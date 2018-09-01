using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NewLeafQR
{
    public partial class _8BitCanvas : PictureBox
    {

        public Bitmap canvImage = new Bitmap(32, 32);
        public Boolean enabled = false;

        public int lastClickX = 0;
        public int lastClickY = 0;

        public int sidew = 0, sideh = 0;

        public _8BitCanvas()
        {
            InitializeComponent();
        }

        public void setCanvasImage(Image i)
        {
            canvImage = (Bitmap)i;
            this.Refresh();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // _8BitCanvas
            // 

            this.Name = "_8BitCanvas";
            this.Size = new System.Drawing.Size(358, 325);

            this.Paint += new System.Windows.Forms.PaintEventHandler(this._8BitCanvas_Paint);
            this.ResumeLayout(false);

        }

        private void _8BitCanvas_Load(object sender, EventArgs e)
        {
            
            enabled = true;
            
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

        }

       /* protected override void OnMouseMove(MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                lastClickX = e.X / sidew;
                lastClickY = e.Y / sideh;

              //  if (lastClickX >= 0 && lastClickX < 32 && lastClickY >= 0 && lastClickY < 32)
                 //   nlbmp.SetPixel(lastClickX, lastClickY, Color.Red);
                // Graphics.FromImage(canvImage).DrawRectangle(Pens.Red, lastClickX, lastClickY, 1, 1);


                this.Refresh();
            }
        } */

        private void _8BitCanvas_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.Clear(Color.White);
            //e.Graphics.FillEllipse(Brushes.Black, new Rectangle(50, 50, 20, 20));

            Bitmap bm = canvImage;

            int wid, hig;
            wid = this.Width; hig = this.Height;

            sidew = wid / 32; sideh = hig / 32;

            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(bm.GetPixel(i, j)), i * sidew, j * sideh, sidew, sideh);
                    e.Graphics.DrawRectangle(Pens.Gray, new Rectangle(i * sidew, j * sideh, sidew, sideh));
                }
            }

            e.Graphics.DrawString(lastClickX + "," + lastClickY, SystemFonts.DefaultFont, Brushes.Yellow, 0, 0);
            e.Graphics.DrawString(lastClickX + "," + lastClickY, SystemFonts.DefaultFont, Brushes.Black, 1, 1);



        }
    }
}
