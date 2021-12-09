using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode
{
    public class Day8 : DayBase
    {
        private List<List<string>> _inputStrings = new List<List<string>>();
        private List<List<string>> _digitStrings = new List<List<string>>();
        private List<string> _uniqueDigits = new List<string>();
        private void Initialize()
        {
            this._inputStrings = this.Items.Select(x => x.Split("|").Where(x => !string.IsNullOrEmpty(x)).ToList()[0]).Select(x => x.Split(" ").Where(x => !string.IsNullOrEmpty(x)).ToList()).ToList();
            this._digitStrings = this.Items.Select(x => x.Split("|").Where(x => !string.IsNullOrEmpty(x)).ToList()[1]).Select(x => x.Split(" ").Where(x => !string.IsNullOrEmpty(x)).ToList()).ToList();
            this._uniqueDigits = this._digitStrings.SelectMany(x => x).Select(x => x).Where(x => x.Length <= 4 || x.Length == 7).ToList();
        }
        private List<string> DeduceOutput(List<string> input)
        {
            List<string> result = new List<string>();
            string candidateCE = "";

            return result;
        }
        [Benchmark]
        public override long PartOne()
        {
            this.Initialize();
            long result  = 0;
            result = this._uniqueDigits.Count();

            return result;
        }
        [Benchmark]
        public override long PartTwo()
        {
            long result  = 0;
            foreach (List<string> signals in this._inputStrings)
            {
                this.DeduceOutput(signals);
            }

            return result;
        }
    }
}