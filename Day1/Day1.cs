using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2021.Puzzles
{
    public class Day1
    {
        private int[] Depths;
        private readonly int WindowSize = 3;

        public Day1 ()
        {
            var input = @"C:\Users\Henri\source\repos\Advent of Code 2021\Advent of Code 2021\Puzzles\Day1\input.txt";

            Depths = System.IO.File.ReadAllLines(input)
                                   .Select(d => Int32.Parse(d))
                                   .ToArray();
        }

        public void Part1()
        {
            var increases = Depths.Take(Depths.Count() - 1)
                                  .Where((d, i) => Depths[i+1] > d)
                                  .Count();

            Console.WriteLine("Increases: " + increases);
        }

        public void Part2()
        {
            var increases = Depths.Take(Depths.Count() - (WindowSize - 1))
                                  .Where((d, i) => SumWindow(i+1) > SumWindow(i))
                                  .Count();

            Console.WriteLine("Increases: " + increases);
        }

        private int SumWindow(int index)
        {
            return Depths.Skip(index).Take(WindowSize).Sum();
        }

    }
}
