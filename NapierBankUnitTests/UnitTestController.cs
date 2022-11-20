using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessenger.MVVM.FileIO;
using NapierBankMessenger.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NapierBankUnitTests
{
    /// <summary>
    /// Test the methods inside the Controller.
    /// </summary>
    [TestClass]
    public class UnitTestController
    {
        Controller ctrl = new Controller();
        PrivateObject controllerClass = new PrivateObject(typeof(Controller));

        Dictionary<string, int> mentionsTest = new Dictionary<string, int>();
        Dictionary<string, int> hashtagsTest = new Dictionary<string, int>();

        [TestMethod]
        // Tests that the controller methods are able to create a file and 
        public void TestFileCreation()
        {
            string filename = "newfile.json";
            ctrl.InitJSONFile(filename);
            bool result = Convert.ToBoolean(controllerClass.Invoke("FileExists", "C:\\Users\\Keir\\Desktop\\courseworkdata", filename));

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        // Test that the mechanism for adding Mentions/hashtags to dictionaries is working OK.
        public void TestAddingTrends()
        {        
            string[] mentions = { "@keirhewitt", "@johndoe", "@lancearmstrong", "@Jason_file", "@jack__jill", "@keirhewit", "@keirhewitt", "@.johndoe", };
            string[] hashtags = { "#keirhewitt", "#johndoe", "#lancearmstrong", "#Jason_file", "#jack__jill", "#keirhewit", "#keirhewitt", "#.johndoe", };

            foreach(string mention in mentions)
                ctrl.AddToDictionary(mentionsTest, mention);
            foreach(string hashtag in hashtags)
                ctrl.AddToDictionary(hashtagsTest, hashtag);    

            Assert.AreEqual(1, mentionsTest["@johndoe"]); // Assert that only 1 @johndoe was passed through
            Assert.AreEqual(2, mentionsTest["@keirhewitt"]); // Assert that the value of @keirhewitt = 2
            Assert.AreEqual(7, mentionsTest.Count); // Counts the number of key/value pairs, NOT the number of items added to it

            Assert.AreEqual(1, hashtagsTest["#johndoe"]); // Assert that only 1 #johndoe was passed through
            Assert.AreEqual(2, hashtagsTest["#keirhewitt"]); // Assert that the value of #keirhewitt = 2
            Assert.AreEqual(7, hashtagsTest.Count);
        }

        [TestMethod]
        // Test that the mechanism for removing Mentions/hashtags to dictionaries is working OK.
        public void TestRemovingTrends()
        {
            string[] mentions = { "@keirhewitt", "@johndoe", "@lancearmstrong", "@Jason_file", "@jack__jill", "@keirhewit", "@keirhewitt", "@.johndoe", };
            string[] hashtags = { "#keirhewitt", "#johndoe", "#lancearmstrong", "#Jason_file", "#jack__jill", "#keirhewit", "#keirhewitt", "#.johndoe", };

            foreach (string mention in mentions)
                ctrl.AddToDictionary(mentionsTest, mention);
            foreach (string hashtag in hashtags)
                ctrl.AddToDictionary(hashtagsTest, hashtag);

            ctrl.RemoveFromDictionary(mentionsTest, "@keirhewitt");
            ctrl.RemoveFromDictionary(mentionsTest, "@johndoe");

            bool ans = mentionsTest.TryGetValue("@johndoe", out int value);
            Assert.IsFalse(ans); // Assert that @johndoe key check is null after removing
            Assert.AreEqual(1, mentionsTest["@keirhewitt"]); // Assert that the value of @keirhewitt = 1 after removing one of them
            Assert.AreEqual(6, mentionsTest.Count); // Assert that this is still 7, since a key/value pair was NOT removed
        }

        [TestMethod]
        // Upon adding and Email with a link, ensure that QuarantinedURLs list in Controller contains the URL
        // Also ensure that the correct formatting is adhered to for the Email body
        public void TestQuarantinedURLs()
        {
            Textspeak.IO();

            string testlink = "https://beta.openai.com/playground";
            string falselink = "http://beta.openai.com/play";
            string test = "This is a test method with a https://beta.openai.com/playground hyperlink contained within it.";
            string expected = "This is a test method with a <URL Quarantined> hyperlink contained within it.";

            Email testemail = new Email("keir@gmail.com", test, "RE: Unit Test Methods");
            ctrl.AddMessage(testemail);

            string actual = testemail.FormatURL(test);

            Assert.AreEqual(expected, actual);
            Assert.IsFalse(ctrl.GetQuarantinedURLs().ContainsKey(falselink));
            Assert.IsTrue(ctrl.GetQuarantinedURLs().ContainsKey(testlink));
        }

        [TestMethod]
        // Upon adding SIRs, ensure that they are added to the SIR list in Controller
        // Index them via Incident Type
        public void TestSIRList()
        {
            Textspeak.IO();

            SIR testsir = new SIR("keir@gmail.com", "83-23-25\nStaff Abuse", "SIR 15/11/22");
            SIR testsir2 = new SIR("keir@gmail.com", "83-23-25\nATM Theft", "SIR 15/11/22");
            ctrl.AddMessage(testsir);

            Assert.IsNotNull(ctrl.GetSIRList().Where(i => i.GetIncidentType() == "Staff Abuse"));
            Assert.IsNotNull(ctrl.GetSIRList().Where(i => i.GetIncidentType() == "ATM Theft"));
        }
    }
}
