using System;
using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace AdventOfCode
{
    class day2 : dayBase
    {
        private int[,] _pos = new int[1, 1] {0, 0};

        [Benchmark]
        public override long partOne()
        {
            var result = 0;
            foreach (string item in this.input)
            {
                string direction = item.Split(" ")[0].ToLower();
                int amount = Int32.Parse(item.Split(" ")[1]);
                switch (direction)
                {
                    case "forward":
                        this._pos[0] += amount;
                        break;
                    case "up":
                        this._pos[1] -= amount;
                        break;
                    case "down":
                        this._pos[1] += amount;
                        break;
                    default:
                        break;
                }
            }
            result = this._pos[0] * this._pos[1];
            return result;
        }
        [Benchmark]
        public override long partTwo()
        {
            throw new NotImplementedException();
        }
    }
}