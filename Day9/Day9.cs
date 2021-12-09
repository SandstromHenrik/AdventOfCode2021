using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Puzzles
{
    public class Day9
    {
        private int[][] HeightMap;

        private class Coord
        {
            public Coord(int x, int y, int[][] hm)
            {
                X = x;
                Y = y;
                H = hm[y][x];
            }

            public int X, Y, H;

            public override bool Equals(object obj) => ((Coord) obj).X == X && ((Coord)obj).Y == Y;
        }

        public Day9()
        {
            HeightMap = File.ReadLines("Puzzles/Day9/input.txt")
                            .Select(l => l.Select(c => Int32.Parse(c.ToString())).ToArray())
                            .ToArray();
        }

        public int Part1() => HeightMap.SelectMany((l, y) => l.Where((h, x) => GetAdjacent(new Coord(x, y, HeightMap), new List<Coord>(), true).All(c => c.H > h))).Sum(h => h+1);

        public int Part2()
        {
            var basins = new List<List<Coord>>();

            Range(0, HeightMap.Length).ForEach(y => Range(0, HeightMap[y].Length).ForEach(x =>
            {
                var c = new Coord(x, y, HeightMap);

                if (c.H < 9 && !basins.Any(l => l.Contains(c)))
                    basins.Add(GetAdjacent(c, new List<Coord>() { c }, false));
            }));

            return basins.OrderByDescending(b => b.Count).Take(3).ToList().Aggregate(1, (v, b) => v * b.Count);
        }

        private List<Coord> GetAdjacent(Coord c1, List<Coord> basin, bool part1)
        {
            Range(0, 4).Select(n => n switch
                {
                    0 => c1.X > 0 ? new Coord (c1.X - 1, c1.Y, HeightMap) : null,                           // (X - 1), Y
                    1 => c1.X < HeightMap[c1.Y].Length-1 ? new Coord(c1.X + 1, c1.Y, HeightMap) : null,     // (X + 1), Y
                    2 => c1.Y > 0 ? new Coord(c1.X, c1.Y - 1, HeightMap) : null,                            // X, (Y - 1)
                    3 => c1.Y < HeightMap.Length-1 ? new Coord(c1.X, c1.Y + 1, HeightMap) : null,           // X, (Y + 1)
                    _ => null
                }
            ).Where(c2 => c2 != null).ToList().ForEach(c2 => {
                if(part1)
                {
                    basin.Add(c2);
                }
                else if(!part1 && !basin.Contains(c2) && c2.H < 9 &&  (c2.H > c1.H || c2.H < c1.H))
                {
                    basin.Add(c2);
                    GetAdjacent(c2, basin, false);
                }             
            });

            return basin;
        }

        public List<int> Range(int s, int l) => Enumerable.Range(s, l).ToList();
    }
}
