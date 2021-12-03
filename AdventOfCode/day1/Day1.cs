using System;
using System.Linq;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode
{
    public class Day1 : DayBase
    {

        [Benchmark]
        public override long PartOne()
        {
            var result = 0;
            List<long> vals = this.Items.ConvertAll<long>(x => x!= "" ? Int64.Parse(x) : 0);
            long currentItem = vals.Take(1).Sum();
            foreach (long item in vals)
            {
                if (currentItem < item)
                {
                    ++result;
                }
                currentItem = item;
            }   
            return result;
        }

        [Benchmark]
        public override long PartTwo()
        {
            var result = 0;
            
            List<long> vals = this.Items.ConvertAll<long>(x => x!= "" ? Int64.Parse(x) : 0);

            long currentSet = vals.Take(3).Sum();

            for (int i = 0; i <= vals.Count; ++i)
            {
                long set = vals.Skip(i).Take(3).Sum();
                if (currentSet < set)
                {
                    ++result;
                }
                currentSet = set;
            }
            
            return result;
        }
    }
}