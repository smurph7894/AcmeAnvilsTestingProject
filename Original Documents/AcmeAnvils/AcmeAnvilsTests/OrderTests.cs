using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcmeAnvils;
using SUT = AcmeAnvils.Order;


namespace AcmeAnvilsTests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        [Timeout(1000)]
        [DataRow("CA", 1)]
        [DataRow("CA", 2)]
        [DataRow("CA", 3)]
        [DataRow("CA", 4)]
        [DataRow("OR", 1)]
        [DataRow("OR", 2)]
        [DataRow("OR", 3)]
        [DataRow("OR", 4)]
        public void CalcShippingCostTest_StateIsORorCA_QTYis1Thorugh4_ShouldReturnFreeShippingString(string state, int quantity)
        {
            //Arrange
            Customer customer = new Customer();
            Anvil anvil = new Anvil();
            SUT order = new SUT(customer, anvil);
            anvil.quantity = quantity;
            customer.state = state;
            Tuple<double?, string> expected = Tuple.Create((double?)null,"Free Shipping!");

            //Act
            Tuple <double?, string> result = order.CalcShippingCost();

            //Assert
            Assert.AreEqual(expected.Item2, result.Item2);
        }

        [TestMethod]
        [Timeout(1000)]
        [DataRow("CA", 5, 560)]
        [DataRow("CA", 6, 672)]
        [DataRow("CA", 40, 4480)]
        [DataRow("OR", 5, 560)]
        [DataRow("OR", 6, 672)]
        [DataRow("OR", 40, 4480)]
        public void CalcShippingCostTest_StateIsORorCA_QTYisMoreThan4_ShouldReturnCost(string state, int quantity, double expectedShipping)
        {
            //Arrange
            AcmeAnvils.Customer customer = new AcmeAnvils.Customer();
            AcmeAnvils.Anvil anvil = new AcmeAnvils.Anvil();
            SUT order = new SUT(customer, anvil);
            anvil.quantity = quantity;
            customer.state = state;;
            Tuple<double?, string> expected = Tuple.Create((double?)expectedShipping,"");

            //Act
            Tuple<double?, string> result = order.CalcShippingCost();

            //Assert
            Assert.AreEqual(expected.Item1, result.Item1);
        }

        [TestMethod]
        [Timeout(1000)]
        [DataRow("WA", 1, 112)]
        [DataRow("NY", 2, 224)]
        [DataRow("DE", 6, 672)]
        public void CalcShippingCostTest_StateIsNotORorCA_ShouldReturnCost(string state, int quantity, double expectedShipping)
        {
            //Arrange
            AcmeAnvils.Customer customer = new AcmeAnvils.Customer();
            AcmeAnvils.Anvil anvil = new AcmeAnvils.Anvil();
            SUT order = new SUT(customer, anvil);
            anvil.quantity = quantity;
            customer.state = state; ;
            Tuple<double?, string> expected = Tuple.Create((double?)expectedShipping, "");

            //Act
            Tuple<double?, string> result = order.CalcShippingCost();

            //Assert
            Assert.AreEqual(expected.Item1, result.Item1);
        }

        [TestMethod]
        [Timeout(1000)]
        [DataRow("O", 0)]
        [DataRow("C", 6)]
        [DataRow("CA", 0)]
        [DataRow("OR", -1)]
        [DataRow("ORR", 0)]
        [DataRow("WAA", 5)]
        public void CalcShippingCostTest_InvalidState_ReturnException(string state, int quantity)
        {
            //Arrange
            AcmeAnvils.Customer customer = new AcmeAnvils.Customer();
            AcmeAnvils.Anvil anvil = new AcmeAnvils.Anvil();
            SUT order = new SUT(customer, anvil);
            anvil.quantity = quantity;
            customer.state = state; ;

            //Act & Assert
            Assert.ThrowsException<ExitAppException>(() => order.CalcShippingCost());
        }
    }
}