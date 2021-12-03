using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode {

    public class LineReader {

        public List<string> GatherLines(string lines) {

            List<string> tempList = new List<string>(lines.TrimEnd('\r').Split("\n"));

            List<string> result = tempList.Where(x => !string.IsNullOrEmpty(x)).ToList();

            return result;


        }

    }

}