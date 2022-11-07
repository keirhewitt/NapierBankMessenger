using NapierBankMessenger.MVVM.View;
using NapierBankMessenger.MVVM.Model;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class ViewModelController : ScriptableObject
    {
        public MessagePage _msgPage { get; }
        public InputParser _inputPage { get; }
        public Controller _controller;
        
        private object _parserView;
        private object _messageListView;  

        private readonly string _title = "Napier Bank";

        public string MainTitle { get { return _title; } }
        
        public object ParserView
        {
            get { return _parserView; }
            set 
            {
                _parserView = value;
                OnPropertyChanged();
            }
        }

        public object MessageView
        {
            get { return _messageListView; }
            set
            {
                _messageListView = value;
                OnPropertyChanged();
            }
        }


        public ViewModelController()
        {
            _msgPage = new MessagePage();
            _inputPage = new InputParser();
            _controller = new Controller();
            
            MessageView = _msgPage;
            ParserView = _inputPage;          
        }

    }
}
