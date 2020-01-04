using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    abstract class HouseBuilder
    {
        protected House house = new House();

        public abstract void CreateCellar();
        public abstract void CreateFloors();
        public abstract void CreateRoof();

        public abstract House GetHouse();
    }

    class TreeHouseBuilder: HouseBuilder
    {

        public override void CreateCellar()
        {
            Console.WriteLine("{0} was buit!", "Cellar");
        }

        public override void CreateFloors()
        {
            Console.WriteLine("{0} were buit!", "Floors");
        }

        public override void CreateRoof()
        {
            Console.WriteLine("{0} was buit!", "Roof");
        }

        public override House GetHouse()
        {
            return house;
        }
    }

    class House
    {
    }

    class Director
    {
        HouseBuilder builder;
        public Director(HouseBuilder builder)
        {
            this.builder = builder;
        }

        public void ConstructProduct()
        {
            builder.CreateCellar();
            builder.CreateFloors();
            builder.CreateRoof();
        }

        public House GetHouse()
        {
            return builder.GetHouse();
        }
    }


}
