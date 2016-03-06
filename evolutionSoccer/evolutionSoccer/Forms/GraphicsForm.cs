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

        // negative field // setted by private int negativeField() method
        // Height (y) has 38p below for some mysterious reason
        //            and 60p above because of status bar and strip menu
        // Width (x) has something on the right, i suppose, but it's insignificant
        private int negField;

        // delegate for refreshing graphs on window size changing
        // lastGraph resets in every graph-drawing method
        private delegate void drawGraph();
        private drawGraph lastGraph;

        public GraphicsForm()
        {
            Console.WriteLine("Opening a form for graphs.");
            InitializeComponent();
            _statsRecorder = null;
            negField = negativeField();

            lastGraph = () =>
            {
                drawCross("clear");
            };
        }

        public GraphicsForm(StatsRecorder _statsRecorder)
        {
            Console.WriteLine("Opening a form for graphs.");
            InitializeComponent();
            this._statsRecorder = _statsRecorder;
            negField = negativeField();

            lastGraph = () =>
            {
                drawCross("clear");
            };
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
            if (lastGraph != null)
                lastGraph();
            else
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
            // y-axis
            graphics.DrawLine(System.Drawing.Pens.Black, new Point(negField, 0), new Point(negField, this.Height));
            // x-axis
            graphics.DrawLine(System.Drawing.Pens.Black, new Point(0, this.Height - 38 - negField), new Point(this.Width, this.Height - 38 - negField)); // x axis

            drawXaxisLabels();  
        }

        // x-axis labels
        private void drawXaxisLabels()
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            for (int i = 0; i < 9; i++)
            {
                if (_statsRecorder != null)
                {
                    string label = null;
                    if (_statsRecorder.matchesPlayed[_statsRecorder.currentRecords - 1] >= 50)
                        label = Convert.ToString(_statsRecorder.matchesPlayed[_statsRecorder.currentRecords - 1] * (i + 1) / 10);
                    else
                        label = Convert.ToString(_statsRecorder.matchesPlayed[_statsRecorder.currentRecords - 1] * (i + 1) / 10.0);
                    int x = (this.Width - negField) * (i + 1) / 10 + negField;
                    int y = this.Height - 32 - negField;

                    graphics.DrawLine(Pens.Black, new Point(x, this.Height - negField - 34), new Point(x, this.Height - negField - 42)); // x-beads                                        
                    graphics.DrawString(label, new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular), Brushes.Black, new Point(x - 4 * label.Length, y));
                }
            }
            //zero point label
            graphics.DrawString("0", new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular), Brushes.Black, new Point(negField - 14, this.Height - negField - 32));
            graphics.DrawString("Matches", new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular), Brushes.Black, new Point(this.Width - 80, this.Height - negField - 20));
        }

        // y-axis labels
        private void drawYaxisLabels(int maxY)
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            for (int i = 0; i < 10; i++)/////
            {
                if (_statsRecorder != null)
                {
                    string label = null;
                    if (maxY >= 50)
                        label = Convert.ToString(maxY * (i + 1) / 10);
                    else
                        label = Convert.ToString(maxY * (i + 1) / 10.0);
                    int x = negField - 4 - label.Length * 6; 
                    int y = (this.Height - 38 - negField) - (this.Height - 98 - negField) * (i + 1) / 10;
                
                    graphics.DrawLine(Pens.Black, new Point(negField - 4, y), new Point(negField + 4, y));         // y-beads           
                    graphics.DrawString(label, new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular), Brushes.Black, new Point(x - 8, y - 8));
                }
            }
        }

        private void drawLine(double x1, double y1, double x2, double y2, Pen colour)
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            negField = negativeField();
            graphics.DrawLine(colour, new Point(negField + Convert.ToInt32(x1), this.Height - 38 - negField - Convert.ToInt32(y1)), new Point(negField + Convert.ToInt32(x2), this.Height - 38 - negField - Convert.ToInt32(y2)));
        }

        private void drawTeamStrengthGraph(int teamNumber, Pen colour)
        {
            if (teamNumber == 0 || teamNumber == 1)
            {
                int maxY = _statsRecorder.teamStrength[0, 0];
                for (int i = 0; i < _statsRecorder.teamStrength.GetLength(1); i++)
                {
                    if (maxY < _statsRecorder.teamStrength[0, i])
                        maxY = _statsRecorder.teamStrength[0, i];
                    if (maxY < _statsRecorder.teamStrength[1, i])
                        maxY = _statsRecorder.teamStrength[1, i];
                }

                drawYaxisLabels(maxY);

                double ratioX = 1.0 * (this.Width - negField) / _statsRecorder.matchesPlayed[_statsRecorder.currentRecords - 1];
                double ratioY = 1.0 * (this.Height - 98 - negField) / maxY;//////
                for (int i = 0; i < _statsRecorder.matchesPlayed.GetLength(0) - 1; i++)
                {
                    drawLine(_statsRecorder.matchesPlayed[i] * ratioX, _statsRecorder.teamStrength[teamNumber,i] * ratioY, _statsRecorder.matchesPlayed[i+1] * ratioX, _statsRecorder.teamStrength[teamNumber, i+1] * ratioY, colour);
                }
            }
        }

        private void clearGraphsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawCross("clear");
        }

        private void teamStrengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lastGraph = () =>
            {
                drawCross("clear");
                drawTeamStrengthGraph(0, Pens.Blue);
                drawTeamStrengthGraph(1, Pens.Red);
            };
            lastGraph();
        }

        private void openGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string path = AppDomain.CurrentDomain.BaseDirectory;
            //Console.WriteLine(path);
            Process.Start("..\\..\\info\\Guide.txt");
        }
    }
}
