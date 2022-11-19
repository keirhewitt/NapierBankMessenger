using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessenger.MVVM.Model;
using NapierBankMessenger.MVVM.FileIO;

namespace NapierBankUnitTests
{
    [TestClass]
    public class UnitTestMessageCreation
    {
        [TestMethod]
        // Make sure email is added correctly
        // Make sure URL is stored in quarantine storage
        // Ensure that when an Email message is added that you can return the url inside the body via the quarantineList
        public void TestEmailObject()
        {
            Textspeak.IO();
            string testBody = "Responding to your query at https://www.wayfair.co.uk/daily-sales/black-friday - I think we can have that brought to the store.";
            string testExpected = "Responding to your query at <URL Quarantined> - I think we can have that brought to the store.";

            Email testemail = new Email("keir11@hotmail.com", "This is", "for testing.");
            Assert.AreEqual(testemail.FormatURL(testBody), testExpected);
            
        }

        [TestMethod]
        // Test that when a SIR is added, it goes into the SIR list
        // Test that the incident type is stored correctly
        public void TestSIRObject()
        {            
            Textspeak.IO();
        }

        [TestMethod]
        // Test SMS message is added appropriately
        // Test that SMS abbreviations are altered as expected
        public void TestSMSObject()
        {
            Textspeak.IO();
            string expected1 = "Yeah AAP <Always a pleasure> mate.";
            string expected2 = "Sorry was away, BAK <Back at keyboard>.";
            string expected3 = "GL <Good luck>!";

            SMS testsms1 = new SMS("07854215232", "Yeah AAP mate.");
            SMS testsms2 = new SMS("07854215232", "Sorry was away, BAK.");
            SMS testsms3 = new SMS("07854215232", "GL!");

            Assert.AreEqual(expected1, testsms1.GetBody());
            Assert.AreEqual(expected2, testsms2.GetBody());
            Assert.AreEqual(expected3, testsms3.GetBody());

        }

        [TestMethod]
        // Test that Tweet objects are added
        // Test that Tweet abbreviations are being handled properly
        public void TestTweetObject()
        {
            Textspeak.IO();
            string expected = "Yeah AAP <Always a pleasure> mate.";
            Tweet testtweet = new Tweet("@testuser", "Yeah AAP mate.");
            Assert.AreEqual(expected, testtweet.GetBody());
        }
    }
}
