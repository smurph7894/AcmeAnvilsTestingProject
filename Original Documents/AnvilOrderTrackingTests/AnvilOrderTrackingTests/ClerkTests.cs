using AnvilsOrderTaking;

namespace AnvilOrderTrackingTests
{
    [TestClass]
    public class ClerkTests
    {
        [TestMethod]
        public void Display_ClerkName_Whencalled()
        {
            // arrange 
            string expected = "Bob";

            // act
            Clerk clerk = new Clerk();
            clerk.SetName("Bob");
            string actual = clerk.GetName();

            // assert
            Assert.AreEqual(expected, actual);

        }
    }
}
