using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CrackDetect
{
    public partial class EnhancedPictureBox : PictureBox
    {
        private Point _imageposition;
        private Point _mousestartposition;
        private bool _ispicturemoving;

        private float[] PercentPreset = { 0.20f, .33f, .50f, .67f, .90f, 1.00f, 1.20f, 1.50f, 2.00f, 4.00f, 8.00f, 16.00f };
        private int DefaultPercent = 0;
        private float _percent;

        public float Percent
        {
            get
            {
                return _percent;
            }
            set
            {
                _percent = value;
            }
        }

        public EnhancedPictureBox()
        {
            InitializeComponent();
            _percent = DefaultPercent;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        {
            if (this.Image == null) return;
            //Image img = new Bitmap(this.Image);
            //Graphics g = Graphics.FromImage(img);
            pe.Graphics.Clear(Color.Black);
            //pe.Graphics.DrawImage(this.Image, new Rectangle(0, 0, 100, 100));
            //pe.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Blue)), new Rectangle(0, 0, 100, 100));
            pe.Graphics.DrawImage(this.Image, new RectangleF(_imageposition, new SizeF(this.Image.Width * Percent, this.Image.Height * Percent)));
            //g.DrawRectangle(new Pen(new SolidBrush(Color.Blue)), new Rectangle(100, 100, 20, 20));
            //g.Flush();
            //g.Dispose();
            //this.CreateGraphics().DrawImage(img, 0, 0);
            //base.OnPaint(pe);
        }

        //Image changed delegate
        public delegate void ImageChanged(Object sender, EventArgs e);

        //event raised when image changed
        public event ImageChanged OnImageChange;

        //overridden/hidden Image property with event hooked up
        public new Image Image
        {
            get
            {
                return base.Image;
            }
            set
            {
                base.Image = value;
                if (OnImageChange != null)
                {
                    OnImageChange(this, new EventArgs());
                }
            }
        }

        private void EnhancedPictureBox_OnImageChange(object sender, System.EventArgs e)
        {
            if (this.Image == null) return;
            if (DefaultPercent == 0)
            {
                float wp = (float)this.Width / this.Image.Width;
                float hp = (float)this.Height / this.Image.Height;
                if (wp > hp)
                {
                    Percent = hp;
                }
                else
                {
                    Percent = wp;
                }
                if (Percent > 1)
                {
                    Percent = 1;
                }
            }
            else
            {
                Percent = DefaultPercent;
            }
            _imageposition.X = (int)((this.Width - this.Image.Width * Percent) / 2);
            _imageposition.Y = (int)((this.Height - this.Image.Height * Percent) / 2);
        }

        //protected virtual void InValidated()
        //{
        //}

        private void EnhancedPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_ispicturemoving)
            {
                if (e.Button == MouseButtons.Left)
                {
                    for (int i = 0; i < PercentPreset.Length - 1; ++i)
                    {
                        if (PercentPreset[i] <= Percent && Percent < PercentPreset[i + 1])
                        {
                            Percent = PercentPreset[i + 1];
                            break;
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    for (int i = 1; i < PercentPreset.Length; ++i)
                    {
                        if (PercentPreset[i - 1] < Percent && Percent <= PercentPreset[i])
                        {
                            Percent = PercentPreset[i - 1];
                            break;
                        }
                    }
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    Percent = 1;
                }
            }
            OnPaint(new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle));
        }

        private void EnhancedPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _ispicturemoving = true;
                _imageposition = new Point(_imageposition.X + e.X - _mousestartposition.X, _imageposition.Y + e.Y - _mousestartposition.Y);
                _mousestartposition = new Point(e.X, e.Y);
                if (this.Image.Width * Percent >= this.Width)
                {
                    if (_imageposition.X > 0)
                    {
                        _imageposition.X = 0;
                    }
                    else if (-_imageposition.X + this.Width > this.Image.Width * Percent)
                    {
                        _imageposition.X = (int)(this.Width - this.Image.Width * Percent);
                    }
                }
                else
                {
                    if (_imageposition.X < 0)
                    {
                        _imageposition.X = 0;
                    }
                    else if (_imageposition.X + this.Image.Width * Percent > this.Width)
                    {
                        _imageposition.X = (int)(this.Width - this.Image.Width * Percent);
                    }
                }
                if (this.Image.Height * Percent >= this.Height)
                {
                    if (_imageposition.Y > 0)
                    {
                        _imageposition.Y = 0;
                    }
                    else if (-_imageposition.Y + this.Height > this.Image.Height * Percent)
                    {
                        _imageposition.Y = (int)(this.Height - this.Image.Height * Percent);
                    }
                }
                else
                {
                    if (_imageposition.Y < 0)
                    {
                        _imageposition.Y = 0;
                    }
                    else if (_imageposition.Y + this.Image.Height * Percent > this.Height)
                    {
                        _imageposition.Y = (int)(this.Height - this.Image.Height * Percent);
                    }
                }
                OnPaint(new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle));
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void EnhancedPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            _ispicturemoving = false;
            _mousestartposition = new Point(e.X, e.Y);
        }
    }
}
