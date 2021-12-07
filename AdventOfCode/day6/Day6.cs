using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode
{
    public class Day6 : DayBase
    {
        private List<long> _fishNum = new List<long>();
        private List<long> _initialValues = new List<long>();
        public override long PartOne()
        {
            long result = 0;
            this._initialValues = this.Items.SelectMany(x => x.Split(",")).ToList().ConvertAll<long>(x => !string.IsNullOrEmpty(x) ? Convert.ToInt64(x) : 0);
            this._fishNum = this._initialValues.Select(x => x).ToList();
            for (int i = 0; i < 80; ++i)
            {
                long add = this._fishNum.Where(x => x == 0).Count();
                this._fishNum = this._fishNum.Select(x => --x).ToList();
                for (int j = 0; j < add; ++j)
                {
                    this._fishNum.Add(8);
                }
                this._fishNum = this._fishNum.Select(x =>
                {
                    if (x < 0)
                    {
                        return x + 7;
                    }
                    return x;
                }).ToList();
            }

            result = this._fishNum.Count();

            return result;
        }
        public override long PartTwo()
        {
            long result = 0;

            return result;
        }
    }
}