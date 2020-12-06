using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestWpfAppAndSQL.MVVM
{
    class User : INotifyPropertyChanged
    {
        private string login;
        private string pass;

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Name");
            }
        }

        public string Password
        {
            get { return pass; }
            set
            {
                pass = value;
                OnPropertyChanged("Password");
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
