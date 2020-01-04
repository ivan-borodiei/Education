using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Others
{
    interface ICovariance<out T>
    {
        T GetMethod();
    }

    interface IContravariance<in T>
    {
        void Method(T param);
    }

    class BaseClass { }

    class DerivedClass : BaseClass { }



    
    class Covariance
    {                
        public void Method()
        {            
            ICovariance<DerivedClass> deriveList1 = null;
            ICovariance<BaseClass> baseList1 = deriveList1;            

            IContravariance<BaseClass> baseList2 = null;
            IContravariance<DerivedClass> deriveList2 = baseList2;


            Func<object, FileStream> f2 = null;
            Func<string, Stream> f1 = null;            
            f1 += Ment1;
            f1 += Ment2;

            f1 = f2;            
        }

        private Stream Ment1(string o)
        {
            Stream fs = new FileStream("", FileMode.Append);
            return fs;
        }

        private FileStream Ment2(object o)
        {
            FileStream fs = new FileStream("", FileMode.Append); 
            return fs;
        }
    }
}
