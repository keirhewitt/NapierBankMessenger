using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class DataValidationViewModel : ScriptableObject
    {
        private readonly string[] email_format_sequences = { "@hotmail.com", "@gmail.com", "@live.napier.ac.uk" };
        private Dictionary<string, int> _flags;


        // Contains flags for each field --> 0: Phone, 1: Email, 2: Tweet
        // For Subject: 3 = Not Required, -1 = Validation Error
        // Int array of size: 3 (Sender, subject, body)
        public Dictionary<string, int> Flags
        {
            get => _flags;
            set
            {
                _flags = value;
                Debug.Write("\n\n\n\n");
                Debug.Write(_flags.Values);
                Debug.Write("\n\n\n\n");
                OnPropertyChanged();
            }
        }

        // Initialise flags as errors for Sender + Body (Empty on page load),
        // 3 for Subject (not required by default/no error)
        public DataValidationViewModel()
        {
            _flags = new Dictionary<string, int> { ["Sender"] = -1, ["Subject"] = 3, ["Body"] = -1 };
        }

        // Return the type of message prefice
        public string GetMessageType()
        {
            if (Flags["Sender"] == 0) { return "S"; }
            else if (Flags["Sender"] == 1) { return "E"; }
            else if (Flags["Sender"] == 2) { return "T"; }
            return null;
        }

        // Data validation for Input forms
        public bool ReadyToParseData()
        {
            // Check if any of the Flags have been set to -1 (Error)
            return !Flags.Any(i => i.Value == -1);

        }

        // Check if the sender field contains any of the email substring addresses allowed
        public bool CheckForEmail(string sender_line)
        {
            return email_format_sequences.Any(sender_line.Contains);
        }

        // Validate a Phone Number in the Sender Field
        public string ValidatePhoneNumber(string phoneNumber)
        {
            // String contains only numeric values, and does not begin with '+'
            if (!phoneNumber.All(char.IsDigit) && !phoneNumber.StartsWith("+"))
            {
                Flags["Sender"] = -1;
                return "Phone number must contain only numeric characters.";
            }

            // If string begins with numeric char
            else if (Regex.IsMatch(phoneNumber, @"^\d"))
            {
                if (phoneNumber.Length > 12)
                {
                    Flags["Sender"] = -1;
                    return "Phone Number must be 12 characters long!";
                }
            }

            // Deal with +44, add 2 digits on to max length for allowance
            else if (phoneNumber.StartsWith("+44"))
            {
                if (phoneNumber.Length > 14)
                {
                    Flags["Sender"] = -1;
                    return "Phone Number must be 12 characters long!";
                }
            }

            // Set the 1st Flag = 0 if no error
            else
            {
                Flags["Sender"] = 0;
            }
            return "";
        }

        // Validate an Email Address in the Sender Field
        public string ValidateEmail(string emailaddress)
        {
            // Email address does not match with any substring of email addresses
            if (!CheckForEmail(emailaddress))
            {
                Flags["Sender"] = -1;
                return "Not a valid email address.";
            }

            // Too long
            else if (emailaddress.Length > 320)
            {
                Flags["Sender"] = -1;
                return "Invalid email address - too large!";
            }
            // Set the 1st Flag = 1 if no error
            else
            {
                Flags["Sender"] = 1;
            }
            return "";
        }

        // Validate a Twitter ID in the Sender Field
        public string ValidateTwitterID(string twitterID)
        {
            // Twitter ID must be between 4 and 16 chars (inclusive)
            if (twitterID.Length > 16 || twitterID.Length < 4)
            {
                Flags["Sender"] = -1;
                return "Twitter ID must be between 4 and 16 characters.";
            }

            // Set the 1st Flag = 2 if no error
            else
            {
                Flags["Sender"] = 2;
            }
            return "";
        }

        // Ensure a correct Sender format, returns Error message as a tooltip when hovering over the Sender Text Box
        public string ValidateSenderField(string sender)
        {
            // Initial Null or Empty check
            if (string.IsNullOrWhiteSpace(sender))
            {
                Flags["Sender"] = -1;
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

        // Validate the subject text field for each message type.
        public string ValidateSubject(string subject)
        {
            // Only validate Subject field if Message type is Email
            if (Flags["Sender"] == 1)
            {
                // If Sender is 1 (Email)
                if (subject.Length > 20)
                {
                    Flags["Subject"] = -1;
                    return "Subject must be a max of 20 characters.";
                }
                else
                {
                    Flags["Subject"] = 1;
                }
            } else
            {
                Flags["Subject"] = 3;
            }
            return "";
        }

        // Validate the body text field for each message type.
        public string ValidateBody(string body)
        {
            // Check first for any empty body messages if Sender field is VALID
            if (Flags["Sender"] != -1 && string.IsNullOrWhiteSpace(body))
            {
                Flags["Body"] = -1;
                return "Cannot submit empty message.";
            }
            // If Message is SMS
            else if (Flags["Sender"] == 0)
            {
                if (body.Length > 140)
                {
                    Flags["Body"] = -1;
                    return "SMS message cannot be longer than 140 characters.";
                }
                else
                {
                    Flags["Body"] = 0;
                }
            }

            // Email
            else if (Flags["Sender"] == 1)
            {
                if (body.Length > 1028)
                {
                    Flags["Body"] = -1;
                    return "Email message cannot be longer than 1028 characters.";
                }
                else
                {
                    Flags["Body"] = 1;
                }

            }

            // Tweet
            else if (Flags["Sender"] == 2)
            {
                if (body.Length > 140)
                {
                    Flags["Body"] = -1;
                    return "Tweet cannot be longer than 140 characters.";
                }
                else
                {
                    Flags["Body"] = 2;
                }

            }

            else
            {
                return "";
            }

            return "";
        }
    }
}
