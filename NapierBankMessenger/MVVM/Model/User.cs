using System;
using System.Collections.Generic;

namespace NapierBankMessenger.MVVM.Model
{
    public class User
    {
        private string _id;
        private string _sender;
        private List<Message> _message_history;

        public User(string sender)
        {
            _id = Guid.NewGuid().ToString();
            _sender = sender;
        }

        private void addMessage(Message msg) { _message_history.Add(msg); }
        private string getSender() { return _sender; }
        private string getID() { return _id; }
    }
}
