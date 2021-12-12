using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode
{
    public class Day11 : DayBase
    {
        private long _countSteps = 0;
        private List<List<Octopus>> _octopuses = new List<List<Octopus>>();
        Dictionary<long, string> _directions = new Dictionary<long, string>();
        List<Octopus> _alreadyFlashed = new List<Octopus>();
        private long _totalFlashes = 0;
        private struct Octopus
        {
            public int[] coords;
            public long val;
            public long[] neighbours;
            public Octopus(long[] values) {this.neighbours = values[1..]; this.val = values[0]; coords = new int[] {};}
            public Octopus(long[] values, int[] coordsIn) {this.neighbours = values[1..]; this.val = values[0]; coords = coordsIn;}
            public Octopus Inc()
            {
                ++val;
                return this;
            }
        }
        private long[] GetNeighbours(int i, int j, List<string> input)
        {
            long[] values = {-99, -99, -99, -99, -99, -99, -99, -99, -99};
            values[0] = Convert.ToInt64(input[i][j].ToString());
            if (i > 0)
            {
                if (j > 0)
                {
                    values[1] = Convert.ToInt64(input[i - 1][j - 1].ToString());
                }
                values[2] = Convert.ToInt64(input[i - 1][j].ToString());
                if (j < input[i].Length - 1)
                {
                    values[3] = Convert.ToInt64(input[i - 1][j + 1].ToString());
                }
            }
            if (j > 0)
            {
                values[4] = Convert.ToInt64(input[i][j - 1].ToString());
            }
            if (j < input[i].Length - 1)
            {
                values[5] = Convert.ToInt64(input[i][j + 1].ToString());
            }
            if (i < input.Count - 1)
            {
                if (j > 0)
                {
                    values[6] = Convert.ToInt64(input[i + 1][j - 1].ToString());
                }
                values[7] = Convert.ToInt64(input[i + 1][j].ToString());
                if (j < input[i].Length - 1)
                {
                    values[8] = Convert.ToInt64(input[i + 1][j + 1].ToString());
                }
            }
            return values;
        }
        private void ScanForOctopuses(List<string> input)
        {
            
            for (int i = 0; i < input.Count; ++i)
            {
                this._octopuses.Add(new List<Octopus>());
                for (int j = 0; j < input[i].Length; ++j)
                {
                    long[] values = this.GetNeighbours(i, j, input);
                    this._octopuses[i].Add(new Octopus(values, new int[] {i, j}));
                }
            }
        }
        private long CountAllFlashers()
        {
            long oldCount = this._alreadyFlashed.Count;
            List<Octopus> findFlashed = this._octopuses.SelectMany(x => x).Select(x =>
            {
                if (this._alreadyFlashed.Where(y => y.coords == x.coords).ToList().Count == 0 && x.val > 9)
                {
                    this._alreadyFlashed.Add(x);
                    ++this._totalFlashes;
                    this.FlashAllNeighbours(x.coords);
                }
                return x;
            }).ToList();
            long newCount = this._alreadyFlashed.Count;
            if (oldCount != newCount)
            {
                CountAllFlashers();
            }
            return this._alreadyFlashed.Count;
        }
        private void FlashAllNeighbours(int[] input)
        {
            List<Octopus> increaseNeighbours = this._octopuses.SelectMany(x => x).Where(x => x.coords == input).Select(x =>
            {
                for (long i = 0; i < x.neighbours.Length; ++i)
                {
                    if (x.neighbours[i] > 0)
                    {
                        string directionString = this._directions[i];
                        int xPos = x.coords[0] + Convert.ToInt32(directionString.Split("/")[0].ToString());
                        int yPos = x.coords[1] + Convert.ToInt32(directionString.Split("/")[1].ToString());
                        this._octopuses = this._octopuses.Select(x => x.Select(x =>
                        {
                            if (x.coords[0] == xPos && x.coords[1] == yPos)
                            {
                                return x.Inc();
                            }
                            return x;
                        }).ToList()).ToList();
                    }
                }
                return x;
            }).ToList();
        }
        private void Initialize()
        {
            this._directions.Add(0, "-1/-1");
            this._directions.Add(1, "-1/0");
            this._directions.Add(2, "-1/1");
            this._directions.Add(3, "0/-1");
            this._directions.Add(4, "0/1");
            this._directions.Add(5, "1/-1");
            this._directions.Add(6, "1/0");
            this._directions.Add(7, "1/1");

        }
        [Benchmark]
        public override long PartOne()
        {
            if (this._directions.Count == 0)
            {
                this.Initialize();
            }
            if (this._octopuses.Count == 0)
            {
                this.ScanForOctopuses(this.Items);
            }
            long result = 0;

            for (int i = 0; i < 100; ++i)
            {
                ++this._countSteps;
                this._octopuses = this._octopuses.Select(x => x.Select(x => x.Inc()).ToList()).ToList();
                this.CountAllFlashers();
                this._octopuses = this._octopuses.Select(x => x.Select(x => {
                    if (x.val > 9)
                    {
                        x.val = 0;
                    }
                    return x;
                }).ToList()).ToList();
                this._alreadyFlashed = new List<Octopus>();
            }

            result = this._totalFlashes;

            return result;
        }
        [Benchmark]
        public override long PartTwo()
        {
            long result = 0;
            long listLength = this._octopuses.SelectMany(x => x).Select(x => x).ToList().Count;

            while (true)
            {
                ++this._countSteps;
                this._octopuses = this._octopuses.Select(x => x.Select(x => x.Inc()).ToList()).ToList();
                this.CountAllFlashers();
                this._octopuses = this._octopuses.Select(x => x.Select(x =>
                {
                    if (x.val > 9)
                    {
                        x.val = 0;
                    }
                    return x;
                }).ToList()).ToList();
                this._alreadyFlashed = new List<Octopus>();

                long zeroes = this._octopuses.SelectMany(x => x).Where(x => x.val == 0).ToList().Count;

                List<long> test = this._octopuses.SelectMany(x => x).Select(x => x.val).ToList();

                if (zeroes == listLength)
                {
                    break;
                }
            }

            result = this._countSteps;

            return result;
        }
    }
}