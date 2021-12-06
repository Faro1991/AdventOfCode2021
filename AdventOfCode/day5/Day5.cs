using System;
using System.Linq;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using AocHelpers;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day5 : DayBase
    {
        private Dictionary<string, long> _positions = new Dictionary<string, long>();
        private List<Point> _points = new List<Point>();
        private List<long[]> _cleanInput = new List<long[]>();

        private void AddPositions(long steps, long[] startingPoint, bool vertical = false)
        {
            long valToIncrease = Convert.ToInt64(vertical);
            if (steps < 0)
            {
                startingPoint[valToIncrease] = Math.Abs(startingPoint[valToIncrease] + steps);
                steps = Math.Abs(steps);
            }
            for (int i = 0; i <= steps; ++i)
            {
                string posString = $"{startingPoint[0]}/{startingPoint[1]}";
                if (!this._positions.ContainsKey(posString))
                {
                    this._positions.Add(posString, 1);
                }
                else
                {
                    ++this._positions[posString];
                }
                ++startingPoint[valToIncrease];
            }
        }
        private void AddDiagonalLines(long[] steps, long[] startingPoint)
        {
            long stepValX = 1;
            long stepValY = 1;
            if (steps[0] < 0)
            {
                stepValX = -1;
            }
            if (steps[1] < 0)
            {
                stepValY = -1;
            }
            for (int i = 0; i <= Math.Abs(steps[0]); ++i)
            {
                string posString = $"{startingPoint[0]}/{startingPoint[1]}";
                if (!this._positions.ContainsKey(posString))
                {
                    this._positions.Add(posString, 1);
                }
                else
                {
                    ++this._positions[posString];
                }
                startingPoint[0] += stepValX;
                startingPoint[1] += stepValY;
            }
        }

        public override long PartOne()
        {
            long result = 0;

            this._cleanInput = this.Items.Select(x => Regex.Replace(x, @"\s+->\s+", ",").Split(",").Select(x => Convert.ToInt64(x)).ToArray()).ToList();

            foreach (long[] coords in this._cleanInput)
            {
                Point newPoint = new Point(coords[0..2]);
                newPoint.GenerateVector(new long[] {coords[2] - coords[0], coords[3] - coords[1]});
                this._points.Add(newPoint);
            }
            foreach (Point point in this._points)
            {
                long[] targetPositions = point.GetTargetPositions();
                if (point.positionY == targetPositions[1] || point.positionX == targetPositions[0])
                {
                    long horizontalSteps = point.GetVectorLength(true);
                    long verticalSteps = point.GetVectorLength();
                    if (point.positionY == targetPositions[1])
                    {
                        this.AddPositions(horizontalSteps, new long[] {point.positionX, point.positionY});
                    }
                    else
                    {
                        this.AddPositions(verticalSteps, new long[] {point.positionX, point.positionY}, true);
                    }
                }
                
            }

            result = this._positions.Count(x => x.Value > 1);

            return result;
        }

        public override long PartTwo()
        {
            long result = 0;
            foreach (Point point in this._points)
            {
                long[] targetPositions = point.GetTargetPositions();
                long horizontalSteps = point.GetVectorLength(true);
                long verticalSteps = point.GetVectorLength();
                if (point.positionY != targetPositions[1] && point.positionX != targetPositions[0])
                {
                    this.AddDiagonalLines(new long[] {horizontalSteps, verticalSteps}, new long[] {point.positionX, point.positionY});
                }
            }

            List<string> test = this._positions.Where(x => x.Value > 1).Select(x => x.Key).ToList();
            result = this._positions.Count(x => x.Value > 1);
            return result;
        }
    }
}