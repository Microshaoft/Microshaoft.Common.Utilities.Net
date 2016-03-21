﻿namespace Test
{
    using Microshaoft;
    using System;
    //using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Diagnostics;
    class Program
    {
        static void Main(string[] args)
        {
            var enabledCountPerformance = false;
            var q = new ConcurrentAsyncQueue<int>();
            q.AttachPerformanceCounters
                    (
                        "new"
                        , "Microshaoft ConcurrentAsyncQueue Performance Counters"
                        , new QueuePerformanceCountersContainer()
                        , ()=>
                        {
                            return enabledCountPerformance;
                        }
                        , PerformanceCounterInstanceLifetime.Process
                        , 0
                    );
            Random random = new Random();
            q.OnDequeue += //new ConcurrentAsyncQueue<int>.QueueEventHandler
                            (
                                (x) =>
                                {
                                    //int sleep = random.Next(0, 9) * 5;
                                    //Console.WriteLine(sleep);
                                    Thread.Sleep(50);
                                    //if (sleep > 400)
                                    //{
                                    //    throw new Exception();
                                    //    Console.WriteLine(x);
                                    //}

                                }
                            );
            q.OnEnqueueProcessCaughtException += //new ConcurrentAsyncQueue<int>.ExceptionEventHandler
                                    (
                                        (x, y, z, w) =>
                                        {
                                            Console.WriteLine(y.ToString());
                                            return false;
                                        }
                                    );
            q.OnDequeueProcessCaughtException += //new ConcurrentAsyncQueue<int>.ExceptionEventHandler
                                                (
                                                    (x, y, z, w) =>
                                                    {
                                                        Console.WriteLine(y.ToString());
                                                        return false;
                                                    }
                                                );

            Console.WriteLine("begin ...");
            //q.StartAdd(10);
            string r = string.Empty;
            while ((r = Console.ReadLine()) != "q")
            {
                int i;
                if (int.TryParse(r, out i))
                {
                    Console.WriteLine("Parallel Enqueue {0} begin ...", i);
                    new Thread
                            (
                                new ParameterizedThreadStart
                                            (
                                                (x) =>
                                                {
                                                    Parallel.For
                                                                (
                                                                    0
                                                                    , i
                                                                    , (xx) =>
                                                                    {
                                                                        q.Enqueue(xx);
                                                                    }
                                                                );
                                                    Console.WriteLine("Parallel Enqueue {0} end ...", i);
                                                }
                                            )
                            ).Start();
                }
                else if (r.ToLower() == "stop")
                {
                    q.StartDecreaseDequeueProcessThreads(10);
                }
                else if (r.ToLower() == "add")
                {
                    q.StartIncreaseDequeueProcessThreads(20);
                }
                else if (r.ToLower() == "countstart")
                {
                    enabledCountPerformance = true;
                }
                else if (r.ToLower() == "countstop")
                {
                    enabledCountPerformance = false;
                }
                else if (r.ToLower() == "clear")
                {
                    q.ClearPerformanceCountersValues(10);
                    q.ClearPerformanceCountersValues(100);
                    q.ClearPerformanceCountersValues(1000);
                }
                else if (r.ToLower() == "pool")
                {
                    var s = string.Format
                                    (
                                        "Pool.Count: [{0}], PooledObjectsCount Got:[{1}]-Return:[{2}], NonPooledObjectsCount Got:[{3}]-Release:[{4}]"
                                       , q
                                            .StopwatchsPool
                                            .Pool
                                            .Count
                                       , q
                                            .StopwatchsPool
                                            .PooledObjectsGotCount
                                       , q
                                            .StopwatchsPool
                                            .PooledObjectsReturnCount
                                       , q
                                            .StopwatchsPool
                                            .NonPooledObjectsGotCount
                                       , q
                                            .StopwatchsPool
                                            .NonPooledObjectsReleaseCount
                                    );
                    Console.WriteLine(s);
                    
                }
                else
                {
                    Console.WriteLine("please input Number!");
                }
            }
            Console.ReadLine();
        }
    }
}