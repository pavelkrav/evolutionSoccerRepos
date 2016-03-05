﻿using System;

namespace evolutionSoccer
{
    public class StatsRecorder
    {
        // recordStats method in class Season
        // should be used as a property or a field of class Season

        public int[] matchesPlayed { get; set; }
        public int[,] teamStrength { get; set;}
        public int frequency { get; }
        public int currentRecords { get; set; }

        public StatsRecorder(int totalMatches)
        {
            currentRecords = 0;

            if (totalMatches >= 10)
                frequency = Convert.ToInt32(Math.Pow(10, totalMatches.ToString().Length - 2));
            else
                frequency = 1;

            int c = Convert.ToInt32(Math.Floor(1.0 * totalMatches / frequency)) + 2;
            matchesPlayed = new int[c];
            teamStrength = new int[2,c];
        }
    }
}
