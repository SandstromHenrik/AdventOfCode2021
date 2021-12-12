using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Puzzles
{
    public class Day10
    {
        private string[] Lines;

        private char[] Opening = new char[] { '(', '[', '{', '<' };
        private char[] Closing = new char[] { ')', ']', '}', '>' };

        private int[] Points = new int[] { 3, 57, 1197, 25137 };

        public Day10() => Lines = File.ReadLines("Puzzles/Day10/input.txt").ToArray();

        public void Part1And2()
        {
            var points = 0;

            // Calculate syntax error score and return incomplete lines
            var incomplete = Lines.Select(l => {
                for(int i = 0; i < l.Length-1; i++)
                {
                    if(Opening.Contains(l[i]) && Closing.Contains(l[i+1]))
                    {
                        var cIndex = Array.IndexOf(Closing, l[i+1]);
                        var oIndex = Array.IndexOf(Opening, l[i]);

                        if(cIndex == oIndex)
                        {
                            l = l.Remove(i, 2);
                            i = 0;
                        }
                        else
                        {
                            points += Points[cIndex];
                            return null;
                        }
                    }
                }
                return l;
            }).Where(l => l != null).ToList();

            // Calculate score for incomplete lines
            var scores = incomplete.Select(l =>
            {
                long score = 0;

                l.Reverse().ToList().ForEach(c =>
                {
                    score *= 5;
                    score += Array.IndexOf(Opening, c) + 1;
                });

                return score;
            }).OrderBy(s => s).ToArray();

            Console.WriteLine("Syntax error score for first illegal character in each corrupted line: " + points);
            Console.WriteLine("What is the middle score for the incomplete lines?: " + scores[scores.Length / 2]);
        }

    }
}
