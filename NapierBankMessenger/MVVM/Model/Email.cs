
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NapierBankMessenger.MVVM.Model
{
    public class Email : Message
    {
        private string subject;

        private readonly string urlPlaceholder = "<URL Quarantined>";
        private List<string> _quarantinedURLS;

        // Subclass contains unique subject method
        public Email(string sender, string body, string subject) : base(sender, body)
        {
            SetType("E");
            this.subject = subject;
            _quarantinedURLS = new List<string>();
        }

        // Return true/false if search term exists in Sender, Body and Subject
        public override bool FindMatch(string searchQuery)
        {
            return GetSender().Contains(searchQuery) || GetBody().Contains(searchQuery) || subject.Contains(searchQuery);
        }

        // Format the hyperlinks
        public override void FormatBody()
        {
            SetBody(FormatURL(GetBody()));
        }

        // Get URLS, replace with placeholder text, add actual URL to Quarantine list
        public string FormatURL(string text)
        {
            string actualUrl = "";

            Match match = Regex.Match(text, @"(http|ftp|https)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?/g");

            if (match.Success)
            {
                // Separate out the actual URL and add it to Quarantined list
                actualUrl = "<" + match.Value + ">";
                _quarantinedURLS.Add(actualUrl);

                // Replace the displayed text with the placeholder text
                return text.Replace(match.ToString(), urlPlaceholder);
            }
            return text;
        }

        public List<string> GetQuarantinedURLs() { return _quarantinedURLS; }
        public string GetSubject() {  return subject; }

        // Override ToString() due to Email having Subject property
        public override string ToString()
        {
            return "Sender: " + GetSender() +
                    "\n" +
                    "Subject: " + GetSubject() + 
                    "\n" +
                    GetBody();
        }
    }
}
