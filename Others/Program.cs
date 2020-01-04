using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.IO;

//[assembly: System.Runtime.CompilerServices.CompilationRelaxations(System.Runtime.CompilerServices.CompilationRelaxations.NoStringInterning)]

namespace Others
{
    
    /*public struct Nullable<T> where T:struct
    {
        internal T value;
        bool hasValue;

        public Nullable(T value)
        {
            this.hasValue = true;
            this.value = value;
        }

        public static implicit operator Nullable<T>(T value)
        {
            return new Nullable<T>(value);
        }

        public static implicit operator T (Nullable<T> value)
        {
            //return null;
            return value.value;
        }

        
        public override bool Equals(object other)
        { return false; }
        
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public T GetValueOrDefault()
        {
            return value;
        }

        public T GetValueOrDefault(T defaultValue)
        {
            return value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }*/


    class Program
    {        

        static async Task AsynkOperation()
        {
             await Task.Run(() => { Thread.Sleep(5000); });
        }

        static async void M1()
        {
            await AsynkOperation();
        }


        static async Task<string> AsynkOperation2()
        {
            return await Task.Run(() => { Thread.Sleep(5000); return "sad"; });            
        }

        static async Task<string> M2()
        {
            return await AsynkOperation2();            
        }

        static void Main(string[] args)
        {            
            //M1();
            //Task<string> t = M2();            
            //t.ContinueWith(x => Console.WriteLine(t.Result));

            //System.Nullable<int> t = null;

            //Others.Nullable<Int32> t = 5;
            //System.Nullable<Int32> v = 5;
            //int i = v;
            //int? i = v;
            
            //Covariance();
            Exceptions();                        
            //Factorial(5);
            //Expression();
            //CustomSort();
            //UseLinkedList();
            //Iterator();


            //MatrixLongestWay();
            Console.ReadLine();
        }

        static void Iterator()
        {
            Console.WriteLine("More than 1!");
            IEnumerable<int> ar = new int[] { 1, 2, 1, 1, 3 }.Where(x => x > 1).ToArray();
            foreach(var v in ar)
            {
                Console.Write(v + " ");
            }
        }

        static void Covariance(int i = 10)
        {            
            new Covariance().Method();
        }

        static void Exceptions()
        {
            MyExceptions.GenerateException();
        }

        
        static void Factorial(int a)
        {
            Func<int, int> factorial = null;
            factorial = x => x == 1 ? 1 : x * factorial(x - 1);
            Console.WriteLine(String.Format("{0}! = {1}", a, factorial(a)));
        }

        static void Expression()
        {
            //new MyExpression().CreateSqrExpression();
            new MyExpression().CreateWhereExpression();
        }


        static void CustomSort()
        {
            int[] a = new int[] { 1, 2, 2, 3 };
            int[] b = { 1, 2, 3, 4, 5 };
            
            int[] res = new int[a.Length + b.Length];

            int i = 0, j = 0, idx = 0;
            while (i < a.Length || j < b.Length)
            {
                if ((j >= b.Length) || (i < a.Length && a[i] <= b[j]))
                {
                    res[idx] = a[i];
                    i++;
                }
                else if (j < b.Length)
                {
                    res[idx] = b[j];
                    j++;
                }
                idx++;
            }

            res.All(x => { Console.WriteLine(x); return true; });            
        }


        static void UseLinkedList()
        {
            LinkedList list = new LinkedList();
            for (int i = 1; i <= 10; i++ )
                list.Add(new Node() { Data = i.ToString() });
            
            list.ShowData();
            list.Reverse();
            list.ShowData();            
        }


        static void MatrixLongestWay()
        {
            MatrixWay matrix = new MatrixWay();
            matrix.ShowMatrix();
            matrix.FillWays(1,1);
            matrix.ShowPathLists();
            matrix.ShowLongestPath();
        }

    }

}


