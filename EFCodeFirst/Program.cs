using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EFContext())
            {
                //Policy p = context.Policies.Add(new Policy() { PolicyNo = "AB23732", AppNo = "ФС43252" });
                //Person per = context.Persons.Add(new Person() { Name = "Borodey", Policy = p });                

                //Policy pol = context.Policies.Include("Persons").Where(x => x.Id == 1).Single();
                //pol.AppNo = "SFU2343";
                //Person per = context.Persons.Add(new Person() { Name = "Antonuk", Policy = pol });                
                //per.Phones.Add(new PersonPhone(){ Person = per, Phone = new Phone(){ PhoneNumber = "0984589865"}});
                //pol.Persons.Add(per);

                Person per = context.Persons.Include("Phones").AsNoTracking().Single(x => x.Id == 2);                
                per.Phones[0].PhoneType = PhoneType.Mobile;
                context.SaveChanges();
            }
        }
    }
}
