using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETConsoleApp
{
    class Dynamic
    {
        public void TestDynamic()
        {
            dynamic[] arr = new dynamic[] { new Duck(), new Mallard(), new Robot()};
            foreach (dynamic d in arr)
            {
                Console.WriteLine(d.GetType());
                d.Walk();
                Console.WriteLine();
            }
        }
    }
    class Duck
    {
        public void Walk()
        {
            Console.WriteLine(this.GetType() + ".Walk");
        }
    }

    class Mallard : Duck
    {

    }

    class Robot
    {
        public void Walk()
        {
            Console.WriteLine(this.GetType() + ".Walk");
        }
    }
}
