using System;
using System.Windows;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace TestWpfAppAndSQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AuthWindow Authorizate;
        private bool Authorized = false;
        private string connectionString;
        
        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Authorizate = new AuthWindow();
            Authorizate.auth += Close_Auth_window;
            
        }

        private void Close_Auth_window()
        {
            Authorizate.Close();
            Authorized = true;
            LoadNomenclatures();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Authorizate.Show();
        }

        private void LoadNomenclatures()
        {
            DataTable Nomenclature = new DataTable();
            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand("sel_nomenclature", connect)
                    {
                        CommandType = CommandType.StoredProcedure
                    }))
                    {
                        connect.Open();
                        adapter.Fill(Nomenclature);
                        NomenclatureGrid.ItemsSource = Nomenclature.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(Authorized)
            {

            }
        }
    }
}
