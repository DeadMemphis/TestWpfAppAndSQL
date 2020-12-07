using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestWpfAppAndSQL.Data;

namespace TestWpfAppAndSQL.MVVM
{
    class User : INotifyPropertyChanged
    {
        private Command check;
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

        public bool Authorized { get; set; }

        public Command CheckAuthenticate
        {
            get
            {
                return check ??
                (check = new Command(obj =>
                {
                    User user = obj as User;
                    if (user != null)
                        Authorized = SQLManager.GetInstance().CheckAuth(user);
                }));
            }
        }

        public bool CanCheck
        {
            get { return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
