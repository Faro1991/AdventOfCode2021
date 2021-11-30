using System.Collections.Generic;

namespace AdventOfCode {

    public class lineReader {

        public List<string> gatherLines(string lines) {

            List<string> result = new List<string>(lines.TrimEnd('\r').Split("\n"));

            return result;


        }

    }

}