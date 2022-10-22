using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void addMessage(string message)
        {
            
        }
    }
}
