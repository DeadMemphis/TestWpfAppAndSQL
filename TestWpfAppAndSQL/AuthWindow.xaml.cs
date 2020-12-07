using System.Windows;
using TestWpfAppAndSQL.MVVM;

namespace TestWpfAppAndSQL
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public event AuthEvent auth; 
        public delegate void AuthEvent();

        private User user = new User();

        public AuthWindow()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            user.Login = LoginBox.Text;
            user.Password = PassBox.Password;
            if (LoginBox.Text != null && PassBox.Password != null)
            {
                user.CheckAuthenticate.Execute(user);
                if (user.Authorized)
                {
                    MessageBox.Show("Authorization successful!");
                    auth?.Invoke();
                }
                else MessageBox.Show("Wrong login or password");
            }
        }
    }
}
