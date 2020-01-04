using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    interface IAdapter
    {
        void DoSomething();
    }

    class Adapter : IAdapter 
    {
        EuroPlug plug;
        public Adapter(EuroPlug plug)
        {
            this.plug = plug;
        }

        public void DoSomething()
        {            
            plug.MakeSomething();
        }
    }


    class EuroPlug
    {
        public void MakeSomething()
        {            
            Console.WriteLine("Convert EuroPlug to SimplePlug!");            
        }
    }


    class Device
    {
        IAdapter adapter;
        
        public Device(IAdapter adapter)
        {
            this.adapter = adapter;

        }

        public void UsePlug()
        {
            adapter.DoSomething();
        }
    }






    //Second Realizations
    internal class AdapterInstance
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    //AdapterHelper: адаптер, принимает Comparison<T>, возвращает IComparer<T>
    
    public class AdapterHelper
    {
        public static IComparer<T> GetComparer<T>(Comparison<T> value)
        {
            return new Adaptee<T>(value);
        }

        //адаприруемый обьект
        private class Adaptee<T> : IComparer<T>
        {
            Comparison<T> comparison;
            public Adaptee(Comparison<T> comparison)
            {
                this.comparison = comparison;
            }

            int IComparer<T>.Compare(T x, T y)
            {
                return comparison(x, y);
            }
        }
    }

}
