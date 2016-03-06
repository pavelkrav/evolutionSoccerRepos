using System;

namespace evolutionSoccer
{
    class Season
    {
        private Team[] team;
        public int totalMatches { get; }
        private int playedMatches;
        private int[] wins; // wins[2] = draws
        public StatsRecorder _statsRecorder { get; }

        public Season (string team1, string team2, int matches)
        {
            totalMatches = matches;
            playedMatches = 0;
            wins = new int[3] { 0, 0, 0 };
            team = new Team[2];
            team[0] = new Team(team1, 15, 5); // initial conditions
            team[1] = new Team(team2, 15, 5); // initial conditions
            printTeams("(at the beginning of the season):");
            _statsRecorder = new StatsRecorder(totalMatches);
            recordStats();
        }

        private void recordStats()
        {
            int i = _statsRecorder.currentRecords;

            if (i < Convert.ToInt32(Math.Floor(1.0 * totalMatches / _statsRecorder.frequency)) + 2)
            {
                _statsRecorder.matchesPlayed[i] = playedMatches;

                _statsRecorder.teamStrength[0, i] = team[0].teamStrength;
                _statsRecorder.teamStrength[1, i] = team[1].teamStrength;
                //...
            }

            _statsRecorder.currentRecords++;
        }

        public void printTeams()
        {
            team[0].printTeam();
            team[1].printTeam();
        }

        public void printTeams(String message)
        {
            team[0].printTeam(message);
            team[1].printTeam(message);
        }

        private int simulateMatch()
        {
            Match match = new Match(team[0], team[1]);
            //match.showScore();                              // showing score line
            if (match.winner >= 0)
            {
                team[match.looser].looses++;
                team[match.looser].looseStreak++;  
                team[match.winner].wins++;
                team[match.winner].looseStreak = 0;  // looseStreak only resets after a win, not a draw (should be fixed??)

                wins[match.winner]++;

                team[match.looser] = new Team(team[match.looser], team[match.winner], match.goals[match.looser] - match.goals[match.winner]);
            }
            else
            {
                wins[2]++;
            }
            playedMatches++;

            if (playedMatches % _statsRecorder.frequency == 0)
            {
                recordStats();
            }

            return match.winner;
        }

        public void simulateSeries(int matches)
        {
            if (matches > totalMatches - playedMatches)
                matches = totalMatches - playedMatches;
            for (int i = 0; i < matches; i++)
            {
                simulateMatch();
            }
        }

        public void printScore()
        {
            Console.WriteLine("Total in season:\n{0}  {1} - {2}  {3}\nDraws - {4}\n", team[0].name, wins[0], wins[1], team[1].name, wins[2]);
        }

        public void simulateSeason()
        {
            simulateSeries(totalMatches);
            printTeams("(at the ending of the season):");
            printScore();
            recordStats();            
        }

    }
}
