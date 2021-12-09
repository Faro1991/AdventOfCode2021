using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode
{
    public class Day8 : DayBase
    {
        private Dictionary<string, string> _originalMapping = new Dictionary<string, string>();
        private List<List<string>> _inputStrings = new List<List<string>>();
        private List<List<string>> _digitStrings = new List<List<string>>();
        private List<string> _uniqueDigits = new List<string>();
        private void Initialize()
        {
            if (this._inputStrings.Count == 0)
            {
                this._inputStrings = this.Items.Select(x => x.Split("|").Where(x => !string.IsNullOrEmpty(x)).ToList()[0]).Select(x => x.Split(" ").Where(x => !string.IsNullOrEmpty(x)).ToList()).ToList();
                this._digitStrings = this.Items.Select(x => x.Split("|").Where(x => !string.IsNullOrEmpty(x)).ToList()[1]).Select(x => x.Split(" ").Where(x => !string.IsNullOrEmpty(x)).ToList()).ToList();
                this._uniqueDigits = this._digitStrings.SelectMany(x => x).Select(x => x).Where(x => x.Length <= 4 || x.Length == 7).ToList();
            }
            if (this._originalMapping.Count == 0)
            {
                this._originalMapping.Add("abcefg", "0");
                this._originalMapping.Add("cf", "1");
                this._originalMapping.Add("acdeg", "2");
                this._originalMapping.Add("acdfg", "3");
                this._originalMapping.Add("bcdf", "4");
                this._originalMapping.Add("abdfg", "5");
                this._originalMapping.Add("abdefg", "6");
                this._originalMapping.Add("acf", "7");
                this._originalMapping.Add("abcdefg", "8");
                this._originalMapping.Add("abcdfg", "9");
            }
        }

        private Dictionary<string, long> CountUniqueOccurrences(List<string> input)
        {
            Dictionary<string, long> result = new Dictionary<string, long>();
            List<char> countInput = input.SelectMany(x => x).Select(x =>{
                string xAsString = x.ToString();
                if (result.ContainsKey(xAsString)){
                    ++result[xAsString];
                    return x;
                }
                result.Add(xAsString, 1);
                return x;
            }).ToList();
            return result;
        }
        private List<string> DeduceOutput(List<string> input)
        {
            List<string> result = new List<string>();
            List<string> alreadyMapped = new List<string>();

            // prepare necessary sets/strings
            string one = input.Where(x => x.Length == 2).Select(x => x).First().ToString();
            List<string> twoThreeFive = input.Where(x => x.Length == 5).Select(x => x).ToList();
            string four = input.Where(x => x.Length == 4).Select(x => x).First().ToString();
            string seven = input.Where(x => x.Length == 3).Select(x => x).First().ToString();
            string eight = input.Where(x => x.Length == 7).Select(x => x).First().ToString();
            
            // deduce a by distinct set of 7 and 1
            string mapToA = new string(seven.ToList().Where(x => !one.ToList().Contains(x)).Select(x => x).ToArray());
            alreadyMapped.Add(mapToA);
            
            // deduce b, e and f by counting occurences across input
            Dictionary<string, long> countAllValues = this.CountUniqueOccurrences(input);
            string mapToB = countAllValues.Where(x => x.Value == 6).Select(x => x.Key).First();
            string mapToE = countAllValues.Where(x => x.Value == 4).Select(x => x.Key).First();
            string mapToF = countAllValues.Where(x => x.Value == 9).Select(x => x.Key).First();
            alreadyMapped.Add(mapToB);
            alreadyMapped.Add(mapToE);
            alreadyMapped.Add(mapToF);
            
            // deduce c by distinct set of {2, 3, 5} and {a, b, e, f}
            List<string> filterTwoThreeFive = twoThreeFive.Select(x => {
                foreach(string mapped in alreadyMapped)
                {
                    x = x.Replace(mapped, "");
                }
                return x;
            }).ToList();
            Dictionary<string, long> findC = this.CountUniqueOccurrences(filterTwoThreeFive);
            string mapToC = findC.Where(x => x.Value == 2).Select(x => x.Key).First();
            alreadyMapped.Add(mapToC);

            // deduce d by distinct set of 4 and {a, b, c, e, f}
            string mapToD = four;
            foreach (string mapped in alreadyMapped)
            {
                mapToD = mapToD.Replace(mapped, "");
            }
            alreadyMapped.Add(mapToD);

            // deduce g by distinct set of 8 and {a, b, c, d, e, f}
            string mapToG = eight;
            foreach (string mapped in alreadyMapped)
            {
                mapToG = mapToG.Replace(mapped, "");
            }
            
            result.Add(mapToA);
            result.Add(mapToB);
            result.Add(mapToC);
            result.Add(mapToD);
            result.Add(mapToE);
            result.Add(mapToF);
            result.Add(mapToG);

            return result;
        }

        private string ReturnDigitValue(List<string> input, int pos)
        {

            string result = "";

            Dictionary<string, string> mappedValues = new Dictionary<string, string>();
            mappedValues.Add("a", input[0]);
            mappedValues.Add("b", input[1]);
            mappedValues.Add("c", input[2]);
            mappedValues.Add("d", input[3]);
            mappedValues.Add("e", input[4]);
            mappedValues.Add("f", input[5]);
            mappedValues.Add("g", input[6]);



            foreach (string output in this._digitStrings[pos])
            {
                string inputToFind = "";
                foreach (char currentChar in output)
                {
                    inputToFind += mappedValues.Where(x => x.Value == currentChar.ToString()).Select(x => x.Key).First().ToString();
                }
                List<char> tmp = inputToFind.ToCharArray().ToList();
                tmp.Sort();
                inputToFind = new string(tmp.ToArray());
                result += this._originalMapping[inputToFind];
            }

            return result;
        }

        [Benchmark]
        public override long PartOne()
        {
            if (this._uniqueDigits.Count == 0)
            {
                this.Initialize();
            }
            long result  = 0;
            result = this._uniqueDigits.Count();

            return result;
        }
        [Benchmark]
        public override long PartTwo()
        {
            long result  = 0;
            int pos = 0;
            if (this._inputStrings.Count > 0)
            {
                foreach (List<string> signals in this._inputStrings)
                {
                    List<string> mappedOutput = this.DeduceOutput(signals);
                    result += Convert.ToInt64(this.ReturnDigitValue(mappedOutput, pos));
                    ++pos;
                }
            }

            return result;
        }
    }
}