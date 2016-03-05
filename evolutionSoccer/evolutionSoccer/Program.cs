using System;
using System.Windows.Forms;

namespace evolutionSoccer
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Season season1 = new Season("Russia", "Brasil", 400);            
            season1.simulateSeason();
            //Console.WriteLine("frequency: {0}, size: {1}", season1._statsRecorder.frequency, Convert.ToInt32(Math.Floor(1.0 * season1.totalMatches / season1._statsRecorder.frequency)) + 1);
            //for (int i = 0; i < Convert.ToInt32(Math.Floor(1.0 * season1.totalMatches / season1._statsRecorder.frequency)) + 2; i++)
            //{
            //    Console.WriteLine("{2} {0}\t{1}", season1._statsRecorder.teamStrength[0, i], season1._statsRecorder.teamStrength[1, i], season1._statsRecorder.matchesPlayed[i]);
            //}
            Application.EnableVisualStyles();
            Application.Run(new GraphicsForm(season1._statsRecorder));
            Console.ReadKey();

            //for (int i = 0; i < 35; i++) // Testing scores
            //{
            //    Match match1 = new Match(new Team("Russia", 20, 5), new Team("Brazil", 20, 5));
            //    match1.showScore();
            //}
            //Console.ReadKey();
        }
    }
}