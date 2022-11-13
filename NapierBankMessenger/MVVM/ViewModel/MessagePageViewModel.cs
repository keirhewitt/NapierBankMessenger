using NapierBankMessenger.MVVM.Model;
using System.Collections.ObjectModel;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class MessagePageViewModel : ScriptableObject
    {
        private string MessageListHeader { get { return "Messages"; } }
        private Controller _controller;

        public Controller Ctrl 
        { 
            get { return _controller; } 
        }

        public ObservableCollection<Message> Messages
        {
            get => Ctrl.Messages;
            set
            {
                Ctrl.Messages = value;
                OnPropertyChanged("Messages");
            }
        }

        public MessagePageViewModel(Controller ctrl)
        {
            _controller = ctrl;
        }

    }
}
