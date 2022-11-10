
using NapierBankMessenger.MVVM.Model;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class ParseMessageViewModel
    {
        public string _sender;
        public string _subject;
        public string _body;

        public void ParseMessage(string type, string sender, string subject, string body)
        {
             switch (type)
            {
                case "S":
                    Message newSMS = new Message("S", sender, body);
                    break;
                case "E":
                    Message newEmail = new Message("E", sender, body, subject);
                    break;
                case "T":
                    Message newTweet = new Message("T", sender, body);
                    break;
            }
        }

        public void DetermineType()
        {

        }
    }
}
