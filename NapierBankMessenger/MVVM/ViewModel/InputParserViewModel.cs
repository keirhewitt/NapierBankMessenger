
using NapierBankMessenger.Commands;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using NapierBankMessenger.MVVM.Model;
using System;

namespace NapierBankMessenger.MVVM.ViewModel
{
    /// <summary>
    /// Data Context for the Input Parser, passes data from the UI to the validation View Model and receives error messages
    /// Will make the final decision on whether to create the User defined Message
    /// </summary>
    public class InputParserViewModel : ScriptableObject, IDataErrorInfo
    {
        private readonly string[] email_format_sequences = { "@hotmail.com", "@gmail.com", "@live.napier.ac.uk" };
        private readonly Controller _controller;

        // Properties of the user entered message
        private string _sender;
        private string _subject;
        private string _body;

        // Output of message input by user, displayed as feedback
        private string _output;

        // Is subject required for message? Email/SIR-
        private bool subjectRequired = false;

        public ICommand ParseDataButton { get; private set; }

        public Controller Ctrl { get => _controller; }
        public DataValidationViewModel validationModel;

        // Flag for message type: 0 = SMS, 1 = Email, 2 = Tweet, 3 = SIR, -1 = Error
        public Int16 MessageType
        {
            get => validationModel.MessageType;
            private set
            {
                validationModel.MessageType = value;
                OnPropertyChanged();
            } 
        }

        // Binded to Subject Text Box - Field linked with "IsEnabled"
        public bool SubjectLineEnabled { get => subjectRequired; set { subjectRequired = value; } }

        // Sender notifies each field as to which type of Message the user is trying to create
        // This will inform the subsequent validation
        public string Sender 
        {
            get => _sender;
            set
            {
                _sender = value;

                // IF Sender line is in Email format, enable Subject Box
                if (validationModel.CheckForEmail(Sender))
                {
                    SubjectLineEnabled = true;
                }
                else
                {
                    SubjectLineEnabled = false;
                    Subject = "";
                }
                OnPropertyChanged();
                OnPropertyChanged("Body");
                OnPropertyChanged("Subject");
                OnPropertyChanged("SubjectLineEnabled");
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
            set { _body = value; OnPropertyChanged(); OnPropertyChanged("Sender"); OnPropertyChanged("Subject"); }
        }

        public string Output
        {
            get => _output;
            set { _output = value; OnPropertyChanged(); }
        }

        public string Error { get => null; }

        // Return DataErrors specific to each field
        // Gets return message from GetFieldValidationErrors() function
        public string this[string field]
        {
            get => GetFieldValidationErrors(field);
        }

        public InputParserViewModel(Controller ctrl)
        {
            _controller = ctrl;
            validationModel = new DataValidationViewModel();
            ParseDataButton = new RelayCommand(ParseData, ParseCondition);
        }

        // Collects the validation errors for each field
        private string GetFieldValidationErrors(string propName)
        {
            // Initialize a null error every time before validating
            // Stops it from returning old errors
            string fieldError = null;

            // Validation whichever field was passed through
            switch (propName)
            {
                case "Sender":
                    fieldError = validationModel.ValidateSenderField(Sender);
                    break;
                case "Subject":
                    //if (SubjectLineEnabled)
                        //if (!string.IsNullOrEmpty(Subject))
                    fieldError = validationModel.ValidateSubject(Subject);
                    break;
                case "Body":
                    fieldError = validationModel.ValidateBody(Body);
                    break;
            }

            return fieldError;
        }

        // Checks if ready to submit message
        private bool ParseCondition(object data)
        {
            // Get confirmation from validation view model
            if (validationModel.ReadyToParseData())
            {
                // Make sure no fields have errors
                if (GetFieldValidationErrors("Sender") == "" && 
                    GetFieldValidationErrors("Subject") == "" &&
                    GetFieldValidationErrors("Body") == "")
                    {
                        return true;
                    }
            }
            return false;
        }

        // Determine message type and then create and add Message object with the relevant params
        private void ParseData(object data)
        {
            // SMS
            if (validationModel.GetMessageType() == 0)
            {
                Ctrl.AddMessage(new SMS(Sender, Body));
            }
            // Email
            else if (validationModel.GetMessageType() == 1)
            {
                Ctrl.AddMessage(new Email(Sender, Body, Subject));
            }
            // Tweet
            else if (validationModel.GetMessageType() == 2)
            {
                Ctrl.AddMessage(new Tweet(Sender, Body));
            }
            else
            {
                Ctrl.AddMessage(new SIR(Sender, Body, Subject));
            }

            // Display output as some feedback to user
            Output = Ctrl.Messages.Last().ToString();
        }


    }
}
