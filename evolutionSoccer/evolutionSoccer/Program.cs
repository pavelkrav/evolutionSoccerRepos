using System;
using System.Windows.Forms;

namespace evolutionSoccer
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Season series1 = new Season("Russia", "Brasil", 200);            
            series1.printTeams();
            series1.simulateSeason();
            series1.printTeams();
            Application.EnableVisualStyles();
            Application.Run(new GraphicsForm());
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