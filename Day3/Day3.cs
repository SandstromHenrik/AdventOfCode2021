using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2021.Puzzles
{
    public class Day3
    {
        private readonly bool[][] Diagnostics;
        private readonly int BinarySize;

        public Day3()
        {
            Diagnostics = System.IO.File.ReadAllLines("Puzzles/Day3/input.txt")
                                        .Select(
                                           d => d.Select(b => b == '1').ToArray()
                                        ).ToArray();

            BinarySize = Diagnostics[0].Length;
        }

        public void Part1()
        {
            // Loop through positions in each binary
            // If 1s (true) in the current position constitues more than half of the array then 1 else 0 (true - false)
            var gamma = Enumerable.Range(0, BinarySize)
                                  .Select(b => Diagnostics.Where(d => d[b]).Count() > (Diagnostics.Length / 2)) 
                                  .ToArray();

            // Reverse values of bits in array
            var epsilon = gamma.Select(g => !g).ToArray(); 

            Console.WriteLine("Power consumption: " + (BinaryToDecimal(gamma) * BinaryToDecimal(epsilon))); // 3901196
        }

        public void Part2()
        {
            var O2 = GetDiagnostics(Diagnostics, true)[0];
            var CO2 = GetDiagnostics(Diagnostics, false)[0];

            Console.WriteLine("Life support rating: " + (BinaryToDecimal(O2) * BinaryToDecimal(CO2))); // 4412188
        }

        public bool[][] GetDiagnostics(bool[][] diagnostics, bool mostCommon, int i = 0)
        {
            // Get most common bit / bool for the current binary position
            var bit = diagnostics.Where(d => d[i]).Count() >= (diagnostics.Length / 2);

            // Get binaries that match the current criteria (most or least common bit of pos i)
            diagnostics = diagnostics.Where(d => d[i] == (mostCommon ? bit : !bit)).ToArray();

            return diagnostics.Length > 1 ? GetDiagnostics(diagnostics, mostCommon, i + 1) : diagnostics;
        }

        public double BinaryToDecimal(bool[] binary)
        {
            return binary.Reverse().Select((b, i) => b ? (Math.Pow(2, i + 1) / 2) : 0).Sum();
        }

    }
}
