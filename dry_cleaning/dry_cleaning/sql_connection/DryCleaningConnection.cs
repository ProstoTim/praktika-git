using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dry_cleaning.sql_connection
{
    public class DryCleaningConnection
    {
        private string connectionString = "Data Source=adclg1;Initial Catalog=!!!Шарафутдинов_УП;Trusted_Connection=True;";
        private SqlConnection connection;

        public DryCleaningConnection()
        {
            connection = new SqlConnection(connectionString);
        }

        public void openConnection()
        {
            try
            {
                connection.Open();
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка открытия подключения: " + ex.Message);
            }
        }

        public void closeConnection()
        {
            try
            {
                connection.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка закрытия подключения: " + ex.Message);
            }
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }

        public string getString()
        {
            return connectionString;
        }
    }
}
