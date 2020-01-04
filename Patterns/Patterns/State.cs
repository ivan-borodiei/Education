using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    class Car
    {
        public string Mark { get; set; }
        public Transmission CurrentTransmission { get; set; }
        public Car()
        {
            CurrentTransmission = new NeutralTransmission();
        }

        public void NextTransmission()
        {
            CurrentTransmission = CurrentTransmission.Up();
        }

        public void PreviousTransmission()
        {
            CurrentTransmission = CurrentTransmission.Down();
        }

    }

    //Facade
    class AutomaticCar : Car
    {
        public void Drive()
        {
            CurrentTransmission = CurrentTransmission.Up();
            CurrentTransmission = CurrentTransmission.Up();
            CurrentTransmission = CurrentTransmission.Down();
            CurrentTransmission = CurrentTransmission.Down();
            CurrentTransmission = CurrentTransmission.Down();
            CurrentTransmission = CurrentTransmission.Down();
        }
    }

    abstract class Transmission
    {
        protected int number; 

        public Transmission(int number)
        {
            this.number = number;
            Console.WriteLine("Car is driving on {0} Transmission!", number);
        }

        public abstract Transmission Up();
        public abstract Transmission Down();
    }

    class NeutralTransmission : Transmission
    {
        public NeutralTransmission()
            : base(0)
        {
        }

        public override Transmission Up()
        {
            return new FirstTransmission();
        }

        public override Transmission Down()
        {
            Console.WriteLine("Car has been already stoped!");
            return this;
        }

    }

    class FirstTransmission : Transmission
    {
        //protected override int number { get { return 1; } }

        public FirstTransmission()
            : base(1)
        { }

        public override Transmission Up()
        {
            return new SecondTransmission();
        }

        public override Transmission Down()
        {
            return new NeutralTransmission();
        }

    }

    class SecondTransmission : Transmission
    {
        public SecondTransmission()
            : base(2) 
        { }

        public override Transmission Up()
        {
            throw new Exception("Car has been exploded!");
        }

        public override Transmission Down()
        {
            return new FirstTransmission();
        }

    }

}
