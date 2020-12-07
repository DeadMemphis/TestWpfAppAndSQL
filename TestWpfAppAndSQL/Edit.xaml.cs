using System;
using System.Windows;
using System.Windows.Controls;
using TestWpfAppAndSQL.MVVM;

namespace TestWpfAppAndSQL
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        private Nomenclature nomenclatureToEdit;
        private int selectedRowId;
        private bool loaded = false;

        public event EditEvent editRow;
        public delegate void EditEvent();

        public Nomenclature NomenclatureToEdit
        {
            get { return nomenclatureToEdit; }
            set
            {
                nomenclatureToEdit = value;
            }
        }
        public Edit()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            NomenclatureView nomView = new NomenclatureView(NomenclatureToEdit);
            nomView.Edit.Execute(nomenclatureToEdit);
            this.DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            selectedRowId = nomenclatureToEdit.Id;
            NameBox.Text = nomenclatureToEdit.Name;
            PriceBox.Text = nomenclatureToEdit.Price.ToString();
            FromDatePicker.SelectedDate = nomenclatureToEdit.DateFrom;
            ToDatePicker.SelectedDate = nomenclatureToEdit.DateTo;
            SaveButton.IsEnabled = false;
            loaded = true;
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (loaded)
            {
                nomenclatureToEdit.Name = NameBox.Text;
                SaveButton.IsEnabled = true;
            }
        }

        private void PriceBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (loaded)
            {
                nomenclatureToEdit.Price = Decimal.Parse(PriceBox.Text);
                SaveButton.IsEnabled = true;
            }
        }

        private void FromDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaded)
            {
                nomenclatureToEdit.DateFrom = (DateTime)FromDatePicker.SelectedDate;
                SaveButton.IsEnabled = true;
            }
        }

        private void ToDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaded)
            {
                nomenclatureToEdit.DateTo = (DateTime)ToDatePicker.SelectedDate;
                SaveButton.IsEnabled = true;
            }
        }
    }
}
