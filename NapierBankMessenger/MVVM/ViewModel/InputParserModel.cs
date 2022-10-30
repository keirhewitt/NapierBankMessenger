
using NapierBankMessenger.Commands;
using NapierBankMessenger.MVVM.View;
using System.Diagnostics;
using System.Windows.Input;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class InputParserModel : ViewModelController
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
