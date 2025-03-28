using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnvilsOrderTaking;

namespace AnvilOrderTrackingTests
{
    [TestClass]
    public class InvoiceTests
    {
        private Customer TestCustomerLoyaltyFalse ()
        {
            Customer customer = new Customer();
            customer.SetInfo("Test Customer", "123 Test St", "Test City", "TS", "12345", false);
            return customer;
        }

        private Order TestOrder(int numAnvils, double pricePerAnvil, Customer customer)
        {
            return new Order(numAnvils, pricePerAnvil, customer);
        }


        [TestMethod]
        public void ReturnsInvoice_NoLoyalty_ThatContainsRequiredElements()
        {
            // Arrange
            Customer customer = TestCustomerLoyaltyFalse();
            Order order = TestOrder(2, 88.50, customer);
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
            Assert.IsTrue(output.Contains("Test City"));
            Assert.IsTrue(output.Contains("TS"));
            Assert.IsTrue(output.Contains("12345"));
            Assert.IsTrue(output.Contains("Quantity ordered:"));
            Assert.IsTrue(output.Contains("Cost per anvil:"));
            Assert.IsTrue(output.Contains("Subtotal:"));
            Assert.IsTrue(output.Contains("Sales Tax:"));
            Assert.IsTrue(output.Contains("Shipping:"));
            Assert.IsTrue(output.Contains("TOTAL:"));
        }

        [TestMethod]
        [DataRow(1, 88.50, "Test Customer", "123 Test St", "Test City", "TS", "12345", true)]
        [DataRow(2, 88.50, "Test Customer", "123 Test St", "Test City", "WA", "12345", true)]
        [DataRow(5, 88.50, "Test Customer", "123 Test St", "Test City", "OR", "12345", true)]
        [DataRow(9, 88.50, "Test Customer", "123 Test St", "Test City", "CA", "12345", true)]
        [DataRow(10, 70.00, "Test Customer", "123 Test St", "Test City", "WA", "12345", true)]
        [DataRow(11, 70.00, "Test Customer", "123 Test St", "Test City", "TS", "12345", true)]
        [DataRow(15, 70.00, "Test Customer", "123 Test St", "Test City", "OR", "12345", true)]
        [DataRow(19, 70.00, "Test Customer", "123 Test St", "Test City", "CA", "12345", true)]
        [DataRow(20, 68.25, "Test Customer", "123 Test St", "Test City", "WA", "12345", true)]
        [DataRow(21, 68.25, "Test Customer", "123 Test St", "Test City", "OR", "12345", true)]
        [DataRow(30, 68.25, "Test Customer", "123 Test St", "Test City", "TS", "12345", true)]
        [DataRow(40, 68.25, "Test Customer", "123 Test St", "Test City", "CA", "12345", true)]
        public void ReturnsInvoice_LoyaltyClub_ThatContainsRequiredElements(int qty, double price, string name, string street, string city, string state, string zip, bool loyaltyClub)
        {
            // Arrange
            Customer customer = new Customer();
            customer.SetInfo(name, street, city, state, zip, loyaltyClub);

            Order order = TestOrder(qty, price, customer);
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
            Assert.IsTrue(output.Contains($"{name}"));
            Assert.IsTrue(output.Contains($"{street}"));
            Assert.IsTrue(output.Contains($"{city}"));
            Assert.IsTrue(output.Contains($"{state}"));
            Assert.IsTrue(output.Contains($"{zip}"));
            Assert.IsTrue(output.Contains("Quantity ordered:"));
            Assert.IsTrue(output.Contains("Cost per anvil:"));
            Assert.IsTrue(output.Contains("Subtotal:"));
            Assert.IsTrue(output.Contains("Less 15% Loyalty Club:"));
            Assert.IsTrue(output.Contains("Taxable amount:"));
            Assert.IsTrue(output.Contains("Sales Tax:"));
            Assert.IsTrue(output.Contains("Shipping:"));
            Assert.IsTrue(output.Contains("TOTAL:"));
        }
    }
}
