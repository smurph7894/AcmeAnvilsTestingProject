using AnvilsOrderTaking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnvilOrderTrackingTests
{
    [TestClass]
    public class InvoiceTests
    {
        private Customer TestCustomer()
        {
            Customer customer = new Customer();
            customer.SetInfo("Test Customer", "123 Test St", "Test City", "TS", "12345");
            return customer;
        }
        private Order TestOrder(int numAnvils, double pricePerAnvil, string state)
        {
            return new Order(numAnvils, pricePerAnvil, state);
        }


        [TestMethod]
        public void Returns_Invoice_ThatContainsRequiredElements()
        {
            // Arrange
            Customer customer = TestCustomer();
            Order order = TestOrder(2, 88.50, "TS");
            Invoice invoice = new Invoice(order, customer);

            // Redirect console output to capture it
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            invoice.PrintInvoice();
            string output = stringWriter.ToString();

            // Assert
            Assert.IsTrue(output.Contains("ACME Anvils Corporation"));
            Assert.IsTrue(output.Contains("Customer Invoice"));
            Assert.IsTrue(output.Contains("SHIP TO:"));
            Assert.IsTrue(output.Contains("Test Customer"));
            Assert.IsTrue(output.Contains("123 Test St"));
            Assert.IsTrue(output.Contains("Test City, TS 12345"));
            Assert.IsTrue(output.Contains("Quantity ordered:"));
            Assert.IsTrue(output.Contains("Cost per anvil:"));
            Assert.IsTrue(output.Contains("Subtotal:"));
            Assert.IsTrue(output.Contains("Sales Tax:"));
            Assert.IsTrue(output.Contains("Shipping:"));
            Assert.IsTrue(output.Contains("TOTAL:"));
        }

    }
}
