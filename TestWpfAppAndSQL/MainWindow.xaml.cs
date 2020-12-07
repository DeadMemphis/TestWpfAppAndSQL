using System;
using System.Windows;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using TestWpfAppAndSQL.MVVM;
using TestWpfAppAndSQL.Data;

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
        private NomenclatureViewModel model = new NomenclatureViewModel();


        public MainWindow()
        {
            InitializeComponent();
            AddButton.IsEnabled = false;
            EditButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }

        private void Close_Auth_Window()
        {
            Authorizate.Close();
            Authorized = true;
            NomenclatureGrid.DataContext = model;
            NomenclatureGrid.ItemsSource = model.Nomenclatures;
            AddButton.IsEnabled = true;
            EditButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
        }

        private void Close_Edit_Window()
        {
            EditWindow.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Authorizate = new AuthWindow();
            Authorizate.auth += Close_Auth_Window;
            Authorizate.Show();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow = new Add();
            AddWindow.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (NomenclatureGrid.SelectedItem != null && Authorized)
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
            //if (DeleteWindow.DialogResult == true) LoadNomenclatures();
        }
    }
}
