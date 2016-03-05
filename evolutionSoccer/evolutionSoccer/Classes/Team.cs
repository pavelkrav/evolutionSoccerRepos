using System;
using System.Linq;
using System.Threading;

namespace evolutionSoccer
{
    class Team
    {
        public string name { get; }
        private int gk;
        private int[] def;
        private int[] cen;
        private int[] att;

        public int looseStreak { get; set; }
        public int looses { get; set; }
        public int wins { get; set; }

        public int defSize
        { get { return def.GetLength(0); } }
        public int cenSize
        { get { return cen.GetLength(0); } }
        public int attSize
        { get { return att.GetLength(0); } }

        public int defStrength
        { get { return def.Sum(); } }
        public int cenStrength
        { get { return cen.Sum(); } }
        public int attStrength
        { get { return att.Sum(); } }
        public int gkStrength
        { get { return gk; } }
        public int teamStrength
        { get { return defStrength + cenStrength + attStrength + gk; } }
        public int teamAvgStrength
        { get { return (defStrength + cenStrength + attStrength + gk) / 11; } }

        private delegate int formula(int x);

        private int formulaFull(int x)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int shift = Convert.ToInt32(Math.Floor(Convert.ToDouble(3 / (rand.Next(2) + 1)))) - 2;
            int res = x + looseStreak * shift;
             int max = 95 + rand.Next(11);
            if (res > max) // replace with event
                res = max;
            if (res < 10)
                res = 10;
            return res;
        }
        private int formulaHalf(int x)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int shift = Convert.ToInt32(Math.Floor(Convert.ToDouble(3 / (rand.Next(2) + 1)))) - 2;
            int res = x + Convert.ToInt32(Math.Ceiling(Convert.ToDouble(looseStreak / 2))) * shift;
            int max = 95 + rand.Next(11);
            if (res > max) // replace with event
                res = max;
            if (res < 10)
                res = 10;
            return res;
        }
        private int formulaThird(int x)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int shift = Convert.ToInt32(Math.Floor(Convert.ToDouble(3 / (rand.Next(2) + 1)))) - 2;
            int res = x + Convert.ToInt32(Math.Ceiling(Convert.ToDouble(looseStreak / 3))) * shift;
            int max = 95 + rand.Next(11);
            if (res > max) // replace with event
                res = max;
            if (res < 10)
                res = 10;
            return res;
        }

        private void switchStrategy(int times)
        {
            while (times > 0)
            {
                int[] fullTeam = new int[10];
                for (int i = 0; i < defSize; i++)
                    fullTeam[i] = def[i];
                for (int i = 0; i < cenSize; i++)
                    fullTeam[i + defSize] = cen[i];
                for (int i = 0; i < attSize; i++)
                    fullTeam[i + defSize + cenSize] = att[i];

                Random rand = new Random(Guid.NewGuid().GetHashCode());
                int defShift = 0;
                int cenShift = 0;
                int attShift = 0;
                do
                {
                    do { defShift = rand.Next(3) - 1; }
                    while ((defSize + defShift) < 2);
                    if (defShift == 0)
                    {
                        cenShift = Convert.ToInt32(Math.Floor(Convert.ToDouble(3 / (rand.Next(2) + 1)))) - 2;
                        attShift = cenShift * (-1);
                    }
                    else
                    {
                        cenShift = rand.Next(2) * (-1) * defShift;
                        attShift = (defShift + cenShift) * (-1);
                    }
                }
                while (((attSize + attShift) < 1) || ((cenSize + cenShift) < 2));

                this.def = new int[defSize + defShift];
                this.cen = new int[cenSize + cenShift];
                this.att = new int[attSize + attShift];
                for (int i = 0; i < defSize; i++)
                    def[i] = fullTeam[i];
                for (int i = 0; i < cenSize; i++)
                    cen[i] = fullTeam[i + defSize];
                for (int i = 0; i < attSize; i++)
                    att[i] = fullTeam[i + defSize + cenSize];

                times--;
            }
        }

        public void printTeam()
        {
            Console.WriteLine("Team \"{0}\"", name);
            Console.WriteLine("Goalkeeper:\t{0}", this.gk);
            Console.Write("Defence:   \t");
            for (int i = 0; i < this.defSize; i++)
                Console.Write("{0}\t", def[i]);
            Console.WriteLine();
            Console.Write("Center:    \t");
            for (int i = 0; i < this.cenSize; i++)
                Console.Write("{0}\t", cen[i]);
            Console.WriteLine();
            Console.Write("Attack:    \t");
            for (int i = 0; i < this.attSize; i++)
                Console.Write("{0}\t", att[i]);
            Console.WriteLine("\nTotal:     \t{0}\n", teamStrength);
        }

        public void printTeam(String message)
        {
            Console.WriteLine("Team \"{0}\" " + message, name);
            Console.WriteLine("Goalkeeper:\t{0}", this.gk);
            Console.Write("Defence:   \t");
            for (int i = 0; i < this.defSize; i++)
                Console.Write("{0}\t", def[i]);
            Console.WriteLine();
            Console.Write("Center:    \t");
            for (int i = 0; i < this.cenSize; i++)
                Console.Write("{0}\t", cen[i]);
            Console.WriteLine();
            Console.Write("Attack:    \t");
            for (int i = 0; i < this.attSize; i++)
                Console.Write("{0}\t", att[i]);
            Console.WriteLine("\nTotal:     \t{0}\n", teamStrength);
        }


        public Team()
        {
            name = "Untitled";
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            gk = rand.Next(11) + 15;
            def = new int[4] { rand.Next(11) + 15, rand.Next(11) + 15, rand.Next(11) + 15, rand.Next(11) + 15 };
            cen = new int[4] { rand.Next(11) + 15, rand.Next(11) + 15, rand.Next(11) + 15, rand.Next(11) + 15 };
            att = new int[2] { rand.Next(11) + 15, rand.Next(11) + 15 };
            looseStreak = 0;
            looses = 0;
            wins = 0;
        }

        public Team(string name, int avgStats, int err)
        {

            if ((avgStats - err) < 10) // replace with event
                avgStats = err + 10;
            if ((avgStats + err) > 100)
                avgStats = 100 - err;
            this.name = name;
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            gk = rand.Next(err + 1) + avgStats;
            def = new int[4] { rand.Next(err + 1) + avgStats, rand.Next(err + 1) + avgStats, rand.Next(err + 1) + avgStats, rand.Next(err + 1) + avgStats };
            cen = new int[4] { rand.Next(err + 1) + avgStats, rand.Next(err + 1) + avgStats, rand.Next(err + 1) + avgStats, rand.Next(err + 1) + avgStats };
            att = new int[2] { rand.Next(err + 1) + avgStats, rand.Next(err + 1) + avgStats };
            looseStreak = 0;
            looses = 0;
            wins = 0;
        }

        public Team(Team parent)
        {
            this.name = parent.name;
            this.gk = parent.gk;
            this.def = parent.def;
            this.cen = parent.cen;
            this.att = parent.att;
            this.looseStreak = parent.looseStreak;
            this.looses = parent.looses;
            this.wins = parent.wins;

            switchStrategy(Convert.ToInt32(Math.Floor(Convert.ToDouble(looseStreak / 2)))); // evolutionary significant constant (/ n)

            formula fm = null;
            if (wins / (wins + looses) < 0.4)
                fm = formulaFull;
            else if (wins / (wins + looses) < 0.6)
                fm = formulaHalf;
            else
                fm = formulaThird;

            for (int i = 0; i < defSize; i++)
                def[i] = fm(def[i]);
            for (int i = 0; i < cenSize; i++)
                cen[i] = fm(cen[i]);
            for (int i = 0; i < attSize; i++)
                att[i] = fm(att[i]);
            gk = fm(gk);

        }

        public Team(Team parent, Team enemy, int advantage) //evolve
        {
            Team evolved = parent;
            for (int i = 0; i < parent.looseStreak * 10; i++)    // evolutionary significant constant (* n)
            {
                Team child = new Team(parent);
                Match simulation = new Match(child, enemy);
                if (advantage < simulation.goals[0] - simulation.goals[1])
                {
                    advantage = simulation.goals[0] - simulation.goals[1];
                    evolved = child;
                }
            }

            this.name = evolved.name;
            this.gk = evolved.gk;
            this.def = evolved.def;
            this.cen = evolved.cen;
            this.att = evolved.att;
            this.looseStreak = evolved.looseStreak;
            this.looses = evolved.looses;
            this.wins = evolved.wins;
            
        }
    }
}