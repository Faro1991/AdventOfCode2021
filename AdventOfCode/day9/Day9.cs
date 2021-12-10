using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode
{
    public class Day9 : DayBase
    {
        List<List<Point>> _points = new List<List<Point>>();
        List<string> _basins = new List<string>();
        Dictionary<long, string> _directions = new Dictionary<long, string>();
        private struct Point
        {
            public long val;
            public long[] neighbours;
            public bool isLowPoint;
            public bool alreadyMarked;

            public void setMarked()
            {
                this.alreadyMarked = true;
            }

            public Point(long[] values) {this.neighbours = values[1..]; this.isLowPoint = values[1..].Min() > values[0]; this.val = values[0]; this.alreadyMarked = false;}
        }
        private long[] GetNeighbours(int i, int j, List<string> input)
        {
            long[] values = {9, 9, 9, 9, 9};
            values[0] = Convert.ToInt64(input[i][j].ToString());
            if (j > 0)
            {
                values[1] = Convert.ToInt64(input[i][j - 1].ToString());
            }
            if (j < input[i].Length - 1)
            {
                values[2] = Convert.ToInt64(input[i][j + 1].ToString());
            }
            if (i > 0)
            {
                values[3] = Convert.ToInt64(input[i - 1][j].ToString());
            }
            if (i < input.Count - 1)
            {
                values[4] = Convert.ToInt64(input[i + 1][j].ToString());
            }
            return values;
        }
        private void ScanForPoints(List<string> input)
        {
            
            for (int i = 0; i < input.Count; ++i)
            {
                this._points.Add(new List<Point>());
                for (int j = 0; j < input[i].Length; ++j)
                {
                    long[] values = this.GetNeighbours(i, j, input);
                    this._points[i].Add(new Point(values));
                }
            }
        }
        private string BuildBasin(List<List<Point>> input, int[] coords)
        {
            string result = "";
            int x = coords[0];
            int y = coords[1];

            long[] neighbours = input[x][y].neighbours;
            long val = input[x][y].val;

            for (int i = 0; i < neighbours.Length; ++i)
            {
                if (neighbours[i] < 9 && neighbours[i] > val)
                {
                    string[] posString = this._directions[i].Split("/");
                    int xChange = x + Convert.ToInt32(posString[0]);
                    int yChange = y + Convert.ToInt32(posString[1]);
                    int[] nextHop = {xChange, yChange};
                    result += BuildBasin(input, nextHop);
                }
            }
            if (!input[x][y].alreadyMarked)
            {
                result += val.ToString();
                input[x][y].setMarked();
            }

            return result;
        }
        private void ScanForBasinStarts(List<List<Point>> input)
        {
            for (int i = 0; i < input.Count; ++i)
            {
                for (int j = 0; j < input[i].Count; ++j)
                {
                    if (input[i][j].isLowPoint)
                    {
                        this._basins.Add(BuildBasin(input, new int[] {i, j}));
                    }
                }
            }
        }
        private long MultiplyList(List<int> input)
        {
            long result = 1;

            foreach (int item in input)
            {
                result *= item;
            }

            return result;
        }
        private void Initialize()
        {
            this._directions.Add(0, "0/-1");
            this._directions.Add(1, "0/1");
            this._directions.Add(2, "-1/0");
            this._directions.Add(3, "1/0");
        }
        public override long PartOne()
        {
            this.Initialize();
            long result = 0;

            this.ScanForPoints(this.Items);
            result = this._points.SelectMany(x => x).Where(x => x.isLowPoint).Select(x => x.val + 1).Sum();

            return result;
        }
        public override long PartTwo()
        {
            long result = 0;
            this.ScanForBasinStarts(this._points);
            List<int> basinSizes= this._basins.Select(x => x.Length).ToList();
            basinSizes.Sort();

            result = this.MultiplyList(basinSizes.TakeLast(3).ToList());

            return result;
        }
    }
}