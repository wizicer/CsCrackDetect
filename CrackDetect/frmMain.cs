using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections;

namespace CrackDetect
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        public Image oripic;
        public Image binpic;
        public Image simpic;
        //public Image nowpic;
        public Image[,] fragpic = new Image[2,2];

        struct BestPositionStruct
        {
            public int X;
            public int Y;
            public int Value;
            public BestPositionStruct(int x, int y, int value)
            {
                X = x;
                Y = y;
                Value = value;
            }
        }

        enum JoinType { Horizon, Vertical };

        private Integration integ = new Integration();

        private void frmMain_Load(object sender, EventArgs e)
        {
            //oripic = new Bitmap("crackpic.png");
            //picMain.Image = oripic;
            picMain.Load("crackpic.png");
            oripic = picMain.Image;
            binpic = picMain.Image;
            simpic = picMain.Image;
        }

        private void frmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (s.Length == 0) return;
            //oripic = new Bitmap(s[0]);
            //picMain.Image = oripic;
            picMain.Load(s[0]);
            oripic = picMain.Image;
            binpic = picMain.Image;
            binpic = picMain.Image;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (oripic == null) return;
            btnThinner_Click(sender, e);
            btnJoin_Click(sender, e);
            btnMarkCross_Click(sender, e);
            btnMarkCross_Click(sender, e);
            btnWipeSmallLine_Click(sender, e);
            btnMarkLineEnd_Click(sender, e);
        }

        private void btnThinner_Click(object sender, EventArgs e)
        {
            //Image tmp = Thinner(picMain.Image, oripic.Width, oripic.Height);
            picMain.Image = integ.Thinner(picMain.Image, oripic.Width, oripic.Height, (int)numThinnerTimes.Value);
        }

        private void btnGauss_Click(object sender, EventArgs e)
        {
            //picMain.Image = integ.Gaussion(picMain.Image);
            MessageBox.Show("此方法已废除！");
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            picMain.Image = integ.JoinShortLines(picMain.Image, (int)numShortDistance.Value);
        }

        private void btnWipeSmallLine_Click(object sender, EventArgs e)
        {
            byte[] buff = integ.getBinaryFromImage(picMain.Image);
            integ.MinLineLength = (int)numMinLineDistance.Value;
            integ.WipeOffShortLine(ref buff, picMain.Image.Width, picMain.Image.Height);
            picMain.Image = integ.getImageFromBinary(buff, picMain.Image.Width, picMain.Image.Height);
        }

        private void btnMarkCross_Click(object sender, EventArgs e)
        {
            integ.MarkCross(picMain.Image);
            Bitmap bmp = new Bitmap(picMain.Image);
            for (int i = 0; i < integ.arrCrossList.Count; ++i)
            {
                bmp.SetPixel(integ.arrCrossList[i].X, integ.arrCrossList[i].Y, Color.White);
            }
            picMain.Image = bmp;
        }


        private void picMain_MouseMove(object sender, MouseEventArgs e)
        {
            lblPosition.Text = string.Format("X:{0,3:G}, Y:{1,3:G}", e.X, e.Y);
            lblZoomLevel.Text = string.Format("{0,2:G}%", picMain.Percent * 100);
        }

        private void picMain_MouseLeave(object sender, EventArgs e)
        {
            lblPosition.Text = "准备";
        }

        private void picMain_Click(object sender, EventArgs e)
        {
            picMain.Refresh();
        }

        private void btnIntegration_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(picMain.Image);
            Graphics g = Graphics.FromImage(bmp);
            integ.arrFinalLineList.Clear();
            Pen greenpen = new Pen(new SolidBrush(Color.Green));
            Pen bluepen = new Pen(new SolidBrush(Color.Blue));
            byte[] buff = integ.getBinaryFromImage(bmp);
            for (int i = 0; i < integ.arrEndList.Count; ++i)
            {
                //int k = arrEndList[i].X + arrEndList[i].Y * picMain.Image.Width;
                Point[] r = integ.dfsFindPath(buff, picMain.Image.Width, picMain.Image.Height, integ.arrEndList[i].X, integ.arrEndList[i].Y,/* 0, (int)numMaxLineFluctuate.Value,*/ (int)numPrecision.Value);
                g.DrawRectangle(greenpen, new Rectangle(r[0].X - 1, r[0].Y - 1, 2, 2));
                for (int j = 1; j < r.Length; ++j)
                {
                    g.DrawLine(bluepen, r[j], r[j - 1]);
                    g.DrawRectangle(greenpen, new Rectangle(r[j].X - 1, r[j].Y - 1, 2, 2));
                }
                Integration.MultiLineData mld = new Integration.MultiLineData();
                mld.points = r;
                integ.arrFinalLineList.Add(mld);
            }
            g.Flush();
            g.Dispose();
            picMain.Image = bmp;
        }

        private void btnGetLineWidth_Click(object sender, EventArgs e)
        {
            Image img = binpic;
            Bitmap bmp = new Bitmap(picMain.Image);
            Graphics g = Graphics.FromImage(bmp);
            Pen greenpen = new Pen(new SolidBrush(Color.Green));
            Pen bluepen = new Pen(new SolidBrush(Color.Blue));
            byte[] buff = integ.getBinaryFromImage(img);
            for (int i = 0; i < integ.arrFinalLineList.Count; ++i)
            {
                Integration.MultiLineData mld = integ.arrFinalLineList[i];
                double[] widths = new double[mld.points.Length - 1];
                for (int j = 1; j < mld.points.Length; ++j)
                {
                    widths[j - 1] = integ.getMaxWidthOfLinePoint(buff, img.Width, img.Height, mld.points[j].X, mld.points[j].Y, mld.points[j], mld.points[j - 1]);
                    g.DrawLine(new Pen(new SolidBrush(Color.Blue), (int)widths[j - 1]), mld.points[j], mld.points[j - 1]);
                    g.DrawRectangle(greenpen, new Rectangle(mld.points[j].X - 1, mld.points[j].Y - 1, 2, 2));
                }
                mld.width = widths;
                integ.arrFinalLineList.RemoveAt(i);
                integ.arrFinalLineList.Insert(i, mld);
            }
            g.Flush();
            g.Dispose();
            picMain.Image = bmp;
        }

        private void btnMarkLineEnd_Click(object sender, EventArgs e)
        {
            simpic = picMain.Image;
            Bitmap bmp = new Bitmap(picMain.Image);
            Graphics g = Graphics.FromImage(bmp);
            integ.MarkLineEnds(picMain.Image);
            foreach (Point pt in integ.arrEndList)
            {
                g.DrawRectangle(new Pen(new SolidBrush(Color.Blue)), new Rectangle(pt.X - 1, pt.Y - 1, 2, 2));
            } 
            g.Flush();
            g.Dispose();
            picMain.Image = bmp;

        }

        private void btnNewThinner_Click(object sender, EventArgs e)
        {
            //picMain.Image = integ.newThining(picMain.Image);
            MessageBox.Show("此方法已废除！");
        }

        private void btnBinary_Click(object sender, EventArgs e)
        {
            picMain.Image = integ.Binary(picMain.Image, (int)numBrightThreshold.Value);
            binpic = picMain.Image;
        }

        private void btnGradient_Click(object sender, EventArgs e)
        {
            picMain.Image = integ.getImageFromBinary(integ.GradientBytes(picMain.Image, (int)numGradientLevel.Value, (int)numBrightThreshold.Value), picMain.Image.Width, picMain.Image.Height);
            //picMain.Image = integ.GradientImage(picMain.Image, (int)numGradientLevel.Value, (int)numBrightThreshold.Value);
            binpic = picMain.Image;
        }

        private void btnErosion_Click(object sender, EventArgs e)
        {
            byte[] buffin = integ.getBinaryFromImage(picMain.Image);
            byte[] buffout = (byte[])buffin.Clone();
            integ.Erosion(buffin, ref buffout, picMain.Image.Width, picMain.Image.Height);
            picMain.Image = integ.getImageFromBinary(buffout, picMain.Image.Width, picMain.Image.Height);
            binpic = picMain.Image;
        }

        private void btnDilation_Click(object sender, EventArgs e)
        {
            byte[] buffin = integ.getBinaryFromImage(picMain.Image);
            byte[] buffout = (byte[])buffin.Clone();
            integ.Dilation(buffin, ref buffout, picMain.Image.Width, picMain.Image.Height);
            picMain.Image = integ.getImageFromBinary(buffout, picMain.Image.Width, picMain.Image.Height);
            binpic = picMain.Image;
        }

        private void btnReloadBinaryPic_Click(object sender, EventArgs e)
        {
            picMain.Image = binpic;
        }

        private void btnReloadOriginPic_Click(object sender, EventArgs e)
        {
            picMain.Image = oripic;
        }

        private void btnReloadLast_Click(object sender, EventArgs e)
        {
            picMain.Image = simpic;
        }

        private void btnJoinPicture_Click(object sender, EventArgs e)
        {
            int marginWidth = (int)numMaxWidth.Value;
            int marginHeight = (int)numMaxHeight.Value;
            Bitmap bmp;// = new Bitmap();
            bmp = new Bitmap("CrackPicv0.png");
            bmp.SetResolution(96, 96);
            fragpic[0, 0] = bmp;
            bmp = new Bitmap("CrackPicv1.png");
            bmp.SetResolution(96, 96);
            fragpic[0, 1] = bmp;
            //fragpic[0, 0] = integ.GradientImage(new Bitmap("CrackPicc0.png"), (int)numGradientLevel.Value, (int)numBrightThreshold.Value);
            //fragpic[0, 1] = integ.GradientImage(new Bitmap("CrackPicc1.png"), (int)numGradientLevel.Value, (int)numBrightThreshold.Value);
            picMain.Image = JoinTwoPicture(fragpic[0, 0], fragpic[0, 1], JoinType.Vertical, marginWidth, marginHeight);
        }

        private Image JoinTwoPicture(Image img1, Image img2, JoinType jointype, int marginWidth, int marginHeight)
        {
            //byte[] buff1 = integ.getBinaryFromImage(img1);
            //byte[] buff2 = integ.getBinaryFromImage(img2);
            byte[] buff1 = integ.GradientBytes(img1, (int)numGradientLevel.Value, (int)numBrightThreshold.Value);
            byte[] buff2 = integ.GradientBytes(img2, (int)numGradientLevel.Value, (int)numBrightThreshold.Value);
            int width = fragpic[0, 0].Width;
            int height = fragpic[0, 0].Height;
            List<BestPositionStruct> lstPosition = new List<BestPositionStruct>();
            if (jointype == JoinType.Horizon)
            {
                for (int ox = 1; ox < marginWidth; ++ox)
                {
                    for (int oy = 1 - marginHeight; oy < marginHeight; ++oy)
                    {
                        int left1 = width - ox;
                        int top1 = 0 + (oy > 0 ? oy : 0);
                        int left2 = 0;//mw - ox;
                        int top2 = 0 + (oy < 0 ? -oy : 0);
                        int w = ox;
                        int h = height - (oy > 0 ? oy : -oy);
                        int dcount = 0;
                        //int wcount = 0;
                        int bcount = 0;//black count
                        for (int x = 0; x < w; ++x)
                        {
                            for (int y = 0; y < h; ++y)
                            {
                                byte p1 = buff1[(y + top1) * width + x + left1];
                                byte p2 = buff2[(y + top2) * width + x + left2];
                                if (p1 != p2) dcount++;
                                //else if (p1 == 0 || p2 == 0) wcount++;
                                if (p1 == 1 || p2 == 1) bcount++;
                            }
                        }
                        if (bcount == 0)
                        {
                            lstPosition.Add(new BestPositionStruct(ox, oy, 100));
                        }
                        else
                        {
                            lstPosition.Add(new BestPositionStruct(ox, oy, dcount * 100 / bcount));
                        }
                    }
                }
            }
            else if (jointype == JoinType.Vertical)
            {
                for (int oy = 1; oy < marginWidth; ++oy)
                {
                    for (int ox = 1 - marginHeight; ox < marginHeight; ++ox)
                    {
                        int left1 = 0 + (ox > 0 ? ox : 0);
                        int top1 = height - oy;
                        int left2 = 0 + (ox < 0 ? -ox : 0);
                        int top2 = 0;
                        int w = width - (ox > 0 ? ox : -ox);
                        int h = oy;
                        //int left1 = width - oy;
                        //int top1 = 0 + (ox > 0 ? ox : 0);
                        //int left2 = 0;//mw - oy;
                        //int top2 = 0 + (ox < 0 ? -ox : 0);
                        //int w = oy;
                        //int h = height - (ox > 0 ? ox : -ox);
                        int dcount = 0;
                        //int wcount = 0;
                        int bcount = 0;
                        for (int x = 0; x < w; ++x)
                        {
                            for (int y = 0; y < h; ++y)
                            {
                                byte p1 = buff1[(y + top1) * width + x + left1];
                                byte p2 = buff2[(y + top2) * width + x + left2];
                                if (p1 != p2) dcount++;
                                //else if (p1 == 0 || p2 == 0) wcount++;
                                if (p1 == 1 || p2 == 1) bcount++;
                            }
                        }
                        if (bcount == 0)
                        {
                            lstPosition.Add(new BestPositionStruct(ox, oy, 100));
                        }
                        else
                        {
                            lstPosition.Add(new BestPositionStruct(ox, oy, dcount * 100 / bcount));
                        }
                    }
                }
            }
            BestPositionStruct[] tmpb = lstPosition.ToArray();
            BestPositionStruct tmpbest = new BestPositionStruct(0, 0, 100);
            foreach (BestPositionStruct bps in tmpb)
            {
                if (bps.Value < tmpbest.Value) tmpbest = bps;
                //if (bps.Value > 50) continue;
                //Console.Write(string.Format("({0},{1}):{2}\n", bps.X, bps.Y, bps.Value));
                //if (bps.Value == 0) Console.WriteLine("====================\n");
            }
            Bitmap bmp;// = new Bitmap(2 * width - tmpbest.X, height + Math.Abs(tmpbest.Y));
            if (jointype == JoinType.Horizon)
            {
                bmp = new Bitmap(2 * width - tmpbest.X, height + Math.Abs(tmpbest.Y));
            }
            else
            {
                bmp = new Bitmap(width + Math.Abs(tmpbest.X), 2 * height - tmpbest.Y);
            }
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            if (jointype == JoinType.Horizon)
            {
                g.DrawImageUnscaled(img1, 0, tmpbest.Y > 0 ? 0 : -tmpbest.Y);
                g.DrawImageUnscaled(img2, width - tmpbest.X, tmpbest.Y < 0 ? 0 : tmpbest.Y);
            }
            else if (jointype == JoinType.Vertical)
            {
                g.DrawImageUnscaled(img1, tmpbest.X > 0 ? 0 : -tmpbest.X, 0);
                g.DrawImageUnscaled(img2, tmpbest.X < 0 ? 0 : tmpbest.X, height - tmpbest.Y);
            }
            g.Flush();
            g.Dispose();
            return bmp;
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            string str = "";// = string.Format("{0},{1},0", integ.arrFinalLineList[i].points[j].X, integ.arrFinalLineList[i].points[j].Y);
            for (int i = 0; i < integ.arrFinalLineList.Count; ++i)
            {
                Integration.MultiLineData mld = integ.arrFinalLineList[i];
                if (mld.points.Length <= 1) continue;
                //str += string.Format("{0},{1},{2},", mld.points[0].X, mld.points[0].Y);
                for (int j = 0; j < mld.points.Length - 1; ++j)
                {
                    str += string.Format("{0},{1},{2},", mld.points[j].X, mld.points[j].Y, mld.width[j]);
                }
                str += string.Format("{0},{1},0|", mld.points[mld.points.Length - 1].X, mld.points[mld.points.Length - 1].Y);
                //str = str.Remove(str.Length - 1) + "|";
            }
            str = str.Remove(str.Length - 1);
            Clipboard.SetText(str);
            MessageBox.Show(string.Format("字符串为\"{0}\"\r\n已放入剪贴板！", str));
        }

    }
}
