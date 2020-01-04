using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.Runtime.Remoting;

namespace Assemblies
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Version number - {0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);

            LoadCloseAssembly();
            LoadGacAssembly();
            Console.ReadLine();
        }

        static void LoadCloseAssembly()
        { 
            AppDomain.CurrentDomain.AssemblyLoad += domain_AssemblyLoad;
            Assembly asm = AppDomain.CurrentDomain.Load("Domains");            
        }

        //сборка д.б зарегена в GAC, иначе будет использоваться тег CodeBase
        static void LoadGacAssembly()
        {
            string asmName = "Patterns, Version=1.0.0.0, Culture=neutral, PublicKeyToken=06930f93091076a7";

            Assembly assembly = Assembly.Load(asmName);
            object[] Args = new object[] { new string[] { } };
            
            assembly.EntryPoint.Invoke(null, Args);            
        }


        static void domain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            AppDomain domain = sender as AppDomain;
            Console.WriteLine("Event: Assembly {0} Loaded", args.LoadedAssembly.FullName);                        
        }

    }
}
