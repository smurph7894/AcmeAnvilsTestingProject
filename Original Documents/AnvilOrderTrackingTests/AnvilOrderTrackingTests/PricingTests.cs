﻿using AnvilsOrderTaking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnvilOrderTrackingTests
{

    [TestClass]
    public class PricingTests
    {
        private readonly Pricing _pricing = new Pricing();
        private const double Delta = 0.001;

        [DataTestMethod]
        [DataRow(-1, 0.0, DisplayName = "Negative quantity returns zero")]
        [DataRow(0, 0.0, DisplayName = "Zero quantity returns zero")]
        [DataRow(1, 88.50, DisplayName = "Small quantity (1) returns high price")]
        [DataRow(5, 88.50, DisplayName = "Small quantity (5) returns high price")]
        [DataRow(9, 88.50, DisplayName = "Small quantity upper bound (9) returns high price")]
        [DataRow(10, 70.00, DisplayName = "Medium quantity lower bound (10) returns medium price")]
        [DataRow(15, 70.00, DisplayName = "Medium quantity (15) returns medium price")]
        [DataRow(19, 70.00, DisplayName = "Medium quantity upper bound (19) returns medium price")]
        [DataRow(20, 68.25, DisplayName = "Large quantity lower bound (20) returns low price")]
        [DataRow(50, 68.25, DisplayName = "Large quantity (50) returns low price")]
        [DataRow(100, 68.25, DisplayName = "Very large quantity (100) returns low price")]
        public void Return_PricePerAnvil_WithCorrectPrice(int quantity, double expectedPrice)
        {
            // act
            double actualPrice = _pricing.GetPricePerAnvil(quantity);

            // assert
            Assert.AreEqual(expectedPrice, actualPrice, Delta);
        }
    }
}
