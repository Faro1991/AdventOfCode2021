using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode
{
    public class Day6 : DayBase
    {
        private List<_fish> _fishes = new List<_fish>();
        private List<long> _initialValues = new List<long>();
        private struct _fish
        {
            long internalTimer;
            public _fish(bool newborn = true) {internalTimer = 8;}
            public _fish(long initialDays) {internalTimer = initialDays;}
            public void DayTick()
            {
                --this.internalTimer;
            }
            public bool SpawnNewFish()
            {
                if (this.internalTimer == 0)
                {
                    this.internalTimer = 6;
                    return true;
                }
                return false;
            }
        }
        public override long PartOne()
        {
            long result = 0;
            Console.WriteLine(this.Items.ToString());
            this._initialValues = this.Items.ToString().Split(",").Select(x => Convert.ToInt64(x)).ToList();
            foreach (long days in this._initialValues)
            {
                _fishes.Add(new _fish(days));
            }

            for (int i = 0; i < 80; ++i)
            {
                List<_fish> tmpList = _fishes;
                foreach (_fish fish in tmpList)
                {
                    fish.DayTick();
                    if (fish.SpawnNewFish())
                    {
                        this._fishes.Add(new _fish(true));
                    }
                }
            }

            result = _fishes.Count;

            return result;
        }
        public override long PartTwo()
        {
            long result = 0;

            return result;
        }
    }
}