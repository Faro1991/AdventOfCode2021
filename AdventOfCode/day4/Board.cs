using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
                content[pos] = tempArr;
            }
            public void MarkLots(long lot)
            {
                for (int i = 0; i < content.Length; ++i)
                {
                    content[i] = content[i].Select(x => 
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
                int count = 0;
                for (int i = 0; i < content.Length; ++i)
                {
                    if (content[pos][i].marked)
                    {
                        ++count;
                    }
                }
                return count == content.Length;
            }
            private bool CheckCol(int pos)
            {
                int count = 0;
                for (int i = 0; i < content.Length; ++i)
                {
                    if (content[i][pos].marked)
                    {
                        ++count;
                    }
                }
                return count == content.Length;
            }
            public bool CheckResults(long lot)
            {
                var win = false;
                for (int i = 0; i < content.Length; ++i)
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
                long result = 0;

                for (int i = 0; i < content.Length; ++i)
                {
                    result += content[i].Where(x => !x.marked).Select(x => x.value).Sum();
                }

                return result;
            }
            public void DeclareWinner()
            {
                this.wonAlready = true;
            }
        }
        private List<_field> _fields = new List<_field>();
        private HashSet<_field> _winners = new HashSet<_field>();

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
            foreach (_field candidate in _fields)
            {
                if (candidate.CheckResults(lot) && !this._fields[count].wonAlready)
                {
                    result = this._fields[count].GetUnmarkedSum();
                    if (result != 0)
                    {
                        if (!this._fields[count].wonAlready)
                        {
                            candidate.DeclareWinner();
                            this._winners.Add(candidate);
                        }
                        this._fields[count] = candidate;
                        return result;
                    }
                }
                ++count;
            }
            return 0;
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

        public long[] ReturnFirstWinner()
        {
            long[] result = {this._winners.First().winningNum, this._winners.First().winningLot};
            return result;
        }

        public long[] ReturnLastWinner()
        {
            List<_field> winnerList = this._winners.ToList();
            long[] result = {winnerList.Last().winningNum, winnerList.Last().winningLot};
            return result;
        }
    }
}