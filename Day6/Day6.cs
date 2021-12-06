using System;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Puzzles
{ 
    public class Day6
    {
        // Fish Count by day (pos 1-9)
        // pos 0 does not represent a day and is used to keep track of newborns
        private long[] FC = new long[10]; 

        public Day6()
        {
            File.ReadLines("Puzzles/Day6/input.txt")
                .First()
                .Split(',')
                .ToList()
                .ForEach(t => {
                    FC[Int32.Parse(t)+1]++;
                });
        }

        public long FishGrowth(int days)
        {
            Enumerable.Range(0, days)
                      .ToList()
                      .ForEach(d => FC[7] = FC[8] + (FC[9] = FC.Take(9).Select((c, i) => FC[i] = FC[i+1]).ToArray()[0]));

            return FC.Skip(1).Sum();
        }
    }
}
