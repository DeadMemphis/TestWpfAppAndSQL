using System;
using System.Windows;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Controls;

namespace TestWpfAppAndSQL
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        private string connectionString;
        private DataRowView rowToEdit;
        private DateTime From;
        private DateTime To;
        private int selectedRowId;
        public event EditEvent editRow;
        public delegate void EditEvent();

        public DataRowView RowToEdit
        {
            get { return rowToEdit; }
            set
            {
                rowToEdit = value;
            }
        }
        public Edit()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text != null && PriceBox.Text != null)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand("iud_nomenclature", connect))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Id", SqlDbType.Int).Value = selectedRowId;
                            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = NameBox.Text;
                            decimal price;
                            Decimal.TryParse(PriceBox.Text, out price);
                            command.Parameters.Add("@Price", SqlDbType.Decimal).Value = price;
                            command.Parameters.Add("@FromDate", SqlDbType.Date).Value = FromDatePicker.SelectedDate;
                            command.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDatePicker.SelectedDate;
                            command.Parameters.Add("@FLAG", SqlDbType.NVarChar).Value = 'U';
                            SqlParameter returnResult = new SqlParameter("returnVal", SqlDbType.Int);
                            returnResult.Direction = ParameterDirection.ReturnValue;
                            command.Parameters.Add(returnResult);

                            connect.Open();
                            command.ExecuteScalar();
                            int result = (int)returnResult.Value;

                            if (result == 0)
                            {
                                MessageBox.Show("Changes saved!");
                                editRow?.Invoke();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            selectedRowId = (int)rowToEdit.Row.ItemArray[0];
            NameBox.Text = rowToEdit.Row.ItemArray[1].ToString();
            PriceBox.Text = rowToEdit.Row.ItemArray[2].ToString();
            DateTime.TryParse(rowToEdit.Row.ItemArray[3].ToString(), out From);
            FromDatePicker.SelectedDate = From;
            DateTime.TryParse(rowToEdit.Row.ItemArray[4].ToString(), out To);
            ToDatePicker.SelectedDate = To;
        }
    }
}
