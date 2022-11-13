using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessenger.MVVM.Model
{
    public class Tweet : Message
    {
        public Tweet(string type, string sender, string body) : base(type, sender, body) { }

        // Overridden search query function
        public override bool FindMatch(string searchQuery)
        {
            return GetSender().Contains(searchQuery) || GetBody().Contains(searchQuery);
        }

        public override void FormatBody()
        {
            throw new NotImplementedException();
        }

        public void GetHashtagsAndMentions()
        {
            foreach (string word in GetBody().Split(' '))
            {
                if (word.StartsWith("#"))
                {

                }
            }
        }

    }
}
