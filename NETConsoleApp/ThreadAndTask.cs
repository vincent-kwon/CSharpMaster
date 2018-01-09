using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NETConsoleApp
{
    class Counter
    {
        readonly object thisLock;
        bool lockedCount = false;

        private int count;
        public int Count
        {
            get { return count; }
        }
        public Counter()
        {
            thisLock = new object();
            count = 0;
        }
        public void Increase()
        {
            int loopCount = 1000;
            Console.WriteLine("[Increase]");
            while (loopCount-- > 0)
            {
                lock (thisLock)
                {
                    while (count > 0 || lockedCount == true)
                    {
                        //Console.WriteLine("[Wait] ...");
                        Monitor.Wait(thisLock);
                    }
                    lockedCount = true;
                    count++;
                    //Console.WriteLine(count);
                    lockedCount = false;
                    Monitor.Pulse(thisLock);
                }
            }
        }

        public void Decrease()
        {
            int loopCount = 1000;
            Console.WriteLine("[Decrease]");
            while (loopCount-- > 0)
            {
                lock (thisLock)
                {
                    while (count <= 0 || lockedCount == true)
                    {
                        Console.WriteLine("[Wait] ...");
                        Monitor.Wait(thisLock);
                    }
                    lockedCount = true;
                    count--;
                    //Console.WriteLine(count);
                    lockedCount = false;
                    Monitor.Pulse(thisLock);
                }
            }
        }
    }
    class ThreadAndTask
    {
        private readonly object thisLock = new object();

        //@20180109-vincent: Running, Stopped, Suspended, Aborted, Background
        void DoSomething()
        {
            try
            {
                lock (thisLock)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine("DoSomething: {0} ", i);
                        Thread.Sleep(10); //@20180109-vincent: ms
                        //@20180109-vincent: Thread.Suspend(); Thread.Resume();
                    }
                }

            }
            catch (ThreadInterruptedException e) //@20180109-vincnet: thread interrupt will be caught here
            {
                Console.WriteLine("Thread is interrupted:");
            }
            catch (ThreadAbortException e) 
            {
                Thread.ResetAbort();
            }
            finally
            {
                Console.WriteLine("clean resources for thread: ");
            }
        }

        public void TestThread()
        {
            Thread t1 = new Thread(new ThreadStart(DoSomething));
            t1.Start();
            t1.IsBackground = true;
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Main: {0} ", i);
                Thread.Sleep(10);
            }

            Console.WriteLine("Waiting until thread stops");

            t1.Interrupt();
            Console.WriteLine("State: " + t1.ThreadState);
            t1.Join();
        }

        public void TestMonitor()
        {
            Console.WriteLine("Enter TestMonitor");
            Counter counter = new Counter();
            Thread incThread = new Thread(new ThreadStart(counter.Increase));
            Thread decThread = new Thread(new ThreadStart(counter.Decrease));
            incThread.Start();
            decThread.Start();
            incThread.Join();
            decThread.Join();
            Console.WriteLine("Exit TestMonitor");
        }

        public void TestTask()
        {
            Action someAction = () =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Print async");
            };
            Task myTask = new Task(someAction);
            myTask.Start();

            var myTask2 = Task.Run(() => 
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Printed async2");
                }
            );
            Console.WriteLine("Pinted synchronously");
            myTask.Wait();
        }

        static bool IsPrime(long number)
        {
            if (number < 2)
            {
                return false;
            }

            if (number % 2 == 0 && number != 2)
                return false;

            for (long i = 2; i < number; i++)
            {
                if (number % i == 0) return false;
            }

            return true;
        }

        public void TestTaskComplex()
        {
            long from = 0;
            long to = 100;
            int taskCount = 10;

            Func<object, List<long>> FindPrimeFunc = (objRange) =>
            {
                long[] range = (long[])objRange;
                List<long> found = new List<long>();

                for (long i = range[0]; i < range[1]; i++)
                {
                    if (IsPrime(i)) found.Add(i);
                }

                return found;
            };

            Task<List<long>>[] tasks = new Task<List<long>>[taskCount];

            long currentFrom = from;
            long currentTo = to / tasks.Length;

            for (int i = 0; i < tasks.Length; i++)
            {
                Console.WriteLine("Task [{0}] : {1} - {2} ", i, currentFrom, currentTo);

                tasks[i] = new Task<List<long>>(FindPrimeFunc, new long[] { currentFrom, currentTo });

                currentFrom = currentTo + 1;

                if (i == (tasks.Length - 2))
                {
                    currentTo = to;
                }
                else
                {
                    currentTo = currentTo + (to / tasks.Length);
                }
            }

            Console.WriteLine("please press enter to start");
            Console.ReadLine();
            Console.WriteLine("Started....");

            DateTime startTime = DateTime.Now;

            List<long> total = new List<long>();

            foreach (Task<List<long>> task in tasks)
            {
                task.Wait();
                total.AddRange(task.Result.ToArray()); //@20180109-vincent: this is when returns as array
            }

            DateTime endTime = DateTime.Now;
            TimeSpan ellapsed = endTime - startTime;
            Console.WriteLine("{0} ~ {1} : {2}", from, to, total.Count);
            Console.WriteLine("Ellapsed time: {0}", ellapsed);
        } 

        public void TestParallel()
        {
            long from = 0;
            long to = 100;

            Console.WriteLine("please press enter to start");
            Console.ReadLine();
            Console.WriteLine("Started....");

            DateTime startTime = DateTime.Now;
            List<long> total = new List<long>();
            Parallel.For(from, to, (long i) =>
                {
                    if (IsPrime(i)) total.Add(i);
                }
            );           

            DateTime endTime = DateTime.Now;
            TimeSpan ellapsed = endTime - startTime;
            Console.WriteLine("{0} ~ {1} : {2}", from, to, total.Count);
            Console.WriteLine("Ellapsed time: {0}", ellapsed);
        }

        async public static void MyMethodAsync(int count) //@20180109-vincent: async method MUST be only void, Task, or Task<TResult>
                                                          // use void if you can forget, otherwise Task, Task<TResult>
        {
            Console.WriteLine("CD");
            await Task.Run(
                async () => {
                    for (int i = 1; i <= count; i++)
                    {
                        Console.WriteLine("{0}/{1} ... ", i, count);
                        await Task.Delay(100); //@20180109-vincent: IMPORTANT. this is different from Thread.Sleep since it does not BLOCK
                    }
                }
            );
            Console.WriteLine("GH");
        } 

        public void TestAsync()
        {
            Console.WriteLine("AB");
            MyMethodAsync(3);
            Console.WriteLine("CD");
        }
    }
}

