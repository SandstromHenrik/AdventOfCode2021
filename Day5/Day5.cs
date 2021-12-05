using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent_of_Code_2021.Puzzles
{
    public class Lines
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
    }

    public class Day5
    {
        private Lines[] Lines;

        public Day5()
        {
            Lines = File.ReadAllLines("Puzzles/Day5/input.txt")
                        .Select(s => Regex.Split(s, @"\D+").Select(c => Int32.Parse(c)).ToArray())
                        .ToArray()
                        .Select(l => new Lines() { X1 = l[0], Y1 = l[1], X2 = l[2], Y2 = l[3] })
                        .ToArray();
        }

        public void Part1And2()
        {
            var countNonDiagonal = CoordsWithMultiOL(GetAllCoords(false));
            var countWithDiagonal = CoordsWithMultiOL(GetAllCoords(true));

            Console.WriteLine("At how many points do at least two lines overlap (non-diagonal)? - " + countNonDiagonal);
            Console.WriteLine("At how many points do at least two lines overlap (with diagonal)? - " + countWithDiagonal);
        }


        public IEnumerable<string> GetAllCoords(bool withDiagonal)
        {
            var (verticalM, horizontalM) = (1, 1); // Modifiers

            return Lines.SelectMany(cs =>
            {
                if (cs.X1 == cs.X2) // Vertical
                {
                    return GetRange(cs.Y1, cs.Y2).Select(y => cs.X1 + "," + y);
                }
                else if (cs.Y1 == cs.Y2) // Horizontal
                {
                    return GetRange(cs.X1, cs.X2).Select(x => x + "," + cs.Y1);
                }
                else if(withDiagonal) // Diagonal
                {
                    (verticalM, horizontalM) = (cs.X1 < cs.X2 ? 1 : -1, cs.Y1 < cs.Y2 ? 1 : -1);

                    return GetRange(cs.Y1, cs.Y2).Select((c, i) => (cs.X1 + (i * 1 * verticalM)) + "," + (cs.Y1 + (i * 1 * horizontalM)));
                }
                return new string[] { };
            });
        }

        public int CoordsWithMultiOL(IEnumerable<string> coordinates)
        {
            return coordinates.GroupBy(c => c).Where(c => c.Count() >= 2).Count();
        }

        public IEnumerable<int> GetRange(int c1, int c2)
        {
            return Enumerable.Range(new int[] { c1, c2 }.Min(), Math.Abs(c1 - c2) + 1);
        }
    }
}
