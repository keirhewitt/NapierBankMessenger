using NapierBankMessenger.MVVM.Model;
using System.Collections.ObjectModel;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class MessagePageController : ScriptableObject
    {
        public ObservableCollection<Message> Messages { get; set; }
        public string MessageListHeader { get { return "Messages"; } }

        public MessagePageController()
        {
            Messages = new ObservableCollection<Message>();
            TestFunction();
        }

        private void TestFunction()
        {
            Message testMsg1 = new Message("S", "07854215232", "Test Message #1");
            Message testMsg2 = new Message("E", "keir11@hotmail.com", "Hi all, this is a test email.");
            Message testMsg3 = new Message("T", "@Keir__HEWitt", "Twitter message 3, this is a test!");

            Messages.Add(testMsg1);
            Messages.Add(testMsg2); 
            Messages.Add(testMsg3);
        }
    }
}
