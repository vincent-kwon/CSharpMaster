﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETConsoleApp
{
    //@20180109-vincent: 
    delegate int Cal2(int i, int j); //@20180109-vincent: better define it in the namespace area

    class EventDelegateAction
    {
        int divMethod(int i, int j) => i / j;
        static int mulMethod(int i, int j) => i * j;
        public delegate int Cal(int i, int j);
        delegate int Compare<T>(T a, T b);

        //@20180109-vincent: Name event handler is not important, any delegator is fine
        public delegate void EventHandler(string message);

        public delegate void EventHandler2(int i);

        //@20180109-vincent: event is just simple repository of EventHandler
        public event EventHandler SomethingHappened;
        public event EventHandler2 SomethingHappened2;

        static int AscendCompare<T>(T a, T b) where T : IComparable<T>
        {
            Console.WriteLine("Ascending compare....");
            return a.CompareTo(b);
        }

        static int DescendCompare<T>(T a, T b) where T : IComparable<T>
        {
            Console.WriteLine("Descending compare....");
            return a.CompareTo(b);
        }

        static void BubbleSort<T>(T[] dataSet, Compare<T> comparer)
        {
            comparer(dataSet[0], dataSet[1]);
        }

        delegate int Calculator(int a, int b);

        public void TestDelegate()
        {
            Cal divOp = new Cal(divMethod); //@20180109-vincent: new is ok for delegate
            Console.WriteLine(divOp(1000, 200));

            Cal2 mulOp = EventDelegateAction.mulMethod; //@20180109-vincent: // also instance method and static method all can be delegate
            Console.WriteLine(mulOp(1000, 200));

            //@20180109-vincent: delegate chain is possible !!!
            BubbleSort<int>(new int[] { 7, 8 }, new Compare<int>(AscendCompare) + new Compare<int>(DescendCompare));

            Calculator calc;
            calc = delegate (int a, int b) //@20180109-vincent: method can be anonymous like this
            {
                return a + b;
            };
            Console.WriteLine("Anonymouse delegate: " + calc(3, 4));
        }

        //public void MyHandler(string message)
        //{
        //    Console.WriteLine("<my handler event>");
        //}

        public void TestEvent()
        {
            SomethingHappened += new EventHandler(delegate (string message)
            {
                Console.WriteLine("Event callback !!!!!");
            });
            SomethingHappened("Event Invoker....");

            SomethingHappened2 += new EventHandler2(delegate (int i)
            {
                Console.WriteLine("1st delegate " + i);
            });

            SomethingHappened2 += new EventHandler2(delegate (int i)
            {
                Console.WriteLine("2nd delegate " + i);
            });
            SomethingHappened2(7777777); //@20180109-vincent: This can't be called outside but delegate is allowed
        } 
    }
}