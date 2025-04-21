using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using Voting.Models;

namespace Voiting
{
    public partial class UsersManagementForm : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Mi\Documents\Database312.mdb;Persist Security Info=False;Jet OLEDB:Database Password=;";

        public UsersManagementForm()
        {
            InitializeComponent();
            ConfigureDataGridView();
            LoadUsers();
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            // Добавляем все колонки
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "UserID",
                DataPropertyName = "UserID",
                HeaderText = "ID",
                ReadOnly = true
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Login",
                DataPropertyName = "Login",
                HeaderText = "Логин"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Role",
                DataPropertyName = "Role",
                HeaderText = "Роль"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "FullName",
                DataPropertyName = "FullName",
                HeaderText = "ФИО"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ContactInfo",
                DataPropertyName = "ContactInfo",
                HeaderText = "Контактная информация"
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "isActive",
                DataPropertyName = "isActive",
                HeaderText = "Активен"
            });
        }

        private void LoadUsers()
        {
            try
            {
                DataTable dt = new DataTable();

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                using (OleDbCommand cmd = new OleDbCommand("SELECT UserID, Login, Role, FullName, ContactInfo, isActive FROM Users", conn))
                {
                    conn.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }

                // Обновляем DataGridView в UI-потоке
                if (dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke(new Action(() =>
                    {
                        dataGridView1.DataSource = dt;
                        dataGridView1.Refresh();
                    }));
                }
                else
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.Refresh();
                }
            }
            catch (Exception ex)
            {
                ShowError("Ошибка загрузки пользователей", ex);
            }
        }
        private void ShowError(string message, Exception ex = null)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ShowError(message, ex)));
                return;
            }

            string errorMessage = message;
            if (ex != null)
            {
                errorMessage += $"\n\nДетали:\n{ex.Message}";
                if (ex is OleDbException oleEx)
                {
                    errorMessage += $"\nКод ошибки: {oleEx.ErrorCode}";
                }
            }

            MessageBox.Show(this, errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new UserEditForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Обновляем данные после добавления
                    LoadUsers();

                    // Прокручиваем к последней записи
                    if (dataGridView1.Rows.Count > 0)
                        dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для редактирования!");
                return;
            }

            var userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["UserID"].Value);
            using (var form = new UserEditForm(userId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadUsers();

                    // Возвращаем выделение на редактируемую строку
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (Convert.ToInt32(row.Cells["UserID"].Value) == userId)
                        {
                            row.Selected = true;
                            dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                            break;
                        }
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                ShowError("Выберите пользователя для удаления");
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int userId = Convert.ToInt32(selectedRow.Cells["UserID"].Value);
            string userName = selectedRow.Cells["FullName"].Value?.ToString() ?? "неизвестный пользователь";

            if (MessageBox.Show(this,
                $"Вы уверены, что хотите удалить пользователя {userName}?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                bool success = DeleteUser(userId);

                if (success)
                {
                    LoadUsers(); // Перезагружаем данные
                    MessageBox.Show(this, "Пользователь успешно удалён", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при удалении пользователя", ex);
            }
        }

        private bool DeleteUser(int userId)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            using (OleDbCommand cmd = new OleDbCommand("DELETE FROM Users WHERE UserID = ?", conn))
            {
                cmd.Parameters.AddWithValue("UserID", userId);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }
    }
}