using BenchmarkDotNet.Running;

namespace AdventOfCode
{

    class CalendarRun
    {


        static void Main() {

            var day1 = new Day1();
            day1.Day = 1;
            day1.Input = @"day1/inputDay1.txt";
            day1.DayRun();

            var day2 = new Day2();
            day2.Day = 2;
            day2.Input = @"day2/inputDay2.txt";
            day2.DayRun();

            var day3 = new Day3();
            day3.Day = 3;
            day3.Input = @"day3/inputDay3.txt";
            day3.DayRun();

            var summary = BenchmarkRunner.Run<Day3>();

        }


    }


}