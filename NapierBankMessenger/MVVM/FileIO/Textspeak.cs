using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NapierBankMessenger.MVVM.FileIO
{
    // Handles loading of Textspeak abbreviations CSV file
    public static class Textspeak
    {
        // Separate lists for abbreviations and phrases
        private static List<string> abbreviations = new List<string>();
        private static List<string> phrases = new List<string>();

        // Read data from csv file and append each column to lists
        public static void IO()
        {
            string[] l = File.ReadAllLines(@"C:\\Users\\Keir\\OneDrive\\University Work\\Year 3\\Software Engineering\\Coursework\\textwords.csv");
            foreach (string s in l)
            {
                string abbrev = s.Split(',')[0];
                string phrase = s.Split(',')[1];
                abbreviations.Add(abbrev);
                phrases.Add(phrase);
            }
        }

        // Make lists publically available for access
        public static List<string> GetAbbreviations() {  return abbreviations; }
        public static List<string> GetPhrases() { return phrases; }
    }
}
