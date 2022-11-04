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
            Message testMsg2 = new Message
                (
                    "E", 
                    "keir11@hotmail.com", 
                    "Hi all, this is a test email."
                );
            Message testMsg3 = new Message
                (
                    "T", 
                    "@Keir__HEWitt", 
                    "Reminding you to keep your login details safe and step away from unsafe URLs. "+
                    "If you see a suspicious email, message, or notice that looks like it’s from us," +
                    " remember we'll never ask for login info via email, DM, or non-Twitter website."
                );
            Message testMsg4 = new Message("S", "+448843221230", "Twitter message 3, this is a test!");
            Message testMsg5 = new Message
                (
                    "E", "victoria.jast@hotmail.com", "Good Morning\n\n, " +
                    "I’ve recently had work done on the cast iron downpipe outside my flat and was advised to get an ASAP fix.\n"+
                    "All pieces are damaged and leaking, as well as some cracked brackets – is this something that you could fix ?\n" +
                    "Images of the damage attached above.\n" +
                    "Thanks for your time.\n" +
                    "Keir"
                );
            Message testMsg6 = new Message("E", "emmet30@yahoo.com", "Twitter message 3, this is a test!");
            Message testMsg7 = new Message("T", "@_001sfc", "Gerard Piqué announces he is retiring from football and will play his last match for Barcelona this Saturday 🔵🔴");
            Message testMsg8 = new Message("T", "@Cristiano", "We move on and we keep going after our goals this season! Thanks to our supporters that never give up on us!👏🏽");
            Message testMsg9 = new Message("S", "09968458932", "Leaving in 10.");
            Message testMsg10 = new Message
                (
                    "T", 
                    "@Keir__HEWitt", 
                    "Eli: Lets go to Peyton. Hes going to breakdown that touchdown."+
                    "Peyton: Can't hear sh-t"+
                    "Eli: Nevermind"+
                    "😂😂😂"
                );
            Message testMsg11 = new Message("T", "@__Edd_", "Walmart be havin 80 year old Grandmas watchin the door, Ofc ima steal 💀💀💀");
            Message testMsg12 = new Message
                (
                    "T",
                    "@dodhria", 
                    "Update mais 🇧🇷PATRIOTA do Eurotruck!"+
                    "GRANDE DIA!!! 👍👍👍"
                );


            Messages.Add(testMsg1);
            Messages.Add(testMsg2); 
            Messages.Add(testMsg3);
            Messages.Add(testMsg4);
            Messages.Add(testMsg5);
            Messages.Add(testMsg6);
            Messages.Add(testMsg7);
            Messages.Add(testMsg8);            
            Messages.Add(testMsg9);
            Messages.Add(testMsg10);
            Messages.Add(testMsg11);
            Messages.Add(testMsg12);

        }
    }
}
