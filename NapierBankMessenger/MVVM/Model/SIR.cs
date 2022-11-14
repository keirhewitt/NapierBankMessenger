
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NapierBankMessenger.MVVM.Model
{
    public class SIR : Email
    {
        private readonly string _incident_type;

        public SIR(string sender, string body, string subject) : base(sender, body, subject)
        {
            SetType("SIR");
            _incident_type = subject;
            FormatBody();
        }

        public string GetIncidentType() {  return _incident_type; }

        // Override ToString() for unique properties and change formatting
        public override string ToString()
        {
            return "Date: " + GetSender() +
                    "\n" +
                    "Nature Of Incident: " + GetSubject() +
                    "\n" +
                    GetBody();
        }
    }
}
