using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent_of_Code_2021.Puzzles
{ 
    public class Day6
    {
        private long[] TimerCount = new long[9];

        public Day6()
        {
            File.ReadLines("Puzzles/Day6/input.txt")
                .First()
                .Split(',')
                .ToList()
                .ForEach(t => {
                    TimerCount[Int32.Parse(t)]++;
                });
        }

        public void Part1And2(int days)
        {
            for(int d = 0; d < days; d++)
            {
                long newCount = TimerCount[0];

                for(int t = 1; t < TimerCount.Length; t++)
                    TimerCount[t-1] = TimerCount[t];

                TimerCount[6] += TimerCount[8] = newCount;
            }

            Console.WriteLine($"How many lanternfish would there be after {days} days? - " + TimerCount.Sum());
        }


    }
}
