using System;
using System.Collections.Generic;
using System.Net;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace AdventOfCode
{
    
    public abstract class dayBase {

        public int day {get; set;}
        public string input {get; set;}

        public List<string> items = new List<string>();

        public abstract long partOne();
        public abstract long partTwo();

        public virtual void getInput(int day) {

            string filePath = System.IO.Path.Join("day" + day, @"/inputDay" + day + ".txt");

            string domain = @".adventofcode.com";
            string url = @"https://adventofcode.com/2021/day/" + day + @"/input";
            string autcVal = System.IO.File.ReadAllText(@"authCookie.autc");

            try {
                HttpWebRequest download = (HttpWebRequest) WebRequest.Create(url);
                Cookie authCookie = new Cookie("session", autcVal);
                authCookie.Domain = domain;
                download.CookieContainer = new CookieContainer(1);
                download.CookieContainer.Add(authCookie);

                WebResponse inputText = download.GetResponse();

                string result = new System.IO.StreamReader(inputText.GetResponseStream()).ReadToEnd();

                System.IO.File.WriteAllText(filePath, result);
            }
            catch (WebException e) {

                Console.WriteLine("File not (yet) found on server");
                Console.WriteLine(e);

            }

        }

        public virtual void dayRun() {

            if (!System.IO.Directory.Exists("day" + this.day)) {

                System.IO.Directory.CreateDirectory("day" + this.day);

            }

            if (!System.IO.File.Exists(this.input)) {

                getInput(this.day);

            }
            

            lineReader read = new lineReader();
            resultWriter write = new resultWriter();

            try {
                string text = System.IO.File.ReadAllText(this.input);
                this.items = read.gatherLines(text);

                long result = partOne();
                long resultPartTwo = partTwo();

                write.partResults.Add("part one", result.ToString());
                write.partResults.Add("Part two", resultPartTwo.ToString());

                write.writeResults(day);
            }
            catch (System.IO.FileNotFoundException e) {

                Console.WriteLine("could not find file, skipping day " + day);
                Console.WriteLine(e);

            }
            catch (System.NotImplementedException e) {

                Console.WriteLine("functions not yet implemented. better get going ;)");
                Console.WriteLine(e);

            }
            

        }

    }

}