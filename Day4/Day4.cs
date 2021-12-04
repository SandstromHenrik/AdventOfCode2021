using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2021.Puzzles
{
    public class Day4
    {
        private int[][][] Boards;
        private int[] Sequence;

        public Day4()
        {
            var lines = File.ReadAllLines("Puzzles/Day4/input.txt");

            Sequence = lines.First().Split(',').Select(n => Int32.Parse(n)).ToArray();

            Boards = Enumerable.Range(0, (lines.Length - 1) / 6).Select((b, i) =>
                lines.Skip(i * 6 + 2)
                     .Take(5)
                     .Select(s =>
                        s.Split(' ')
                         .Where(l => !String.IsNullOrWhiteSpace(l))
                         .Select(n => Int32.Parse(n))
                         .ToArray()
                     ).ToArray()
            ).ToArray();
        }

        public void Part1And2()
        {
            var winningIndexes = new List<int>();
            int[] firstSequence, lastSequence = firstSequence = null;

            for (int i = 1; i <= Sequence.Length; i++)
            {
                var sequence = Sequence.Take(i).ToArray();

                var indexes = Boards.Where((b, bi) => !winningIndexes.Contains(bi) && (IsWinner(b, sequence) || IsWinner(VerticalBoard(b), sequence)))
                                          .Select(b => Array.IndexOf(Boards, b));

                if (indexes.Count() > 0)
                {
                    firstSequence = firstSequence ?? sequence;
                    lastSequence = sequence;
                    winningIndexes.AddRange(indexes);                    
                }                    
            }

            Console.WriteLine("Final score of first winning board: " + FinalScore(winningIndexes.First(), firstSequence));
            Console.WriteLine("Final score of last winning board: " + FinalScore(winningIndexes.Last(), lastSequence));
        }

        public int[][] VerticalBoard(int[][] board)
        {
            return Enumerable.Range(0, 5).Select(r => Enumerable.Range(0, 5).Select(l => board[l][r]).ToArray()).ToArray();
        }        

        public bool IsWinner(int[][] board, int[] sequence)
        {
            return board.Any(l => l.All(n => sequence.Contains(n)));
        }
        public int SumUnmarked(int[][] board, int[] sequence)
        {
            return board.Select(l => l.Where(n => !sequence.Contains(n)).Sum()).Sum();
        }

        public int FinalScore(int index, int[] sequence)
        {
            return SumUnmarked(Boards[index], sequence) * sequence.Last();
        }

    }
}
