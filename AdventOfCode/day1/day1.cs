using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode {
    public class day1 : dayBase {

        [Benchmark]
        public override long partOne()
        {
            var result = 0;
            string currentItem = this.input.First();
            foreach (string item in this.input)
            {
                if (Int64.TryParse(currentItem) < Int64.TryParse(item))
                {
                    ++result;
                }
                currentItem = item;
            }   
            return result;
        }

        [Benchmark]
        public override long partTwo()
        {
            throw new NotImplementedException();
        }
    }
}