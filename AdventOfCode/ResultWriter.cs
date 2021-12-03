using System;
using System.Collections.Generic;

namespace AdventOfCode {

    class ResultWriter {

        public List<TimeSpan> TimesTaken {get; set;} = new List<TimeSpan>();

        public Dictionary<string, string> PartResults {get; set;} = new Dictionary<string, string>();
        public void WriteResults(int day) {

            Console.WriteLine("---------------------");
            Console.WriteLine("Day " + day + ": ");
            Console.WriteLine("---------------------");
            foreach (KeyValuePair<string, string> item in PartResults) {

                Console.WriteLine(item.Key + ": " + item.Value);

            }

            //deprecated in favour of BenchmarkDotNEt
            /*
            foreach (TimeSpan time in TimesTaken) {

                if (time.TotalMilliseconds < 1000) {

                    Console.WriteLine("Time taken (part " + (TimesTaken.IndexOf(time) + 1) + "): " + time.TotalMilliseconds + "ms");

                }
                else {

                     Console.WriteLine("Time taken (part " + (TimesTaken.IndexOf(time) + 1) + "): " + time.Seconds + "s");

                }

            }
            */

            Console.WriteLine("\n\n");

        }

    }

}