using System.Windows;
using TestWpfAppAndSQL.MVVM;

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
        private NomenclatureViewList model = new NomenclatureViewList();


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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Authorizate = new AuthWindow();
            Authorizate.auth += Close_Auth_Window;
            Authorizate.Show();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow = new Add();
            AddWindow.ShowDialog();
            model.Load();
            NomenclatureGrid.DataContext = model;
            NomenclatureGrid.ItemsSource = model.Nomenclatures;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (NomenclatureGrid.SelectedItem != null && Authorized)
            {
                EditWindow = new Edit();
                EditWindow.NomenclatureToEdit = (Nomenclature)NomenclatureGrid.SelectedItem;
                EditWindow.ShowDialog();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteWindow = new DeleteModal();
            DeleteWindow.NomenclatureToDelete = (Nomenclature)NomenclatureGrid.SelectedItem;
            DeleteWindow.ShowDialog();
            model.Load();
            NomenclatureGrid.DataContext = model;
            NomenclatureGrid.ItemsSource = model.Nomenclatures;
        }
    }
}
