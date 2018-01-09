using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NETConsoleApp
{
    class EnumerableTest<T> : IEnumerable<T>, IEnumerator<T> //@20180109-vincent: Watch out T in all
    {
        private T[] array;
        private int position = -1;

        public T this[int index] {
            get {
                return array[index];
            }
            set {
                array[index] = value;
            }
        }

        public EnumerableTest()
        {
            array = new T[10];
        }

        object IEnumerator.Current //@20180109-vincent: careful IEnumerator.Current and Current both needed
        {
            get
            {
                return array[position];
            }
        }

        public T Current
        {
            get
            {
                return array[position];
            }
        }

        public IEnumerator<T> GetEnumerator() //@20170108-vincent: yield return returns IEnumerator?
        {
            for(int i = 0; i < array.Length; i++)
            {
                yield return (array[i]);
            }    
        }

        IEnumerator IEnumerable.GetEnumerator() //@20180109-vincent: careful Enumerator<T> IEnumerator.GetEnumerator and T GetEnumerator both needed
        {
            for (int i = 0; i < array.Length; i++)
                yield return (array[i]);
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
            position = -1; //@20170108-vincent: watch out initial poTs
        }

        public void Dispose()
        {

        }
    }
}
