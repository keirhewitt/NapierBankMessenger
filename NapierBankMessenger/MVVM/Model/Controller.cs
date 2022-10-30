
using System.Collections.Generic;

namespace NapierBankMessenger.MVVM.Model
{
    public class Controller
    {
        private List<Message> _messages { get; set; }
        private List<SIR> _SIRs { get; set; }
        public Message[] Messages { get; set; }

        public void validateMessage(Message message)
        {
            
        }

        public void sanitizeMessage(Message message)
        {

        }

        public void categorizeMessage(Message message)
        {

        }

        public void sendMessageToJSON(Message message)
        {

        }

        public void loadMessagesFromJSON()
        {

        }

        public void addMessage(Message message) {  _messages.Add(message); }
        public void removeMessage(Message message) { _messages.Remove(message); }
        public void addSIR(SIR sir) { _SIRs.Add(sir); }
        public void removeSIR(SIR sir) { _SIRs.Remove(sir); }

        public List<Message> getMessages() { return _messages; }
        public List<SIR> getSIRs() { return _SIRs; }
    }
}
