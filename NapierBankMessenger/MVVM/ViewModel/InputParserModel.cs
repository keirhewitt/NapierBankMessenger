
using NapierBankMessenger.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class InputParserModel : ScriptableObject, IDataErrorInfo
    {
        private readonly string[] email_format_sequences = { "@hotmail.com", "@gmail.com", "@live.napier.ac.uk" };

        public Dictionary<string, string> InputErrors { get; private set; } = new Dictionary<string, string>();

        private string _sender;
        private string _subject;
        private string _body;
        private bool subjectRequired = false;

        public ICommand ParseDataButton { get; private set; }

        // Contains flags for each field --> 0: Phone, 1: Email, 2: Tweet
        // Int array of size: 3 (Sender, subject, body)
        public int[] Flags { get; private set; }

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

        public string Error { get { return null; } }

        public string this[string field]
        {
            get
            {
                string fieldError = null;

                switch(field)
                {
                    case "Sender":
                        fieldError = ValidateSenderField(Sender);
                        break;
                    case "Subject":
                        if (SubjectLineDisabled)
                            fieldError = ValidateSubject(Subject);
                        break;
                    case "Body":
                        // ... Create ValidateBody()
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


        public InputParserModel()
        {
            ParseDataButton = new RelayCommand(ParseData, ReadyToParseData);
            Flags = new int[3] { -1, -1 , -1 };
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

        // Data validation for Input forms
        private bool ReadyToParseData(object data)
        {   
            // Check if any of the Flags have been set to -1 (Error)
            if (Flags.Any(i => i == -1))
            {
                return false;
            }

            return true;
        }

        private void ParseData(object data)
        {
            // ...
        }

        private string ValidateSubject(string subject)
        {
            // If Sender is 1 (Email)
            if (subject.Length > 20)
            {
                Flags[1] = -1;
                return "Subject must be a max of 20 characters.";
            }
            else
            {
                Flags[1] = 1;
            }
            return "";
        }

        // Validate a Phone Number in the Sender Field
        private string ValidatePhoneNumber(string phoneNumber)
        {
            // String contains only numeric values
            if (!phoneNumber.All(char.IsDigit))
            {
                Flags[0] = -1;
                return "Phone number must contain only numeric characters.";
            }

            if (Regex.IsMatch(phoneNumber, @"^\d")) // If string begins with numeric char
            {                
                if (phoneNumber.Length > 12)
                {
                    Flags[0] = -1;
                    return "Phone Number must be 12 characters long!";
                }
            }
            else if (phoneNumber.StartsWith("+44")) // Deal with +44, add 2 digits on to max length
            {
                if (phoneNumber.Length > 14)
                {
                    Flags[0] = -1;
                    return "Phone Number must be 12 characters long!";
                }
            }
            // Set the Flag if no error
            else
            {
                Flags[0] = 0;
            }
            return "";
        }

        // Validate an Email Address in the Sender Field
        private string ValidateEmail(string emailaddress)
        {
            if (!CheckForEmail(emailaddress))
            {
                Flags[0] = -1;
                return "Not a valid email address.";
            }
            else if (emailaddress.Length > 320)
            {
                Flags[0] = -1;
                return "Invalid email address - too large!";
            }
            // Set the Flag if no error
            else
            {
                Flags[0] = 1;
            }
            return "";
        }

        // Validate a Twitter ID in the Sender Field
        private string ValidateTwitterID(string twitterID)
        {
            if (twitterID.Length > 16 || twitterID.Length < 4)
            {
                Flags[0] = -1;
                return "Twitter ID must be between 4 and 16 characters.";    
            }
            // Set the Flag if no error
            else
            {
                Flags[0] = 2;
            }
            return "";
        }

        // Ensure a correct Sender format, returns Error message as a tooltip when hovering over the Sender Text Box
        private string ValidateSenderField(string sender)
        {
            // Initial Null or Empty check
            if (string.IsNullOrWhiteSpace(Sender))
            {
                return "Sender field cannot be empty!";
            }

            // Phone Number
            if (Regex.IsMatch(sender, @"^\d") || sender.StartsWith("+44"))
            {
                return ValidatePhoneNumber(sender);
            }

            // Email Address
            else if (sender.Contains("@") && !sender.StartsWith("@")) // Contains `@` but does not start with `@` - twitter ID starts with this.
            {
                return ValidateEmail(sender);
            }

            // Twitter ID
            else if (sender.StartsWith("@"))
            {
                return ValidateTwitterID(sender);
            }

            return "";
        }

    }
}
