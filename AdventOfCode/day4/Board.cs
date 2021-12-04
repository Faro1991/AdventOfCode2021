using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AocHelpers
{
    public class Board
    {
        private HashSet<_field> _winners = new HashSet<_field>();
        private struct _number
        {
            public bool marked;
            public long value;
            public _number(string num, bool marked = false) {this.value = Convert.ToInt64(num); this.marked = marked;}
        }
        private struct _field
        {
            public _number[][] content;
            public _field(bool yes = true) {content = new _number[5][];}
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
            public bool CheckResults()
            {
                var win = false;
                for (int i = 0; i < content.Length; ++i)
                {
                    if (CheckRow(i) || CheckCol(i))
                    {
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
        }
        private List<_field> _fields = new List<_field>();
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

        public long CheckWin()
        {
            long result = 0;
            foreach (_field candidate in _fields)
            {
                if (candidate.CheckResults())
                {
                    result = candidate.GetUnmarkedSum();
                    if (this._winners.Add(candidate))
                    {
                        return result;
                    }
                }
            }
            return 0;
        }

        public long AmountOfWinners()
        {
            long result = this._winners.Count();
            return result;
        }
    }
}