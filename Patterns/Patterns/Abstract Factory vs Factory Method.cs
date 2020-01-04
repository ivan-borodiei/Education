using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    abstract class Pizza
    {        
        public string Name { get; protected set; }
        public int Weight { get; protected set; }
    }

    class HunterPizza : Pizza
    {
        public HunterPizza()
        {
            Name = "Hunter";
            Weight = 1000;
        }
    }

    class PapperonyPizza : Pizza
    {
        public PapperonyPizza()
        {
            Name = "Papperony";
            Weight = 1500;
        }
    }


    abstract class PizzaFactory
    {
        public abstract Pizza CreateHunterPizza();
        public abstract Pizza CreatePapperonyPizza();
    }

    class UkrainePizzaFactory: PizzaFactory
    {
        public override Pizza CreateHunterPizza()
        {
            return new HunterPizza();
        }

        public override Pizza CreatePapperonyPizza()
        {
            return new PapperonyPizza();
        }
    }


    abstract class PizzaStore
    {
        protected PizzaFactory factory;
        protected Pizza pizza { get; set; }

        public PizzaStore(PizzaFactory factory)
        {
            this.factory = factory;
        }

        //with abstract factory
        public Pizza OrderPizza()
        {
            pizza = factory.CreateHunterPizza();
            return pizza;
        }

        public abstract Pizza CreatePizza(string type);

        //with factory method
        public Pizza OrderPizza(string type)
        {
            pizza = CreatePizza(type);
            return pizza;
        }
    }

    class UkrainianPizzaStore : PizzaStore
    {
        public UkrainianPizzaStore(PizzaFactory factory):base(factory)
        {                
        }

        public override Pizza CreatePizza(string type)
        {
            switch(type)
            {
                case "Hunter": return new HunterPizza();
                case "Papperony": return new PapperonyPizza();
                default: throw new Exception("There is no type!");
            }            
        }

    }
}
