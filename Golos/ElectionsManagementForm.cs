using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Voiting
{
    public partial class ElectionsManagementForm : Form
    {
        private readonly string _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Mi\Documents\Database312.mdb;";
        private readonly int _currentUserId;

        public ElectionsManagementForm(int currentUserId)
        {
            InitializeComponent();
            _currentUserId = currentUserId;

            // Инициализация интерфейса
            ConfigureDataGridView();
            ConfigureButtons();
            LoadElections();
        }

        private void ConfigureDataGridView()
        {
            dgvElections.AutoGenerateColumns = false;
            dgvElections.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvElections.MultiSelect = false;
            dgvElections.AllowUserToAddRows = false;
            dgvElections.ReadOnly = true;

            // Очистка существующих колонок
            dgvElections.Columns.Clear();

            // Добавление колонок
            dgvElections.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ElectionID",
                DataPropertyName = "ElectionID",
                HeaderText = "ID",
                Visible = false
            });

            dgvElections.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Title",
                DataPropertyName = "Title",
                HeaderText = "Название выборов",
                Width = 200
            });

            dgvElections.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StartDate",
                DataPropertyName = "StartDate",
                HeaderText = "Дата начала",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" }
            });

            dgvElections.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EndDate",
                DataPropertyName = "EndDate",
                HeaderText = "Дата окончания",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" }
            });

            dgvElections.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                DataPropertyName = "Status",
                HeaderText = "Статус",
                Width = 120
            });
        }

        private void ConfigureButtons()
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;

            // Обработчик изменения выделения
            dgvElections.SelectionChanged += (s, e) =>
            {
                bool hasSelection = dgvElections.SelectedRows.Count > 0;
                btnEdit.Enabled = hasSelection;
                btnDelete.Enabled = hasSelection;
            };
        }

        private void LoadElections()
        {
            try
            {
                using (var conn = new OleDbConnection(_connectionString))
                using (var cmd = new OleDbCommand(
                    "SELECT ElectionID, Title, StartDate, EndDate, Status FROM Elections ORDER BY StartDate DESC",
                    conn))
                {
                    var dt = new DataTable();
                    var adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(dt);

                    dgvElections.DataSource = dt;

                    // Автоматическая настройка ширины столбцов
                    foreach (DataGridViewColumn column in dgvElections.Columns)
                    {
                        if (column.Visible)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        }
                    }
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show($"Ошибка базы данных: {ex.Message}\nПроверьте подключение и существование таблицы Elections.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неожиданная ошибка: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new EditElectionForm(_currentUserId))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadElections(); // Обновляем данные после добавления
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании выборов: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvElections.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите выборы для редактирования",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var electionId = Convert.ToInt32(dgvElections.SelectedRows[0].Cells["ElectionID"].Value);

                using (var form = new EditElectionForm(_currentUserId, electionId))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadElections(); // Обновляем данные после редактирования
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании выборов: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvElections.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите выборы для удаления",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvElections.SelectedRows[0];
            var electionId = Convert.ToInt32(selectedRow.Cells["ElectionID"].Value);
            var electionTitle = selectedRow.Cells["Title"].Value.ToString();

            var confirmResult = MessageBox.Show(
                $"Вы уверены, что хотите удалить выборы '{electionTitle}'?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new OleDbConnection(_connectionString))
                    using (var cmd = new OleDbCommand(
                        "DELETE FROM Elections WHERE ElectionID = @ElectionID",
                        conn))
                    {
                        cmd.Parameters.AddWithValue("@ElectionID", electionId);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Выборы успешно удалены",
                                "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadElections(); // Обновляем список
                        }
                    }
                }
                catch (OleDbException ex)
                {
                    MessageBox.Show($"Ошибка базы данных при удалении: {ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Неожиданная ошибка: {ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadElections();
        }
    }
}