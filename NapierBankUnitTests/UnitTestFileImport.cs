using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessenger.MVVM.FileIO;
using System;
using System.Linq;

namespace NapierBankUnitTests
{
    [TestClass]
    public class UnitTestFileImport
    {
        [TestMethod]
        public void AbbreviationsImport()
        {
            Textspeak.IO();
            Assert.IsNotNull(Textspeak.GetAbbreviations());
            Assert.AreEqual("AAP", Textspeak.GetAbbreviations()[0]);
            Assert.AreEqual("YW", Textspeak.GetAbbreviations().LastOrDefault());
        }

        [TestMethod]
        // Make sure csv data has been imported properly by checking against known results
        public void PhrasesImport()
        {
            Textspeak.IO();
            Assert.IsNotNull(Textspeak.GetPhrases());
            Assert.AreEqual("Always a pleasure", Textspeak.GetPhrases()[0]);
            Assert.AreEqual("You're welcome", Textspeak.GetPhrases().LastOrDefault());

        }
    }
}
