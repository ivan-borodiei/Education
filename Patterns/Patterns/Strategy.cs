using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    interface ISortable
    {
        void Sort<T>(T[] collect) where T:IComparable<T>;
    }

    class SelectionSort: ISortable
    {
        public void Sort<T>(T[] collect) where T : IComparable<T>
        {
            for (int i = 0; i < collect.Length; i++)
            {
                for (int j = i + 1; j < collect.Length; j++ )
                {
                    if (collect[j].CompareTo(collect[i]) < 0)                    
                    {
                        T tmp = collect[j];
                        collect[j] = collect[i];
                        collect[i] = tmp;
                    }
                }
            }
        }

    }


    class Subject<T> where T: IComparable<T>
    {        
        private ISortable sortAlgorytm;
        public T[] collection { get; set; }

        public Subject(ISortable sortAlgorytm)
        {
            this.sortAlgorytm = sortAlgorytm;
        }

        public void Sort()
        {
            sortAlgorytm.Sort<T>(collection);
        }

        public void ShowSortedCollection()
        {
            foreach (T i in collection)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }
    }
}
