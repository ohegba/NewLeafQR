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
    public partial class ACColorChooser : Form
    {
        public byte chosenColorIndex;
        private Font smallFont;

        public ACColorChooser()
        {
            InitializeComponent();
            smallFont = new Font("MS Dialog", 5, FontStyle.Regular);
            button1.Font = new Font("MS Dialog", 7, FontStyle.Italic);
        }

        private void ACColorChooser_Load(object sender, EventArgs e)
        {
            groupBox1.Controls.Clear();

            int n_pallet = 159;
 int rect_size = 22;
 int size_x = rect_size*17;
 int size_y = rect_size*19;

              int big_x = 0;
  int big_y = 0;

  int small_x = 0;
  int small_y = 0;

  for(int i=0; i < n_pallet; i++){
    if(i < 144){
      big_x = (int)(i/9);
      big_x = big_x % 4; 
      small_x = (int)(i % 3);
      big_y = (int)(i/36);
      small_y = i % 9;
      small_y = (int)(small_y /3);
    }else{
      big_x = 0;
      big_y = 4;
      small_x = i-144;
      small_y = 0;
    }
      Button nova = new Button();
      nova.BackColor = NewLeafBitmap.getTrueColor((byte)i);
      nova.Text = i.ToString();
      nova.Font = smallFont;
      nova.Click += new EventHandler(tagColor);

      nova.SetBounds(big_x*rect_size*4 + (small_x+1)*rect_size, big_y*rect_size*4 + (small_y+1)*rect_size,
          rect_size,rect_size);

      groupBox1.Controls.Add(nova);
      
  }
        }

        private void tagColor(object sender, EventArgs e)
        {
            chosenColorIndex = Byte.Parse(((Button)sender).Text);
            panel1.BackColor = NewLeafBitmap.getTrueColor(this.chosenColorIndex);

            label2.Text = panel1.BackColor.Name.ToUpper();
            label3.Text = " R: " +panel1.BackColor.R
                +         " G: " +panel1.BackColor.G
                +         " B: " +panel1.BackColor.B;

            label4.Text = " H: " + Math.Round(panel1.BackColor.GetHue(), 2).ToString()
                + " S: " + Math.Round(panel1.BackColor.GetSaturation(), 2).ToString()
                + " B: " + Math.Round(panel1.BackColor.GetBrightness(), 2).ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
