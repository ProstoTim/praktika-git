using dry_cleaning.sql_connection;
using dry_cleaning.sql_query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace dry_cleaning
{
    public partial class Employee : Form
    {
        DryCleaningConnection dryCleaningConnection = new DryCleaningConnection();

        private List<SqlDataAdapter> dataAdapters;
        private List<BindingSource> bindingSources;

        private static Random random = new Random();

        public Employee()
        {
            InitializeComponent();

            this.BackColor = Color.FromArgb(91, 161, 153);

            dataAdapters = new List<SqlDataAdapter>();
            bindingSources = new List<BindingSource>();

            dataGridView1.ReadOnly = true;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView3.AllowUserToDeleteRows= false;

            dataLoad();

        }

        //загрузка начальных данных
        private void dataLoad()
        {
            dataAdapters.Add(new SqlDataAdapter("SELECT * FROM Service", dryCleaningConnection.getString()));
            bindingSources.Add(new BindingSource());

            addTable(dataAdapters[0], bindingSources[0], dataGridView1);

            dataAdapters.Add(new SqlDataAdapter("SELECT * FROM Client", dryCleaningConnection.getString()));
            bindingSources.Add(new BindingSource());

            addTable(dataAdapters[1], bindingSources[1], dataGridView2);

            dataAdapters.Add(new SqlDataAdapter("SELECT * FROM OrderTable", dryCleaningConnection.getString()));
            bindingSources.Add(new BindingSource());

            addTable(dataAdapters[2], bindingSources[2], dataGridView3);

        }

        //добавление адаптера к dgv
        private void addTable(SqlDataAdapter da, BindingSource bs, DataGridView dgv)
        {
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
            DataTable table = new DataTable();
            da.Fill(table);
            bs.DataSource = table;
            dgv.DataSource = bs;
        }

        //обновить услуги
        private void button1_Click(object sender, EventArgs e)
        {
            dataAdapters[0].Update((DataTable)bindingSources[0].DataSource);

            addTable(dataAdapters[0], bindingSources[0], dataGridView1);
        }

        //обновить клиенты
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataAdapters[1].Update((DataTable)bindingSources[1].DataSource);

                addTable(dataAdapters[1], bindingSources[1], dataGridView2);
            }
            catch
            {
                MessageBox.Show("Не все данные были заполнены");
            }
        }

        //обновить заказы
        private void button3_Click(object sender, EventArgs e)
        {
            try 
            {
                dataAdapters[2].Update((DataTable)bindingSources[2].DataSource);

                addTable(dataAdapters[2], bindingSources[2], dataGridView3);
            }
            catch
            {
                MessageBox.Show("Не все данные были заполнены");
            }
            
        }

        //закрытие
        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }

        //добавление заказа (показывает логин и пароль пользователя последнего добавленного заказа)
        private void button4_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);

            dryCleaningConnection.openConnection();
            SqlCommand command1 = new SqlCommand(EmployeeQuery.GetLastAddedClient, dryCleaningConnection.GetConnection());
            SqlDataReader reader1 = command1.ExecuteReader();

            reader1.Read();
            int client_id = Convert.ToInt32(reader1["client_id"].ToString());
            reader1.Close();

            SqlCommand command2 = new SqlCommand(EmployeeQuery.UserLoginCheck, dryCleaningConnection.GetConnection());
            command2.Parameters.AddWithValue("@clientOnOrder", client_id);

            SqlDataReader reader2 = command2.ExecuteReader();
            reader2.Read();

            string login = string.Empty;
            string password = string.Empty;

            if (!reader2.HasRows)
            {
                reader2.Close();

                bool exceptionThrown = true;
                while (exceptionThrown)
                {
                    try
                    {
                        login = CreateNewLogin();

                        password = CreateNewPassword();

                        SqlCommand command3 = new SqlCommand(EmployeeQuery.CreateNewUserClient, dryCleaningConnection.GetConnection());
                        command3.Parameters.AddWithValue("@NewUser", login);
                        command3.Parameters.AddWithValue("@NewClient_id", client_id);
                        command3.Parameters.AddWithValue("@NewPassword", password);

                        command3.ExecuteNonQuery();

                        exceptionThrown = false;
                    }
                    catch (Exception)
                    {
                        // повторное выполнение кода
                        exceptionThrown = true;
                    }
                }
                
            }
            else
            {
                login = reader2["Login"].ToString();

                SqlCommand command4 = new SqlCommand(EmployeeQuery.DecryptPassword, dryCleaningConnection.GetConnection());
                command4.Parameters.AddWithValue("@UserLogin", login);
                reader2.Close();

                SqlDataReader psreader = command4.ExecuteReader();
                psreader.Read();
                password= psreader["Password"].ToString();
                psreader.Close();
            }
            
            dryCleaningConnection.closeConnection();

            MessageBox.Show($"Логин пользователя: {login} пароль: {password}");
        }


        //создание логина
        private string CreateNewLogin()
        {
            string login = "user";

            for (int i = 0; i < 5; i++)
            {
                int charType = random.Next(2);

                if (charType == 0)
                {
                    // генерация случайной буквы
                    char letter = (char)('a' + random.Next(26));
                    login += letter;
                }
                else if (charType == 1)
                {
                    // генерация случайной цифры
                    char digit = (char)('0' + random.Next(10));
                    login += digit;
                }
            }

            return login;
        }

        //создание пароля
        private string CreateNewPassword()
        {
            string password = "";

            for (int i = 0; i < 12; i++)
            {
                int charType = random.Next(2);

                if (charType == 0)
                {
                    // генерация случайной буквы
                    char letter = (char)('a' + random.Next(26));
                    password += letter;
                }
                else if (charType == 1)
                {
                    // генерация случайной цифры
                    char digit = (char)('0' + random.Next(10));
                    password += digit;
                }
                
            }
            return password;
        }
    }


}
