using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Prime
{
    class Program
    {
        static int min = 2;
        static long max;
        static bool ifPrime;
        static Stopwatch sw = new Stopwatch();
        static List<long> primes = new List<long>();
        static Random rndm = new Random();
        static bool firstCurrentlyPrime;
        static bool secondCurrentlyPrime;
        static bool currentlyPrime;
        static int threads = 1000;

        static int currentNum;

        static Task newThread;
        static Task firstHalfThread;
        static Task secondHalfThread;

        static void Main(string[] args)
        {
            //Console.WriteLine("What is the minimum? ");
            //min = int.Parse(Console.ReadLine());

            Console.WriteLine("What is the maximum? ");
            max = long.Parse(Console.ReadLine());

            sw.Start();
            Task();
            sw.Stop();

            Console.WriteLine($"The time that has passed is {sw.Elapsed}");
            //Console.WriteLine($"The result of the check was that the primes were: {Check()}");
            Console.ReadLine();
        }

        static void ThreadedPrime()
        {
            firstCurrentlyPrime = true;
            secondCurrentlyPrime = true;
            firstHalfThread = new Task(checkFirst);
            secondHalfThread = new Task(checkSecond);
            firstHalfThread.Start();
            secondHalfThread.Start();

            while (!(firstHalfThread.IsCompleted && secondHalfThread.IsCompleted)) { /*Console.WriteLine("Waiting");*/ }
            //Console.WriteLine($"{currentNum} {firstHalfThread.IsCompleted} { secondHalfThread.IsCompleted}");
            if (firstCurrentlyPrime && secondCurrentlyPrime)
            {
                primes.Add(currentNum);
            }

            //Console.WriteLine($"Is {i} prime: {ifPrime}");
        }

        static bool isPrime(long num)
        {
            for (int a = 0; a < primes.Count() - 1 && primes[a] <= Math.Sqrt(num); a++)
            {
                if (num % primes[a] == 0)
                {
                    return false;
                }
            }
            primes.Add(num);
            return true;
        }

        static void Task()
        {
            for (int i = min; i <= max; i++)
            {
                currentNum = i;
                //Console.WriteLine(currentNum);
                RunThreads();
                //Console.WriteLine($"Is {i} prime: {ifPrime}");
            }
        }

        static void RunThreads()
        {
            currentlyPrime = true;
            for (int i = 0; i < threads; i++)
            {
                newThread = new Task(() => ThreadPrime(i));
                newThread.Start();
            }

            tasks.WaitAll();
            if (firstCurrentlyPrime && secondCurrentlyPrime)
            {
                primes.Add(currentNum);
            }
        }

        static void ThreadPrime(int section)
        {
            int sectionLen = (primes.Count() - 1) / threads;
            int start = sectionLen * section;
            int end = sectionLen * (section + 1);
            for (int a = start; a < end && primes[a] <= Math.Sqrt(currentNum); a++)
            {
                if (currentNum % primes[a] == 0)
                {
                    currentlyPrime = false;
                }
            }
        }


        static void checkFirst()
        {
            for (int a = 0; a < (primes.Count() - 1) / 2 && primes[a] <= Math.Sqrt(currentNum); a++)
            {
                if (currentNum % primes[a] == 0)
                {
                    currentlyPrime = false;
                }
            }
        }

        static void checkSecond()
        {
            int halfLen = (primes.Count() - 1) / 2;
            for (int a = halfLen; a < primes.Count() - 1 && primes[a] <= Math.Sqrt(currentNum); a++)
            {
                if (currentNum % primes[a] == 0)
                {
                    secondCurrentlyPrime = false;
                }
            }
        }


        /*static bool SlowIsPrime(long num)
        {
            for (int i = 2; i <= Math.Sqrt(num);i ++)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        static bool Check()
        {
            bool listCorrect = true;
            for(int i = 0; i < 10000; i++)
            {
                int num = rndm.Next(0, primes.Count() - 1);
                listCorrect = SlowIsPrime(primes[num]);
            }
            return listCorrect;
        }*/
    }
}
