using NapierBankMessenger.MVVM.FileIO;

namespace NapierBankMessenger.MVVM.Model
{
    public class SMS : Message
    {
        // Inherits all base constructor behaviour
        public SMS(string sender, string body) : base(sender, body) { SetType("S"); }

        // Overridden search query function
        public override bool FindMatch(string searchQuery)
        {
            return GetSender().Contains(searchQuery) || GetBody().Contains(searchQuery);
        }

        // SetBody -> Any abbreviations found and replaced in GetBody
        public override void FormatBody()
        {
            SetBody(FindAbbreviations(GetBody()));
        }       
    }
}
