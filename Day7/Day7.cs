using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Puzzles
{ 
    public class Day7
    {
        private int[] HP;

        public Day7()
        {
            HP = File.ReadLines("Puzzles/Day7/input.txt")
                     .First()
                     .Split(',')
                     .Select(p => Int32.Parse(p))
                     .ToArray();
        }

        public int Part1() => Range.Select(p => HP.Select(hp => Math.Abs(hp - p)).Sum()).Min();
        public int Part2() => Range.Select(p => HP.Select(hp =>(Math.Abs(hp - p) * (Math.Abs(hp - p) + 1)) / 2).Sum()).Min();

        private IEnumerable<int> Range => Enumerable.Range(HP.Min(), HP.Max() - HP.Min());
    }
}
