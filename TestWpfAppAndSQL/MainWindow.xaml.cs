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
        private Edit EditWindow;
        private Add AddWindow;
        private DeleteModal DeleteWindow;
        private bool Authorized = false;
        private string connectionString;
        

        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void Close_Auth_Window()
        {
            Authorizate.Close();
            Authorized = true;
            LoadNomenclatures();
        }

        private void Close_Edit_Window()
        {
            EditWindow.Close();
            LoadNomenclatures();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Authorizate = new AuthWindow();
            Authorizate.auth += Close_Auth_Window;
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (Authorized)
            {
                AddWindow = new Add();
                AddWindow.Show();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if(NomenclatureGrid.SelectedItem != null && Authorized)
            {
                EditWindow = new Edit();
                EditWindow.editRow += Close_Edit_Window;
                EditWindow.RowToEdit = (DataRowView)NomenclatureGrid.SelectedItem;
                EditWindow.Show();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteWindow = new DeleteModal();
            DeleteWindow.RowToDelete = (DataRowView)NomenclatureGrid.SelectedItem;
            DeleteWindow.ShowDialog();
            if (DeleteWindow.DialogResult == true) LoadNomenclatures();
        }
    }
}
