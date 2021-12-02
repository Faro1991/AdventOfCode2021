using BenchmarkDotNet.Running;

namespace AdventOfCode
{

    class calendarRun
    {


        static void Main() {

            var day1 = new day1();
            day1.day = 1;
            day1.input = @"day1/inputDay1.txt";
            day1.dayRun();

            var day2 = new day2();
            day2.day = 2;
            day2.input = @"day2/inputDay2.txt";
            day2.dayRun();

            var day3 = new day3();
            day3.day = 3;
            day2.input = @"day3/inputDay3.txt";
            //day3.dayRun();

            var summary = BenchmarkRunner.Run<day2>();

        }


    }


}