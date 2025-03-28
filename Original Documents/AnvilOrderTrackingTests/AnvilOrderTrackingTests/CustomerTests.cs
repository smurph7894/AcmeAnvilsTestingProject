using AnvilsOrderTaking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnvilOrderTrackingTests
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void Sets_CustomerInfo_WhenSet()
        {
            // arrange
            Customer customer = new Customer();
            string name = "Toco";
            string street = "123 elm st";
            string city = "Bellevue";
            string state = "WA";
            string zip = "98005";

            // act
            customer.SetInfo(name, street, city, state, zip);
            string customerDetails = customer.GetCustomerDetails();

            // assert
            string expected = "Toco\n123 elm st\nBellevue, WA 98005";
            Assert.AreEqual(expected, customerDetails);
        }

        [TestMethod]
        public void Get_CustomerDetails_FormatsOutputCorrectly()
        {
            // arrange
            Customer customer = new Customer();
            customer.SetInfo("Bob Doe", "123 Elm St", "Seattle", "NY", "12345");

            // act
            string result = customer.GetCustomerDetails();

            // assert
            Assert.IsTrue(result.Contains("Bob Doe"));
            Assert.IsTrue(result.Contains("123 Elm St"));
            Assert.IsTrue(result.Contains("Seattle, NY 12345"));
        }

        [TestMethod]
        public void Returns_FormattedString_WhenValuesAreEmpty()
        {
            // arrange
            Customer customer = new Customer();
            customer.SetInfo("", "", "", "", "");

            // act
            string result = customer.GetCustomerDetails();

            // assert
            string expected = "\n\n,  ";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Set_AccuratlyUpdatesExistingValues_WhenNewValuesAreIntroduced()
        {
            // arrange
            Customer customer = new Customer();
            customer.SetInfo("Initial Name", "Initial Street", "Initial City", "IC", "00000");

            // act
            customer.SetInfo("Updated Name", "Updated Street", "Updated City", "UC", "11111");
            string result = customer.GetCustomerDetails();

            // assert
            string expected = "Updated Name\nUpdated Street\nUpdated City, UC 11111";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReturnDetails_ContainingAllRequiredElements_WhenInfoIsSet()
        {
            // arrange
            Customer customer = new Customer();
            customer.SetInfo("Test Customer", "Test Address", "Test City", "TC", "12345");

            // act
            string details = customer.GetCustomerDetails();

            // assert
            Assert.IsTrue(details.Contains("Test Customer"));
            Assert.IsTrue(details.Contains("Test Address"));
            Assert.IsTrue(details.Contains("Test City"));
            Assert.IsTrue(details.Contains("TC"));
            Assert.IsTrue(details.Contains("12345"));
            Assert.IsTrue(details.Contains(", "));
            Assert.IsTrue(details.Contains("\n"));
        }
    }
}
