using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace TestWpfAppAndSQL
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        string connectionString;
        public string ViewModel { get; set; }
        public bool Authorized;
        public void ShowViewModel()
        {
            MessageBox.Show(ViewModel);
        }

        public AuthWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Authorized = false;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            bool ExistUser = false;
            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    if (LoginBox.Text != null && PassBox.Text != null)
                    {
                        using (SqlCommand command = new SqlCommand("check_authentication", connect))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Login", SqlDbType.VarChar).Value = LoginBox.Text;
                            command.Parameters.Add("@Pass", SqlDbType.VarChar).Value = PassBox.Text;
                            SqlParameter returnUser = command.Parameters.Add("@existUser", SqlDbType.Bit);
                            returnUser.Direction = ParameterDirection.Output;
                            
                            connect.Open();
                            int temp = command.ExecuteNonQuery();
                            if (temp != 0)
                                ExistUser = (bool)returnUser.Value;
                            if (ExistUser)
                            {
                                MessageBox.Show("Authorization successful!");
                                Authorized = true;
                            }
                            else MessageBox.Show("Wrong login or password");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
