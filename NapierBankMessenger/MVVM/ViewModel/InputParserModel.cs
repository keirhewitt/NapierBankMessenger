
using NapierBankMessenger.MVVM.View;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class InputParserModel : ScriptableObject
    {
        public InputParser InputParserView { get; }
        public object _ipView;

        public object View
        {
            get { return _ipView; }
            set
            {
                _ipView = value;
                OnPropertyChanged();
            }
        }

        public InputParserModel()
        {
            InputParserView = new InputParser();
            View = _ipView;
        }
    }
}
