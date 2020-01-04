using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirst
{
    public class Policy
    {
        public int Id { get; set; }
        public string PolicyNo { get; set; }
        public string AppNo { get; set; }

        public IList<Person> Persons { get; set; }

        public Policy()
        {
            Persons = new List<Person>();
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Policy Policy { get; set; }

        public IList<PersonPhone> Phones { get; set; }

        public Person()
        {
            Phones = new List<PersonPhone>();
        }
    }

    public class Phone
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
    }

    public enum PhoneType
    {
        Mobile = 1,
        Home = 2,
        Work = 3
    }

    public class PersonPhone
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        public Phone Phone { get; set; }
        public PhoneType? PhoneType { get; set; }
    }

}
