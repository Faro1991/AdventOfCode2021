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
            long winningLot = 0;
            long boardVal = 0;
            DrawLots();

            Board board = new Board();
            board.EnterFields(this.Items);
            
            foreach (string lot in this._lots)
            {
                
                long lotVal = Convert.ToInt64(lot);
                board.MarkLot(lotVal);
                board.CheckWin(lotVal);
                if (board.FirstWinner())
                {
                    break;
                }
            }
            long[] winningValues = board.ReturnLastWinner();
            boardVal = winningValues[0];
            winningLot = winningValues[1];
            result = boardVal * winningLot;

            return result;
        }
        [Benchmark]
        public override long PartTwo()
        {
            long result = 0;
            long winningLot = 0;
            long boardVal = 0;

            Board board = new Board();
            board.EnterFields(this.Items);
            
            foreach (string lot in this._lots)
            {
                long lotVal = Convert.ToInt64(lot);
                board.MarkLot(lotVal);
                long test = board.CheckWin(lotVal);
            }
            long[] winningValues = board.ReturnLastWinner();
            boardVal = winningValues[0];
            winningLot = winningValues[1];
            result = boardVal * winningLot;

            return result;
        }
    }
}