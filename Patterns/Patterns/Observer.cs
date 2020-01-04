using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    interface IObserver
    {
        void Notify();
    }

    class Person : IObserver
    {
        private DriveSport driveSport;
        public Person(DriveSport driveSport)
        {
            this.driveSport = driveSport;
            driveSport.Subscribe(this);
        }

        public void Notify()
        {
            Console.WriteLine("You can take new number of magazine " + driveSport.Name);
        }
    }


    abstract class Magazine
    {
        public string Name { get; set; }

        private IList<Person> persons = new List<Person>();

        public void Subscribe(Person observer)
        {
            persons.Add(observer);
        }

        public void UnSubscribe(Person observer)
        {
            persons.Remove(observer);
        }

        public void Notify()
        {
            foreach (Person p in persons)
                p.Notify();
        }
    }

    class DriveSport: Magazine
    {
        public DriveSport()
        {
            Name = "DriveSport";
        }
    }

}

