
namespace NapierBankMessenger.MVVM.Model
{
    public class SIR : Message
    {
        private string _incident_type;

        public SIR(string id, string body_subject, string body_main, string incident_type) : base(id, body_subject, body_main)
        {
            _incident_type = incident_type;
        }
        public string getIncidentType() {  return _incident_type; }
    }
}
