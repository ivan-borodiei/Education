using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    abstract class Beverage
    {
        public abstract string Name { get; }
        public abstract int Cost();
    }

    class Coffee : Beverage
    {                  
        public override string Name
        {
            get { return "Coffee"; }
        }

        public override int Cost()
        {            
            return 10;
        }
    }


    abstract class Decorator: Beverage
    {
        protected Beverage beverage;
        public Decorator(Beverage beverage)
        {
            this.beverage = beverage;
        }        
    }

    class Milk : Decorator
    {
        public Milk(Beverage beverage) : base(beverage) { }

        public override string Name
        {
            get { return beverage.Name + "+" + "Milk"; }
        }

        public override int Cost()
        {
            return beverage.Cost() + 2;
        }
    }

    class Cream : Decorator
    {
        public Cream(Beverage beverage)
            : base(beverage) { }

        public override string Name
        {
            get { return beverage.Name + "+" + "Cream"; }
        }

        public override int Cost()
        {
            return beverage.Cost() + 3;
        }
    }

}
