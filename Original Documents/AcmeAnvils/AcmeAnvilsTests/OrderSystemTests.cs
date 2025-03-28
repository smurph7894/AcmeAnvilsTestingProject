using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcmeAnvils;
using SUT = AcmeAnvils.OrderSystem;
using Microsoft.Testing.Platform.MSBuild;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace AcmeAnvilsTests
{
    [TestClass]
    public class OrderSystemTests
    {
        [TestMethod]
        [Timeout(3000)]
        [DataRow("$88.50", "$88.50", "$112.00", "ClerkA", "1", "John A.", "Doe", "123 Main St", "Springfield", "IL", "62704")]
        [DataRow("$88.50", "$442.50", "$560.00", "ClerkB", "5", "Jane Jane", "Smith", "456 Oak Ave", "Columbus", "OH", "43215")]
        [DataRow("$88.50", "$796.50", "$1008.00", "ClerkC", "9", "Alice", "Johnson", "789 Pine Rd", "Austin", "TX", "73301")]
        [DataRow("$70.00", "$700.00", "$1120.00", "ClerkC", "10", "Alice", "Johnson", "789 Pine Rd", "Austin", "TX", "73301")]
        [DataRow("$70.00", "$770.00", "$1232.00", "ClerkC", "11", "Alice", "Johnson", "789 Pine Rd", "Austin", "TX", "73301")]
        [DataRow("$70.00", "$1050.00", "$1680.00", "ClerkD", "15", "Robert John", "Davis Brown", "101 Maple Blvd", "Denver", "CO", "80202")]
        [DataRow("$70.00", "$1330.00", "2128.00", "ClerkD", "19", "Robert John", "Davis Brown", "101 Maple Blvd", "Denver", "CO", "80202")]
        [DataRow("$68.25", "$1365.00", "$2240.00", "ClerkD", "20", "Robert John", "Davis Brown", "101 Maple Blvd", "Denver", "CO", "80202")]
        [DataRow("$68.25", "$1433.25", "$2352.00", "ClerkF", "21", "Michael", "Miller", "303 Birch Dr", "Miami", "FL", "33101")]
        [DataRow("$68.25", "$2730.00", "$4480.00", "ClerkF", "40", "Michael", "Miller", "303 Birch Dr", "Miami", "FL", "33101")]
        [DataRow("$88.50", "$88.50", "Free Shipping", "ClerkG", "1", "Emily AA", "Davis", "202 Cedar Ln", "San Francisco", "CA", "98101")]
        [DataRow("$88.50", "$354.00", "Free Shipping", "ClerkH", "4", "Emily AA", "Davis", "202 Cedar Ln", "San Francisco", "CA", "98101")]
        [DataRow("$88.50", "$88.50", "Free Shipping", "ClerkJ", "1", "Emily AA", "Davis", "202 Cedar Ln", "San Francisco", "CA", "98101")]
        [DataRow("$88.50", "$354.00", "Free Shipping", "ClerkK", "4", "Michael", "Miller", "303 Birch Dr", "Portland", "OR", "33101")]
        [DataRow("$88.50", "$796.50", "$1008.00", "ClerkI", "9", "Emily AA", "Davis", "202 Cedar Ln", "San Francisco", "CA", "98101")]
        [DataRow("$88.50", "$796.50", "$1008.00", "ClerkK", "9", "Michael", "Miller", "303 Birch Dr", "Portland", "OR", "33101")]
        public void GetInvoice_ValidInputs_ShouldReturnInvoiceStringWithExpectedValues(string costPerAnvil, string subTotal, string shipping, string clerk, string quantity, string firstName, string lastName, string street, string city, string state, string zipcode)
        {
            //Arrange
            SUT orderSystem = new SUT();
            Customer customer = new Customer();
            Anvil anvil = new Anvil();
            Order order = new Order(customer, anvil);

            orderSystem.clerkName = clerk;

            anvil.quantity = int.Parse(quantity);
            customer.firstName = firstName;
            customer.lastName = lastName;
            customer.streetAddress = street;
            customer.city = city;
            customer.state = state;
            customer.zipCode = zipcode;

            anvil.quantity = int.Parse(quantity);

            List<string> expected = new List<string> {costPerAnvil, subTotal, shipping, quantity, firstName, lastName, street, city, state, zipcode };

            //Act
            string output = orderSystem.GetInvoice(customer, anvil, order);
            List<string> invoice = new List<string> { };
            if (output.Contains(costPerAnvil))
            {
                invoice.Add(costPerAnvil);
            }
            if (output.Contains(subTotal))
            {
                invoice.Add(subTotal);
            }
            if (output.Contains(shipping))
            {
                invoice.Add(shipping);
            }
            if (output.Contains(quantity))
            {
                invoice.Add(quantity);
            }
            if (output.Contains(firstName))
            {
                invoice.Add(firstName);
            }
            if (output.Contains(lastName))
            {
                invoice.Add(lastName);
            }
            if (output.Contains(street))
            {
                invoice.Add(street);
            }
            if (output.Contains(city))
            {
                invoice.Add(city);
            }
            if (output.Contains(state))
            {
                invoice.Add(state);
            }
            if (output.Contains(zipcode))
            {
                invoice.Add(zipcode);
            }

            //Assert
            CollectionAssert.AreEqual(expected, invoice);
        }

        [TestMethod]
        [DataRow("$88.50", "$88.50", "$112.00", "ClerkA", "1", "John A.", "Doe", "123 Main St", "Springfield", "ILL", "98055")]
        [DataRow("$88.50", "$88.50", "$112.00", "ClerkA", "1", "John A.", "Doe", "123 Main St", "Springfield", "I", "98055")]
        [DataRow("$88.50", "$88.50", "$112.00", "ClerkA", "1", "John A.", "Doe", "123 Main St", "Springfield", "", "98055")]
        [DataRow("$88.50", "$88.50", "$112.00", "ClerkA", "0", "John A.", "Doe", "123 Main St", "Springfield", "IL", "98055")]
        [DataRow("$88.50", "$88.50", "$112.00", "ClerkA", "-1", "John A.", "Doe", "123 Main St", "Springfield", "I", "98055")]
        public void GetInvoice_InValidInput_ShouldReturnExitAppException(string costPerAnvil, string subTotal, string shipping, string clerk, string quantity, string firstName, string lastName, string street, string city, string state, string zipcode)
        {
            ///Arrange
            SUT orderSystem = new SUT();
            Customer customer = new Customer();
            Anvil anvil = new Anvil();
            Order order = new Order(customer, anvil);

            orderSystem.clerkName = clerk;

            anvil.quantity = int.Parse(quantity);
            customer.firstName = firstName;
            customer.lastName = lastName;
            customer.streetAddress = street;
            customer.city = city;
            customer.state = state;
            customer.zipCode = zipcode;

            anvil.quantity = int.Parse(quantity);
            //Act & Assert
            Assert.ThrowsException<ExitAppException>(() => orderSystem.GetInvoice(customer, anvil, order));
        }

        [TestMethod]
        [Timeout(3000)]
        [DataRow("$88.50", "$208.91", "ClerkA", "1", "John A.", "Doe", "123 Main St", "Springfield", "IL", "62704")]
        [DataRow("$88.50", "$96.91", "ClerkG", "1", "Emily AA", "Davis", "202 Cedar Ln", "San Francisco", "CA", "98101")]
        [DataRow("$88.50", "$1044.54", "ClerkB", "5", "Jane Jane", "Smith", "456 Oak Ave", "Columbus", "OH", "43215")]
        public void GetInvoice_ValidInputs_ShouldDisplayClosingMessage(string costPerAnvil, string total, string clerk, string quantity, string firstName, string lastName, string street, string city, string state, string zipcode)
        {
            //Arrange
            SUT orderSystem = new SUT();
            Customer customer = new Customer();
            Anvil anvil = new Anvil();
            Order order = new Order(customer, anvil);

            orderSystem.clerkName = clerk;

            anvil.quantity = int.Parse(quantity);
            customer.firstName = firstName;
            customer.lastName = lastName;
            customer.streetAddress = street;
            customer.city = city;
            customer.state = state;
            customer.zipCode = zipcode;

            string goodbye = "And don't forget: Acme anvils fly farther, drop faster, and land harder than any other anvil on the market!";

            anvil.quantity = int.Parse(quantity);
            string expected = "And don't forget: Acme anvils fly farther, drop faster, and land harder than any other anvil on the market!";

            //Act
            string output = orderSystem.GetInvoice(customer, anvil, order);
            string invoice = "";
            if (output.Contains(goodbye))
            {
                invoice = goodbye;
            }
            
            //Assert
            Assert.AreEqual(expected, invoice);
        }
    }
}