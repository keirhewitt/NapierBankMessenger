using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace NapierBankMessenger.MVVM.ViewModel
{
    /// <summary>
    /// Contains all methods needed for validation text fields
    /// </summary>
    public class DataValidationViewModel : ScriptableObject
    {
        private readonly string[] serious_incidents = {"Theft",
                                                        "Staff Attack",
                                                        "ATM Theft",
                                                        "Raid",
                                                        "Customer Attack",
                                                        "Staff Abuse",
                                                        "Bomb Threat",
                                                        "Terrorism",
                                                        "Suspicious Incident",
                                                        "Intelligence",
                                                        "Cash Loss" };
        private readonly string[] email_format_sequences = { "@hotmail.com", 
                                                                "@gmail.com", 
                                                                "@live.napier.ac.uk",
                                                                "@outlook.com", 
                                                                "@nb.gov"};
        private Int16 _messageType;
        private DateTime _dt;

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

        // Make sure date is in format dd/MM/yy
        public string ValidateDate(string date)
        {
            if (!StringHasSpace(date))
                return "Invalid format, must be in format: SIR dd/mm/yy";

            // Split string into 'SIR', 'dd/mm/yy' - correct formatting
            string[] senderFormatted = date.Split(' ');

            if (senderFormatted.Length > 2)
                return "Invalid format, must be in format: SIR dd/mm/yy";

            // Parse to this format
            string format = "dd/MM/yy";
            DateTime dateTime;

            // Try to parse dd/mm/yy section of string as DateTime of format : dd/mm/yy
            if (!DateTime.TryParseExact(senderFormatted[1], format, new CultureInfo("en-US"),
                DateTimeStyles.None, out dateTime))
            { 
                return "Invalid date format!";
            }
            return "";
        }

        // Validates a sort code to be in format xx-xx-xx or xxxxxx
        public string ValidateSortCode(string sortcode)
        {
            // Regex for finding sortcodes, allows no dashes
            Regex regX = new Regex(@"\b([0-9]{2})-?([0-9]{2})-?([0-9]{2})\b");
            Match m = regX.Match(sortcode);

            if (!m.Success)
            {
                return "Sort code not valid!";
            }
            return "";
        }

        // Splits body into 2 lines
        // Ensures 2 line formatting
        // Ensures strict Incident Type is chosen
        public string ValidateSortCodeBody(string body)
        {
            string incident = "";

            // Return error if body does not have a line break
            if (!body.Contains(Environment.NewLine))
                return "Body must be in format: xx-xx-xx \\n <Incident Type>";
            string[] lines = body.Split('\n');

            // Return error if body does not have 2 separate lines
            if (lines.Length != 2)
                return "Body must be in format: xx-xx-xx \\n <Incident Type>";
            incident = lines[1];
            
            // Check each serious incident against 2nd body line
            foreach (string i in serious_incidents)
            {
                if (incident.Equals(i))
                {
                    return "";
                }
            }
            return "Invalid serious incident type.";
        }

        // Make sure a string can be Split()
        public bool StringHasSpace(string str)
        {
            return str.Any(i => Char.IsWhiteSpace(i));
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
            if (MessageType != 0 && MessageType != 2 && MessageType != -1)
            {
                // Check if either Email or SIR
                if (subject.StartsWith("SIR"))
                    MessageType = 3;
                // Only validate Subject field if Message type is Email
                if (MessageType == 1)
                {
                    // Email cannot have empty subject
                    if (string.IsNullOrWhiteSpace(subject))
                        return "Email cannot have empty subject!";

                    // If Sender is 1 (Email)
                    if (subject.Length > 20)
                    {
                        return "Subject must be a max of 20 characters.";
                    }
                }
                else if (MessageType == 3)
                {
                    return ValidateDate(subject);
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

            // SIR
            else if (MessageType == 3)
            {
                return ValidateSortCodeBody(body);
            }

            return "";
        }
    }
}
