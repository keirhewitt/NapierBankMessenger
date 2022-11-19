
namespace NapierBankMessenger.MVVM.Model
{
    /// <summary>
    /// SMS messages need to find and format the abbreviations in the body text
    /// </summary>
    public class SMS : Message
    {
        // Inherits all base constructor behaviour
        public SMS(string sender, string body) : base(sender, body, "") 
        { 
            FormatBody();
            FormatHeader();
        }

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

        public override void FormatHeader()
        {
            SetHeader("S" + IDSelector.ToString("000000000"));
        }
    }
}
