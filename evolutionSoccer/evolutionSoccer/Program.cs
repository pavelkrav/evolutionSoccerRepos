using System;
using System.Windows.Forms;

namespace evolutionSoccer
{
    class Program
    {
        private String teamName1;
        private String teamName2;
        private int matches;
        private Season season;

        [STAThread]
        static void Main()
        {
            Program program = new Program();                        
            program.Menu();               
        }

        Program()
        {
            teamName1 = "Russia";
            teamName2 = "Brazil";
            matches = 400;
            season = new Season(teamName1, teamName2, matches);
        }

        private void Menu()
        {
            int sw = 1;
            while (sw != 0)
            {
                switch (mainMenu())
                {
                    case 0:
                        sw = 0;
                        break;
                    case 1:
                        Console.Clear();
                        season.printTeams("(at the beginning of the season):");
                        season.simulateSeason();
                        Console.WriteLine("Press any key... ");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        Console.Write("Input new season length... ");
                        matches = int.Parse(Console.ReadLine());
                        season = new Season(teamName1, teamName2, matches);
                        break;
                    case 3:
                        Application.EnableVisualStyles();
                        Application.Run(new GraphicsForm(season._statsRecorder));
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }

        private int mainMenu()
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("1 - Run new simulation ({0} matches)", matches);
            Console.WriteLine("2 - Change season length");
            Console.WriteLine("3 - Show graphs");

            Console.WriteLine("0 - Exit\n");
            Console.Write("Go to... ");

            int rt = 0;
            try
            {
                rt = int.Parse(Console.ReadLine());
            }
            catch(System.FormatException)
            {
                    return mainMenu();
            }
            return rt;
        }
    }
}