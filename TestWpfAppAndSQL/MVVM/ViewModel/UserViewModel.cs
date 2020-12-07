using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace TestWpfAppAndSQL.MVVM.ViewModel
{
    class UserViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
