using System;
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

        public GraphicsForm()
        {
            Console.WriteLine("Opening a form for graphs");
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawCross();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            DrawCross();
        }

        private void DrawCross()
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            graphics.Clear(Color.White);
            graphics.DrawLine(System.Drawing.Pens.Black, new Point(this.Width / 20, 0), new Point(this.Width / 20, this.Height / 2));
            graphics.DrawLine(System.Drawing.Pens.Black, new Point(0, this.Height / 2 - this.Width / 20), new Point(this.Width, this.Height / 2 - this.Width / 20));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawCross();
        }

    }
}
