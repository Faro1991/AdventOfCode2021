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
            int iter = 0;
            int end = 0;
            if (this.Items.Count > 0)
            {
                string longest = this.Items.Aggregate((longest, next) => next.Length > longest.Length ? next : longest);
                end = longest.Length;
            }

            do
            {
                gammaRateString += FindMostCommonBit(this.Items, iter).ToString();
                epsilonRateString += FindLeastCommonBit(this.Items, iter).ToString();
                ++iter;
            }while (iter < end);
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
            int iter = 0;
            int end = 0;
            if (this.Items.Count > 0)
            {
                string longest = this.Items.Aggregate((longest, next) => next.Length > longest.Length ? next : longest);
                end = longest.Length;
            }

            do
            {
                if (tempListOxygen.Count > 1)
                {
                    mostCommonbit = FindMostCommonBit(tempListOxygen, iter);
                    tempListOxygen = tempListOxygen.Where(x => x[iter].ToString() == mostCommonbit.ToString()).ToList();
                }
                if (tempListCo2.Count > 1)
                {
                    leastCommonBit = FindLeastCommonBit(tempListCo2, iter);
                    tempListCo2 = tempListCo2.Where(x => x[iter].ToString() == leastCommonBit.ToString()).ToList();
                }
                if (tempListCo2.Count == 1 && tempListOxygen.Count == 1)
                {
                    break;
                }
                ++iter;
            }while (iter < end);

            if (tempListCo2.Count == 1 && tempListOxygen.Count == 1)
            {
                oxygenRating = Convert.ToInt64(tempListOxygen.First(), 2);
                co2Rating = Convert.ToInt64(tempListCo2.First(), 2);
                result = oxygenRating * co2Rating;
            }
            return result;
        }
    }
}