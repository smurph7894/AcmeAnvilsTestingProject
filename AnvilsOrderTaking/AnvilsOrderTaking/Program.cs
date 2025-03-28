/*
 * Oscar Moreno
 * Individual Project
 * 03/04/2025
 * DEV324 - Software Testing
 */

using static System.Console;

namespace AnvilsOrderTaking
{
    public class Program
    {
        private static void Main(string[] args)
        {
            const string Company = "Acme Anvils Corporation";
            const string Slogan = "Supporting the animation industry for 50 years and counting!";
            string name, customerName, address, city, state, zip;
            int numOfAnvils;

            Clerk clerk = new Clerk();
            Customer customer = new Customer();
            Order order;
            Pricing pricing = new Pricing();

            WriteLine($"*** {Company} ***\n{Slogan}\n");

            // Clerk portal
            Write("Employee Portal\nEnter your name: ");
            name = ReadLine();
            clerk.SetName(name);

            // Customer Portal
            WriteLine("\nCustomer Portal");
            Write($"\nHi, I'm {clerk.GetName()}. How many anvils would you like to order today?\n" +
                $"Number of anvils: ");

            if (int.TryParse(ReadLine(), out numOfAnvils) && numOfAnvils > 0)
            {
                WriteLine($"You entered: {numOfAnvils}");
            }
            else
            {
                WriteLine("Invalid input.");
                return;
            }

            /* Determine eligibility for the Loyalty Club Loyalty discount.
            * IMPORTANT NOTE: If the customer is not a Loyalty Club member, do not show discount line on invoice.
            */

            Write("Are you a member of our Futility Club Loyalty program('Y' if yes)?");
            bool loyaltyClub = ReadLine().ToUpper() == "Y";
            if (loyaltyClub)
            {
                Write("\nYou'll get that AMAZING 15% loyalty bonus today!\n");
            }

            WriteLine("\nI can certainly help you with that. Could you please give me your name and address?");
            Write("First and last name: \t");
            customerName = ReadLine();
            Write("Street address: \t");
            address = ReadLine();
            Write("City: \t\t\t");
            city = ReadLine();
            Write("State (2-letters): \t");
            state = ReadLine().ToUpper();
            Write("Zip code: \t\t");
            zip = ReadLine();

            customer.SetInfo(customerName, address, city, state, zip, loyaltyClub);
            order = new Order(numOfAnvils, pricing.GetPricePerAnvil(numOfAnvils), customer);

            WriteLine("\nPress any Key to display invoice. . .");
            Console.ReadKey();

            // invoice
            Invoice invoice = new Invoice(order, customer);
            invoice.PrintInvoice();

            WriteLine($"\nYour total today is ${order.GetTotal().ToString("F2")}. Thanks for shopping with Acme!\n");
            WriteLine($"And don't forget: {Slogan}");

            WriteLine("\nPress any Key to end program...");
            ReadKey();
        }
    }

    public class Order
    {
        private int NumAnvils { get; set; }
        private double PricePerAnvil { get; set; }
        private double Subtotal { get; set; }
        private double Discount { get; set; }
        private double Tax { get; set; }
        private double Total { get; set; }
        private double totalAnvilWeight { get; set; }
        private double ShippingCost { get; set; }
        private double TaxableAmount { get; set; }
        private Customer customer { get; set; }
        private const double TaxRate = 0.095;

        public Order(int numAnvils, double pricePerAnvil, Customer _customer)
        {
            customer = _customer;
            NumAnvils = numAnvils;
            PricePerAnvil = pricePerAnvil;
            CalcTotalWeightOfAnvils();
            CalculateSubtotal();
            ApplyLoyaltyDiscount();
            CalcTaxableAmount();
            ApplyTax();
            CalculateShipping();
            CalcTotal();
        }

        private void CalculateSubtotal() => Subtotal = NumAnvils * PricePerAnvil;

        private void ApplyLoyaltyDiscount()
        {
            if (customer.GetLoyaltyStatus())
                Discount = Subtotal * 0.15;
        }

        private void CalcTaxableAmount() => TaxableAmount = Subtotal - Discount;

        private void ApplyTax()
        {
            if (customer.GetLoyaltyStatus())
                Tax = TaxableAmount * TaxRate;
            else
            {
                Tax = Subtotal * TaxRate;
            }
        }

        private void CalcTotalWeightOfAnvils()
        {
            // calculate total weight of anvils
            // anvil is 50 pounds
            // 50 * NumAnvils
            totalAnvilWeight = NumAnvils * 50;
        }

        private void CalculateShipping()
        {
            // calcaulte shipping cost by weight
            // $5 per pound - anvil is 50 pounds
            // CA and OR who order 5+ anvils discount is 10%
            if ((customer.GetState() == "CA" && NumAnvils > 5 || customer.GetState() == "OR" && NumAnvils > 5))
            {
                ShippingCost = (totalAnvilWeight * 5.00) * .90;
            }
            else
                ShippingCost = totalAnvilWeight * 5.00;
        }

        private void CalcTotal()
        {
            if (customer.GetLoyaltyStatus())
                Total = TaxableAmount + Tax + ShippingCost;
            else
            {
                Total = Subtotal + Tax + ShippingCost;
            }
        }

        public double GetSubtotal() => Subtotal;

        public double GetTax() => Tax;

        public double GetShippingCost() => ShippingCost;

        public int GetNumAnvils() => NumAnvils;

        public double GetPricePerAnvil() => PricePerAnvil;
        public double GetLoyaltyDiscount() => Discount;

        public double GetTotal() => Total;
        public double GetTaxableAmount() => TaxableAmount;
    }

    public class Pricing
    {
        public double GetPricePerAnvil(int quantity)
        {
            //10% discount for 10 or more anvils, **instructions do not say that 88.50 is the base price for all anvils so continuing with original prices with additional 10% discount for 10 or more anvils**
            if (quantity <= 0) return 0.0;
            if (quantity >= 20) return (68.25 * .90);
            if (quantity >= 10) return (70.00 * .90);
            return 88.50;
        }
    }

    public class Customer
    {
        //loyalty club - 15% on all purchases
        /*Determine eligibility for the Loyalty Club Loyalty discount.
            - Ask the customer whether they are a member of the Loyalty Club.
            - If they answer “Y” or “y”, give them a 15% pre-tax discount on the order.- 
        */

        private string Name { get; set; }
        private string StreetAddress { get; set; }
        private string City { get; set; }
        private string State { get; set; }
        private string ZipCode { get; set; }
        private bool IsLoyaltyMember { get; set; } = false;

        public void SetInfo(string name, string street, string city, string state, string zip, bool loyaltyClub)
        {
            Name = name;
            StreetAddress = street;
            City = city;
            State = state;
            ZipCode = zip;
            IsLoyaltyMember = loyaltyClub;
        }

        public string GetCustomerDetails()
        {
            return $"\t{Name}\n\t{StreetAddress}\n\t{City}\n\t{State}\n\t{ZipCode}";
        }

        public bool GetLoyaltyStatus() => IsLoyaltyMember;
        public string GetState() => State;
    }

    public class Clerk
    {
        public string Name { get; set; } = string.Empty;

        public void SetName(string n) => Name = n;

        public string GetName() => Name;
    }

    public class Invoice
    {
        private Order OrderDetails { get; set; }
        private Customer CustomerDetails { get; set; }

        public Invoice(Order order, Customer customer)
        {
            OrderDetails = order;
            CustomerDetails = customer;
        }

        public void PrintInvoice()
        {
            //customer loyalty only visible if they are a member
            WriteLine("\n*******************************");
            WriteLine("*** ACME Anvils Corporation ***");
            WriteLine("Customer Invoice\n");
            WriteLine("SHIP TO:");
            WriteLine(CustomerDetails.GetCustomerDetails());
            WriteLine($"\nQuantity ordered:           {OrderDetails.GetNumAnvils()}");
            WriteLine($"Cost per anvil:            {OrderDetails.GetPricePerAnvil():C}");
            WriteLine($"Subtotal:                  {OrderDetails.GetSubtotal():C}");
            if (CustomerDetails.GetLoyaltyStatus())
            {
                WriteLine($"Less 15% Loyalty Club:     ({OrderDetails.GetLoyaltyDiscount():C})");
                WriteLine($"Taxable amount:            {OrderDetails.GetTaxableAmount():C}");
            }
            WriteLine($"Sales Tax:                 {OrderDetails.GetTax():C}");
            WriteLine($"Shipping:                  {OrderDetails.GetShippingCost():C}");
            WriteLine("___________________________________");
            WriteLine($"TOTAL:                     {OrderDetails.GetTotal():C}");
            WriteLine("*************************************");
        }
    }
}