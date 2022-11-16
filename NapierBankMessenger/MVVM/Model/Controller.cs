using System.Text.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NapierBankMessenger.MVVM.ViewModel;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace NapierBankMessenger.MVVM.Model 
{
    public class Controller : ScriptableObject
    {
        protected readonly string filePath = Directory.GetCurrentDirectory();
        protected readonly string[] filePaths = { @"C:\Users", "Keir", "Desktop", "courseworkdata" };
        protected readonly string fileName = @"export.json";

        private string fullpath;

        private ObservableCollection<Message> _messages;
        private ObservableCollection<SIR> _sirList;
        private List<Message> serializableList;
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
            InitJSONFile();
        }

        // Creates JSON file if one does not exist in location
        public void InitJSONFile()
        {
            fullpath = Path.Combine(filePaths);
            if (!FileExists(fullpath, fileName))
            {
                FileStream stream = File.Create(fullpath + "/" + fileName);
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
            File.WriteAllText(ReturnJSONFilepath(), JsonConvert.SerializeObject(serializableList, Formatting.Indented));          
        }

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

        // Take dictionary and re-order it by Value
        public void OrderDictionary(Dictionary<string, int> dict)
        {
            dict = dict.OrderBy(key => key.Value).ToDictionary(key => key.Key, key => key.Value);
        }

        // TEST FUNCTION
        private void TestFunction()
        {
            Message testMsg1 = new SMS("07854215232", "Yeah I heard ROTFL !");
            Message testMsg2 = new Email
                (
                    "keir11@hotmail.com",
                    "Hi all, this is a test email.",
                    "RE: How are you?"
                );
            Message testMsg3 = new Tweet
                (
                    "@Twitter_Support",
                    "Twitter will never ask for your login details #support #secure"
                );
            Message testMsg4 = new SMS("+448843221230", "Just got in the house sorry! Yeah I can make it. SLAP mate. ");
            Message testMsg5 = new Email
                (
                    "victoria.jast@hotmail.com", "Good Morning\n\n, " +
                    "I’ve recently had work done on the cast iron downpipe outside my flat and was advised to get an ASAP fix.\n" +
                    "All pieces are damaged and leaking, as well as some cracked brackets – is this something that you could fix ?\n" +
                    "Images of https://www.wayfair.co.uk/garden/sb0/conversations-sets-c1876293.htmls the damage attached above.\n" +
                    "Thanks for your time.\n" +
                    "Keir",
                    "Cast Iron Downpipe Query"
                );
            Message testMsg6 = new Email("emmet30@yahoo.com", "I've just received this. Thank you.", "RE: Today's headings!!");
            Message testMsg7 = new Tweet("@_001sfc", "Gerard Piqué announces he is retiring from football and will play his last match for Barcelona this Saturday #WorldCup");
            Message testMsg8 = new SIR("harryJones@hotmail.com", "222343\\nATM Theft", "SIR 11/10/22"); 
            Message testMsg9 = new SMS("09968458932", "Leaving in 10. SYS");
            Message testMsg10 = new Tweet
                (
                    "@Keir__HEWitt",
                    "World Cup soon! #WorldCup #WorldCup #Football"
                );
            Message testMsg11 = new SIR("keir1@gmail.com", "832325\\nTheft", "SIR 15/11/22");
            Message testMsg12 = new Tweet
                (
                    "@dodhria",
                    "Update mais 🇧🇷PATRIOTA @twitter_user223 do Eurotruck! #EuroTruck #Spain #Relaxing \n " +
                    "GRANDE DIA!!! 👍👍👍"
                );

            /* !! Add these using the above methods !! */
            AddMessage(testMsg1);
            AddMessage(testMsg2);
            AddMessage(testMsg3);
            AddMessage(testMsg4);
            AddMessage(testMsg5);
            AddMessage(testMsg6);
            AddMessage(testMsg7);
            AddMessage(testMsg8);
            AddMessage(testMsg9);
            AddMessage(testMsg10);
            AddMessage(testMsg11);
            AddMessage(testMsg12);


        }
    }
}
