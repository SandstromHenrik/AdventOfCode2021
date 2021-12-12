using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Puzzles
{
    public class Day11
    {
        private int[][] Energies;
        private int Flashes = 0, Steps = 0;

        public Day11() => Energies = File.ReadLines("Puzzles/Day11/input.txt")
                                         .Select(l => l.Select(e => Int32.Parse(e + "")).ToArray())
                                         .ToArray();

        public int Part1()
        {
            Range(0, 100).ForEach(i => Step());

            return Flashes;
        }

        public int Part2()
        {
            while (!Energies.All(l => l.All(e => e == 0))) Step();

            return Steps;
        }

        private void Step()
        {
            // Increase all
            Range(0, Energies.Length).ForEach(y => Range(0, Energies[y].Length).ForEach(x => {
                if (Energies[y][x]++ == 9)
                    Flash(x, y);
            }));

            // Reset flashed
            Range(0, Energies.Length).ForEach(y => Range(0, Energies[y].Length).ForEach(x => Energies[y][x] = Energies[y][x] >= 10 ? 0 : Energies[y][x]));

            Steps++;
        }


        public void Flash(int x, int y)
        {
            Range(0, 8).ForEach(i =>
            {
                var coords = i switch
                {
                    0 => y > 0 ? new { x, y = y - 1 } : null,                                                           // x, y - 1
                    1 => y > 0 && x < Energies[y].Length - 1 ? new { x = x + 1, y = y - 1 } : null,                     // x + 1, y - 1
                    2 => x < Energies[y].Length - 1 ? new { x = x + 1, y } : null,                                      // x + 1, y
                    3 => y < Energies.Length - 1 && x < Energies[y].Length - 1 ? new { x = x + 1, y = y + 1 } : null,   // x + 1, y + 1
                    4 => y < Energies.Length - 1 ? new { x, y = y + 1 } : null,                                         // x - 1, y + 1
                    5 => y < Energies.Length - 1 && x > 0 ? new { x = x - 1, y = y + 1 } : null,                        // x - 1, y + 1
                    6 => x > 0 ? new { x = x - 1, y } : null,                                                           // x - 1, y
                    7 => y > 0 && x > 0 ? new { x = x - 1, y = y - 1 } : null,                                          // x - 1, y - 1
                    _ => null
                };

                if (coords != null && Energies[coords.y][coords.x]++ == 9)
                    Flash(coords.x, coords.y);
            });

            Flashes++;
        }

        private List<int> Range(int s, int l) => Enumerable.Range(s, l).ToList();

    }
}
