using BenchmarkDotNet.Running;

namespace AdventOfCode
{

    class calendarRun
    {


        static void Main() {

            var day1 = new day1();
            day1.day = 1;
            day1.input = @"Day1\inputDay1.txt";
            day1.dayRun();

            var summary = BenchmarkRunner.Run<day1>();

        }


    }


}