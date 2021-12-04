using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AocHelpers
{
    public class Board
    {
        private struct _number
        {
            public bool marked;
            public long value;
            public _number(string num, bool marked = true) {this.value = Convert.ToInt64(num); this.marked = marked;}
        }
        private struct _field
        {
            public _number[,] content;
            public _field(bool yes = true) {content = new _number[5,5];}
            public void EnterNumbers(string[] vals, int pos)
            {
                vals.Select(x => StringToNumber(x)).ToArray().CopyTo(this.content, pos);
            }
            public void MarkLots(long lot)
            {
                content.Cast<_number>().Select(x => 
                {
                    if (x.value == lot)
                    {
                        x.marked = true;
                    }
                    return new _number();
                });
            }
            private bool CheckRow(int pos)
            {
                int count = 0;
                for (int i = 0; i < content.GetLength(1); ++i)
                {
                    if (content[pos, i].marked)
                    {
                        ++count;
                    }
                }
                return count == content.GetLength(1);
            }
            private bool CheckCol(int pos)
            {
                int count = 0;
                for (int i = 0; i < content.GetLength(1); ++i)
                {
                    if (content[i, pos].marked)
                    {
                        ++count;
                    }
                }
                return count == content.GetLength(1);
            }
            public bool CheckResults()
            {
                var win = false;
                for (int i = 0; i < content.GetLength(0); ++i)
                {
                    if (CheckRow(i) || CheckCol(i))
                    {
                        win = true;
                    }
                }

                return win;
            }
            public long GetUnmarkedSum()
            {
                long result = content.Cast<_number>().Where(x => !x.marked).Select(x => x.value).Sum();

                return result;
            }
        }
        private List<_field> _fields;
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
                }
            }
            return result;
        }
    }
}