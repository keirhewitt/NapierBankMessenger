
using System.Text.RegularExpressions;

namespace NapierBankMessenger.MVVM.Model
{
    public class Email : Message
    {
        private string subject;

        public Email(string type, string sender, string body, string subject) : base(type, sender, body)
        {
            this.subject = subject;
        }

        // Return true/false if search term exists in Sender, Body and Subject
        public override bool FindMatch(string searchQuery)
        {
            return GetSender().Contains(searchQuery) || GetBody().Contains(searchQuery) || subject.Contains(searchQuery);
        }

        // Format the hyperlinks
        public override void FormatBody()
        {
            SetBody(MakeLink(GetBody()));
        }

        // Surround hyperlinks with < > 
        public string MakeLink(string text)
        {
            string formattedMatch = "";
            Match match = Regex.Match(text, @"(http|ftp|https)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?/g");
            if (match.Success)
            {
                formattedMatch = "<" + match.Value + ">";
                return text.Replace(match.ToString(), formattedMatch.ToString());
            }
            return text;
        }
    }
}
