using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Puzzles
{
    public class Day12
    {
        private string[][] Cons;

        private HashSet<List<string>> Paths = new HashSet<List<string>>();

        public Day12() => Cons = File.ReadLines("Puzzles/Day12/input.txt")
                                      .Select(l => l.Split('-').ToArray())
                                      .ToArray();

        public int Part1()
        {
            AllCons.Where(c => c[0] == "start").ToList().ForEach(c => Step(new List<string>() { c[0], c[1] }));

            return Paths.Count();
        }

        public int Part2()
        {
            AllCons.Where(c => c[0] == "start").ToList().ForEach(c => Step(new List<string>() { c[0], c[1] }, true));

            return Paths.Count();
        }

        public void Step(List<string> paths, bool smallTwice = false, bool hasSmallTwice = false)
        {
            hasSmallTwice = hasSmallTwice ? true : paths.Where(p => !IsBig(p)).GroupBy(p => p).Any(g => g.Count() > 1);

            AllCons.Where(c => MeetsConditions(c, paths, smallTwice, hasSmallTwice)).ToList().ForEach(c =>
            {
                var newPaths = new List<string>(paths) { c[1] };

                if (c[1] == "end")
                    Paths.Add(newPaths);
                else
                    Step(newPaths, smallTwice, hasSmallTwice);
            });
        }

        private IEnumerable<string[]> AllCons => Cons.Concat(Cons.Select(c => c.Reverse().ToArray()));

        private bool IsBig(string cave) => Char.IsUpper(cave.First());
        private bool IsStEn(string cave) => cave == "start" || cave == "end";      
        private bool HasPath(List<string> paths, string cave) => Paths.Select(p => p.Take(paths.Count+1)).Contains(new List<string>(paths) { cave });

        private bool MeetsConditions(string[] caves, List<string> paths, bool smallTwice, bool hasSmallTwice)
        {
            return caves[0] == paths.Last() && (
                    IsBig(caves[1]) || !paths.Contains(caves[1]) ||
                    (smallTwice && !hasSmallTwice && !IsStEn(caves[1]) && !HasPath(paths, caves[1]))
                );
        }

    }
}
