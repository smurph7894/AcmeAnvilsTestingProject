using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnvilsOrderTaking;

namespace AnvilOrderTrackingTests
{
    [TestClass]
    public class OrderTests
    {
        private const double Delta = 0.001;

        [TestMethod]
        [DataRow(-5, 88.50)]
        [DataRow(-1, 88.50)]
        [DataRow(0, 88.50)]
        public void Returns_CorrectCalculation_NoLoyaltyIncorrectValue(int quantity, double pricePerAnvil)
        {
            // arrange

            //$5 per pound - anvil is 50 pounds
            double OneAnvilShippingCost = 5.00 * 50;
            Customer customer = new Customer();
            customer.SetInfo("Toco", "123 elm st", "Bellevue", "NY", "98005", false);

            // act
            Order order = new Order(quantity, pricePerAnvil, customer);

            // assert
            double expectedSubtotal = pricePerAnvil * quantity;
            double expectedTax = expectedSubtotal * 0.095;
            double expectedShipping = OneAnvilShippingCost * quantity;
            double expectedTotal = expectedSubtotal + expectedTax + expectedShipping;

            Assert.AreEqual(expectedSubtotal, order.GetSubtotal(), Delta);
            Assert.AreEqual(expectedTax, order.GetTax(), Delta);
            Assert.AreEqual(expectedShipping, order.GetShippingCost(), Delta);
            Assert.AreEqual(expectedTotal, order.GetTotal(), Delta);
        }

        [TestMethod]
        [DataRow(1, 88.50)]
        [DataRow(2, 88.50)]
        [DataRow(5, 88.50)]
        [DataRow(9, 88.50)]
        public void Returns_CorrectCalculation_NoLoyaltySmallOrderQuantity(int quantity, double pricePerAnvil)
        {
            // arrange

            //$5 per pound - anvil is 50 pounds
            double OneAnvilShippingCost = 5.00 * 50;
            Customer customer = new Customer();
            customer.SetInfo("Toco", "123 elm st", "Bellevue", "NY", "98005", false);

            // act
            Order order = new Order(quantity, pricePerAnvil, customer);

            // assert
            double expectedSubtotal = pricePerAnvil * quantity;
            double expectedTax = expectedSubtotal * 0.095;
            double expectedShipping = OneAnvilShippingCost * quantity;
            double expectedTotal = expectedSubtotal + expectedTax + expectedShipping;

            Assert.AreEqual(expectedSubtotal, order.GetSubtotal(), Delta);
            Assert.AreEqual(expectedTax, order.GetTax(), Delta);
            Assert.AreEqual(expectedShipping, order.GetShippingCost(), Delta);
            Assert.AreEqual(expectedTotal, order.GetTotal(), Delta);
        }

        [TestMethod]
        [DataRow(1, 88.50)]
        [DataRow(2, 88.50)]
        [DataRow(5, 88.50)]
        [DataRow(9, 88.50)]
        public void Returns_CorrectCalculation_YesLoyaltySmallOrderQuantity(int quantity, double pricePerAnvil)
        {
            // arrange

            //$5 per pound - anvil is 50 pounds
            double OneAnvilShippingCost = 5.00 * 50;

            Customer customer = new Customer();
            customer.SetInfo("Toco", "123 elm st", "Bellevue", "NY", "98005", true);

            // act
            Order order = new Order(quantity, pricePerAnvil, customer);

            // assert
            double expectedSubtotal = pricePerAnvil * quantity;
            double expectedDiscount = expectedSubtotal * 0.15;
            double expectedTaxableAmount = expectedSubtotal - expectedDiscount;
            double expectedTax = expectedTaxableAmount * 0.095;
            double expectedShipping = OneAnvilShippingCost * quantity;
            double expectedTotal = expectedTaxableAmount + expectedTax + expectedShipping;

            Assert.AreEqual(expectedSubtotal, order.GetSubtotal(), Delta);
            Assert.AreEqual(expectedDiscount, order.GetLoyaltyDiscount(), Delta);
            Assert.AreEqual(expectedTaxableAmount, order.GetTaxableAmount(), Delta);
            Assert.AreEqual(expectedTax, order.GetTax(), Delta);
            Assert.AreEqual(expectedShipping, order.GetShippingCost(), Delta);
            Assert.AreEqual(expectedTotal, order.GetTotal(), Delta);
        }

        [TestMethod]
        [DataRow(10, 63.00)]
        [DataRow(11, 63.00)]
        [DataRow(15, 63.00)]
        [DataRow(18, 63.00)]
        [DataRow(19, 63.00)]
        public void Returns_CorrectCalculation_NoLoyaltyMidOrderQuantity(int quantity, double pricePerAnvil)
        {
            // arrange
            //$5 per pound - anvil is 50 pounds
            double OneAnvilShippingCost = 5.00 * 50;
            Customer customer = new Customer();
            customer.SetInfo("Toco", "123 elm st", "Bellevue", "NY", "98005", false);

            // act
            Order order = new Order(quantity, pricePerAnvil, customer);

            // assert
            double expectedSubtotal = pricePerAnvil * quantity;
            double expectedTax = expectedSubtotal * 0.095;
            double expectedShipping = OneAnvilShippingCost * quantity;
            double expectedTotal = expectedSubtotal + expectedTax + expectedShipping;

            Assert.AreEqual(expectedSubtotal, order.GetSubtotal(), Delta);
            Assert.AreEqual(expectedTax, order.GetTax(), Delta);
            Assert.AreEqual(expectedShipping, order.GetShippingCost(), Delta);
            Assert.AreEqual(expectedTotal, order.GetTotal(), Delta);
        }

        [TestMethod]
        [DataRow(10, 63.00)]
        [DataRow(11, 63.00)]
        [DataRow(15, 63.00)]
        [DataRow(18, 63.00)]
        [DataRow(19, 63.00)]
        public void Returns_CorrectCalculation_YesLoyaltyMidOrderQuantity(int quantity, double pricePerAnvil)
        {
            // arrange
            //$5 per pound - anvil is 50 pounds
            double OneAnvilShippingCost = 5.00 * 50;

            Customer customer = new Customer();
            customer.SetInfo("Toco", "123 elm st", "Bellevue", "NY", "98005", true);

            // act
            Order order = new Order(quantity, pricePerAnvil, customer);

            // assert
            double expectedSubtotal = pricePerAnvil * quantity;
            double expectedDiscount = expectedSubtotal * 0.15;
            double expectedTaxableAmount = expectedSubtotal - expectedDiscount;
            double expectedTax = expectedTaxableAmount * 0.095;
            double expectedShipping = OneAnvilShippingCost * quantity;
            double expectedTotal = expectedTaxableAmount + expectedTax + expectedShipping;

            Assert.AreEqual(expectedSubtotal, order.GetSubtotal(), Delta);
            Assert.AreEqual(expectedDiscount, order.GetLoyaltyDiscount(), Delta);
            Assert.AreEqual(expectedTaxableAmount, order.GetTaxableAmount(), Delta);
            Assert.AreEqual(expectedTax, order.GetTax(), Delta);
            Assert.AreEqual(expectedShipping, order.GetShippingCost(), Delta);
            Assert.AreEqual(expectedTotal, order.GetTotal(), Delta);
        }


        [TestMethod]
        [DataRow(20, 61.425)]
        [DataRow(21, 61.425)]
        [DataRow(30, 61.425)]
        public void Returns_CorrectCalculation_NoLoyaltyBigOrderQuantity(int quantity, double pricePerAnvil)
        {
            // arrange

            //$5 per pound - anvil is 50 pounds
            double OneAnvilShippingCost = 5.00 * 50;
            Customer customer = new Customer();
            customer.SetInfo("Toco", "123 elm st", "Bellevue", "NY", "98005", false);

            // act
            Order order = new Order(quantity, pricePerAnvil, customer);

            // assert
            double expectedSubtotal = pricePerAnvil * quantity;
            double expectedTax = expectedSubtotal * 0.095;
            double expectedShipping = OneAnvilShippingCost * quantity;
            double expectedTotal = expectedSubtotal + expectedTax + expectedShipping;

            Assert.AreEqual(expectedSubtotal, order.GetSubtotal(), Delta);
            Assert.AreEqual(expectedTax, order.GetTax(), Delta);
            Assert.AreEqual(expectedShipping, order.GetShippingCost(), Delta);
            Assert.AreEqual(expectedTotal, order.GetTotal(), Delta);
        }

        [TestMethod]
        [DataRow(20, 61.425)]
        [DataRow(21, 61.425)]
        [DataRow(30, 61.425)]
        public void Returns_CorrectCalculation_YesLoyaltyBigOrderQuantity(int quantity, double pricePerAnvil)
        {
            // arrange

            //$5 per pound - anvil is 50 pounds
            double OneAnvilShippingCost = 5.00 * 50;

            Customer customer = new Customer();
            customer.SetInfo("Toco", "123 elm st", "Bellevue", "NY", "98005", true);

            // act
            Order order = new Order(quantity, pricePerAnvil, customer);

            // assert
            double expectedSubtotal = pricePerAnvil * quantity;
            double expectedDiscount = expectedSubtotal * 0.15;
            double expectedTaxableAmount = expectedSubtotal - expectedDiscount;
            double expectedTax = expectedTaxableAmount * 0.095;
            double expectedShipping = OneAnvilShippingCost * quantity;
            double expectedTotal = expectedTaxableAmount + expectedTax + expectedShipping;

            Assert.AreEqual(expectedSubtotal, order.GetSubtotal(), Delta);
            Assert.AreEqual(expectedDiscount, order.GetLoyaltyDiscount(), Delta);
            Assert.AreEqual(expectedTaxableAmount, order.GetTaxableAmount(), Delta);
            Assert.AreEqual(expectedTax, order.GetTax(), Delta);
            Assert.AreEqual(expectedShipping, order.GetShippingCost(), Delta);
            Assert.AreEqual(expectedTotal, order.GetTotal(), Delta);
        }

        //the other tests inherently test the shipping cost of not CA or OR
        [TestMethod]
        [DataRow(1, 88.50, "CA", DisplayName = "CA, <6 anvils, no Shipping Discount")]
        [DataRow(2, 88.50, "CA", DisplayName = "CA, <6 anvils, no Shipping Discount")]
        [DataRow(5, 88.50, "CA", DisplayName = "CA, <6 anvils, no Shipping Discount.")]
        [DataRow(6, 88.50, "CA", DisplayName = "CA, 6+ anvils - Shipping Discount")]
        [DataRow(9, 88.50, "CA", DisplayName = "CA, 6+ anvils - Shipping Discount.")]
        [DataRow(10, 63.00, "CA", DisplayName = "CA, 10+ anvils - Shipping and qty Discount.")]
        [DataRow(11, 63.00, "CA", DisplayName = "CA, 10+ anvils - Shipping and qty Discount.")]
        [DataRow(15, 63.00, "CA", DisplayName = "CA, 10+ anvils - Shipping and qty Discount.")]
        [DataRow(18, 63.00, "CA", DisplayName = "CA, 10+ anvils - Shipping and qty Discount.")]
        [DataRow(19, 63.00, "CA", DisplayName = "CA, 10+ anvils - Shipping and qty Discount.")]
        [DataRow(20, 61.425, "CA", DisplayName = "CA, 10+ anvils - Shipping and qty Discount.")]
        [DataRow(21, 61.425, "CA", DisplayName = "CA, 10+ anvils - Shipping and qty Discount.")]
        [DataRow(30, 61.425, "CA", DisplayName = "CA, 10+ anvils - Shipping and qty Discount.")]
        [DataRow(1, 88.50, "OR", DisplayName = "OR, <6 anvils, no Shipping Discount.")]
        [DataRow(2, 88.50, "OR", DisplayName = "OR, <6 anvils, no Shipping Discount.")]
        [DataRow(5, 88.50, "OR", DisplayName = "OR, <6 anvils, no Shipping Discount.")]
        [DataRow(6, 88.50, "OR", DisplayName = "OR, 6+ anvils - Shipping Discount.")]
        [DataRow(9, 88.50, "OR", DisplayName = "OR, 6+ anvils - Shipping Discount.")]
        [DataRow(10, 63.00, "OR", DisplayName = "OR, 10+ anvils - Shipping and qty Discount.")]
        [DataRow(11, 63.00, "OR", DisplayName = "OR, 10+ anvils - Shipping and qty Discount.")]
        [DataRow(15, 63.00, "OR", DisplayName = "OR, 10+ anvils - Shipping and qty Discount.")]
        [DataRow(18, 63.00, "OR", DisplayName = "OR, 10+ anvils - Shipping and qty Discount.")]
        [DataRow(19, 63.00, "OR", DisplayName = "OR, 10+ anvils - Shipping and qty Discount.")]
        [DataRow(20, 61.425, "OR", DisplayName = "OR, 10+ anvils - Shipping and qty Discount.")]
        [DataRow(21, 61.425, "OR", DisplayName = "OR, 10+ anvils - Shipping and qty Discount.")]
        [DataRow(30, 61.425, "OR", DisplayName = "OR, 10+ anvils - Shipping and qty Discount.")]
        public void Returns_CorrectCalculation_NoLoyaltyDiscountShippingCA_OR_6orMoreAnvils(int quantity, double pricePerAnvil, string state)
        {
            // Arrange
            Customer customer = new Customer();
            customer.SetInfo("Toco", "123 elm st", "Bellevue", state, "98005", false);

            //$5 per pound - anvil is 50 pounds
            double OneAnvilShippingCost = (5.00 * 50);
            if (quantity >= 6)
            {
                // with CA/OR discount
                OneAnvilShippingCost = (5.00 * 50) * .9;
            }

            // act
            Order order = new Order(quantity, pricePerAnvil, customer);

            // assert
            double expectedSubtotal = pricePerAnvil * quantity;
            double expectedTax = expectedSubtotal * 0.095;
            double expectedShipping = OneAnvilShippingCost * quantity;
            double expectedTotal = expectedSubtotal + expectedTax + expectedShipping;

            Assert.AreEqual(expectedSubtotal, order.GetSubtotal(), Delta);
            Assert.AreEqual(expectedTax, order.GetTax(), Delta);
            Assert.AreEqual(expectedShipping, order.GetShippingCost(), Delta);
            Assert.AreEqual(expectedTotal, order.GetTotal(), Delta);
        }

        [TestMethod]
        [DataRow(1, 88.50, "CA", DisplayName = "CA, isLoyaltyMember, <6 anvils, no Shipping Discount but yes loyalty Discount.")]
        [DataRow(2, 88.50, "CA", DisplayName = "CA, isLoyaltyMember, <6 anvils, no Shipping Discount but yes loyalty Discount.")]
        [DataRow(5, 88.50, "CA", DisplayName = "CA, isLoyaltyMember, <6 anvils, no Shipping Discount but yes loyalty Discount.")]
        [DataRow(6, 88.50, "CA", DisplayName = "CA, isLoyaltyMember, 6+ anvils - Shipping and loyalty Discount.")] 
        [DataRow(9, 88.50, "CA", DisplayName = "CA, isLoyaltyMember, 6+ anvils - Shipping and loyalty Discount.")]
        [DataRow(10, 63.00, "CA", DisplayName = "CA, isLoyaltyMember, 10+ anvils - Shipping, loyalty, and qty Discount.")]
        [DataRow(11, 63.00, "CA", DisplayName = "CA, isLoyaltyMember, 10+ anvils - Shipping, loyalty, and qty Discount.")]
        [DataRow(15, 63.00, "CA", DisplayName = "CA, isLoyaltyMember, 10+ anvils - Shipping, loyalty, and qty Discount.")]
        [DataRow(18, 63.00, "CA", DisplayName = "CA, isLoyaltyMember, 10+ anvils - Shipping, loyalty, and qty Discount.")]
        [DataRow(19, 63.00, "CA", DisplayName = "CA, isLoyaltyMember, 10+ anvils - Shipping, loyalty, and qty Discount.")]
        [DataRow(20, 61.425, "CA", DisplayName = "CA, isLoyaltyMember, 10+ anvils - Shipping, loyalty, and qty Discount.")]
        [DataRow(21, 61.425, "CA", DisplayName = "CA, isLoyaltyMember, 10+ anvils - Shipping, loyalty, and qty Discount.")]
        [DataRow(30, 61.425, "CA", DisplayName = "CA, isLoyaltyMember, 10+ anvils - Shipping, loyalty, and qty Discount.")]
        [DataRow(1, 88.50, "OR", DisplayName = "OR, isLoyaltyMember, <6 anvils, no Shipping Discount but yes loyalty Discount.")]
        [DataRow(2, 88.50, "OR", DisplayName = "OR, isLoyaltyMember, <6 anvils, no Shipping Discount but yes loyalty Discount.")]
        [DataRow(5, 88.50, "OR", DisplayName = "OR, isLoyaltyMember, <6 anvils, no Shipping Discount but yes loyalty Discount.")]
        [DataRow(6, 88.50, "OR", DisplayName = "OR, isLoyaltyMember, 6+ anvils - Shipping and loyalty Discount.")]
        [DataRow(9, 88.50, "OR", DisplayName = "OR, isLoyaltyMember, 6+ anvils - Shipping and loyalty Discount.")]
        [DataRow(10, 63.00, "OR", DisplayName = "OR, isLoyaltyMember, 10+ anvils - Shipping, loyalty, and qty Discount.")]
        [DataRow(11, 63.00, "OR", DisplayName = "OR, isLoyaltyMember, 10+ anvils - Shipping, loyalty, and qty Discount.")]
        [DataRow(15, 63.00, "OR", DisplayName = "OR, isLoyaltyMember, 10+ anvils - Shipping, loyalty, and qty Discount.")]
        [DataRow(18, 63.00, "OR", DisplayName = "OR, isLoyaltyMember, 10+ anvils - Shipping, loyalty, and qty Discount.")]
        [DataRow(19, 63.00, "OR", DisplayName = "OR, isLoyaltyMember, 10+ anvils - Shipping, loyalty, and qty Discount.")]
        [DataRow(20, 61.425, "OR", DisplayName = "OR, isLoyaltyMember, 10+ anvils - Shipping, loyalty, and qty Discount.")]
        [DataRow(21, 61.425, "OR", DisplayName = "OR, isLoyaltyMember, 10+ anvils - Shipping, loyalty, and qty Discount.")]
        [DataRow(30, 61.425, "OR", DisplayName = "OR, isLoyaltyMember, 10+ anvils - Shipping, loyalty, and qty Discount.")]
        public void Returns_CorrectCalculation_YesLoyaltyDiscountShippingCA_OR_6orMoreAnvils(int quantity, double pricePerAnvil, string state)
        {
            // Arrange
            Customer customer = new Customer();
            customer.SetInfo("Toco", "123 elm st", "Bellevue", state, "98005", true);

            //$5 per pound - anvil is 50 pounds
            double OneAnvilShippingCost = (5.00 * 50);

            if (quantity >= 6)
            {
                // with CA/OR discount
                OneAnvilShippingCost = (5.00 * 50) * .9;
            }

            // act
            Order order = new Order(quantity, pricePerAnvil, customer);

            // assert
            double expectedSubtotal = pricePerAnvil * quantity;
            double expectedDiscount = expectedSubtotal * 0.15;
            double expectedTaxableAmount = expectedSubtotal - expectedDiscount;
            double expectedTax = expectedTaxableAmount * 0.095;
            double expectedShipping = OneAnvilShippingCost * quantity;
            double expectedTotal = expectedTaxableAmount + expectedTax + expectedShipping;

            Assert.AreEqual(expectedSubtotal, order.GetSubtotal(), Delta);
            Assert.AreEqual(expectedDiscount, order.GetLoyaltyDiscount(), Delta);
            Assert.AreEqual(expectedTaxableAmount, order.GetTaxableAmount(), Delta);
            Assert.AreEqual(expectedTax, order.GetTax(), Delta);
            Assert.AreEqual(expectedShipping, order.GetShippingCost(), Delta);
            Assert.AreEqual(expectedTotal, order.GetTotal(), Delta);
        }
    }
}
