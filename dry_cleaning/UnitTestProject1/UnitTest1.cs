using Microsoft.VisualStudio.TestTools.UnitTesting;
using dry_cleaning.sql_connection;
using System;
using dry_cleaning.sql_query;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        DryCleaningConnection connection = new DryCleaningConnection();

        [TestMethod]
        public void CheckOnLoginQuery()
        {
            connection.openConnection();
            SqlCommand command = new SqlCommand(LoginQuery.LoginCheck, connection.GetConnection());
            command.Parameters.AddWithValue("@your_login", "admin1");
            SqlDataReader reader = command.ExecuteReader();

            reader.Read();
            
            Assert.IsTrue(reader.HasRows);

            reader.Close();
            connection.closeConnection();
        }

        [TestMethod]
        public void CheckOnAdminPasswordQuery()
        {
            connection.openConnection();
            SqlCommand command = new SqlCommand(LoginQuery.PasswordCheck, connection.GetConnection());
            command.Parameters.AddWithValue("@your_login", "admin1");
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            string readInfo = reader["Password"].ToString();

            reader.Close();
            connection.closeConnection();

            Assert.AreEqual(readInfo, "admin1");
        }

        [TestMethod]
        public void AdminRoleCheckQuery()
        {
            connection.openConnection();
            SqlCommand command = new SqlCommand(LoginQuery.RoleCheck, connection.GetConnection());
            command.Parameters.AddWithValue("@your_login", "admin1");
            SqlDataReader reader = command.ExecuteReader();

            reader.Read();
            string role = reader["Role"].ToString();

            reader.Close();
            connection.closeConnection();

            Assert.AreEqual(role, "admin");
        }

        [TestMethod]
        public void GetLastAddedClientQuery()
        {
            connection.openConnection();
            SqlCommand command1 = new SqlCommand(EmployeeQuery.GetLastAddedClient, connection.GetConnection());
            SqlDataReader reader1 = command1.ExecuteReader();

            reader1.Read();
            int client_id = Convert.ToInt32(reader1["client_id"].ToString());
            reader1.Close();

            Assert.AreEqual(client_id, 5);
        }

        [TestMethod]
        public void GetClientIdQurey()
        {
            connection.openConnection();
            SqlCommand command1 = new SqlCommand(LoginQuery.GetClientId, connection.GetConnection());
            command1.Parameters.AddWithValue("@your_login", "usere65pi");
            SqlDataReader reader1 = command1.ExecuteReader();

            reader1.Read();
            int client_id = Convert.ToInt32(reader1["Client_id"].ToString());
            reader1.Close();

            Assert.AreEqual(client_id, 4);
        }

        [TestMethod]
        public void CheckOnUserOrderData()
        {
            connection.openConnection();
            SqlCommand command = new SqlCommand($"SELECT * FROM OrderTable WHERE client_id = 4", connection.GetConnection());
            SqlDataReader reader = command.ExecuteReader();

            int rowCount = 0;
            while (reader.Read())
            {
                rowCount++;
            }

            reader.Close();
            connection.closeConnection();

            Assert.AreEqual(2, rowCount);
        }

        [TestMethod]
        public void CheckOnEmployeePasswordQuery()
        {
            connection.openConnection();
            SqlCommand command = new SqlCommand(LoginQuery.PasswordCheck, connection.GetConnection());
            command.Parameters.AddWithValue("@your_login", "employee1");
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            string readInfo = reader["Password"].ToString();

            reader.Close();
            connection.closeConnection();

            Assert.AreEqual(readInfo, "employee1");
        }

        [TestMethod]
        public void EmployeeRoleCheckQuery()
        {
            connection.openConnection();
            SqlCommand command = new SqlCommand(LoginQuery.RoleCheck, connection.GetConnection());
            command.Parameters.AddWithValue("@your_login", "employee1");
            SqlDataReader reader = command.ExecuteReader();

            reader.Read();
            string role = reader["Role"].ToString();

            reader.Close();
            connection.closeConnection();

            Assert.AreEqual(role, "employee");
        }

        [TestMethod]
        public void CheckOnUserPasswordQuery()
        {
            connection.openConnection();
            SqlCommand command = new SqlCommand(LoginQuery.PasswordCheck, connection.GetConnection());
            command.Parameters.AddWithValue("@your_login", "usere65pi");
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            string readInfo = reader["Password"].ToString();

            reader.Close();
            connection.closeConnection();

            Assert.AreEqual(readInfo, "b6781b48bl24");
        }

        [TestMethod]
        public void UserRoleCheckQuery()
        {
            connection.openConnection();
            SqlCommand command = new SqlCommand(LoginQuery.RoleCheck, connection.GetConnection());
            command.Parameters.AddWithValue("@your_login", "usere65pi");
            SqlDataReader reader = command.ExecuteReader();

            reader.Read();
            string role = reader["Role"].ToString();

            reader.Close();
            connection.closeConnection();

            Assert.AreEqual(role, "user");
        }
    }
}
