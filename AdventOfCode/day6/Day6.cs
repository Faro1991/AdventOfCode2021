using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode
{
    public class Day6 : DayBase
    {

        private Dictionary<long, long> _numberOfFish = new Dictionary<long, long>();
        private List<long> _initialValues = new List<long>();

        private void Initialize()
        {
            this._initialValues = this.Items.SelectMany(x => x.Split(",")).ToList().ConvertAll<long>(x => !string.IsNullOrEmpty(x) ? Convert.ToInt64(x) : 0);

            this._numberOfFish.Add(0, 0);
            this._numberOfFish.Add(1, 0);
            this._numberOfFish.Add(2, 0);
            this._numberOfFish.Add(3, 0);
            this._numberOfFish.Add(4, 0);
            this._numberOfFish.Add(5, 0);
            this._numberOfFish.Add(6, 0);
            this._numberOfFish.Add(7, 0);
            this._numberOfFish.Add(8, 0);
            this._numberOfFish.Add(9, 0);

            foreach (long item in _initialValues)
            {
                ++this._numberOfFish[item];
            }
        }

        private long CalcBirths(int start, int end)
        {
            long result = 0;
            long births = 0;

            for (int i = start; i < end; ++i)
            {
                births = this._numberOfFish[0];
                this._numberOfFish[7] += births;
                for (int newFish = 0; newFish < births; ++newFish)
                {
                    ++this._numberOfFish[9];
                }
                for (int key = 1; key <= 9; ++key)
                {
                    this._numberOfFish[key - 1] = this._numberOfFish[key];
                    this._numberOfFish[key] = 0;
                }               
            }
            result = this._numberOfFish.Values.Select(x => x).Sum();
            return result;
        }
        public override long PartOne()
        {
            long result = 0;

            Initialize();
            
            result = this.CalcBirths(0, 80);

            return result;
        }
        public override long PartTwo()
        {
            long result = 0;

            result = this.CalcBirths(81, 256);
        
            return result;
        }
    }
}