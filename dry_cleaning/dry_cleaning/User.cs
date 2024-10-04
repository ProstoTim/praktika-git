using dry_cleaning.sql_connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dry_cleaning
{
    public partial class User : Form
    {
        DryCleaningConnection dryCleaningConnection = new DryCleaningConnection();

        private SqlDataAdapter dataAdapters;
        private BindingSource bindingSources;
        private int clientId;

        public User(int clientId)
        {
            InitializeComponent();

            this.BackColor = Color.FromArgb(91, 161, 153);
            label1.ForeColor = Color.FromArgb(221, 190, 170);

            dataGridView1.ReadOnly = true;

            this.clientId = clientId;

            dataLoad();
        }

        //загрузка начальных данных
        private void dataLoad()
        {
            dataAdapters = new SqlDataAdapter($"SELECT * FROM OrderTable WHERE client_id = {clientId}", dryCleaningConnection.getString());
            bindingSources = new BindingSource();

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapters);
            DataTable table = new DataTable();
            dataAdapters.Fill(table);
            bindingSources.DataSource = table;
            dataGridView1.DataSource = bindingSources;

        }

        //переход на страницу логина
        private void User_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }
    }
}
