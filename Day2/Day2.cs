using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2021.Puzzles
{
    public class Day2
    {
        private string[][] Commands;

        public Day2()
        {
            var input = @"C:\Users\Henri\source\repos\Advent of Code 2021\Advent of Code 2021\Puzzles\Day2\input.txt";

            Commands = System.IO.File.ReadAllLines(input)
                                     .Select(d => d.Split(' '))
                                     .ToArray();
        }

        public void Part1()
        {
            var (horizontal, depth) = (0, 0);

            foreach(var command in Commands)
            {
                var value = Int32.Parse(command[1]);

                switch (command[0])
                {
                    case "forward":
                        horizontal += value;
                        break;
                    case "up":
                        depth -= value;
                        break;
                    case "down":
                        depth += value;
                        break;                     
                }
            }

            Console.WriteLine("Horizontal * depth = " + horizontal * depth);
        }

        public void Part2()
        {
            var (horizontal, depth, aim) = (0, 0, 0);

            foreach (var command in Commands)
            {
                var value = Int32.Parse(command[1]);

                switch (command[0])
                {
                    case "forward":
                        horizontal += value;
                        depth += aim * value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                    case "down":
                        aim += value;
                        break;
                }
            }

            Console.WriteLine("Horizontal * depth = " + horizontal * depth);
        }

    }
}
