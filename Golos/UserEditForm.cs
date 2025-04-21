using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Voiting
{
    public partial class UserEditForm : Form
    {
        private readonly int? _userId;
        private readonly string _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Mi\Documents\Database312.mdb;Persist Security Info=False;";

        public UserEditForm(int? userId = null)
        {
            InitializeComponent();
            _userId = userId;
            InitializeForm();
        }

        private void InitializeForm()
        {
            Text = _userId.HasValue ? "Редактирование пользователя" : "Новый пользователь";
            LoadRoles();
            if (_userId.HasValue) LoadUserData();
        }

        private void LoadRoles()
        {
            cmbRole.Items.AddRange(new[] { "Admin", "Commission", "Observer", "User" });
            cmbRole.SelectedIndex = 0;
        }

        private void LoadUserData()
        {
            try
            {
                using (var conn = new OleDbConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new OleDbCommand("SELECT * FROM Users WHERE UserID = @UserID", conn);
                    cmd.Parameters.Add("@UserID", OleDbType.Integer).Value = _userId.Value;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtLogin.Text = reader["Login"].ToString();
                            txtPassword.Text = reader["Password"].ToString();
                            cmbRole.SelectedItem = reader["Role"].ToString();
                            txtFullName.Text = reader["FullName"].ToString();
                            txtContact.Text = reader["ContactInfo"].ToString();
                            chkIsActive.Checked = Convert.ToBoolean(reader["isActive"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                if (_userId.HasValue)
                {
                    UpdateUser();
                }
                else
                {
                    CreateUser();
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show($"Ошибка базы данных: {ex.Message}\n\nПроверьте:\n1. Уникальность логина\n2. Корректность данных",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неожиданная ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateUser()
        {
            using (var conn = new OleDbConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new OleDbCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "INSERT INTO Users (Login, Password, Role, FullName, ContactInfo, isActive) " +
                        "VALUES (@Login, @Password, @Role, @FullName, @ContactInfo, @IsActive)";

                    AddParameters(cmd);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateUser()
        {
            using (var conn = new OleDbConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new OleDbCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText =
                        "UPDATE Users SET " +
                        "Login = @Login, " +
                        "Password = @Password, " +
                        "Role = @Role, " +
                        "FullName = @FullName, " +
                        "ContactInfo = @ContactInfo, " +
                        "isActive = @IsActive " +
                        "WHERE UserID = @UserID";

                    AddParameters(cmd);
                    cmd.Parameters.Add("@UserID", OleDbType.Integer).Value = _userId.Value;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void AddParameters(OleDbCommand cmd)
        {
            cmd.Parameters.Add("@Login", OleDbType.VarWChar).Value = txtLogin.Text.Trim();
            cmd.Parameters.Add("@Password", OleDbType.VarWChar).Value = txtPassword.Text.Trim();
            cmd.Parameters.Add("@Role", OleDbType.VarWChar).Value = cmbRole.SelectedItem.ToString();
            cmd.Parameters.Add("@FullName", OleDbType.VarWChar).Value = txtFullName.Text.Trim();
            cmd.Parameters.Add("@ContactInfo", OleDbType.VarWChar).Value = txtContact.Text.Trim();
            cmd.Parameters.Add("@IsActive", OleDbType.Boolean).Value = chkIsActive.Checked;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Введите логин пользователя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Введите пароль пользователя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}