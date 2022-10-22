
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

        private string _id; // "S", "E" or "T"
        private string _header; // _id + 9 numeric chars
        private string _body; // Text content of the message

        public Message(string id, string body)
        {
            _id = id;
            _header = id + IDSelector.ToString("000000000");
            _body = body;
            IDSelector += 1;
        }

        public string getID(){ return _id; }
        public string getHeader(){ return _header; }
        public string getBody(){ return _body; }
    }
}
