
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NapierBankMessenger.MVVM.ViewModel;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace NapierBankMessenger.MVVM.Model 
{
    /// <summary>
    /// Controller acts as a centralized point for all Model data
    /// Handles the storage and manipulation of Message objects
    /// </summary>
    public class Controller : ScriptableObject
    {
        // JSON file variables
        protected readonly string filePath = Directory.GetCurrentDirectory();
        protected readonly string[] filePaths = { @"C:\Users", "Keir", "Desktop", "courseworkdata" };
        protected readonly string fileName = @"export.json";
        private string fullpath;

        private List<Message> serializableList;

        private ObservableCollection<Message> _messages;// Where all of the messages are stored, Messages property will access this
        private ObservableCollection<SIR> _sirList;     // Where all of the SIRs are stored, SIRs property will access this

        private Dictionary<string, int> _mentionsList;  // Collection of all mentions in any session
        private Dictionary<string, int> _trendingList;  // Collection of all hashtags in any session
        private Dictionary<string, int> _quarantineList;// Collection of quarantined URLs

        // Creating all publically accessible objects
        public Dictionary<string,int> TwitterMentions
        {
            get => _mentionsList;
            set => _mentionsList = value;
        }

        public Dictionary<string,int> TrendingList
        {
            get => _trendingList;
            set => _trendingList = value;
        }

        public Dictionary<string, int> QuarantineList
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
            _sirList = new ObservableCollection<SIR>();
            _mentionsList = new Dictionary<string, int>();
            _trendingList = new Dictionary<string, int>();
            _quarantineList = new Dictionary<string, int>();
            InitJSONFile(fileName);
        }

        // Creates JSON file if one does not exist in location
        public void InitJSONFile(string filename)
        {
            fullpath = Path.Combine(filePaths);
            if (!FileExists(fullpath, filename))
            {
                FileStream stream = File.Create(fullpath + "/" + filename);
            }          
        }

        // Checks if file <filename> exists in location <directory>
        private bool FileExists(string directory, string filename)
        {
            DirectoryInfo dir = new DirectoryInfo(directory);
            FileInfo[] files = dir.GetFiles();
            foreach(FileInfo file in files)
            {
                if (file.Name == filename)
                    return true;
            }
            return false;
        }

        // Returns the full path to JSON file if it exists
        private string ReturnJSONFilepath()
        {
            DirectoryInfo dir = new DirectoryInfo(fullpath);
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.Name == fileName)
                    return file.FullName;
            }
            return "";
        }

        // Sends list of messages to JSON file
        public void SendToJSON()
        {
            ConvertToSerializableList();
            string x = JsonConvert.SerializeObject(
                serializableList, 
                Formatting.Indented, 
                new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                });

            File.WriteAllText(ReturnJSONFilepath(), x);
        }

        // Converts objects to a serializable List<Message>
        private void ConvertToSerializableList()
        {
            serializableList = _messages.ToList();       
        }

        // Add Hashtags and Mentions to the relevant lists
        public void AddTweet(Tweet tweet)
        {
            foreach(string hashtag in tweet.GetHashtags())
                AddToDictionary(TrendingList, hashtag);
            foreach(string mention in tweet.GetMentions())
                AddToDictionary(TwitterMentions, mention);

            Messages.Add(tweet);              
        }

        // Add any Email URLs to quarantine list
        public void AddEmail(Email email)
        {
            foreach (string q_url in email.GetQuarantinedURLs())
                AddToDictionary(QuarantineList, q_url);

            Messages.Add(email);
        }

        // No extra functionality required for SMS
        public void AddSMS(SMS sms)
        {
            Messages.Add(sms);
        }

        // Add SIR to SIR list only
        public void AddSIR(SIR sir)
        {
            foreach(string q_url in sir.GetQuarantinedURLs())
                AddToDictionary(QuarantineList, q_url);
            SIRs.Add(sir);
            Messages.Add(sir);
            
        }

        // Shorten amount code for these operations
        public void AddToDictionary(Dictionary<string,int> dict, string key)
        {
            if (dict.ContainsKey(key))
                dict[key]++; // Increment already present key's value
            else
                dict[key] = 1;  // Add key and instantiate value to 1 (amount of instances of key in dict)
        }

        // .. . shortened code
        public void RemoveFromDictionary(Dictionary<string, int> dict, string key)
        {
            if (dict.ContainsKey(key))
            {
                dict[key]--;
                if (dict[key] == 0)
                {
                    dict.Remove(key);
                }
            }
            else
                return;
        }

        // Determines message type and handles each in separate functions
        public void AddMessage(Message message) 
        {
            if (message is Tweet)
                AddTweet((Tweet)message);
            else if (message is SIR)
                AddSIR((SIR)message);
            else if (message is Email)
                AddEmail((Email)message);
            else if (message is SMS)
                AddSMS((SMS)message);     
            else
                return;
        }

        // Removes message if present in collection
        public void RemoveMessageFromCollection(ObservableCollection<Message> collection, Message message)
        {
            // Match unique headers
            var x = collection.Where(X => X.GetHeader() == message.GetHeader()).Single();
            if (x != null)
                collection.Remove(x);

        }

        // Take dictionary and re-order it by Value (ascending)
        public void OrderDictionary(Dictionary<string, int> dict)
        {
            dict = dict.OrderBy(key => key.Value).ToDictionary(key => key.Key, key => key.Value);
        }

        // Returns the dictionary of Quarantined URLS (mainly for testing)
        public Dictionary<string, int> GetQuarantinedURLs() { return _quarantineList; }

        // Returns the dictionary of SIRs (mainly for testing)
        public ObservableCollection<SIR> GetSIRList() { return _sirList; }

    }
}
