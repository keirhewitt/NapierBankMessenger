using System.Text.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NapierBankMessenger.MVVM.ViewModel;

namespace NapierBankMessenger.MVVM.Model 
{
    public class Controller : ScriptableObject
    {
        private ObservableCollection<Message> _messages;
        private ObservableCollection<SIR> _sirList;
        private Dictionary<string, int> _twitterMentions;  // List of all mentions in any session
        private Dictionary<string, int> _trendingList; // List of all hashtags in any session
        private List<string> _quarantineList; // List of quarantined URLs

        public Dictionary<string,int> TwitterMentions
        {
            get => _twitterMentions;
            set => _twitterMentions = value;
        }

        public Dictionary<string,int> TrendingList
        {
            get => _trendingList;
            set => _trendingList = value;
        }

        public List<string> QuarantineList
        {
            get => _quarantineList;
            set => _quarantineList = value;
        }

        public ObservableCollection<SIR> SIRs 
        { 
            get => _sirList;
            set => _sirList = value; 
        }

        public ObservableCollection<Message> Messages 
        { 
            get => _messages; 
            set => _messages = value;
        }

        // Initialise all collections
        public Controller()
        {
            _messages = new ObservableCollection<Message>();
            _twitterMentions = new Dictionary<string, int>();
            _trendingList = new Dictionary<string, int>();
        }

        public void SendMessageToJSON(Message message)
        {

        }

        public void LoadMessagesFromJSON()
        {

        }

        public void AddMessage(Message message) 
        {  
            if (message.GetMessageType() == "T")
            {

            }
            _messages.Add(message); 
        }
        public void RemoveMessage(Message message) 
        { 
            _messages.Remove(message); 
        }

        public void AddMention(string mention) 
        { 
            if (_twitterMentions.ContainsKey(mention)) 
            {
                _twitterMentions[mention]++;    // Increment index if already present in dictionary
            } 
            else
            {
                _twitterMentions.Add(mention, 1); // Else add and set value to 1
            }
        }

        public void RemoveMention(string mention)
        {
            if (_twitterMentions.ContainsKey(mention))
            {
                _twitterMentions[mention]--;    // Increment index if already present in dictionary
                if (_twitterMentions[mention] == 0)
                {
                    _twitterMentions.Remove(mention);
                }
            }
        }

        public void AddTrendingTopic(string topic)
        {
            if (_trendingList.ContainsKey(topic))
            {
                _trendingList[topic]++;    // Increment index if already present in dictionary
            }
            else
            {
                _trendingList.Add(topic, 1); // Else add and set value to 1
            }
        }
        public void RemoveTrendingTopic(string topic)
        {
            if (_trendingList.ContainsKey(topic))
            {
                _trendingList[topic]--;    // Increment index if already present in dictionary
                if (_trendingList[topic] == 0)
                {
                    _trendingList.Remove(topic);
                }
            }
        }
        public void AddSIR(SIR sir) { _sirList.Add(sir); }
        public void RemoveSIR(SIR sir) { _sirList.Remove(sir); }
    }
}
