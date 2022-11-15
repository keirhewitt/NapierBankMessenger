using NapierBankMessenger.MVVM.View;
using NapierBankMessenger.MVVM.Model;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class ViewModelController : ScriptableObject
    {
        private MessagePage MsgPage { get; }
        private InputParser InputPage { get; }
        private EndOfSession EndSession { get; }
        private Controller Ctrl { get; }
        
        private object _parserView;
        private object _messageListView;
        private object _endOfSession;

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

        public object EOS
        {
            get { return _endOfSession; }
            set
            {
                _endOfSession = value;
                OnPropertyChanged();
            }
        }

        public ViewModelController()
        {
            Ctrl = new Controller();
            MsgPage = new MessagePage(Ctrl);
            InputPage = new InputParser(Ctrl);
            EndSession = new EndOfSession();
            
            MessageView = MsgPage;
            ParserView = InputPage;
            EOS = EndSession;
        }
    }
}
