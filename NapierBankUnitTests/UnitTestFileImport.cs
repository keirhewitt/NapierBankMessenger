using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessenger.MVVM.FileIO;
using System;
using System.Linq;

namespace NapierBankUnitTests
{
    /// <summary>
    /// Testing the FileIO class to ensure that the CSV file is imported and handled correctly
    /// </summary>
    [TestClass]
    public class UnitTestFileImport
    {
        [TestMethod]
        // Import abbreviations and verify the first and last elements in the Array against the known results
        public void AbbreviationsImport()
        {
            Textspeak.IO();
            Assert.IsNotNull(Textspeak.GetAbbreviations());
            Assert.AreEqual("AAP", Textspeak.GetAbbreviations()[0]);
            Assert.AreEqual("YW", Textspeak.GetAbbreviations().LastOrDefault());
        }

        [TestMethod]
        // Import phrases and verify the first and last elements in the Array against the known results
        public void PhrasesImport()
        {
            Textspeak.IO();
            Assert.IsNotNull(Textspeak.GetPhrases());
            Assert.AreEqual("Always a pleasure", Textspeak.GetPhrases()[0]);
            Assert.AreEqual("You're welcome", Textspeak.GetPhrases().LastOrDefault());
        }
    }
}
