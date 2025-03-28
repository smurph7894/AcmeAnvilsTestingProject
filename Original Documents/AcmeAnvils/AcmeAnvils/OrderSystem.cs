using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeAnvils
{
    public class OrderSystem
    {
        //Fields
        public string clerkName { get; set; }

        //Constructor
        public OrderSystem() { }

        public void CollectOrder()
        {
            // Collects the order from the customer
            // This method should prompt the user for the following information:
            // - Clerks name
            // - Customer first name
            // - Customer last name
            // - Customer street address
            // - Customer city
            // - Customer state
            // - Customer zip code
            // - Quantity of anvils

            Customer newCustomer = new Customer();
            Anvil newAnvil = new Anvil();
            Order newOrder = new Order(newCustomer, newAnvil);
            Console.Write("Enter the clerk's name: ");
            clerkName = Console.ReadLine();
            Console.Clear();

            //Welcome message
            Console.WriteLine("***ACME Anvils Corporation***");
            Console.WriteLine("Supporting the animation industry for 50 years and counting!");

            Console.WriteLine("");

            //script and prompt user for # anvils
            Console.WriteLine($"Hi, I'm {clerkName}. How many anvils would you like to order today?");
            Console.Write("Number of anvils:");

            try
            {
                newAnvil.quantity = int.Parse(Console.ReadLine());
            }
            catch (FormatException exception)
            {
                throw new ExitAppException();
            }
            Console.WriteLine("");

            //prompt user for customer information
            Console.WriteLine("I can certainly help you with that. Could you please give me your name and address?");
            Console.Write($"{"First and Last Name:", -24}");
            string input = Console.ReadLine();

            //check if input is valid then separate first and last name to input into customer instance
            if (!string.IsNullOrEmpty(input))
            {
                Customer.SeparateNames(input, out string fName, out string lName);
                newCustomer.firstName = fName;
                newCustomer.lastName = lName;
            }
            else
            {
                Console.WriteLine("Invalid input");
                Environment.Exit(0);
            }

            Console.Write($"{"Street Address:",-24}");
            newCustomer.streetAddress = Console.ReadLine();

            Console.Write($"{"City:",-24}");
            newCustomer.city = Console.ReadLine();

            Console.Write($"{"State:",-24}");
            string state = Console.ReadLine();
            newCustomer.state = Customer.CheckStateLength(state);

            Console.Write($"{"Zip Code:", -24}");
            string zip = Console.ReadLine();
            newCustomer.zipCode = Customer.CheckZipCodeLength(zip);

            Console.WriteLine("");

            Console.WriteLine("Press any key to display invoice...");
            Console.ReadKey();
            Console.WriteLine("*******************************");
            Console.WriteLine("");
            Console.WriteLine("***ACME Anvils Corporation***");
            Console.WriteLine("Customer Invoice");

            Console.WriteLine("");

            string invoice = GetInvoice(newCustomer, newAnvil, newOrder);
            Console.WriteLine(invoice);
        }
        
        public string GetInvoice(Customer customer, Anvil anvil, Order order)
        {
            Tuple<double?, string> shipping = order.CalcShippingCost();

            string _shippingCost = ($"${shipping.Item1?.ToString("0.00")}");
            double totalPlusShipping = shipping.Item1 != null
                ? anvil.CalcTotalPrice() + (double)shipping.Item1
                : anvil.CalcTotalPrice();
            string shippingNotice = shipping.Item1 != null
                ? $"{"Shipping:",-20}{_shippingCost,10}"
                : $"\t{shipping.Item2}";

            return $@"SHIP TO:
    {customer.GetFullName()}
    {customer.streetAddress}
    {customer.city}
    {customer.state}
    {customer.zipCode}

{"Quantity ordered:",-20}{anvil.quantity,10}
{"Cost per anvil:",-20}{anvil.GetDisplayPricePerAnvil(),10}
{"Subtotal:",-20}{anvil.GetDisplaySubTotal(),10}
{"Sales Tax:",-20}{anvil.GetDisplaySalesTax(),10}
{new string('-', 20),30}
{"Total:",-20}{anvil.GetDisplayTotal(),10}
{shippingNotice}
*******************************

Your total today is ${totalPlusShipping.ToString("0.00")}. Thanks for shopping with Acme!

And don't forget: Acme anvils fly farther, drop faster, and land harder than any other anvil on the market!

Press any key to end program...";
        }
    }
}
