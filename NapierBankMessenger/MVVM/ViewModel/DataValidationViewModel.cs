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
        private Int16 _messageType;

        // Message Type corresponds to SMS, Email, Tweet, SIR
        public Int16 MessageType
        {
            get => _messageType;
            set
            {
                _messageType = value;
                OnPropertyChanged();
            }
        }

        // Initialise message type as -1: error
        public DataValidationViewModel()
        {
            _messageType = -1;
        }

        // Return the type of message
        public Int16 GetMessageType()
        {
            return MessageType;
        }

        // Data validation for Input forms
        public bool ReadyToParseData()
        {
            // Check if message type is not -1 (OK)
            return MessageType != -1;

        }

        // Check if the Sender field contains any of the email substring addresses allowed
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
                return "Phone number must contain only numeric characters.";
            }

            // If string begins with numeric char
            else if (Regex.IsMatch(phoneNumber, @"^\d"))
            {
                if (phoneNumber.Length > 12)
                {
                    return "Phone Number must be 12 characters long!";
                }
            }

            // Deal with +44, add 2 digits on to max length for allowance
            else if (phoneNumber.StartsWith("+44"))
            {
                if (phoneNumber.Length > 14)
                {
                    return "Phone Number must be 12 characters long!";
                }
            }
            return "";
        }

        // Validate an Email Address in the Sender Field
        public string ValidateEmail(string emailaddress)
        {
            // Email address does not match with any substring of email addresses
            if (!CheckForEmail(emailaddress))
            {
                return "Not a valid email address.";
            }

            // Too long
            else if (emailaddress.Length > 320)
            {
                return "Invalid email address - too large!";
            }
            return "";
        }

        // Validate a Twitter ID in the Sender Field
        public string ValidateTwitterID(string twitterID)
        {
            // Twitter ID must be between 4 and 16 chars (inclusive)
            if (twitterID.Length > 16 || twitterID.Length < 4)
            {
                return "Twitter ID must be between 4 and 16 characters.";
            }
            return "";
        }

        // Ensure a correct Sender format, returns Error message as a tooltip when hovering over the Sender Text Box
        public string ValidateSenderField(string sender)
        {
            // Initial Null or Empty check
            if (string.IsNullOrWhiteSpace(sender))
            {
                MessageType = -1;
                return "Sender field cannot be empty!";
            }

            // Phone Number
            if (Regex.IsMatch(sender, @"^\d") || sender.StartsWith("+44"))
            {
                MessageType = 0;
                return ValidatePhoneNumber(sender);
            }

            // Email Address
            else if (sender.Contains("@") && !sender.StartsWith("@")) // Contains `@` but does not start with `@` - twitter ID starts with this.
            {
                MessageType = 1;
                return ValidateEmail(sender);
            }

            // Twitter ID
            else if (sender.StartsWith("@"))
            {
                MessageType = 2;
                return ValidateTwitterID(sender);
            }

            return "";
        }

        // Validate the subject text field for each message type.
        public string ValidateSubject(string subject)
        {
            // Only validate Subject field if Message type is Email
            if (MessageType == 1)
            {
                // If Sender is 1 (Email)
                if (subject.Length > 20)
                {
                    return "Subject must be a max of 20 characters.";
                }

            } 
            return "";
        }

        // Validate the Body text field for each message type.
        public string ValidateBody(string body)
        {
            // Check first for any empty Body messages if Sender field is VALID
            if (MessageType != -1 && string.IsNullOrWhiteSpace(body))
            {
                return "Cannot submit empty message.";
            }

            // If Message is SMS
            else if (MessageType == 0)
            {
                if (body.Length > 140)
                {
                    return "SMS message cannot be longer than 140 characters.";
                }
            }

            // Email
            else if (MessageType == 1)
            {
                if (body.Length > 1028)
                {
                    return "Email message cannot be longer than 1028 characters.";
                }

            }

            // Tweet
            else if (MessageType == 2)
            {
                if (body.Length > 140)
                {
                    return "Tweet cannot be longer than 140 characters.";
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
