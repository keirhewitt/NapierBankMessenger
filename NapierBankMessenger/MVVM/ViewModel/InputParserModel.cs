
using System.Windows.Data;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class InputParserModel : ScriptableObject
    {
        private readonly string[] email_format_sequences = { "@hotmail.com", "@gmail.com", "@live.napier.ac.uk" };

        private string _sender;
        private string _subject;
        private string _body;

        public bool subjectRequired = true;

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
                // Sender line is in Email format, enable Subject Box
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
            
        }

        private void OnTargetUpdate(object sender, DataTransferEventArgs args)
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

    }
}
