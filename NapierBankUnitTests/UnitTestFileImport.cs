using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessenger.MVVM.FileIO;
using System;

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
            foreach(string s in Textspeak.GetAbbreviations())
            {
                Assert.AreEqual("h", s);
            }
            Assert.AreEqual("AAP", Textspeak.GetAbbreviations()[0]);
        }

        [TestMethod]
        public void PhrasesImport()
        {
            Textspeak.IO();
            Assert.IsNotNull(Textspeak.GetPhrases());
            Assert.AreEqual("Always A Pleasure", Textspeak.GetPhrases()[0]);
            foreach (string s in Textspeak.GetAbbreviations())
            {
                Assert.AreEqual("Always A Pleasure", s);
            }
        }
    }
}
