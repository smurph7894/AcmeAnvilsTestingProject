using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SUT = AcmeAnvils.Anvil;
using AcmeAnvils;

namespace AcmeAnvilsTests
{
    [TestClass]
    public class AnvilTests
    {
        [TestMethod]
        [DataRow(1, "$88.50")]
        [DataRow(2, "$88.50")]
        [DataRow(5, "$88.50")]
        [DataRow(8, "$88.50")]
        [DataRow(9, "$88.50")]
        [DataRow(10, "$70.00")]
        [DataRow(11, "$70.00")]
        [DataRow(15, "$70.00")]
        [DataRow(18, "$70.00")]
        [DataRow(19, "$70.00")]
        [DataRow(20, "$68.25")]
        [DataRow(21, "$68.25")]
        [DataRow(40, "$68.25")]
        [DataRow(100, "$68.25")]
        public void DisplayPricePerAnvilTest_ValidInput_ShouldReturn2DecimalString(int quantity, string expectedPrice)
        {
            //Arrange
            SUT anvil = new SUT();
            anvil.quantity = quantity;
            //Act
            string displayPrice = anvil.GetDisplayPricePerAnvil();

            //Assert
            Assert.AreEqual(expectedPrice, displayPrice);
        }

        [TestMethod]
        [DataRow(1, "$88.50")]
        [DataRow(2, "$177.00")]
        [DataRow(5, "$442.50")]
        [DataRow(8, "$708.00")]
        [DataRow(9, "$796.50")]
        [DataRow(10, "$700.00")]
        [DataRow(11, "$770.00")]
        [DataRow(15, "$1050.00")]
        [DataRow(18, "$1260.00")]
        [DataRow(19, "$1330.00")]
        [DataRow(20, "$1365.00")]
        [DataRow(21, "$1433.25")]
        [DataRow(40, "$2730.00")]
        public void DisplaySubTotalTest_ValidInput_ShouldReturn2DecimalString(int quantity, string expectedPrice)
        {
            //Arrange
            SUT anvil = new SUT();
            anvil.quantity = quantity;
            //Act
            string displayPrice = anvil.GetDisplaySubTotal();

            //Assert
            Assert.AreEqual(expectedPrice, displayPrice);
        }

        [TestMethod]
        [Timeout(1000)]
        [DataRow(1, "$8.41")]
        [DataRow(2, "$16.82")]
        [DataRow(5, "$42.04")]
        [DataRow(8, "$67.26")]
        [DataRow(9, "$75.67")]
        [DataRow(10, "$66.50")]
        [DataRow(11, "$73.15")]
        [DataRow(15, "$99.75")]
        [DataRow(18, "$119.70")]
        [DataRow(19, "$126.35")]
        [DataRow(20, "$129.68")]
        [DataRow(21, "$136.16")]
        [DataRow(40, "$259.35")]
        public void DisplaySalesTaxTest_ValidInput_ShouldReturn2DecimalString(int quantity, string expectedPrice)
        {
            //Arrange
            SUT anvil = new SUT();
            anvil.quantity = quantity;
            //Act
            string displayPrice = anvil.GetDisplaySalesTax();

            //Assert
            Assert.AreEqual(expectedPrice, displayPrice);
        }

        [TestMethod]
        [Timeout(1000)]
        [DataRow(1, "$96.91")]
        [DataRow(2, "$193.82")]
        [DataRow(5, "$484.54")]
        [DataRow(8, "$775.26")]
        [DataRow(9, "$872.17")]
        [DataRow(10, "$766.50")]
        [DataRow(11, "$843.15")]
        [DataRow(15, "$1149.75")]
        [DataRow(18, "$1379.70")]
        [DataRow(19, "$1456.35")]
        [DataRow(20, "$1494.68")]
        [DataRow(21, "$1569.41")]
        [DataRow(40, "$2989.35")]
        public void DisplayTotalTest_ValidInput_ShouldReturn2DecimalString(int quantity, string expectedPrice)
        {
            //Arrange
            SUT anvil = new SUT();
            anvil.quantity = quantity;
            //Act
            string displayPrice = anvil.GetDisplayTotal();

            //Assert
            Assert.AreEqual(expectedPrice, displayPrice);
        }

        //Failure point for this class is in quantity, if quantity is 0 or negative
        [TestMethod]
        [Timeout(1000)]
        [DataRow(1, 88.50)]
        [DataRow(2, 88.50)]
        [DataRow(5, 88.50)]
        [DataRow(8, 88.50)]
        [DataRow(9, 88.50)]
        [DataRow(10, 70.00)]
        [DataRow(11, 70.00)]
        [DataRow(15, 70.00)]
        [DataRow(18, 70.00)]
        [DataRow(19, 70.00)]
        [DataRow(20, 68.25)]
        [DataRow(21, 68.25)]
        [DataRow(40, 68.25)]
        public void CalcPricePerAnvilTest_ValidInput_ShouldReturnExpectedPrice(int quantity, double expectedPrice)
        {
            //Arrange
            SUT anvil = new SUT();
            anvil.quantity = quantity;

            //Act
            double calPrice = anvil.CalcPricePerAnvil();

            //Assert
            Assert.AreEqual(expectedPrice, calPrice);
        }

        [TestMethod]
        [Timeout(1000)]
        [DataRow(0)]
        [DataRow(-1)]
        public void CalcPricePerAnvilTest_InValidInputNull_ShouldThrowException(int quantity)
        {
            // Arrange
            SUT anvil = new SUT();
            anvil.quantity = quantity;

            // Act & Assert
            Assert.ThrowsException<ExitAppException>(() => anvil.CalcPricePerAnvil());
        }

        [TestMethod]
        [Timeout(1000)]
        [DataRow(1, 88.50)]
        [DataRow(2, 177.00)]
        [DataRow(5, 442.5)]
        [DataRow(8, 708.00)]
        [DataRow(9, 796.50)]
        [DataRow(10, 700.00)]
        [DataRow(11, 770.00)]
        [DataRow(15, 1050.00)]
        [DataRow(18, 1260.00)]
        [DataRow(19, 1330.00)]
        [DataRow(20, 1365.00)]
        [DataRow(21, 1433.25)]
        [DataRow(40, 2730.00)]
        [DataRow(100, 6825.00)]
        [DataRow(1000, 68250.00)]
        [DataRow(10000, 682500.00)]
        public void CalcSubTotalTest_ValidInput_ShouldBeExpectedValue(int quantity, double expectedSubTotal)
        {
            //Arrange
            SUT anvil = new SUT();
            anvil.quantity = quantity;

            //Act
            double calSubTotal = anvil.CalcSubTotalPrice();

            //Assert
            Assert.AreEqual(expectedSubTotal, calSubTotal);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void CalcSubTotalTest_InvalidQty_ShouldThrowException(int quantity)
        {
            //Arrange
            SUT anvil = new SUT();
            anvil.quantity = quantity;
            //Act & Assert
            Assert.ThrowsException<ExitAppException>(() => anvil.CalcSubTotalPrice());
        }

        [TestMethod]
        [Timeout(1000)]
        [DataRow(1, 8.41)]
        [DataRow(2, 16.82)]
        [DataRow(5, 42.04)]
        [DataRow(8, 67.26)]
        [DataRow(9, 75.67)]
        [DataRow(10, 66.50)]
        [DataRow(11, 73.15)]
        [DataRow(15, 99.75)]
        [DataRow(18, 119.70)]
        [DataRow(19, 126.35)]
        [DataRow(20, 129.68)]
        [DataRow(21, 136.16)]
        [DataRow(40, 259.35)]
        [DataRow(100, 648.38)]
        [DataRow(1000, 6483.75)]
        [DataRow(10000, 64837.50)]
        public void CalcSalesTaxTest_ValidInput_ShouldBeExpectedValue(int quantity, double expectedSalesTax)
        {
            //Arrange
            SUT anvil = new SUT();
            anvil.quantity = quantity;

            //Act
            double salesTax = anvil.CalcSalesTax();

            //Assert
            Assert.AreEqual(expectedSalesTax, salesTax);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void CalcSalesTax_InvalidQty_ShouldThrowException(int quantity)
        {
            //Arrange
            SUT anvil = new SUT();
            anvil.quantity = quantity;
            //Act & Assert
            Assert.ThrowsException<ExitAppException>(() => anvil.CalcSalesTax());
        }

        [TestMethod]
        [Timeout(1000)]
        [DataRow(1, 96.91)]
        [DataRow(2, 193.82)]
        [DataRow(5, 484.54)]
        [DataRow(8, 775.26)]
        [DataRow(9, 872.17)]
        [DataRow(10, 766.50)]
        [DataRow(11, 843.15)]
        [DataRow(15, 1149.75)]
        [DataRow(18, 1379.70)]
        [DataRow(19, 1456.35)]
        [DataRow(20, 1494.68)]
        [DataRow(21, 1569.41)]
        [DataRow(40, 2989.35)]
        [DataRow(100, 7473.38)]
        [DataRow(1000, 74733.75)]
        [DataRow(10000, 747337.50)]
        public void CalcTotalPricePreShippingTest_ValidInput_ShouldBeExpectedValue(int quantity, double expectedTotal)
        {
            //Arrange
            SUT anvil = new SUT();
            anvil.quantity = quantity;

            //Act
            double calcTotal = anvil.CalcTotalPrice();

            //Assert
            Assert.AreEqual(expectedTotal, calcTotal);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void CalcTotalPrice_InvalidQty_ShouldThrowException(int quantity)
        {
            //Arrange
            SUT anvil = new SUT();
            anvil.quantity = quantity;
            //Act & Assert
            Assert.ThrowsException<ExitAppException>(() => anvil.CalcTotalPrice());
        }
    }
}