using NapierBankMessenger.MVVM.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NapierBankMessenger.MVVM.ViewModel
{
    /// <summary>
    /// Controller passes this class all of the relevant End of Session data to be displayed in it's counterpart View
    /// </summary>
    public class EndOfSessionViewModel : ScriptableObject
    {
        private Controller Ctrl;

        public Dictionary<string, int> TwitterMentions
        {
            get => Ctrl.TwitterMentions;
        }

        public Dictionary<string, int> TrendingList
        {
            get => Ctrl.TrendingList;
        }

        public ObservableCollection<SIR> SIRs
        {
            get => Ctrl.SIRs;
        }

        public EndOfSessionViewModel(Controller ctrl)
        {
            Ctrl = ctrl;
        }

    }
}
