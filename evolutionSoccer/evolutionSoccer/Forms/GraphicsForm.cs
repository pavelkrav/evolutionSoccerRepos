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
        // it records last graph drawing method
        // pens are containing colours both teams' graphs will be drawn with
        private delegate void graphDelegate(Pen colour1, Pen colour2);
        private graphDelegate lastGraph;
        private Pen teamColour1;
        private Pen teamColour2;

        public GraphicsForm()
        {
            Console.WriteLine("Opening a form for graphs.");
            InitializeComponent();
            _statsRecorder = null;
            negField = negativeField();

            teamColour1 = Pens.Blue;
            teamColour2 = Pens.Red;
            lastGraph = (teamColour1, teamColour2) =>
            {
                drawCross();
            };
        }

        public GraphicsForm(StatsRecorder _statsRecorder)
        {
            Console.WriteLine("Opening a form for graphs.");
            InitializeComponent();
            this._statsRecorder = _statsRecorder;
            negField = negativeField();

            teamColour1 = Pens.Blue;
            teamColour2 = Pens.Red;
            lastGraph = (teamColour1, teamColour2) =>
            {
                drawCross();
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

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            drawCross();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (lastGraph != null)
                lastGraph(teamColour1, teamColour2);
            else
                drawCross();
        }

        private void drawCross()
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            graphics.Clear(Color.White);
            // remold negative field in case of window size changing
            negField = negativeField();
            // y-axis
            graphics.DrawLine(System.Drawing.Pens.Black, new Point(negField, 0), new Point(negField, this.Height));
            // x-axis
            graphics.DrawLine(System.Drawing.Pens.Black, new Point(0, this.Height - 38 - negField), new Point(this.Width, this.Height - 38 - negField)); // x axis

            drawXaxisLabels();
        }

        private void drawCross(int[] statsArray)
        {
            drawCross();
            int maxY = 0;
            if (statsArray.GetLength(0) > 0)
            {
                maxY = statsArray[0];
                for (int i = 0; i < statsArray.GetLength(0); i++)
                {
                    if (maxY < statsArray[i])
                        maxY = statsArray[i];
                }
            }

            drawYaxisLabels(maxY);
        }

        private void drawCross(int[,] statsArray)
        {
            drawCross();
            int maxY = 0;
            if (statsArray.GetLength(0) > 0 && statsArray.GetLength(1) > 0)
            {
                maxY = statsArray[0, 0];
                for (int i = 0; i < statsArray.GetLength(1); i++)
                {
                    if (maxY < statsArray[0, i])
                        maxY = statsArray[0, i];
                    if (maxY < statsArray[1, i])
                        maxY = statsArray[1, i];
                }
            }

            drawYaxisLabels(maxY);
        }

        // x-axis labels
        private void drawXaxisLabels()
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            if (_statsRecorder != null)
            {
                for (int i = 0; i < 9; i++)
                {
                    string label = null;
                    if (_statsRecorder.matchesPlayed[_statsRecorder.currentRecords - 1] >= 50)
                        label = Convert.ToString(_statsRecorder.matchesPlayed[_statsRecorder.currentRecords - 1] * (i + 1) / 10);
                    else
                        label = Convert.ToString(_statsRecorder.matchesPlayed[_statsRecorder.currentRecords - 1] * (i + 1) / 10.0);
                    int x = (this.Width - negField) * (i + 1) / 10 + negField;
                    int y = this.Height - 32 - negField;

                    graphics.DrawLine(Pens.LightGray, new Point(x, this.Height - 38 - negField), new Point(x, 0));                      // coordinate grid    
                    graphics.DrawLine(Pens.Black, new Point(x, this.Height - negField - 34), new Point(x, this.Height - negField - 42)); // x-beads                                    
                    graphics.DrawString(label, new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular), Brushes.Black, new Point(x - 4 * label.Length, y)); // labels
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
            if (maxY > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    string label = null;
                    if (maxY >= 50)
                        label = Convert.ToString(maxY * (i + 1) / 10);
                    else
                        label = Convert.ToString(maxY * (i + 1) / 10.0);
                    int x = negField - 4 - label.Length * 6;
                    int y = (this.Height - 38 - negField) - (this.Height - 98 - negField) * (i + 1) / 10;

                    graphics.DrawLine(Pens.LightGray, new Point(this.Width, y), new Point(negField, y));              // coordinate grid
                    graphics.DrawLine(Pens.Black, new Point(negField - 4, y), new Point(negField + 4, y));         // y-beads       
                    graphics.DrawString(label, new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular), Brushes.Black, new Point(x - 8, y - 8)); // labels
                }
                // team labels
                //graphics.DrawString("Matches", new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular), Brushes.Black, new Point(this.Width - 80, this.Height - negField - 20));
            }
            else if (maxY == 0)
            {
                string label = "1";
                int x = negField - 4 - label.Length * 6;
                int y = (this.Height - 38 - negField) - (this.Height - 98 - negField) * 9 / 10;

                graphics.DrawLine(Pens.Black, new Point(negField - 4, y), new Point(negField + 4, y));                   
                graphics.DrawString(label, new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular), Brushes.Black, new Point(x - 8, y - 8));
            }
        }

        // draw a line about the coordinate system, not the form
        private void drawLine(double x1, double y1, double x2, double y2, Pen colour)
        {
            if (colour != null)
            {
                System.Drawing.Graphics graphics = this.CreateGraphics();
                negField = negativeField();
                graphics.DrawLine(colour, new Point(negField + Convert.ToInt32(x1), this.Height - 38 - negField - Convert.ToInt32(y1)), new Point(negField + Convert.ToInt32(x2), this.Height - 38 - negField - Convert.ToInt32(y2)));
            }
        }

        private void drawGraph(int[,] statsArray, int teamNumber, Pen colour)
        {
            if (statsArray.GetLength(0) > 0 && statsArray.GetLength(1) > 0)
            {
                int maxY = statsArray[0, 0];
                for (int i = 0; i < statsArray.GetLength(1); i++)
                {
                    if (maxY < statsArray[0, i])
                        maxY = statsArray[0, i];
                    if (maxY < statsArray[1, i])
                        maxY = statsArray[1, i];
                }

                double ratioX = 1.0 * (this.Width - negField) / _statsRecorder.matchesPlayed[_statsRecorder.currentRecords - 1];
                double ratioY = 0;
                if (maxY > 0)
                    ratioY = 1.0 * (this.Height - 98 - negField) / maxY;
                for (int i = 0; i < _statsRecorder.matchesPlayed.GetLength(0) - 1; i++)
                {
                    drawLine(_statsRecorder.matchesPlayed[i] * ratioX, statsArray[teamNumber, i] * ratioY, _statsRecorder.matchesPlayed[i + 1] * ratioX, statsArray[teamNumber, i + 1] * ratioY, colour);
                }
            }
        }

        private void drawGraph(int[] statsArray, Pen colour)
        {
            if (statsArray.GetLength(0) > 0)
            {
                int maxY = statsArray[0];
                for (int i = 0; i < statsArray.GetLength(0); i++)
                {
                    if (maxY < statsArray[i])
                        maxY = statsArray[i];
                }

                double ratioX = 1.0 * (this.Width - negField) / _statsRecorder.matchesPlayed[_statsRecorder.currentRecords - 1];
                double ratioY = 0;
                if (maxY > 0)
                    ratioY = 1.0 * (this.Height - 98 - negField) / maxY;
                for (int i = 0; i < _statsRecorder.matchesPlayed.GetLength(0) - 1; i++)
                {
                    drawLine(_statsRecorder.matchesPlayed[i] * ratioX, statsArray[i] * ratioY, _statsRecorder.matchesPlayed[i + 1] * ratioX, statsArray[i + 1] * ratioY, colour);
                }
            }
        }

        private void drawTeamStrengthGraph(int teamNumber, Pen colour)
        {
            if (teamNumber < _statsRecorder.teamStrength.GetLength(0) && teamNumber >= 0)
            {
                drawGraph(_statsRecorder.teamStrength, teamNumber, colour);
            }
        }

        private void drawWinsGraph(int teamNumber, Pen colour)
        {
            if (teamNumber < _statsRecorder.wins.GetLength(0) && teamNumber >= 0)
            {
                drawGraph(_statsRecorder.wins, teamNumber, colour);
            }
        }

        private void drawDrawsGraph(Pen colour)
        {
            drawGraph(_statsRecorder.draws, colour);
        }

        private void drawLooseStreakGraph(int teamNumber, Pen colour)
        {
            if (teamNumber < _statsRecorder.looseStreak.GetLength(0) && teamNumber >= 0)
            {
                drawGraph(_statsRecorder.looseStreak, teamNumber, colour);
            }
        }

        private void clearGraphsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lastGraph = (teamColour1, teamColour2) =>
            {
                drawCross();
            };
            Console.WriteLine("Graphs are cleared");
            lastGraph(teamColour1, teamColour2);
        }

        private void teamStrengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lastGraph = (teamColour1, teamColour2) =>
            {
                drawCross(_statsRecorder.teamStrength);
                drawTeamStrengthGraph(0, teamColour1);
                drawTeamStrengthGraph(1, teamColour2);
            };
            Console.WriteLine("Building graph showing teams strength through simulated matches");
            lastGraph(teamColour1, teamColour2);
        }

        private void winsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lastGraph = (teamColour1, teamColour2) =>
            {
                drawCross(_statsRecorder.wins);
                drawWinsGraph(0, teamColour1);
                drawWinsGraph(1, teamColour2);
            };
            Console.WriteLine("Building graph showing wins through simulated matches");
            lastGraph(teamColour1, teamColour2);
        }

        private void drawsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lastGraph = (teamColour1, teamColour2) =>
            {
                drawCross(_statsRecorder.draws);
                if (teamColour1 != null)
                    drawDrawsGraph(teamColour1);
                else if (teamColour2 != null)
                    drawDrawsGraph(teamColour2);
                else drawDrawsGraph(Pens.Black);
            };
            Console.WriteLine("Building graph showing draws through simulated matches");
            lastGraph(teamColour1, teamColour2);
        }

        private void looseStreakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lastGraph = (teamColour1, teamColour2) =>
            {
                drawCross(_statsRecorder.looseStreak);
                drawLooseStreakGraph(0, teamColour1);
                drawLooseStreakGraph(1, teamColour2);
            };
            Console.WriteLine("Building graph showing loose streaks through simulated matches");
            lastGraph(teamColour1, teamColour2);
        }

        private void openGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string path = AppDomain.CurrentDomain.BaseDirectory;
            //Console.WriteLine(path);
            Process.Start("..\\..\\info\\Guide.txt");
        }

        //
        // block of code that provides changing graphs' colours
        //
        private void changeColour(int teamNumber, Pen colour)
        {
            if (teamNumber == 0)
            {
                teamColour1 = colour;
            }
            else if (teamNumber == 1)
                teamColour2 = colour;
            lastGraph(teamColour1, teamColour2);
        }

        //
        // Team 1
        //
        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeColour(0, Pens.Blue);
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeColour(0, Pens.Red);
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeColour(0, Pens.Green);
        }

        private void orangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeColour(0, Pens.Orange);
        }

        private void violetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeColour(0, Pens.Violet);
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeColour(0, null);
        }

        //
        //Team 2
        //
        private void blueToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            changeColour(1, Pens.Blue);
        }

        private void redToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            changeColour(1, Pens.Red);
        }

        private void greenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            changeColour(1, Pens.Green);
        }

        private void orangeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            changeColour(1, Pens.Orange);
        }

        private void violetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            changeColour(1, Pens.Violet);
        }

        private void noneToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            changeColour(1, null);
        }
        //
        // end of block
        //

    }
}
