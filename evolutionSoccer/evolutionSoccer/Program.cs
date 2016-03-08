using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace evolutionSoccer
{
    class Program
    {
        private String teamName1;
        private String teamName2;
        private int matches;
        private Season season;
        private bool isSimulated;

        private delegate int menuDelegate();

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
            matches = 800;
            season = new Season(teamName1, teamName2, matches);
            isSimulated = false;
        }

        private void Menu()
        {
            menuDelegate menu = null;
            int sw = 1;
            while (sw != 0)
            {
                if (isSimulated)
                    menu = mainMenu2;
                else
                    menu = mainMenu1;
                switch (menu())
                {
                    case 0:
                        sw = 0;
                        break;
                    case 1:
                        Console.Clear();
                        season = new Season(teamName1, teamName2, matches);
                        season.simulateSeason();
                        isSimulated = true;
                        Console.WriteLine("Press any key... ");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        season.printTeams();
                        season.printScore();
                        Console.WriteLine("Press any key... ");
                        Console.ReadKey();
                        break;                    
                    case 3:
                        //for (int i = 0; i < season._statsRecorder.matchesPlayed.GetLength(0); i++) // checking plausibility of graphs
                        //    Console.WriteLine("{0}   {1}", season._statsRecorder.teamStrength[0, i], season._statsRecorder.teamStrength[1, i]);
                        Application.EnableVisualStyles();
                        Application.Run(new GraphicsForm(season._statsRecorder));
                        break;
                    case 4:
                        Console.Clear();
                        Console.Write("Input new season length... ");
                        int n = int.Parse(Console.ReadLine()); // need exception here
                        if (n > 0)
                        {
                            matches = n;
                            season = new Season(teamName1, teamName2, matches);
                        }
                        break;
                    case 9:
                        Process.Start("..\\..\\info\\Guide.txt");
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }

        private int mainMenu1()
        {
            int[] choice = new int[4] { 0, 1, 4, 9 };
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("1 - Run new simulation ({0} matches)", matches);
            Console.WriteLine("2 - Change season length");
            Console.WriteLine("3 - Help");

            Console.WriteLine("0 - Exit\n");
            Console.Write("Go to... ");

            int rt = choice[0];
            try
            {
                rt = choice[int.Parse(Console.ReadLine())];
            }
            catch (System.FormatException)
            {
                return mainMenu1();
            }
            catch (System.IndexOutOfRangeException)
            {
                return mainMenu1();
            }
            return rt;
        }

        private int mainMenu2()
        {
            int[] choice = new int[6] { 0, 1, 2, 3, 4, 9 };
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("1 - Run new simulation ({0} matches)", matches);
            Console.WriteLine("2 - Show last season statistics");
            Console.WriteLine("3 - Show graphs");
            Console.WriteLine("4 - Change season length");
            Console.WriteLine("5 - Help");

            Console.WriteLine("0 - Exit\n");
            Console.Write("Go to... ");

            int rt = choice[0];
            try
            {
                rt = choice[int.Parse(Console.ReadLine())];
            }
            catch (System.FormatException)
            {
                return mainMenu2();
            }
            catch (System.IndexOutOfRangeException)
            {
                return mainMenu2();
            }
            return rt;
        }
    }
}