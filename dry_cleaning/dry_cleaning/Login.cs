using dry_cleaning.sql_connection;
using dry_cleaning.sql_query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dry_cleaning
{
    public partial class Login : Form
    {
        DryCleaningConnection dryCleaningConnection = new DryCleaningConnection();

        public Login()
        {
            InitializeComponent();

            this.BackColor = Color.FromArgb(70, 149, 151);
            label1.ForeColor = Color.FromArgb(221,190,170);
            label2.ForeColor = Color.FromArgb(221, 190, 170);
            label3.ForeColor = Color.FromArgb(221, 190, 170);
            button1.ForeColor = Color.FromArgb(91, 161, 153);
        }

        //вход
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!loginCheck())
                    throw new ArgumentException("Неверный логин");

                if (passwordCheck())
                {
                    dryCleaningConnection.openConnection();
                    SqlCommand command = new SqlCommand(LoginQuery.RoleCheck, dryCleaningConnection.GetConnection());
                    command.Parameters.AddWithValue("@your_login", textBox1.Text);
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();
                    string role = reader["Role"].ToString();

                    dryCleaningConnection.closeConnection();

                    //проверка на роль пользователя
                    if (role == "employee")
                    {
                        this.Hide();
                        Employee employee = new Employee();
                        employee.ShowDialog();
                    }
                    else if (role == "admin")
                    {
                        this.Hide();
                        Admin admin = new Admin();
                        admin.ShowDialog();
                    }
                    else if (role == "user")
                    {
                        this.Hide();

                        dryCleaningConnection.openConnection();
                        SqlCommand command2 = new SqlCommand(LoginQuery.GetClientId, dryCleaningConnection.GetConnection());
                        command2.Parameters.AddWithValue("@your_login", textBox1.Text);
                        SqlDataReader reader2 = command2.ExecuteReader();

                        reader2.Read();
                        int clientId = Convert.ToInt32(reader2["Client_id"].ToString());

                        dryCleaningConnection.closeConnection();

                        User user = new User(clientId);
                        user.ShowDialog();
                    }
                    else
                        throw new ArgumentException("Неверная роль");

                }
                else
                    throw new ArgumentException("Неверный пароль");
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //проверка на логин
        public bool loginCheck()
        {
            dryCleaningConnection.openConnection();
            SqlCommand command = new SqlCommand(LoginQuery.LoginCheck, dryCleaningConnection.GetConnection());
            command.Parameters.AddWithValue("@your_login", textBox1.Text);
            SqlDataReader reader = command.ExecuteReader();

            reader.Read();

            if (reader.HasRows)
            {
                reader.Close();
                dryCleaningConnection.closeConnection();

                return true;
            }
            else
            {
                reader.Close();
                dryCleaningConnection.closeConnection();

                return false;
            }

            
        }

        //проверка на пароль
        public bool passwordCheck()
        {
            dryCleaningConnection.openConnection();
            SqlCommand command = new SqlCommand(LoginQuery.PasswordCheck, dryCleaningConnection.GetConnection());
            command.Parameters.AddWithValue("@your_login", textBox1.Text);
            SqlDataReader reader = command.ExecuteReader();

            reader.Read();

            string readInfo = reader["Password"].ToString();

            reader.Close();
            dryCleaningConnection.closeConnection();

            if (textBox2.Text.Equals(readInfo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
