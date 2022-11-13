using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NapierBankMessenger.MVVM.Model;

namespace NapierBankUnitTests
{
    
    [TestClass]
    public class MessageCreation
    {

        [TestMethod]
        public void TestMethod1()
        {
            string test = "This is a test method with a https://www.wayfair.co.uk/furniture/pdp/17-stories-multimedia-open-dvdcd-shelf-u000782724.html hyperlink contained within it.";
            string expected = "This is a test method with a <https://www.wayfair.co.uk/furniture/pdp/17-stories-multimedia-open-dvdcd-shelf-u000782724.html> hyperlink contained within it.";
            //string actual = Email.MakeLink(test);
        }
    }
}
