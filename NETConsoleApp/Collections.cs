using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETConsoleApp
{
    class Collections
    {
        public Collections()
        {
            arrayInt = new int[10];
            TestArray();
            TestCollections();
        }

        public void TestArray()
        {
            string[] strs = new string[3] { "A", "B", "C" }; //@20180108-vincent: array declaration
            string[] str2 = new string[] { "A", "B", "C" }; //@20180108-vincent: array declaration
            string[] str3 = { "A", "B", "C" }; //@20180108-vincent: array declaration
            Console.WriteLine(strs.GetType() + " of " + strs.Length);
            Console.WriteLine(strs.GetLength(0)); // dimension
            //Sort, ForEac, FindIndex, Resize, Clear
            int[] scores = { 80, 74, 81, 90, 34 };
            foreach (int s in scores)
            Console.WriteLine("Value: " + s);
            Console.WriteLine("binary search : 81 is at {0}", Array.BinarySearch<int>(scores, 81));
            Console.WriteLine("array.indexof : 90 is at {0}", Array.IndexOf<int>(scores, 90));

            //2-dimentional array
            int[,] array2 = new int[2, 2];
            array2[0, 0] = 1;
            array2[0, 1] = 1;
            array2[1, 0] = 1;
            array2[1, 1] = 1;

            int[,] arr = new int[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };

            //@20180108-vincent: variable array
            int[][] jagged = new int[3][];
            jagged[0] = new int[5] { 1, 2, 3, 4, 6 };
            jagged[1] = new int[2] { 2, 3 };
            jagged[2] = new int[3] { 100, 200, 300 };
        }
        void TestCollections()
        {
            // ArrayList @20180109-vincent-c++: like vector
            ArrayList list = new ArrayList();
            list.Add(10);
            list.Add(20);
            list.Add(30);
            list.RemoveAt(1);
            list.Insert(1, 25); //@20180109-vincent: add in front of 25
            foreach (int i in list) Console.WriteLine(i);

            Queue queue = new Queue();
            queue.Enqueue(1);
            queue.Enqueue(2);
            Console.WriteLine(queue.Dequeue());
            Console.WriteLine(queue.Dequeue());

            // stack
            Stack stack = new Stack();
            stack.Push(1);
            stack.Push(2);
            Console.WriteLine(stack.Pop() + " " + stack.Count);
            Console.WriteLine(stack.Pop());

            // hashtable
            Hashtable hash = new Hashtable();
            hash["good"] = "morning";
            Console.WriteLine(hash["good"]);
            hash["bye"] = "Jennie";
            Console.WriteLine(hash["bye"] + " " + hash.Count);

            foreach (DictionaryEntry e in hash) Console.WriteLine(e.Key + "," + e.Value);

            // dictionary
            Dictionary<int, string> dc = new Dictionary<int, string>();
            dc.Add(1, "A");
            dc.Add(2, "B");
            dc.Add(3, "C");

            foreach (KeyValuePair<int, string> kvp in dc)
            {
                Console.WriteLine(kvp.Key + " - " + kvp.Value);
            }

            Hashtable ht = new Hashtable();
            ht.Add(1, "A");
            ht.Add(2, "B");
            ht.Add(3, "C");

            Console.WriteLine("Hashtable");

            foreach (DictionaryEntry de in ht)
            {
                Console.WriteLine(de.Key + " - " + de.Value);
            }
        }
        private int[] arrayInt;
        public int this[int index] //@20180108-vincent: indexer grammar
        {
            get
            {
                return arrayInt[index];
            }
            set
            {
                if (index > arrayInt.Length)
                {
                    Array.Resize<int>(ref arrayInt, 12);
                }
            }
        }
    }
}
