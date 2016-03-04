﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace evolutionSoccer
{
    public partial class GraphicsForm : Form
    {
        private System.Drawing.Graphics graphics; // is it good?

        public GraphicsForm()
        {
            Console.WriteLine("Opening a form for graphs.");
            InitializeComponent();
            graphics = this.CreateGraphics();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Console.WriteLine("Closing graphs form.");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawLine(System.Drawing.Pens.Black, new Point(this.Width / 20, 0), new Point(this.Width / 20, this.Height / 2));
            e.Graphics.DrawLine(System.Drawing.Pens.Black, new Point(0, this.Height / 2 - this.Width / 20), new Point(this.Width, this.Height / 2 - this.Width / 20));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            DrawCross();
        }

        private void DrawCross()
        {
            graphics = this.CreateGraphics();
            if (graphics != null)
            {
                graphics.Clear(Color.White);
                graphics.DrawLine(System.Drawing.Pens.Black, new Point(this.Width / 20, 0), new Point(this.Width / 20, this.Height / 2));
                graphics.DrawLine(System.Drawing.Pens.Black, new Point(0, this.Height / 2 - this.Width / 20), new Point(this.Width, this.Height / 2 - this.Width / 20));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawCross();
        }

        public void DrawLine(double x1, double y1, double x2, double y2, Pen colour)
        {
            if (graphics != null)
                graphics.DrawLine(colour, new Point(this.Width / 20 + Convert.ToInt32(x1), this.Height / 2 - this.Width / 20 - Convert.ToInt32(y1)), new Point(this.Width / 20 + Convert.ToInt32(x2), this.Height / 2 - this.Width / 20 - Convert.ToInt32(y2)));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DrawLine(0, 0, 10, 10, System.Drawing.Pens.Black);
            DrawLine(10, 10, 10, 20, System.Drawing.Pens.Black);
            DrawLine(10, 20, 50, 100, System.Drawing.Pens.Black);
        }
    }
}
