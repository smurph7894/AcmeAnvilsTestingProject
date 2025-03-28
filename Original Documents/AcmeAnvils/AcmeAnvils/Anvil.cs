using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeAnvils
{
    public class Anvil
    {
        //Fields
        public int quantity { get; set; }

        //Constructor
        public Anvil(){ }

        public string GetDisplayPricePerAnvil()
        {
            return $"${this.CalcPricePerAnvil().ToString("0.00")}";
        }

        public string GetDisplaySubTotal()
        {
            return $"${this.CalcSubTotalPrice().ToString("0.00")}";
        }

        public string GetDisplaySalesTax()
        {
            return $"${this.CalcSalesTax().ToString("0.00")}";
        }

        public string GetDisplayTotal()
        {
            return $"${this.CalcTotalPrice().ToString("0.00")}";
        }

        public double CalcPricePerAnvil()
        {
            // Calculates the volume discount for some quantity of anvils at some regular price
            // Expected discount:
            // - 1 - 9 anvils:      88.5
            // - 10 - 19 anvils:    70.00
            // - 20 or more anvils: 68.25.
            //
            //  If either quantity is 0, unitPrice should return 0
            if (quantity <= 0 )
            {
                Console.WriteLine("Looks like you didn't want any anvils. The system will close now.");
                throw new ExitAppException();
            }

            double unitPrice = 0;

            if (quantity >= 20)
            {
                unitPrice=68.25;
            }
            else if (quantity >= 10 && quantity < 20)
            {
                unitPrice = 70.00;
            }
            else if (quantity > 0 && quantity < 10)
            {
                unitPrice=88.50;
            }
            return unitPrice;
        }

        public double CalcSubTotalPrice()
        {
            //take unitPrice calculated from CalcPricePerAnvil and multiply by quantity to get subTotal price
            double subTotalPrice = 0;
            subTotalPrice = CalcPricePerAnvil() * quantity;
            return subTotalPrice;
        }

        public double CalcSalesTax()
        {
            // Calculate sales tax at 9.5% of the subtotal price
            double salesTax = 0;
            salesTax = Math.Round((CalcSubTotalPrice() * 0.095), 2);
            return salesTax;
        }

        public double CalcTotalPrice()
        {
            // Calculate the total price by adding the subtotal price and the sales tax
            double totalPrice = 0;
            totalPrice = Math.Round((CalcSubTotalPrice() + CalcSalesTax()), 2);
            return totalPrice;
        }
    }
}
