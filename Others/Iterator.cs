using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Others
{
    public static class MyExtentions
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> list, Predicate<T> cond)
        {
            //foreach (var v in list)
            //    if (cond(v))                    
            //        yield return v;            
            MyIterator<T> iter = new MyIterator<T>(list, cond);
            return iter;
        }

    }

    class MyIterator<T> : IEnumerator<T>, IEnumerable<T>
    {
        IList<T> list;
        int index = -1;
        Predicate<T> cond;

        public MyIterator(IEnumerable<T> list, Predicate<T> cond)
        {
            this.list = list.ToList();
            this.cond = cond;
        }

        T IEnumerator<T>.Current
        {
            get { return list[index]; }
        }

        void IDisposable.Dispose()
        {
            ((IEnumerator<T>)this).Reset();
        }

        object IEnumerator.Current
        {
            get { return ((IEnumerator<T>)this).Current; }
        }

        bool IEnumerator.MoveNext()
        {
            while (index < list.Count - 1)
            {
                index++;
                if (cond(((IEnumerator<T>)this).Current))
                    return true;
                else
                {
                    return ((IEnumerator<T>)this).MoveNext();
                }
            }

            ((IEnumerator<T>)this).Reset();
            return false;
        }

        void IEnumerator.Reset()
        {
            index = -1;
        }
        
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }
    }

}
