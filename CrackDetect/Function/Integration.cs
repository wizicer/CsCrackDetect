using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CrackDetect
{
    class Integration
    {
        #region 枚举、结构及变量
        public List<Point> arrEndList = new List<Point>();
        public List<Point> arrCrossList = new List<Point>();

        public struct MultiLineData
        {
            public Point[] points;// = new List<Point>();
            public double[] width;
        }
        public List<MultiLineData> arrFinalLineList = new List<MultiLineData>();

        public enum MapData { Black = 1, None = 0, Marked = 2 };
        public int MinLineLength = 20;
        #endregion

        #region 膨胀腐蚀
        /// <summary>
        /// 二值腐蚀
        /// </summary>
        public void Erosion(ref byte[] data, int x, int y, int width, int height, int w, int h)
        {
            byte[] dataout = (byte[])data.Clone();
            Erosion(data, ref dataout, x, y, width, height, w, h);
            data = (byte[])dataout.Clone();
        }

        public void Erosion(byte[] datain, ref byte[] dataout, int w, int h)
        {
            Erosion(datain, ref dataout, 0, 0, w, h, w, h);
        }

        public void Erosion(byte[] datain, ref byte[] dataout, int x, int y, int width, int height, int w, int h)
        {
            for (int j = x + 1; j < x + width - 1; j++)
            {
                for (int i = y + 1; i < y + height - 1; i++)
                {
                    int k = (h - i - 1) * w + j;
                    int mn = 0;
                    mn += datain[k - 1] == (byte)MapData.Black ? 1 : 0;
                    mn += datain[k + 1] == (byte)MapData.Black ? 1 : 0;
                    mn += datain[k + 0] == (byte)MapData.Black ? 1 : 0;
                    mn += datain[k + w + 0] == (byte)MapData.Black ? 1 : 0;
                    mn += datain[k + w - 1] == (byte)MapData.Black ? 1 : 0;
                    mn += datain[k + w + 1] == (byte)MapData.Black ? 1 : 0;
                    mn += datain[k - w - 1] == (byte)MapData.Black ? 1 : 0;
                    mn += datain[k - w + 1] == (byte)MapData.Black ? 1 : 0;
                    mn += datain[k - w + 0] == (byte)MapData.Black ? 1 : 0;

                    if (mn > 7)
                    {
                        dataout[k] = (byte)MapData.Black;
                    }
                    else
                    {
                        dataout[k] = (byte)MapData.None;
                    }
                }
            }
        }

        /// <summary>
        /// 二值膨胀
        /// </summary>
        public void Dilation(ref byte[] data, int x, int y, int width, int height, int w, int h)
        {
            byte[] dataout = (byte[])data.Clone();
            Dilation(data, ref dataout, x, y, width, height, w, h);
            data = (byte[])dataout.Clone();
        }

        public void Dilation(byte[] datain, ref byte[] dataout, int w, int h)
        {
            Dilation(datain, ref dataout, 0, 0, w, h, w, h);
        }

        public void Dilation(byte[] datain, ref byte[] dataout, int x, int y, int width, int height, int w, int h)
        {
            for (int j = x + 1; j < x + width - 1; j++)
            {
                for (int i = y + 1; i < y + height - 1; i++)
                {
                    int k = (h - i - 1) * w + j;
                    if (datain[k] == (byte)MapData.Black)
                    {
                        dataout[k - 1] = (byte)MapData.Black;
                        dataout[k + 1] = (byte)MapData.Black;
                        dataout[k + 0] = (byte)MapData.Black;
                        dataout[k + w - 1] = (byte)MapData.Black;
                        dataout[k + w + 1] = (byte)MapData.Black;
                        dataout[k + w + 0] = (byte)MapData.Black;
                        dataout[k - w - 1] = (byte)MapData.Black;
                        dataout[k - w + 1] = (byte)MapData.Black;
                        dataout[k - w + 0] = (byte)MapData.Black;
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 宽度拟合，寻找拟合线段周围的最宽宽度
        /// </summary>
        /// <param name="data">图像的二进制数据</param>
        /// <param name="w">图像宽度</param>
        /// <param name="h">图像高度</param>
        /// <param name="x">拟合出发点X坐标</param>
        /// <param name="y">拟合出发点Y坐标</param>
        /// <param name="ln1">拟合直线一端坐标</param>
        /// <param name="ln2">拟合直线另一端坐标</param>
        /// <returns>该拟合直线最大宽度</returns>
        public double getMaxWidthOfLinePoint(byte[] data, int w, int h, int x, int y, Point ln1, Point ln2)
        {
            byte tgtvalue = 0;
            byte orivalue = 1;
            List<int> nodes = new List<int>();
            int hd = -1;
            double tempmax = double.MinValue;
            int k = x + y * w;
            nodes.Add(k);
            do
            {
                ++hd;
                data[nodes[hd]] = tgtvalue;
                int ty = nodes[hd];
                int tx = ty % w;
                ty = (ty - tx) / w;
                double tmp = PointToLineDistance(new Point(tx, ty), ln1, ln2);
                if (tmp > tempmax) tempmax = tmp;
                if (nodes[hd] + 1 < w * h && data[nodes[hd] + 1] == orivalue) { nodes.Add(nodes[hd] + 1); LineWidthCheck(nodes, w, ln1, ln2); }
                if (nodes[hd] + w < w * h && data[nodes[hd] + w] == orivalue) { nodes.Add(nodes[hd] + w); LineWidthCheck(nodes, w, ln1, ln2); }
                if (nodes[hd] - w >= 0 && data[nodes[hd] - w] == orivalue) { nodes.Add(nodes[hd] - w); LineWidthCheck(nodes, w, ln1, ln2); }
                if (nodes[hd] - 1 >= 0 && data[nodes[hd] - 1] == orivalue) { nodes.Add(nodes[hd] - 1); LineWidthCheck(nodes, w, ln1, ln2); }
                if (nodes[hd] + w + 1 < w * h && data[nodes[hd] + w + 1] == orivalue) { nodes.Add(nodes[hd] + w + 1); LineWidthCheck(nodes, w, ln1, ln2); }
                if (nodes[hd] + w - 1 < w * h && data[nodes[hd] + w - 1] == orivalue) { nodes.Add(nodes[hd] + w - 1); LineWidthCheck(nodes, w, ln1, ln2); }
                if (nodes[hd] - w + 1 >= 0 && data[nodes[hd] - w + 1] == orivalue) { nodes.Add(nodes[hd] - w + 1); LineWidthCheck(nodes, w, ln1, ln2); }
                if (nodes[hd] - w - 1 >= 0 && data[nodes[hd] - w - 1] == orivalue) { nodes.Add(nodes[hd] - w - 1); LineWidthCheck(nodes, w, ln1, ln2); }

            } while (hd < nodes.Count - 1);
            return tempmax;
        }

        /// <summary>
        /// 拟合直线宽度使用的检查函数
        /// </summary>
        private void LineWidthCheck(List<int> nodes, int w, Point ln1, Point ln2)
        {
            int i;
            int tl = nodes.Count - 1;
            for (i = 0; i < tl; ++i)
            {
                if (nodes[i] == nodes[tl]) break;
            }
            if (i < tl && nodes[i] == nodes[tl])
            {
                nodes.RemoveAt(tl);
            }
            else
            {
                int ty = nodes[tl];
                int tx = ty % w;
                ty = (ty - tx) / w;
                if (!isBetweenTwoPointLine(new Point(tx, ty), ln1, ln2))
                {
                    nodes.RemoveAt(tl);
                }
            }
        }

        /// <summary>
        /// 判定点是否在两条指定平行线之间
        /// </summary>
        /// <param name="pt">点坐标</param>
        /// <param name="ln1">一条平行线的坐标</param>
        /// <param name="ln2">另一条平行线的坐标</param>
        /// <returns>是否</returns>
        public bool isBetweenTwoPointLine(Point pt, Point ln1, Point ln2)
        {
            Point nln1 = new Point();
            Point nln2 = new Point();
            Point lnc = new Point((ln1.X + ln2.X) / 2, (ln1.Y + ln2.Y) / 2);
            int offset = 5;
            if (ln1.Y == ln2.Y)
            {
                nln1.Y = ln1.Y;
                nln2.Y = ln2.Y;
                nln1.X = lnc.X - offset;
                nln2.X = lnc.X + offset;
            }
            else
            {
                double k = (double)(ln1.X - ln2.X) / (ln2.Y - ln1.Y);
                double c = lnc.Y - k * lnc.X;
                nln1.X = lnc.X - offset;
                nln2.X = lnc.X + offset;
                nln1.Y = (int)(k * nln1.X + c);
                nln2.Y = (int)(k * nln2.X + c);
            }
            double oridis = Math.Sqrt((ln1.X - ln2.X) * (ln1.X - ln2.X) + (ln1.Y - ln2.Y) * (ln1.Y - ln2.Y)) / 2;
            double nowdis = PointToLineDistance(pt, nln1, nln2);
            if (nowdis > oridis)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 图像二值化
        /// </summary>
        /// <param name="img">输入图像</param>
        /// <param name="brightThreshole">亮度阈值</param>
        /// <returns>输出图像</returns>
        public Image Binary(Image img, int brightThreshole)
        {
            Color ColorOrigin = new Color();
            double Red, Green, Blue, Y;
            Bitmap Bmp1 = new Bitmap(img);

            //Graphics g = pictureBox1.CreateGraphics();
            for (int i = 0; i <= img.Width - 1; i++)
            {
                for (int j = 0; j <= img.Height - 1; j++)
                {
                    ColorOrigin = Bmp1.GetPixel(i, j);
                    Red = ColorOrigin.R;
                    Green = ColorOrigin.G;
                    Blue = ColorOrigin.B;
                    Y = 0.59 * Red + 0.3 * Green + 0.11 * Blue;
                    if (Y > brightThreshole)
                    {
                        Color ColorProcessed = Color.FromArgb(255, 255, 255);
                        Bmp1.SetPixel(i, j, ColorProcessed);
                    }
                    if (Y <= brightThreshole)
                    {
                        Color ColorProcessed = Color.FromArgb(0, 0, 0);
                        Bmp1.SetPixel(i, j, ColorProcessed);
                    }

                }
            }
            return Bmp1;

        }

        private Image abandonedThining(Image img)
        {
            int[,] a = new int[img.Width, img.Height];
            int[,] b = new int[img.Width, img.Height];
            int r;
            int n = 0;
            int s = 0;
            Color c = new Color();
            Bitmap box1 = new Bitmap(img);
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    c = box1.GetPixel(i, j);
                    r = c.R;
                    if (r == 0)
                        a[i, j] = 1;
                    else
                    {
                        a[i, j] = 0;
                        box1.SetPixel(i, j, Color.White);//找出所有亮点。
                    }
                }
            }
            ///开始细化运算。
            for (int t = 0; t < 1; t++)
            {
                for (int i = 1; i < img.Width - 1; i++)
                {
                    for (int j = 1; j < img.Height - 1; j++)
                    {
                        n = a[i - 1, j - 1] + a[i - 1, j] + a[i - 1, j + 1] + a[i, j - 1] + a[i, j + 1] + a[i + 1, j - 1] + a[i + 1, j] + a[i + 1, j + 1];//8 neighbers;
                        if ((a[i, j - 1] == 0 && a[i + 1, j - 1] == 1) ^ (a[i + 1, j - 1] == 0 && a[i + 1, j] == 1) ^ (a[i + 1, j] == 0 && a[i + 1, j + 1] == 1) ^ (a[i + 1, j + 1] == 0 && a[i, j + 1] == 1)
                          ^ (a[i, j + 1] == 0 && a[i - 1, j + 1] == 1) ^ (a[i - 1, j + 1] == 0 && a[i - 1, j] == 1) ^ (a[i - 1, j] == 0 && a[i - 1, j - 1] == 1) ^ (a[i - 1, j - 1] == 0 && a[i, j - 1] == 1))
                        { s = 1; }
                        if (a[i, j] == 1 && n >= 2 && n <= 6 && a[i, j - 1] * a[i + 1, j] * a[i, j + 1] == 0 && a[i + 1, j] * a[i, j + 1] * a[i, j - 1] == 0)
                        { b[i, j] = 2; }
                    }
                }
                for (int i = 0; i < img.Width - 1; i++)
                {
                    for (int j = 0; j < img.Height - 1; j++)
                    {
                        if (b[i, j] == 2 && s == 1)
                        {
                            box1.SetPixel(i, j, Color.White);
                            a[i, j] = 0;
                        }
                    }
                }
                for (int i = 1; i < img.Width - 1; i++)
                {
                    for (int j = 1; j < img.Height - 1; j++)
                    {
                        n = a[i - 1, j - 1] + a[i - 1, j] + a[i - 1, j + 1] + a[i, j - 1] + a[i, j + 1] + a[i + 1, j - 1] + a[i + 1, j] + a[i + 1, j + 1];//8 neighbers;
                        if ((a[i, j - 1] == 0 && a[i + 1, j - 1] == 1) ^ (a[i + 1, j - 1] == 0 && a[i + 1, j] == 1) ^ (a[i + 1, j] == 0 && a[i + 1, j + 1] == 1) ^ (a[i + 1, j + 1] == 0 && a[i, j + 1] == 1)
                          ^ (a[i, j + 1] == 0 && a[i - 1, j + 1] == 1) ^ (a[i - 1, j + 1] == 0 && a[i - 1, j] == 1) ^ (a[i - 1, j] == 0 && a[i - 1, j - 1] == 1) ^ (a[i - 1, j - 1] == 0 && a[i, j - 1] == 1))
                        { s = 1; }
                        if (a[i, j] == 1 && n >= 2 && n <= 6 && a[i, j - 1] * a[i + 1, j] * a[i - 1, j] == 0 && a[i, j - 1] * a[i, j + 1] * a[i - 1, j] == 0)
                        { b[i, j] = 2; }
                    }
                }
                for (int i = 0; i < img.Width - 1; i++)
                {
                    for (int j = 0; j < img.Height - 1; j++)
                    {
                        if (b[i, j] == 2 && s == 1)
                        {
                            box1.SetPixel(i, j, Color.White);
                            a[i, j] = 0;
                        }
                    }
                }

            }
            return box1;
        }


        public byte[] GradientBytes(Image img, int GradientThreshold, int brightThreshold)
        {
            int h = img.Height, w = img.Width;
            MemoryStream ms = new MemoryStream();
            int BPP = 4;
            //if (img.PixelFormat.ToString().IndexOf("32") > -1) BPP = 4;
            Bitmap b = new Bitmap(img);
            b.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] bout = new byte[h * w];
            byte[] buffers = ms.ToArray();
            for (int j = 0; j < w - 1; ++j)
            {
                for (int i = h - 1; i > 0; --i)
                {
                    // 取得当前点的RGB值
                    byte pb = buffers[0 + 54 + (j + (h - i) * w) * BPP];
                    byte pg = buffers[1 + 54 + (j + (h - i) * w) * BPP];
                    byte pr = buffers[2 + 54 + (j + (h - i) * w) * BPP];
                    // 取得当前点上面那一点的RGB值
                    byte upb = buffers[0 + 54 + (j + (h - i - 1) * w) * BPP];
                    byte upg = buffers[1 + 54 + (j + (h - i - 1) * w) * BPP];
                    byte upr = buffers[2 + 54 + (j + (h - i - 1) * w) * BPP];
                    // 取得当前点上面那一点的RGB值
                    byte rpb = buffers[0 + 54 + (j + 1 + (h - i) * w) * BPP];
                    byte rpg = buffers[1 + 54 + (j + 1 + (h - i) * w) * BPP];
                    byte rpr = buffers[2 + 54 + (j + 1 + (h - i) * w) * BPP];
                    // 取得当前点上面那一点的RGB值
                    byte cpb = buffers[0 + 54 + (j + 1 + (h - i - 1) * w) * BPP];
                    byte cpg = buffers[1 + 54 + (j + 1 + (h - i - 1) * w) * BPP];
                    byte cpr = buffers[2 + 54 + (j + 1 + (h - i - 1) * w) * BPP];
                    // 分别计算两个点的亮度值
                    double YB = 0.3 * pr + 0.59 * pg + 0.11 * pb;
                    double YU = 0.3 * upr + 0.59 * upg + 0.11 * upb;
                    double YR = 0.3 * rpr + 0.59 * rpg + 0.11 * rpb;
                    double YC = 0.3 * cpr + 0.59 * cpg + 0.11 * cpb;
                    // 求出两个点的亮度差值
                    //double tmpDelta = YU - YB > 0 ? YU - YB : YB - YU;
                    //double tmpDelta = YB - YU;
                    double tmpDelta = Math.Abs(YB - YU) + Math.Abs(YB - YR) + Math.Abs(YB - YC) + Math.Abs(YR - YU);
                    if (tmpDelta > GradientThreshold) // 与预设的阈值数据进行比较
                    {
                        bout[j + (i) * w] = 1;//大于，则表示有梯度
                    }
                    else
                    {
                        if (YU > brightThreshold)
                        {
                            bout[j + (i) * w] = 0;//小于，则表示无梯度
                        }
                        else
                        {
                            bout[j + (i) * w] = 1;//大于，则表示有梯度
                        }
                    }
                }
            }
            return bout;
        }

        /// <summary>
        /// 梯度算法
        /// </summary>
        /// <param name="img">输入图片</param>
        /// <param name="GradientThreshole">梯度阈值</param>
        /// <param name="brightThreshole">亮度阈值</param>
        /// <returns>输出图片</returns>
        [Obsolete()]
        public Image GradientImage(Image img, int GradientThreshold, int brightThreshold)
        {
            Bitmap Bmp1 = new Bitmap(img);
            Color ColorOrigin0, ColorOrigin1, ColorOrigin2, ColorOrigin3;
            int R0, R1, R2, R3;
            int Delta;
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    if (i == img.Width - 1 || j == img.Height - 1)
                    {
                        Bmp1.SetPixel(i, j, Color.White);
                    }
                    else
                    {
                        ColorOrigin0 = Bmp1.GetPixel(i, j);
                        ColorOrigin1 = Bmp1.GetPixel(i + 1, j);
                        ColorOrigin2 = Bmp1.GetPixel(i, j + 1);
                        ColorOrigin3 = Bmp1.GetPixel(i + 1, j + 1);
                        R0 = ColorOrigin0.R;
                        R1 = ColorOrigin1.R;
                        R2 = ColorOrigin2.R;
                        R3 = ColorOrigin3.R;
                        Delta = Math.Abs(R0 - R1) + Math.Abs(R0 - R2) + Math.Abs(R0 - R3) + Math.Abs(R1 - R2);
                        if (Delta >= GradientThreshold)
                        {

                            Bmp1.SetPixel(i, j, Color.Black);
                        }
                        if (Delta < GradientThreshold)
                        {
                            if (R0 > 0.5 * brightThreshold)
                            {
                                Bmp1.SetPixel(i, j, Color.White);
                            }
                            else
                            {
                                Bmp1.SetPixel(i, j, Color.Black);
                            }

                        }
                    }
                }
            }
            return Bmp1;
        }

        /// <summary>
        /// 线段拟合
        /// </summary>
        /// <param name="data">图像的二进制数据</param>
        /// <param name="w">图像宽度</param>
        /// <param name="h">图像高度</param>
        /// <param name="x">线段端X坐标</param>
        /// <param name="y">线段端Y坐标</param>
        /// <param name="minpoints">拟合精细度，即最小拟合线段长度</param>
        /// <returns>拟合线段坐标点集</returns>
        public Point[] dfsFindPath(byte[] data, int w, int h, int x, int y, /*int deep, int maxfluctuate,*/ int minpoints)
        {
            List<Point> ptout = new List<Point>();
            int s = w * h;
            List<Point> nodes = new List<Point>();
            nodes.Add(new Point(x, y));
            ptout.Add(new Point(x, y));
            bool flag = true;
            int i = 0;
            int k = 0;
            while (flag)
            {
                x = nodes[i].X;
                y = nodes[i].Y;
                k = x + y * w;
                data[k] = 0;
                ++i;
                if (k + 1 < s && data[k + 1] == 1)
                {
                    nodes.Add(new Point(x + 1, y));
                }
                else if (k - 1 >= 0 && data[k - 1] == 1)
                {
                    nodes.Add(new Point(x - 1, y));
                }
                else if (k + w < s && data[k + w] == 1)
                {
                    nodes.Add(new Point(x, y + 1));
                }
                else if (k - w >= 0 && data[k - w] == 1)
                {
                    nodes.Add(new Point(x, y - 1));
                }
                else if (k + w + 1 < s && data[k + w + 1] == 1)
                {
                    nodes.Add(new Point(x + 1, y + 1));
                }
                else if (k + w - 1 < s && data[k + w - 1] == 1)
                {
                    nodes.Add(new Point(x - 1, y + 1));
                }
                else if (k - w + 1 >= 0 && data[k - w + 1] == 1)
                {
                    nodes.Add(new Point(x + 1, y - 1));
                }
                else if (k - w - 1 >= 0 && data[k - w - 1] == 1)
                {
                    nodes.Add(new Point(x - 1, y - 1));
                }
                else
                {
                    flag = false;
                }
            }

            //int minfluctuate = 2;
            k = 0;
            while (k < nodes.Count - minpoints)
            {
                Point spt = nodes[k];// start point
                int pos = 0;
                double minvalue = double.MaxValue;
                double[] distance = new double[nodes.Count];
                for (i = k + 1; i < nodes.Count; ++i)
                {
                    if (i - k < minpoints)
                    {
                        distance[i] = double.MaxValue;
                    }
                    else
                    {
                        Point ept = nodes[i];// end point
                        double tmpr = 0;
                        for (int j = k; j < i; ++j)
                        {
                            tmpr += PointToLineDistance(nodes[j], spt, ept);
                        }
                        distance[i] = tmpr / (i - k);
                    }
                    if (distance[i] < minvalue)
                    {
                        pos = i;
                        minvalue = distance[i];
                    }
                }
                ptout.Add(nodes[pos]);
                k = pos;
            }
            if (ptout[ptout.Count - 1] != nodes[nodes.Count - 1])
            {
                ptout.Add(nodes[nodes.Count - 1]);
            }
            return ptout.ToArray();
        }

        /// <summary>
        /// 计算点到直线的距离
        /// </summary>
        /// <param name="pt">点坐标</param>
        /// <param name="ln1">直线段一端坐标</param>
        /// <param name="ln2">直线段另一端坐标</param>
        /// <returns>距离</returns>
        public double PointToLineDistance(Point pt, Point ln1, Point ln2)
        {
            if (ln1.X == ln2.X)
            {
                return Math.Abs(pt.X - ln2.X);
            }
            else if (ln1.Y == ln2.Y)
            {
                return Math.Abs(pt.Y - ln2.Y);
            }
            else
            {
                double a = ln2.Y - ln1.Y;
                double b = ln1.X - ln2.X;
                double c = ln1.Y * ln2.X - ln1.X * ln2.Y;
                return Math.Abs(a * pt.X + b * pt.Y + c) / Math.Sqrt(a * a + b * b);
            }
        }

        /// <summary>
        /// 标记交叉点
        /// </summary>
        /// <param name="img">输入图片</param>
        public void MarkCross(Image img)
        {
            arrCrossList.Clear();
            byte[] buff = getBinaryFromImage(img);
            int w = img.Width;
            int h = img.Height;
            for (int i = 1; i < h - 1; ++i)
            {
                for (int j = 1; j < w - 1; ++j)
                {
                    int k = i * w + j;
                    if (j == 198 && i == 339)
                    { }
                    if (buff[k] == 1)
                    {
                        int up = 0, down = 0, right = 0, left = 0;
                        int nup = 0, ndown = 0, nright = 0, nleft = 0;
                        right += buff[k + 1];
                        left += buff[k - 1];
                        down += buff[k + w];
                        up += buff[k - w];
                        if (down > right) ndown += buff[k + w + 1]; else nright += buff[k + w + 1];
                        if (left > down) nleft += buff[k + w - 1]; else ndown += buff[k + w - 1];
                        if (right > up) nright += buff[k - w + 1]; else nup += buff[k - w + 1];
                        if (up > left) nup += buff[k - w - 1]; else nleft += buff[k - w - 1];
                        right += nright;
                        left += nleft;
                        down += ndown;
                        up += nup;
                        int n = (up > 0 ? 1 : 0) + (down > 0 ? 1 : 0) + (right > 0 ? 1 : 0) + (left > 0 ? 1 : 0);
                        if (n > 2)
                        {
                            //picMain.CreateGraphics().DrawRectangle(new Pen(new SolidBrush(Color.Red)), new Rectangle(j - 1, i - 1, 2, 2));
                            arrCrossList.Add(new Point(j, i));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 连接短断口
        /// </summary>
        /// <param name="img">输入图片</param>
        /// <param name="maxlen">断口最长距离</param>
        /// <returns>输出图片</returns>
        public Image JoinShortLines(Image img, int maxlen)
        {
            Pen blackpen = new Pen(new SolidBrush(Color.Black));
            Bitmap bmp = new Bitmap(img);
            Graphics g = Graphics.FromImage(bmp);
            for (int k = 1; k <= maxlen; ++k)
            {
                MarkLineEnds(bmp);
                Point[] pts = arrEndList.ToArray();
                int max = (int)Math.Pow((double)k, 2);
                for (int i = 0; i < pts.Length - 1; ++i)
                {
                    for (int j = i + 1; j < pts.Length; ++j)
                    {
                        int x = pts[i].X - pts[j].X;
                        int y = pts[i].Y - pts[j].Y;
                        int r = x * x + y * y;
                        if (r <= max)
                        {
                            g.DrawLine(blackpen, pts[i], pts[j]);
                            //DrawLine(img, pts[i], pts[j]);
                        }
                    }
                }
                g.Flush();
            }
            g.Dispose();
            return bmp;
        }

        private void DrawLine(Bitmap bmp, Point pt1, Point pt2)
        {
            if (pt1.X > pt2.X)
            {
                Point tmpoint = pt1;
                pt1 = pt2;
                pt2 = tmpoint;
            }
            double k = (double)(pt2.Y - pt1.Y) / (pt2.X - pt1.X);
            for (int x = 1; x < pt2.X - pt1.X; ++x)
            {
                int y = (int)(k * x);
                bmp.SetPixel(x + pt1.X, y + pt1.Y, Color.Black);
            }
        }

        #region 去除斑点
        /// <summary>
        /// 二值图像去斑
        /// </summary>
        public void WipeOffShortLine(ref byte[] data, int w, int h)
        {
            int i = 0;
            for (int k = 0; k < w * h; ++k)
            {
                if (data[k] == (byte)MapData.Black)
                {
                    if (IsShortLine(ref data, w, h, k))
                    {
                        EraseLinePoint(ref data, w, h, k);
                        ++i;
                    }
                    else
                    {
                        RestoreLinePoint(ref data, w, h, k);
                    }
                }
            }
            for (int k = 0; k < w * h; ++k)
            {
                data[k] = data[k] == (byte)MapData.Marked ? (byte)MapData.Black : data[k];
            }
            //Console.WriteLine("Delete Spots Num: "+i);
        }

        /// <summary>
        /// 检测是否斑点过大
        /// </summary>
        public bool IsShortLine(ref byte[] data, int w, int h, int k)
        {
            if (SetLinePoint(ref data, w, h, k, (byte)MapData.Marked, (byte)MapData.Black) < MinLineLength)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 擦除斑点
        /// </summary>
        public void EraseLinePoint(ref byte[] data, int w, int h, int k)
        {
            SetLinePoint(ref data, w, h, k, (byte)MapData.None, (byte)MapData.Marked);
        }

        /// <summary>
        /// 恢复斑点
        /// </summary>
        public void RestoreLinePoint(ref byte[] data, int w, int h, int k)
        {
            SetLinePoint(ref data, w, h, k, (byte)MapData.Black, (byte)MapData.Marked);
        }

        /// <summary>
        /// 设置斑点的内存中的数值，即进行了宽度优先的搜索
        /// </summary>
        public int SetLinePoint(ref byte[] data, int w, int h, int k, byte tgtvalue, byte orivalue)
        {
            //node[] nodes = new node[MinLineLength + 5];
            List<int> nodes = new List<int>();
            int hd = -1;
            //int tl = 0;
            nodes.Add(k);
            do
            {
                ++hd;
                data[nodes[hd]] = tgtvalue;
                if (nodes[hd] + 1 < w * h && data[nodes[hd] + 1] == orivalue) { nodes.Add(nodes[hd] + 1); LinePointCheckSame(nodes); }
                if (nodes[hd] + w < w * h && data[nodes[hd] + w] == orivalue) { nodes.Add(nodes[hd] + w); LinePointCheckSame(nodes); }
                if (nodes[hd] - w >= 0 && data[nodes[hd] - w] == orivalue) { nodes.Add(nodes[hd] - w); LinePointCheckSame(nodes); }
                if (nodes[hd] - 1 >= 0 && data[nodes[hd] - 1] == orivalue) { nodes.Add(nodes[hd] - 1); LinePointCheckSame(nodes); }
                if (nodes[hd] + w + 1 < w * h && data[nodes[hd] + w + 1] == orivalue) { nodes.Add(nodes[hd] + w + 1); LinePointCheckSame(nodes); }
                if (nodes[hd] + w - 1 < w * h && data[nodes[hd] + w - 1] == orivalue) { nodes.Add(nodes[hd] + w - 1); LinePointCheckSame(nodes); }
                if (nodes[hd] - w + 1 >= 0 && data[nodes[hd] - w + 1] == orivalue) { nodes.Add(nodes[hd] - w + 1); LinePointCheckSame(nodes); }
                if (nodes[hd] - w - 1 >= 0 && data[nodes[hd] - w - 1] == orivalue) { nodes.Add(nodes[hd] - w - 1); LinePointCheckSame(nodes); }

            } while (hd < nodes.Count - 1 && nodes.Count <= MinLineLength);
            return nodes.Count - 1;
        }

        /// <summary>
        /// 宽度优先算法配套的检查重复节点方法
        /// </summary>
        public void LinePointCheckSame(List<int> nodes)
        {
            int i;
            int tl = nodes.Count - 1;
            for (i = 0; i < tl; ++i)
            {
                if (nodes[i] == nodes[tl]) break;
            }
            if (i < tl && nodes[i] == nodes[tl]) nodes.RemoveAt(tl);
        }
        #endregion

        /// <summary>
        /// 细化
        /// </summary>
        /// <param name="img">输入图片</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="times">细化次数</param>
        /// <returns>输出图片</returns>
        public Image Thinner(Image img, int width, int height, int times)
        {
            int dwWidth = width;
            int dwHeight = height;
            int i, j, time, change;
            int[,] f = new int[width + 2, height + 2];
            int[,] t = new int[width + 2, height + 2];
            int[,] x = new int[width + 2, height + 2];
            int[,] three = new int[width + 2, height + 2];
            int[,] four = new int[width + 2, height + 2];
            int[,] five1 = new int[width + 2, height + 2];
            int[,] five2 = new int[width + 2, height + 2];
            //Color c = new Color();
            byte c;
            //Bitmap box1 = new Bitmap(img);
            byte[] buff = getBinaryFromImage(img);
            change = 1;
            for (time = 1; time <= times; time++)
            {
                switch (change)
                {
                    case 1:
                        for (i = 1; i < dwWidth; i++)
                            for (j = 1; j < dwHeight; j++)
                            {
                                c = buff[j * dwWidth + i];
                                //c = box1.GetPixel(i, j);
                                f[i, j] = c;
                            }
                        for (i = 2; i < dwWidth; i++)
                            for (j = 2; j < dwHeight; j++)
                                x[i, j] = Math.Abs(f[i + 1, j - 1] - f[i + 1, j]) +
                                       Math.Abs(f[i, j - 1] - f[i + 1, j - 1]) +
                                       Math.Abs(f[i - 1, j - 1] - f[i, j - 1]) +
                                       Math.Abs(f[i - 1, j] - f[i - 1, j - 1]) +
                                       Math.Abs(f[i - 1, j + 1] - f[i - 1, j]) +
                                       Math.Abs(f[i, j + 1] - f[i - 1, j + 1]) +
                                       Math.Abs(f[i + 1, j + 1] - f[i, j + 1]);
                        //DEUTSCH
                        for (i = 1; i < dwWidth; i++)
                            for (j = 1; j < dwHeight; j++)
                                t[i, j] = f[i - 1, j - 1] + f[i - 1, j]
                                      + f[i - 1, j + 1] + f[i, j - 1]
                                      + f[i, j + 1] + f[i + 1, j - 1]
                                      + f[i + 1, j] + f[i + 1, j + 1];
                        for (i = 2; i <= dwWidth; i++)
                            for (j = 2; j < dwHeight; j++)
                            {
                                if (f[i + 1, j] == 0 || f[i, j - 1] == 0 || f[i - 1, j] == 0)
                                    three[i, j] = 1;
                                if (f[i + 1, j] == 0 || f[i, j - 1] == 0 || f[i, j + 1] == 0)
                                    four[i, j] = 1;
                                if ((f[i + 1, j] == 1 && f[i, j + 1] == 1) && (f[i + 1, j - 1] == 1
                                  || f[i - 1, j + 1] == 1) && (f[i, j - 1] == 0 && f[i - 1, j - 1] == 0 && f[i - 1, j] == 0 && f[i + 1, j + 1] == 0))
                                    five1[i, j] = 1;
                                if ((f[i + 1, j] == 1 && f[i, j - 1] == 1) && (f[i - 1, j - 1] == 1
                                  || f[i + 1, j + 1] == 1) && (f[i + 1, j - 1] == 0 && f[i - 1, j] == 0 && f[i - 1, j + 1] == 0 && f[i, j + 1] == 0))
                                    five2[i, j] = 1;
                            }
                        for (i = 2; i < dwWidth; i++)
                            for (j = 2; j < dwHeight; j++)
                            {
                                if ((x[i, j] == 0 || x[i, j] == 2) && t[i, j] != 1 && three[i, j] == 1 && four[i, j] == 1 ||
                                    (x[i, j] == 4 && t[i, j] != 1 && three[i, j] == 1 && four[i, j] == 1 && (five1[i, j] == 1 || five2[i, j] == 1)))
                                    buff[j * dwWidth + i] = 0;
                            }
                        break;
                    case 2:
                        for (i = 1; i < dwWidth; i++)
                            for (j = 1; j < dwHeight; j++)
                            {
                                c = buff[j * dwWidth + i];
                                //c = box1.GetPixel(i, j);
                                f[i, j] = c;
                            }
                        for (i = 2; i < dwWidth; i++)
                            for (j = 2; j < dwHeight; j++)
                                x[i, j] = Math.Abs(f[i - 1, j + 1] - f[i - 1, j]) +
                                    Math.Abs(f[i, j + 1] - f[i - 1, j + 1]) +
                                    Math.Abs(f[i + 1, j + 1] - f[i, j + 1]) +
                                    Math.Abs(f[i + 1, j] - f[i + 1, j + 1]) +
                                    Math.Abs(f[i + 1, j - 1]) - f[i + 1, j] +
                                    Math.Abs(f[i, j - 1] - f[i + 1, j - 1]) +
                                    Math.Abs(f[i - 1, j - 1] - f[i, j - 1]);
                        for (i = 1; i < dwWidth; i++)
                            for (j = 1; j < dwHeight; j++)
                                t[i, j] = f[i + 1, j + 1] + f[i + 1, j] + f[i + 1, j - 1] + f[i, j + 1] + f[i, j - 1] + f[i - 1, j + 1] + f[i - 1, j] + f[i - 1, j - 1];
                        for (i = 2; i < dwWidth; i++)
                            for (j = 2; j < dwHeight; j++)
                            {
                                if (f[i - 1, j] == 0 || f[i, j + 1] == 0 || f[i + 1, j] == 0)
                                    three[i, j] = 1;
                                if (f[i - 1, j] == 0 || f[i, j + 1] == 0 || f[i, j - 1] == 0)
                                    four[i, j] = 1;
                                if ((f[i - 1, j] == 1 && f[i, j - 1] == 1) && (f[i - 1, j + 1] == 1 || f[i + 1, j - 1] == 1) && (f[i, j + 1] == 0 && f[i + 1, j + 1] == 0
                                    && f[i + 1, j] == 0 && f[i - 1, j - 1] == 0))
                                    five1[i, j] = 1;
                                if ((f[i - 1, j] == 1 && f[i, j + 1] == 1) && (f[i + 1, j + 1] == 1 || f[i - 1, j - 1] == 1) && (f[i - 1, j + 1] == 0 && f[i + 1, j] == 0
                                    && f[i + 1, j - 1] == 0 && f[i, j - 1] == 0))
                                    five2[i, j] = 1;
                            }
                        for (i = 2; i < dwWidth; i++)
                            for (j = 2; j < dwHeight; j++)
                            {
                                if ((x[i, j] == 0 || x[i, j] == 2) && t[i, j] != 1 && three[i, j] == 1 && four[i, j] == 1 ||
                                    (x[i, j] == 4 && t[i, j] != 1 && three[i, j] == 1 && four[i, j] == 1 && (five1[i, j] == 1 || five2[i, j] == 1)))
                                    buff[j * dwWidth + i] = 0;
                            }
                        break;
                }
                if (change == 1) change = 2;
                else if (change == 2) change = 1;
            }
            return getImageFromBinary(buff, img.Width, img.Height);
        }

        private Image Gaussion(Image img)
        {
            Color c1 = new Color();
            Color c2 = new Color();
            Color c3 = new Color();
            Color c4 = new Color();
            Color c5 = new Color();
            Color c6 = new Color();
            Color c7 = new Color();
            Color c8 = new Color();
            Color c9 = new Color();
            int rr, r1, r2, r3, r4, r5, r6, r7, r8, r9, i, j, fxr;
            Bitmap box1 = new Bitmap(img);
            for (i = 1; i <= img.Width - 2; i++)
            {
                for (j = 1; j <= img.Height - 2; j++)
                {
                    c1 = box1.GetPixel(i, j - 1);
                    c2 = box1.GetPixel(i - 1, j);
                    c3 = box1.GetPixel(i, j);
                    c4 = box1.GetPixel(i + 1, j);
                    c5 = box1.GetPixel(i, j + 1);
                    c6 = box1.GetPixel(i - 1, j - 1);
                    c7 = box1.GetPixel(i - 1, j + 1);
                    c8 = box1.GetPixel(i + 1, j - 1);
                    c9 = box1.GetPixel(i + 1, j + 1);
                    r1 = c1.R;
                    r2 = c2.R;
                    r3 = c3.R;
                    r4 = c4.R;
                    r5 = c5.R;
                    r6 = c6.R;
                    r7 = c7.R;
                    r8 = c8.R;
                    r9 = c9.R;
                    fxr = (r6 + r7 + r8 + r9 + 2 * r1 + 2 * r2 + 2 * r4 + 2 * r5 + 4 * r3) / 16;
                    rr = fxr;
                    if (rr < 0) rr = 0;
                    if (rr > 255) rr = 255;
                    Color cc = Color.FromArgb(rr, rr, rr);
                    box1.SetPixel(i, j, cc);
                }
            }
            return box1;
        }

        /// <summary>
        /// 标记线段端点
        /// </summary>
        /// <param name="img">输入图片</param>
        public void MarkLineEnds(Image img)
        {
            arrEndList.Clear();
            byte[] buff = getBinaryFromImage(img);
            int w = img.Width;
            int h = img.Height;
            for (int i = 1; i < h - 1; ++i)
            {
                for (int j = 1; j < w - 1; ++j)
                {
                    int k = i * w + j;
                    if (buff[k] == 1)
                    {

                        int n = 0;
                        n += buff[k + 1];
                        n += buff[k - 1];
                        n += buff[k + w];
                        n += buff[k - w];
                        n += buff[k + w + 1];
                        n += buff[k + w - 1];
                        n += buff[k - w + 1];
                        n += buff[k - w - 1];
                        int m = 0;
                        m += buff[k + 1] * 0x01;
                        m += buff[k - w + 1] * 0x02;
                        m += buff[k - w] * 0x04;
                        m += buff[k - w - 1] * 0x08;
                        m += buff[k - 1] * 0x10;
                        m += buff[k + w - 1] * 0x20;
                        m += buff[k + w] * 0x40;
                        m += buff[k + w + 1] * 0x80;
                        while (m % 2 == 0 && m > 0)
                        {
                            m /= 2;
                        }
                        if (n < 2 || m == 3 || m == 0x81)
                        {
                            //picMain.CreateGraphics().DrawRectangle(new Pen(new SolidBrush(Color.Blue)), new Rectangle(j - 1, i - 1, 2, 2));
                            arrEndList.Add(new Point(j, i));
                        }
                    }
                }
            }
        }

        #region 图像与二进制转换函数
        /// <summary>
        /// 将图片转换为二进制格式
        /// </summary>
        /// <param name="img">输入图片</param>
        /// <returns>二进制数组</returns>
        public byte[] getBinaryFromImage(Image img)
        {
            int BPP = 3;
            Bitmap b = new Bitmap(img);
            if (b.PixelFormat.ToString().IndexOf("32") > -1) BPP = 4;
            int width = b.Width;
            int height = b.Height;

            //Bitmap dstImage = new Bitmap(width, height);

            BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            //BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            byte[] buff = new byte[img.Width * img.Height];
            int k = 0;

            int stride = srcData.Stride;
            int offset = stride - width * BPP;

            unsafe
            {
                byte* src = (byte*)srcData.Scan0;
                //byte* dst = (byte*)dstData.Scan0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        //dst[0] = (byte)(255 - src[0]);
                        //dst[1] = (byte)(255 - src[1]);
                        //dst[2] = (byte)(255 - src[2]);
                        //dst[3] = src[3];

                        buff[k++] = src[0] == 255 ? (byte)0 : (byte)1;
                        src += BPP;
                        //dst += BPP;
                    } // x

                    src += offset;
                    //dst += offset;
                } // y
            }

            b.UnlockBits(srcData);
            //dstImage.UnlockBits(dstData);

            b.Dispose();
            return buff;
        }

        /// <summary>
        /// 将二进制格式转换为图片格式
        /// </summary>
        /// <param name="buff">二进制数组</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>图片</returns>
        public Image getImageFromBinary(byte[] buff, int width, int height)
        {
            int BPP = 4;
            Bitmap b = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            //if (b.PixelFormat.ToString().IndexOf("32") > -1) BPP = 4;

            BitmapData dstData = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int k = 0;

            int stride = dstData.Stride;
            int offset = stride - width * BPP;

            unsafe
            {
                byte* dst = (byte*)dstData.Scan0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        dst[0] = buff[k] == 1 ? (byte)0 : (byte)255;
                        dst[1] = buff[k] == 1 ? (byte)0 : (byte)255;
                        dst[2] = buff[k] == 1 ? (byte)0 : (byte)255;
                        dst[3] = (byte)255;

                        k++;
                        dst += BPP;
                    } // x

                    dst += offset;
                } // y
            }

            b.UnlockBits(dstData);
            return b;
        }
        #endregion
    }
}
