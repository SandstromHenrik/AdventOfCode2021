using System;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Puzzles
{ 
    public class Day6
    {
        private long[] DayCount = new long[10];

        public Day6()
        {
            File.ReadLines("Puzzles/Day6/input.txt")
                .First()
                .Split(',')
                .ToList()
                .ForEach(t => {
                    DayCount[Int32.Parse(t)+1]++;
                });
        }

        public void Part1And2(int days, long ph = 0)
        {
            RangeAction(0, days, d => { 
                RangeAction(0, DayCount.Length, t => ph = t < DayCount.Length-1 ? (DayCount[t] = DayCount[t+1]) : (DayCount[7] += DayCount[9] = DayCount[0]));
            });

            Console.WriteLine($"How many lanternfish would there be after {days} days? - " + DayCount.Skip(1).Sum());
        }

        public void RangeAction(int s, int l, Action<int> a) => Enumerable.Range(s, l).ToList().ForEach(a);
    }
}
