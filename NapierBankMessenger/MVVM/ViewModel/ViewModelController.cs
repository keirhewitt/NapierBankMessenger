
using NapierBankMessenger.MVVM.View;
using NapierBankMessenger.Commands;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class ViewModelController : ScriptableObject
    {
        public HomePage homePage { get; }

        private object _currentView;
        private object _messageListView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public object MessageView
        {
            get { return _messageListView; }
            set
            {
                _messageListView = value;
                OnPropertyChanged();
            }
        }

        public ViewModelController()
        {
            homePage = new HomePage();
            MessageView = homePage;
        }
    }
}
