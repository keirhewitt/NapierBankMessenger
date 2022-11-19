
using System;
using System.Collections.Generic;

namespace NapierBankMessenger.MVVM.Model
{
    /// <summary>
    /// Email (+ SIR) replace URLs in body with placeholder URL and store the full text one 
    /// They also have a non-empty subject
    /// </summary>
    public class Email : Message
    {
        private string subject;

        private readonly string urlPlaceholder = "<URL Quarantined>";
        private List<string> _quarantinedURLS = new List<string>();

        // Subclass contains unique subject method
        public Email(string sender, string body, string subject) : base(sender, body, subject)
        {
            this.subject = subject;
            FormatBody();
            FormatHeader();
        }

        // Return true/false if search term exists in Sender, Body and Subject
        public override bool FindMatch(string searchQuery)
        {
            return GetSender().Contains(searchQuery) || GetBody().Contains(searchQuery) || subject.Contains(searchQuery);
        }

        public override void FormatHeader()
        {
            SetHeader("E" + IDSelector.ToString("000000000"));
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
            Uri temp;

            foreach (string word in text.Split(' '))
            {
                // try to create Uri from word (Determine if valid URI or not)
                if (Uri.TryCreate(word, UriKind.Absolute, out temp))
                {
                    // Separate out the actual URL and add it to Quarantined list
                    actualUrl = word;
                    _quarantinedURLS.Add(actualUrl);

                    // Replace the displayed text with the placeholder text
                    return text.Replace(word, urlPlaceholder);
                }
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
