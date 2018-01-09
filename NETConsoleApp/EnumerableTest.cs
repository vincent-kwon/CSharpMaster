using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETConsoleApp
{
    class EnumerableTest : IEnumerable, IEnumerator
    {
        private int[] array;
        private int position = -1;

        public int this[int index] {
            get {
                return array[index];
            }
            set {
                array[index] = value;
            }
        }

        public EnumerableTest()
        {
            array = new int[10];
        }

        public object Current
        {
            get
            {
                return array[position];
            }
        }
        public IEnumerator GetEnumerator() //@20170108-vincent: yield return returns IEnumerator?
        {
            for(int i = 0; i < array.Length; i++)
            {
                yield return (array[i]);
            }    
        }

        public bool MoveNext() //@20170108-vincent: watch out move next first to iterate each step
        {
            if (position == array.Length - 1)
            {
                Reset();
                return false;
            }

            position++;
            return position < array.Length;
        }

        public void Reset()
        {
            position = -1; //@20170108-vincent: watch out initial points
        }
    }
}
