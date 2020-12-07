using System;
using System.Windows;
using TestWpfAppAndSQL.MVVM;

namespace TestWpfAppAndSQL
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        private NomenclatureView nomenclatureView;
        private Nomenclature nomenclature;

        public Add()
        {
            InitializeComponent();
            nomenclature = new Nomenclature();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text != null && PriceBox.Text != null)
            {
                nomenclatureView = new NomenclatureView(nomenclature);
                nomenclatureView.Add.Execute(nomenclature);
                this.DialogResult = true;
            }
        }

        private void NameBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            nomenclature.Name = NameBox.Text;
        }

        private void PriceBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            nomenclature.Price = Decimal.Parse(PriceBox.Text);
        }

        private void FromDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            nomenclature.DateFrom = (DateTime)FromDatePicker.SelectedDate;
        }

        private void ToDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            nomenclature.DateTo = (DateTime)ToDatePicker.SelectedDate;
        }
    }
}
