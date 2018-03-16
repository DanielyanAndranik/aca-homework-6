using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionaries;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            AVLDictionary<int, int> avlDictionary = new AVLDictionary<int, int>();
            for (int i = 0; i < 320; i++)
            {
                try
                {
                    avlDictionary.Add(random.Next(1000), i);
                }
                catch(Exception)
                {

                }
            }
            ;

          //  TimeComparision(320);

        }

        static void TimeComparision(int count)
        {
            Random random = new Random(5);
            double elapsedTime;

            var watch = Stopwatch.StartNew();
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            for(int i = 0; i < count; i++)
            {
                dictionary.Add(random.Next(), random.Next());
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);

            Console.WriteLine($"Time it took to create and fill the Dictionary is {elapsedTime} miliseconds.");


            watch = Stopwatch.StartNew();
            AVLDictionary<int, int> avlDictionary = new AVLDictionary<int, int>();
            for (int i = 0; i < count; i++)
            {
                avlDictionary.Add(random.Next(), random.Next());
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);

            Console.WriteLine($"Time it took to create and fill the AVLDictionary is {elapsedTime} miliseconds.");


            watch = Stopwatch.StartNew();
            RedBlackDictionary<int, int> rbDictionary = new RedBlackDictionary<int, int>();
            for (int i = 0; i < count; i++)
            {
                rbDictionary.Add(random.Next(), random.Next());
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);

            Console.WriteLine($"Time it took to create and fill the RedBlackDictionary is {elapsedTime} miliseconds.");

        }
    }
}
