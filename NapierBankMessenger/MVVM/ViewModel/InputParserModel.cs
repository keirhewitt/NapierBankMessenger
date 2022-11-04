
using NapierBankMessenger.Commands;
using System.Windows.Data;
using System.Windows.Input;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class InputParserModel : ScriptableObject
    {
        private readonly string[] email_format_sequences = { "@hotmail.com", "@gmail.com", "@live.napier.ac.uk" };

        private string _sender;
        private string _subject;
        private string _body;
        private bool subjectRequired = true;

        public ICommand ParseDataButton { get; private set; }

        // Binded to Subject Text Box - Field linked with "IsEnabled"
        public bool SubjectLineDisabled
        {
            get { return subjectRequired; }
        }

        public string Sender 
        { 
            get { return _sender; }
            set
            {
                _sender = value;
                // IF Sender line is in Email format, enable Subject Box
                if (CheckForEmail(_sender))
                {
                    subjectRequired = true;
                }
                else
                {
                    subjectRequired = false;
                }
                OnPropertyChanged();
                OnPropertyChanged("SubjectLineDisabled");
            } 
        }

        public string Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
                OnPropertyChanged();
            }
        }

        public string Body
        {
            get { return _body; }
            set
            {
                _body = value;
                OnPropertyChanged();
            }
        }


        public InputParserModel()
        {
            ParseDataButton = new RelayCommand(ParseData, ReadyToParseData);
        }

        private void RaiseInputError(string error)
        {

        }

        public bool CheckForEmail(string sender_line)
        {
            for (int i = 0; i < email_format_sequences.Length; i++)
            {
                if (sender_line.Contains(email_format_sequences[i]))
                    return true;
            }
            return false;
        }

        private bool ReadyToParseData(object data)
        {
            if (subjectRequired)
            {
                if (!string.IsNullOrEmpty(Subject))
                {

                }
            }

            return true;
        }

        private void ParseData(object data)
        {
            // ...
        }

    }
}
