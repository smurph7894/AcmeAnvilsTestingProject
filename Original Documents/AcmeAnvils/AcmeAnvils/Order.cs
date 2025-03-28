using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeAnvils
{
    public class Order
    {
        //Fields
        Customer customer { get; set; }
        Anvil anvil { get; set; }

        //Constructor
        public Order(Customer _customer, Anvil _anvil)
        {
            customer = _customer;
            anvil = _anvil;
        }

        public Tuple<double?, string> CalcShippingCost()
        {
            // Calculates the shipping cost for some quantity of anvils
            //Shipping is:
            //    - free for CA and OR for 1-4 anvils, return "Free Shipping!" message
            //    - $112 per an anvil otherwise

            string state = customer.state;
            int quantity = anvil.quantity;

            if (state.Length != 2 || quantity <= 0)
            {
                Console.WriteLine("Invalid state input");
                throw new ExitAppException();
            }

            //Tuple allows us to return two values from a method, string if shipping is free, double if not.
            if (state == "CA" || state == "OR")
            {
                if (quantity >= 1 && quantity <= 4)
                {
                    return Tuple.Create<double?, string>(null, "Free Shipping!");
                }
                else
                {
                    return Tuple.Create<double?, string>(112 * quantity, null);
                }
            }
            else
            {
                return Tuple.Create<double?, string>(112 * quantity, null);
            }
        }
    }
}
