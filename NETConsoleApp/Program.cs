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

        protected PClass(int b) : this() //@20180108-vincent-c++: In c++, base
        {
            Console.WriteLine("PClass(int b) {0}", b);
        }

        public PClass(int b, int c) : this(b)
        {
            Console.WriteLine("PClass(int b, int c) {0}", c);
        }

        ~PClass() //@20180108-vincent: no 한정자 //@02180108-vincent-c++: In c++, virtual needed
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
            Collections cl = new Collections();

            EnumerableTest<string> et = new EnumerableTest<string>();
            for (int i = 0; i < 5; i++) et[i] = i.ToString();
            foreach (var e in et) Console.WriteLine("EnumerationTest: " + e );

            Generic<int> gInt = new Generic<int>();
            gInt.AddElement(5,0);
            gInt.AddElement(6,1);
            gInt.AddElement(7,2);
            Console.WriteLine("Generic: " + gInt.GetElement(0));
            Console.WriteLine("Generic: " + gInt.GetElement(1));
            try
            {
                Exceptions.TestException();
            }
            finally
            {
                Console.WriteLine("Calling finally");
            }
            EventDelegateAction del = new EventDelegateAction();
            del.TestDelegate();
            del.TestEvent();

            NETConsoleApp.Lambda l = new NETConsoleApp.Lambda();
            l.TestLambda();

            FunctionAction fa = new FunctionAction();
            fa.TestFunction();
            fa.TestAction();
            fa.TestExpression();

            ReflectionAttribute ra = new ReflectionAttribute();
            ra.TestReflection();
            ra.TestAttribute();

            Dynamic dm = new Dynamic();
            dm.TestDynamic();
            ThreadAndTask tt = new ThreadAndTask();
            tt.TestThread();
            tt.TestMonitor();
            tt.TestTask();
            //tt.TestTaskComplex();
            tt.TestParallel();
            tt.TestAsync();
            Console.ReadLine();
            SpecialOperators.TestSpecials();
        }
    }
}
