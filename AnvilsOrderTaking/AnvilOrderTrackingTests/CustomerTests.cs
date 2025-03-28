using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnvilsOrderTaking;

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
            bool loyaltyClub = false;

            // act
            customer.SetInfo(name, street, city, state, zip, loyaltyClub);
            string customerDetails = customer.GetCustomerDetails();

            // assert
            string expected = "\tToco\n\t123 elm st\n\tBellevue\n\tWA\n\t98005";
            Assert.AreEqual(expected, customerDetails);
        }

        [TestMethod]
        public void Get_CustomerDetails_FormatsOutputCorrectly()
        {
            // arrange
            Customer customer = new Customer();
            customer.SetInfo("Bob Doe", "123 Elm St", "Seattle", "NY", "12345", false);

            // act
            string result = customer.GetCustomerDetails();

            // assert
            Assert.IsTrue(result.Contains("Bob Doe"));
            Assert.IsTrue(result.Contains("123 Elm St"));
            Assert.IsTrue(result.Contains("Seattle"));
            Assert.IsTrue(result.Contains("NY"));
            Assert.IsTrue(result.Contains("12345"));
        }

        [TestMethod]
        public void Returns_FormattedString_WhenValuesAreEmpty()
        {
            // arrange
            Customer customer = new Customer();
            customer.SetInfo("", "", "", "", "", false);

            // act
            string result = customer.GetCustomerDetails();

            // assert
            string expected = "\t\n\t\n\t\n\t\n\t";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Set_AccuratlyUpdatesExistingValues_WhenNewValuesAreIntroduced()
        {
            // arrange
            Customer customer = new Customer();
            customer.SetInfo("Initial Name", "Initial Street", "Initial City", "IC", "00000", false);

            // act
            customer.SetInfo("Updated Name", "Updated Street", "Updated City", "UC", "11111", false);
            string result = customer.GetCustomerDetails();

            // assert
            string expected = "\tUpdated Name\n\tUpdated Street\n\tUpdated City\n\tUC\n\t11111";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReturnDetails_ContainingAllRequiredElements_WhenInfoIsSet()
        {
            // arrange
            Customer customer = new Customer();
            customer.SetInfo("Test Customer", "Test Address", "Test City", "TC", "12345", false);

            // act
            string details = customer.GetCustomerDetails();

            // assert
            Assert.IsTrue(details.Contains("Test Customer"));
            Assert.IsTrue(details.Contains("Test Address"));
            Assert.IsTrue(details.Contains("Test City"));
            Assert.IsTrue(details.Contains("TC"));
            Assert.IsTrue(details.Contains("12345"));
            Assert.IsTrue(details.Contains("\t"));
            Assert.IsTrue(details.Contains("\n"));
        }

        [TestMethod]
        [DataRow("Test Customer", "Test Address", "Test City", "TC", "12345", false)]
        [DataRow("Test Customer2", "Test Address2", "Test City", "TC", "12345", false)]
        [DataRow("Test Customer3", "Test Address3", "Test City", "TC", "12345", false)]
        public void ReturnsFalse_WhenLoyaltyStatusIsFalse(string name, string street, string city, string state, string zip, bool loyaltyClub)
        {
            // arrange
            Customer customer = new Customer();
            customer.SetInfo(name, street, city, state, zip, loyaltyClub);
            // act
            bool result = customer.GetLoyaltyStatus();
            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("Test Customer", "Test Address", "Test City", "TC", "12345", true)]
        [DataRow("Test Customer2", "Test Address2", "Test City", "TC", "12345", true)]
        [DataRow("Test Customer3", "Test Address3", "Test City", "TC", "12345", true)]
        public void ReturnsTrue_WhenLoyaltyStatusIsTrue(string name, string street, string city, string state, string zip, bool loyaltyClub)
        {
            // arrange
            Customer customer = new Customer();
            customer.SetInfo(name, street, city, state, zip, loyaltyClub);
            // act
            bool result = customer.GetLoyaltyStatus();
            // assert
            Assert.IsTrue(result);
        }
    }
}
