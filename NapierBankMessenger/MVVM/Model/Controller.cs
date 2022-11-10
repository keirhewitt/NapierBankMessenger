using System.Text.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NapierBankMessenger.MVVM.Model
{
    public class Controller
    {
        private ObservableCollection<Message> _messages;
        private List<SIR> _SIRs { get; set; }

        public ObservableCollection<Message> Messages 
        { 
            get => _messages; 
        }

        public Controller()
        {
            _messages = new ObservableCollection<Message>();
        }

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
        public List<SIR> getSIRs() { return _SIRs; }
    }
}
