using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CrackDisplay
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        public struct MultiLineData
        {
            public Point[] points;
            public double[] width;
        }
        public List<MultiLineData> arrFinalLineList = new List<MultiLineData>();

        struct MOUSESTATESTRUCT
        {
            public MouseButtons Buttons;
            public Point Position;
        }
        MOUSESTATESTRUCT mouseState;

        private float[] PercentPreset = { 0.20f, .33f, .50f, .67f, .90f, 1.00f, 1.20f, 1.50f, 2.00f, 4.00f, 8.00f, 16.00f };
        private int DefaultPercent = 0;
        private float _percent;
        private PointF _imageposition;
        private Size _basesize = new Size(557, 392);
        private Size _imagesize;
        private Point _lastdrawimageposition;
        private Point _mousestartposition;
        private bool _ispicturemoving;

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            arrFinalLineList.Clear();
            string str = txtLineData.Text;
            foreach (string bigpart in str.Split('|'))
            {
                string[] tmp = new string[3];
                int k = 0;
                List<Point> points = new List<Point>();
                List<double> widths = new List<double>();
                MultiLineData mld = new MultiLineData();
                foreach (string smallpart in bigpart.Split(','))
                {
                    tmp[k++] = smallpart;
                    if (k == 3)
                    {
                        points.Add(new Point(Convert.ToInt32(tmp[0]), Convert.ToInt32(tmp[1])));
                        widths.Add(Convert.ToDouble(tmp[2]));
                        k = 0;
                    }
                }
                widths.RemoveAt(widths.Count - 1);
                mld.points = points.ToArray();
                mld.width = widths.ToArray();
                arrFinalLineList.Add(mld);
            }
            DrawImage(0, 0, 0, 0);
        }

        private void picMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _ispicturemoving = true;
                _imageposition = new PointF(_imageposition.X + ((e.X - _mousestartposition.X) / _percent), _imageposition.Y + ((e.Y - _mousestartposition.Y) / _percent));
                _mousestartposition = new Point(e.X, e.Y);
                if (_imagesize.Width >= picMain.Width)
                {
                    if (_imageposition.X > 0)
                    {
                        _imageposition.X = 0;
                    }
                    else if (-_imageposition.X + picMain.Width > _imagesize.Width)
                    {
                        _imageposition.X = (int)(picMain.Width - _imagesize.Width);
                    }
                }
                else
                {
                    //if (_imageposition.X < 0)
                    //{
                    //    _imageposition.X = 0;
                    //}
                    //else if (_imageposition.X + _imagesize.Width > picMain.Width)
                    //{
                    //    _imageposition.X = (int)(picMain.Width - _imagesize.Width);
                    //}
                    _imageposition.X = (int)((picMain.Width - _imagesize.Width) / 2);
                }
                if (_imagesize.Height * _percent >= picMain.Height)
                {
                    if (_imageposition.Y > 0)
                    {
                        _imageposition.Y = 0;
                    }
                    else if (-_imageposition.Y + picMain.Height > _imagesize.Height * _percent)
                    {
                        _imageposition.Y = (int)(picMain.Height - _imagesize.Height * _percent);
                    }
                }
                else
                {
                    //if (_imageposition.Y < 0)
                    //{
                    //    _imageposition.Y = 0;
                    //}
                    //else if (_imageposition.Y + _imagesize.Height * _percent > picMain.Height)
                    //{
                    //    _imageposition.Y = (int)(picMain.Height - _imagesize.Height * _percent);
                    //}
                    _imageposition.Y = (int)((picMain.Height - _imagesize.Height * _percent) / 2);
                }
                this.Cursor = Cursors.Hand;
                this.Text = string.Format("({0},{1}): {2}%", _imageposition.X, _imageposition.Y, _percent * 100);
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }
            //if (Math.Abs(_lastdrawimageposition.X - (int)_imageposition.X) > 0 || Math.Abs(_lastdrawimageposition.Y - (int)_imageposition.Y) > 0)
            //{
                DrawImage(-(int)_imageposition.X, -(int)_imageposition.Y, e.X, e.Y);
            //    _lastdrawimageposition = new Point((int)_imageposition.X, (int)_imageposition.Y);
            //}
        }

        private void DrawImage(int left, int top, int XPos, int YPos)
        {
            Bitmap bmp = new Bitmap(picMain.Width, picMain.Height);
            Graphics g = Graphics.FromImage(bmp);
            Pen greenpen = new Pen(new SolidBrush(Color.Green));
            Pen bluepen = new Pen(new SolidBrush(Color.Blue));
            Pen redpen = new Pen(new SolidBrush(Color.Red));
            bool p = false;
            string str = "";
            for (int i = 0; i < arrFinalLineList.Count; ++i)
            {
                Point[] r = arrFinalLineList[i].points;
                g.DrawRectangle(greenpen, new Rectangle((int)((r[0].X - left) * _percent), (int)((r[0].Y - top) * _percent), 2, 2));
                for (int j = 1; j < r.Length; ++j)
                {
                    if (!p && HitLine(new Point((int)((r[j].X - left) * _percent), (int)((r[j].Y - top) * _percent)),
                        new Point((int)((r[j-1].X - left) * _percent), (int)((r[j-1].Y - top) * _percent)), new Point(XPos, YPos)))
                    {
                        //DrawLine(bmp, new Point((int)((r[j].X - left) * _percent), (int)((r[j].Y - top) * _percent)),
                        //new Point((int)((r[j - 1].X - left) * _percent), (int)((r[j - 1].Y - top) * _percent)), redpen.Color);
                        g.DrawLine(new Pen(new SolidBrush(Color.Red), (float)(arrFinalLineList[i].width[j - 1] * _percent)), new Point((int)((r[j].X - left) * _percent), (int)((r[j].Y - top) * _percent)),
new Point((int)((r[j - 1].X - left) * _percent), (int)((r[j - 1].Y - top) * _percent)));
                        p = true;
                        str = string.Format("({4}, {5})\r\n[({0}, {1})-({2}, {3})]:\r\n{6}",
                            r[j].X, r[j].Y, r[j - 1].X, r[j - 1].Y, XPos, YPos, arrFinalLineList[i].width[j - 1]);
                    }
                    else
                    {
                        //DrawLine(bmp, new Point((int)((r[j].X - left) * _percent), (int)((r[j].Y - top) * _percent)),
                        //new Point((int)((r[j - 1].X - left) * _percent), (int)((r[j - 1].Y - top) * _percent)), bluepen.Color);
                        g.DrawLine(new Pen(new SolidBrush(bluepen.Color), (float)(arrFinalLineList[i].width[j - 1] * _percent)), new Point((int)((r[j].X - left) * _percent), (int)((r[j].Y - top) * _percent)),
new Point((int)((r[j - 1].X - left) * _percent), (int)((r[j - 1].Y - top) * _percent)));
                    }
                    g.DrawRectangle(greenpen, new Rectangle((int)((r[j].X - left) * _percent), (int)((r[j].Y - top) * _percent), 2, 2));
                }
            }
            g.Flush();
            g.Dispose();
            picMain.Image = bmp;
            txtLineState.Text = str;
        }

        private void DrawLine(Bitmap bmp, Point pt1, Point pt2, Color c)
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
                if (x + pt1.X > 0 && x + pt1.X < bmp.Width && y + pt1.Y > 0 && y + pt1.Y < bmp.Height)
                {
                    bmp.SetPixel(x + pt1.X, y + pt1.Y, c);
                }
            }

            if (pt1.Y > pt2.Y)
            {
                Point tmpoint = pt1;
                pt1 = pt2;
                pt2 = tmpoint;
            }
            k = (double)(pt2.X - pt1.X) / (pt2.Y - pt1.Y);
            for (int y = 1; y < pt2.Y - pt1.Y; ++y)
            {
                int x = (int)(k * y);
                if (x + pt1.X > 0 && x + pt1.X < bmp.Width && y + pt1.Y > 0 && y + pt1.Y < bmp.Height)
                {
                    bmp.SetPixel(x + pt1.X, y + pt1.Y, c);
                }
            }
        }

        private bool HitLine(Point pt1, Point pt2, Point hpt)
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
                //if (new Point(x + pt1.X, y + pt1.Y) == hpt) return true;
                Rectangle rect = new Rectangle(new Point(x + pt1.X - 1, y + pt1.Y - 1), new Size(3, 3));
                if (rect.Contains(hpt)) return true;
            }

            if (pt1.Y > pt2.Y)
            {
                Point tmpoint = pt1;
                pt1 = pt2;
                pt2 = tmpoint;
            }
            k = (double)(pt2.X - pt1.X) / (pt2.Y - pt1.Y);
            for (int y = 1; y < pt2.Y - pt1.Y; ++y)
            {
                int x = (int)(k * y);
                Rectangle rect = new Rectangle(new Point(x + pt1.X - 1, y + pt1.Y - 1), new Size(3, 3));
                if (rect.Contains(hpt)) return true;
            }
            return false;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //if (mouseState.Buttons == MouseButtons.Right)
            //{
                float oldp = _percent;
                if (e.Delta > 0)
                {
                    for (int i = 0; i < PercentPreset.Length - 1; ++i)
                    {
                        if (PercentPreset[i] <= _percent && _percent < PercentPreset[i + 1])
                        {
                            _percent = PercentPreset[i + 1];
                            break;
                        }
                    }
                }
                else if (e.Delta < 0)
                {
                    for (int i = 1; i < PercentPreset.Length; ++i)
                    {
                        if (PercentPreset[i - 1] < _percent && _percent <= PercentPreset[i])
                        {
                            _percent = PercentPreset[i - 1];
                            break;
                        }
                    }
                }
                float newp = _percent;
                if (oldp != newp)
                {
                    _imageposition.X = ((oldp - newp) / oldp) * (e.X - _imageposition.X);
                    _imageposition.Y = ((oldp - newp) / oldp) * (e.Y - _imageposition.Y);
                }
                _imagesize = new Size((int)(_basesize.Width * _percent), (int)(_basesize.Height * _percent));
            //}
            DrawImage(-(int)_imageposition.X, -(int)_imageposition.Y, e.X, e.Y);
            this.Text = string.Format("({0},{1}): {2}%", _imageposition.X, _imageposition.Y, _percent * 100);
        }

        private void picMain_MouseDown(object sender, MouseEventArgs e)
        {
            mouseState.Buttons = e.Button;
            mouseState.Position = new Point(e.X, e.Y);
            _ispicturemoving = false;
            _mousestartposition = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Middle)
            {
                _percent = 1;
            }
        }

        private void picMain_MouseUp(object sender, MouseEventArgs e)
        {
            mouseState.Buttons = e.Button;
            mouseState.Position = new Point(e.X, e.Y);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            mouseState.Position = new Point();
            _percent = DefaultPercent == 0 ? 1f : 0f;
            _imagesize = _basesize;
        }

    }
}