using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class CustomCollection<T>: IEnumerable<T>, IEnumerator<T>
    {        
        T[] array = new T[5];
        int currentIndex = -1;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (this as IEnumerator<T>);

            //while ((this as IEnumerator<T>).MoveNext())
            //    yield return (this as IEnumerator<T>).Current;

            //foreach (T t in array)
            //{
            //    yield return t;
            //}            
        }        

        IEnumerator IEnumerable.GetEnumerator()
        {
            //return array.GetEnumerator();
            return this as IEnumerator;
        }

        T IEnumerator<T>.Current
        {
            get { return array[currentIndex]; }
        }

        void IDisposable.Dispose()
        {
            (this as IEnumerator).Reset();
        }

        object IEnumerator.Current
        {
            get { return array[currentIndex]; }
        }

        bool IEnumerator.MoveNext()
        {
            if (currentIndex < array.Length - 1)
            {
                currentIndex++;
                return true;
            }
            (this as IEnumerator).Reset();
            return false;
        }

        void IEnumerator.Reset()
        {
            currentIndex = -1;
        }

        public T this [int i]
        {
            get { return array[i]; }
            set { array[i] = value; }
        }
    }
}
