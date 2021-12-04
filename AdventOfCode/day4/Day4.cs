using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using System.Linq;
using AocHelpers;

namespace AdventOfCode
{
    public class Day4 : DayBase
    {
        private List<string> _lots = new List<string>();

        private void DrawLots()
        {
            if (this.Items.Count > 0)
            {
                List<string> result = this.Items.First().Split(",").ToList();
                this.Items.Remove(this.Items.First());
                this._lots = result;
            }
        }
        [Benchmark]
        public override long PartOne()
        {
            long result = 0;
            DrawLots();

            Board board = new Board();
            board.EnterFields(this.Items);
            
            foreach (string lot in this._lots)
            {
                long lotVal = Convert.ToInt64(lot);
                board.MarkLot(lotVal);
                long boardTest = board.CheckWin();
                if (boardTest != 0)
                {
                    result = boardTest * lotVal;
                    break;
                }
            }

            return result;
        }
        [Benchmark]
        public override long PartTwo()
        {
            long result = 0;
            long lastLength = 0;

            Board board = new Board();
            board.EnterFields(this.Items);
            
            foreach (string lot in this._lots)
            {
                long lotVal = Convert.ToInt64(lot);
                board.MarkLot(lotVal);
                long boardTest = board.CheckWin();
                long length = board.AmountOfWinners();
                if (boardTest != 0)
                {
                    if (lastLength == length)
                    {
                        break;
                    }
                    result = boardTest * lotVal;
                    lastLength = length;
                }
            }

            return result;
        }
    }
}