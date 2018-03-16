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
            // Repeat 320 times.
            IDictionary<int, int> redBlackDictionary = new RedBlackDictionary<int, int>();
            Add(redBlackDictionary, 320);
            Retrieve(redBlackDictionary);
            Delete(redBlackDictionary);

            IDictionary<int, int> avlDictionary = new AVLDictionary<int, int>();
            Add(avlDictionary, 320);
            Retrieve(avlDictionary);
            Delete(avlDictionary);

            IDictionary<int, int> dictionary = new Dictionary<int, int>();
            Add(dictionary, 320);
            Retrieve(dictionary);
            Delete(dictionary);

            // Repeat 640 times.
            redBlackDictionary.Clear();
            Add(redBlackDictionary, 640);
            Retrieve(redBlackDictionary);
            Delete(redBlackDictionary);

            avlDictionary.Clear();
            Add(avlDictionary, 640);
            Retrieve(avlDictionary);
            Delete(avlDictionary);

            dictionary.Clear();
            Add(dictionary, 640);
            Retrieve(dictionary);
            Delete(dictionary);

            // Repeat 1280 times.
            redBlackDictionary.Clear();
            Add(redBlackDictionary, 1280);
            Retrieve(redBlackDictionary);
            Delete(redBlackDictionary);

            avlDictionary.Clear();
            Add(avlDictionary, 1280);
            Retrieve(avlDictionary);
            Delete(avlDictionary);

            dictionary.Clear();
            Add(dictionary, 1280);
            Retrieve(dictionary);
            Delete(dictionary);
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
                    Console.WriteLine("Key is exist.");
                }
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);

            Console.WriteLine($"Time it took to create and fill the {dictionary.GetType().Name} is {elapsedTime} microseconds. \n");
        }

        /// <summary>
        /// Deletes randomly elements from dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        static void Delete(IDictionary<int, int> dictionary)
        {
            Random random = new Random(5);
            double elapsedTime;

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < dictionary.Count; i++)
            {
                dictionary.Remove(random.Next());
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);

            Console.WriteLine($"Time it took to remove elements from {dictionary.GetType().Name} is {elapsedTime} microseconds. \n");
        }

        /// <summary>
        /// Retrieves elements from dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        static void Retrieve(IDictionary<int, int> dictionary)
        {
            Random random = new Random(5);
            double elapsedTime;

            var watch = Stopwatch.StartNew();
            int j;
            for (int i = 0; i < dictionary.Count; i++)
            {
                try
                {
                    j = random.Next();
                    Console.WriteLine($"Key - {j}, Value - {dictionary[j]}");
                }
                catch (Exception)
                {
                    //Console.WriteLine("Item doesn't found.");
                }
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);

            Console.WriteLine($"Time it took to retrieve elements from {dictionary.GetType().Name} is {elapsedTime} microseconds. \n");
        }

        
    }
}
