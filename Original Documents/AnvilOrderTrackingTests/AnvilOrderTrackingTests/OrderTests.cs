using AnvilsOrderTaking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnvilOrderTrackingTests
{
    [TestClass]
    public class OrderTests
    {
        private const double Delta = 0.001;
        [TestMethod]
        public void Returns_CorrectCalculation_SmallOrderQuantity1()
        {
            // arrange
            int quantity = 1;
            double pricePerAnvil = 88.50;
            string state = "NY";

            // act
            Order order = new Order(quantity, pricePerAnvil, state);

            // assert
            double expectedSubtotal = 88.50;
            double expectedTax = 88.50 * 0.095;
            double expectedShipping = 112.00;
            double expectedTotal = expectedSubtotal + expectedTax + expectedShipping;

            Assert.AreEqual(expectedSubtotal, order.GetSubtotal(), Delta);
            Assert.AreEqual(expectedTax, order.GetTax(), Delta);
            Assert.AreEqual(expectedShipping, order.GetShippingCost(), Delta);
            Assert.AreEqual(expectedTotal, order.GetTotal(), Delta);
        }

        [TestMethod]
        public void Returns_CorrectCalculation_MidOrderQuantity10()
        {
            // arrange
            int quantity = 10;
            double pricePerAnvil = 70.00;
            string state = "FL";

            // act
            Order order = new Order(quantity, pricePerAnvil, state);

            // assert
            double expectedSubtotal = 10 * 70.00;
            double expectedTax = expectedSubtotal * 0.095;
            double expectedShipping = 10 * 112.00;
            double expectedTotal = expectedSubtotal + expectedTax + expectedShipping;

            Assert.AreEqual(expectedSubtotal, order.GetSubtotal(), Delta);
            Assert.AreEqual(expectedTax, order.GetTax(), Delta);
            Assert.AreEqual(expectedShipping, order.GetShippingCost(), Delta);
            Assert.AreEqual(expectedTotal, order.GetTotal(), Delta);
        }
        [TestMethod]
        public void Returns_CorrectCalculation_BigOrderQuantity20()
        {
            // arrange
            int quantity = 20;
            double pricePerAnvil = 68.25;
            string state = "TX";

            // act
            Order order = new Order(quantity, pricePerAnvil, state);

            // assert
            double expectedSubtotal = 20 * 68.25;
            double expectedTax = expectedSubtotal * 0.095;
            double expectedShipping = 20 * 112.00;
            double expectedTotal = expectedSubtotal + expectedTax + expectedShipping;

            Assert.AreEqual(expectedSubtotal, order.GetSubtotal(), Delta);
            Assert.AreEqual(expectedTax, order.GetTax(), Delta);
            Assert.AreEqual(expectedShipping, order.GetShippingCost(), Delta);
            Assert.AreEqual(expectedTotal, order.GetTotal(), Delta);
        }

        [TestMethod]
        public void Returns_CorrectCalculation_FreeShippingCalifornia()
        {
            // Arrange
            int quantity = 3;
            double pricePerAnvil = 88.50;
            string state = "CA";

            // Act
            Order order = new Order(quantity, pricePerAnvil, state);

            // Assert
            double expectedSubtotal = 3 * 88.50;
            double expectedTax = expectedSubtotal * 0.095;
            double expectedShipping = 0.00;
            double expectedTotal = expectedSubtotal + expectedTax + expectedShipping;

            Assert.AreEqual(expectedSubtotal, order.GetSubtotal(), Delta);
            Assert.AreEqual(expectedTax, order.GetTax(), Delta);
            Assert.AreEqual(expectedShipping, order.GetShippingCost(), Delta);
            Assert.AreEqual(expectedTotal, order.GetTotal(), Delta);
        }

        [TestMethod]
        public void Returns_CorrectCalculation_FreeShippingOregon()
        {
            // arrange
            int quantity = 4;
            double pricePerAnvil = 88.50;
            string state = "OR";

            // act
            Order order = new Order(quantity, pricePerAnvil, state);

            // assert
            double expectedSubtotal = 4 * 88.50;
            double expectedTax = expectedSubtotal * 0.095;
            double expectedShipping = 0.00;
            double expectedTotal = expectedSubtotal + expectedTax + expectedShipping;

            Assert.AreEqual(expectedSubtotal, order.GetSubtotal(), Delta);
            Assert.AreEqual(expectedTax, order.GetTax(), Delta);
            Assert.AreEqual(expectedShipping, order.GetShippingCost(), Delta);
            Assert.AreEqual(expectedTotal, order.GetTotal(), Delta);
        }

        [TestMethod]
        public void Returns_CorrectCalculation_CaliforniaShippingQuantity5()
        {
            // arrange
            int quantity = 5;
            double pricePerAnvil = 88.50;
            string state = "CA";

            // act
            Order order = new Order(quantity, pricePerAnvil, state);

            // assert
            double expectedSubtotal = 5 * 88.50;
            double expectedTax = expectedSubtotal * 0.095;
            double expectedShipping = 5 * 112.00; // No free shipping for CA with >= 5 anvils
            double expectedTotal = expectedSubtotal + expectedTax + expectedShipping;

            Assert.AreEqual(expectedSubtotal, order.GetSubtotal(), Delta);
            Assert.AreEqual(expectedTax, order.GetTax(), Delta);
            Assert.AreEqual(expectedShipping, order.GetShippingCost(), Delta);
            Assert.AreEqual(expectedTotal, order.GetTotal(), Delta);
        }
    }
}
