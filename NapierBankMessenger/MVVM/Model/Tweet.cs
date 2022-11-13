using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessenger.MVVM.Model
{
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
            SetType("T");
            //hashtags = new List<string>();
            //mentions = new List<string>();
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
