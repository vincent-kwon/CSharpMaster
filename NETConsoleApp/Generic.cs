using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETConsoleApp
{
    class Generic<T> //where T : class //@20180109-vincent: struct, class, new(), 명시된파생 클/인, U(또다른 매개변수)
    {
        public Generic() {
            array = new T[10];
            TestGenericCollections();
        }

        private void TestGenericCollections()
        {
            List<int> list = new List<int>();
            list.Add(777);
            list.Add(888);
            foreach (var i in list) Console.WriteLine(i);

            Stack<string> ss = new Stack<string>();
            ss.Push("Abc");
            ss.Push("Def");
            foreach (var i in ss) Console.WriteLine(i);

            Queue<int> qq = new Queue<int>();
            qq.Enqueue(10000);
            qq.Enqueue(9999);
            qq.Enqueue(9998);
            Console.WriteLine(qq.Dequeue());
            Console.WriteLine(qq.Dequeue());
            Console.WriteLine(qq.Dequeue());

            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict[0] = "GGG";
            dict[1] = "FFF";
            dict[2] = "III";
            foreach (KeyValuePair<int, string> i in dict) Console.WriteLine(i.Key + ":" + i.Value);
        }

        private T[] array;

        public T GetElement(int index) => array[index];

        public void AddElement(T t, int index) => array[index] = t;
    }
}
