using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SUT = AcmeAnvils.Customer;
using AcmeAnvils;

namespace AcmeAnvilsTests
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        [DataRow("Ava", "Abercromby")]
        [DataRow("Brad", "Blackwell")]
        [DataRow("Chloe", "Crawford")]
        [DataRow("Darius", "Daniels")]
        public void GetFullNameTest(string inputFirstName, string inputLastName)
        {
            SUT customer = new SUT();
            customer.firstName = inputFirstName;
            customer.lastName = inputLastName;
            var result = customer.GetFullName();
            Assert.AreEqual(String.Concat(inputFirstName, " ", inputLastName), result);
        }


        [TestMethod]
        [DataRow("Ava")]
        [DataRow("Brad")]
        [DataRow("Chloe")]
        [DataRow("Darius")]
        [DataRow("Eleanor")]
        [DataRow("Felicity")]
        [DataRow("Gabrielle")]
        [DataRow("Hildegarde")]
        public void SeparateNamesTest_SingleName(string name)
        {
            SUT.SeparateNames(name, out string firstName, out string lastName);

            Assert.AreEqual(name, firstName);
            Assert.AreEqual(String.Empty, lastName);
        }

        [TestMethod]
        [DataRow("Ava", "Abercromby")]
        [DataRow("Brad", "Blackwell")]
        [DataRow("Chloe", "Crawford")]
        [DataRow("Darius", "Daniels")]
        [DataRow("Eleanor", "Elliot")]
        [DataRow("Felicity", "Frost")]
        [DataRow("Gabrielle", "Gale")]
        [DataRow("Hildegarde", "Han")]
        public void SeparateNamesTest_FirstLastName(string inputFirstName, string inputLastName)
        {
            var name = String.Concat(inputFirstName, " ", inputLastName);
            SUT.SeparateNames(name, out string firstName, out string lastName);

            Assert.AreEqual(inputFirstName, firstName);
            Assert.AreEqual(inputLastName, lastName);
        }

        [TestMethod]
        [DataRow("Ava", "A", "Abercromby")]
        [DataRow("Brad", "B", "Blackwell")]
        [DataRow("Chloe", "C", "Crawford")]
        [DataRow("Darius", "D", "Daniels")]
        [DataRow("Eleanor", "E", "Elliot")]
        [DataRow("Felicity", "F", "Frost")]
        [DataRow("Gabrielle", "G", "Gale")]
        [DataRow("Hildegarde", "H", "Han")]
        [DataRow("Ava", "A.", "Abercromby")]
        [DataRow("Brad", "B.", "Blackwell")]
        [DataRow("Chloe", "C.", "Crawford")]
        [DataRow("Darius", "D.", "Daniels")]
        [DataRow("Eleanor", "E.", "Elliot")]
        [DataRow("Felicity", "F.", "Frost")]
        [DataRow("Gabrielle", "G.", "Gale")]
        [DataRow("Hildegarde", "H.", "Han")]
        public void SeparateNamesTest_FirstMiddleLastName(string inputFirstName, string inputMiddleName, string inputLastName)
        {
            var firstMiddleName = String.Concat(inputFirstName, " ", inputMiddleName);
            var name = String.Concat(firstMiddleName, " ", inputLastName);
            SUT.SeparateNames(name, out string firstName, out string lastName);

            Assert.AreEqual(firstMiddleName, firstName);
            Assert.AreEqual(inputLastName, lastName);
        }

        [TestMethod]
        [DataRow("Ava", "AA", "", "Abercromby")] 
        [DataRow("Brad", "BB", "", "Blackwell")]
        [DataRow("Chloe", "CC", "", "Crawford")]
        [DataRow("Darius", "DD", "", "Daniels")]
        [DataRow("Eleanor", "EE", "", "Elliot")]
        [DataRow("Felicity", "FF", "", "Frost")]
        [DataRow("Gabrielle", "GG", "", "Gale")]
        [DataRow("Hildegarde", "HH", "", "Han")]
        [DataRow("Ava", "AA.", "", "Abercromby")]
        [DataRow("Brad", "BB.", "", "Blackwell")]
        [DataRow("Chloe", "CC.", "", "Crawford")]
        [DataRow("Darius", "DD.", "", "Daniels")]
        [DataRow("Eleanor", "EE.", "", "Elliot")]
        [DataRow("Felicity", "FF.", "", "Frost")]
        [DataRow("Gabrielle", "GG.", "", "Gale")]
        [DataRow("Hildegarde", "HH.", "", "Han")]
        [DataRow("Ava", "AA", "AA", "Abercromby")]
        [DataRow("Brad", "BB", "BB", "Blackwell")]
        [DataRow("Chloe", "CC", "CC", "Crawford")]
        [DataRow("Darius", "DD", "DD", "Daniels")]
        [DataRow("Eleanor", "EE", "EE", "Elliot")]
        [DataRow("Felicity", "FF", "FF", "Frost")]
        [DataRow("Gabrielle", "GG", "GG", "Gale")]
        [DataRow("Hildegarde", "HH", "HH", "Han")]
        [DataRow("Ava", "AA", "AA AA", "Abercromby")]
        [DataRow("Brad", "BB", "BB BB", "Blackwell")]
        [DataRow("Chloe", "CC", "CC CC", "Crawford")]
        [DataRow("Darius", "DD", "DD DD", "Daniels")]
        [DataRow("Eleanor", "EE", "EE EE", "Elliot")]
        [DataRow("Felicity", "FF", "FF FF", "Frost")]
        [DataRow("Gabrielle", "GG", "GG GG", "Gale")]
        [DataRow("Hildegarde", "HH", "HH HH", "Han")]
        public void SeparateNamesTest_FirstMiddleLastName_LongLastName(string inputFirstName, string inputMiddleName, string inputLastName1, string inputLastName2)
        {
            var expectedFirstMiddle = String.Concat(inputFirstName, " ", inputMiddleName);
            var expectedLastName = String.Concat(inputLastName1, " ", inputLastName2).Trim();
            var name = String.Concat(expectedFirstMiddle, " ", expectedLastName);
            SUT.SeparateNames(name, out string firstName, out string lastName);

            Assert.AreEqual(expectedFirstMiddle, firstName);
            Assert.AreEqual(expectedLastName, lastName);
        }

        [TestMethod]
        public void CheckStateLengthTest_NullState()
        {
            Assert.ThrowsException<ExitAppException>(() => SUT.CheckStateLength(null));
        }

        [TestMethod]
        [DataRow("A")]
        [DataRow("ABC")]
        [DataRow("ABCD")]
        [DataRow("ABCDE")]
        [DataRow("ABCDEF")]
        [DataRow("ABCDEFG")]
        public void CheckStateLengthTest_WrongCharacterLength(string input)
        {
            Assert.ThrowsException<ExitAppException>(() => SUT.CheckStateLength(input));
        }

        [TestMethod]
        [DataRow("wa", "WA")]
        [DataRow("WA", "WA")]
        [DataRow("Wa", "WA")]
        [DataRow("wA", "WA")]
        public void CheckStateLengthTest_Success(string input, string expectedResult)
        {
            var result = SUT.CheckStateLength(input);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void CheckZipCodeLengthTest_NullZipCode()
        {
            Assert.ThrowsException<ExitAppException>(() => SUT.CheckZipCodeLength(null));
        }

        [TestMethod]
        [DataRow("1")]
        [DataRow("12")]
        [DataRow("123")]
        [DataRow("1234")]
        [DataRow("123456")]
        [DataRow("1234567")]
        [DataRow("12345678")]
        [DataRow("123456789")]
        [DataRow("1234567890")]
        public void CheckZipCodeLengthTest_WrongLength(string input)
        {
            Assert.ThrowsException<ExitAppException>(() => SUT.CheckZipCodeLength(input));
        }

        [TestMethod]
        [DataRow("ABCDE")]
        [DataRow("1BCDE")]
        [DataRow("12CDE")]
        [DataRow("123DE")]
        [DataRow("1234E")]
        public void CheckZipCodeLengthTest_ContainsCharacters(string input)
        {
            Assert.ThrowsException<ExitAppException>(() => SUT.CheckZipCodeLength(input));
        }

        [TestMethod]
        [DataRow("12345")]
        [DataRow("67890")]
        public void CheckZipCodeLengthTest_Success(string input)
        {
            var result = SUT.CheckZipCodeLength(input);

            Assert.AreEqual(input, result);
        }
    }
}