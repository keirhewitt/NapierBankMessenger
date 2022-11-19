
using System.Collections.Generic;

namespace NapierBankMessenger.MVVM.Model
{
    /// <summary>
    /// Tweet objects are to keep track of hashtags and mentions.
    /// </summary>
    public class Tweet : Message
    {
        /* Lists of hashtags and mentions means that the Controller
         * can send request down the stack to get this data rather than
         * creating more controller instances and dependencies to
         * access it from down here */
        private List<string> hashtags = new List<string>();
        private List<string> mentions = new List<string>();

        public Tweet(string sender, string body) : base(sender, body)
        {
            FormatBody();
            FormatHeader();
        }

        // Overridden search query function
        public override bool FindMatch(string searchQuery)
        {
            return GetSender().Contains(searchQuery) || GetBody().Contains(searchQuery);
        }

        // Change any abbreviations and collect all hashtags and mentions
        public override void FormatBody()
        {
            SetBody(FindAbbreviations(GetBody()));
            GetHashtagsAndMentions();
        }

        // Override the FormatHeader to use unique char 'T'
        public override void FormatHeader()
        {
            SetHeader("T" + IDSelector.ToString("000000000"));
        }

        // Add Hashtags and Mentions to relevant lists.
        public void GetHashtagsAndMentions()
        {
            foreach (string word in GetBody().Split(' '))
            {
                if (word.StartsWith("#"))
                    hashtags.Add(word);
                else if (word.StartsWith("@"))
                    mentions.Add(word);
                continue;
            }
        }

        // Getters
        public List<string> GetHashtags() { return hashtags; }
        public List<string> GetMentions() { return mentions; }

    }
}
