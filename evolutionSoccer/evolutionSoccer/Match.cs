using System;
using System.Linq;
using System.Threading;

namespace evolutionSoccer
{
    class Match
    {
        private Team[] team;
        private int[] attacks;
        public int[] goals { get; }
        public int winner { get; }
        public int looser { get; }
        private string[] resultState;

        private int simulateMatch()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int[] attacksCountMod = new int[2] { (team[0].cenStrength * 10 + team[0].attStrength), (team[1].cenStrength * 10 + team[1].attStrength) };
            int summaryAttacks = Convert.ToInt32(1.0 * attacksCountMod.Sum() / (team[0].teamStrength + team[1].teamStrength) * 10) + rand.Next(16);
            attacks[0] = Convert.ToInt32(1.0 * attacksCountMod[0] / attacksCountMod.Sum() * summaryAttacks);
            attacks[1] = Convert.ToInt32(1.0 * attacksCountMod[1] / attacksCountMod.Sum() * summaryAttacks);
            int[] attackMod = new int[2] { team[0].attStrength * 11 + team[0].cenStrength * 2, team[1].attStrength * 11 + team[1].cenStrength * 2 };
            int[] defenceMod = new int[2] { team[0].defStrength * 11 + team[0].cenStrength * 4, team[1].defStrength * 11 + team[1].cenStrength * 4 };
            //Console.WriteLine("Summary attacks: {0}", summaryAttacks);
            //Console.WriteLine("Team1 attacks: {0}", attacks[0]);
            //Console.WriteLine("Team2 attacks: {0}", attacks[1]);

            for (int i = 0; i < attacks[0]; i++)
            {
                int chanceOnTarget = Convert.ToInt32(1.0 * attackMod[0] / defenceMod[1] * 100 * team[0].teamAvgStrength / 100);
                chanceOnTarget = Convert.ToInt32(100.0 * (Math.Sin(chanceOnTarget / 300.0)));
                if (chanceOnTarget > 50)
                    chanceOnTarget = 50;
                int chanceToSave = Convert.ToInt32(1.0 * 30 * Math.Sin(team[1].gkStrength / 100.0));
                int rollShot = rand.Next(100) + 1;
                int rollSave = rand.Next(100) + 1;
                if (chanceOnTarget >= rollShot && chanceToSave < rollSave)
                    goals[0]++;
                //Console.WriteLine("Team1::   ChanceOnTarget: {0}   ChanceToSave: {1}", chanceOnTarget, chanceToSave);
            }
            for (int i = 0; i < attacks[1]; i++)
            {
                int chanceOnTarget = Convert.ToInt32(1.0 * attackMod[1] / defenceMod[0] * 100 * team[1].teamAvgStrength / 100);
                chanceOnTarget = Convert.ToInt32(100.0 * (Math.Sin(chanceOnTarget / 300.0)));
                if (chanceOnTarget > 50)
                    chanceOnTarget = 50;
                int chanceToSave = Convert.ToInt32(1.0 * 30 * Math.Sin(team[0].gkStrength / 100.0));
                int rollShot = rand.Next(100) + 1;
                int rollSave = rand.Next(100) + 1;
                if (chanceOnTarget >= rollShot && chanceToSave < rollSave)
                    goals[1]++;
                //Console.WriteLine("Team2::   ChanceOnTarget: {0}   ChanceToSave: {1}", chanceOnTarget, chanceToSave);
            }

            if (goals[0] > goals[1])
                return 0;
            else if (goals[0] < goals[1])
                return 1;
            else return -1;
        }

        public void showScore()
        {
            Console.WriteLine("{4} {0} {1} - {2} {3} {5}\n", team[0].name, goals[0], goals[1], team[1].name, resultState[0], resultState[1]);

        }

        public Match(Team team1, Team team2)
        {
            team = new Team[2];
            team[0] = team1;
            team[1] = team2;

            attacks = new int[2];
            goals = new int[2];

            winner = simulateMatch();
            looser = (winner - 1) * (-1);
            resultState = new string[2];
            if (winner < 0)
            {
                resultState[0] = "[Draw]";
                resultState[1] = "[Draw]";
            }
            else
            {
                resultState[winner] = "[Winner]";
                resultState[looser] = "[Looser]";
            }

        }
    }
}