using BenchmarkDotNet.Running;

namespace AdventOfCode
{

    class CalendarRun
    {


        static void Main() {

            /* var day1 = new Day1();
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

            var day4 = new Day4();
            day4.Day = 4;
            day4.Input = @"day4/inputDay4.txt";
            day4.DayRun();

            var day5 = new Day5();
            day5.Day = 5;
            day5.Input = @"day5/inputDay5.txt";
            day5.DayRun();

            var day6 = new Day6();
            day6.Day = 6;
            day6.Input = @"day6/inputDay6.txt";
            day6.DayRun();

            var day7 = new Day7();
            day7.Day = 7;
            day7.Input = @"day7/inputDay7.txt";
            day7.DayRun();*/

            var day8 = new Day8();
            day8.Day = 8;
            day8.Input = @"day8/inputDay8.txt";
            day8.DayRun();

            var summary = BenchmarkRunner.Run<Day7>();

        }


    }


}