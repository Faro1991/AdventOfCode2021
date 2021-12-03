using System;
using BenchmarkDotNet.Attributes;
using System.Linq;

namespace AdventOfCode
{
    public class Day3 : DayBase
    {
        private long FindMostCommonBit(int pos)
        {
            long ones = this.Items.Count(x => x[pos] == '1');
            long zeros = this.Items.Count(x => x[pos] == '0');

            return Convert.ToInt64(ones > zeros);
        }

        private long FindLeastCommonBit(int pos)
        {
            long ones = this.Items.Count(x => x[pos] == '1');
            long zeros = this.Items.Count(x => x[pos] == '0');

            return Convert.ToInt64(ones < zeros);
        }

        [Benchmark]
        public override long PartOne()
        {
            long result = 0;
            var gammaRateString = "";
            var epsilonRateString = "";
            long gammaRate = 0;
            long epsilonRate = 0;
            string longest = this.Items.Aggregate((longest, next) => next.Length > longest.Length ? next : longest);
            int end = longest.Length;
            for (int i = 0; i < end; ++i)
            {
                gammaRateString += FindMostCommonBit(i).ToString();
                epsilonRateString += FindLeastCommonBit(i).ToString();
            }
            gammaRate = Convert.ToInt64(gammaRateString, 2);
            epsilonRate = Convert.ToInt64(epsilonRateString, 2);

            result = gammaRate * epsilonRate;

            return result;
        }
        [Benchmark]
        public override long PartTwo()
        {
            var result = 0;

            return result;
        }
    }
}