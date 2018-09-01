using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NewLeafQR
{

        public class RGBOctree
        {

            RGBOctreeNode head = null;

            public int leafCount = 0;

            public int colorDepth = 0;

            public List<RGBOctreeNode> leafNodes = new List<RGBOctreeNode>();
            public List<RGBOctreeNode> lastQuantizedPalette = new List<RGBOctreeNode>();

            int treeDepth = 3;

            public class RGBOctreeNode
            {
                public long pixelCount = 0;
                public int level = -1;

                public Int64 r, g, b;

                public RGBOctreeNode[] children;
                public Color color;
                public bool isCulled = false;
                public Boolean isLeaf = false;
                public Boolean decomissioned = false;

                public List<Color> blend = new List<Color>();

                public RGBOctreeNode parent;

                public RGBOctreeNode(int level)
                {
                    this.level = level;
                    children = new RGBOctreeNode[8];
                }


            }

            public RGBOctree(Bitmap bmp, int nColors)
            {

                colorDepth = nColors;
                /*
                // Lock the bitmap's bits.  
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                System.Drawing.Imaging.BitmapData bmpData =
                    bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    bmp.PixelFormat);

                // Get the address of the first line.
                IntPtr ptr = bmpData.Scan0;

                // Declare an array to hold the bytes of the bitmap. 
                int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
                byte[] rgbValues = new byte[bytes];

                // Copy the RGB values into the array.
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);



                // Set every third value to 255. A 24bpp bitmap will look red.   
                //for (int counter =  0; counter < rgbValues.Length; counter += 4)
               //{
                 //   Color readColor = Color.FromArgb(rgbValues[counter], rgbValues[counter +1], rgbValues[counter+2]);
                 //   this.addColorToOctree(readColor);
               //   
               // }*/
                for (int i = 0; i < bmp.Width; i++)
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        Color cccp = bmp.GetPixel(i, j);
                        this.addColorToOctree(cccp);
                    }

                // Console.Beep(300, 300);
                MessageBox.Show("RARR");

                // Copy the RGB values back to the bitmap
                //System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

                // Unlock the bits.
                //bmp.UnlockBits(bmpData);

                leafNodes.Sort(comp);
                leafNodes.Reverse();

                reduceCheck(colorDepth);

                foreach (RGBOctreeNode cur in leafNodes)
                {
                    //  MessageBox.Show(cur.blend.Count+"");
                    // cur.color = Color.FromArgb( (int) (cur.r / cur.pixelCount),  (int) (cur.g / cur.pixelCount),  (int) (cur.b / cur.pixelCount));
                    //  cur.color = avgColor(cur.blend);
                    //   MessageBox.Show(cur.color.R+","+ cur.color.G);
                    // MessageBox.Show(cur.r/cur.pixelCount+"");
                }



                lastQuantizedPalette = new List<RGBOctreeNode>();



                for (int i = 0; i < leafNodes.Count; i++)
                    lastQuantizedPalette.Add(leafNodes[i]);



            }

            public int comp(RGBOctreeNode a, RGBOctreeNode b)
            {
                if (a == null || b == null)
                    return 0;

                if (a.pixelCount > b.pixelCount)
                    return 1;

                if (a.pixelCount < b.pixelCount)
                    return -1;

                return 0;
            }


            public void ReduceChildren(RGBOctreeNode leafParent)
            {
                // MessageBox.Show("TRY");
                if (leafParent == null)
                {
                    MessageBox.Show("FAIL ");
                    return;
                }

                long R, G, B, S, PC;
                R = G = B = S = PC = 0;
                //MessageBox.Show("PRE DEL " + leafNodes.Count);

                List<Color> childColors = new List<Color>();

                for (int i = 0; i < 8; i++)
                {
                    if (leafParent.children[i] != null)
                    {
                        RGBOctreeNode child = leafParent.children[i];
                        leafParent.blend.AddRange(leafParent.children[i].blend);
                        childColors.Add(child.color);
                        R += child.r * child.pixelCount; G += child.g * child.pixelCount; B += child.b * child.pixelCount; S += 1 * child.pixelCount;

                        child.decomissioned = true;
                        //child.isLeaf = false;
                        PC += child.pixelCount;
                        leafParent.color = leafParent.children[i].color;

                        leafNodes.Remove(child);


                    }

                }
                //    MessageBox.Show("POST DEL " + leafNodes.Count);



                leafParent.isLeaf = true;
                leafParent.isCulled = true;
                leafParent.pixelCount = PC;

                leafNodes.Add(leafParent);

                leafParent.r = R;
                leafParent.g = G;
                leafParent.b = B;

                // R /= S; G /= S; B /= S;
                leafParent.color = avgColor(childColors);
                //   MessageBox.Show(leafParent.color.R + "");
                //    MessageBox.Show(avgColor

            }

            public Color chooseClosestPaletteColor(Color c)
            {
                Color champion = lastQuantizedPalette[0].color;
                double minDist = 60000;
                //  MessageBox.Show(lastQuantizedPalette.Count+"");
                for (int i = 0; i < lastQuantizedPalette.Count; i++)
                {
                    double dist = EuclideanRGB(c, lastQuantizedPalette[i].color);
                    //  MessageBox.Show(dist + "");

                    if (dist <= minDist)
                    {
                        //    MessageBox.Show("WI" + i);
                        minDist = dist;
                        champion = lastQuantizedPalette[i].color;
                    }

                }

                return champion;

            }


            public static double EuclideanRGB(Color a, Color b)
            {
                double a_h, a_s, a_b;

                a_h = a.R;
                a_s = a.G;
                a_b = a.B;


                double b_h, b_s, b_b;

                b_h = b.R;
                b_s = b.G;
                b_b = b.B;


                return Math.Sqrt(Math.Pow(a_h - b_h, 2) + Math.Pow(a_s - b_s, 2) + Math.Pow(a_b - b_b, 2));

            }

            public void reduceCheck(int nColors)
            {

                while (leafNodes.Count >= nColors)
                {
                    //  MessageBox.Show(leafNodes.Count+"");
                    int diff = nColors - leafNodes.Count;
                    // MessageBox.Show(leafNodes.Count + " remove");

                    //MessageBox.Show(diff+"");

                    for (int i = 0; i < leafNodes.Count; i++)
                    {

                        if (leafNodes[i].parent != null)
                            ReduceChildren(leafNodes[i].parent);
                        if (leafNodes.Count <= nColors)
                            break;
                    }



                    if (leafNodes.Count <= nColors)
                        break;


                    //  else
                    //     MessageBox.Show(leafNodes[ind].level+"");


                }


                //  MessageBox.Show(leafNodes.Count + "");






            }

            public Color avgColor(List<Color> cli)
            {
                double R, G, B;
                R = G = B = 0;

                for (int i = 0; i < cli.Count; i++)
                {
                    R += cli[i].R;
                    G += cli[i].G;
                    B += cli[i].B;

                }

                R /= cli.Count; G /= cli.Count; B /= cli.Count;

                //  cli.Clear();
                //  cli.Add(Color.FromArgb((int)R, (int)G, (int)B));

                return Color.FromArgb((int)R, (int)G, (int)B);

            }

            public Color RGBtoColor(int r, int g, int b)
            {
                r = Math.Min(255, Math.Max(r, 0));
                g = Math.Min(255, Math.Max(r, 0));
                b = Math.Min(255, Math.Max(r, 0));

                return Color.FromArgb(r, g, b);
            }


            public RGBOctreeNode newLeaf(Color c, RGBOctreeNode cur, int level)
            {


                int whichChild = ColorToIndex(c, 8 - level);

                RGBOctreeNode nextNode = new RGBOctreeNode(level + 1);

                bool newd = false;

                if (cur.isCulled)
                {
                    //   cur.blend.Add(c);
                    //if (Form1.rng.NextDouble() > .90 && cur.blend.Count > 10)
                    //     cur.color = avgColor(cur.blend);
                    return null;
                }

                if (cur.children[whichChild] == null)
                {
                    cur.children[whichChild] = nextNode;
                    newd = true;
                }

                else//if (cur.children[whichChild] != null)
                {
                    nextNode = cur.children[whichChild];
                }

                nextNode.parent = cur;


                if (level + 1 == treeDepth)
                {
                    if (newd)
                    {
                        nextNode.color = c;
                        nextNode.r = c.R;
                        nextNode.g = c.G;
                        nextNode.b = c.B;

                        nextNode.blend = new List<Color>();
                        nextNode.blend.Add(c);
                        nextNode.isLeaf = true;
                        leafCount++;
                        leafNodes.Add(nextNode);
                        //  MessageBox.Show(nextNode.color.R + " " + nextNode.color.G + " " + nextNode.color.B);
                    }

                    nextNode.pixelCount++;

                    return nextNode;
                }


                return newLeaf(c, nextNode, level + 1);


            }

            public RGBOctreeNode addColorToOctree(Color c)
            {
                if (head == null)
                {
                    head = new RGBOctreeNode(0);
                }

                RGBOctreeNode cur = head;
                RGBOctreeNode ret = newLeaf(c, cur, 0);

                if (leafNodes.Count > 160)
                    reduceCheck(colorDepth);

                return ret;


            }

            public int getNthBit(Int32 int32, int n)
            {
                return ((int32 & (1 << n)) != 0) ? 1 : 0;
            }

            public int ColorToIndex(Color c, int n)
            {
                return (4 * getNthBit(c.R, n)) + (2 * getNthBit(c.G, n)) + (1 * getNthBit(c.B, n));
            }


            public Image QuantizeOnLastPalette(Bitmap inn)
            {
                Bitmap bmp = inn;
                /*
                // Lock the bitmap's bits.  
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                System.Drawing.Imaging.BitmapData bmpData =
                    bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    bmp.PixelFormat);

                // Get the address of the first line.
                IntPtr ptr = bmpData.Scan0;

                // Declare an array to hold the bytes of the bitmap. 
                int bytes  = Math.Abs(bmpData.Stride) * bmp.Height;
                byte[] rgbValues = new byte[bytes];

                // Copy the RGB values into the array.
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

           
                // Set every third value to 255. A 24bpp bitmap will look red.   
                for (int counter = 0; counter < rgbValues.Length; counter += 3)
                {
                    Color readColor = Color.FromArgb(rgbValues[counter],rgbValues[counter+1],rgbValues[counter+2]);
                    Color writeColor = chooseClosestPaletteColor(readColor);

                    rgbValues[counter] = writeColor.R;
                    rgbValues[counter + 1] = writeColor.G;
                    rgbValues[counter + 2] = writeColor.B;
                    //double prog = (counter / (float)rgbValues.Length) * 100;
                    //if (prog > 1)
                    //MessageBox.Show(prog+"");
                }

                //MessageBox.Show("DUNN");

                // Copy the RGB values back to the bitmap
                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

                // Unlock the bits.
                bmp.UnlockBits(bmpData);
                */



                for (int i = 0; i < bmp.Width; i++)
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        Color oldpixel = bmp.GetPixel(i, j);
                        Color newpixel = chooseClosestPaletteColor(oldpixel);

                        double dist = EuclideanRGB(oldpixel, newpixel);

                        double distr = newpixel.R - oldpixel.R;
                        double distg = newpixel.G - oldpixel.G;
                        double distb = newpixel.B - oldpixel.B;

                        bmp.SetPixel(i, j, newpixel);
                        /* 
                         if ((i + 1) < bmp.Width)
                         {
                             Color ccc = bmp.GetPixel(i + 1, j);
                             ccc.
                            bmp.SetPixel(i + 1, j, RGBtoColor((int)(ccc.R + (7 / 16.0) * distr), (int)(ccc.G + (7 / 16.0) * distg), (int)(ccc.B + (7 / 16.0) * distb)));

                         }
                         if ((i - 1) > 0 && (j + 1) < bmp.Height)
                         {
                            // Color ccc = bmp.GetPixel(i - 1, j + 1);
                            // bmp.SetPixel(i - 1, j + 1, chooseClosestPaletteColor(  Color.FromArgb((int)(ccc.ToArgb() + (3 / 16.0) * dist)) ));

                             Color ccc = bmp.GetPixel(i - 1, j + 1);
                     bmp.SetPixel(i - 1, j + 1, RGBtoColor((int)(ccc.R + (3 / 16.0) * distr), (int)(ccc.G + (3 / 16.0) * distg), (int)(ccc.B + (3 / 16.0) * distb)));


                         }
                         if ((j + 1) < bmp.Height)
                         {
                             //    bmp.SetPixel(i, j + 1, p.colors[p.findClosestPaletteIndex(Color.FromArgb((int)(bmp.GetPixel(i, j + 1).ToArgb() + (5 / 16.0) * dist)))]);

                             Color ccc = bmp.GetPixel(i, j + 1);
                             bmp.SetPixel(i, j + 1, RGBtoColor((int)(ccc.R + (5 / 16.0) * distr), (int)(ccc.G + (5 / 16.0) * distg), (int)(ccc.B + (5 / 16.0) * distb)));

                    
                         }
                         if ((i + 1) < bmp.Width && (j + 1) < bmp.Height)
                         {
                             Color ccc = bmp.GetPixel(i+1, j + 1);
                             bmp.SetPixel(i + 1, j + 1, RGBtoColor((int)(ccc.R + (1 / 16.0) * distr), (int)(ccc.G + (1 / 16.0) * distg), (int)(ccc.B + (1 / 16.0) * distb)));
                         }
                         */

                    }

                return bmp;


            }

        }
    
}
