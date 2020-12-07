using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using TestWpfAppAndSQL.MVVM;

namespace TestWpfAppAndSQL.Data
{
    class SQLManager
    {
        public readonly string ConnectionString;
        private readonly static SQLManager instance = new SQLManager();
        
        private SQLManager()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public static SQLManager GetInstance()
        {
            return instance;
        }

        public void Add(Nomenclature nomenclature)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("iud_nomenclature", connect))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = nomenclature.Name;
                        command.Parameters.Add("@Price", SqlDbType.Decimal).Value = nomenclature.Price;
                        command.Parameters.Add("@FromDate", SqlDbType.Date).Value = nomenclature.DateFrom;
                        command.Parameters.Add("@ToDate", SqlDbType.Date).Value = nomenclature.DateTo;
                        command.Parameters.Add("@FLAG", SqlDbType.NVarChar).Value = 'I';
                        SqlParameter returnResult = new SqlParameter("returnVal", SqlDbType.Int);
                        returnResult.Direction = ParameterDirection.ReturnValue;
                        command.Parameters.Add(returnResult);

                        connect.Open();
                        command.ExecuteScalar();
                        int result = (int)returnResult.Value;

                        if (result == 0)
                            MessageBox.Show("Record added!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Edit(Nomenclature nomenclature)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("iud_nomenclature", connect))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = nomenclature.Id;
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = nomenclature.Name;
                        command.Parameters.Add("@Price", SqlDbType.Decimal).Value = nomenclature.Price;
                        command.Parameters.Add("@FromDate", SqlDbType.Date).Value = nomenclature.DateFrom;
                        command.Parameters.Add("@ToDate", SqlDbType.Date).Value = nomenclature.DateTo;
                        command.Parameters.Add("@FLAG", SqlDbType.NVarChar).Value = 'U';
                        SqlParameter returnResult = new SqlParameter("returnVal", SqlDbType.Int);
                        returnResult.Direction = ParameterDirection.ReturnValue;
                        command.Parameters.Add(returnResult);

                        connect.Open();
                        command.ExecuteScalar();
                        int result = (int)returnResult.Value;

                        if (result == 0)
                            MessageBox.Show("Changes saved!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("iud_nomenclature", connect))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        command.Parameters.Add("@FLAG", SqlDbType.NVarChar).Value = 'D';
                        SqlParameter returnResult = new SqlParameter("returnVal", SqlDbType.Int);
                        returnResult.Direction = ParameterDirection.ReturnValue;
                        command.Parameters.Add(returnResult);

                        connect.Open();
                        command.ExecuteScalar();
                        int result = (int)returnResult.Value;

                        if (result == 0)
                            MessageBox.Show("Deleted!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ObservableCollection<Nomenclature> LoadData()
        {
            ObservableCollection<Nomenclature> nomenclatures = new ObservableCollection<Nomenclature>();
            try
            {
                using (SqlConnection connect = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sel_nomenclature", connect)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        connect.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            decimal price = reader.GetDecimal(2);
                            DateTime from = reader.GetDateTime(3);
                            DateTime to = reader.GetDateTime(4);

                            nomenclatures.Add(new Nomenclature()
                            {
                                Id = id,
                                Name = name,
                                Price = price,
                                DateFrom = from,
                                DateTo = to
                            });
                        }
                        return nomenclatures;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return nomenclatures;
        }

        public bool CheckAuth(User user)
        {
            bool exist = false;
            try
            {
                using (SqlConnection connect = new SqlConnection(ConnectionString))
                {

                    using (SqlCommand command = new SqlCommand("check_authentication", connect))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Login", SqlDbType.NVarChar).Value = user.Login;
                        command.Parameters.Add("@Pass", SqlDbType.NVarChar).Value = user.Password;
                        SqlParameter returnUser = command.Parameters.Add("@existUser", SqlDbType.Bit);
                        returnUser.Direction = ParameterDirection.Output;

                        connect.Open();
                        int temp = command.ExecuteNonQuery();
                        if (temp != 0)
                            exist = (bool)returnUser.Value;
                        return exist;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

    }
}
