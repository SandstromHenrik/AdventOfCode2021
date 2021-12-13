using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Puzzles
{
    public class Day13
    {
        private List<int[]> Dots;

        private List<KeyValuePair<string, int>> Folds;

        public Day13() 
        {
            Dots = File.ReadLines("Puzzles/Day13/input.txt")   
                       .Where(l => l.Contains(","))
                       .Select(l => l.Split(',').Select(int.Parse).ToArray())
                       .ToList();

            Folds = File.ReadLines("Puzzles/Day13/input.txt")
                       .Where(l => l.Contains("="))
                       .Select(l => new KeyValuePair<string, int>(l.Contains("x") ? "vertical" : "horizontal", Int32.Parse(l.Split('=').Last())))
                       .ToList();
        } 

        public int Part1() => Fold(Folds.First().Key == "vertical", Folds.First().Value);

        public void Part2()
        {
            Folds.ForEach(f => Fold(f.Key == "vertical", f.Value));

            // Print
            Range(0, Dots.Max(d => d[1]) + 1).ForEach(y => Range(0, Dots.Max(d => d[0]) + 1).ForEach(x => {
                if (x == 0) Console.WriteLine();
                Console.Write(GetChar(x, y));
            }));
        }

        private int Fold(bool vertical, int pos)
        {
            Dots.Where(d => vertical ? d[0] > pos : d[1] > pos).ToList().ForEach(d =>
            {
                Dots.Remove(d);

                var newDot = vertical ? new int[] { pos - (d[0] - pos), d[1] } 
                                      : new int[] { d[0], pos - (d[1] - pos) };

                if (!Exists(newDot))
                    Dots.Add(newDot);
            });

            return Dots.Count;
        }

        private bool Exists(int[] dot) => Dots.Any(d => d[0] == dot[0] && d[1] == dot[1]);       

        private List<int> Range(int s, int l) => Enumerable.Range(s, l).ToList();

        private char GetChar(int x, int y) => Exists(new int[] { x, y }) ? '#' : '.';

    }
}
