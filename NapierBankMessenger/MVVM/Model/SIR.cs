
namespace NapierBankMessenger.MVVM.Model
{
    /// <summary>
    /// SIR has every property of email, except Incident Type
    /// </summary>
    public class SIR : Email
    {
        private string _incident_type;

        public SIR(string sender, string body, string subject) : base(sender, body, subject)
        {
            _incident_type = subject;           
            FormatBody();
            FormatHeader();
            SetIncidentType();
        }

        // Custom SIR header
        public override void FormatHeader()
        {
            SetHeader("SIR" + IDSelector.ToString("000000000"));
        }

        public void SetIncidentType()
        {
            string[] bodytext = GetBody().Split('\n');
            this._incident_type = bodytext[1];
        }

        public string GetIncidentType() 
        {
            return _incident_type;  
        }

        // Override ToString() for unique properties and change formatting
        public override string ToString()
        {
            return "Date: " + GetSubject() +
                    "\n" +
                    "Nature Of Incident: " + GetIncidentType() +
                    "\n" +
                    GetSender();
        }
    }
}
