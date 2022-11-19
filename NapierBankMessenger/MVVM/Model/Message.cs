
using NapierBankMessenger.MVVM.FileIO;
using System;
using System.Collections.Generic;

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
     *                                 
       /// <summary>
       /// Abstract class which all Message types will be based on.
       /// </summary>
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
        protected Message(string sender, string body, string subject="")
        {  
            this.Sender = sender;
            this.Body = body;
            this.Subject = subject;
            IDSelector += 1;
        }

        // Implemented by each subclass to format their respective Body texts i.e. URLs, hyperlinks etc.
        public abstract void FormatBody();

        // !! EVOLUTION !!
        // For searching for specific messages
        public abstract bool FindMatch(string searchQuery);

        // Set the header as a unique value preficed by the message type
        public abstract void FormatHeader();

        // Finds any abbreviations noted in the csv file and replaces them
        public string FindAbbreviations(string bodyText)
        {
            // Store abbreviations so we are not continually fetching the list for every check
            List<string> abb = Textspeak.GetAbbreviations();

            // Had to create new string, strings are immutable in C# !
            string newBody = bodyText;

            // Split into words, separate punctuation also i.e. LOL! = {'LOL', '!'} rather than: {'LOL!'}
            string[] bodySplit = bodyText.Split(new char[] { ' ', ',', '-', '!', '.' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < abb.Count; i++)
            {
                foreach (string word in bodySplit)
                {
                    if (word.Equals(abb[i]))
                    {
                        // Example: replace 'AAP', with 'AAP <Always a pleasure>'
                        newBody = bodyText.Replace(abb[i], abb[i] + " <" + ReplaceAbbreviations(i) + ">");
                    }                  
                }
            }
            return newBody;
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
        public void     SetHeader(string header) { this.Header = header; }
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
