
using NapierBankMessenger.MVVM.FileIO;

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
        public static int IDSelector = 1;

        private readonly string type; // "S", "E" or "T"
        private readonly string header; // _id + 9 numeric chars
        private readonly string sender; // Sender i.e. phone number, email, twitter ID
        private string body; // Text content of the message

        /**
         *  Default param for subject since this will only be overwritten for Email messages
         */
        protected Message(string type, string sender, string body)
        {
            this.type = type;
            this.header = type + IDSelector.ToString("000000000");
            this.sender = sender;
            this.body = body;
            IDSelector += 1;
        }

        // Implemented by each subclass to format their respective body texts i.e. URLs, hyperlinks etc.
        public abstract void FormatBody();

        // For searching for specific messages
        public abstract bool FindMatch(string searchQuery);

        // Finds any abbreviations noted in the csv file and replaces them
        public string FindAbbreviations(string bodyText)
        {
            for (int i = 0; i < Textspeak.GetAbbreviations().Count; i++)
            {
                if (bodyText.Contains(Textspeak.GetAbbreviations()[i]))
                {
                    // Example: replace 'ROFL' with 'ROFL <Rolls on floor laughing>'
                    bodyText.Replace(Textspeak.GetAbbreviations()[i], Textspeak.GetAbbreviations()[i] + " <" + ReplaceAbbreviations(i) + ">");
                }
            }
            return bodyText;
        }

        // Given an abbreviations list index, return the phrases counterpart
        public string ReplaceAbbreviations(int index)
        {
            // Get the relevant phrase
            return Textspeak.GetPhrases()[index];
        }

        public string   GetMessageType(){ return type; }
        public string   GetHeader(){ return header; }
        public string   GetSender(){ return sender; }
        public string   GetBody() { return body; }
        public void     SetBody(string body) { this.body = body; }
    }
}
