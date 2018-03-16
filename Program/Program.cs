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
            IDictionary<int, int> redBlackDictionary = new RedBlackDictionary<int, int>();
            IDictionary<int, int> avlDictionary = new AVLDictionary<int, int>();
            IDictionary<int, int> dictionary = new Dictionary<int, int>();


            // Repeat 320 times.
            int count = 320;

            Add(redBlackDictionary, count);
            Add(avlDictionary, count);
            Add(dictionary, count);     

            Retrieve(redBlackDictionary, count);
            Retrieve(avlDictionary, count);
            Retrieve(dictionary, count);

            Delete(redBlackDictionary, count);
            Delete(avlDictionary, count);
            Delete(dictionary, count);


            redBlackDictionary.Clear();
            avlDictionary.Clear();
            dictionary.Clear();

            // Repeat 640 times.
            count = 640;
 
            Add(redBlackDictionary, count);
            Add(avlDictionary, count);
            Add(dictionary, count);

            Retrieve(redBlackDictionary, count);
            Retrieve(avlDictionary, count);
            Retrieve(dictionary, count);

            Delete(redBlackDictionary, count);
            Delete(avlDictionary, count);
            Delete(dictionary, count);

            redBlackDictionary.Clear();
            avlDictionary.Clear();
            dictionary.Clear();

            // Repeat 1280 times.
            count = 1280;

            Add(redBlackDictionary, count);
            Add(avlDictionary, count);
            Add(dictionary, count);

            Retrieve(redBlackDictionary, count);
            Retrieve(avlDictionary, count);
            Retrieve(dictionary, count);

            Delete(redBlackDictionary, count);
            Delete(avlDictionary, count);
            Delete(dictionary, count);
        }

        /// <summary>
        /// Fills dictionary with random elements.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="count">The count.</param>
        static void Add(IDictionary<int, int> dictionary, int count)
        {
            Random random = new Random(5);
            double elapsedTime;

            var watch = Stopwatch.StartNew(); 
            while(dictionary.Count < count)
            { 
                try
                {
                    dictionary.Add(random.Next(), random.Next());
                }
                catch(Exception)
                {
                }
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);

            Console.WriteLine($"Time it took to create and fill the {dictionary.GetType().Name} with {count} random elements is {elapsedTime} microseconds. \n");
        }

        /// <summary>
        /// Deletes randomly elements from dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        static void Delete(IDictionary<int, int> dictionary, int count)
        {
            Random random = new Random(5);
            double elapsedTime;

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < count; i++)
            {
                dictionary.Remove(random.Next());
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);

            Console.WriteLine($"Time it took to  try remove elements from {dictionary.GetType().Name} {count} times is {elapsedTime} microseconds. \n");
        }

        /// <summary>
        /// Retrieves elements from dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        static void Retrieve(IDictionary<int, int> dictionary, int count)
        {
            Random random = new Random(5);
            double elapsedTime;

            var watch = Stopwatch.StartNew();
            int value;
            for (int i = 0; i < count; i++)
            {
                dictionary.TryGetValue(random.Next(), out value);
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);

            Console.WriteLine($"Time it took to  try get elements from {dictionary.GetType().Name} {count} times is {elapsedTime} microseconds. \n");
        }

        
    }
}
