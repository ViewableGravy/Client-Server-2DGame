using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace testingConcepts
{
    public class Program
    {
        static void Main(string[] args)
        {
            int length = 100000;
            int operationLength = 4;

            Console.WriteLine("linear: " + LinearList(length, operationLength));
            Console.WriteLine("parallel: " + ParallelList(length, operationLength));

            ConcurrentQueue<int> test = new ConcurrentQueue<int>();
            for (int i = 0; i < 100; i++)
                test.Enqueue(i);

            TestExecutionTime(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    Queue<int> newQueue = new Queue<int>();
                    while (test.TryDequeue(out var temp))
                    {
                        newQueue.Enqueue(temp);
                    }
                }
            });

            TestExecutionTime(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    var original = new Queue<int>(test.ToArray());
                }
            });
        }

        public static void TestExecutionTime(Action method)
        {
            var stopwatch = Stopwatch.StartNew();

            method();

            stopwatch.Stop();
            Console.WriteLine("test execution on method. ms: {0}, mms: {1}",
                (int)stopwatch.Elapsed.TotalMilliseconds,
                (int)((stopwatch.Elapsed.TotalMilliseconds - ((int)stopwatch.Elapsed.TotalMilliseconds)) * 1000));

        }




        public static TimeSpan ParallelList(int listLength, int TimePerOperation)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < listLength; i++)
            {
                list.Add(i);
            }

            var stopwatch = Stopwatch.StartNew();

            Parallel.ForEach(list, value =>
            {
                for (int i = 0; i < TimePerOperation; i++) ;
            });

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        public static TimeSpan LinearList(int listLength, int TimePerOperation)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < listLength; i++)
            {
                list.Add(i);
            }

            var stopwatch = Stopwatch.StartNew();

            foreach (int num in list)
            {
                for (int i = 0; i < TimePerOperation; i++) ;
            }

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}
