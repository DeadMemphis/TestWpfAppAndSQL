using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;


namespace TestWpfAppAndSQL
{
    /// <summary>
    /// Interaction logic for DeleteModal.xaml
    /// </summary>
    public partial class DeleteModal : Window
    {
        private DataRowView rowToDelete;
        private string connectionString;

        public DataRowView RowToDelete
        {
            get { return rowToDelete; }
            set
            {
                rowToDelete = value;
            }
        }

        public DeleteModal()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("iud_nomenclature", connect))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = rowToDelete.Row.ItemArray[0];
                        command.Parameters.Add("@FLAG", SqlDbType.NVarChar).Value = 'D';
                        SqlParameter returnResult = new SqlParameter("returnVal", SqlDbType.Int);
                        returnResult.Direction = ParameterDirection.ReturnValue;
                        command.Parameters.Add(returnResult);

                        connect.Open();
                        command.ExecuteScalar();
                        int result = (int)returnResult.Value;

                        if (result == 0)
                        {
                            MessageBox.Show("Deleted!");
                            this.DialogResult = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CanсelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
