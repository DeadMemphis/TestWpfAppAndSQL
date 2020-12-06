using System;
using System.Windows;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace TestWpfAppAndSQL
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        private string connectionString;
        public Add()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
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
                            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = NameBox.Text;
                            decimal price;
                            Decimal.TryParse(PriceBox.Text, out price);
                            command.Parameters.Add("@Price", SqlDbType.Decimal).Value = price;
                            command.Parameters.Add("@FromDate", SqlDbType.Date).Value = FromDatePicker.SelectedDate;
                            command.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDatePicker.SelectedDate;
                            command.Parameters.Add("@FLAG", SqlDbType.NVarChar).Value = 'I';
                            SqlParameter returnResult = new SqlParameter("returnVal", SqlDbType.Int);
                            returnResult.Direction = ParameterDirection.ReturnValue;
                            command.Parameters.Add(returnResult);

                            connect.Open(); 
                            command.ExecuteScalar();
                            int result = (int)returnResult.Value;

                            //if (ExistUser)
                            //{
                            //    MessageBox.Show("Authorization successful!");
                            //    auth?.Invoke();
                            //}
                            //else MessageBox.Show("Wrong login or password");
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
}
