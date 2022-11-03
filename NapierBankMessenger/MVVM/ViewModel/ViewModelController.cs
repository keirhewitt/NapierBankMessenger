
using NapierBankMessenger.MVVM.View;
using NapierBankMessenger.MVVM.Model;
using System.Windows.Input;
using System.Diagnostics;
using NapierBankMessenger.Commands;
using System.Collections.ObjectModel;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class ViewModelController : ScriptableObject
    {
        public MessagePage _msgPage { get; }
        public InputParser _inputPage { get; }
        public Controller _controller;

        private readonly string[] email_format_sequences = { "@hotmail.com", "@gmail.com", "@live.napier.ac.uk" };
        public bool _isEmail = false;

        public string _sender { get; set; }
        public string _subject { get; set; }
        public string _body { get; set; }
        public string _output { get; set; }

        private object _parserView;
        private object _messageListView;

        public ICommand ParseDataButton { get; private set; }

        public bool IsEmail
        {
            get { return _isEmail; }
            set
            {
                _isEmail = value;
                OnPropertyChanged();
            }
        }

        public string Sender
        {
            get { return _sender; }
            set 
            {  
                _sender = value;
                if (CheckForEmail(_sender))
                {
                    // Enable subject field
                    _isEmail = true;
                    
                }
                else
                {
                    // Disable subject field + clear text from field
                    _isEmail = false;
                }
                OnPropertyChanged();
            }
        }

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; OnPropertyChanged(); }
        }

        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        public string Output
        {
            get { return _output; }
            set { _output = value; OnPropertyChanged(); }
        }

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
            
            ParseDataButton = new RelayCommand(ParseData);
        }

        private void ParseData()
        {
        }

        private bool CheckForEmail(string sender_text)
        {
            for (int i = 0; i < email_format_sequences.Length; i++)
            {
                if (sender_text.Contains(email_format_sequences[i]))
                {
                    return true;
                }else
                {
                    Debug.Write("Does not contain email!");
                }
            }
            return false;
        }


    }
}
