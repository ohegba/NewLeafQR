namespace NewLeafQR
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.namebox = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pairswop = new System.Windows.Forms.CheckBox();
            this.printRaw = new System.Windows.Forms.CheckBox();
            this.revbyt = new System.Windows.Forms.CheckBox();
            this.bitord = new System.Windows.Forms.CheckBox();
            this.shuffleBox = new System.Windows.Forms.CheckBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nombo = new System.Windows.Forms.TextBox();
            this.citybox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.authbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Decipher";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(11, 326);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(247, 148);
            this.textBox1.TabIndex = 1;
            this.textBox1.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(313, 83);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(202, 207);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(415, 49);
            this.trackBar1.Maximum = 15;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.Visible = false;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(416, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Palette Round Shift";
            this.label1.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(301, 379);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(22, 21);
            this.button2.TabIndex = 5;
            this.button2.Text = "1";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(284, 402);
            this.pictureBox2.MinimumSize = new System.Drawing.Size(179, 40);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(301, 87);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // namebox
            // 
            this.namebox.Location = new System.Drawing.Point(180, 35);
            this.namebox.Name = "namebox";
            this.namebox.Size = new System.Drawing.Size(75, 23);
            this.namebox.TabIndex = 7;
            this.namebox.Text = "button3";
            this.namebox.UseVisualStyleBackColor = true;
            this.namebox.Visible = false;
            this.namebox.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pairswop);
            this.groupBox1.Controls.Add(this.printRaw);
            this.groupBox1.Controls.Add(this.revbyt);
            this.groupBox1.Controls.Add(this.bitord);
            this.groupBox1.Controls.Add(this.shuffleBox);
            this.groupBox1.Location = new System.Drawing.Point(264, 326);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 47);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Palette";
            this.groupBox1.Visible = false;
            // 
            // pairswop
            // 
            this.pairswop.AutoSize = true;
            this.pairswop.Location = new System.Drawing.Point(247, 0);
            this.pairswop.Name = "pairswop";
            this.pairswop.Size = new System.Drawing.Size(74, 17);
            this.pairswop.TabIndex = 21;
            this.pairswop.Text = "Pair Swap";
            this.pairswop.UseVisualStyleBackColor = true;
            this.pairswop.Visible = false;
            this.pairswop.CheckedChanged += new System.EventHandler(this.pairswop_CheckedChanged);
            // 
            // printRaw
            // 
            this.printRaw.AutoSize = true;
            this.printRaw.Location = new System.Drawing.Point(-60, 24);
            this.printRaw.Name = "printRaw";
            this.printRaw.Size = new System.Drawing.Size(76, 17);
            this.printRaw.TabIndex = 9;
            this.printRaw.Text = "Print RAW";
            this.printRaw.UseVisualStyleBackColor = true;
            this.printRaw.Visible = false;
            // 
            // revbyt
            // 
            this.revbyt.AutoSize = true;
            this.revbyt.Location = new System.Drawing.Point(188, 23);
            this.revbyt.Name = "revbyt";
            this.revbyt.Size = new System.Drawing.Size(119, 17);
            this.revbyt.TabIndex = 10;
            this.revbyt.Text = "Reverse Byte Order";
            this.revbyt.UseVisualStyleBackColor = true;
            this.revbyt.Visible = false;
            this.revbyt.CheckedChanged += new System.EventHandler(this.revbyt_CheckedChanged);
            // 
            // bitord
            // 
            this.bitord.AutoSize = true;
            this.bitord.Location = new System.Drawing.Point(76, 24);
            this.bitord.Name = "bitord";
            this.bitord.Size = new System.Drawing.Size(110, 17);
            this.bitord.TabIndex = 11;
            this.bitord.Text = "Reverse Bit Order";
            this.bitord.UseVisualStyleBackColor = true;
            this.bitord.Visible = false;
            this.bitord.CheckedChanged += new System.EventHandler(this.bitord_CheckedChanged);
            // 
            // shuffleBox
            // 
            this.shuffleBox.AutoSize = true;
            this.shuffleBox.Location = new System.Drawing.Point(5, 24);
            this.shuffleBox.Name = "shuffleBox";
            this.shuffleBox.Size = new System.Drawing.Size(59, 17);
            this.shuffleBox.TabIndex = 14;
            this.shuffleBox.Text = "Shuffle";
            this.shuffleBox.UseVisualStyleBackColor = true;
            this.shuffleBox.Visible = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox3.Location = new System.Drawing.Point(12, 78);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(243, 146);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 12;
            this.pictureBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "QR IMAGE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 236);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Design Name";
            // 
            // nombo
            // 
            this.nombo.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombo.Location = new System.Drawing.Point(11, 252);
            this.nombo.MaxLength = 8;
            this.nombo.Name = "nombo";
            this.nombo.Size = new System.Drawing.Size(123, 22);
            this.nombo.TabIndex = 16;
            // 
            // citybox
            // 
            this.citybox.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.citybox.Location = new System.Drawing.Point(11, 290);
            this.citybox.MaxLength = 8;
            this.citybox.Name = "citybox";
            this.citybox.Size = new System.Drawing.Size(123, 22);
            this.citybox.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(155, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Design Author";
            // 
            // authbox
            // 
            this.authbox.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.authbox.Location = new System.Drawing.Point(155, 252);
            this.authbox.MaxLength = 8;
            this.authbox.Name = "authbox";
            this.authbox.Size = new System.Drawing.Size(100, 22);
            this.authbox.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 274);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "City of Creation";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(313, 54);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 22;
            this.button3.Text = "New Image";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(11, 14);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(146, 44);
            this.button4.TabIndex = 23;
            this.button4.Text = "Decipher QR Code On Clipboard";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(376, 296);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 24;
            this.button5.Text = "Edit Image";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(528, 145);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(68, 41);
            this.button6.TabIndex = 25;
            this.button6.Text = "Encode To QR";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(557, 17);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 26;
            this.button7.Text = "button7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 321);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.authbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.citybox);
            this.Controls.Add(this.nombo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.namebox);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "NewLeafQR";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button namebox;
        private System.Windows.Forms.ColorDialog colorDialog1;
        public  System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox printRaw;
        private System.Windows.Forms.CheckBox revbyt;
        private System.Windows.Forms.CheckBox bitord;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox shuffleBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nombo;
        private System.Windows.Forms.TextBox citybox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox authbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox pairswop;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
    }
}

