using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NapierBankMessenger.MVVM.FileIO
{
    // Handles loading of Textspeak abbreviations CSV file
    public static class Textspeak
    {
        private static List<string> abbreviations = new List<string>();
        private static List<string> phrases = new List<string>();

        // Read data from csv file and append each column to lists
        public static void IO()
        {
            var result = File.ReadAllLines("C:\\Users\\Keir\\OneDrive\\University Work\\Year 3\\Software Engineering\\Coursework\\textwords.csv")
                .Select(row=>row.Split(';')).ToList();

            foreach (string abb in result[0])
            {
                abbreviations.Add(abb);
            }

            foreach (string phr in result[1])
            {
                phrases.Add(phr);
            }
        }

        // Make lists publically available for access
        public static List<string> GetAbbreviations() {  return abbreviations; }
        public static List<string> GetPhrases() { return phrases; }
    }
}
