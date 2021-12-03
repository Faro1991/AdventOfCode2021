using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode
{
    public class Day3 : DayBase
    {
        private long FindMostCommonBit(List<string> input, int pos)
        {
            long ones = input.Count(x => x[pos] == '1');
            long zeros = input.Count(x => x[pos] == '0');
            long result = Convert.ToInt64(ones >= zeros);

            return result;
        }

        private long FindLeastCommonBit(List<string> input, int pos)
        {
            long ones = input.Count(x => x[pos] == '1');
            long zeros = input.Count(x => x[pos] == '0');
            long result = Convert.ToInt64(ones < zeros);

            return result;
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
                gammaRateString += FindMostCommonBit(this.Items, i).ToString();
                epsilonRateString += FindLeastCommonBit(this.Items, i).ToString();
            }
            gammaRate = Convert.ToInt64(gammaRateString, 2);
            epsilonRate = Convert.ToInt64(epsilonRateString, 2);

            result = gammaRate * epsilonRate;

            return result;
        }
        [Benchmark]
        public override long PartTwo()
        {
            long result = 0;
            
            List<string> tempListOxygen = this.Items;
            List<string> tempListCo2 = this.Items;
            long oxygenRating = 0;
            long co2Rating = 0;
            long mostCommonbit = 0;
            long leastCommonBit = 0;

            string longest = this.Items.Aggregate((longest, next) => next.Length > longest.Length ? next : longest);
            int end = longest.Length;
            int iter = 0;
            do
            {
                mostCommonbit = FindMostCommonBit(tempListOxygen, iter);
                leastCommonBit = FindLeastCommonBit(tempListCo2, iter);
                if (tempListOxygen.Count != 1)
                {
                    tempListOxygen = tempListOxygen.Where(x => x[iter].ToString() == mostCommonbit.ToString()).ToList();
                }
                if (tempListCo2.Count != 1)
                {
                    tempListCo2 = tempListCo2.Where(x => x[iter].ToString() == leastCommonBit.ToString()).ToList();
                }
                ++iter;
            }while (iter < end || (tempListOxygen.Count != 1 && tempListCo2.Count != 1));

            oxygenRating = Convert.ToInt64(tempListOxygen.First(), 2);
            co2Rating = Convert.ToInt64(tempListCo2.First(), 2);
            result = oxygenRating * co2Rating;

            return result;
        }
    }
}