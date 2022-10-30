
using NapierBankMessenger.MVVM.View;
using NapierBankMessenger.MVVM.Model;
using System.Windows.Input;
using System.Diagnostics;
using NapierBankMessenger.Commands;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class ViewModelController : ScriptableObject
    {
        public MessagePage _msgPage { get; }
        public InputParser _inputPage { get; }
        public Controller _controller;

        public string _sender { get; set; }
        public string _subject { get; set; }
        public string _body { get; set; }
        public string _output { get; }

        private object _parserView;
        private object _messageListView;

        public ICommand ParseDataButton { get; private set; }

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
            MessageView = _msgPage;
            _inputPage = new InputParser();
            ParserView = _inputPage;
            _controller = new Controller();
            ParseDataButton = new RelayCommand(ParseData);
        }

        private void ParseData()
        {
            Debug.WriteLine("Button pressed.");
        }

        private 

    }
}
