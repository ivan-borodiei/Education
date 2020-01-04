using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    abstract class Party
    {
        public void ArrangeParty()
        {
            OrderPlace();
            BuyDrinks();
            InviteFriands();
        }

        private void OrderPlace()
        {
            Console.WriteLine("Place has been ordered!");
        }

        protected abstract void BuyDrinks();
        protected abstract void InviteFriands();
    }

    class BirthdayParty: Party
    {
        protected override void BuyDrinks()
        {
            Console.WriteLine("Drinks has been bought!");
        }

        protected override void InviteFriands()
        {
            Console.WriteLine("Friends has been invited!");
        }
        
    }
}
