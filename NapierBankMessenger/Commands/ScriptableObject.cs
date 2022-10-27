
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NapierBankMessenger.MVVM.ViewModel
{
    public class ScriptableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
