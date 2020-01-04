using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using System.Diagnostics;
namespace Reflection
{
    class MyReflection
    {
        public int MyProperty { get; set; }
        private string myField;
        private void MyMethod() { myField = ""; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Assembly[] asmList = AppDomain.CurrentDomain.GetAssemblies();
            Assembly asm = Assembly.GetExecutingAssembly();
            //foreach (var asm in asmList)
            {
                Console.WriteLine("AssemblyName = {0}", asm.FullName);
                Type[] typeList = asm.GetTypes();
                foreach (Type t in typeList)
                {
                    Display(t.GetTypeInfo(), 5);
                    foreach (MemberInfo m in t.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
                    {
                        Display(m, 10);
                    }
                }
            }
            Console.ReadLine();
        }

        private static void Display(MemberInfo mi, int span)
        {
            Console.WriteLine("{0}MemberType = {1}, MemberName = {2}", new String(' ', span), mi.MemberType.ToString(), mi/*.Name*/);
        }
    }
}
