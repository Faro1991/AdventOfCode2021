using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode
{
    public class Day7 : DayBase
    {
        private List<long> _horizontalCrabPositions = new List<long>();
        private void Initialize()
        {
            this._horizontalCrabPositions = this.Items.SelectMany(x => x.Split(",")).Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToInt64(x)).ToList();
            this._horizontalCrabPositions.Sort();
        }
        private long CalculateLowestFuelUsage(List<long> positions)
        {
            long median = positions[positions.Count / 2];
            long result = positions.Select(x => Math.Abs(median - x)).Sum();
            return result;
        }
        private long ImprovedFuelUsageCalculation(List<long> positions)
        {
            long mean = Convert.ToInt64(Math.Round(positions.Average()));
            List<long> candidates = new List<long>();
            for (long range = mean - 10; range < mean + 10; ++range)
            {
            candidates.Add(positions.Select(x => Math.Abs(range - x)).SelectMany(x =>
                {
                    List<long> tmpList = new List<long>();
                    for (int i = 0; i <= x; ++i)
                    {
                        tmpList.Add(i);
                    }
                    return tmpList;
                }
            ).Sum());
            }
            long result = candidates.Min();
            return result;
        }
        [Benchmark]
        public override long PartOne()
        {
            this.Initialize();
            long result = 0;
            result = this.CalculateLowestFuelUsage(this._horizontalCrabPositions);

            return result;
        }
        [Benchmark]
        public override long PartTwo()
        {
            long result = 0;
            result = this.ImprovedFuelUsageCalculation(this._horizontalCrabPositions);

            return result;
        }
    }
}