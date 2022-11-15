
using NapierBankMessenger.MVVM.FileIO;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace NapierBankMessenger.MVVM.Model
{
    /**
     * Message types can be:
     *  SMS     -- BODY -> Sender: Int'l number, Body: 140 chars max.
     *              `-> [Textspeak abbreviations(CSV file)]
     *  Email   -- BODY -> Sender: Email address, Subject: 20 chars max, Body: 1028 chars max.
     *              `-> [Embedded urls] 
     *  Tweet   -- BODY -> Sender: Twitter ID, Tweet text: 140 chars max
     *              `-> [Textspeak abbreviations(CSV file), Hashtags, Twitter IDs]
     *  Significant Incident Report    -- SUBJECT - SIR dd/mm/yy
     *                                 -- BODY - sort code xx-xx-xx, Nature of incident (list of incidents given)
     *                                 -- Extra -> Sort Code and Nature of Incident written to a SIR list
     *                                 -- Any URLS will be removed and written to a quarantine list and replaced with <`URL Quarantined> in the BODY
     */
    public abstract class Message
    {
        public static int IDSelector = 1; // Static to keep the ID for each message unique
        private string type; // "S", "E" or "T"
        public string Header { get; private set; } // _id + 9 numeric chars
        public string Subject { get; private set; }
        public string Sender { get; private set; } // Sender i.e. phone number, email, twitter ID
        public string Body { get; private set; }  // Text content of the message
    
        // Default param for subject since this will only be overwritten for Email messages
        protected Message(string sender, string body)
        {
            this.Header = type + IDSelector.ToString("000000000");
            this.Sender = sender;
            this.Body = body;
            this.Subject = "";
            IDSelector += 1;
        }

        // Implemented by each subclass to format their respective Body texts i.e. URLs, hyperlinks etc.
        public abstract void FormatBody();

        // For searching for specific messages
        public abstract bool FindMatch(string searchQuery);

        // Finds any abbreviations noted in the csv file and replaces them
        public string FindAbbreviations(string bodyText)
        {
            for (int i = 0; i < Textspeak.GetAbbreviations().Count; i++)
            {
                if (bodyText.Contains(Textspeak.GetAbbreviations()[i]))
                    // Example: replace 'ROFL' with 'ROFL <Rolls on floor laughing>'
                    bodyText.Replace(Textspeak.GetAbbreviations()[i], Textspeak.GetAbbreviations()[i] + " <" + ReplaceAbbreviations(i) + ">");
            }
            return bodyText;
        }

        // Given an abbreviations list index, return the phrases counterpart
        public string ReplaceAbbreviations(int index)
        {
            // Get the relevant phrase
            return Textspeak.GetPhrases()[index];
        }

        // Getters + Setters
        public string   GetMessageType(){ return type; }
        public void     SetType(string type) { this.type = type; }
        public string   GetHeader(){ return Header; }
        public string   GetSender(){ return Sender; }
        public string   GetBody() { return Body; }
        public void     SetBody(string body) { this.Body = body; }

        // Override ToString() 
        public override string ToString()
        {
            return "Sender: " + GetSender() +
                    "\n" +
                    GetBody();
        }
    }
}
