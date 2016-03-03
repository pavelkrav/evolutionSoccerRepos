using System;

namespace evolutionSoccer
{
    class Program
    {
        static void Main()
        {
            Season series1 = new Season("Russia", "Brasil", 500);
            series1.printTeams();
            series1.simulateSeason();
            series1.printTeams();
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