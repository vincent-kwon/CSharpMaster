using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETConsoleApp
{
    class PClass : ICloneable//@20180108-vincent: no 한정자 for class
    {
        public string Name;
        public string Color;
        public static int Count = 0;
        public PClass() //@20180108-vincent: need public 한정자
        {
            Name = "Eve";
            Color = "White";

            Console.WriteLine("PClass(): {0} {1} ", Name, Color); //@20180108-vincent: print
        }

        protected PClass(int b) : this()
        {
            Console.WriteLine("PClass(int b) {0}", b);
        }

        public PClass(int b, int c) : this(b)
        {
            Console.WriteLine("PClass(int b, int c) {0}", c);
        }

        ~PClass() //@20180108-vincent: no 한정자
        {
            Console.WriteLine("destructor: {0} {1}", Name, PClass.Count);
        }

        public Object Clone() //@20180108-vincent: ICloneable interface
        {
            PClass newPc = new PClass();
            newPc.Name = this.Name;
            newPc.Color = this.Color;
            return newPc;
        }

        public void Test()
        {
            Console.WriteLine(this.Name + "PClass's Test()");
        }
        protected internal int Age => 100;
    }

    sealed class PPClass : PClass //@20180108-vincent: can't be inherited
    {

        public PPClass()
        {
            Console.WriteLine("Protected internal age: " + Age);
        }

        public void Test()
        {
            Console.WriteLine(this.Name + "PPClass's Test()");
            base.Test(); //@20180108-vincent: usage of base
        }
    }

    public class Program
    {
        internal int GetInt (int x) => x + 100; //@20180108-vincent: element body  

        static void Main(string[] args)
        {
            System.Diagnostics.Debug.WriteLine("Hello");
            Program p = new Program();
            Console.WriteLine("Element body:" + p.GetInt(7));
            PClass pc = new PClass(100,200);
            PClass shallowCopyPc = pc;
            PClass deepCopyPc = new PClass();
            deepCopyPc.Name = pc.Name;
            deepCopyPc.Color = pc.Color; //@20180108-vincent: deep copy
            PPClass pp = new PPClass();
            pp.Test();
            Ironman tony = new Ironman();
            tony.Initialize();
            ArmorSuite amSuite = tony;
            amSuite.Initialize();

        }
    }
}
