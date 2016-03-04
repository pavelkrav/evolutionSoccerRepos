using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evolutionSoccer
{
    public class StatsRecorder
    {
        public int[,] teamStrength { get; set;}
        public int frequency { get; }

        public StatsRecorder(int totalMatches)
        {
            if (totalMatches >= 10)
                frequency = Convert.ToInt32(Math.Pow(10, totalMatches.ToString().Length - 2));
            else
                frequency = 1;
            int c = Convert.ToInt32(Math.Floor(1.0 * totalMatches / frequency));
            teamStrength = new int[2,c];
        }
    }
}
