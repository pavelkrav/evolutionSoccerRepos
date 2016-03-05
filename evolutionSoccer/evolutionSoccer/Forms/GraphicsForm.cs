using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace evolutionSoccer
{
    public partial class GraphicsForm : Form
    {        
        private StatsRecorder _statsRecorder;
        private int negField; // negative field (height for some reason have 38 additional points)

        public GraphicsForm()
        {
            Console.WriteLine("Opening a form for graphs.");
            InitializeComponent();
            _statsRecorder = null;
            negField = negativeField();
        }

        public GraphicsForm(StatsRecorder _statsRecorder)
        {
            Console.WriteLine("Opening a form for graphs.");
            InitializeComponent();
            this._statsRecorder = _statsRecorder;
            negField = negativeField();
        }

        private int negativeField() // negative field method
        {
            return (this.Height + this.Width) / 30;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Console.WriteLine("Closing graphs form.");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            drawCross("none");
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            drawCross("clear");
        }

        private void drawCross(String arg)
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();

            // dont need form clearing on paint
            if (arg == "clear")
                graphics.Clear(Color.White);

            // remold negative field in case of window size changing
            negField = negativeField();
            // y axis
            graphics.DrawLine(System.Drawing.Pens.Black, new Point(negField, 0), new Point(negField, this.Height));
            // x axis
            graphics.DrawLine(System.Drawing.Pens.Black, new Point(0, this.Height - 38 - negField), new Point(this.Width, this.Height - 38 - negField)); // x axis

            //x axis labels
            for (int i = 0; i < 9; i++)
            {
                int x = (this.Width - negField) * (i + 1) / 10 + negField;
                int y = this.Height - 32 - negField;
                if (_statsRecorder != null)
                {
                    graphics.DrawLine(Pens.Black, new Point(x, this.Height - negField - 34), new Point(x, this.Height - negField - 42));
                    string label = Convert.ToString(_statsRecorder.matchesPlayed[_statsRecorder.currentRecords - 1] * (i + 1) / 10);                    
                    graphics.DrawString(label, new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular), Brushes.Black, new Point(x - 4 * label.Length, y));
                }
            }
            //zero point label
            graphics.DrawString("0", new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular), Brushes.Black, new Point(negField - 12, this.Height - negField - 32));
        }

        public void drawLine(double x1, double y1, double x2, double y2, Pen colour) // should be private later
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            negField = negativeField();
            graphics.DrawLine(colour, new Point(negField + Convert.ToInt32(x1), this.Height - 38 - negField - Convert.ToInt32(y1)), new Point(negField + Convert.ToInt32(x2), this.Height - 38 - negField - Convert.ToInt32(y2)));
        }

        private void clearGraphsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawCross("clear");
        }

        private void teamStrengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawLine(0, 0, 10, 10, System.Drawing.Pens.Black);
            drawLine(10, 10, 10, 20, System.Drawing.Pens.Black);
            drawLine(10, 20, 50, 100, System.Drawing.Pens.Black);
        }

        private void openGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string path = AppDomain.CurrentDomain.BaseDirectory;
            //Console.WriteLine(path);
            Process.Start("..\\..\\info\\Guide.txt");
        }
    }
}
