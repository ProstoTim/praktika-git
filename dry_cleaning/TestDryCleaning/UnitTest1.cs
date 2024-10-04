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
            // Установите логин и пароль администратора
            _loginForm.textBox1.Text = _adminLogin;
            _loginForm.textBox2.Text = _adminPassword;

            // Вызовите метод входа
            _loginForm.button1_Click(null, null);

            // Проверьте, что форма администратора открыта
            Assert.IsTrue(_loginForm.Visible == false);
            Assert.IsTrue(_loginForm.DialogResult == DialogResult.OK);
        }

        [Test]
        public void TestEmployeeLogin()
        {
            // Установите логин и пароль сотрудника
            _loginForm.textBox1.Text = _employeeLogin;
            _loginForm.textBox2.Text = _employeePassword;

            // Вызовите метод входа
            _loginForm.button1_Click(null, null);

            // Проверьте, что форма сотрудника открыта
            Assert.IsTrue(_loginForm.Visible == false);
            Assert.IsTrue(_loginForm.DialogResult == DialogResult.OK);
        }

        [Test]
        public void TestUserLogin()
        {
            // Установите логин и пароль пользователя
            _loginForm.textBox1.Text = _userLogin;
            _loginForm.textBox2.Text = _userPassword;

            // Вызовите метод входа
            _loginForm.button1_Click(null, null);

            // Проверьте, что форма пользователя открыта
            Assert.IsTrue(_loginForm.Visible == false);
            Assert.IsTrue(_loginForm.DialogResult == DialogResult.OK);
        }

        [Test]
        public void TestInvalidLogin()
        {
            // Установите неверный логин и пароль
            _loginForm.textBox1.Text = "invalid_login";
            _loginForm.textBox2.Text = "invalid_password";

            // Вызовите метод входа
            _loginForm.button1_Click(null, null);

            // Проверьте, что форма не открыта
            Assert.IsTrue(_loginForm.Visible == true);
            Assert.IsTrue(_loginForm.DialogResult == DialogResult.None);
        }

        [Test]
        public void TestInvalidPassword()
        {
            // Установите логин и неверный пароль
            _loginForm.textBox1.Text = _adminLogin;
            _loginForm.textBox2.Text = "invalid_password";

            // Вызовите метод входа
            _loginForm.button1_Click(null, null);

            // Проверьте, что форма не открыта
            Assert.IsTrue(_loginForm.Visible == true);
            Assert.IsTrue(_loginForm.DialogResult == DialogResult.None);
        }
    }
}