using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Remoting;
using System.Reflection;

namespace Domains
{
    class Program
    {
        static void Main(string[] args)
        {            
            //CreateClassInNewDomain();
            Marshalling();
                     
            Console.ReadLine();
        }

        private static void CreateClassInNewDomain()
        {
            Program.GetDomainInfo(AppDomain.CurrentDomain);

            try
            {
                AppDomain newDomain = AppDomain.CreateDomain("NewDomain");
                ObjectHandle obj = newDomain.CreateInstance(Assembly.GetExecutingAssembly().FullName, typeof(MarshalByRef).FullName);
                MarshalByRef cl = (MarshalByRef)obj.Unwrap();
                Console.WriteLine("Is Proxy - {0}", RemotingServices.IsTransparentProxy(cl));
                //cl.GetDomainInfo();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception - " + e.Message);
            }
        }

        private static void Marshalling()
        {
            Program.GetDomainInfo(AppDomain.CurrentDomain);
            Console.WriteLine();

            AppDomain domain = AppDomain.CreateDomain("Marshalling");            

            Assembly asm = Assembly.GetExecutingAssembly();
            Console.WriteLine("MarshalByRef");
            MarshalByRef clByRef = (MarshalByRef)domain.CreateInstanceAndUnwrap(asm.FullName, "Domains.MarshalByRef");
            Console.WriteLine("Is Proxy - {0}", RemotingServices.IsTransparentProxy(clByRef));            
            Console.WriteLine();

            Console.WriteLine("MarshalByVal");
            MarshalByVal clByVal = (MarshalByVal)domain.CreateInstanceAndUnwrap(asm.FullName, "Domains.MarshalByVal");// clByRef.GetClassByVal();
            Console.WriteLine("Is Proxy - {0}", RemotingServices.IsTransparentProxy(clByVal));
            clByVal.Print();
            Console.WriteLine();

            Console.WriteLine("NoMarshal:");
            try
            {
                NoMarshal noMarshal = clByRef.NoMarshal();
                Console.WriteLine("Is Proxy - {0}", RemotingServices.IsTransparentProxy(noMarshal));
                Console.WriteLine();
            }
            catch (System.Runtime.Serialization.SerializationException e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
        }

        public static void GetDomainInfo(AppDomain appDomain, string info = null)
        {
            if (info != null)
                Console.WriteLine(info);
            Console.WriteLine("Domain name - {0}, DomainID - {1}", appDomain.FriendlyName, appDomain.Id);
        }
    }

    
    class MarshalByRef: MarshalByRefObject
    {
        public MarshalByRef()
        {
            Program.GetDomainInfo(AppDomain.CurrentDomain);
        }

        public void GetDomainInfo()
        {
            Program.GetDomainInfo(AppDomain.CurrentDomain, "MyClass");
            //throw new Exception();
        }

        public MarshalByVal GetClassByVal()
        {            
            return new MarshalByVal();
        }

        public NoMarshal NoMarshal()
        {            
            return new NoMarshal(); //.NoMarshal();
        }

    }


    
    [Serializable]
    class MarshalByVal
    {
        public MarshalByVal()
        {
            Program.GetDomainInfo(AppDomain.CurrentDomain);
        }

        public void Print()
        {
            Program.GetDomainInfo(AppDomain.CurrentDomain);
        }

        public NoMarshal GetNoMarshal(NoMarshal n)
        {
            return new NoMarshal();
        }

    }

    //убрать атрибут - будет без продвижения (будет исключение)
    //[Serializable]
    class NoMarshal
    {        
        public NoMarshal()
        {            
            Program.GetDomainInfo(AppDomain.CurrentDomain);
        }

        public void NoMarshalling()
        {            
            Program.GetDomainInfo(AppDomain.CurrentDomain, "NoMarshal:");
        }

    }

}
