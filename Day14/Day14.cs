using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Puzzles
{
    public class Day14
    {
        private Dictionary<string, char> Rules;
        private Dictionary<string, long> PairCount;

        public Day14()
        {
            var input = File.ReadLines("Puzzles/Day14/input.txt");
            var template = input.First();

            Rules = input.Skip(2)
                         .Select(l => l.Split(new string[] { " -> " }, StringSplitOptions.None).ToArray())
                         .ToDictionary(v => v[0], v => v[1].First());

            PairCount = template.Skip(1).Select((c, i) => "" + template[i] + c)
                                .GroupBy(p => p)
                                .ToDictionary(v => v.Key, v => long.Parse(v.Count()+""));
        }

        public void Part1And2(int steps = 10)
        {
            for (int i = 0; i < steps; i++) PairCount = Step();
            Console.WriteLine($"Most common element substracted by least common element at {steps} steps: " + (Result().Max() - Result().Min()));
        }

        private Dictionary<string, long> Step() {
            return PairCount.SelectMany(p => new Dictionary<string, long>()
            {
                { "" + p.Key[0] + Rules[p.Key], p.Value }, // new pair (1)
                { "" + Rules[p.Key] + p.Key[1], p.Value }  // new pair (2)
            })
            .GroupBy(p => p.Key)
            .ToDictionary(v => v.Key, v => v.Sum(g => g.Value));
        }

        private IEnumerable<long> Result() {
            return Rules.SelectMany(r => r.Key.ToArray())
                        .Distinct()
                        .Select(c => 
                            PairCount.Sum(p => p.Key[1] == c ? p.Value : 0) +             // Sum second element in each pair
                            (c == PairCount.First().Key[0] ? PairCount.First().Value : 0) // First element in first pair only exists in first pair
                        );
        } 
    }
}