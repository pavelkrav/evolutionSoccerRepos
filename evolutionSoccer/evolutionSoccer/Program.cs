using System;
using System.Windows.Forms;

namespace evolutionSoccer
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Season season1 = new Season("Russia", "Brasil", 200);            
            season1.simulateSeason();
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