using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            //CustomIterator();
            SortCollection();
            int[] intArrray = new int[3];
            int[] intArrray2 = new int[4];

            string[] strArrray = new string[3];
            string[] strArrray2 = new string[3];

            Program[] progArray = new Program[5];

            Console.ReadLine();
        }

        static void CustomIterator()
        {            
            CustomCollection<int> col = new CustomCollection<int>();
            col[0] = 1;
            col[1] = 2;
            col[2] = 3;
            col[3] = 4;
            col[4] = 5;
            
            int[] ar = new int[] { 1, 2, 3, 4, 5 };
            string s = "Вывод элементов массива!";
            Console.WriteLine(s);
            foreach (var v in col)            
                Console.WriteLine(v);
            
            Console.WriteLine(new string('-', s.Length));

            foreach (var v in col)
                Console.WriteLine(v);
        }

        static void SortCollection()
        {
            Action<string, List<CustomSort>> display = (x, y) =>
            {
                Console.WriteLine(x);
                foreach (var i in y)
                    Console.WriteLine(i.A);
                Console.WriteLine();
            };

            List<CustomSort> list = new List<CustomSort>() { new CustomSort() { A = 2 }, new CustomSort() { A = 1 } };


            //IComparable<T>
            list.Sort();
            display("Sort by IComparable<T>", list);
            
            //IComparer<T>
            list.Sort(new CustomSort());
            display("Sort by IComparer<T>", list);

            // Comparioson<>
            list.Sort((x, y) => x.A - y.A);
            display("Sort by Comparison<T>", list);

        }

    }
}
