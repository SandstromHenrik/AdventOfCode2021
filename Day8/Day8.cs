using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Puzzles
{
    public class Day8
    {
        private string[][] Segments;
        private string[][] Outputs;

        private readonly int[] UniqueLengths = new int[] { 2, 3, 4, 7 };
        private readonly Dictionary<string, int> DigitMap = new Dictionary<string, int>()
        {
            { "012345", 0 },
            { "12", 1 },
            { "01346", 2 },
            { "01236", 3 },
            { "1256", 4 },
            { "02356", 5 },
            { "023456", 6 },
            { "012", 7 },
            { "0123456", 8 },
            { "012356", 9 }
        };


        public Day8()
        {
            var input = File.ReadLines("Puzzles/Day8/input.txt")
                            .Select(l => l.Split(new string[] { " | " }, StringSplitOptions.None));

            Segments = input.Select(l =>
                l.First()
                 .Split(' ')
                 .ToArray()
            ).ToArray();

            Outputs = input.Select(l => 
                l.Last()
                 .Split(' ')
                 .ToArray()
            ).ToArray();
        }

        public int Part1() => Outputs.Sum(o => o.Count(s => UniqueLengths.Contains(s.Length)));

        public int Part2() {
            return Enumerable.Range(0, Segments.Length).Sum(i => {
                var cm = GetCharMap(Segments[i]);
                return Int32.Parse(String.Join("", Outputs[i].Select(s => DigitMap[String.Join("", s.Select(c => cm[c]).OrderBy(c => c))])));
            });
        }        

        public Dictionary<char, int> GetCharMap(string[] segment)
        {
            var (arr, count) = (new char[7], 0);

            return UniqueLengths.Select(l => segment.Single(s => s.Length == l)).SelectMany(p =>
                p.Length switch
                {
                    2 => p.Select(c => new { key = arr[count++] = c, value = segment.Count(s => s.Contains(c)) == 8 ? 1 : 2 }),
                    3 => p.Where(c => !arr.Contains(c)).Select(c => new { key = arr[count++] = c, value = 0 }),
                    4 => p.Where(c => !arr.Contains(c)).Select(c => new { key = arr[count++] = c, value = segment.Count(s => s.Contains(c)) == 6 ? 5 : 6 }),
                    7 => p.Where(c => !arr.Contains(c)).Select(c => new { key = arr[count++] = c, value = segment.Count(s => s.Contains(c)) == 4 ? 4 : 3 }),
                    _ => null
                }
            ).ToDictionary(v => v.key, v => v.value);
        }

    }
}
