using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Prime
{
    class Program
    {
        static int min = 1;
        static long max;
        static bool ifPrime;
        static Stopwatch sw = new Stopwatch();
        static List<long> primes = new List<long>();
        static Random rndm = new Random();

        static void Main(string[] args)
        {
            sw.Start();
            Task();
            sw.Stop();

            Console.WriteLine($"The time that has passed is {sw.Elapsed}");
            Console.WriteLine($"The result of the check was that the primes were: {Check()}");
            Console.ReadLine();
        }

        static bool isPrime(long num)
        {
            for (int a = 0; a < primes.Count() - 1 && primes[a] < Math.Sqrt(num); a++)
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
            //Console.WriteLine("What is the minimum? ");
            //min = int.Parse(Console.ReadLine());

            Console.WriteLine("What is the maximum? ");
            max = long.Parse(Console.ReadLine());

            for (int i = min; i <= max; i++)
            {
                ifPrime = isPrime(i);
                //Console.WriteLine($"Is {i} prime: {ifPrime}");
            }
        }

        static bool SlowIsPrime(long num)
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
        }
    }
}
