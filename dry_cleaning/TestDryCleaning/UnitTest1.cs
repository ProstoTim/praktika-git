using dry_cleaning;
using dry_cleaning.sql_connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TestDryCleaning
{
    [TestFixture]
    public class Tests
    {
        private Login _loginForm;
        private DryCleaningConnection _dryCleaningConnection;
        private string _adminLogin = "admin_login";
        private string _adminPassword = "admin_password";
        private string _employeeLogin = "employee_login";
        private string _employeePassword = "employee_password";
        private string _userLogin = "user_login";
        private string _userPassword = "user_password";

        [SetUp]
        public void Setup()
        {
            _loginForm = new Login();
            _dryCleaningConnection = new DryCleaningConnection();
        }

        [Test]
        public void TestAdminLogin()
        {
            // ���������� ����� � ������ ��������������
            _loginForm.textBox1.Text = _adminLogin;
            _loginForm.textBox2.Text = _adminPassword;

            // �������� ����� �����
            _loginForm.button1_Click(null, null);

            // ���������, ��� ����� �������������� �������
            Assert.IsTrue(_loginForm.Visible == false);
            Assert.IsTrue(_loginForm.DialogResult == DialogResult.OK);
        }

        [Test]
        public void TestEmployeeLogin()
        {
            // ���������� ����� � ������ ����������
            _loginForm.textBox1.Text = _employeeLogin;
            _loginForm.textBox2.Text = _employeePassword;

            // �������� ����� �����
            _loginForm.button1_Click(null, null);

            // ���������, ��� ����� ���������� �������
            Assert.IsTrue(_loginForm.Visible == false);
            Assert.IsTrue(_loginForm.DialogResult == DialogResult.OK);
        }

        [Test]
        public void TestUserLogin()
        {
            // ���������� ����� � ������ ������������
            _loginForm.textBox1.Text = _userLogin;
            _loginForm.textBox2.Text = _userPassword;

            // �������� ����� �����
            _loginForm.button1_Click(null, null);

            // ���������, ��� ����� ������������ �������
            Assert.IsTrue(_loginForm.Visible == false);
            Assert.IsTrue(_loginForm.DialogResult == DialogResult.OK);
        }

        [Test]
        public void TestInvalidLogin()
        {
            // ���������� �������� ����� � ������
            _loginForm.textBox1.Text = "invalid_login";
            _loginForm.textBox2.Text = "invalid_password";

            // �������� ����� �����
            _loginForm.button1_Click(null, null);

            // ���������, ��� ����� �� �������
            Assert.IsTrue(_loginForm.Visible == true);
            Assert.IsTrue(_loginForm.DialogResult == DialogResult.None);
        }

        [Test]
        public void TestInvalidPassword()
        {
            // ���������� ����� � �������� ������
            _loginForm.textBox1.Text = _adminLogin;
            _loginForm.textBox2.Text = "invalid_password";

            // �������� ����� �����
            _loginForm.button1_Click(null, null);

            // ���������, ��� ����� �� �������
            Assert.IsTrue(_loginForm.Visible == true);
            Assert.IsTrue(_loginForm.DialogResult == DialogResult.None);
        }
    }
}