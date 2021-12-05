using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace AocHelpers
{
    public class Board
    {
        //declare necessary structs & variables
        private struct _number
        {
            public bool marked;
            public long value;
            public _number(string num, bool marked = false) {this.value = Convert.ToInt64(num); this.marked = marked;}
        }
        private struct _field
        {
            public _number[][] content;
            public bool wonAlready {get; set;}
            public long winningLot {get; set;}
            public long winningNum {get; set;}
            public _field(bool yes = true) {content = new _number[5][]; winningLot = 0; winningNum = 0; wonAlready = false;}
            public void EnterNumbers(string[] vals, int pos)
            {
                _number[] tempArr = vals.Where(x => x != "").Select(x => StringToNumber(x)).ToArray<_number>();
                this.content[pos] = tempArr;
            }
            public void MarkLots(long lot)
            {
                for (int i = 0; i < this.content.Length; ++i)
                {
                    this.content[i] = this.content[i].Select(x => 
                    {
                        if (x.value == lot)
                        {
                            x.marked = true;
                        }
                        return x;
                    }).ToArray();
                }
            }
            private bool CheckRow(int pos)
            {
                for (int i = 0; i < this.content.Length; ++i)
                {
                    if (!this.content[pos][i].marked)
                    {
                        return false;
                    }
                }
                return true;
            }
            private bool CheckCol(int pos)
            {
                for (int i = 0; i < this.content.Length; ++i)
                {
                    if (!this.content[i][pos].marked)
                    {
                        return false;
                    }
                }
                return true;
            }
            public bool CheckResults(long lot)
            {
                var win = false;
                for (int i = 0; i < this.content.Length; ++i)
                {
                    if (CheckRow(i) || CheckCol(i))
                    {
                        this.winningLot = lot;
                        this.winningNum = this.GetUnmarkedSum();
                        win = true;
                        break;
                    }
                }

                return win;
            }
            public long GetUnmarkedSum()
            {
                long result = content.SelectMany(x => x).Where(x => !x.marked).Select(x => x.value).Sum();
                return result;
            }
            public void DeclareWinner()
            {
                this.wonAlready = true;
            }
        }
        private List<_field> _fields = new List<_field>();
        private Stack<_field> _winners = new Stack<_field>();

        //class methods
        private static _number StringToNumber(string input)
        {
            if (input != "")
            {
                _number result = new _number(input);
                return result;
            }
            return new _number();
        }

        public void EnterFields(List<string> input)
        {
            _field tmpField = new _field(true);
            int iter = 0;
            foreach (string item in input)
            {
                string[] lineVals = Regex.Replace(item, @"\s+", " ").Split(" ");
                tmpField.EnterNumbers(lineVals, iter);
                if (++iter % 5 == 0)
                {
                    this._fields.Add(tmpField);
                    tmpField = new _field(true);
                    iter = 0;
                }
            }
        }

        public void MarkLot(long lot)
        {
            foreach (_field candidate in _fields)
            {
                candidate.MarkLots(lot);
            }
        }

        public long CheckWin(long lot)
        {
            int count = 0;
            long result = 0;
            List<_field> checkList = this._fields.Select(x => x).ToList();
            foreach (_field candidate in checkList)
            {
                if (!this._fields[count].wonAlready && candidate.CheckResults(lot))
                {
                    result = this._fields[count].GetUnmarkedSum();
                    if (result != 0)
                    {
                        candidate.DeclareWinner();
                        this._winners.Push(candidate);
                        this._fields[count] = candidate;
                    }
                }
                ++count;
            }
            return result;
        }

        public long WinnerCount()
        {
            long result = this._winners.Count;
            return result;
        }

        public bool FirstWinner()
        {
            bool result = this.WinnerCount() > 0;
            return result;
        }

        public long[] ReturnLastWinner()
        {
            _field lastWinner = new _field();
            if (this.WinnerCount() > 0)
            {
                lastWinner = this._winners.Pop();
            }

            
            long[] result = {lastWinner.winningNum, lastWinner.winningLot};
            return result;
        }
    }
}