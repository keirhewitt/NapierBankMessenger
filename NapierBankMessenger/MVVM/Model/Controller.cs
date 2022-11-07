using System.Text.Json;
using System.Collections.Generic;

namespace NapierBankMessenger.MVVM.Model
{
    public class Controller
    {
        private List<Message> _messages { get; set; }
        private List<SIR> _SIRs { get; set; }
        public Message[] Messages { get; set; }

        public void ValidateMessage(Message message)
        {
            
        }

        public void SanitizeMessage(Message message)
        {

        }

        public void CategorizeMessage(Message message)
        {

        }

        public void SendMessageToJSON(Message message)
        {

        }

        public void LoadMessagesFromJSON()
        {

        }

        public void AddMessage(Message message) {  _messages.Add(message); }
        public void RemoveMessage(Message message) { _messages.Remove(message); }
        public void AddSIR(SIR sir) { _SIRs.Add(sir); }
        public void RemoveSIR(SIR sir) { _SIRs.Remove(sir); }

        public List<Message> getMessages() { return _messages; }
        public List<SIR> getSIRs() { return _SIRs; }
    }
}
