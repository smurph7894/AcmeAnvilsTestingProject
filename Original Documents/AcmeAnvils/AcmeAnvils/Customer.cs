using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeAnvils
{
    public class Customer
    {
        //Fields
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }

        //Constructor
        public Customer() { }

        public string GetFullName()
        {
            return $"{this.firstName} {this.lastName}";
        }

        public static void SeparateNames(string fullName, out string fName, out string lName)
        {
            fName = string.Empty;
            lName = string.Empty;

            // Split the input by spaces.
            string[] nameParts = fullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            fName = "";
            lName = "";

            if (nameParts.Length == 1)
            {
                // Only one name entered.
                fName = nameParts[0];
            }
            else if (nameParts.Length == 2)
            {
                // Exactly two names: assign first and last.
                fName = nameParts[0];
                lName = nameParts[1];
            }
            else if (nameParts.Length >= 3)
            {
                fName = string.Concat(nameParts[0], " ", nameParts[1]);
                lName = string.Join(" ", nameParts, 2, nameParts.Length - 2);
            }
        }

        public static string CheckStateLength(string state)
        {
            // Check if the state is a valid two-letter abbreviation.
            if (state == null || state.Length != 2)
            {
                Console.WriteLine("Invalid state input");
                throw new ExitAppException();
            }
            return state.ToUpper();
        }

        public static string CheckZipCodeLength(string zipCode)
        {
            // Check if the zip code is a valid five-digit number
            if (zipCode == null || zipCode.Length != 5 || !Int32.TryParse(zipCode, out int zipResult))
            {
                Console.WriteLine("Invalid zip code input");
                throw new ExitAppException();
            }
            return zipCode;
        }
    }
}
