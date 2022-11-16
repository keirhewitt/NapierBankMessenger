using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessenger.MVVM.Model;
using System;

namespace NapierBankUnitTests
{
    
    [TestClass]
    public class UnitTestMessageCreation
    {

        [TestMethod]
        public void TestEmailObject()
        {     
            string test = "This is a test method with a https://www.wayfair.co.uk/furniture/pdp/17-stories-multimedia-open-dvdcd-shelf-u000782724.html hyperlink contained within it.";
            string expected = "This is a test method with a <URL Quarantined> hyperlink contained within it.";

            Email testemail = new Email("keir@gmail.com", test, "Test");

            string actual = testemail.FormatURL(test);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSIRObject()
        {

            SIR testsir = new SIR("keir@gmail.com", "83-23-25", "SIR 15/11/22");
            Assert.IsNotNull(testsir);
        }

        [TestMethod]
        public void TestSMSObject()
        {
            string expected = "Yeah AAP <Always a pleasure> mate.";
            SMS testsms = new SMS("07854215232", "Yeah AAP mate.");
            Assert.AreEqual(expected, testsms.GetBody());
        }
    }
}
