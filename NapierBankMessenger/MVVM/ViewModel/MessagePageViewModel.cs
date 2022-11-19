
using NapierBankMessenger.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NapierBankMessenger.MVVM.ViewModel
{
    /// <summary>
    /// Handles the displaying of each message as a counterpart to the MessagePage View
    /// </summary>
    public class MessagePageViewModel : ScriptableObject
    {
        private string _messageListHeader = "Messages";

        public string MessageListHeader 
        { 
            get => _messageListHeader;         
        }

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
