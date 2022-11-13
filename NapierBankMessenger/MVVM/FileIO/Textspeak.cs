using System.Collections.Generic;
using System.IO;

namespace NapierBankMessenger.MVVM.FileIO
{
    public static class Textspeak
    {
        private static List<string> abbreviations = new List<string>();
        private static List<string> phrases = new List<string>();

        // Read in CSV file and append each column to the 2 lists
        public static void Main()
        {
            using(var r = new StreamReader("C:\\Users\\Keir\\OneDrive\\University Work\\Year 3\\Software Engineering\\Coursework\\textwords.csv"))
            {
                abbreviations = new List<string>();
                phrases = new List<string>();

                while(r.EndOfStream)
                {
                    var line = r.ReadLine();
                    var sep = line.Split(';');

                    abbreviations.Add(sep[0]); // Add first col to abbreviations list
                    phrases.Add(sep[1]); // Add second col to phrases list

                }
            }
        }

        // Make lists publically available for access
        public static List<string> GetAbbreviations() {  return abbreviations; }
        public static List<string> GetPhrases() { return phrases; }
    }
}
