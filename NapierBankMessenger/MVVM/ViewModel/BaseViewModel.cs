
using System.ComponentModel;


namespace NapierBankMessenger.MVVM.ViewModel
{
    internal class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public void OnChanged(string val)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(val));
        }
    }
}
