
namespace NapierBankMessenger.MVVM.Model
{
    public class SIR : Email
    {
        private readonly string _incident_type;

        public SIR(string type, string sender, string body, string subject, string incident_type) : base(type, sender, body, subject)
        {
            _incident_type = incident_type;
        }

        public string GetIncidentType() {  return _incident_type; }
    }
}
