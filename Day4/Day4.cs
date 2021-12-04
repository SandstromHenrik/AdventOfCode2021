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
                     .Select(l =>
                        l.Split(' ')
                         .Where(il => !String.IsNullOrWhiteSpace(il))
                         .Select(n => Int32.Parse(n))
                         .ToArray()
                     ).ToArray()
            ).ToArray();
        }

        public void Part1()
        {
            for(int i = 1; i <= Sequence.Length; i++)
            {
                var sequence = Sequence.Take(i).ToArray();

                var winningBoard = Boards.SingleOrDefault(b => IsWinner(b, sequence) || IsWinner(VerticalBoard(b), sequence));

                if(winningBoard != null) 
                {
                    Console.WriteLine("Final score of first winning board: " + SumUnmarked(winningBoard, sequence) * sequence.Last());
                    break;
                }
            }                    
        }

        public void Part2()
        {
            var (indexes, lastWinningSequence) = (new List<int>(), new int[Sequence.Length]);

            for (int i = 1; i <= Sequence.Length; i++)
            {
                var sequence = Sequence.Take(i).ToArray();

                var winningBoards = Boards.Where((b, bi) => !indexes.Contains(bi) && (IsWinner(b, sequence) || IsWinner(VerticalBoard(b), sequence)))
                                          .Select(b => Boards.ToList().IndexOf(b));

                if (winningBoards.Count() > 0)
                {
                    indexes.AddRange(winningBoards);
                    lastWinningSequence = sequence;
                }                    
            }

            Console.WriteLine("Final score of last winning board: " + SumUnmarked(Boards[indexes.Last()], lastWinningSequence) * lastWinningSequence.Last());
        }

        public int[][] VerticalBoard(int[][] board)
        {
            return Enumerable.Range(0, 5).Select(r => Enumerable.Range(0, 5).Select(l => board[l][r]).ToArray()).ToArray();
        }

        public int SumUnmarked(int[][] board, int[] sequence)
        {
            return board.Select(l => l.Where(n => !sequence.Contains(n)).Sum()).Sum();
        }

        public bool IsWinner(int[][] board, int[] sequence)
        {
            return board.Any(l => l.All(n => sequence.Contains(n)));
        }

    }
}
