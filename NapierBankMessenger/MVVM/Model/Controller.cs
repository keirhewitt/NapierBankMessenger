using System.Text.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NapierBankMessenger.MVVM.ViewModel;

namespace NapierBankMessenger.MVVM.Model 
{
    public class Controller : ScriptableObject
    {
        private ObservableCollection<Message> _messages;
        private ObservableCollection<SIR> _sirList;

        public ObservableCollection<SIR> SIRs 
        { 
            get => _sirList;
            set => _sirList = value; 
        }

        public ObservableCollection<Message> Messages 
        { 
            get => _messages; 
            set => _messages = value;
        }

        public Controller()
        {
            _messages = new ObservableCollection<Message>();
        }

        public void SendMessageToJSON(Message message)
        {

        }

        public void LoadMessagesFromJSON()
        {

        }

        public void AddMessage(Message message) {  _messages.Add(message); }
        public void RemoveMessage(Message message) { _messages.Remove(message); }
        public void AddSIR(SIR sir) { _sirList.Add(sir); }
        public void RemoveSIR(SIR sir) { _sirList.Remove(sir); }
    }
}
