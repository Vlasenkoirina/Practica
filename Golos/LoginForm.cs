using System;
using System.Data.OleDb;
using System.Windows.Forms;
using Voting;

namespace Voiting
{
    public partial class LoginForm : Form
    {
        private const string ConnectionString =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Mi\Documents\Database312.mdb;Persist Security Info=False;";

        public LoginForm()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
            this.Text = "Авторизация";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new OleDbConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT UserID, Role, FullName 
                        FROM Users 
                        WHERE Login = @Login 
                          AND Password = @Password 
                          AND IsActive = True";

                    using (var cmd = new OleDbCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Login", txtLogin.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userId = Convert.ToInt32(reader["UserID"]);
                                string fullName = reader["FullName"].ToString();
                                string role = reader["Role"].ToString();

                                this.Hide();
                                new MainForm(userId, fullName, role).ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Неверные учетные данные или учетная запись неактивна!",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show($"Ошибка базы данных:\n{ex.Message}\n\n" +
                               "Проверьте:\n" +
                               "1. Установлен ли Microsoft Access Database Engine\n" +
                               "2. Существует ли файл базы данных\n" +
                               "3. Не открыта ли база в другой программе",
                               "Ошибка подключения",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неожиданная ошибка:\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}