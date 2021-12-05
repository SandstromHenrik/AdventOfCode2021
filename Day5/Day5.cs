using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
                        .Select(s => 
                            s.Split(new string[] { " -> " }, StringSplitOptions.None)
                             .Select(l => l.Split(',').Select(c => Int32.Parse(c)).ToArray())
                             .ToArray()
                        )
                        .ToArray()
                        .Select(l => new Lines() { X1 = l[0][0], Y1 = l[0][1], X2 = l[1][0], Y2 = l[1][1] })
                        .ToArray();
        }

        public void Part1And2()
        {
            var countNonDiagonal = CordsWithMultiOL(GetAllCoordinates(false));
            var countWithDiagonal = CordsWithMultiOL(GetAllCoordinates(true));

            Console.WriteLine("At how many points do at least two lines overlap (non-diagonal)? - " + countNonDiagonal);
            Console.WriteLine("At how many points do at least two lines overlap (with diagonal)? - " + countWithDiagonal);
        }


        public IEnumerable<string> GetAllCoordinates(bool withDiagonal)
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

        public int CordsWithMultiOL(IEnumerable<string> coordinates)
        {
            return coordinates.GroupBy(c => c).Where(c => c.Count() >= 2).Count();
        }

        public IEnumerable<int> GetRange(int c1, int c2)
        {
            return Enumerable.Range(new int[] { c1, c2 }.Min(), Math.Abs(c1 - c2) + 1);
        }
    }
}
