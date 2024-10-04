using dry_cleaning.sql_connection;
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
    public partial class Admin : Form
    {
        DryCleaningConnection dryCleaningConnection = new DryCleaningConnection();

        private List<SqlDataAdapter> dataAdapters;
        private List<BindingSource> bindingSources;
        
        public Admin()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(91, 161, 153);

            dataAdapters = new List<SqlDataAdapter>();
            bindingSources = new List<BindingSource>();

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

            dataAdapters.Add(new SqlDataAdapter("SELECT * FROM Good", dryCleaningConnection.getString()));
            bindingSources.Add(new BindingSource());

            addTable(dataAdapters[3], bindingSources[3], dataGridView4);

            dataAdapters.Add(new SqlDataAdapter("SELECT * FROM Employee", dryCleaningConnection.getString()));
            bindingSources.Add(new BindingSource());

            addTable(dataAdapters[4], bindingSources[4], dataGridView5);

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

        //обновить улсуги
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataAdapters[0].Update((DataTable)bindingSources[0].DataSource);

                addTable(dataAdapters[0], bindingSources[0], dataGridView1);
            }
            catch
            {
                MessageBox.Show("Не все данные были заполнены");
            }
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

        //обновить товары
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                dataAdapters[3].Update((DataTable)bindingSources[3].DataSource);

                addTable(dataAdapters[3], bindingSources[3], dataGridView4);
            }
            catch
            {
                MessageBox.Show("Не все данные были заполнены");
            }
        }

        //обновить сотрудники
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                dataAdapters[4].Update((DataTable)bindingSources[4].DataSource);

            addTable(dataAdapters[4], bindingSources[4], dataGridView5);
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
    }
}
