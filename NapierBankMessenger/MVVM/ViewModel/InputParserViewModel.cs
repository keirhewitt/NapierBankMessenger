
using NapierBankMessenger.Commands;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using NapierBankMessenger.MVVM.Model;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class InputParserViewModel : ScriptableObject, IDataErrorInfo
    {
        private readonly string[] email_format_sequences = { "@hotmail.com", "@gmail.com", "@live.napier.ac.uk" };
        private readonly Controller _controller;

        // Properties of the user entered message
        private string _sender;
        private string _subject;
        private string _body;

        // Properties of the submitted message into the appropriate format
        private string _output_sender;
        private string _output_subject;
        private string _output_body;
        private string _output;

        private bool subjectRequired = false;

        public Dictionary<string, string> InputErrors { get; private set; } = new Dictionary<string, string>();
        public ICommand ParseDataButton { get; private set; }

        public Controller Ctrl {  get => _controller; }
        public DataValidationViewModel validationModel;

        // Contains flags for each field --> 0: Phone, 1: Email, 2: Tweet
        // For Subject: 3 = Not Required, -1 = Validation Error
        // Int array of size: 3 (Sender, subject, body)
        public Dictionary<string, int> Flags
        {
            get => validationModel.Flags;
            private set
            {
                validationModel.Flags = value;
                OnPropertyChanged();
            } 
        }

        // Binded to Subject Text Box - Field linked with "IsEnabled"
        public bool SubjectLineDisabled { get => subjectRequired; }

        public string Sender 
        {
            get => _sender;
            set
            {
                _sender = value;
                // IF Sender line is in Email format, enable Subject Box
                if (validationModel.CheckForEmail(_sender) ? subjectRequired = true : subjectRequired = false)
                OnPropertyChanged();
                OnPropertyChanged("SubjectLineDisabled");
            } 
        }

        public string Subject
        {
            get => _subject;
            set { _subject = value; OnPropertyChanged(); }
        }

        public string Body
        {
            get => _body;
            set { _body = value; OnPropertyChanged(); }
        }

        public string Output
        {
            get => _output;
            set { _output = value; OnPropertyChanged(); }
        }

        public string Error { get => null; }

        // Return DataErrors specific to each field
        public string this[string field]
        {
            get
            {
                string fieldError = null;

                switch(field)
                {
                    case "Sender":
                        fieldError = validationModel.ValidateSenderField(Sender);
                        break;
                    case "Subject":
                        if (SubjectLineDisabled)
                            fieldError = validationModel.ValidateSubject(Subject);
                        break;
                    case "Body":
                        fieldError = validationModel.ValidateBody(Body);
                        break;
                }

                // If InputErrors contains the field Key, set the new error Value to that Key
                if (InputErrors.ContainsKey(field))
                {
                    InputErrors[field] = fieldError;
                }
                // Otherwise create a new field Key and Value
                else if (fieldError != null)
                {
                    InputErrors.Add(field, fieldError);
                }

                OnPropertyChanged("InputErrors");

                return fieldError;
            }
        }

        public InputParserViewModel(Controller ctrl)
        {
            _controller = ctrl;
            validationModel = new DataValidationViewModel();
            ParseDataButton = new RelayCommand(ParseData, ParseCondition);
        }

        // Checks if ready to submit message
        private bool ParseCondition(object data)
        {
            return validationModel.ReadyToParseData();
        }

        // Determine message type and then create and add Message object with the relevant params
        private void ParseData(object data)
        {
            // SMS
            if (validationModel.GetMessageType() == "S")
            {
                Ctrl.AddMessage(new Message("S", Sender, Body));
            }
            // Email
            else if (validationModel.GetMessageType() == "E")
            {
                Ctrl.AddMessage(new Message("E", Sender, Body, Subject));
            }
            // Tweet
            else
            {
                Ctrl.AddMessage(new Message("T", Sender, Body, Subject));
            }

            Output = Ctrl.Messages.Last().Sender + "\n" + Ctrl.Messages.Last().Subject + "\n" + Ctrl.Messages.Last().Body;
        }

    }
}
