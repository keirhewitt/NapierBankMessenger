

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
    public class Message
    {
        public static int IDSelector = 1;

        public string Type { get; set; } // "S", "E" or "T"
        public string Header { get; set; } // _id + 9 numeric chars
        public string Sender { get; set; } // Sender i.e. phone number, email, twitter ID
        public string Subject { get; set; } // Subject of the message
        public string Body { get; set; } // Text content of the message

        /**
         *  Default param for subject since this will only be overwritten for Email messages
         */
        public Message(string type, string sender, string body, string subject="")
        {
            Type = type;
            Header = type + IDSelector.ToString("000000000");
            Sender = sender;
            Subject = subject;
            Body = body;
            IDSelector += 1;
        }

        // For searching for specific messages
        public bool FindMatch(string searchQuery)
        {
            return Sender.Contains(searchQuery) || Subject.Contains(searchQuery) || Body.Contains(searchQuery);
        }

        /*public string getType(){ return type; }
        public string getHeader(){ return Header; }
        public string getSender(){ return Sender; }
        public string getSubject(){ return Subject; }
        public string getMainBody() { return Body; }*/
    }
}
