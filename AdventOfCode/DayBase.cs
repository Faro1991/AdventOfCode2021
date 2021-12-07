using System;
using System.Collections.Generic;
using System.Net;

namespace AdventOfCode
{
    
    public abstract class DayBase {

        public int Day {get; set;}
        public string Input {get; set;}

        public List<string> Items = new List<string>();

        public abstract long PartOne();
        public abstract long PartTwo();

        public virtual void GetInput(int day) {

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

                WebResponse InputText = download.GetResponse();

                string Result = new System.IO.StreamReader(InputText.GetResponseStream()).ReadToEnd();

                System.IO.File.WriteAllText(filePath, Result);
            }
            catch (WebException e) {

                Console.WriteLine("File not (yet) found on server");
                Console.WriteLine(e);

            }

        }

        public virtual void Setup()
        {
            if (!System.IO.Directory.Exists("day" + this.Day)) {

                System.IO.Directory.CreateDirectory("day" + this.Day);

            }

            if (!System.IO.File.Exists(this.Input)) {

                GetInput(this.Day);

            }
            

            LineReader Read = new LineReader();

            try {
                string text = System.IO.File.ReadAllText(this.Input);
                this.Items = Read.GatherLines(text);
            }
            catch (System.IO.FileNotFoundException e) {

                Console.WriteLine("could not find file, skipping day " + Day);
                Console.WriteLine(e);

            }
        }

        public virtual void DayRun() {

            this.Setup();
            ResultWriter Write = new ResultWriter();

            try {

                long Result = PartOne();
                long ResultPartTwo = PartTwo();

                Write.PartResults.Add("part one", Result.ToString());
                Write.PartResults.Add("Part two", ResultPartTwo.ToString());

                Write.WriteResults(Day);
            }
            catch (System.NotImplementedException e) {

                Console.WriteLine("functions not yet implemented. better get going ;)");
                Console.WriteLine(e);

            }
            

        }

    }

}