using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode
{
    public class Day10 : DayBase
    {
        private List<string> _cleanedLines = new List<string>();
        private List<string> _invalidLines = new List<string>();
        private List<string> _corruptLines = new List<string>();
        private string[] _validPairs = {"()", "[]", "{}", "<>"};
        private Dictionary<string, long> _closerValues = new Dictionary<string, long>();
        private Dictionary<string, long> _completionValues = new Dictionary<string, long>();
        private void Initialize()
        {
            this._closerValues.Add(")", 3);
            this._closerValues.Add("]", 57);
            this._closerValues.Add("}", 1197);
            this._closerValues.Add(">", 25137);

            this._completionValues.Add(")", 1);
            this._completionValues.Add("]", 2);
            this._completionValues.Add("}", 3);
            this._completionValues.Add(">", 4);
        }
        private string RemoveValidPairs(string input)
        {
            string result = input;
            foreach (string validPair in this._validPairs)
            {
                result = result.Replace(validPair, "");
            }
            if (result == input)
            {
                return result;
            }
            return RemoveValidPairs(result);
        }
        private string RemoveOpeners(string input)
        {
            string result = input;
            foreach (string validPair in this._validPairs)
            {
                result = result.Replace(validPair[0].ToString(), "");
            }
            if (result == input)
            {
                return result;
            }
            return RemoveOpeners(result);

        }
        private long CompletingMatchesSum(string input)
        {
            long result = 0;
            foreach (char c in input)
            {
                result *= 5;
                result += this._completionValues[c.ToString()];
            }
            return result;
        }
        [Benchmark]
        public override long PartOne()
        {
            if (this._closerValues.Count == 0)
            {
                this.Initialize();
            }
            this._cleanedLines = this.Items.Select(x => RemoveValidPairs(x)).ToList();
            this._corruptLines = this._cleanedLines.Where(x => 
            {
                foreach (char bracket in x)
                {
                    if (this._closerValues.Keys.Contains(bracket.ToString()))
                    {
                        return true;
                    }
                }
                return false;
            }).ToList();
            this._invalidLines = this._cleanedLines.Where(x => 
            {
                foreach (char bracket in x)
                {
                    if (this._closerValues.Keys.Contains(bracket.ToString()))
                    {
                        return false;
                    }
                }
                return true;
            }).ToList();

            long result = 0;
            List <string> firstClosers = this._corruptLines.Select(x => RemoveOpeners(x)).ToList();
            result = firstClosers.Select(x => this._closerValues[x[0].ToString()]).Sum();

            return result;
        }
        [Benchmark]
        public override long PartTwo()
        {
            long result = 0;

            List<string> completingMatches = this._invalidLines.Select(x => 
            {
                string result = "";
                for (int i = x.Length - 1; i >= 0; --i)
                {
                    char c = x[i];
                    if (c != '(')
                    {
                        result += (char) (c + 2);
                    }
                    else
                    {
                        result += (char) (c + 1);
                    }
                }
                return result;
            }).ToList();

            List<long> completionResults = completingMatches.Select(x => this.CompletingMatchesSum(x)).ToList();
            completionResults.Sort();

            if (completionResults.Count > 0)
            {
                result = completionResults[completionResults.Count / 2];
            }

            return result;
        }
    }
}